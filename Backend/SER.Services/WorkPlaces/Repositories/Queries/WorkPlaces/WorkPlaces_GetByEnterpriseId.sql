SELECT *
FROM workplaces w
WHERE w.enterpriseid = @p_enterpriseid
  AND (
  EXISTS (
      SELECT 1
      FROM students s
      WHERE w.id = ANY(s.prevworkplaceids)
         OR w.id = s.currentworkplaceid
    )
  );
