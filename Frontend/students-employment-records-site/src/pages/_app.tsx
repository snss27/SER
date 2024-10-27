import Sidebar from "@/components/sidebar/sidebar"
import "@fontsource/roboto/300.css"
import "@fontsource/roboto/400.css"
import "@fontsource/roboto/500.css"
import "@fontsource/roboto/700.css"
import { Box, CssBaseline, ThemeProvider } from "@mui/material"
import { AppProps } from "next/app"
import Head from "next/head"
import theme from "../constants/muiTheme"
import "../styles/global.css"

const App = ({ Component, pageProps }: AppProps) => {
    return (
        <Box sx={{ display: "flex" }}>
            <Head>
                <title>Система управление трудоустройством студентов</title>
            </Head>
            <ThemeProvider theme={theme}>
                <CssBaseline />
                <Sidebar />
                <main>
                    <Box sx={{ flexGrow: 1, height: "100vh", padding: "8px" }}>
                        <Component {...pageProps} />
                    </Box>
                </main>
            </ThemeProvider>
        </Box>
    )
}

export default App
