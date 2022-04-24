IF (SELECT object_id FROM sys.tables WHERE Name = 'Recipe') IS NULL
	BEGIN
		print N'Table is not present. Will try to create a table in MasterDB.'
		CREATE TABLE [dbo].[Recipe] (
		[Name]	 NCHAR (100) NOT NULL,
		[Rashod] FLOAT (53)  NOT NULL
		);
	END
ELSE
	BEGIN
		print N'Table is already present'
	END


IF ((SELECT COUNT(*) FROM Recipe) = 0)
	BEGIN
		BULK INSERT Recipe
		FROM 'D:\Projects\visualstudio_source\Resources\Plan1.txt'
		WITH(
		CODEPAGE = '65001',
		FIELDTERMINATOR = ',',
		ROWTERMINATOR = '\n'
		);
	SELECT * FROM Recipe
	END
ELSE
	BEGIN
		print N'Unauthorized data is present. Data is removed, try again.'
	END

DELETE FROM Recipe
