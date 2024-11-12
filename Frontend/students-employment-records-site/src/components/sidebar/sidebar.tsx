import {
    Box,
    Divider,
    List,
    ListItem,
    ListItemButton,
    ListItemIcon,
    ListItemText,
} from "@mui/material"
import { useRouter } from "next/router"
import { SideBarElements } from "./enums/sideBarElements"

const Sidebar = () => {
    const router = useRouter()

    const elements = SideBarElements.getAll()

    return (
        <Box
            component="aside"
            sx={{
                position: "fixed",
                left: 0,
                top: 0,
                width: "220px",
            }}>
            <Box sx={{ width: "200px", height: "100vh" }}>
                <List>
                    {elements.map((element, index) => (
                        <ListItem key={index} disablePadding>
                            <ListItemButton
                                onClick={() => router.push(SideBarElements.getUrl(element))}>
                                <ListItemIcon>{SideBarElements.getIcon(element)}</ListItemIcon>
                                <ListItemText
                                    primary={SideBarElements.getText(element)}
                                    sx={{ fontSize: "24px" }}
                                />
                            </ListItemButton>
                        </ListItem>
                    ))}
                </List>
            </Box>
            <Divider
                component="hr"
                orientation="vertical"
                sx={{
                    position: "absolute",
                    right: 0,
                    top: 8,
                    height: "calc(100% - 16px)",
                }}
            />
        </Box>
    )
}

export default Sidebar
