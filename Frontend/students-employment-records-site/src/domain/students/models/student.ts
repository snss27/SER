import { AdditionalQualification } from "@/domain/additionalQualifications/models/additionalQualification"
import { Enterprise } from "@/domain/enterprises/models/enterprise"
import { Group } from "@/domain/groups/models/group"
import { Workplace } from "@/domain/workplaces/models/workplace"
import { WorkplaceBlank } from "@/domain/workplaces/models/workplaceBlank"
import { BlankFiles } from "@/tools/blankFiles"
import { Gender } from "../enums/gender"
import { SocialStatus } from "../enums/socialStatus"
import { StudentStatus } from "../enums/studentStatus"
import { StudentBlank } from "./studentBlank"

export class Student {
    constructor(
        public readonly id: string,
        public readonly name: string,
        public readonly secondName: string,
        public readonly lastName: string | null,
        public readonly gender: Gender,
        public readonly birthDate: Date | null,
        public readonly phoneNumber: string | null,
        public readonly representativePhoneNumber: string | null,
        public readonly isOnPaidStudy: boolean,
        public readonly snils: string | null,
        public readonly group: Group,
        public readonly passportNumber: string | null,
        public readonly passportSeries: string | null,
        public readonly passportIssuedBy: string | null,
        public readonly passportIssuedDate: Date | null,
        public readonly passportFiles: string[],
        public readonly prevWorkplaces: Workplace[],
        public readonly currentWorkplace: Workplace | null,
        public readonly additionalQualifications: AdditionalQualification[],
        public readonly isTargetAgreement: boolean,
        public readonly targetAgreementFile: string | null,
        public readonly targetAgreementDate: Date | null,
        public readonly targetAgreementEnterprise: Enterprise | null,
        public readonly mustServeInArmy: boolean,
        public readonly armySubpoenaFile: string | null,
        public readonly armyCallDate: Date | null,
        public readonly socialStatuses: SocialStatus[],
        public readonly status: StudentStatus,
        public readonly address: string | null,
        public readonly isForeignCitizen: boolean,
        public readonly inn: string | null,
        public readonly mail: string | null,
        public readonly otherFiles: string[]
    ) {}

    public get displayName(): string {
        return `${this.secondName} ${this.name} ${this.lastName ?? ""}`
    }

    public toBlank(): StudentBlank {
        const currentWorkplace = this.currentWorkplace
            ? WorkplaceBlank.create(this.currentWorkplace)
            : null
        const prevWorkplaces = this.prevWorkplaces.map(WorkplaceBlank.create)
        return {
            id: this.id,
            name: this.name,
            secondName: this.secondName,
            lastName: this.lastName,
            gender: this.gender,
            birthDate: this.birthDate,
            phoneNumber: this.phoneNumber,
            representativePhoneNumber: this.representativePhoneNumber,
            isOnPaidStudy: this.isOnPaidStudy,
            snils: this.snils,
            group: this.group,
            passportNumber: this.passportNumber,
            passportSeries: this.passportSeries,
            passportIssuedBy: this.passportIssuedBy,
            passportIssuedDate: this.passportIssuedDate,
            passportFiles: BlankFiles.fromUrls(this.passportFiles, 5),
            prevWorkplaces,
            currentWorkplace,
            additionalQualifications: this.additionalQualifications,
            isTargetAgreement: this.isTargetAgreement,
            targetAgreementFile: this.targetAgreementFile
                ? BlankFiles.fromUrl(this.targetAgreementFile)
                : BlankFiles.create(1),
            targetAgreementDate: this.targetAgreementDate,
            targetAgreementEnterprise: this.targetAgreementEnterprise,
            mustServeInArmy: this.mustServeInArmy,
            armySubpoenaFile: this.armySubpoenaFile
                ? BlankFiles.fromUrl(this.armySubpoenaFile)
                : BlankFiles.create(1),
            armyCallDate: this.armyCallDate,
            socialStatuses: this.socialStatuses,
            status: this.status,
            address: this.address,
            isForeignCitizen: this.isForeignCitizen,
            inn: this.inn,
            mail: this.mail,
            otherFiles: BlankFiles.fromUrls(this.otherFiles, 10),
        }
    }

    public static fromAny(any: any): Student {
        const birthDate = any.birthDate ? new Date(any.birthDate) : null
        const group = Group.fromAny(any.group)
        const passportIssuedDate = any.passportIssuedDate ? new Date(any.passportIssuedDate) : null
        const prevWorkplaces = (any.prevWorkplaces as any[]).map(Workplace.fromAny)
        const currentWorkplace = any.currentWorkplace
            ? Workplace.fromAny(any.currentWorkplace)
            : null
        const additionalQualifications = (any.additionalQualifications as any[]).map(
            AdditionalQualification.fromAny
        )
        const targetAgreementDate = any.targetAgreementDate
            ? new Date(any.targetAgreementDate)
            : null
        const targetAgreementEnterprise = any.targetAgreementEnterprise
            ? Enterprise.fromAny(any.targetAgreementEnterprise)
            : null
        const armyCallDate = any.armyCallDate ? new Date(any.armyCallDate) : null

        return new Student(
            any.id,
            any.name,
            any.secondName,
            any.lastName,
            any.gender,
            birthDate,
            any.phoneNumber,
            any.representativePhoneNumber,
            any.isOnPaidStudy,
            any.snils,
            group,
            any.passportNumber,
            any.passportSeries,
            any.passportIssuedBy,
            passportIssuedDate,
            any.passportFiles,
            prevWorkplaces,
            currentWorkplace,
            additionalQualifications,
            any.isTargetAgreement,
            any.targetAgreementFile,
            targetAgreementDate,
            targetAgreementEnterprise,
            any.mustServeInArmy,
            any.armySubpoenaFile,
            armyCallDate,
            any.socialStatuses,
            any.status,
            any.address,
            any.isForeignCitizen,
            any.inn,
            any.mail,
            any.otherFiles
        )
    }
}
