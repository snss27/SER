import PageUrls from "@/constants/pages"
import { enumToArrayNumber } from "@/tools/enums/enumUtils"
import { NeverUnreachable } from "@/tools/neverUreachable"
import GroupsIcon from "@mui/icons-material/Groups"
import HomeIcon from "@mui/icons-material/Home"

export enum SideBarElements {
    Main = 1,
    Groups = 2,
}

export namespace SideBarElements {
    export function getAll() {
        return enumToArrayNumber<SideBarElements>(SideBarElements)
    }

    export function getDefault() {
        return SideBarElements.Main
    }

    export function getIcon(element: SideBarElements): JSX.Element {
        switch (element) {
            case SideBarElements.Main:
                return <HomeIcon />
            case SideBarElements.Groups:
                return <GroupsIcon />
            default:
                throw new NeverUnreachable(element)
        }
    }

    export function getText(element: SideBarElements): string {
        switch (element) {
            case SideBarElements.Main:
                return "Главная"
            case SideBarElements.Groups:
                return "Группы"
            default:
                throw new NeverUnreachable(element)
        }
    }

    export function getUrl(element: SideBarElements) {
        switch (element) {
            case SideBarElements.Main:
                return PageUrls.Main
            case SideBarElements.Groups:
                return PageUrls.Groups
            default:
                throw new NeverUnreachable(element)
        }
    }
}
