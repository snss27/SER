INSERT INTO curators(
	id,
	name,
	surname,
	patronymic,
	createddatetimeutc,
	modifieddatetimeutc,
	isremoved
)
VALUES(
	@p_id,
	@p_namer,
	@p_surname,
	@p_patronymic,
	@p_currentdatetimeutc,
	null,
	false
)
ON CONFLICT (id) DO UPDATE SET
	name = @p_name,
	surname = @p_surname, 
	patronymic = @p_patronymic,
	modifieddatetimeutc = @p_currentdatetimeutc
