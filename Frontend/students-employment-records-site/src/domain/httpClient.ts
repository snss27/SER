import { InfrastructureUrls } from "@/constants/infrastructureUrls"

export class HttpClient {
    private static apiHost = process.env.NEXT_PUBLIC_API_URL
    private static fileStorageHost = process.env.NEXT_PUBLIC_FILE_STORAGE_URL
    private static fileStorageSecretKey = process.env.NEXT_PUBLIC_FILE_STORAGE_SECRET_KEY
    private static apiKeyword: string = process.env.NEXT_PUBLIC_API_KEYWORD!

    public static async getJsonAsync(url: string, params?: any): Promise<any> {
        const fullUrl = `${this.apiHost}${url}${HttpClient.toQueryString(params)}`

        const response = await HttpClient.httpHandler(
            await fetch(fullUrl, {
                method: "GET",
                headers: HttpClient.headers,
                credentials: "include",
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
        const fullUrl = `${this.apiHost}${url}${
            params != null ? HttpClient.toQueryString(params) : ""
        }`

        const response = await HttpClient.httpHandler(
            await fetch(fullUrl, {
                method: "POST",
                headers: HttpClient.headers,
                body: JSON.stringify(this.convertDatesWithOffset(data)),
                credentials: "include",
            })
        )

        return await response.json()
    }

    public static async uploadFilesAsync(files: File[], folder: string): Promise<string[]> {
        const keyword = this.fileStorageSecretKey

        const uploadTasks = files.map((file) => {
            const filePath = folder
                ? `${folder}/${Date.now()}_${file.name}`
                : `${Date.now()}_${file.name}`
            const query = this.toQueryString({ path: filePath, keyword })
            const formData = new FormData()
            formData.append("file", file)

            return fetch(`${this.fileStorageHost}/upload${query}`, {
                method: "POST",
                body: formData,
            }).then((response) => {
                if (!response.ok) throw new Error(`Upload failed for ${file.name}`)
                return `${this.fileStorageHost}/${filePath}`
            })
        })

        return Promise.all(uploadTasks)
    }

    public static async deleteFileAsync(url: string): Promise<void> {
        const keyword = this.fileStorageSecretKey
        const host = this.fileStorageHost
        const path = url.replace(`${host}/`, "")

        const query = this.toQueryString({ path, keyword })

        const response = await fetch(`${host}/delete${query}`, {
            method: "DELETE",
        })

        if (!response.ok) {
            const errorText = await response.text()
            throw new Error(`Ошибка удаления: ${errorText}`)
        }
    }

    private static toQueryString(obj: any) {
        if (obj == null) return ""
        obj = this.convertDatesWithOffset(obj)

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

    private static convertDatesWithOffset(obj: any): any {
        if (obj instanceof Date) {
            const adjusted = new Date(obj.getTime() + 3 * 60 * 60 * 1000)
            return adjusted.toISOString()
        }

        if (Array.isArray(obj)) {
            return obj.map((item) => this.convertDatesWithOffset(item))
        }

        if (obj !== null && typeof obj === "object") {
            const newObj: any = {}
            for (const key in obj) {
                newObj[key] = this.convertDatesWithOffset(obj[key])
            }
            return newObj
        }

        return obj
    }

    private static get headers(): Headers {
        const headers: Headers = new Headers()

        headers.append("X-Requested-With", "XMLHttpRequest")
        headers.append("Content-Type", "application/json")
        headers.append("Accept", "application/json")
        headers.append("KeywordApi", this.apiKeyword)

        return headers
    }

    private static httpHandler(response: Response): Promise<Response> {
        if (response.redirected) {
            window.location.href = response.url
            return Promise.reject(new Error("Redirected"))
        }

        if (response.ok) return Promise.resolve(response)

        switch (response.status) {
            case 401: {
                window.location.href = InfrastructureUrls.Login
                return Promise.reject(new Error("Unauthorized"))
            }
        }

        return Promise.reject(`${response.status} - unknown status code`)
    }
}

export default HttpClient
