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
        public readonly status: StudentStatus,
        public readonly gender: Gender,
        public readonly phoneNumber: string | null,
        public readonly representativePhoneNumber: string | null,
        public readonly birthDate: Date | null,
        public readonly snils: string | null,
        public readonly socialStatuses: SocialStatus[],
        public readonly address: string | null,
        public readonly mail: string | null,
        public readonly inn: string | null,
        public readonly groupId: string | null,
        public readonly isForeignCitizen: boolean,
        public readonly isOnPaidStudy: boolean,

        public readonly passportSeries: string | null,
        public readonly passportNumber: string | null,
        public readonly passportIssuedBy: string | null,
        public readonly passportIssuedDate: Date | null,
        public readonly passportFiles: string[],

        public readonly currentWorkplaceId: string | null,
        public readonly prevWorkplaceIds: string[],

        public readonly additionalQualificationIds: string[],

        public readonly isTargetAgreement: boolean,
        public readonly targetAgreementDate: Date | null,
        public readonly targetAgreementEnterpriseId: string | null,
        public readonly targetAgreementFile: string | null,

        public readonly mustServeInArmy: boolean,
        public readonly armySubpoenaFile: string | null,
        public readonly armyCallDate: Date | null,

        public readonly otherFiles: string[]
    ) {}

    public get displayName(): string {
        return `${this.name}  ${this.secondName}`
    }

    public toBlank(): StudentBlank {
        const birthDate = !!this.birthDate ? new Date(this.birthDate.getTime()) : null
        const passportIssuedDate = !!this.passportIssuedDate
            ? new Date(this.passportIssuedDate.getTime())
            : null
        const passportFiles = new BlankFiles(this.passportFiles, [], 5)
        const currentWorkplace = WorkplaceBlank.create(this.currentWorkplaceId)
        const prevWorkplaces = this.prevWorkplaceIds.map((id) => WorkplaceBlank.create(id))
        const targetAgreementDate = !!this.targetAgreementDate
            ? new Date(this.targetAgreementDate.getTime())
            : null
        const targetAgreementFile = new BlankFiles(
            !!this.targetAgreementFile ? [this.targetAgreementFile] : [],
            [],
            1
        )
        const armySubpoenaFile = new BlankFiles(
            !!this.armySubpoenaFile ? [this.armySubpoenaFile] : [],
            [],
            1
        )
        const otherFiles = new BlankFiles(this.otherFiles, [], 10)

        return {
            id: this.id,
            name: this.name,
            secondName: this.secondName,
            lastName: this.lastName,
            status: this.status,
            gender: this.gender,
            phoneNumber: this.phoneNumber,
            representativePhoneNumber: this.representativePhoneNumber,
            birthDate,
            snils: this.snils,
            socialStatuses: this.socialStatuses,
            address: this.address,
            mail: this.mail,
            inn: this.inn,
            groupId: this.groupId,
            isForeignCitizen: this.isForeignCitizen,
            isOnPaidStudy: this.isOnPaidStudy,

            passportSeries: this.passportSeries,
            passportNumber: this.passportNumber,
            passportIssuedBy: this.passportIssuedBy,
            passportIssuedDate,
            passportFiles,

            currentWorkplace,
            prevWorkplaces,

            additionalQualificationIds: this.additionalQualificationIds,

            isTargetAgreement: this.isTargetAgreement,
            targetAgreementDate,
            targetAgreementEnterpriseId: this.targetAgreementEnterpriseId,
            targetAgreementFile,

            mustServeInArmy: this.mustServeInArmy,
            armySubpoenaFile,
            armyCallDate: this.armyCallDate,

            otherFiles,
        }
    }

    public static fromAny(any: any): Student {
        const birthDate = !!any.birthDate ? new Date(any.birthDate) : null
        const passportIssuedDate = !!any.passportIssuedDate
            ? new Date(any.passportIssuedDate)
            : null
        const targetAgreementDate = !!any.targetAgreementDate
            ? new Date(any.targeAgreementDate)
            : null
        const armyCallDate = !!any.armyCallDate ? new Date(any.armyCallDate) : null

        return new Student(
            any.id,
            any.name,
            any.secondName,
            any.lastName,
            any.status,
            any.gender,
            any.phoneNumber,
            any.representativePhoneNumber,
            birthDate,
            any.snils,
            any.socialStatuses,
            any.address,
            any.mail,
            any.inn,
            any.groupId,
            any.isForeignCitizen,
            any.isOnPaidStudy,
            any.passportSeries,
            any.passportNumber,
            any.passportIssuedBy,
            passportIssuedDate,
            any.passportFiles,
            any.currentWorkplaceId,
            any.prevWorkplaceIds,
            any.additionalQualificationIds,
            any.isTargetAgreement,
            targetAgreementDate,
            any.targetAgreementEnterpriseId,
            any.targetAgreementFile,
            any.mustServeInArmy,
            any.armySubpoenaFile,
            armyCallDate,
            any.otherFiles
        )
    }
}
