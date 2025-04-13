using Microsoft.AspNetCore.Http;

namespace SER.Tools.Types.Types;
public class BlankFiles
{
	public String[] FileUrls { get; set; } = [];
	public IFormFile[] Files { get; set; } = [];
	public Int32 MaxFiles { get; set; }
}
