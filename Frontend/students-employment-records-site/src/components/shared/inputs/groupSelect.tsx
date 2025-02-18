import { SxProps, Theme } from "@mui/material"
import React from "react"
import Select from "./select"
import { Group } from "@/domain/groups/models/group"



interface GroupSelectProps {
    value: Group | null
    onChange: (value: Group | null) => void
    label?: string
    disabled?: boolean
    sx?: SxProps<Theme>
    options: Group[] 
}

export const GroupSelect = (props: GroupSelectProps) => {
    return (
        <Select
            {...props}  
            options={props.options}  
            getOptionLabel={(group) => group.number}  
        />
    )
}
