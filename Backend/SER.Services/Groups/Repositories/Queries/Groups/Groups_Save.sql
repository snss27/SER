INSERT INTO groups(
	id,
	number,
	structuralunit,
	specialityid,
	enrollmentyear,
	curatorid,

	createddatetimeutc,
	modifieddatetimeutc,
	isremoved
)
VALUES (
    @p_id,
	@p_number,
	@p_structuralunit,
	@p_specialityid,
	@p_enrollmentyear,
	@p_curatorid,

	@p_currentdatetimeutc,
	null,
	false
)
ON CONFLICT (id) DO
UPDATE SET
	number = @p_number,
	structuralunit = @p_structuralunit,
	specialityid = @p_specialityid,
	enrollmentyear = @p_enrollmentyear,
	curatorid = @p_curatorid,

	modifieddatetimeutc = @p_currentdatetimeutc
