import {
    Box,
    Table,
    TableBody,
    TableContainer,
    TableFooter,
    TablePagination,
    TableRow,
} from "@mui/material"

const StudentsTable = () => {
    return (
        <Box>
            <TableContainer>
                <Table>
                    <TableBody>
                        <TableRow></TableRow>
                    </TableBody>
                    <TableFooter>
                        <TableRow>
                            <TablePagination
                                colSpan={3}
                                count={100}
                                rowsPerPage={25}
                                page={1}
                                slotProps={{
                                    select: {
                                        inputProps: {
                                            "aria-label": "rows per page",
                                        },
                                        native: true,
                                    },
                                }}
                                onPageChange={() => undefined}
                                onRowsPerPageChange={() => undefined}
                            />
                        </TableRow>
                    </TableFooter>
                </Table>
            </TableContainer>
        </Box>
    )
}

export default StudentsTable
