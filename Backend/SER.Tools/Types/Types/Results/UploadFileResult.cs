using Newtonsoft.Json;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Domain.Files;
public class UploadFileResult : Result
{
	public ID[] NotSavedFileIds { get; }
	public ID[] NotRemovedFileIds { get; }

	[JsonConstructor]
	public UploadFileResult(Error[] errors, ID[] notSavedFileIds, ID[] notRemovedFileIds) : base(errors)
	{
		NotSavedFileIds = notSavedFileIds;
		NotRemovedFileIds = notRemovedFileIds;
	}

	public new static UploadFileResult Success()
	{
		return new UploadFileResult(Array.Empty<Error>(), Array.Empty<ID>(), Array.Empty<ID>());
	}

	public static UploadFileResult Fail(Error error, ID[]? notSavedFileIds = null, ID[]? notRemovedFileIds = null)
	{
		notSavedFileIds ??= Array.Empty<ID>();
		notRemovedFileIds ??= Array.Empty<ID>();

		return new UploadFileResult(new Error[] { error }, notSavedFileIds, notRemovedFileIds);
	}

	public static UploadFileResult Fail(String errorMessage, ID[]? notSavedFileIds = null, ID[]? notRemovedFileIds = null)
	{
		notSavedFileIds ??= Array.Empty<ID>();
		notRemovedFileIds ??= Array.Empty<ID>();

		return new UploadFileResult(new Error[] { new Error(errorMessage) }, notSavedFileIds, notRemovedFileIds);
	}
}
