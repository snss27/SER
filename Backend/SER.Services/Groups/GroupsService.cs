using SER.Domain.Groups;
using SER.Domain.Services;
using SER.Services.Groups.Repositories;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Services.Groups;
public class GroupsService : IGroupsService
{
	private readonly IGroupsRepository _groupsRepository;

	public GroupsService(IGroupsRepository groupsRepository)
	{
		_groupsRepository = groupsRepository;
	}

	public async Task<Result> Save(GroupBlank blank)
	{
		if (String.IsNullOrWhiteSpace(blank.Number)) return Result.Fail("Введите номер группы");

		blank.Number = blank.Number.Trim();

		if (!Int32.TryParse(blank.Number, out Int32 id)) return Result.Fail("Номер группы должен быть целым числом");

		if (blank.Number.Length != 5) return Result.Fail("Номер группы должен состоять из 5 цифр");

		if (blank.StructuralUnit is null) return Result.Fail("Выберите струкрутное подразделение");

		if (blank.SpecialityId is null) return Result.Fail("Выберите специальность");

		if (blank.EnrollmentYear is null) return Result.Fail("Выберите год поступления");

		if (blank.CuratorId is null) return Result.Fail("Выберите куратора");

		blank.Id ??= ID.New();

		return await _groupsRepository.Save(blank);
	}

	public async Task<Result> Remove(ID id)
	{
		return await _groupsRepository.Remove(id);
	}

	public async Task<Group> Get(ID id)
	{
		return await _groupsRepository.Get(id);
	}

	public async Task<Group[]> GetAll()
	{
		return await _groupsRepository.GetAll();
	}
}
