import Sidebar from "@/components/sidebar"
import "@fontsource/roboto/300.css"
import "@fontsource/roboto/400.css"
import "@fontsource/roboto/500.css"
import "@fontsource/roboto/700.css"
import { CssBaseline, ThemeProvider } from "@mui/material"
import { AppProps } from "next/app"
import Head from "next/head"
import theme from "../constants/muiTheme"
import "../styles/global.css"

const App = ({ Component, pageProps }: AppProps) => {
    return (
        <>
            <Head>
                <title>Система управление трудоустройством студентов</title>
            </Head>
            <ThemeProvider theme={theme}>
                <CssBaseline />
                <Sidebar />
                <main>
                    <Component {...pageProps} />
                </main>
            </ThemeProvider>
        </>
    )
}

export default App
