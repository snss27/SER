SELECT * FROM specialities s
WHERE s.name ~* @p_searchtext AND
	  NOT s.isremoved
ORDER BY s.name
