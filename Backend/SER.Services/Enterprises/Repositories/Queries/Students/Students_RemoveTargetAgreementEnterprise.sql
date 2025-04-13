UPDATE students
SET
targetagreemententerpriseid = null,
modifieddatetimeutc = @p_currentdatetimeutc
WHERE targetagreemententerpriseid = @p_enterpriseid