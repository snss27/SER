INSERT INTO specialities(
	id,
	name,
	studyyears,
	studymonths,
	createddatetimeutc,
	modifieddatetimeutc,
	isremoved
)
VALUES(
	@p_id,
	@p_name,
	@p_studyyears,
	@p_studymonths,
	@p_currentdatetimeutc,
	null,
	false
)
ON CONFLICT (id) DO UPDATE SET
	name = @p_name,
	studyyears = @p_studyyears,
	studymonths = @p_studymonths,
	modifieddatetimeutc = @p_currentdatetimeutc
