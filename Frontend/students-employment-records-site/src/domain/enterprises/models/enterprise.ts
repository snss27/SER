import { EnterpriseBlank } from "./enterpriseBlank"

export class Enterprise {
    constructor(
        public readonly id: string,
        public readonly name: string,
        public readonly legalAddress: string | null,
        public readonly actualAddress: string | null,
        public readonly address: string | null,
        public readonly INN: string | null,
        public readonly KPP: string | null,
        public readonly ORGN: string | null,
        public readonly phone: string | null,
        public readonly mail: string | null,
        public readonly isOPK: boolean
    ) {}

    public static fromAny(any: any): Enterprise {
        return new Enterprise(
            any.id,
            any.name,
            any.legalAddress,
            any.actualAddress,
            any.address,
            any.inn,
            any.kpp,
            any.orgn,
            any.phone,
            any.mail,
            any.isOpk
        )
    }

    public toBlank(): EnterpriseBlank {
        return {
            id: this.id,
            name: this.name,
            legalAddress: this.legalAddress,
            actualAddress: this.actualAddress,
            address: this.address,
            INN: this.INN,
            KPP: this.KPP,
            ORGN: this.ORGN,
            phone: this.phone,
            mail: this.mail,
            isOPK: this.isOPK,
        }
    }
}
