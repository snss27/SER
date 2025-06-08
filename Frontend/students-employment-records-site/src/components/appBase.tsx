import theme from "@/constants/muiTheme"
import { DialogProvider } from "@/hooks/useDialog/dialogProvider"
import { Box, CssBaseline, ThemeProvider } from "@mui/material"
import { LocalizationProvider } from "@mui/x-date-pickers-pro"
import { AdapterDateFns } from "@mui/x-date-pickers/AdapterDateFns"
import { ru } from "date-fns/locale/ru"
import Head from "next/head"
import { SnackbarProvider } from "notistack"
import { PropsWithChildren } from "react"

export function AppBase({ children }: PropsWithChildren) {
    return (
        <Box sx={{ display: "flex", minHeight: "100vh" }}>
            <Head>
                <title>Система управления трудоустройством студентов</title>
                <meta name="viewport" content="initial-scale=1, width=device-width" />
            </Head>
            <ThemeProvider theme={theme}>
                <LocalizationProvider dateAdapter={AdapterDateFns} adapterLocale={ru}>
                    <SnackbarProvider maxSnack={3}>
                        <DialogProvider>
                            <CssBaseline />
                            {children}
                        </DialogProvider>
                    </SnackbarProvider>
                </LocalizationProvider>
            </ThemeProvider>
        </Box>
    )
}
