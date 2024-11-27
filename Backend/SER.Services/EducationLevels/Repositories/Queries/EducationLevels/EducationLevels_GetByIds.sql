select * from educationlevels where id=ANY(@p_ids) and not isremoved
