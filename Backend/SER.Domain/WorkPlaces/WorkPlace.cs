using CSharpFunctionalExtensions;
using SER.Domain.Enterprises;
using SER.Domain.Students;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Domain.Workplaces;
public class WorkPlace
{
	public ID Id { get; }
	public Enterprise Enterprise { get; } = default!;
	public String? Post {  get; }
	public String[] WorkBookExtractFiles { get; }
	public DateTime? StartDateTime { get; }
	public DateTime? FinishDateTime { get; } 
	public Boolean IsCurrent { get; }

	private WorkPlace(ID id, Enterprise enterprise, String? post, String[] workBookExtractFiles, DateTime? startDateTime, DateTime? finishDateTime, Boolean isCurrent)
	{
		Id = id;
		Enterprise = enterprise;
		Post = post;
		WorkBookExtractFiles = workBookExtractFiles;
		StartDateTime = startDateTime;
		FinishDateTime = finishDateTime;
		IsCurrent = isCurrent;
	}

	public static Result<WorkPlace, Error> Create(ID? id, Enterprise? enterprise, String? post, String[] workBookExtractFiles, DateTime? startDateTime, DateTime? finishDateTime, Boolean? isCurrent)
	{
		if (enterprise is null) return new Error("Укажите предприятие у места работы");

		if (isCurrent is null) return new Error("Укажите, текущее ли это место работы");

		ID _id = id ?? ID.New();

		return new WorkPlace(_id, enterprise, post, workBookExtractFiles, startDateTime, finishDateTime, isCurrent.Value);
	}
}
