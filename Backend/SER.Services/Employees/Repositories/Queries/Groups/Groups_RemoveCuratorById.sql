UPDATE groups
SET
curatorid = null,
modifieddatetimeutc = @p_currentdatetimeutc
WHERE curatorid = @p_curatorid