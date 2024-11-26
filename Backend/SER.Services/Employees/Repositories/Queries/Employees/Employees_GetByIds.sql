select * from employees where id=ANY(@p_ids) and not isremoved
