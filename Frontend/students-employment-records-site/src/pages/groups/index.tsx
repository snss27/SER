import { IconPosition, IconType } from "@/components/shared/buttons"
import Button from "@/components/shared/buttons/button"
import PageUrls from "@/constants/pageUrls"
import { Box, Typography } from "@mui/material"
import { useRouter } from "next/navigation"
import React from "react"
import { GroupsTable } from "@/components/groups/groupsTable"

const GroupsPage: React.FC = () => {
    const navigator = useRouter()

    return (
        <Box className="container-fill">
            <Box className="inner-container">
                <Box className="header-container">
                    <Typography variant="h1" sx={{ flex: 1 }} textAlign="center">
                        Группы
                    </Typography>
                    <Button
                        text="Добавить группу"
                        onClick={() => navigator.push(PageUrls.AddGroup)}
                        icon={{ type: IconType.Add, position: IconPosition.Start }}
                    />
                </Box>
                <GroupsTable />
            </Box>
        </Box>
    )
}

export default GroupsPage
