BEGIN
    CREATE TABLE [dbo].[Syrie] (
    [NameSyrie] NCHAR (100) NOT NULL,
    [SV]        FLOAT (53)  NOT NULL,
    [SrokHran]  INT         NOT NULL,
    [Nagruzka]  INT         NOT NULL,
    [Sklad]     NCHAR (100) NULL
    );
END
GO

BEGIN
    BULK INSERT Syrie 
    FROM 'D:\Projects\visualstudio_source\Resources\ProdSyrie.txt' 
    WITH(
    CODEPAGE = '65001',
    FIELDTERMINATOR = ',',
    ROWTERMINATOR = '\n'
    );
END
GO