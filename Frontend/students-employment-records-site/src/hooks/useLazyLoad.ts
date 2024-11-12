import { useEffect, useRef, useState } from "react"

interface Props<T> {
    load: () => Promise<T[]>
}

interface Returns<T> {
    values: T[]
    page: number
    nextPage: () => void
    isLoading: boolean
}

const useLazyLoad = <T>({ load }: Props<T>): Returns<T> => {
    const [page, setPage] = useState(1)
    const [values, setValues] = useState<T[]>([])
    const [isLoading, setIsLoading] = useState(false)
    const hasMoreRef = useRef(true)

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

    return { values, page, nextPage, isLoading }
}

export default useLazyLoad
