import { Cluster } from "@/domain/clusters/models/cluster"
import { Employee } from "@/domain/employees/models/employee"
import { StructuralUnits } from "@/domain/groups/enums/structuralUnits"
import { Group } from "@/domain/groups/models/group"
import { GroupGroupingType } from "../../enums/groupGroupingType"
import { EducationLevelGroupingOptions } from "./educationLevelGroupingOptions"

export type GroupGroupingOptions =
    | { type: GroupGroupingType.NotGrouping }
    | { type: GroupGroupingType.Groups; groups: Group[] }
    | { type: GroupGroupingType.StructuralUnits; structuralUnits: StructuralUnits[] }
    | {
          type: GroupGroupingType.EducationLevel
          educationLevelGroupingOptions: EducationLevelGroupingOptions
      }
    | {
          type: GroupGroupingType.EnrollmentYearPeriod
          enrollmentYearPeriod: [Date | null, Date | null]
      }
    | { type: GroupGroupingType.Curators; curators: Employee[] }
    | { type: GroupGroupingType.Clusters; clusters: Cluster[] }
