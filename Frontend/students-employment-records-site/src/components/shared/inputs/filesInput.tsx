import HttpClient from "@/domain/httpClient"
import useNotifications from "@/hooks/useNotifications"
import AddIcon from "@mui/icons-material/Add"
import DeleteIcon from "@mui/icons-material/Delete"
import { Box, IconButton, Tooltip, Typography } from "@mui/material"
import { useTransition } from "react"
import { useDropzone } from "react-dropzone"

interface FileUploaderProps {
    files: string[]
    maxFiles: number
    folder: string
    label?: string
    onChange: (urls: string[]) => void
}

function isImage(url: string) {
    return /\.(jpg|jpeg|png|gif|webp)$/i.test(url)
}

function getFileName(url: string) {
    const decoded = decodeURIComponent(url.split("/").pop() || "Файл")
    const index = decoded.indexOf("_")
    if (index !== -1) {
        return decoded.slice(index + 1)
    }
    return decoded
}

export default function FilesInput({
    files,
    maxFiles,
    folder,
    label = "Файлы",
    onChange,
}: FileUploaderProps) {
    const [isUploading, startUploading] = useTransition()
    const { showError } = useNotifications()

    function handleDrop(acceptedFiles: File[]) {
        const available = maxFiles - files.length
        if (acceptedFiles.length > available) {
            showError(`Можно загрузить максимум ещё ${available} файл(ов)`)
            return
        }

        startUploading(async () => {
            try {
                const uploadedUrls = await HttpClient.uploadFilesAsync(acceptedFiles, folder)
                onChange([...files, ...uploadedUrls])
            } catch (error) {
                showError(`Ошибка загрузки`)
            }
        })
    }

    function handleDelete(index: number) {
        const fileUrl = files[index]
        try {
            HttpClient.deleteFileAsync(fileUrl)
        } catch {
            //ignore
        }

        const updated = [...files]
        updated.splice(index, 1)
        onChange(updated)
    }

    const { getRootProps, getInputProps, isDragActive } = useDropzone({
        onDrop: handleDrop,
    })

    return (
        <Box>
            {label && (
                <Typography variant="h6" sx={{ mb: 1 }}>
                    {label} ({files.length}/{maxFiles})
                </Typography>
            )}
            <Box display="flex" gap={2} sx={{ overflowX: "auto", height: 236 }}>
                {files.map((url, index) => (
                    <Box key={index} position="relative" height={220}>
                        {isImage(url) ? (
                            <a href={url} target="_blank" rel="noopener noreferrer">
                                <img
                                    src={url}
                                    alt="uploaded"
                                    style={{
                                        height: "100%",
                                        borderRadius: 8,
                                    }}
                                />
                            </a>
                        ) : (
                            <Box
                                sx={{
                                    width: 220,
                                    height: 220,
                                    display: "flex",
                                    justifyContent: "center",
                                    alignItems: "center",
                                }}
                                border="1px solid #ccc"
                                borderRadius={2}>
                                <Typography
                                    component="a"
                                    href={url}
                                    target="_blank"
                                    rel="noopener noreferrer"
                                    textAlign="center"
                                    sx={{
                                        fontSize: 14,
                                        textDecoration: "none",
                                        paddingX: 2,
                                        wordBreak: "break-word",
                                    }}>
                                    {getFileName(url)}
                                </Typography>
                            </Box>
                        )}
                        <IconButton
                            size="small"
                            onClick={() => handleDelete(index)}
                            sx={{
                                position: "absolute",
                                top: 8,
                                right: 8,
                                bgcolor: "#B5B8B1",
                            }}>
                            <DeleteIcon sx={{ color: "#474A51" }} fontSize="small" />
                        </IconButton>
                    </Box>
                ))}

                {files.length < maxFiles && (
                    <div {...getRootProps()}>
                        <input {...getInputProps()} />
                        <Tooltip title="Добавить файл">
                            <IconButton
                                disabled={isUploading}
                                sx={{
                                    display: "flex",
                                    justifyContent: "center",
                                    alignItems: "center",
                                    width: 220,
                                    height: 220,
                                    border: "2px dashed #ccc",
                                    borderRadius: 2,
                                    bgcolor: isDragActive ? "#e3f2fd" : "#fafafa",
                                }}>
                                <AddIcon />
                            </IconButton>
                        </Tooltip>
                    </div>
                )}
            </Box>
        </Box>
    )
}
