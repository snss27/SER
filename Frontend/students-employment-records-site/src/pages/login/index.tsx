import { IconType } from "@/components/shared/buttons"
import Button from "@/components/shared/buttons/button"
import TextInput from "@/components/shared/inputs/textInput"
import PageUrls from "@/constants/pageUrls"
import { AuthProvider } from "@/domain/auth/authProvider"
import { AuthRequest } from "@/domain/auth/models/authRequest"
import useNotifications from "@/hooks/useNotifications"
import { Box, Stack, Typography } from "@mui/material"
import { useRouter } from "next/router"
import { useReducer } from "react"

const LoginPage = () => {
    const [authRequest, dispatch] = useReducer(AuthRequest.reducer, AuthRequest.empty())
    const { showError } = useNotifications()
    const router = useRouter()

    async function auth() {
        const result = await AuthProvider.Auth(authRequest)

        if (!result.isSuccess) {
            dispatch({ type: "RESET" })
            showError(result.getErrorsString)
            return
        }

        router.push(PageUrls.Students)
    }

    return (
        <Box sx={{ display: "flex", alignItems: "center", justifyContent: "center", flex: 1 }}>
            <Stack direction="column" sx={{ width: 440, gap: 3 }}>
                <Typography variant="h1">Вход</Typography>
                <TextInput
                    label="Логин"
                    value={authRequest.login}
                    onChange={(login) => dispatch({ type: "CHANGE_LOGIN", payload: { login } })}
                />
                <TextInput
                    type="password"
                    label="Пароль"
                    value={authRequest.password}
                    onChange={(password) =>
                        dispatch({ type: "CHANGE_PASSWORD", payload: { password } })
                    }
                />
                <Box sx={{ alignSelf: "flex-end" }}>
                    <Button icon={{ type: IconType.Save }} text="Подтвердить" onClick={auth} />
                </Box>
            </Stack>
        </Box>
    )
}

export default LoginPage
