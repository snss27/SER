using CSharpFunctionalExtensions;
using SER.Domain.EducationLevels.Enums;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Domain.EducationLevels;
public class EducationLevel
{
	public ID Id { get; }
	public EducationLevelType Type { get; }
	public String Name { get; } = default!;
	public String Code { get; } = default!;
	public String? StudyTime { get; }

	private EducationLevel(ID id, EducationLevelType type, String name, String code, String? studyTime)
	{
		Id = id;
		Type = type;
		Name = name;
		Code = code;
		StudyTime = studyTime;
	}

	public static Result<EducationLevel, Error> Create(ID? id, EducationLevelType? type, String? name, String? code, String? studyTime)
	{
		if (type is null) return new Error("Укажите тип уровня образования");

		if (String.IsNullOrWhiteSpace(name)) return new Error("Укажите наименование уровня образования");

		if (String.IsNullOrWhiteSpace(code)) return new Error("Укажите код уровня образования");

		ID _id = id ?? ID.New();

		return new EducationLevel(_id, type.Value, name, code, studyTime);
	}
}
