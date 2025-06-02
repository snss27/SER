using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SER.Tools.Types.IDs;

namespace SER.Database.Models.ConfigurationTools;
public class IDConverter : ValueConverter<ID, byte[]>
{
	public IDConverter()
		: base(
			id => id.ToByteArray(),
			bytes => new ID(bytes))
	{
	}
}
