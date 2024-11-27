using SER.Domain.EducationLevels;
using SER.Domain.Employees;
using SER.Tools.Types.Results;

namespace SER.Domain.Groups.Converters;
public static class GroupDtoConverter
{
	public static GroupDto ToGroupDto(this Group group, EducationLevel? speciality, Employee? curator)
	{
		return new GroupDto(group.Id, group.Number, group.StructuralUnit, speciality, group.EnrollmentYear, curator);
	}

	public static GroupDto[] ToGroupDtos(this Group[] groups, EducationLevel[] specialities, Employee[] curators)
	{
		List<GroupDto> result = [];
		foreach (Group group in groups)
		{
			EducationLevel? speciality = specialities.FirstOrDefault(speciality => speciality.Id == group.SpecialityId);
			Employee? curator = curators.FirstOrDefault(curator => curator.Id == group.CuratorId);
			GroupDto groupDto = new(group.Id, group.Number, group.StructuralUnit, speciality, group.EnrollmentYear, curator);
			result.Add(groupDto);
		}

		return result.ToArray();
	}
}
