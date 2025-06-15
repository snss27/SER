import Switch from "@/components/shared/buttons/switch"
import {
    reportSelectionLabels,
    ReportSelectionOptions,
} from "@/domain/reports/models/selection/reportSelectionOptions"
import { Box } from "@mui/material"
import { Dispatch, SetStateAction } from "react"

interface Props {
    selectionOptions: ReportSelectionOptions
    setSelectionOptions: Dispatch<SetStateAction<ReportSelectionOptions>>
}

export function Selection({ selectionOptions, setSelectionOptions }: Props) {
    const entries = Object.entries(reportSelectionLabels) as [
        keyof ReportSelectionOptions,
        string,
    ][]

    return (
        <Box
            key="selection"
            sx={{ p: 2, flex: 1, display: "flex", flexDirection: "column", gap: 3 }}>
            {Array.from({ length: Math.ceil(entries.length / 3) }, (_, rowIndex) => (
                <Box key={rowIndex} sx={{ display: "flex", gap: 2 }}>
                    {entries.slice(rowIndex * 3, rowIndex * 3 + 3).map(([key, label]) => (
                        <Box key={key} sx={{ flex: 1 }}>
                            <Switch
                                value={selectionOptions[key]}
                                label={label}
                                onChange={(isChecked) =>
                                    setSelectionOptions((prev) => ({
                                        ...prev,
                                        [key]: isChecked,
                                    }))
                                }
                            />
                        </Box>
                    ))}
                    {entries.length % 3 !== 0 &&
                        rowIndex === Math.floor(entries.length / 3) &&
                        [...Array(3 - (entries.length % 3))].map((_, i) => (
                            <Box key={`empty-${i}`} sx={{ flex: 1 }} />
                        ))}
                </Box>
            ))}
        </Box>
    )
}
