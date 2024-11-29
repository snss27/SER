import { Box, Typography } from "@mui/material"
import React from "react"
import { EditEmployeeForm } from "@/components/employees/editEmployeeForm"
import { EmployeeBlank } from "@/domain/employees/models/employeeBlank"

const AddEmployeesPage: React.FC = () => {
    return (
        <Box className="container-fill">
            <Box className="edit-page-container">
                <Typography variant="h1" textAlign="center">
                    Добавление сотрудника
                </Typography>
                <EditEmployeeForm initialBlank={EmployeeBlank.empty()} />
            </Box>
        </Box>
    )
}

export default AddEmployeesPage
