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
        <Box sx={{ display: "flex", alignItems: "center" }}>
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
                orientation="vertical"
                sx={{ height: "calc(100% - 16px)", marginLeft: "10px" }}
            />
        </Box>
    )
}

export default Sidebar
