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
import { SideBarElementUtils } from "./enums/sideBarElements"

const Sidebar = () => {
    const router = useRouter()

    const elements = SideBarElementUtils.getAll()

    return (
        <Box sx={{ display: "flex", alignItems: "center" }}>
            <Box sx={{ width: "200px", height: "100vh" }}>
                <List>
                    {elements.map((element, index) => (
                        <ListItem key={index} disablePadding>
                            <ListItemButton
                                onClick={() =>
                                    router.push(SideBarElementUtils.getRouterPath(element))
                                }>
                                <ListItemIcon>{SideBarElementUtils.getIcon(element)}</ListItemIcon>
                                <ListItemText
                                    primary={SideBarElementUtils.getText(element)}
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
