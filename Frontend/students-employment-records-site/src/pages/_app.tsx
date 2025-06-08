import { AppBase } from "@/components/appBase"
import Sidebar from "@/components/sidebar/sidebar"
import "@fontsource/roboto/300.css"
import "@fontsource/roboto/400.css"
import "@fontsource/roboto/500.css"
import "@fontsource/roboto/700.css"
import { Box } from "@mui/material"
import { AppProps } from "next/app"
import { useRouter } from "next/router"
import "../styles/global.css"

const App = ({ Component, pageProps }: AppProps) => {
    const router = useRouter()

    const publicPages = ["/login"]
    const isPublicPage = publicPages.includes(router.pathname)

    return (
        <AppBase>
            {!isPublicPage ? (
                <>
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
                                p: 2,
                            }}>
                            <Component {...pageProps} />
                        </Box>
                    </Box>{" "}
                </>
            ) : (
                <Component {...pageProps} />
            )}
        </AppBase>
    )
}

export default App
