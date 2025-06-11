import PageUrls from "@/constants/pageUrls"
import { enumToArrayNumber } from "@/tools/enums/enumUtils"
import { NeverUnreachable } from "@/tools/neverUreachable"
import ApartmentIcon from "@mui/icons-material/Apartment"
import AssessmentIcon from "@mui/icons-material/Assessment"
import EmojiPeopleIcon from "@mui/icons-material/EmojiPeople"
import EngineeringIcon from "@mui/icons-material/Engineering"
import GroupsIcon from "@mui/icons-material/Groups"
import SchemaIcon from "@mui/icons-material/Schema"
import SchoolIcon from "@mui/icons-material/School"
import WorkIcon from "@mui/icons-material/Work"
import { JSX } from "react"

export enum SideBarElements {
    Students = 1,
    Groups = 2,
    EducationLevels = 3,
    Employees = 4,
    AdditionalQualifications = 5,
    Enterprises = 6,
    Clusters = 7,
    Reports = 8,
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
            case SideBarElements.EducationLevels:
                return <EngineeringIcon />
            case SideBarElements.Employees:
                return <EmojiPeopleIcon />
            case SideBarElements.AdditionalQualifications:
                return <WorkIcon />
            case SideBarElements.Enterprises:
                return <ApartmentIcon />
            case SideBarElements.Clusters:
                return <SchemaIcon />
            case SideBarElements.Reports:
                return <AssessmentIcon />
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
            case SideBarElements.EducationLevels:
                return "Уровни образования"
            case SideBarElements.Employees:
                return "Сотрудники"
            case SideBarElements.AdditionalQualifications:
                return "Дополнительные квалификации"
            case SideBarElements.Enterprises:
                return "Организации"
            case SideBarElements.Clusters:
                return "Кластеры"
            case SideBarElements.Reports:
                return "Генерация отчета"
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
            case SideBarElements.EducationLevels:
                return PageUrls.EducationLevels
            case SideBarElements.Employees:
                return PageUrls.Employees
            case SideBarElements.AdditionalQualifications:
                return PageUrls.AdditionalQualifications
            case SideBarElements.Enterprises:
                return PageUrls.Enterprises
            case SideBarElements.Clusters:
                return PageUrls.Clusters
            case SideBarElements.Reports:
                return PageUrls.Reports
            default:
                throw new NeverUnreachable(element)
        }
    }
}
