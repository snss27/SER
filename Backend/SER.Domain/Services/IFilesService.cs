using Microsoft.AspNetCore.Http;
using SER.Tools.Types.Types;

namespace SER.Domain.Services;
public interface IFilesService
{
	public String[] SaveWorkBookFile(BlankFiles blankFiles, String groupAlias, String studentAlias);
	public String[] SavePassportFiles(BlankFiles blankFiles, String groupAlias, String studentAlias);
	public String[] SaveTargetAfreementFiles(BlankFiles blankFiles, String groupAlias, String studentAlias);
	public String[] SaveArmySubpoenaFiles(BlankFiles blankFiles, String groupAlias, String studentAlias);
	public String[] SaveOtherFiles(BlankFiles blankFiles, String groupAlias, String studentAlias);
}
