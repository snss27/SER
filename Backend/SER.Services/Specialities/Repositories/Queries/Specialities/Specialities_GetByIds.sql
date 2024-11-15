select * from specialities where id=ANY(@p_ids) and not isremoved
