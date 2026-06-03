/****** Object:  UserDefinedTableType [dbo].[ActorsList]    Script Date: 2026/06/03 17:01:47 ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'ActorsList' AND ss.name = N'dbo')
CREATE TYPE [dbo].[ActorsList] AS TABLE(
	[ActorId] [int] NULL,
	[Character] [nvarchar](max) NULL,
	[Order] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[IntegersList]    Script Date: 2026/06/03 17:01:47 ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'IntegersList' AND ss.name = N'dbo')
CREATE TYPE [dbo].[IntegersList] AS TABLE(
	[Id] [int] NULL
)
GO
/****** Object:  Table [dbo].[Actors]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Actors]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Actors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[DateOfBirth] [datetime2](7) NOT NULL,
	[Picture] [nvarchar](max) NULL,
 CONSTRAINT [PK_Actors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ActorsMovies]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ActorsMovies]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ActorsMovies](
	[ActorId] [int] NOT NULL,
	[MovieId] [int] NOT NULL,
	[Order] [int] NOT NULL,
	[Character] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ActorsMovies] PRIMARY KEY CLUSTERED 
(
	[ActorId] ASC,
	[MovieId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Comments]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Comments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Body] [nvarchar](max) NOT NULL,
	[MovieId] [int] NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Errors]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Errors]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Errors](
	[Id] [uniqueidentifier] NOT NULL,
	[ErrorMessage] [nvarchar](max) NOT NULL,
	[StackTrace] [nvarchar](max) NULL,
	[Date] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Errors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Genres]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Genres]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Genres](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Genres] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[GenresMovies]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GenresMovies]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GenresMovies](
	[MovieId] [int] NOT NULL,
	[GenreId] [int] NOT NULL,
 CONSTRAINT [PK_GenresMovies] PRIMARY KEY CLUSTERED 
(
	[MovieId] ASC,
	[GenreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Movies]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Movies]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Movies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](150) NOT NULL,
	[InTheaters] [bit] NOT NULL,
	[ReleaseDate] [datetime2](7) NOT NULL,
	[Poster] [nvarchar](max) NULL,
 CONSTRAINT [PK_Movies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Roles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[RolesClaims]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RolesClaims]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RolesClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_RolesClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Users](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UsersClaims]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UsersClaims]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UsersClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_UsersClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UsersLogins]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UsersLogins]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UsersLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UsersLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UsersRoles]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UsersRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UsersRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UsersRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UsersTokens]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UsersTokens]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UsersTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_UsersTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Users_EmailConfirmed]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_EmailConfirmed]  DEFAULT ('false') FOR [EmailConfirmed]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Users_PhoneNumberConfirmed]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_PhoneNumberConfirmed]  DEFAULT ('false') FOR [PhoneNumberConfirmed]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Users_TwoFactorEnabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_TwoFactorEnabled]  DEFAULT ('false') FOR [TwoFactorEnabled]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Users_LockoutEnabled]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_LockoutEnabled]  DEFAULT ('false') FOR [LockoutEnabled]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Users_AccessFailedCount]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_AccessFailedCount]  DEFAULT ((0)) FOR [AccessFailedCount]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ActorsMovies_Actors]') AND parent_object_id = OBJECT_ID(N'[dbo].[ActorsMovies]'))
ALTER TABLE [dbo].[ActorsMovies]  WITH CHECK ADD  CONSTRAINT [FK_ActorsMovies_Actors] FOREIGN KEY([ActorId])
REFERENCES [dbo].[Actors] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ActorsMovies_Actors]') AND parent_object_id = OBJECT_ID(N'[dbo].[ActorsMovies]'))
ALTER TABLE [dbo].[ActorsMovies] CHECK CONSTRAINT [FK_ActorsMovies_Actors]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ActorsMovies_Movies]') AND parent_object_id = OBJECT_ID(N'[dbo].[ActorsMovies]'))
ALTER TABLE [dbo].[ActorsMovies]  WITH CHECK ADD  CONSTRAINT [FK_ActorsMovies_Movies] FOREIGN KEY([MovieId])
REFERENCES [dbo].[Movies] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ActorsMovies_Movies]') AND parent_object_id = OBJECT_ID(N'[dbo].[ActorsMovies]'))
ALTER TABLE [dbo].[ActorsMovies] CHECK CONSTRAINT [FK_ActorsMovies_Movies]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Comments_Movies]') AND parent_object_id = OBJECT_ID(N'[dbo].[Comments]'))
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Movies] FOREIGN KEY([MovieId])
REFERENCES [dbo].[Movies] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Comments_Movies]') AND parent_object_id = OBJECT_ID(N'[dbo].[Comments]'))
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Movies]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Comments_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Comments]'))
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Comments_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Comments]'))
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Users]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GenresMovies_Genres]') AND parent_object_id = OBJECT_ID(N'[dbo].[GenresMovies]'))
ALTER TABLE [dbo].[GenresMovies]  WITH CHECK ADD  CONSTRAINT [FK_GenresMovies_Genres] FOREIGN KEY([GenreId])
REFERENCES [dbo].[Genres] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GenresMovies_Genres]') AND parent_object_id = OBJECT_ID(N'[dbo].[GenresMovies]'))
ALTER TABLE [dbo].[GenresMovies] CHECK CONSTRAINT [FK_GenresMovies_Genres]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GenresMovies_Movies]') AND parent_object_id = OBJECT_ID(N'[dbo].[GenresMovies]'))
ALTER TABLE [dbo].[GenresMovies]  WITH CHECK ADD  CONSTRAINT [FK_GenresMovies_Movies] FOREIGN KEY([MovieId])
REFERENCES [dbo].[Movies] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GenresMovies_Movies]') AND parent_object_id = OBJECT_ID(N'[dbo].[GenresMovies]'))
ALTER TABLE [dbo].[GenresMovies] CHECK CONSTRAINT [FK_GenresMovies_Movies]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RolesClaims_Roles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RolesClaims]'))
ALTER TABLE [dbo].[RolesClaims]  WITH CHECK ADD  CONSTRAINT [FK_RolesClaims_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RolesClaims_Roles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RolesClaims]'))
ALTER TABLE [dbo].[RolesClaims] CHECK CONSTRAINT [FK_RolesClaims_Roles_RoleId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsersClaims_Users_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersClaims]'))
ALTER TABLE [dbo].[UsersClaims]  WITH CHECK ADD  CONSTRAINT [FK_UsersClaims_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsersClaims_Users_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersClaims]'))
ALTER TABLE [dbo].[UsersClaims] CHECK CONSTRAINT [FK_UsersClaims_Users_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsersLogins_Users_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersLogins]'))
ALTER TABLE [dbo].[UsersLogins]  WITH CHECK ADD  CONSTRAINT [FK_UsersLogins_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsersLogins_Users_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersLogins]'))
ALTER TABLE [dbo].[UsersLogins] CHECK CONSTRAINT [FK_UsersLogins_Users_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsersRoles_Roles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersRoles]'))
ALTER TABLE [dbo].[UsersRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersRoles_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsersRoles_Roles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersRoles]'))
ALTER TABLE [dbo].[UsersRoles] CHECK CONSTRAINT [FK_UsersRoles_Roles_RoleId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsersRoles_Users_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersRoles]'))
ALTER TABLE [dbo].[UsersRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersRoles_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsersRoles_Users_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersRoles]'))
ALTER TABLE [dbo].[UsersRoles] CHECK CONSTRAINT [FK_UsersRoles_Users_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsersTokens_Users_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersTokens]'))
ALTER TABLE [dbo].[UsersTokens]  WITH CHECK ADD  CONSTRAINT [FK_UsersTokens_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsersTokens_Users_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersTokens]'))
ALTER TABLE [dbo].[UsersTokens] CHECK CONSTRAINT [FK_UsersTokens_Users_UserId]
GO
/****** Object:  StoredProcedure [dbo].[Actors_Count]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Actors_Count]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Actors_Count] AS' 
END
GO

ALTER PROCEDURE [dbo].[Actors_Count]
	
AS
BEGIN

	SET NOCOUNT ON;

	SELECT COUNT(*) FROM Actors;
END
GO
/****** Object:  StoredProcedure [dbo].[Actors_Create]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Actors_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Actors_Create] AS' 
END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[Actors_Create]
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
/****** Object:  StoredProcedure [dbo].[Actors_Delete]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Actors_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Actors_Delete] AS' 
END
GO

ALTER PROCEDURE [dbo].[Actors_Delete]
	@Id int
AS
BEGIN

	SET NOCOUNT ON;

    DELETE FROM Actors WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[Actors_Exist]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Actors_Exist]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Actors_Exist] AS' 
END
GO

ALTER PROCEDURE [dbo].[Actors_Exist]
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
/****** Object:  StoredProcedure [dbo].[Actors_GetAll]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Actors_GetAll]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Actors_GetAll] AS' 
END
GO

ALTER PROCEDURE [dbo].[Actors_GetAll]
	@page int, 
	@recordsPerPage int
AS
BEGIN

	SET NOCOUNT ON;

    SELECT Id, Name, DateOfBirth, Picture FROM Actors ORDER BY Name
	OFFSET ((@page - 1) * @recordsPerPage) ROWS FETCH NEXT @recordsPerPage ROWS ONLY;
END
GO
/****** Object:  StoredProcedure [dbo].[Actors_GetById]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Actors_GetById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Actors_GetById] AS' 
END
GO

ALTER PROCEDURE [dbo].[Actors_GetById]
	@Id int
AS
BEGIN

	SET NOCOUNT ON;

    SELECT Id, Name, DateOfBirth, Picture FROM Actors WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[Actors_GetByName]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Actors_GetByName]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Actors_GetByName] AS' 
END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[Actors_GetByName]
	@Name nvarchar(150)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM Actors
	WHERE Name LIKE '%' + @Name + '%'
END
GO
/****** Object:  StoredProcedure [dbo].[Actors_GetBySeveralIds]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Actors_GetBySeveralIds]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Actors_GetBySeveralIds] AS' 
END
GO

ALTER PROCEDURE [dbo].[Actors_GetBySeveralIds]
	@ActorsIds IntegersList READONLY
AS
BEGIN
	
	SET NOCOUNT ON;

    Select Id from dbo.Actors
	where Id in (select Id from @ActorsIds)
END
GO
/****** Object:  StoredProcedure [dbo].[Actors_Update]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Actors_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Actors_Update] AS' 
END
GO

ALTER PROCEDURE [dbo].[Actors_Update]
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
/****** Object:  StoredProcedure [dbo].[Comments_Create]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Comments_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Comments_Create] AS' 
END
GO

ALTER PROCEDURE [dbo].[Comments_Create]
	@Body nvarchar(max),
	@MovieId int,
	@userId nvarchar(450)
	
AS
BEGIN

	SET NOCOUNT ON;

	INSERT INTO dbo.Comments (Body, MovieId, UserId) Values (@Body, @MovieId, @userId)
	SELECT SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[Comments_Delete]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Comments_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Comments_Delete] AS' 
END
GO

ALTER PROCEDURE [dbo].[Comments_Delete]
	@Id int
	
AS
BEGIN

	SET NOCOUNT ON;

	DeLETE FROM dbo.Comments
	WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[Comments_Exists]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Comments_Exists]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Comments_Exists] AS' 
END
GO

ALTER PROCEDURE [dbo].[Comments_Exists]
	@Id int
	
AS
BEGIN

	SET NOCOUNT ON;

	IF EXISTS (SELECT 1 FROM dbo.Comments WHERE Id = @Id)
		RETURN 1;
	ELSE
		RETURN 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Comments_GetById]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Comments_GetById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Comments_GetById] AS' 
END
GO

ALTER PROCEDURE [dbo].[Comments_GetById]
	@id int
	
AS
BEGIN

	SET NOCOUNT ON;

	Select * from dbo.Comments
	where Id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[Comments_GetByMovieId]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Comments_GetByMovieId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Comments_GetByMovieId] AS' 
END
GO

ALTER PROCEDURE [dbo].[Comments_GetByMovieId]
	@MovieId int
	
AS
BEGIN

	SET NOCOUNT ON;

	Select * from dbo.Comments
	where MovieId = @MovieId
END
GO
/****** Object:  StoredProcedure [dbo].[Comments_Update]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Comments_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Comments_Update] AS' 
END
GO

ALTER PROCEDURE [dbo].[Comments_Update]
	@id INT,
	@body NVARCHAR(MAX),
	@movieId INT
	
AS
BEGIN

	SET NOCOUNT ON;

	Update dbo.Comments set Body = @body, MovieId = @movieId
	WHERE Id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[Errors_Create]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Errors_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Errors_Create] AS' 
END
GO

ALTER PROCEDURE [dbo].[Errors_Create]
	@id uniqueidentifier,
	@errorMessage nvarchar(max),
	@stackTrace nvarchar(max),
	@date datetime2
AS
BEGIN

	SET NOCOUNT ON;

    insert into dbo.Errors (Id, ErrorMessage, StackTrace, Date)
	values (@id, @errorMessage, @stackTrace, @date);
END
GO
/****** Object:  StoredProcedure [dbo].[Genres_Create]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Genres_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Genres_Create] AS' 
END
GO

ALTER PROCEDURE [dbo].[Genres_Create]
	@Name nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Genres (Name) VALUES (@Name); 
	SELECT SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[Genres_Delete]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Genres_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Genres_Delete] AS' 
END
GO

ALTER PROCEDURE [dbo].[Genres_Delete]
	@Id int
AS
BEGIN
	SET NOCOUNT ON;
	DELETE FROM Genres WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[Genres_Exist]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Genres_Exist]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Genres_Exist] AS' 
END
GO

ALTER PROCEDURE [dbo].[Genres_Exist]
	@Id int
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS (SELECT 1 FROM Genres WHERE Id = @Id) SELECT 1; ELSE SELECT 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Genres_ExistsByIdAndName]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Genres_ExistsByIdAndName]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Genres_ExistsByIdAndName] AS' 
END
GO

ALTER PROCEDURE [dbo].[Genres_ExistsByIdAndName]
	@Id int,
	@Name nvarchar(50)
	
AS
BEGIN
	
	SET NOCOUNT ON;

    IF Exists(SELECT 1 FROM dbo.Genres WHERE Id != @Id AND Name = @Name)
		Select 1;
	Else
		Select 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Genres_GetAll]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Genres_GetAll]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Genres_GetAll] AS' 
END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[Genres_GetAll] 
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Id, Name FROM Genres ORDER BY Name
END
GO
/****** Object:  StoredProcedure [dbo].[Genres_GetById]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Genres_GetById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Genres_GetById] AS' 
END
GO

ALTER PROCEDURE [dbo].[Genres_GetById]
	@Id int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Id, Name FROM Genres WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[Genres_GetBySeveralIds]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Genres_GetBySeveralIds]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Genres_GetBySeveralIds] AS' 
END
GO

ALTER PROCEDURE [dbo].[Genres_GetBySeveralIds]
	@GenresIds IntegersList READONLY
	
AS
BEGIN
	
	SET NOCOUNT ON;

    Select Id from dbo.Genres
	where Id in (select Id from @GenresIds)
END
GO
/****** Object:  StoredProcedure [dbo].[Genres_Update]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Genres_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Genres_Update] AS' 
END
GO

ALTER PROCEDURE [dbo].[Genres_Update]
	@Id int, @Name nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Genres SET Name = @Name WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[Movies_AssignActors]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Movies_AssignActors]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Movies_AssignActors] AS' 
END
GO

ALTER PROCEDURE [dbo].[Movies_AssignActors]
	@MovieId int,
	@Actors ActorsList READONLY
	
AS
BEGIN
	
	SET NOCOUNT ON;

    Delete from dbo.ActorsMovies
	where MovieId = @MovieId

	Insert into dbo.ActorsMovies (ActorId, MovieId, [Order], Character)
	Select ActorId, @MovieId, [Order], Character from @Actors
END
GO
/****** Object:  StoredProcedure [dbo].[Movies_AssignGenres]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Movies_AssignGenres]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Movies_AssignGenres] AS' 
END
GO

ALTER PROCEDURE [dbo].[Movies_AssignGenres]
	@MovieId int,
	@GenreId IntegersList READONLY
	
AS
BEGIN
	SET NOCOUNT ON;

    DELETE FROM GenresMovies
	WHERE MovieId = @MovieId;

	INSERT INTO GenresMovies (MovieId, GenreId)
	SELECT @MovieId, Id FROM @GenreId;
END
GO
/****** Object:  StoredProcedure [dbo].[Movies_Count]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Movies_Count]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Movies_Count] AS' 
END
GO

ALTER PROCEDURE [dbo].[Movies_Count]
	@Title nvarchar(150) = '',
	@GenreId int = 0,
	@FutureReleases bit = 'False',
	@InTheaters bit = 'False'
AS
BEGIN
	SET NOCOUNT ON;

	Select Count(*) From dbo.Movies
	Where 
	(Title LIKE '%' + @Title + '%' Or @Title='')
	And
	(ReleaseDate > GETDATE() Or @FutureReleases='False')
	And
	(InTheaters = 'True' Or @InTheaters='False')
END
GO
/****** Object:  StoredProcedure [dbo].[Movies_Create]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Movies_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Movies_Create] AS' 
END
GO

ALTER PROCEDURE [dbo].[Movies_Create]
	@Title nvarchar(150),
	@InTheaters bit,
	@ReleaseDate datetime2,
	@Poster nvarchar(max)
	
AS
BEGIN
	SET NOCOUNT ON;

	Insert into dbo.Movies (Title, InTheaters, ReleaseDate, Poster)
	Values (@Title, @InTheaters, @ReleaseDate, @Poster)
	Select SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[Movies_Delete]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Movies_Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Movies_Delete] AS' 
END
GO

ALTER PROCEDURE [dbo].[Movies_Delete]
	@Id int
	
AS
BEGIN
	SET NOCOUNT ON;

	Delete from dbo.Movies
	where Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[Movies_Exists]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Movies_Exists]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Movies_Exists] AS' 
END
GO

ALTER PROCEDURE [dbo].[Movies_Exists]
	@Id int
	
	
AS
BEGIN
	SET NOCOUNT ON;

	if exists (select 1 from dbo.Movies where Id = @Id)
		select 1;
	else
		select 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Movies_Filter]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Movies_Filter]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Movies_Filter] AS' 
END
GO
ALTER PROCEDURE [dbo].[Movies_Filter]
	@Page int,
	@RecordsPerPage int,
	@Title nvarchar(150),
	@GenreId int,
	@FutureReleases bit,
	@InTheaters bit,
	@OrderByField nvarchar(150),
	@OrderByAscending bit
AS
BEGIN
	
	SET NOCOUNT ON;

    Select * From Movies
	Where 
	(Title LIKE '%' + @Title + '%' Or @Title='')
	And
	(ReleaseDate > GETDATE() Or @FutureReleases='False')
	And
	(InTheaters = 'True' Or @InTheaters='False')
	And
	(Id IN (Select MovieId From GenresMovies Where GenreId = @GenreId) Or @GenreId=0)
	Order By
		Case When @OrderByField='Title' And @OrderByAscending='TRUE' Then Title End Asc,
		Case When @OrderByField='Title' And @OrderByAscending='FALSE' Then Title End Desc,
		Case When @OrderByField='ReleaseDate' And @OrderByAscending='TRUE' Then ReleaseDate End Asc,
		Case When @OrderByField='ReleaseDate' And @OrderByAscending='FALSE' Then ReleaseDate End Desc
	Offset ((@Page-1)*@RecordsPerPage) Rows Fetch Next @RecordsPerPage Rows Only

END
GO
/****** Object:  StoredProcedure [dbo].[Movies_GetAll]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Movies_GetAll]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Movies_GetAll] AS' 
END
GO

ALTER PROCEDURE [dbo].[Movies_GetAll]
	@page int,
	@recordsPerPage int
	
AS
BEGIN
	SET NOCOUNT ON;

	Select * From dbo.Movies
	Order by Title
	OFFSET (@page - 1) * @recordsPerPage ROWS FETCH NEXT @recordsPerPage ROWS ONLY
END
GO
/****** Object:  StoredProcedure [dbo].[Movies_GetById]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Movies_GetById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Movies_GetById] AS' 
END
GO

ALTER PROCEDURE [dbo].[Movies_GetById]
	@Id int
	
	
AS
BEGIN
	SET NOCOUNT ON;

	Select * from dbo.Movies
	where Id = @Id

	Select * From dbo.Comments
	where MovieId = @Id

	select Id, Name from dbo.Genres
	Inner Join dbo.GenresMovies
	on GenresMovies.GenreId = Id
	where MovieId = @Id

	select Id, Name, Character from dbo.Actors
	Inner Join dbo.ActorsMovies
	On ActorsMovies.ActorId = Id
	where MovieId = @Id
	Order by [Order]
END
GO
/****** Object:  StoredProcedure [dbo].[Movies_Update]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Movies_Update]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Movies_Update] AS' 
END
GO

ALTER PROCEDURE [dbo].[Movies_Update]
	@Id int,
	@Title nvarchar(150),
	@InTheaters bit,
	@ReleaseDate datetime2,
	@Poster nvarchar(max)
	
AS
BEGIN
	SET NOCOUNT ON;

	Update dbo.Movies
	Set Title = @Title,
		InTheaters = @InTheaters,
		ReleaseDate = @ReleaseDate,
		Poster = @Poster
	WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[Users_Create]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users_Create]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Users_Create] AS' 
END
GO

ALTER PROCEDURE [dbo].[Users_Create]
	@id nvarchar(450),
	@email nvarchar(256),
	@normalizedEmail nvarchar(256),
	@UserName nvarchar(256),
	@normalizedUserName nvarchar(256),
	@passwordHash nvarchar(max)
AS
BEGIN

	SET NOCOUNT ON;

    Insert into dbo.Users (Id, Email, NormalizedEmail, UserName, NormalizedUserName, PasswordHash)
	Values (@id, @email, @normalizedEmail, @UserName, @normalizedUserName, @passwordHash);
END
GO
/****** Object:  StoredProcedure [dbo].[Users_GetByEmail]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users_GetByEmail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Users_GetByEmail] AS' 
END
GO

ALTER PROCEDURE [dbo].[Users_GetByEmail]
	@normalizedEmail nvarchar(256)
AS
BEGIN

	SET NOCOUNT ON;

    Select * from dbo.Users
	where NormalizedEmail = @normalizedEmail;
END
GO
/****** Object:  StoredProcedure [dbo].[Users_GetClaims]    Script Date: 2026/06/03 17:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users_GetClaims]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Users_GetClaims] AS' 
END
GO
ALTER PROCEDURE [dbo].[Users_GetClaims]
	@id nvarchar(450)
AS
BEGIN
	SET NOCOUNT ON;

    Select ClaimType as [Type], ClaimValue as [Value] From UsersClaims Where UserId = @id
END
GO
