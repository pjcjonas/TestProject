CREATE TABLE users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    ProviderID VARCHAR(255) NOT NULL,
    ProviderName VARCHAR(50) NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL,
    DisplayName VARCHAR(255),
    ProfilePictureURL VARCHAR(512),
    CreatedAt DATETIME DEFAULT GETDATE(),
    LastLoginAt DATETIME,
    Roles VARCHAR(100),
    IsActive INT DEFAULT 1
);

CREATE TABLE roles (
    RoleID INT PRIMARY KEY IDENTITY(1,1),
    RoleName VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE user_roles (
    UserID INT,
    RoleID INT,
    FOREIGN KEY (UserID) REFERENCES users(UserID),
    FOREIGN KEY (RoleID) REFERENCES roles(RoleID),
    PRIMARY KEY (UserID, RoleID)
);

