UPDATE employees
SET
isremoved = true,
modifieddatetimeutc = @p_currentdatetimeutc
WHERE id = @p_id;
