import { Box, List, ListItem, ListItemButton, ListItemIcon, ListItemText } from "@mui/material"
import { useRouter } from "next/router"
import { SideBarElements } from "./enums/sideBarElements"

const Sidebar = () => {
    const router = useRouter()
    const elements = SideBarElements.getAll()

    return (
        <Box
            component="nav"
            sx={{
                width: 220,
                flexShrink: 0,
                position: "fixed",
                left: 0,
                top: 0,
                bottom: 0,
                overflowY: "auto",
                bgcolor: "background.paper",
                borderRight: "1px solid",
                borderColor: "divider",
                "&::-webkit-scrollbar": { width: "6px" },
                "&::-webkit-scrollbar-thumb": { backgroundColor: "divider" },
            }}>
            <List disablePadding>
                {elements.map((element, index) => (
                    <ListItem key={index} disablePadding>
                        <ListItemButton
                            onClick={() => router.push(SideBarElements.getUrl(element))}
                            sx={{
                                "&:hover": { backgroundColor: "action.hover" },
                                py: 1.5,
                            }}>
                            <ListItemIcon sx={{ minWidth: 40 }}>
                                {SideBarElements.getIcon(element)}
                            </ListItemIcon>
                            <ListItemText
                                primary={SideBarElements.getText(element)}
                                primaryTypographyProps={{
                                    fontSize: 15,
                                    fontWeight: "medium",
                                }}
                            />
                        </ListItemButton>
                    </ListItem>
                ))}
            </List>
        </Box>
    )
}

export default Sidebar
