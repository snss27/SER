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
    const [values, setValues] = useState<T[]>([])
    const [isLoading, startTransition] = useTransition()
    const observerRef = useRef<IntersectionObserver>()

    useEffect(() => {
        loadData()
    }, [])

    const lastElementRef = useCallback((node: HTMLTableRowElement) => {
        if (isLoading) return

        if (observerRef.current) observerRef.current.disconnect()

        observerRef.current = new IntersectionObserver((entries) => {
            if (entries[0].isIntersecting) nextPage()
        })
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
            const data = await load(page.current)

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
