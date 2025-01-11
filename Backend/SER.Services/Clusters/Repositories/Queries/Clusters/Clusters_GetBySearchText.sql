SELECT * FROM clusters c
WHERE c.name ~* @p_searchtext AND
	  NOT c.isremoved
ORDER BY c.name