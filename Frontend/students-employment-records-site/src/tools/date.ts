export function formatDate(date: Date | null): string {
    if (!date) return ""
    return date.toLocaleDateString("ru-RU", {
        year: "numeric",
        month: "long",
        day: "numeric",
    })
}
