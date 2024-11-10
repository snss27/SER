import PageUrls from "@/constants/pageUrls"
import { enumToArrayNumber } from "@/tools/enums/enumUtils"
import { NeverUnreachable } from "@/tools/neverUreachable"
import EngineeringIcon from "@mui/icons-material/Engineering"
import GroupsIcon from "@mui/icons-material/Groups"
import HomeIcon from "@mui/icons-material/Home"

export enum SideBarElements {
    Main = 1,
    Groups = 2,
    Specialities = 3,
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
            case SideBarElements.Specialities:
                return <EngineeringIcon />
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
            case SideBarElements.Specialities:
                return "Специальности"
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
            case SideBarElements.Specialities:
                return PageUrls.Specialities
            default:
                throw new NeverUnreachable(element)
        }
    }
}
