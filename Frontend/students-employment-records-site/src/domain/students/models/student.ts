import { ArmyStatuses } from "../enums/armyStatuses";
import { Genders } from "../enums/genders";
import { Peculiarities } from "../enums/peculiarities";
import { StudentBlank } from "./studentBlank";

export class Student {
    constructor(
        public readonly id: string,
        public readonly name: string,
        public readonly secondName: string | null,
        public readonly lastName: string | null,
        public readonly gender: Genders | null,
        public readonly birthDate: Date | null,
        public readonly phoneNumber: string | null,
        public readonly representativePhoneNumber: string | null,
        public readonly isOnPaidStudy: boolean,
        public readonly snils: string | null,
        public readonly groupId: string | null,
        public readonly passportId: string | null,
        public readonly workplaceInfoId: string | null,
        public readonly additionalQualificationIds: string[],
        public readonly isTargetAgreement: boolean,
        public readonly targetAgreementFile: string | null,
        public readonly armyStatus: ArmyStatuses | null,
        public readonly armySubpoenaFile: string | null,
        public readonly armyServeDate: Date | null,
        public readonly peculiarity: Peculiarities | null,
        public readonly passportSeries: string | null,
        public readonly passportNumber: string | null,
        public readonly mail: string | null,
        public readonly inn: string | null,
        public readonly isForeignCitizen: boolean,
        public readonly address: string | null
    ) {}

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
            any.groupId,
            any.passportId,
            any.workplaceInfoId,
            any.additionalQualificationIds || [],
            any.isTargetAgreement,
            any.targetAgreementFile,
            any.armyStatus,
            any.armySubpoenaFile,
            any.armyServeDate ? new Date(any.armyServeDate) : null,
            any.peculiarity,
            any.passportSeries,
            any.passportNumber,
            any.mail,
            any.inn,
            any.isForeignCitizen,
            any.address
        );
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
            groupId: this.groupId,
            passportId: this.passportId,
            workplaceInfoId: this.workplaceInfoId,
            additionalQualificationIds: this.additionalQualificationIds,
            isTargetAgreement: this.isTargetAgreement,
            targetAgreementFile: this.targetAgreementFile,
            armyStatus: this.armyStatus,
            armySubpoenaFile: this.armySubpoenaFile,
            armyServeDate: this.armyServeDate,
            peculiarity: this.peculiarity,
            passportSeries: this.passportSeries,
            passportNumber: this.passportNumber,
            mail: this.mail,
            inn: this.inn,
            isForeignCitizen: this.isForeignCitizen,
            address: this.address
        };
    }
}