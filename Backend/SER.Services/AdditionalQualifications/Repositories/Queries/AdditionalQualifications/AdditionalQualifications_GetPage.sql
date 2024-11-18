select * from additionalqualifications
where not isremoved
order by modifieddatetimeutc desc, createddatetimeutc desc 
offset @p_offset limit @p_limit
