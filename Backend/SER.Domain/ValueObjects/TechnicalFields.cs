using CSharpFunctionalExtensions;

namespace SER.Database.Models.Types;
public class TechnicalFields : ValueObject
{
	public DateTime CreatedDateTimeUtc { get; private set; }
	public DateTime? ModifiedDateTimeUtc { get; private set; }
	public Boolean IsRemoved { get; private set; }

	private TechnicalFields(DateTime createdDateTimeUtc, DateTime? modifiedDateTimeUtc, Boolean isRemoved)
	{
		CreatedDateTimeUtc = createdDateTimeUtc;
		ModifiedDateTimeUtc = modifiedDateTimeUtc;
		IsRemoved = isRemoved;
	}

	public static TechnicalFields Create()
	{
		return new TechnicalFields(createdDateTimeUtc: DateTime.UtcNow, modifiedDateTimeUtc: null, false);
	}

	public TechnicalFields MarkAsRemoved()
	{
		return new TechnicalFields(CreatedDateTimeUtc, DateTime.UtcNow, isRemoved: true);
	}

	public TechnicalFields UpdateModifiedDateTime()
	{
		return new TechnicalFields(CreatedDateTimeUtc, modifiedDateTimeUtc: DateTime.UtcNow, IsRemoved);
	}

	protected override IEnumerable<Object?> GetEqualityComponents()
	{
		yield return CreatedDateTimeUtc;
		yield return ModifiedDateTimeUtc;
		yield return IsRemoved;
	}
}
