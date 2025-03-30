SELECT * FROM groups g
WHERE g.number ~* @p_searchtext AND
	  NOT g.isremoved
