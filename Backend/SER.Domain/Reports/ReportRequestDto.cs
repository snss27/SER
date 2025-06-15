using SER.Domain.Reports.Grouping;
using SER.Domain.Reports.Selection;

namespace SER.Domain.Reports;
public record ReportRequestDto(
	ReportGroupingOptionsDto GroupingOptions,
	ReportSelectionOptionsDto SelectionOptions
);
