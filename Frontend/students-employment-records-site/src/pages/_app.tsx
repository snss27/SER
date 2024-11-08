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
import "../styles/global.css"

const App = ({ Component, pageProps }: AppProps) => {
    return (
        <Box sx={{ display: "flex" }}>
            <Head>
                <title>Система управление трудоустройством студентов</title>
            </Head>
            <ThemeProvider theme={theme}>
                <LocalizationProvider dateAdapter={AdapterDateFns} adapterLocale={ru}>
                    <CssBaseline />
                    <Sidebar />
                    <main style={{ flexGrow: 1, height: "100vh", padding: "8px" }}>
                        <Component {...pageProps} />
                    </main>
                </LocalizationProvider>
            </ThemeProvider>
        </Box>
    )
}

export default App
