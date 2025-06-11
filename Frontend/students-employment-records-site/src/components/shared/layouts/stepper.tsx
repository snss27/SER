import { Box, Stack, Step, StepLabel, Stepper, Typography } from "@mui/material"
import { ReactNode, useState } from "react"
import Button from "../buttons/button"

interface StepperComponentProps {
    steps: string[]
    stepContent: ReactNode[]
    onComplete?: () => void
    onReset?: () => void
}

const StepperComponent = ({ steps, stepContent, onComplete, onReset }: StepperComponentProps) => {
    const [activeStep, setActiveStep] = useState(0)

    const handleNext = () => {
        if (activeStep === steps.length - 1) {
            onComplete?.()
        } else {
            setActiveStep((prev) => prev + 1)
        }
    }

    const handleBack = () => setActiveStep((prev) => prev - 1)

    const handleReset = () => {
        onReset?.()
        setActiveStep(0)
    }

    const isLastStep = activeStep === steps.length - 1
    const isFirstStep = activeStep === 0

    return (
        <Stack
            sx={{
                width: "100%",
                height: "100%",
                display: "flex",
                flexDirection: "column",
                gap: 2,
            }}>
            <Box sx={{ p: 2 }}>
                <Typography variant="h1" gutterBottom>
                    Генерация отчёта
                </Typography>
            </Box>

            <Box>
                <Stepper activeStep={activeStep} alternativeLabel>
                    {steps.map((label) => (
                        <Step key={label}>
                            <StepLabel>{label}</StepLabel>
                        </Step>
                    ))}
                </Stepper>
            </Box>

            <Box sx={{ flex: 1, overflow: "auto" }}>{stepContent[activeStep]}</Box>

            <Box
                sx={{
                    display: "flex",
                    justifyContent: "space-between",
                    pb: 2,
                    px: 2,
                }}>
                <Box sx={{ display: "flex", gap: 2 }}>
                    <Button text="Назад" onClick={handleBack} disabled={isFirstStep} />
                    <Button text="Сбросить" onClick={handleReset} />
                </Box>
                <Button
                    text={isLastStep ? "Завершить" : "Далее"}
                    variant="contained"
                    onClick={handleNext}
                />
            </Box>
        </Stack>
    )
}

export default StepperComponent
