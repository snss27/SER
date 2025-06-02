using CSharpFunctionalExtensions;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Domain.AdditionalQualifications;

public class AdditionalQualification
{
	public ID Id { get; }
	public String Name { get; } = default!;
	public String Code { get; } = default!;
	public String? StudyTime { get; }

	private AdditionalQualification(ID id, String name, String code, String? studyTime)
	{	
		Id = id;
		Name = name;
		Code = code;
		StudyTime = studyTime;
	}

	public static Result<AdditionalQualification, Error> Create(ID? id, String? name, String? code, String? studyTime)
	{
		if (String.IsNullOrWhiteSpace(name)) return new Error("Укажите наименование дополнительной квалификации");

		if (String.IsNullOrWhiteSpace(code)) return new Error("Укажите код дполнительной квалификации");

		ID _id = id ?? ID.New();

		return new AdditionalQualification(_id, name, code, studyTime);
	}
}
