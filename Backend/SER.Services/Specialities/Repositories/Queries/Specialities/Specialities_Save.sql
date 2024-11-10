INSERT INTO specialities(
	id,
	name,
	studyyears,
	createddatetimeutc,
	modifieddatetimeutc,
	isremoved
)
VALUES(
	@p_id,
	@p_name,
	@p_studyyears,
	@p_currentdatetimeutc,
	null,
	false
)
ON CONFLICT (id) DO UPDATE SET
	name = @p_name,
	studyyears = @p_studyyears, 
	modifieddatetimeutc = @p_currentdatetimeutc
