INSERT INTO additionalqualifications(
	id,
	name,
	code,
	studytime,

	createddatetimeutc,
	modifieddatetimeutc,
	isremoved
)
VALUES(
	@p_id,
	@p_name,
	@p_code,
	@p_studytime,

	@p_currentdatetimeutc,
	null,
	false
)
ON CONFLICT (id) DO UPDATE SET
	name = @p_name,
	code = @p_code,
	studytime = @p_studytime,

	modifieddatetimeutc = @p_currentdatetimeutc
