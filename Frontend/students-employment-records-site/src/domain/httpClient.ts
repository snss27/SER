export class HttpClient {
    private static host = "https://localhost:44377/api"
    private static fileStorageHost = ""

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

    public static async postFormDataAsync(url: string, data: FormData): Promise<string> {
        const response = await HttpClient.httpHandler(
            await fetch(`${this.fileStorageHost}${url}`, {
                method: "POST",
                credentials: "same-origin",
                headers: { Enctype: "multipart/form-data" },
                body: data,
            })
        )
        return await response.json()
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
