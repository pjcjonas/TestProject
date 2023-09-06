CREATE PROCEDURE AddUser 
    @ProviderID VARCHAR(255),
    @ProviderName VARCHAR(50),
    @Email VARCHAR(255),
    @DisplayName VARCHAR(255),
    @ProfilePictureURL VARCHAR(512) = NULL  -- Assuming this parameter can be optional
AS
BEGIN
    -- Check if the user with the given ProviderID and ProviderName already exists
    IF NOT EXISTS (SELECT 1 FROM users WHERE ProviderID = @ProviderID AND ProviderName = @ProviderName)
    BEGIN
        INSERT INTO users (ProviderID, ProviderName, Email, DisplayName, ProfilePictureURL, CreatedAt, IsActive)
        VALUES (@ProviderID, @ProviderName, @Email, @DisplayName, @ProfilePictureURL, GETDATE(), 1)
    END
    ELSE
    BEGIN
        -- Handle user already existing, perhaps raise an error or just silently end the procedure
        RAISERROR('User already exists with the provided ProviderID and ProviderName', 16, 1)
    END
END
