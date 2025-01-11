INSERT INTO groups(
	id,
	number,
	structuralunit,
	educationlevelid,
	enrollmentyear,
	curatorid,
	hascluster,
	clusterid,

	createddatetimeutc,
	modifieddatetimeutc,
	isremoved
)
VALUES (
    @p_id,
	@p_number,
	@p_structuralunit,
	@p_educationlevelid,
	@p_enrollmentyear,
	@p_curatorid,
	@p_hascluster,
	@p_clusterid,

	@p_currentdatetimeutc,
	null,
	false
)
ON CONFLICT (id) DO
UPDATE SET
	number = @p_number,
	structuralunit = @p_structuralunit,
	educationlevelid = @p_educationlevelid,
	enrollmentyear = @p_enrollmentyear,
	curatorid = @p_curatorid,
	hascluster = @p_hascluster,
	clusterid = @p_clusterid,

	modifieddatetimeutc = @p_currentdatetimeutc
