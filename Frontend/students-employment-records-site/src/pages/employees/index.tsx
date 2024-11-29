import { IconPosition, IconType } from "@/components/shared/buttons"
import Button from "@/components/shared/buttons/button"
import PageUrls from "@/constants/pageUrls"
import { Box, Typography } from "@mui/material"
import { useRouter } from "next/router"
import React from "react"
import { EmployeesTable } from "@/components/employees/employeesTable"

const EmployeesPage: React.FC = () => {
    const navigator = useRouter()

    return (
        <Box className="container-fill">
            <Box className="inner-container">
                <Box className="header-container">
                    <Typography variant="h1" sx={{ flex: 1 }} textAlign="center">
                        Сотрудники
                    </Typography>
                    <Button
                        text="Добавить сотрудника"
                        onClick={() => navigator.push(PageUrls.AddEmployee)}
                        icon={{ type: IconType.Add, position: IconPosition.Start }}
                    />
                </Box>
                <EmployeesTable />
            </Box>
        </Box>
    )
}

export default EmployeesPage
