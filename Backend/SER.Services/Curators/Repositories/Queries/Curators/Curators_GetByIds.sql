select * from curators where id=ANY(@p_ids) and not isremoved
