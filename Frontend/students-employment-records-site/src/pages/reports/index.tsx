import { Grouping } from "@/components/reports/grouping/grouping"
import { Selection } from "@/components/reports/selection/selection"
import StepperComponent from "@/components/shared/layouts/stepper"
import { ReportGroupingOptions } from "@/domain/reports/models/grouping/reportGroupingOptions"
import { ReportSelectionOptions } from "@/domain/reports/models/selection/reportSelectionOptions"
import { ReportsProvider } from "@/domain/reports/reportsProvider"
import { Box } from "@mui/material"
import { useReducer, useState } from "react"

//TODO Это всё не группировка данных, а фильтрация. Надо всё переименовать
const ReportsPage = () => {
    const [groupingOptions, dispatch] = useReducer(
        ReportGroupingOptions.reducer,
        ReportGroupingOptions.empty()
    )

    const [selectionOptions, setSelectionOptions] = useState(ReportSelectionOptions.getDefault())

    async function handleComplete() {
        const result = await ReportsProvider.Generate(groupingOptions, selectionOptions)
    }

    function handleReset() {
        setSelectionOptions(ReportSelectionOptions.getDefault())
        dispatch({ type: "RESET" })
    }

    return (
        <Box className="container">
            <StepperComponent
                steps={["Фильтрация", "Выбор данных"]}
                stepContent={[
                    <Grouping groupingOptions={groupingOptions} dispatch={dispatch} />,
                    <Selection
                        selectionOptions={selectionOptions}
                        setSelectionOptions={setSelectionOptions}
                    />,
                ]}
                onComplete={handleComplete}
                onReset={handleReset}
            />
        </Box>
    )
}

export default ReportsPage
