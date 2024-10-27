import { enumToArrayNumber } from "@/tools/enums/enumUtils"
import { NeverUnreachable } from "@/tools/neverUreachable"
import HomeIcon from "@mui/icons-material/Home"

export enum SideBarElements {
    Main = 1,
}

export class SideBarElementUtils {
    static getAll() {
        return enumToArrayNumber<SideBarElements>(SideBarElements)
    }

    static getDefault() {
        return SideBarElements.Main
    }

    static getIcon(element: SideBarElements): JSX.Element {
        switch (element) {
            case SideBarElements.Main:
                return <HomeIcon />
            default:
                throw new NeverUnreachable(element)
        }
    }

    static getText(element: SideBarElements): string {
        switch (element) {
            case SideBarElements.Main:
                return "Главная"
            default:
                throw new NeverUnreachable(element)
        }
    }

    static getRouterPath(element: SideBarElements) {
        switch (element) {
            case SideBarElements.Main:
                return "/"
            default:
                throw new NeverUnreachable(element)
        }
    }
}
