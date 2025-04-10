SELECT * FROM enterprises e
WHERE e.name ~* @p_searchtext AND
	  NOT e.isremoved
ORDER BY e.name
