using CSharpFunctionalExtensions;
using SER.Domain.Clusters;
using SER.Domain.EducationLevels;
using SER.Domain.Employees;
using SER.Domain.Groups.Enums;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using SER.Tools.Utils;

namespace SER.Domain.Groups;

public class Group
{
	public ID Id { get; }
	public String Number { get; } = default!;
	public StructuralUnit StructuralUnit { get; }
	public EducationLevel EducationLevel { get; } = default!;
	public Int32 EnrollmentYear { get; }
	public Employee? Curator { get; }
	public Boolean HasCluster { get; }
	public Cluster? Cluster { get; }

	private Group(ID id, String number, StructuralUnit structuralUnit, EducationLevel educationLevel, Int32 enrollmentYear, Employee? curator, Boolean hasCluster, Cluster? cluster)
	{
		Id = id;
		Number = number;
		StructuralUnit = structuralUnit;
		EducationLevel = educationLevel;
		EnrollmentYear = enrollmentYear;
		Curator = curator;
		HasCluster = hasCluster;
		Cluster = cluster;
	}

	public static Result<Group, Error> Create(ID? id, String? number, StructuralUnit? structuralUnit, EducationLevel educationLevel, Int32? enrollmentYear, Employee? curator, Boolean? hasCluster, Cluster? cluster)
	{
		if (String.IsNullOrWhiteSpace(number))
		{
			return new Error("Укажите номер группы");
		}

		if (!Regexs.GroupNumberRegex.IsMatch(number))
		{
			return new Error("Номер группы должен быть целым пятизначным числом");
		}

		if (structuralUnit is null)
		{
			return new Error("Укажите струкрутное подразделение");
		}

		if (enrollmentYear is null)
		{
			return new Error("Укажите год поступления");
		}

		if(hasCluster is null)
		{
			return new Error("Укажите, относится ли группа к кластеру");
		}

		ID _id = id ?? ID.New();

		return new Group(_id, number, structuralUnit.Value, educationLevel, enrollmentYear.Value, curator, hasCluster.Value, cluster);
	}
}
