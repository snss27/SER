INSERT INTO enterprises(
	id,
	name,
	legaladdress,
	actualaddress,
	address,
	inn,
	kpp,
	orgn,
	phone,
	mail,

	createddatetimeutc,
	modifieddatetimeutc,
	isremoved
)
VALUES(
	@p_id,
	@p_name,
	@p_legaladdress,
	@p_actualaddress,
	@p_address,
	@p_inn,
	@p_kpp,
	@p_orgn,
	@p_phone,
	@p_mail,

	@p_currentdatetimeutc,
	null,
	false
)
ON CONFLICT (id) DO UPDATE SET
	name = @p_name,
	legaladdress = @p_legaladdress,
	actualaddress = @p_actualaddress,
	address = @p_address,
	inn = @p_inn,
	kpp = @p_kpp,
	orgn = @p_orgn,
	phone = @p_phone,
	mail = @p_mail,

	modifieddatetimeutc = @p_currentdatetimeutc
