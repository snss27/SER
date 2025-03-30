const API_KEY = "51991931ac6fb725aa758d53fdad9f42ce2623e6"
const URL = "https://suggestions.dadata.ru/suggestions/api/4_1/rs/suggest/address"

export class LocationsProvider {
    public static async search(searchText: string): Promise<string[]> {
        const body = {
            query: searchText.trim(),
            count: 10,
            language: "RU",
            division: "ADMINISTRATIVE",
            locations_geo: [
                {
                    lat: 55.079813,
                    lon: 38.814389,
                    radius_meters: 100_000,
                },
            ],
            locations_boost: [{ kladr_id: "5005100000000" }],
        }

        const response = await fetch(URL, {
            method: "POST",
            mode: "cors",
            headers: {
                "Content-Type": "application/json",
                Accept: "application/json",
                Authorization: "Token " + API_KEY,
            },
            body: JSON.stringify(body),
        })

        const data = await response.json()

        return (data.suggestions as any[]).map((suggestion) => suggestion.value)
    }
}
