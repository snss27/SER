using SER.Domain.Enterprises;
using SER.Domain.Enterprises.Converters;
using SER.Domain.Students;
using SER.Domain.Workplaces;

namespace SER.Domain.WorkPlaces.Converters;
public static class WorkPlacesConverters
{
	public static WorkPlaceDto ToDto(this WorkPlace workPlace)
	{
		EnterpriseDto enterprise = workPlace.Enterprise.ToDto();

		return new WorkPlaceDto(workPlace.Id, enterprise, workPlace.Post, workPlace.WorkBookExtractFiles, workPlace.StartDateTime, workPlace.FinishDateTime);
	}

	public static WorkPlace ToDomain(this WorkPlaceDto dto, Boolean isCurrent)
	{
		Enterprise enterprise = dto.Enterprise.ToDomain();
		return WorkPlace.Create(dto.Id, enterprise, dto.Post, dto.WorkBookExtractFiles, dto.StartDate, dto.FinishDate, isCurrent).Value;
	}
}

