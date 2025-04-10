import { AdditionalQualification } from "@/domain/additionalQualifications/models/additionalQualification"
import { Enterprise } from "@/domain/enterprises/models/enterprise"
import { Group } from "@/domain/groups/models/group"
import { Workplace } from "@/domain/workplaces/models/workplace"
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
        public readonly passportIssued: string | null,
        public readonly passportIssuedDate: Date | null,
        public readonly passportFiles: string[],
        public readonly prevWorkplaces: Workplace[],
        public readonly currentWorkplace: Workplace | null,
        public readonly additionalQualifications: AdditionalQualification[],
        public readonly isTargetAgreement: boolean,
        public readonly targetAgreementFile: string | null,
        public readonly targetAgreementDate: Date | null,
        public readonly targetAgreementEnterprise: Enterprise,
        public readonly mustServeInArmy: boolean,
        public readonly armySubpoenaFile: string | null,
        public readonly armyCallDate: Date | null,
        public readonly socialStatuses: SocialStatus[],
        public readonly status: StudentStatus,
        public readonly address: string | null,
        public readonly isForeignCitizen: boolean,
        public readonly inn: string | null,
        public readonly mail: string | null,
        public readonly otherFiles: string[],
        public readonly createdDateTimeUtc: Date,
        public readonly modifiedDateTimeUtc: Date | null
    ) {}

    public get displayName(): string {
        return `${this.name} ${this.secondName}`
    }

    public toBlank(): StudentBlank {
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
            passportIssuedBy: this.passportIssued,
            passportIssuedDate: this.passportIssuedDate,
            passportFiles: new BlankFiles(this.passportFiles, [], 5),
            prevWorkplaces: this.prevWorkplaces,
            currentWorkplace: this.currentWorkplace,
            additionalQualifications: this.additionalQualifications,
            isTargetAgreement: this.isTargetAgreement,
            targetAgreementFile: new BlankFiles(
                this.targetAgreementFile ? [this.targetAgreementFile] : [],
                [],
                1
            ),
            targetAgreementDate: this.targetAgreementDate,
            targetAgreementEnterprise: this.targetAgreementEnterprise,
            mustServeInArmy: this.mustServeInArmy,
            armySubpoenaFile: new BlankFiles(
                this.armySubpoenaFile ? [this.armySubpoenaFile] : [],
                [],
                1
            ),
            armyCallDate: this.armyCallDate,
            socialStatuses: this.socialStatuses,
            status: this.status,
            address: this.address,
            isForeignCitizen: this.isForeignCitizen,
            inn: this.inn,
            mail: this.mail,
            otherFiles: new BlankFiles(this.otherFiles, [], 10),
        }
    }

    public static fromAny(any: any): Student {
        return new Student(
            any.id,
            any.name,
            any.secondName,
            any.lastName,
            any.gender,
            any.birthDate ? new Date(any.birthDate) : null,
            any.phoneNumber,
            any.representativePhoneNumber,
            any.isOnPaidStudy,
            any.snils,
            Group.fromAny(any.group),
            any.passportNumber,
            any.passportSeries,
            any.passportIssued,
            any.passportIssuedDate ? new Date(any.passportIssuedDate) : null,
            any.passportFiles,
            any.prevWorkplaces.map((w: any) => w),
            any.currentWorkplace,
            any.additionalQualifications.map((q: any) => AdditionalQualification.fromAny(q)),
            any.isTargetAgreement,
            any.targetAgreementFile,
            any.targetAgreementDate ? new Date(any.targetAgreementDate) : null,
            Enterprise.fromAny(any.targetAgreementEnterprise),
            any.mustServeInArmy,
            any.armySubpoenaFile,
            any.armyCallDate ? new Date(any.armyCallDate) : null,
            any.socialStatuses,
            any.status,
            any.address,
            any.isForeignCitizen,
            any.inn,
            any.mail,
            any.otherFiles,
            new Date(any.createdDateTimeUtc),
            any.modifiedDateTimeUtc ? new Date(any.modifiedDateTimeUtc) : null
        )
    }
}
