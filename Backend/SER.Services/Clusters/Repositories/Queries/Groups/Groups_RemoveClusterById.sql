UPDATE groups
SET
clusterid = null,
hascluster = false,
modifieddatetimeutc = @p_currentdatetimeutc
WHERE clusterid = @p_clusterid