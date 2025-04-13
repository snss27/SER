import { BlankFiles } from "@/tools/blankFiles"

const CACHED_FILES: Map<string, string> = new Map()
const CONTENT_TYPES_TO_CACHE = ["image/jpeg", "image/png", "image/jpg", "application/pdf"]

export class HttpClient {
    private static host = "https://localhost:44377/api"

    public static async getJsonAsync(url: string, params?: any): Promise<any> {
        const fullUrl = `${this.host}${url}${HttpClient.toQueryString(params)}`

        const response = await HttpClient.httpHandler(
            await fetch(fullUrl, {
                method: "GET",
                headers: HttpClient.headers,
            })
        )

        if (response.status === 204) return null

        return await response.json()
    }

    public static async postJsonAsync(
        url: string,
        data: any = null,
        params: any = null
    ): Promise<any> {
        const fullUrl = `${this.host}${url}${
            params != null ? HttpClient.toQueryString(params) : ""
        }`

        const response = await HttpClient.httpHandler(
            await fetch(fullUrl, {
                method: "POST",
                headers: HttpClient.headers,
                body: JSON.stringify(data),
            })
        )

        return await response.json()
    }

    public static async postFormDataAsync(
        url: string,
        data: any,
        params: any = null
    ): Promise<any> {
        const fullUrl = `${this.host}${url}${
            params != null ? HttpClient.toQueryString(params) : ""
        }`

        const formData = new FormData()
        this.appendToFormData(formData, data)

        const response = await HttpClient.httpHandler(
            await fetch(fullUrl, {
                method: "POST",
                headers: {
                    "X-Requested-With": "XMLHttpRequest",
                    Accept: "application/json",
                },
                body: formData,
            })
        )

        return await response.json()
    }

    private static appendToFormData(formData: FormData, data: any, prefix = "") {
        if (data === null || data === undefined) return

        if (data instanceof File) {
            formData.append(prefix || "file", data)
        } else if (data instanceof BlankFiles) {
            formData.append(`${prefix}.MaxFiles`, data.maxFiles.toString())

            if (data.files && data.files.length > 0) {
                data.files.forEach((file: File) => {
                    formData.append(`${prefix}.Files`, file)
                })
            }

            if (data.fileUrls && data.fileUrls.length > 0) {
                data.fileUrls.forEach((url: string) => {
                    formData.append(`${prefix}.FileUrls`, url)
                })
            }
        } else if (data instanceof Date) {
            const hours = data.getHours()
            data.setHours(hours + 3)
            formData.append(prefix, data.toISOString()) // 👈 добавлено
        } else if (Array.isArray(data)) {
            if (data.length === 0) {
                formData.append(`${prefix}[0]`, "")
            } else {
                data.forEach((item, index) => {
                    const newPrefix = prefix ? `${prefix}[${index}]` : `[${index}]`
                    this.appendToFormData(formData, item, newPrefix)
                })
            }
        } else if (typeof data === "object") {
            Object.entries(data).forEach(([key, value]) => {
                const newPrefix = prefix ? `${prefix}.${key}` : key
                this.appendToFormData(formData, value, newPrefix)
            })
        } else {
            formData.append(prefix, String(data))
        }
    }

    private static checkForFiles(obj: any): boolean {
        if (!obj) return false
        if (obj instanceof File) return true
        if (Array.isArray(obj)) {
            return obj.some((item) => this.checkForFiles(item))
        }
        if (typeof obj === "object") {
            return Object.values(obj).some((value) => this.checkForFiles(value))
        }
        return false
    }

    public static async getFileWithCache(
        fileUrl: string,
        params: any = null
    ): Promise<string | null> {
        const fullUrl = `${this.host}${fileUrl}${
            params != null ? HttpClient.toQueryString(params) : ""
        }`

        let blobUrl = CACHED_FILES.get(fullUrl) ?? null
        if (blobUrl == null) {
            const response = await HttpClient.httpFileHandler(
                await fetch(fullUrl, {
                    method: "GET",
                    headers: HttpClient.headers,
                    cache: "force-cache",
                })
            )
            if (response == null) return null

            const blob = await response.blob()
            blobUrl = URL.createObjectURL(blob)

            if (CONTENT_TYPES_TO_CACHE.includes(blob.type)) CACHED_FILES.set(fullUrl, blobUrl)
        }

        return blobUrl ?? null
    }

    private static toQueryString(obj: any) {
        if (obj == null) return ""

        const parameters = []

        for (const key of Object.keys(obj)) {
            const value = obj[key]
            if (value == null) continue

            if (Array.isArray(value)) {
                const values = value as any[]
                if (values.length === 0) continue

                for (const v of values)
                    parameters.push(`${encodeURIComponent(key)}=${encodeURIComponent(v)}`)
            } else if (value instanceof Date) {
                parameters.push(
                    `${encodeURIComponent(key)}=${encodeURIComponent(value.toISOString())}`
                )
            } else {
                parameters.push(`${encodeURIComponent(key)}=${encodeURIComponent(value)}`)
            }
        }

        if (parameters.length === 0) return ""

        return "?" + parameters.join("&")
    }

    private static get headers(): Headers {
        const headers: Headers = new Headers()

        headers.append("X-Requested-With", "XMLHttpRequest")
        headers.append("Content-Type", "application/json")
        headers.append("Accept", "application/json")

        return headers
    }

    private static httpFileHandler(response: Response): Promise<Response | null> {
        if (response.redirected) {
            window.location.href = response.url
            return Promise.reject()
        }

        if (response.ok) return Promise.resolve(response)
        if (response.status == 404) return Promise.resolve(null)

        switch (response.status) {
            case 403:
                return Promise.reject(new Error("Forbidden"))
        }

        return Promise.reject(`${response.status} - unknown status code`)
    }

    private static httpHandler(response: Response): Promise<Response> {
        if (response.redirected) {
            window.location.href = response.url
            return Promise.reject()
        }

        if (response.ok) return Promise.resolve(response)

        //TODO Сделать страницы с ошибками?
        // switch (response.status) {
        //     case 403:
        //         window.location.href = InfrastructureLinks.forbidden
        //         return Promise.reject(new Error('Forbidden'));
        //     case 404:
        //         window.location.href = InfrastructureLinks.notFound;
        //         return Promise.reject(new Error('Not Found'));
        // }

        return Promise.reject(`${response.status} - unknown status code`)
    }
}

export default HttpClient
