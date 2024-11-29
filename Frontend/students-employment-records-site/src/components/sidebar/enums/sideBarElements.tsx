import PageUrls from "@/constants/pageUrls"
import { enumToArrayNumber } from "@/tools/enums/enumUtils"
import { NeverUnreachable } from "@/tools/neverUreachable"
import ApartmentIcon from "@mui/icons-material/Apartment"
import EmojiPeopleIcon from "@mui/icons-material/EmojiPeople"
import EngineeringIcon from "@mui/icons-material/Engineering"
import GroupsIcon from "@mui/icons-material/Groups"
import SchoolIcon from "@mui/icons-material/School"
import WorkIcon from "@mui/icons-material/Work"

export enum SideBarElements {
    Students = 1,
    Groups = 2,
    Specialities = 3,
    Employees = 4,
    AdditionalQualifications = 5,
    Enterprises = 6,
}

export namespace SideBarElements {
    export function getAll() {
        return enumToArrayNumber<SideBarElements>(SideBarElements)
    }

    export function getDefault() {
        return SideBarElements.Students
    }

    export function getIcon(element: SideBarElements): JSX.Element {
        switch (element) {
            case SideBarElements.Students:
                return <SchoolIcon />
            case SideBarElements.Groups:
                return <GroupsIcon />
            case SideBarElements.Specialities:
                return <EngineeringIcon />
            case SideBarElements.Employees:
                return <EmojiPeopleIcon />
            case SideBarElements.AdditionalQualifications:
                return <WorkIcon />
            case SideBarElements.Enterprises:
                return <ApartmentIcon />
            default:
                throw new NeverUnreachable(element)
        }
    }

    export function getText(element: SideBarElements): string {
        switch (element) {
            case SideBarElements.Students:
                return "Студенты"
            case SideBarElements.Groups:
                return "Группы"
            case SideBarElements.Specialities:
                return "Специальности"
            case SideBarElements.Employees:
                return "Сотрудники"
            case SideBarElements.AdditionalQualifications:
                return "Дополнительные квалификации"
            case SideBarElements.Enterprises:
                return "Организации"
            default:
                throw new NeverUnreachable(element)
        }
    }

    export function getUrl(element: SideBarElements) {
        switch (element) {
            case SideBarElements.Students:
                return PageUrls.Students
            case SideBarElements.Groups:
                return PageUrls.Groups
            case SideBarElements.Specialities:
                return PageUrls.Specialities
            case SideBarElements.Employees:
                return PageUrls.Employees
            case SideBarElements.AdditionalQualifications:
                return PageUrls.AdditionalQualifications
            case SideBarElements.Enterprises:
                return PageUrls.Enterprises
            default:
                throw new NeverUnreachable(element)
        }
    }
}
