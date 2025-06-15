import { Page } from "@/tools/page"
import { useCallback, useEffect, useRef, useState, useTransition } from "react"

interface Props<T> {
    load: (page: number) => Promise<Page<T>>
}

interface Returns<T> {
    values: T[]
    isLoading: boolean
    lastElementRef: (node: HTMLTableRowElement) => void
    refresh: () => void
}

function useLoadData<T>({ load }: Props<T>): Returns<T> {
    const page = useRef(1)
    const hasMoreRef = useRef(true)
    const requestNumberRef = useRef(0)
    const [values, setValues] = useState<T[]>([])
    const [isLoading, startTransition] = useTransition()
    const observerRef = useRef<IntersectionObserver | null>(null)

    useEffect(() => {
        loadData()
    }, [])

    const lastElementRef = useCallback((node: HTMLTableRowElement) => {
        if (isLoading || !node) return

        if (observerRef.current) observerRef.current.disconnect()

        observerRef.current = new IntersectionObserver((entries) => {
            if (entries[0].isIntersecting) nextPage()
        })

        observerRef.current.observe(node)
    }, [])

    const nextPage = useCallback(() => {
        page.current += 1
        loadData()
    }, [])

    const refresh = useCallback(() => {
        setValues([])
        hasMoreRef.current = true
        page.current = 1
        loadData()
    }, [])

    function loadData() {
        if (!hasMoreRef.current) return

        startTransition(async () => {
            requestNumberRef.current += 1
            const requestNumber = requestNumberRef.current
            const data = await load(page.current)
            if (requestNumberRef.current !== requestNumber) return

            setValues((prev) => {
                const { values, totalRows } = data

                const newValues = [...prev, ...values]
                hasMoreRef.current = totalRows >= newValues.length

                return newValues
            })
        })
    }

    return { values, isLoading, lastElementRef, refresh }
}

export default useLoadData
