using System.Collections.Generic;
using SER.Domain.Clusters;
using SER.Domain.EducationLevels;
using SER.Domain.Enterprises;
using SER.Domain.Groups;
using SER.Domain.Workplaces;

namespace SER.Domain.WorkPlaces.Converters;
public static class WorkPlaceDtoConverter
{
	public static WorkPlaceDto ToWorkpalceDto(this WorkPlace workPlace, Enterprise enterprise)
	{
		return new WorkPlaceDto(workPlace.Id, enterprise, workPlace.Post, workPlace.WorkBookExtractFile, workPlace.StartDateTime, workPlace.FinishDateTime);
	}

	public static WorkPlaceDto[] ToWorkpalceDtos(this WorkPlace[] workPlaces, Enterprise[] enterprises)
	{
		List<WorkPlaceDto> result = [];
		result.AddRange(
		from @workPlace in workPlaces
		let enterprise = enterprises.FirstOrDefault(enterprise => enterprise.Id == @workPlace.EnterpriseId)
		select new WorkPlaceDto(
			@workPlace.Id,
			enterprise,
			@workPlace.Post,
			@workPlace.WorkBookExtractFile,
			@workPlace.StartDateTime,
			@workPlace.FinishDateTime
		)
		);

		return result.ToArray();
	}
}

