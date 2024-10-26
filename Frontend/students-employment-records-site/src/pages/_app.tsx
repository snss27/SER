import Sidebar from "@/components/sidebar"
import "@fontsource/roboto/300.css"
import "@fontsource/roboto/400.css"
import "@fontsource/roboto/500.css"
import "@fontsource/roboto/700.css"
import { CssBaseline, ThemeProvider } from "@mui/material"
import { AppProps } from "next/app"
import theme from "../constants/muiTheme"
import "../styles/global.css"

const App = ({ Component, pageProps }: AppProps) => {
    return (
        <ThemeProvider theme={theme}>
            <CssBaseline />
            <Sidebar />
            <main>
                <Component {...pageProps} />
            </main>
        </ThemeProvider>
    )
}

export default App
