SELECT * FROM educationlevels e
WHERE (e.name ~* @p_searchtext OR
	  e.code ~* @p_searchtext) AND
	  NOT e.isremoved
ORDER BY e.name
