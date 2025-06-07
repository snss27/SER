import * as React from "react"

export function useLicenseVerifier() {
    return React.useMemo(
        () => ({
            status: "Valid",
        }),
        []
    )
}

export default useLicenseVerifier
