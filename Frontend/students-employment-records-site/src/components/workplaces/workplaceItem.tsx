import { WorkplaceBlank } from "@/domain/workplaces/models/workplaceBlank"
import { Box, Typography } from "@mui/material"
import Button from "../shared/buttons/button"

interface Props {
    workplace: WorkplaceBlank
    onEdit: () => void
    onDelete: () => void
}

export const WorkplaceItem: React.FC<Props> = ({ workplace, onEdit, onDelete }) => (
    <Box sx={{ p: 2, border: 1, borderColor: "divider", borderRadius: 1, mb: 1 }}>
        <Typography variant="subtitle1">{workplace.post}</Typography>
        <Typography variant="body2" color="text.secondary">
            {workplace.enterpriseId ? "Имя" : "Предприятие не указано"}
        </Typography>
        <Typography variant="caption">
            {workplace.id ? "Сохранено на сервере" : "Не сохранено"}
        </Typography>
        <Box sx={{ display: "flex", gap: 1, mt: 1 }}>
            <Button text="Редактировать" onClick={onEdit} />
            <Button text="Удалить" onClick={onDelete} />
        </Box>
    </Box>
)
