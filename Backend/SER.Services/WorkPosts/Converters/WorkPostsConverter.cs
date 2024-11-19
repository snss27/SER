using SER.Domain.WorkPosts;
using SER.Services.WorkPosts.Models;

namespace SER.Services.WorkPosts.Converters;
internal static class WorkPostsConverter
{
	public static WorkPost ToWorkPost(this WorkPostDB db)
	{
		return new WorkPost(db.Id, db.Name, db.CreatedDateTimeUtc, db.ModifiedDateTimeUtc);
	}

	public static WorkPost[] ToWorkPosts(this WorkPostDB[] dbs)
	{
		return dbs.Select(ToWorkPost).ToArray();
	}
}
