DELETE FROM Sysie
GO

BULK INSERT [Sysie]
FROM 'D:\Projects\visualstudio_source\Resources\ProdSyrieSQL.txt'
WITH(
  CODEPAGE = '65001',
  FIELDTERMINATOR = ',',
  ROWTERMINATOR = '\n'
);
GO

SELECT * FROM Sysie
GO