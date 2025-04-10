import { WorkplaceBlank } from "@/domain/workplaces/models/workplaceBlank"
import DeleteIcon from "@mui/icons-material/Delete"
import EditIcon from "@mui/icons-material/Edit"
import { Box, Card, CardContent, IconButton, Stack, Typography } from "@mui/material"

interface Props {
    workplace: WorkplaceBlank
    onEdit: () => void
    onDelete: () => void
}

export function WorkplaceItem({ workplace, onEdit, onDelete }: Props) {
    return (
        <Card sx={{ mb: 1 }}>
            <CardContent>
                <Stack direction="row" justifyContent="space-between" alignItems="flex-start">
                    <Box>
                        <Typography variant="subtitle1" fontWeight="bold">
                            {workplace.post || "Должность не указана"}
                        </Typography>
                        <Typography variant="body2" color="text.secondary">
                            {workplace.enterprise?.name || "Предприятие не указано"}
                        </Typography>
                    </Box>
                    <Stack direction="row" spacing={1}>
                        <IconButton onClick={onEdit} size="small">
                            <EditIcon />
                        </IconButton>
                        <IconButton onClick={onDelete} size="small" color="error">
                            <DeleteIcon />
                        </IconButton>
                    </Stack>
                </Stack>
            </CardContent>
        </Card>
    )
}
