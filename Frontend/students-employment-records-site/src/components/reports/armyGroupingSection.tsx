import { ArmyGroupingVariant } from "@/domain/reports/models/armyGroupingVariant"
import { Action } from "@/domain/reports/models/reportGroupingOptions"
import { Box, Collapse, FormControlLabel, Radio, Switch } from "@mui/material"
import { DateRangePicker } from "../shared/inputs/dateRangePicker"

export const ArmyGroupingSection = ({
    armyGroupingVariant,
    dispatch,
}: {
    armyGroupingVariant: ArmyGroupingVariant | null
    dispatch: React.Dispatch<Action>
}) => {
    const handleMustServeChange = (mustServe: boolean | null) => {
        if (mustServe === null) {
            dispatch({
                type: "CHANGE_ARMY_GROUPING_VARIANT",
                payload: { armyGroupingVariant: null },
            })
            return
        }

        dispatch({
            type: "CHANGE_ARMY_GROUPING_VARIANT",
            payload: {
                armyGroupingVariant: mustServe
                    ? { mustServe: true, armyCallDatePeriod: [null, null] }
                    : { mustServe: false },
            },
        })
    }

    const handlePeriodChange = (period: [Date | null, Date | null]) => {
        if (!armyGroupingVariant || !armyGroupingVariant.mustServe) return

        dispatch({
            type: "CHANGE_ARMY_GROUPING_VARIANT",
            payload: {
                armyGroupingVariant: {
                    mustServe: true,
                    armyCallDatePeriod: period,
                },
            },
        })
    }

    return (
        <Box sx={{ display: "flex", flexDirection: "column" }}>
            <FormControlLabel
                control={
                    <Switch
                        checked={armyGroupingVariant !== null}
                        onChange={(e) => handleMustServeChange(e.target.checked ? true : null)}
                    />
                }
                label="Группировать по службе в армии"
            />

            <Collapse
                in={armyGroupingVariant !== null}
                sx={{ display: "flex", flexDirection: "column" }}>
                <FormControlLabel
                    control={
                        <Radio
                            checked={armyGroupingVariant ? armyGroupingVariant.mustServe : false}
                            onChange={() => handleMustServeChange(true)}
                        />
                    }
                    label="Только подлежащие призыву"
                />

                <FormControlLabel
                    control={
                        <Radio
                            checked={armyGroupingVariant ? !armyGroupingVariant.mustServe : false}
                            onChange={() => handleMustServeChange(false)}
                        />
                    }
                    label="Только НЕ подлежащие призыву"
                />

                <Collapse in={armyGroupingVariant?.mustServe} sx={{ mt: 1 }}>
                    <DateRangePicker
                        label="Период повестки"
                        value={
                            armyGroupingVariant?.mustServe
                                ? armyGroupingVariant.armyCallDatePeriod
                                : [null, null]
                        }
                        onChange={handlePeriodChange}
                    />
                </Collapse>
            </Collapse>
        </Box>
    )
}
