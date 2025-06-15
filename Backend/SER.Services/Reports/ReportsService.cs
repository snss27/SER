using System.Linq;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using SER.Database;
using SER.Database.Models.Students;
using SER.Domain.EducationLevels.Enums;
using SER.Domain.Groups.Enums;
using SER.Domain.Reports.Grouping;
using SER.Domain.Reports.Grouping.Enums;
using SER.Domain.Reports.Grouping.Extensions;
using SER.Domain.Reports.Selection;
using SER.Domain.Services;
using SER.Domain.Students;
using SER.Domain.Students.Enums;
using SER.Domain.Students.StudentsFilters.Enums;
using SER.Services.Students.Extensions;
using SER.Tools.Types;
using SER.Tools.Types.IDs;

namespace SER.Services.Reports;
public class ReportsService(SERDbContext dbContext) : IReportsService
{
	public async Task<Byte[]> Generate(ReportGroupingOptionsDto groupingOptions, ReportSelectionOptionsDto selectionOptions)
	{
		IQueryable<StudentEntity> query = dbContext.Students
			.AsNoTracking()
			.Include(s => s.Group)
				.ThenInclude(g => g.EducationLevel)
			.Include(s => s.Group)
				.ThenInclude(g => g.Curator)
			.Include(s => s.Group)
				.ThenInclude(g => g.Cluster)
			.Include(s => s.WorkPlaces)
				.ThenInclude(wp => wp.Enterprise)
			.Include(s => s.AdditionalQualifications)
			.Include(s => s.TargetAgreementEnterprise);

		if (groupingOptions.Gender is not null)
		{
			query = query.Where(s => s.Gender == groupingOptions.Gender);
		}

		if (groupingOptions.BirthDatePeriod.From is not null || groupingOptions.BirthDatePeriod.To is not null)
		{
			query = query.Where(s =>
				s.BirthDate != null &&
				(groupingOptions.BirthDatePeriod.From == null || s.BirthDate.Value >= groupingOptions.BirthDatePeriod.From.Value) &&
				(groupingOptions.BirthDatePeriod.To == null || s.BirthDate.Value <= groupingOptions.BirthDatePeriod.To.Value)
			);
		}

		query = groupingOptions.OnPaidStudyVariant switch
		{
			OnPaidStudyVariant.OnlyOnPaidStudy => query.Where(s => s.IsOnPaidStudy),
			OnPaidStudyVariant.OnlyOnFreeStudy => query.Where(s => !s.IsOnPaidStudy),
			_ => query
		};

		groupingOptions.GroupGroupingOptions.Match(
			onGroups: groups =>
			{
				ID[] groupIds = [.. groups.Select(s => s.Id)];

				if (groupIds.Length > 0)
				{
					query = query.Where(s => groupIds.Contains(s.GroupId));
				}
			},

			onStructuralUnits: units =>
			{
				if (units.Length > 0)
				{
					query = query.Where(s => units.Contains(s.Group.StructuralUnit));
				}
			},

			onEducationLevel: educationLevelGroupingOptions =>
			{
				educationLevelGroupingOptions.Match(
					onEducationLevels: educationLevels =>
					{
						ID[] educationLevelIds = [.. educationLevels.Select(s => s.Id)];

						if (educationLevelIds.Length > 0)
						{
							query = query.Where(s => educationLevelIds.Contains(s.Group.EducationLevel.Id));
						}
					},

					onEducationLevelTypes: educationLevelTypes =>
					{
						if (educationLevelTypes.Length > 0)
						{
							query = query.Where(s => educationLevelTypes.Contains(s.Group.EducationLevel.Type));
						}
					}
				);
			},

			onEnrollmentYearPeriod: period =>
			{
				query = query.Where(s =>
					(period.From == null || s.Group.EnrollmentYear >= period.From.Value.Year) &&
					(period.To == null || s.Group.EnrollmentYear <= period.To.Value.Year)
				);
			},

			onCurators: curators =>
			{
				ID[] curatorIds = [.. curators.Select(s => s.Id)];

				if (curatorIds.Length > 0)
				{
					query = query.Where(s => s.Group.Curator != null && curatorIds.Contains(s.Group.Curator.Id));
				}
			},

			onClusters: clusters =>
			{
				ID[] clusterIds = [.. clusters.Select(s => s.Id)];

				if (clusterIds.Length > 0)
				{
					query = query.Where(s => s.Group.Cluster != null && clusterIds.Contains(s.Group.Cluster.Id));
				}
			},

			onNotGrouping: () =>
			{
				//ignore
			}
		);


		ID[] workPlaceEnterpriseIds = [.. groupingOptions.WorkPlaceEnterprises.Select(e => e.Id)];

		if (workPlaceEnterpriseIds.Length > 0)
		{
			query = groupingOptions.WorkPlaceGroupingType switch
			{
				WorkPlaceGroupingType.All => query
					.Where(s => s.WorkPlaces.Any(wp => workPlaceEnterpriseIds.Contains(wp.Enterprise.Id))),

				WorkPlaceGroupingType.OnlyCurrent => query
					.Where(s => s.WorkPlaces.Any(wp => wp.IsCurrent && workPlaceEnterpriseIds.Contains(wp.Enterprise.Id))),

				WorkPlaceGroupingType.OnlyPrev => query
					.Where(s => s.WorkPlaces.Any(wp => !wp.IsCurrent && workPlaceEnterpriseIds.Contains(wp.Enterprise.Id))),

				_ => throw new Exception("Несуществующий тип группировки по местам работы")
			};
		}

		ID[] additionalQualificationIds = [.. groupingOptions.AdditionalQualifications.Select(e => e.Id)];

		if (additionalQualificationIds.Length > 0)
		{
			if (groupingOptions.UseStrictMatchForAdditionalQualifications)
			{
				query = query.Where(s =>
					additionalQualificationIds.All(id =>
						s.AdditionalQualifications.Any(aq => aq.Id == id)
					)
				);
			}
			else
			{
				query = query.Where(s =>
					s.AdditionalQualifications.Any(aq =>
						additionalQualificationIds.Contains(aq.Id)
					)
				);
			}
		}

		ID[] targetAgreementEnterpiseIds = [.. groupingOptions.TargetAgreementEnterprises.Select(e => e.Id)];

		if (targetAgreementEnterpiseIds.Length > 0)
		{
			query = query.Where(s => s.TargetAgreementEnterprise != null && targetAgreementEnterpiseIds.Contains(s.TargetAgreementEnterprise.Id));
		}

		if (groupingOptions.ArmyGroupingOptions != null)
		{
			if (groupingOptions.ArmyGroupingOptions.MustServe)
			{
				DateTimePeriod callDatePeriod = groupingOptions.ArmyGroupingOptions.ArmyCallDatePeriod!;

				query = query.Where(s =>
					s.MustServeInArmy &&
					s.ArmyCallDate != null &&
					(callDatePeriod.From == null || s.ArmyCallDate.Value >= callDatePeriod.From.Value) &&
					(callDatePeriod.To == null || s.ArmyCallDate.Value <= callDatePeriod.To.Value)
				);
			}
			else
			{
				query = query.Where(s => !s.MustServeInArmy);
			}
		}

		Int32[] socialStatuses = [.. groupingOptions.SocialStatuses.Select(ss => (Int32)ss)];

		if (socialStatuses.Length > 0)
		{
			if (groupingOptions.UseStrictMatchForSocialStatuses)
			{
				query = query.Where(s =>
					socialStatuses.All(ss =>
						s.SocialStatuses.Any(studentSs => ss == studentSs)
					)
				);
			}
			else
			{
				query = query.Where(s =>
					s.SocialStatuses.Any(studentSs =>
						socialStatuses.Contains(studentSs)
					)
				);
			}
		}

		if (groupingOptions.Statuses.Length > 0)
		{
			query = query.Where(s => groupingOptions.Statuses.Contains(s.Status));
		}

		query = groupingOptions.ForeignCitizenVariant switch
		{
			ForeignCitizenVariant.OnlyForeignCitizen => query.Where(s => s.IsForeignCitizen),
			ForeignCitizenVariant.OnlyNotForeignCitizen => query.Where(s => !s.IsForeignCitizen),
			_ => query
		};

		List<StudentEntity> entities = await query
			.OrderByDescending(s => s.CreatedDateTimeUtc)
			.ThenByDescending(s => s.ModifiedDateTimeUtc)
			.ToListAsync();

		Student[] students = [.. entities.Select(s => s.ToDomain())];

		List<(String Title, Func<Student, String> Value)> columns =
		[
			("Фамилия", s => s.FullName.Second),
			("Имя", s => s.FullName.First),
			("Отчество", s => s.FullName.Last ?? "—")
		];

		if (selectionOptions.Gender)
			columns.Add(("Пол", s => s.Gender.DisplayName()));
		if (selectionOptions.BirthDate)
			columns.Add(("Дата рождения", s => s.BirthDate?.ToString("dd.MM.yyyy") ?? "—"));
		if (selectionOptions.PhoneNumber)
			columns.Add(("Номер телефона", s => s.PhoneNumber ?? "-"));
		if (selectionOptions.RepresentativePhoneNumber)
			columns.Add(("Номер телефона представителя", s => s.Representative.PhoneNumber ?? "-"));
		if (selectionOptions.RepresentativeAlias)
			columns.Add(("Как обратиться к представителю", s => s.Representative.Alias ?? "-"));
		if (selectionOptions.IsOnPaidStudy)
			columns.Add(("Обучение на платной основе?", s => s.IsOnPaidStudy ? "Да" : "Нет"));
		if (selectionOptions.Snils)
			columns.Add(("СНИЛС", s => s.Snils ?? "-"));
		if (selectionOptions.GroupNumber)
			columns.Add(("Номер группы", s => s.Group.Number));
		if (selectionOptions.StructuralUnit)
			columns.Add(("Структурное подразделение", s => s.Group.StructuralUnit.DisplayName()));
		if (selectionOptions.EducationLevelType)
			columns.Add(("Тип уровня образования", s => s.Group.EducationLevel.Type.DisplayName()));
		if (selectionOptions.EducationLevelName)
			columns.Add(("Наименование уровня образования", s => s.Group.EducationLevel.Name));
		if (selectionOptions.EducationLevelCode)
			columns.Add(("Код уровня образования", s => s.Group.EducationLevel.Code));
		if (selectionOptions.EnrollmentYear)
			columns.Add(("Год поступления", s => s.Group.EnrollmentYear.ToString()));
		if (selectionOptions.Curator)
			columns.Add(("Куратор", s => s.Group.Curator?.FullName.DisplayName() ?? "-"));
		if (selectionOptions.ClusterName)
			columns.Add(("Наименование кластера", s => s.Group.Cluster?.Name ?? "-"));
		if (selectionOptions.PassportNumber)
			columns.Add(("Номер паспорта", s => s.Passport.Number ?? "-"));
		if (selectionOptions.PassportSeries)
			columns.Add(("Серия паспорта", s => s.Passport.Series ?? "-"));
		if (selectionOptions.PassportIssuedBy)
			columns.Add(("Кем выдан паспорт", s => s.Passport.IssuedBy ?? "-"));
		if (selectionOptions.PassportIssueDate)
			columns.Add(("Дата выдачи паспорта паспорта", s => s.Passport.IssuedDate?.ToString("dd.MM.yyyy") ?? "-"));
		if (selectionOptions.TargetAgreementNumber)
			columns.Add(("Номер договора о целевом обучении", s => s.TargetAgreement.Number ?? "-"));
		if (selectionOptions.TargetAgreementEnterpriseName)
			columns.Add(("Предприятие, с которым заключён договор о целевом обучении", s => s.TargetAgreement.Enterprise?.Name ?? "-"));
		if (selectionOptions.TargetAgreementDate)
			columns.Add(("Дата заключения договора о целевом обучении", s => s.TargetAgreement.Date?.ToString("dd.MM.yyyy") ?? "-"));
		if (selectionOptions.SocialStatuses)
			columns.Add(("Социальные статусы", s => s.SocialStatuses.DisplayName()));
		if (selectionOptions.Status)
			columns.Add(("Статус", s => s.Status.DisplayName()));
		if (selectionOptions.Address)
			columns.Add(("Адрес", s => s.Address ?? "-"));
		if (selectionOptions.IsForeignCitizen)
			columns.Add(("Иностранный гражданин?", s => s.IsForeignCitizen ? "Да" : "Нет"));
		if (selectionOptions.Inn)
			columns.Add(("ИНН", s => s.Inn ?? "-"));
		if (selectionOptions.Email)
			columns.Add(("Почта", s => s.Mail ?? "-"));

		using var package = new ExcelPackage();
		var worksheet = package.Workbook.Worksheets.Add("Отчёт");

		for (int i = 0; i < columns.Count; i++)
		{
			worksheet.Cells[1, i + 1].Value = columns[i].Title;
			worksheet.Cells[1, i + 1].Style.Font.Bold = true;
			worksheet.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
			worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
		}

		for (int row = 0; row < students.Length; row++)
		{
			for (int col = 0; col < columns.Count; col++)
			{
				worksheet.Cells[row + 2, col + 1].Value = columns[col].Value(students[row]);
			}
		}

		worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

		return package.GetAsByteArray();
	}
}
