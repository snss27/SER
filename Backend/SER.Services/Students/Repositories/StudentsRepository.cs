using SER.Configurator.Connectors;
using SER.Domain.Students;
using SER.Services._base;
using SER.Services.Students.Converters;
using SER.Services.Students.Repositories.Queries;
using SER.Tools.DataBase;
using SER.Tools.DataBase.Query;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using static SER.Tools.Utils.NumberUtils;

namespace SER.Services.Students.Repositories;

public class StudentsRepository(MainConnector connector) : BaseRepository(connector), IStudentsRepository
{
	public async Task<Result> Save(StudentBlank blank, ID? currentWorkplaceId, ID[] prevWorkplaceIds, String[] passportFileUrls, String? targetAgreementFileUrl, String? armySunpoenaFileUrl, String[] otherFileUrls)
	{
		Query query = _connector.CreateQuery(Sql.Students_Save);
		{
			query.Add(blank.Id);
			query.Add(blank.Name);
			query.Add(blank.SecondName);
			query.Add(blank.LastName);
			query.Add(blank.Gender);
			query.Add(blank.BirthDate);
			query.Add(blank.PhoneNumber);
			query.Add(blank.RepresentativePhoneNumber);
			query.Add(blank.IsOnPaidStudy);
			query.Add(blank.Snils);
			query.Add(blank.Group?.Id, "p_groupid");
			query.Add(blank.PassportNumber);
			query.Add(blank.PassportSeries);
			query.Add(blank.PassportIssuedBy);
			query.Add(blank.PassportIssuedDate);
			query.Add(passportFileUrls, "p_pasportfiles");
			query.Add(prevWorkplaceIds);
			query.Add(currentWorkplaceId);
			query.Add(blank.AdditionalQualifications.Select(q => q.Id).ToArray(), "p_additionalqualifications");
			query.Add(blank.IsTargetAgreement);
			query.Add(targetAgreementFileUrl, "p_targetagreementfile");
			query.Add(blank.TargetAgreementDate);
			query.Add(blank.TargetAgreementEnterprise?.Id, "p_targetagreemententerpriseid");
			query.Add(blank.MustServeInArmy);
			query.Add(armySunpoenaFileUrl, "p_armysubpoenafile");
			query.Add(blank.ArmyCallDate);
			query.Add(blank.SocialStatuses);
			query.Add(blank.Status);
			query.Add(blank.Address);
			query.Add(blank.IsForeignCitizen);
			query.Add(blank.Inn);
			query.Add(blank.Mail);
			query.Add(otherFileUrls, "p_otherfiles");
			query.Add(DateTime.UtcNow, "p_currentdatetimeutc");
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();
		await session.Execute(query);

		return Result.Success();
	}

	public async Task<Result> Remove(ID id)
	{
		Query query = _connector.CreateQuery(Sql.Students_Remove);
		{
			query.Add(id);
			query.Add(DateTime.UtcNow, "p_currentdatetimeutc");
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();
		await session.Execute(query);

		return Result.Success();
	}

	public async Task<Student?> Get(ID id)
	{
		Query query = _connector.CreateQuery(Sql.Students_Get);
		{
			query.Add(id);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.Get<StudentDB?>(query))?.ToStudent();
	}

	public async Task<Student[]> GetPage(Int32 page, Int32 pageSize)
	{
		Query query = _connector.CreateQuery(Sql.Students_GetPage);
		{
			(Int32 offset, Int32 limit) = NormalizeRange(page, pageSize);
			query.Add(offset);
			query.Add(limit);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<StudentDB>(query)).ToStudents();
	}

	public async Task<Student[]> GetByGroupId(ID groupId)
	{
		Query query = _connector.CreateQuery(Sql.Students_GetByGroupId);
		{
			query.Add(groupId);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<StudentDB>(query)).ToStudents();
	}
}