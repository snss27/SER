import { EditEmployeeForm } from "@/components/employees/editEmployeeForm"
import PageUrls from "@/constants/pageUrls"
import { EmployeesProvider } from "@/domain/employees/employeesProvider"
import { EmployeeBlank } from "@/domain/employees/models/employeeBlank"
import useNotifications from "@/hooks/useNotifications"
import { Box, Typography } from "@mui/material"
import { useParams } from "next/navigation"
import { useRouter } from "next/router"
import React, { useEffect, useState } from "react"

const EditEmployeePage: React.FC = () => {
    const [employeeBlank, setEmployeeBlank] = useState<EmployeeBlank | null>(null)
    const navigator = useRouter()
    const { showError } = useNotifications()

    const { id } = useParams<{ id: string }>()

    useEffect(() => {
        async function loadEmployee() {
            const employee = await EmployeesProvider.get(id)
            if (employee === null) {
                showError("Сотрудник не найден")
                navigator.push(PageUrls.Employees)
                return
            }

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
