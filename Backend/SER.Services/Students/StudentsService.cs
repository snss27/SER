using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SER.Database;
using SER.Database.Models.AdditionalQualifications;
using SER.Database.Models.Students;
using SER.Database.Models.WorkPlaces;
using SER.Domain.AdditionalQualifications;
using SER.Domain.AdditionalQualifications.Converters;
using SER.Domain.Enterprises;
using SER.Domain.Enterprises.Converters;
using SER.Domain.Groups;
using SER.Domain.Groups.Converters;
using SER.Domain.Services;
using SER.Domain.Students;
using SER.Domain.Workplaces;
using SER.Services.AdditionalQualifications.Converters;
using SER.Services.Students.Extensions;
using SER.Services.WorkPlaces.Extensions;
using SER.Tools.Types;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using static SER.Tools.Utils.NumberUtils;

namespace SER.Services.Students;

public class StudentsService(SERDbContext dbContext) : IStudentsService
{

	public async Task<OperationResult> Save(StudentBlank blank)
	{
		Group? group = blank.Group?.ToDomain();
		if (group is null) return OperationResult.Fail("Укажите группу студента");

		Enterprise? targetAgreementEnterprise = blank.TargetAgreementEnterprise?.ToDomain();

		ID[] additionalQualificationIds = [.. blank.AdditionalQualifications.Select(aq => aq.Id)];
		List<AdditionalQualificationEntity> additionalQualificationEntities = await dbContext.AdditionalQualifications
			.Where(aq => additionalQualificationIds.Contains(aq.Id))
			.ToListAsync();

		AdditionalQualification[] additionalQualifications = [.. additionalQualificationEntities.Select(aq => aq.ToDomain())];

		List<WorkPlace> workPlaces = [];
		foreach(WorkPlaceBlank workPlaceBlank in blank.WorkPlaces)
		{
			Enterprise? enterprise = workPlaceBlank.Enterprise?.ToDomain();
			Result<WorkPlace, Error> createWorkPlaceResult = WorkPlace.Create(workPlaceBlank.Id, enterprise, workPlaceBlank.Post, workPlaceBlank.WorkBookExtractFiles, workPlaceBlank.StartDate, workPlaceBlank.FinishDate, workPlaceBlank.IsCurrent);
			if (createWorkPlaceResult.IsFailure) return OperationResult.Fail(createWorkPlaceResult.Error);
			workPlaces.Add(createWorkPlaceResult.Value);
		}
 
		Result<Student, Error> result = Student.Create(blank.Id, blank.Name, blank.SecondName, blank.LastName, blank.Gender, blank.BirthDate, blank.PhoneNumber, blank.RepresentativePhoneNumber, blank.RepresentativeAlias, blank.IsOnPaidStudy, blank.Snils, group, blank.PassportNumber, blank.PassportSeries, blank.PassportIssuedBy, blank.PassportIssuedDate, blank.PassportFiles, [.. workPlaces], additionalQualifications, blank.IsTargetAgreement, blank.TargetAgreementNumber, targetAgreementEnterprise, blank.TargetAgreementDate, blank.TargetAgreementFiles, blank.MustServeInArmy, blank.ArmyCallDate, blank.ArmySubpoenaFiles, blank.SocialStatuses, blank.Status, blank.Address, blank.IsForeignCitizen, blank.Inn, blank.Mail, blank.OtherFiles);
		if (result.IsFailure) return OperationResult.Fail(result.Error);
		Student student = result.Value;

		Boolean isNew = blank.Id is null;

		if (isNew)
		{
			StudentEntity entity = student.ToEntity(additionalQualificationEntities);
			entity.WorkPlaces = [.. workPlaces.Select(wp => wp.ToEntity(entity.Id))];
			await dbContext.AddAsync(entity);
		}
		else
		{
			StudentEntity? entity = await dbContext.Students
				.Include(s => s.AdditionalQualifications)
				.Include(s => s.WorkPlaces)
				.FirstOrDefaultAsync(s => s.Id == student.Id);
			if (entity is null) return OperationResult.Fail("Студент не найден");

			HashSet<ID> existingWorkPlaceIds = [.. entity.WorkPlaces.Select(wp => wp.Id)];
			HashSet<ID> newWorkPlaceIds = [.. workPlaces.Select(wp => wp.Id)];

			foreach (WorkPlaceEntity wp in entity.WorkPlaces.Where(wp => !newWorkPlaceIds.Contains(wp.Id)).ToList())
			{
				dbContext.Remove(wp);
			}

			foreach (WorkPlace wp in workPlaces)
			{
				WorkPlaceEntity? existingWorkPlace = entity.WorkPlaces.FirstOrDefault(w => w.Id == wp.Id);
				if (existingWorkPlace != null)
				{
					existingWorkPlace.ApplyChanges(wp);
				}
				else
				{
					entity.WorkPlaces.Add(wp.ToEntity(entity.Id));
				}
			}

			entity.ApplyChanges(student, additionalQualificationEntities);
		}

		await dbContext.SaveChangesAsync();
		return OperationResult.Success();
	}

	public async Task<OperationResult> Remove(ID id)
	{
		StudentEntity? entity = await dbContext.Students.FirstOrDefaultAsync(s => s.Id == id);
		if (entity is null) return OperationResult.Fail("Студент не найден");

		dbContext.Remove(entity);
		await dbContext.SaveChangesAsync();

		return OperationResult.Success();
	}

	public async Task<Student?> Get(ID id)
	{
		StudentEntity? entity = await dbContext.Students
			.Include(s => s.Group)
				.ThenInclude(g => g.EducationLevel)
			.Include(s => s.Group)
				.ThenInclude(g => g.Curator)
			.Include(s => s.Group)
				.ThenInclude(g => g.Cluster)
			.Include(s => s.WorkPlaces)
				.ThenInclude(wp => wp.Enterprise)
			.Include(s => s.AdditionalQualifications)
			.Include(s => s.TargetAgreementEnterprise)
			.FirstOrDefaultAsync(s => s.Id == id);

		return entity?.ToDomain();
	}

	public async Task<PagedResult<Student>> GetPage(Int32 page, Int32 pageSize)
	{
		(Int32 offset, Int32 limit) = NormalizeRange(page, pageSize);

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

		Int32 totalRows = await query.CountAsync();

		List<StudentEntity> entities = await query
			.OrderByDescending(e => e.CreatedDateTimeUtc)
			.ThenByDescending(e => e.ModifiedDateTimeUtc)
			.Skip(offset)
			.Take(limit)
			.ToListAsync();

		return PagedResult.Create(entities.Select(e => e.ToDomain()), totalRows);
	}
}
