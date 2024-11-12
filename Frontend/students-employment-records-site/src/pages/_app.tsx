import Sidebar from "@/components/sidebar/sidebar"
import theme from "@/constants/muiTheme"
import "@fontsource/roboto/300.css"
import "@fontsource/roboto/400.css"
import "@fontsource/roboto/500.css"
import "@fontsource/roboto/700.css"
import { Box, CssBaseline, ThemeProvider } from "@mui/material"
import { LocalizationProvider } from "@mui/x-date-pickers"
import { AdapterDateFns } from "@mui/x-date-pickers/AdapterDateFnsV3"
import { ru } from "date-fns/locale/ru"
import { AppProps } from "next/app"
import Head from "next/head"
import { SnackbarProvider } from "notistack"
import "../styles/global.css"

const App = ({ Component, pageProps }: AppProps) => {
    return (
        <Box>
            <Head>
                <title>Система управление трудоустройством студентов</title>
            </Head>
            <ThemeProvider theme={theme}>
                <LocalizationProvider dateAdapter={AdapterDateFns} adapterLocale={ru}>
                    <SnackbarProvider>
                        <CssBaseline />
                        <Sidebar />
                        <Box
                            component="main"
                            sx={{
                                width: "calc(100% - 220px)",
                                ml: "220px",
                                height: "100vh",
                                padding: 3,
                                overflow: "auto",
                            }}>
                            <Component {...pageProps} />
                        </Box>
                    </SnackbarProvider>
                </LocalizationProvider>
            </ThemeProvider>
        </Box>
    )
}

export default App
