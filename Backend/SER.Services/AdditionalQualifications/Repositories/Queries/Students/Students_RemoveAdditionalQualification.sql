UPDATE students
SET additionalqualifications = array_remove(additionalqualifications, @p_id),
modifieddatetimeutc = @p_currentdatetimeutc
WHERE @p_id = ANY(additionalqualifications)
