import DeleteIcon from "@mui/icons-material/Delete"
import { Box, Button, IconButton, LinearProgress, Typography } from "@mui/material"
import { useCallback, useState } from "react"
import { useDropzone } from "react-dropzone"

interface FileUploaderProps {
    files: string[]
    maxFiles: number
    onUpload: (files: File[]) => Promise<string[]>
    onChange: (urls: string[]) => void
}

function isImage(url: string) {
    return /\.(jpg|jpeg|png|gif|webp)$/i.test(url)
}

function isPdf(url: string) {
    return /\.pdf$/i.test(url)
}

export default function FilesInput({ files, maxFiles, onUpload, onChange }: FileUploaderProps) {
    const [uploading, setUploading] = useState(false)

    const handleDrop = useCallback(
        async (acceptedFiles: File[]) => {
            const available = maxFiles - files.length
            if (acceptedFiles.length > available) {
                alert(`Осталось только ${available} слота(ов) для загрузки`)
                return
            }

            setUploading(true)
            try {
                const uploadedUrls = await onUpload(acceptedFiles)
                onChange([...files, ...uploadedUrls])
            } finally {
                setUploading(false)
            }
        },
        [files, maxFiles, onUpload, onChange]
    )

    const handleDelete = (index: number) => {
        const updated = [...files]
        updated.splice(index, 1)
        onChange(updated)
    }

    const { getRootProps, getInputProps, isDragActive } = useDropzone({ onDrop: handleDrop })

    return (
        <Box display="flex" flexDirection="column" gap={2}>
            <Box display="flex" flexWrap="wrap" gap={2}>
                {files.map((url, index) => (
                    <Box key={index} position="relative" width={128} height={128}>
                        {isImage(url) ? (
                            <img
                                src={url}
                                alt="uploaded"
                                style={{
                                    width: "100%",
                                    height: "100%",
                                    objectFit: "cover",
                                    borderRadius: 8,
                                }}
                            />
                        ) : (
                            <Box
                                width="100%"
                                height="100%"
                                display="flex"
                                alignItems="center"
                                justifyContent="center"
                                border="1px solid #ccc"
                                borderRadius={2}>
                                <Typography
                                    component="a"
                                    href={url}
                                    target="_blank"
                                    rel="noopener noreferrer">
                                    {isPdf(url) ? "📄 PDF" : "📁 Файл"}
                                </Typography>
                            </Box>
                        )}
                        <IconButton
                            size="small"
                            onClick={() => handleDelete(index)}
                            sx={{ position: "absolute", top: 0, right: 0, bgcolor: "white" }}>
                            <DeleteIcon fontSize="small" />
                        </IconButton>
                    </Box>
                ))}
            </Box>

            <Box
                {...getRootProps()}
                sx={{
                    p: 3,
                    border: "2px dashed #ccc",
                    borderRadius: 2,
                    textAlign: "center",
                    bgcolor: isDragActive ? "#e3f2fd" : "#fafafa",
                    cursor: "pointer",
                }}>
                <input {...getInputProps()} />
                <Typography>
                    {isDragActive
                        ? "Отпустите файлы для загрузки"
                        : "Перетащите файлы сюда или нажмите для выбора"}
                </Typography>
            </Box>

            {uploading && <LinearProgress />}

            <Button
                variant="contained"
                disabled={files.length >= maxFiles || uploading}
                onClick={() =>
                    document.querySelector<HTMLInputElement>('input[type="file"]')?.click()
                }>
                Загрузить
            </Button>
        </Box>
    )
}
