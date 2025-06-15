using SER.Domain.Reports.Grouping;
using SER.Domain.Reports.Selection;

namespace SER.Domain.Services;
public interface IReportsService
{
	public Task<Byte[]> Generate(ReportGroupingOptionsDto GroupingOptions, ReportSelectionOptionsDto SelectionOptions);
}
