select * from specialities
where not isremoved
order by createddatetimeutc
offset @p_offset limit @p_limit
