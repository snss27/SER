import { EditEmployeeForm } from "@/components/employees/editEmployeeForm"
import { EmployeeBlank } from "@/domain/employees/models/employeeBlank"
import { Box, Typography } from "@mui/material"
import React from "react"

const AddEmployeesPage: React.FC = () => {
    return (
        <Box className="container" sx={{ px: 4, pt: 4, g: 2 }}>
            <Typography variant="h1" textAlign="center" gutterBottom>
                Добавление сотрудника
            </Typography>
            <EditEmployeeForm initialBlank={EmployeeBlank.empty()} />
        </Box>
    )
}

export default AddEmployeesPage
