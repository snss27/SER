import useNotifications from "@/hooks/useNotifications"
import { Box, Dialog, DialogContent, Typography, styled } from "@mui/material"
import { useCallback, useEffect, useState } from "react"
import { useDropzone } from "react-dropzone"
import { IconType } from "../buttons"
import IconButton from "../buttons/iconButtons"

interface FileWithPreview extends File {
    preview: string
    hash?: string
}

const getFileHash = async (file: File): Promise<string> => {
    const arrayBuffer = await file.arrayBuffer()
    const hashBuffer = await crypto.subtle.digest("SHA-256", arrayBuffer)
    return Array.from(new Uint8Array(hashBuffer))
        .map((b) => b.toString(16).padStart(2, "0"))
        .join("")
}

interface IProps {
    existingUrls: string[]
    newFiles: File[]
    maxFilesCount?: number
    onFilesChange: (newFiles: File[]) => void
    onUrlsChange: (newUrls: string[]) => void
}

type FileItem = { type: "existing"; value: string } | { type: "new"; value: FileWithPreview }
const MAX_FILE_SIZE_MB = 10
const MAX_FILE_SIZE_BYTES = MAX_FILE_SIZE_MB * 1024 * 1024

const PreviewContainer = styled(Box)({
    position: "relative",
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
    maxWidth: "90vw",
    maxHeight: "90vh",
})

const StyledImage = styled("img")({
    maxWidth: "100%",
    maxHeight: "100%",
    objectFit: "contain",
})

export function FilesInput({
    existingUrls,
    newFiles,
    maxFilesCount = 10,
    onFilesChange,
    onUrlsChange,
}: IProps) {
    const { showError } = useNotifications()
    const [filesWithPreview, setFilesWithPreview] = useState<FileWithPreview[]>([])
    const [selectedImage, setSelectedImage] = useState<string | null>(null)

    useEffect(() => {
        const newPreviewFiles = newFiles.map((file) =>
            Object.assign(file, { preview: URL.createObjectURL(file) })
        )
        setFilesWithPreview(newPreviewFiles)

        return () => {
            newPreviewFiles.forEach((file) => URL.revokeObjectURL(file.preview))
        }
    }, [newFiles])

    const handleAddFiles = useCallback(
        async (addedFiles: File[]) => {
            try {
                const currentTotal = existingUrls.length + newFiles.length
                const availableSlots = maxFilesCount - currentTotal

                if (availableSlots <= 0) {
                    showError(`Максимальное количество файлов: ${maxFilesCount}`)
                    return
                }

                const existingHashes = new Set(
                    await Promise.all(
                        newFiles.map((f) => (f as FileWithPreview).hash || getFileHash(f))
                    )
                )

                const validFiles: FileWithPreview[] = []
                for (const file of addedFiles.slice(0, availableSlots)) {
                    if (!file.type.startsWith("image/")) {
                        showError(`Файл ${file.name} не является изображением`)
                        continue
                    }

                    if (file.size > MAX_FILE_SIZE_BYTES) {
                        showError(`Файл ${file.name} превышает лимит ${MAX_FILE_SIZE_MB}МБ`)
                        continue
                    }

                    const hash = await getFileHash(file)

                    if (existingHashes.has(hash)) {
                        showError(`Файл уже добавлен`)
                        continue
                    }

                    const fileWithPreview: FileWithPreview = Object.assign(file, {
                        preview: URL.createObjectURL(file),
                        hash,
                    })

                    validFiles.push(fileWithPreview)
                }

                onFilesChange([...newFiles, ...validFiles])
            } catch (error) {
                showError("Ошибка при обработке файлов")
            }
        },
        [existingUrls, newFiles, onFilesChange, showError]
    )

    const handleDelete = useCallback(
        (item: FileItem) => {
            if (item.type === "existing") {
                onUrlsChange(existingUrls.filter((u) => u !== item.value))
            } else {
                onFilesChange(newFiles.filter((f) => f !== item.value))
            }
        },
        [existingUrls, newFiles, onFilesChange, onUrlsChange]
    )

    const { getRootProps, getInputProps } = useDropzone({
        onDrop: handleAddFiles,
        accept: { "image/*": [] },
        multiple: true,
        noClick: true,
    })

    const openFileDialog = useCallback(() => {
        const input = document.createElement("input")
        input.type = "file"
        input.multiple = true
        input.accept = "image/*"

        input.onchange = (e: Event) => {
            const files = (e.target as HTMLInputElement).files
            if (files) handleAddFiles(Array.from(files))
            input.value = ""
        }

        input.click()
    }, [handleAddFiles])

    const allItems: FileItem[] = [
        ...existingUrls.map((url) => ({ type: "existing", value: url })),
        ...filesWithPreview.map((file) => ({ type: "new", value: file })),
    ]

    return (
        <Box {...getRootProps()}>
            <input {...getInputProps()} />

            <Typography variant="h6">
                {`Прикрепленные файлы `}
                <Typography component="span" variant="body2" color="text.secondary">
                    ({allItems.length}/{maxFilesCount})
                </Typography>
            </Typography>

            <Box
                sx={{
                    display: "flex",
                    gap: 2,
                    overflowX: "auto",
                    py: 1,
                    width: "100%",
                }}>
                {allItems.map((item, index) => (
                    <Box
                        key={index}
                        sx={{
                            position: "relative",
                            flexShrink: 0,
                            width: 200,
                            height: 200,
                            cursor: "pointer",
                            overflow: "hidden",
                            borderRadius: 2,
                        }}
                        onClick={() =>
                            setSelectedImage(
                                item.type === "existing" ? item.value : item.value.preview
                            )
                        }>
                        <img
                            src={item.type === "existing" ? item.value : item.value.preview}
                            alt={`Файл ${index + 1}`}
                            style={{
                                width: "100%",
                                height: "100%",
                                objectFit: "cover",
                            }}
                        />

                        <Box
                            sx={{
                                position: "absolute",
                                top: 0,
                                left: 0,
                                right: 0,
                                background:
                                    "linear-gradient(to bottom, rgba(0,0,0,0.7) 0%, rgba(0,0,0,0.3) 70%, rgba(0,0,0,0.1) 100%)",
                                p: 1,
                                display: "flex",
                                justifyContent: "flex-end",
                                gap: 1,
                            }}>
                            <IconButton
                                icon={IconType.Delete}
                                size="small"
                                onClick={(e) => {
                                    e.stopPropagation()
                                    handleDelete(item)
                                }}
                                sx={{ color: "white" }}
                            />
                        </Box>
                    </Box>
                ))}

                {allItems.length < maxFilesCount && (
                    <Box
                        onClick={openFileDialog}
                        sx={{
                            flexShrink: 0,
                            width: 200,
                            height: 200,
                            border: "2px dashed",
                            borderColor: "divider",
                            borderRadius: 2,
                            display: "flex",
                            alignItems: "center",
                            justifyContent: "center",
                            cursor: "pointer",
                            "&:hover": {
                                borderColor: "primary.main",
                                backgroundColor: "action.hover",
                            },
                        }}>
                        <IconButton icon={IconType.Add} size="large" />
                    </Box>
                )}
            </Box>

            <Dialog
                open={!!selectedImage}
                onClose={() => setSelectedImage(null)}
                PaperProps={{
                    sx: {
                        margin: "32px",
                        maxWidth: "calc(100vw - 64px)",
                        maxHeight: "calc(100vh - 64px)",
                        width: "auto",
                        height: "auto",
                        backgroundColor: "transparent",
                        boxShadow: "none",
                    },
                }}>
                <DialogContent
                    sx={{
                        p: 3,
                        display: "flex",
                        alignItems: "center",
                        justifyContent: "center",
                        position: "relative",
                    }}>
                    <PreviewContainer>
                        <StyledImage
                            src={selectedImage || ""}
                            alt="Просмотр"
                            draggable={false}
                            onDragStart={(e) => e.preventDefault()}
                            sx={{
                                maxWidth: "calc(100vw - 128px)",
                                maxHeight: "calc(100vh - 128px)",
                            }}
                        />
                        <IconButton
                            icon={IconType.Close}
                            size="small"
                            onClick={() => setSelectedImage(null)}
                            sx={{
                                position: "absolute",
                                top: 8,
                                right: 8,
                                color: "white",
                                backgroundColor: "rgba(0,0,0,0.5)",
                                "&:hover": {
                                    backgroundColor: "rgba(0,0,0,0.8)",
                                },
                            }}
                        />
                    </PreviewContainer>
                </DialogContent>
            </Dialog>
        </Box>
    )
}
