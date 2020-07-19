USE TSQL2012
GO
IF(OBJECT_ID('dbo.Klijenti') IS NOT NULL) DROP TABLE dbo.Klijenti;
GO
CREATE TABLE dbo.Klijenti(
	KlijentId int IDENTITY PRIMARY KEY NOT NULL,
	Naziv nvarchar(40) NOT NULL,
	Kontakt nvarchar(30) NOT NULL,
	Grad nvarchar(15) NOT NULL,
	Zemlja nvarchar(15) NOT NULL);

GO
INSERT INTO dbo.Klijenti
(Naziv, Kontakt, Grad, Zemlja)
SELECT companyname, contactname, city, country
FROM Sales.Customers;

GO


--READ PROCEDURA
CREATE PROC ReadKlijenata
AS
BEGIN TRY
		SELECT *
		FROM dbo.Klijenti
		ORDER BY KlijentId
	IF EXISTS(
		SELECT *
		FROM dbo.Klijenti
	)
	BEGIN
		RETURN 0;
	END
	ELSE
	BEGIN
		RETURN 1;
	END
END TRY
BEGIN CATCH
	RETURN @@ERROR;
END CATCH
GO

EXEC ReadKlijenata;

GO
--INSERT PROC
CREATE PROC InsertKlijenata
@Naziv nvarchar(40), 
@Kontakt nvarchar(30),
@Grad nvarchar(15), 
@Zemlja nvarchar(15)
AS
SET LOCK_TIMEOUT 3000;
BEGIN TRY
	IF(@Naziv IS NULL 
	OR @Kontakt IS NULL
	OR @Grad IS NULL
	OR @Zemlja IS NULL)
	BEGIN
		RETURN -1;
	END

	ELSE
	BEGIN
		INSERT INTO dbo.Klijenti(Naziv,Kontakt,Grad,Zemlja)
		VALUES(@Naziv,@Kontakt,@Grad,@Zemlja)
		RETURN 0
	END
END TRY
BEGIN CATCH 
	RETURN @@ERROR
END CATCH
GO


GO
--UPDATE PROC
CREATE PROC UpdateKlijenata
@KlijentID int,
@Naziv nvarchar(40), 
@Kontakt nvarchar(30),
@Grad nvarchar(15), 
@Zemlja nvarchar(15)
AS
BEGIN TRY
	IF(@KlijentID IS NULL 
	OR @Naziv IS NULL 
	OR @Kontakt IS NULL
	OR @Grad IS NULL
	OR @Zemlja IS NULL)
	BEGIN
		RETURN -1;
	END

	ELSE
	BEGIN
		UPDATE dbo.Klijenti
		SET Naziv=@Naziv,
		Kontakt=@Kontakt,
		Grad=@Grad,
		Zemlja=@Zemlja
		WHERE KlijentId=@KlijentID
	END
END TRY
	
BEGIN CATCH
	RETURN @@ERROR
END CATCH
GO

GO

--DELETE PROC
CREATE PROC DeleteKlijenata
@KlijentID int
AS
BEGIN TRY
	IF(@KlijentID IS NULL)
	BEGIN
		RETURN -1;
	END

	ELSE
	BEGIN
		DELETE FROM dbo.Klijenti
		WHERE KlijentId=@KlijentID
	END
END TRY
	
BEGIN CATCH
	RETURN @@ERROR
END CATCH
GO










 

