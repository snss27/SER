using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SER.Domain.Reports;
using SER.Domain.Services;

namespace SER.API.Controllers.Reports;

[Authorize]
[Route("api/reports")]
public class ReportsController(IReportsService reportsService) : ControllerBase
{
	[HttpPost("generate")]
	public async Task<IActionResult> GenerateReport([FromBody] ReportRequestDto reportRequest)
	{
		Byte[] reportBytes = await reportsService.Generate(reportRequest.GroupingOptions, reportRequest.SelectionOptions);

		return File(
			reportBytes,
			"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
			"Report.xlsx"
		);
	}
}
