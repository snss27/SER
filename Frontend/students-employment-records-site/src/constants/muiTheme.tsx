import { autocompleteClasses, Box, createTheme } from "@mui/material"

const theme = createTheme({
    palette: {
        mode: "light",

        primary: {
            main: "#000000",
            dark: "#C6BEBC",
        },

        info: {
            main: "#EE823D",
        },

        warning: {
            main: "#AD4D42",
        },

        background: {
            default: "#FFFFFF",
        },
    },

    components: {
        MuiDateCalendar: {
            styleOverrides: {
                root: {
                    borderRadius: "4px",
                },
            },
        },
        MuiAutocomplete: {
            defaultProps: {
                renderOption: (props, option, state, ownerState) => {
                    const { key, ...optionProps } = props
                    return (
                        <Box
                            key={key}
                            sx={{
                                fontSize: "18px",
                                borderRadius: "8px",
                                marginX: "10px",
                                [`&.${autocompleteClasses.option}`]: {
                                    padding: "10px",
                                    marginY: "6px",
                                },
                            }}
                            component="li"
                            {...optionProps}>
                            {ownerState.getOptionLabel(option)}
                        </Box>
                    )
                },
            },
        },
    },

    typography: {
        fontFamily: '"Helvetica"',

        h1: {
            fontSize: "2rem",
            fontWeight: 700,
            lineHeight: "1.5rem",
        },

        h2: {
            fontSize: "1.5rem",
            fontWeight: 600,
            lineHeight: "1.4rem",
        },

        h3: {
            fontSize: "1.25rem",
            fontWeight: 500,
            lineHeight: "1.3rem",
        },

        h4: {
            fontSize: "1.125rem",
            fontWeight: 500,
            lineHeight: "1.3rem",
        },

        body1: {
            fontSize: "1rem",
            fontWeight: 400,
            lineHeight: "1.2rem",
        },
    },
})

export default theme
