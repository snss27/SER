import HttpClient from "../httpClient"
import { ReportGroupingOptions } from "./models/grouping/reportGroupingOptions"
import { ReportSelectionOptions } from "./models/selection/reportSelectionOptions"

export class ReportsProvider {
    public static async Generate(
        groupingOptions: ReportGroupingOptions,
        selectionOptions: ReportSelectionOptions
    ) {
        const blob = await HttpClient.postBlobAsync("/reports/generate", {
            groupingOptions,
            selectionOptions,
        })

        const url = window.URL.createObjectURL(blob)
        const a = document.createElement("a")
        a.href = url
        a.download = "Report.xlsx"
        document.body.appendChild(a)
        a.click()
        window.URL.revokeObjectURL(url)
        document.body.removeChild(a)
    }
}
