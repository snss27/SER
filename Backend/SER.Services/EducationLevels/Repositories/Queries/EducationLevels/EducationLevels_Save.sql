INSERT INTO educationlevels(
	id,
	type,
	name,
	code,
	studytime,

	createddatetimeutc,
	modifieddatetimeutc,
	isremoved
)
VALUES(
	@p_id,
	@p_type,
	@p_name,
	@p_code,
	@p_studytime,

	@p_currentdatetimeutc,
	null,
	false
)
ON CONFLICT (id) DO UPDATE SET
	type = @p_type,
	name = @p_name,
	code = @p_code,
	studytime = @p_studytime,
	modifieddatetimeutc = @p_currentdatetimeutc
