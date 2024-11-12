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
        },

        h2: {
            fontSize: "1.5rem",
            fontWeight: 600,
        },

        h3: {
            fontSize: "1.25rem",
            fontWeight: 500,
        },

        h4: {
            fontSize: "1.125rem",
            fontWeight: 500,
        },

        body1: {
            fontSize: "1rem",
            fontWeight: 400,
        },
    },
})

export default theme
