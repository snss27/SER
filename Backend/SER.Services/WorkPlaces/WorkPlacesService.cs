using SER.Domain.Services;
using SER.Domain.Workplaces;
using SER.Services.WorkPlaces.Repositories;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Services.WorkPlaces;
public class WorkPlacesService(IWorkPlacesRepository workPlacesRepository, IFilesService filesService) : IWorkPlacesSevice
{
	public async Task<DataResult<ID>> Save(WorkPlaceBlank blank, String groupAlias, String studentAlias)
	{
		Result validateResult = Validate(blank);

		if (!validateResult.IsSuccess) return DataResult<ID>.Failed(validateResult.Errors[0].Message);

		String[] fileUrls = filesService.SaveWorkBookFile(blank.WorkBookExtractFile, groupAlias, studentAlias);

		ID id = blank.Id ?? ID.New();	

		await workPlacesRepository.Save(blank, fileUrls[0]);

		return DataResult<ID>.Success(id);
	}

	public async Task<DataResult<ID[]>> Save(WorkPlaceBlank[] blanks, String groupAlias, String studentAlias)
	{
		foreach(WorkPlaceBlank blank in blanks)
		{
			Result validateResult = Validate(blank);

			if (!validateResult.IsSuccess) return DataResult<ID[]>.Failed(validateResult.Errors[0].Message);
		}

		List<ID> workPlaceIds = new();

		foreach(WorkPlaceBlank blank in blanks)
		{
			String[] fileUrls = filesService.SaveWorkBookFile(blank.WorkBookExtractFile, groupAlias, studentAlias);

			ID id = blank.Id ?? ID.New();

			await workPlacesRepository.Save(blank, fileUrls[0]);
			workPlaceIds.Add(id);
		}

		return DataResult<ID[]>.Success(workPlaceIds.ToArray());
	}

	private Result Validate(WorkPlaceBlank blank)
	{
		if(blank.Enterprise is null)
		{
			return Result.Fail($"Укажите предприятие для места работы");
		}

		return Result.Success();
	}
}

