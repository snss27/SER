using System.Net.Http.Headers;
using SER.Domain.Enterprises;
using SER.Domain.Services;
using SER.Domain.Workplaces;
using SER.Domain.WorkPlaces;
using SER.Domain.WorkPlaces.Converters;
using SER.Services.WorkPlaces.Repositories;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Services.WorkPlaces;
public class WorkPlacesService(IWorkPlacesRepository workPlacesRepository, IEnterprisesService enterprisesService, IFilesService filesService) : IWorkPlacesSevice
{
	public async Task<DataResult<ID>> Save(WorkPlaceBlank blank, String groupAlias, String studentAlias)
	{
		Result validateResult = Validate(blank);

		if (!validateResult.IsSuccess) return DataResult<ID>.Failed(validateResult.Errors[0].Message);

		String[] fileUrls = filesService.SaveWorkBookFile(blank.WorkBookExtractFile, groupAlias, studentAlias);

		blank.Id ??= ID.New();	

		await workPlacesRepository.Save(blank, fileUrls.FirstOrDefault());

		return DataResult<ID>.Success(blank.Id.Value);
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

			blank.Id ??= ID.New();

			await workPlacesRepository.Save(blank, fileUrls.FirstOrDefault());
			workPlaceIds.Add(blank.Id.Value);
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

	public async Task<WorkPlaceDto?> Get(ID id)
	{
		WorkPlace? workPlace = await workPlacesRepository.Get(id);

		if (workPlace is null) return null;

		Enterprise enterprise = await enterprisesService.Get(workPlace.EnterpriseId) ?? throw new NullReferenceException("Предприятия места работы не найдено");

		return workPlace.ToWorkpalceDto(enterprise);
	}

	public async Task<WorkPlaceDto[]> Get(ID[] ids)
	{
		WorkPlace[] workPlaces = await workPlacesRepository.Get(ids);

		ID[] enterpriseIds = workPlaces.Select(x => x.EnterpriseId).ToArray();
		Enterprise[] enterprises = await enterprisesService.Get(enterpriseIds);

		return workPlaces.ToWorkpalceDtos(enterprises);
	}

	public async Task<WorkPlace[]> GetByEnterpriseId(ID enterpriseId)
	{
		return await workPlacesRepository.GetByEnterpriseId(enterpriseId);
	}
}

