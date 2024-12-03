USE RegistrationDB

CREATE TABLE tblRegister (
    UserID INT IDENTITY PRIMARY KEY, 
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) UNIQUE NOT NULL, 
    PasswordHash NVARCHAR(255) NOT NULL,
	City NVARCHAR(100),
    PhoneNumber NVARCHAR(15),
    DOB DATE,
    Gender NVARCHAR(20),
);

SELECT * FROM tblRegister

DROP TABLE tblRegister

---Retrive Applications

ALTER PROCEDURE SP_GetAllApplications
AS
BEGIN
	SELECT UserID,FirstName,LastName,Email,PasswordHash,City,PhoneNumber,DOB,Gender FROM tblRegister WITH(NOLOCK)
END


-- Insert Records

ALTER PROCEDURE SPI_Register
(
@FirstName NVARCHAR(100),
@LastName NVARCHAR(100),
@Email NVARCHAR(255),
@PasswordHash NVARCHAR(255)=NULL,
@City NVARCHAR(100),
@PhoneNumber NVARCHAR(15),
@DOB DATE,
@Gender NVARCHAR(20)
)
AS
BEGIN

DECLARE @RowCount int = 0 	
SET @RowCount = (SELECT COUNT(1) FROM tblRegister WHERE Email=@Email)


	BEGIN TRY
		BEGIN TRAN
		IF(@RowCount = 0)
			BEGIN
				INSERT INTO tblRegister(FirstName,LastName,Email,PasswordHash,City,PhoneNumber,DOB,Gender) 
				VALUES (@FirstName,@LastName,@Email,@PasswordHash,@City,@PhoneNumber,@DOB,@Gender)
			END
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SELECT ERROR_MESSAGE()
	END CATCH
END

-- Get By ID

ALTER PROCEDURE SP_GetElemntById
(@UserID INT )
AS
BEGIN
	SELECT [UserID],[FirstName],[LastName],[Email],[PasswordHash],[City],[PhoneNumber],[DOB],[Gender]
	FROM tblRegister
	WHERE UserID = @UserID
END


-- Update Query

ALTER PROCEDURE SPU_Register
(
@UserID INT,
@FirstName NVARCHAR(100),
@LastName NVARCHAR(100),
@Email NVARCHAR(255),
@PasswordHash NVARCHAR(255)=NULL,
@City NVARCHAR(100),
@PhoneNumber NVARCHAR(15),
@DOB DATE,
@Gender NVARCHAR(20)
)
AS
BEGIN
DECLARE @RowCount int = 0 	
SET @RowCount = (SELECT COUNT(1) FROM tblRegister WHERE Email=@Email and UserID <> @UserID)
	BEGIN TRY
		BEGIN TRAN
		IF(@RowCount = 0)
			BEGIN
				UPDATE tblRegister
				SET FirstName = @FirstName,
					LastName = @Lastname,
					Email = @Email,
					PasswordHash = @PasswordHash,
					City = @City,
					PhoneNumber = @PhoneNumber,
					DOB = @DOB,
					Gender = @Gender
				WHERE UserID = @UserID		
			END
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SELECT ERROR_MESSAGE()
	END CATCH
END

-- Delete Product

CREATE PROCEDURE SPD_Register
(
@UserID INT,
@ReturnMessage NVARCHAR(100) OUTPUT
)
AS
BEGIN
	DECLARE @RowCount int = 0

	BEGIN TRY
	SET @RowCount = (SELECT (1)	FROM tblRegister WHERE UserID = @UserID)
	IF (@RowCount > 0)
	BEGIN
			BEGIN TRAN
				DELETE FROM tblRegister
				WHERE UserID = @UserID
				SET @ReturnMessage = 'Product Deleted Successfuly ...!'
			COMMIT TRAN
	END

	ELSE

	BEGIN
		SET @ReturnMessage = 'Product not avilable with ID : ' + CONVERT(VARCHAR,@UserID)
	END

	END TRY
BEGIN CATCH
	ROLLBACK TRAN
	SET @ReturnMessage = ERROR_MESSAGE()
END CATCH
END
