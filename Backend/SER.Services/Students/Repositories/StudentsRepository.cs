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
	public async Task<Result> Save(StudentBlank blank)
	{
		Query query = _connector.CreateQuery(Sql.Students_Save);
		{
			query.Add(blank.Id, "p_id");
			query.Add(blank.Name, "p_name");
			query.Add(blank.Surname, "p_secondname");
			query.Add(blank.Patronymic, "p_lastname");
			query.Add(blank.Gender, "p_gender");
			query.Add(blank.BirthDate, "p_birthDate");
			query.Add(blank.PhoneNumber, "p_phonenumber");
			query.Add(blank.RepresentativePhoneNumber, "p_representativephonenumber");
			query.Add(blank.IsOnPaidStudy, "p_isonpaidstudy");
			query.Add(blank.Snils, "p_snils");
			query.Add(blank.GroupId, "p_groupid");
			query.Add(blank.AdditionalQualifications, "p_additionalqualifications");
			query.Add(blank.IsTargetAgreement, "p_istargetagreement");
			query.Add(blank.TargetAgreementFile, "p_targetagreementfile");
			query.Add(blank.ArmySubpoenaFile, "p_armysubpoenafile");
			query.Add(blank.ArmyServeDate, "p_armyservedate");
			query.Add(blank.Address, "p_address");
			query.Add(blank.IsForeignCitizen, "p_isforeigncitizen");
			query.Add(blank.Inn, "p_inn");
			query.Add(blank.Mail, "p_mail");
			query.Add(blank.PassportNumber, "p_pasportnumber");
			query.Add(blank.PassportSeries, "p_pasportseries");
			query.Add(DateTime.UtcNow, "p_currentdatetimeutc");
		}

		try
		{
			await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();
			await session.Execute(query);
			return Result.Success();
		}
		catch (Exception ex)
		{
			return Result.Fail($"Ошибка при выполнении запроса: {ex.Message}");
		}

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

		return (await session.Get<StudentDB?>(query))?.ToFlatStudent();
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

		return (await session.GetArray<StudentDB>(query)).ToFlatStudents();
	}
}