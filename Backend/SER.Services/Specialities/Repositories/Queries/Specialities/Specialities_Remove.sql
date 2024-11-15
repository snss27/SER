UPDATE specialities SET
isremoved = true,
modifieddatetimeutc = @p_currentdatetimeutc
where id = @p_id;

UPDATE groups SET
specialityid = null,
modifieddatetimeutc = @p_currentdatetimeutc
where specialityid = @p_id;

