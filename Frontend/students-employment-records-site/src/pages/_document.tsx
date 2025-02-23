import { Head, Html, Main, NextScript } from "next/document"

const Document = () => {
    return (
        <Html lang="ru">
            <Head>
                <link rel="icon" href="/favicon.svg" type="image/svg+xml" />
                <link
                    rel="stylesheet"
                    href="https://fonts.googleapis.com/icon?family=Material+Icons"
                />
            </Head>
            <body style={{ margin: 0, overflowX: "hidden" }}>
                <Main />
                <NextScript />
            </body>
        </Html>
    )
}

export default Document
