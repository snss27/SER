import { ClusterBlank } from "./clusterBlank"

export class Cluster {
    constructor(
        public readonly id: string,
        public readonly name: string
    ) {}

    public static fromAny(any: any): Cluster {
        return new Cluster(any.id, any.name)
    }

    public toBlank(): ClusterBlank {
        return {
            id: this.id,
            name: this.name,
        }
    }
}
