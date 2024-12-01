UPDATE groups
SET
educationlevelid = null,
modifieddatetimeutc = @p_currentdatetime
WHERE educationlevelid = @p_educationlevelid