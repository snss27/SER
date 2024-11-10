UPDATE groups SET
isremoved = true,
modifieddatetimeutc = @p_currentdatetimeutc
where id = @p_id
