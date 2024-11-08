class Id {
    public static new() {
        return crypto.randomUUID().replace(/-/g, "").toUpperCase()
    }
}

export default Id
