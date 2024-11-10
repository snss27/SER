INSERT INTO groups(
	id,
	number,
	structuralunit,
	specialityid,
	enrollmentyear,
	curatorname,
	createddatetimeutc,
	modifieddatetimeutc,
	isremoved
)
VALUES(
	@p_id,
	@p_number,
	@p_structuralunit,
	@p_specialityid,
	@p_enrollmentyear,
	@p_curatorname,
	@p_currentdatetimeutc,
	null,
	false
)
ON CONFLICT (id) DO UPDATE SET
	number = @p_number,
	structuralunit = @p_structuralunit, 
	specialityid = @p_specialityid,
	enrollmentyear = @p_enrollmentyear,
	curatorname = @p_curatorname,
	modifieddatetimeutc = @p_currentdatetimeutc
