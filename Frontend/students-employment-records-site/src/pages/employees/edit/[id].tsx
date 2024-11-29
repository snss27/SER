import { Box, Typography } from "@mui/material"
import { useParams } from "next/navigation"
import React, { useEffect, useState } from "react"
import { EmployeeBlank } from "@/domain/employees/models/employeeBlank"
import { EmployeesProvider } from "@/domain/employees/employeesProvider"
import { EditEmployeeForm } from "@/components/employees/editEmployeeForm"

const EditEmployeePage: React.FC = () => {
    const [employeeBlank, setEmployeeBlank] = useState<EmployeeBlank | null>(null)

    const { id } = useParams<{ id: string }>()

    useEffect(() => {
        async function loadEmployee() {
            const employee = await EmployeesProvider.get(id)

            setEmployeeBlank(employee.toBlank())
        }

        loadEmployee()
    }, [])

    if (employeeBlank === null) return null

    return (
        <Box className="container-fill">
            <Box className="edit-page-container">
                <Typography variant="h1" textAlign="center">
                    Редактирование сотрудника
                </Typography>
                <EditEmployeeForm initialBlank={employeeBlank} />
            </Box>
        </Box>
    )
}

export default EditEmployeePage
