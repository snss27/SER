import { useEffect, useRef } from "react"

const useDebounce = (
    action: () => void,
    variables: any[],
    delay: number,
    skipFirstChange: boolean = false
) => {
    const changeCount = useRef(0)

    useEffect(() => {
        changeCount.current++

        if (skipFirstChange && changeCount.current === 1) return

        const handler = setTimeout(() => action(), delay)
        return () => clearTimeout(handler)
    }, [...variables, delay])
}

export default useDebounce
