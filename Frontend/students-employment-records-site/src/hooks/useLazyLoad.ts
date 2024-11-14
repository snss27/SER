import { useCallback, useEffect, useRef, useState } from "react"

interface Returns<T> {
    values: T[]
    isLoading: boolean
    lastElementRef: (node: HTMLTableRowElement) => void
    nextPage: () => void
    updateValues: () => Promise<void>
}

interface Props<T> {
    paginationFunction: (page: number, pageSize: number) => Promise<T[]>
    pageSize?: number
}

//TODO Проверить работу функции updateValues
const useLazyLoad = <T>({ paginationFunction, pageSize = 20 }: Props<T>): Returns<T> => {
    const [page, setPage] = useState(1)
    const [values, setValues] = useState<T[]>([])
    const [isLoading, setIsLoading] = useState(false)
    const hasMoreRef = useRef(true)

    async function load() {
        return await paginationFunction(page, pageSize)
    }

    const observerRef = useRef<IntersectionObserver>()

    const lastElementRef = useCallback(
        (node: HTMLTableRowElement) => {
            if (isLoading) return

            if (observerRef.current) observerRef.current.disconnect()

            observerRef.current = new IntersectionObserver((entries) => {
                if (entries[0].isIntersecting) nextPage()
            })

            if (node) observerRef.current.observe(node)
        },
        [isLoading]
    )

    useEffect(() => {
        loadData()
    }, [page])

    async function loadData() {
        if (!hasMoreRef.current) return

        setIsLoading(true)

        const data = await load()

        setValues((prev) => [...prev, ...data])
        hasMoreRef.current = data.length > 0
        setIsLoading(false)
    }

    function nextPage() {
        setPage((prev) => prev + 1)
    }

    async function updateValues() {
        setIsLoading(true)
        const updatedValues = await paginationFunction(1, values.length - 1)
        setValues(updatedValues)
        setIsLoading(false)
    }

    return { values, nextPage, isLoading, lastElementRef, updateValues }
}

export default useLazyLoad
