import path from "path"

/** @type {import('next').NextConfig} */
const nextConfig = {
    async redirects() {
        return [
            {
                source: "/",
                destination: "/students",
                permanent: true,
            },
        ]
    },
    webpack: (config) => {
        config.resolve.alias = {
            ...config.resolve.alias,
            "@mui/x-license/esm/useLicenseVerifier/useLicenseVerifier.js": path.resolve(
                process.cwd(),
                "hack/useLicenseVerifier.js"
            ),
        }
        return config
    },
}

export default nextConfig
