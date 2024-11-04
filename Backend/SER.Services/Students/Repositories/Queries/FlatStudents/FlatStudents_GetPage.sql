select (
	id,
	name,
	surname, 
	patronymic,
	date_of_birth, 
	is_on_paid_study, 
	group_id, 
	work_places_info_id
) from students
ORDER BY created_date_time_utc desc 
OFFSET @p_offset 
LIMIT @p_limit;