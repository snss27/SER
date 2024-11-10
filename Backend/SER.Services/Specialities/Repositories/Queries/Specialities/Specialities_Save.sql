INSERT INTO specialities(
	id,
	name,
	study_years,
	created_date_time_utc,
	modified_date_time_utc,
	is_removed
)
VALUES(
	@p_id,
	@p_name,
	@p_study_years,
	@p_current_date_time_utc,
	null,
	false
)
ON CONFLICT (id) DO UPDATE SET
	name = @p_name,
	study_years = @p_study_years, 
	modified_date_time_utc = @p_current_date_time_utc
