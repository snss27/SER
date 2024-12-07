using SER.Domain.EducationLevels;
using SER.Domain.Employees;

namespace SER.Domain.Groups.Converters;

public static class GroupDtoConverter
{
	public static GroupDto ToGroupDto(this Group group, EducationLevel? speciality, Employee? curator)
	{
		return new GroupDto(group.Id, group.Number, group.StructuralUnit, speciality, group.EnrollmentYear,
			curator);
	}

	public static GroupDto[] ToGroupDtos(this Group[] groups, EducationLevel[] specialities, Employee[] curators)
	{
		List<GroupDto> result = [];
		result.AddRange(
			from @group in groups
			let speciality = specialities.FirstOrDefault(s => s.Id == @group.SpecialityId)
			let curator = curators.FirstOrDefault(curator => curator.Id == @group.CuratorId)
			select new GroupDto(
				@group.Id,
				@group.Number,
				@group.StructuralUnit,
				speciality,
				@group.EnrollmentYear,
				curator
			)
		);

		return result.ToArray();
	}
}