INSERT INTO employees(
	id,
	name,
	secondname,
	lastname,

	createddatetimeutc,
	modifieddatetimeutc,
	isremoved
)
VALUES(
	@p_id,
	@p_name,
	@p_secondname,
	@p_lastname,

	@p_currentdatetimeutc,
	null,
	false
)
ON CONFLICT (id) DO UPDATE SET
	name = @p_name,
	secondname = @p_secondname, 
	lastname = @p_lastname,
	modifieddatetimeutc = @p_currentdatetimeutc
