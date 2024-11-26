SELECT * FROM employees c
WHERE (c.name ~* @p_searchtext OR
	  c.secondname ~* @p_searchtext OR
	  c.lastname ~* @p_searchtext) AND
	  NOT c.isremoved
ORDER BY c.secondname, c.name
