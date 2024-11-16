UPDATE curators SET
isremoved = true,
modifieddatetimeutc = @p_currentdatetimeutc
where id = @p_id;

UPDATE groups SET
curatorid = null,
modifieddatetimeutc = @p_currentdatetimeutc
where curatorid = @p_id;
