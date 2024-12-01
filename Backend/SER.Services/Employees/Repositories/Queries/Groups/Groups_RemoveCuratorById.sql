UPDATE groups
SET
curatorid = null,
modifieddatetimeutc = @p_currentdatetime
WHERE curatorid = @p_curatorid