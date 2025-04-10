SELECT * FROM additionalqualifications a
WHERE (a.name ~* @p_searchtext OR
	a.code ~* @p_searchtext) AND
	  NOT a.isremoved
ORDER BY a.name