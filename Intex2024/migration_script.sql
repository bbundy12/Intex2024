IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AspNetRoles] (
    [Id] TEXT NOT NULL,
    [Name] TEXT NULL,
    [NormalizedName] TEXT NULL,
    [ConcurrencyStamp] TEXT NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] TEXT NOT NULL,
    [FirstName] TEXT NOT NULL,
    [LastName] TEXT NOT NULL,
    [BirthDate] TEXT NOT NULL,
    [CountryOfResidence] TEXT NOT NULL,
    [Gender] TEXT NOT NULL,
    [Age] INTEGER NOT NULL,
    [UserName] TEXT NULL,
    [NormalizedUserName] TEXT NULL,
    [Email] TEXT NULL,
    [NormalizedEmail] TEXT NULL,
    [EmailConfirmed] INTEGER NOT NULL,
    [PasswordHash] TEXT NULL,
    [SecurityStamp] TEXT NULL,
    [ConcurrencyStamp] TEXT NULL,
    [PhoneNumber] TEXT NULL,
    [PhoneNumberConfirmed] INTEGER NOT NULL,
    [TwoFactorEnabled] INTEGER NOT NULL,
    [LockoutEnd] TEXT NULL,
    [LockoutEnabled] INTEGER NOT NULL,
    [AccessFailedCount] INTEGER NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Products] (
    [ProductId] INTEGER NOT NULL,
    [Name] TEXT NOT NULL,
    [Year] INTEGER NOT NULL,
    [NumParts] INTEGER NOT NULL,
    [Price] TEXT NOT NULL,
    [ImgLink] TEXT NOT NULL,
    [PrimaryColor] TEXT NOT NULL,
    [SecondaryColor] TEXT NOT NULL,
    [Description] TEXT NOT NULL,
    [Category] TEXT NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([ProductId])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] INTEGER NOT NULL,
    [RoleId] TEXT NOT NULL,
    [ClaimType] TEXT NULL,
    [ClaimValue] TEXT NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] INTEGER NOT NULL,
    [UserId] TEXT NOT NULL,
    [ClaimType] TEXT NULL,
    [ClaimValue] TEXT NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] TEXT NOT NULL,
    [ProviderKey] TEXT NOT NULL,
    [ProviderDisplayName] TEXT NULL,
    [UserId] TEXT NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] TEXT NOT NULL,
    [RoleId] TEXT NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] TEXT NOT NULL,
    [LoginProvider] TEXT NOT NULL,
    [Name] TEXT NOT NULL,
    [Value] TEXT NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Carts] (
    [CartId] INTEGER NOT NULL,
    [CustomerId] TEXT NOT NULL,
    CONSTRAINT [PK_Carts] PRIMARY KEY ([CartId]),
    CONSTRAINT [FK_Carts_AspNetUsers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Orders] (
    [TransactionId] INTEGER NOT NULL,
    [Date] TEXT NOT NULL,
    [DayOfWeek] TEXT NOT NULL,
    [Time] TEXT NOT NULL,
    [TypeOfCard] TEXT NOT NULL,
    [EntryMode] TEXT NOT NULL,
    [Amount] TEXT NOT NULL,
    [TypeOfTransaction] TEXT NOT NULL,
    [CountryOfTransaction] TEXT NOT NULL,
    [ShippingAddress] TEXT NOT NULL,
    [Bank] TEXT NOT NULL,
    [Fraud] INTEGER NOT NULL,
    [CustomerId] TEXT NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([TransactionId]),
    CONSTRAINT [FK_Orders_AspNetUsers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ProductRecommendations] (
    [RecommendedProductId] INTEGER NOT NULL,
    [Rank] INTEGER NOT NULL,
    [ProductId] INTEGER NOT NULL,
    [SimilarityScore] TEXT NOT NULL,
    CONSTRAINT [PK_ProductRecommendations] PRIMARY KEY ([RecommendedProductId], [Rank]),
    CONSTRAINT [FK_ProductRecommendations_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserRecommendations] (
    [RecommendationId] INTEGER NOT NULL,
    [UserId] TEXT NOT NULL,
    [ProductId] INTEGER NOT NULL,
    CONSTRAINT [PK_UserRecommendations] PRIMARY KEY ([RecommendationId]),
    CONSTRAINT [FK_UserRecommendations_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRecommendations_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]) ON DELETE CASCADE
);
GO

CREATE TABLE [CartLine] (
    [CartLineId] INTEGER NOT NULL,
    [ProductId] INTEGER NOT NULL,
    [Quantity] INTEGER NOT NULL,
    [CartId] INTEGER NULL,
    CONSTRAINT [PK_CartLine] PRIMARY KEY ([CartLineId]),
    CONSTRAINT [FK_CartLine_Carts_CartId] FOREIGN KEY ([CartId]) REFERENCES [Carts] ([CartId]),
    CONSTRAINT [FK_CartLine_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]) ON DELETE CASCADE
);
GO

CREATE TABLE [LineItems] (
    [TransactionId] INTEGER NOT NULL,
    [ProductId] INTEGER NOT NULL,
    [Quantity] INTEGER NOT NULL,
    [Rating] INTEGER NOT NULL,
    CONSTRAINT [PK_LineItems] PRIMARY KEY ([ProductId], [TransactionId]),
    CONSTRAINT [FK_LineItems_Orders_TransactionId] FOREIGN KEY ([TransactionId]) REFERENCES [Orders] ([TransactionId]) ON DELETE CASCADE,
    CONSTRAINT [FK_LineItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([ProductId]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]);
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]);
GO

CREATE INDEX [IX_CartLine_CartId] ON [CartLine] ([CartId]);
GO

CREATE INDEX [IX_CartLine_ProductId] ON [CartLine] ([ProductId]);
GO

CREATE INDEX [IX_Carts_CustomerId] ON [Carts] ([CustomerId]);
GO

CREATE INDEX [IX_LineItems_TransactionId] ON [LineItems] ([TransactionId]);
GO

CREATE INDEX [IX_Orders_CustomerId] ON [Orders] ([CustomerId]);
GO

CREATE INDEX [IX_ProductRecommendations_ProductId] ON [ProductRecommendations] ([ProductId]);
GO

CREATE INDEX [IX_UserRecommendations_ProductId] ON [UserRecommendations] ([ProductId]);
GO

CREATE INDEX [IX_UserRecommendations_UserId] ON [UserRecommendations] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240409173223_FullDatabase', N'8.0.3');
GO

COMMIT;
GO

