using CSharpFunctionalExtensions;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Domain.Clusters;

public class Cluster
{
	public ID Id { get; }
	public String Name { get; } = default!;

	private Cluster(ID id, String name)
	{
		Id = id;
		Name = name;
	}

	public static Result<Cluster, Error> Create(ID? id, String? name)
	{
		if (String.IsNullOrWhiteSpace(name)) return new Error("Укажите наименование кластера");

		ID _id = id ?? ID.New();

		return new Cluster(_id, name);
	}
}
