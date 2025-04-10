INSERT INTO workplaces(
	id,
	enterpriseid,
	post,
	workbookextractfile,
	startdate,
	finishdate,

	createddatetimeutc,
	modifieddatetimeutc,
	isremoved
)
VALUES(
	@p_id,
	@p_enterpriseid,
	@p_post,
	@p_workbookextractfile,
	@p_startdate,
	@p_finishdate,

	@p_currentdatetimeutc,
	null,
	false
)
ON CONFLICT (id) DO UPDATE SET
	enterpriseid = @p_enterpriseid,
	post = @p_post,
	workbookextractfile = @p_workbookextractfile,
	startdate = @p_startdate,
	finishdate = @p_finishdate,

	modifieddatetimeutc = @p_currentdatetimeutc
RETURNING id;
