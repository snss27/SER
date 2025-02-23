import useNotifications from "@/hooks/useNotifications"
import DescriptionIcon from "@mui/icons-material/Description"
import InsertDriveFileIcon from "@mui/icons-material/InsertDriveFile"
import PictureAsPdfIcon from "@mui/icons-material/PictureAsPdf"
import { Box, Dialog, DialogContent, Typography, styled } from "@mui/material"
import { useCallback, useEffect, useState } from "react"
import { useDropzone } from "react-dropzone"
import { IconType } from "../buttons"
import IconButton from "../buttons/iconButtons"

interface FileWithPreview extends File {
    preview: string
    hash?: string
    fileType: "image" | "pdf" | "doc" | "xls" | "ppt" | "other"
}

const getFileHash = async (file: File): Promise<string> => {
    const arrayBuffer = await file.arrayBuffer()
    const hashBuffer = await crypto.subtle.digest("SHA-256", arrayBuffer)
    return Array.from(new Uint8Array(hashBuffer))
        .map((b) => b.toString(16).padStart(2, "0"))
        .join("")
}

interface IProps {
    urls: string[]
    files: File[]
    maxFilesCount?: number
    label?: string
    onFilesChange: (newFiles: File[]) => void
    onUrlsChange: (newUrls: string[]) => void
}

type FileItem = { type: "existing"; value: string } | { type: "new"; value: FileWithPreview }
const MAX_FILE_SIZE_MB = 20
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

const FileTypeIconWrapper = styled(Box)({
    width: "100%",
    height: "100%",
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
    backgroundColor: "#f5f5f5",
})

const getFileType = (file: File): FileWithPreview["fileType"] => {
    if (file.type.startsWith("image/")) return "image"
    if (file.type === "application/pdf") return "pdf"
    if (file.type.includes("word")) return "doc"
    if (file.type.includes("excel") || file.type.includes("sheet")) return "xls"
    if (file.type.includes("powerpoint") || file.type.includes("presentation")) return "ppt"
    return "other"
}

const FileIcon = ({
    type,
    fontSize,
}: {
    type: FileWithPreview["fileType"]
    fontSize?: "small" | "large"
}) => {
    const style = { fontSize: fontSize === "large" ? 48 : 24 }

    switch (type) {
        case "pdf":
            return <PictureAsPdfIcon sx={style} />
        case "doc":
            return <DescriptionIcon sx={style} />
        case "xls":
            return <DescriptionIcon sx={style} />
        case "ppt":
            return <DescriptionIcon sx={style} />
        default:
            return <InsertDriveFileIcon sx={style} />
    }
}

export function FilesInput({
    urls: existingUrls,
    files: newFiles,
    maxFilesCount = 10,
    label = "Прикрепленные файлы",
    onFilesChange,
    onUrlsChange,
}: IProps) {
    const { showError } = useNotifications()
    const [filesWithPreview, setFilesWithPreview] = useState<FileWithPreview[]>([])
    const [selectedFile, setSelectedFile] = useState<{
        url: string
        type: FileWithPreview["fileType"]
    } | null>(null)

    useEffect(() => {
        const newPreviewFiles = newFiles.map((file) => {
            const fileType = getFileType(file)
            return Object.assign(file, {
                preview: fileType === "image" ? URL.createObjectURL(file) : "",
                fileType,
            })
        })
        setFilesWithPreview(newPreviewFiles)

        return () => {
            newPreviewFiles.forEach((file) => {
                if (file.fileType === "image") URL.revokeObjectURL(file.preview)
            })
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
                    if (file.size > MAX_FILE_SIZE_BYTES) {
                        showError(`Файл ${file.name} превышает лимит ${MAX_FILE_SIZE_MB}МБ`)
                        continue
                    }

                    const hash = await getFileHash(file)
                    if (existingHashes.has(hash)) {
                        showError(`Файл уже добавлен`)
                        continue
                    }

                    const fileType = getFileType(file)
                    const fileWithPreview: FileWithPreview = Object.assign(file, {
                        preview: fileType === "image" ? URL.createObjectURL(file) : "",
                        hash,
                        fileType,
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
        accept: {
            "image/*": [".jpeg", ".jpg", ".png", ".gif"],
            "application/pdf": [".pdf"],
            "application/msword": [".doc"],
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document": [".docx"],
            "application/vnd.ms-excel": [".xls"],
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet": [".xlsx"],
            "application/vnd.ms-powerpoint": [".ppt"],
            "application/vnd.openxmlformats-officedocument.presentationml.presentation": [".pptx"],
        },
        multiple: true,
        noClick: true,
    })

    const openFileDialog = useCallback(() => {
        const input = document.createElement("input")
        input.type = "file"
        input.multiple = true
        input.accept = "image/*, .pdf, .doc, .docx, .xls, .xlsx, .ppt, .pptx"

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

    const handleFileClick = (item: FileItem) => {
        if (item.type === "existing") {
            setSelectedFile({ url: item.value, type: "image" }) // Предполагаем, что существующие URL - изображения
        } else {
            const { fileType } = item.value
            if (fileType === "image") {
                setSelectedFile({ url: item.value.preview, type: "image" })
            } else {
                window.open(item.value.preview, "_blank") // Для скачивания файла
            }
        }
    }

    return (
        <Box {...getRootProps()}>
            <input {...getInputProps()} />

            <Typography variant="h6">
                {`${label} `}
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
                {allItems.map((item, index) => {
                    const isImage = item.type === "existing" || item.value.fileType === "image"
                    const url = item.type === "existing" ? item.value : item.value.preview
                    const fileType = item.type === "existing" ? "image" : item.value.fileType

                    return (
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
                            onClick={() => handleFileClick(item)}>
                            {isImage ? (
                                <img
                                    src={url}
                                    alt={`Файл ${index + 1}`}
                                    style={{
                                        width: "100%",
                                        height: "100%",
                                        objectFit: "cover",
                                    }}
                                />
                            ) : (
                                <FileTypeIconWrapper>
                                    <FileIcon type={fileType} fontSize="large" />
                                </FileTypeIconWrapper>
                            )}

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
                    )
                })}

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
                open={!!selectedFile?.url && selectedFile.type === "image"}
                onClose={() => setSelectedFile(null)}
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
                            src={selectedFile?.url || ""}
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
                            onClick={() => setSelectedFile(null)}
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
