CREATE PROCEDURE Genres_GetAll 
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Id, Name FROM Genres ORDER BY Name
END
GO

CREATE PROCEDURE Genres_GetById
	@Id int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Id, Name FROM Genres WHERE Id = @Id
END
GO

CREATE PROCEDURE Genres_Create
	@Name nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Genres (Name) VALUES (@Name); 
	SELECT SCOPE_IDENTITY();
END
GO

CREATE PROCEDURE Genres_Delete
	@Id int
AS
BEGIN
	SET NOCOUNT ON;
	DELETE FROM Genres WHERE Id = @Id
END
GO

CREATE PROCEDURE Genres_Exist
	@Id int
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS (SELECT 1 FROM Genres WHERE Id = @Id) SELECT 1; ELSE SELECT 0;
END
GO

CREATE PROCEDURE Genres_Update
	@Id int, @Name nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Genres SET Name = @Name WHERE Id = @Id
END
GO

CREATE PROCEDURE Actors_Create
	@Name nvarchar(150),
	@DateOfBirth datetime2,
	@Picture nvarchar(max)
AS
BEGIN

	SET NOCOUNT ON;

    INSERT INTO Actors (Name, DateOfBirth, Picture)
	VAlUES (@Name, @DateOfBirth, @Picture)
END
GO

CREATE PROCEDURE Actors_GetAll
AS
BEGIN

	SET NOCOUNT ON;

    SELECT Id, Name, DateOfBirth, Picture FROM Actors ORDER BY Name;
END
GO

CREATE PROCEDURE Actors_GetById
	@Id int
AS
BEGIN

	SET NOCOUNT ON;

    SELECT Id, Name, DateOfBirth, Picture FROM Actors WHERE Id = @Id;
END
GO

CREATE PROCEDURE Actors_Exist
	@Id int
AS
BEGIN

	SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Actors WHERE Id = @Id) 
		SELECT 1
	ELSE
		SELECT 0;
END
GO

CREATE PROCEDURE Actors_Update
	@Id int,
	@Name nvarchar(150),
	@DateOfBirth datetime2,
	@Picture nvarchar(max)
AS
BEGIN

	SET NOCOUNT ON;

    UPDATE Actors SET Name = @Name, DateOfBirth = @DateOfBirth, Picture = @Picture WHERE Id = @Id;
END
GO

CREATE PROCEDURE Actors_Delete
	@Id int
AS
BEGIN

	SET NOCOUNT ON;

    DELETE FROM Actors WHERE Id = @Id;
END
GO

CREATE PROCEDURE Actors_GetByName
	@Name nvarchar(150)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM Actors
	WHERE Name LIKE '%' + @Name + '%'
END
GO