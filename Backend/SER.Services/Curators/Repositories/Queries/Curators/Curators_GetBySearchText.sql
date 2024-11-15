SELECT * FROM curators c
WHERE c.name ~* @p_searchtext OR
	  c.surname ~* @p_searchtext OR
	  c.patronymic ~* @p_searchtext AND
	  NOT c.isremoved
ORDER BY c.surname, c.name
