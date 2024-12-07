SELECT * FROM educationlevels e
WHERE (e.name ~* @p_searchtext OR
	  e.code ~* @p_searchtext) AND
	  type = 1 AND
	  NOT e.isremoved
ORDER BY e.name