import Switch from "@/components/shared/buttons/switch"
import { DateRangePicker } from "@/components/shared/inputs/dateRangePicker"
import { ArmyGroupingOptions } from "@/domain/reports/models/grouping/armyGroupingVariant"
import { Action } from "@/domain/reports/models/grouping/reportGroupingOptions"
import { Box, Collapse, FormControlLabel, Radio } from "@mui/material"

export const ArmyGroupingSection = ({
    armyGroupingOptions,
    dispatch,
}: {
    armyGroupingOptions: ArmyGroupingOptions | null
    dispatch: React.Dispatch<Action>
}) => {
    const handleMustServeChange = (mustServe: boolean | null) => {
        if (mustServe === null) {
            dispatch({
                type: "CHANGE_ARMY_GROUPING_OPTIONS",
                payload: { armyGroupingOptions: null },
            })
            return
        }

        dispatch({
            type: "CHANGE_ARMY_GROUPING_OPTIONS",
            payload: {
                armyGroupingOptions: mustServe
                    ? { mustServe: true, armyCallDatePeriod: [null, null] }
                    : { mustServe: false },
            },
        })
    }

    const handlePeriodChange = (period: [Date | null, Date | null]) => {
        if (!armyGroupingOptions || !armyGroupingOptions.mustServe) return

        dispatch({
            type: "CHANGE_ARMY_GROUPING_OPTIONS",
            payload: {
                armyGroupingOptions: {
                    mustServe: true,
                    armyCallDatePeriod: period,
                },
            },
        })
    }

    return (
        <Box sx={{ display: "flex", flexDirection: "column" }}>
            <Switch
                label="Фильтровать по службе в армии"
                value={armyGroupingOptions !== null}
                onChange={(isChecked) => handleMustServeChange(isChecked ? true : null)}
            />

            <Collapse
                in={armyGroupingOptions !== null}
                sx={{ display: "flex", flexDirection: "column" }}>
                <FormControlLabel
                    control={
                        <Radio
                            checked={armyGroupingOptions ? armyGroupingOptions.mustServe : false}
                            onChange={() => handleMustServeChange(true)}
                        />
                    }
                    label="Только подлежащие призыву"
                />

                <FormControlLabel
                    control={
                        <Radio
                            checked={armyGroupingOptions ? !armyGroupingOptions.mustServe : false}
                            onChange={() => handleMustServeChange(false)}
                        />
                    }
                    label="Только НЕ подлежащие призыву"
                />

                <Collapse in={armyGroupingOptions?.mustServe} sx={{ mt: 1 }}>
                    <DateRangePicker
                        label="Период повестки"
                        value={
                            armyGroupingOptions?.mustServe
                                ? armyGroupingOptions.armyCallDatePeriod
                                : [null, null]
                        }
                        onChange={handlePeriodChange}
                    />
                </Collapse>
            </Collapse>
        </Box>
    )
}
