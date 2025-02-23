import Sidebar from "@/components/sidebar/sidebar"
import theme from "@/constants/muiTheme"
import { DialogProvider } from "@/hooks/useDialog/dialogProvider"
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
                            <Sidebar />
                            <Box
                                component="main"
                                sx={{
                                    flexGrow: 1,
                                    p: 3,
                                    ml: "220px",
                                    width: { xs: "100%", sm: "calc(100% - 220px)" },
                                    minHeight: "100vh",
                                    display: "flex",
                                    flexDirection: "column",
                                }}>
                                <Box
                                    sx={{
                                        flex: 1,
                                        overflowY: "auto",
                                        "-webkit-overflow-scrolling": "touch",
                                        pr: 2,
                                        mb: 2,
                                    }}>
                                    <Component {...pageProps} />
                                </Box>
                            </Box>
                        </DialogProvider>
                    </SnackbarProvider>
                </LocalizationProvider>
            </ThemeProvider>
        </Box>
    )
}

export default App
