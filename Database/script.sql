USE [TEST]
GO
/****** Object:  User [dotnet_owner]    Script Date: 21.10.2015 11:49:34 ******/
CREATE USER [dotnet_owner] FOR LOGIN [dotnet_owner] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  DatabaseRole [aspnet_WebEvent_FullAccess]    Script Date: 21.10.2015 11:49:34 ******/
CREATE ROLE [aspnet_WebEvent_FullAccess]
GO
/****** Object:  DatabaseRole [aspnet_Roles_ReportingAccess]    Script Date: 21.10.2015 11:49:34 ******/
CREATE ROLE [aspnet_Roles_ReportingAccess]
GO
/****** Object:  DatabaseRole [aspnet_Roles_FullAccess]    Script Date: 21.10.2015 11:49:34 ******/
CREATE ROLE [aspnet_Roles_FullAccess]
GO
/****** Object:  DatabaseRole [aspnet_Roles_BasicAccess]    Script Date: 21.10.2015 11:49:34 ******/
CREATE ROLE [aspnet_Roles_BasicAccess]
GO
/****** Object:  DatabaseRole [aspnet_Profile_ReportingAccess]    Script Date: 21.10.2015 11:49:34 ******/
CREATE ROLE [aspnet_Profile_ReportingAccess]
GO
/****** Object:  DatabaseRole [aspnet_Profile_FullAccess]    Script Date: 21.10.2015 11:49:34 ******/
CREATE ROLE [aspnet_Profile_FullAccess]
GO
/****** Object:  DatabaseRole [aspnet_Profile_BasicAccess]    Script Date: 21.10.2015 11:49:34 ******/
CREATE ROLE [aspnet_Profile_BasicAccess]
GO
/****** Object:  DatabaseRole [aspnet_Personalization_ReportingAccess]    Script Date: 21.10.2015 11:49:34 ******/
CREATE ROLE [aspnet_Personalization_ReportingAccess]
GO
/****** Object:  DatabaseRole [aspnet_Personalization_FullAccess]    Script Date: 21.10.2015 11:49:34 ******/
CREATE ROLE [aspnet_Personalization_FullAccess]
GO
/****** Object:  DatabaseRole [aspnet_Personalization_BasicAccess]    Script Date: 21.10.2015 11:49:34 ******/
CREATE ROLE [aspnet_Personalization_BasicAccess]
GO
/****** Object:  DatabaseRole [aspnet_Membership_ReportingAccess]    Script Date: 21.10.2015 11:49:34 ******/
CREATE ROLE [aspnet_Membership_ReportingAccess]
GO
/****** Object:  DatabaseRole [aspnet_Membership_FullAccess]    Script Date: 21.10.2015 11:49:34 ******/
CREATE ROLE [aspnet_Membership_FullAccess]
GO
/****** Object:  DatabaseRole [aspnet_Membership_BasicAccess]    Script Date: 21.10.2015 11:49:34 ******/
CREATE ROLE [aspnet_Membership_BasicAccess]
GO
/****** Object:  DatabaseRole [aspnet_ChangeNotification_ReceiveNotificationsOnlyAccess]    Script Date: 21.10.2015 11:49:34 ******/
CREATE ROLE [aspnet_ChangeNotification_ReceiveNotificationsOnlyAccess]
GO
ALTER ROLE [db_owner] ADD MEMBER [dotnet_owner]
GO
ALTER ROLE [aspnet_Roles_ReportingAccess] ADD MEMBER [aspnet_Roles_FullAccess]
GO
ALTER ROLE [aspnet_Roles_BasicAccess] ADD MEMBER [aspnet_Roles_FullAccess]
GO
ALTER ROLE [aspnet_Profile_ReportingAccess] ADD MEMBER [aspnet_Profile_FullAccess]
GO
ALTER ROLE [aspnet_Profile_BasicAccess] ADD MEMBER [aspnet_Profile_FullAccess]
GO
ALTER ROLE [aspnet_Personalization_ReportingAccess] ADD MEMBER [aspnet_Personalization_FullAccess]
GO
ALTER ROLE [aspnet_Personalization_BasicAccess] ADD MEMBER [aspnet_Personalization_FullAccess]
GO
ALTER ROLE [aspnet_Membership_ReportingAccess] ADD MEMBER [aspnet_Membership_FullAccess]
GO
ALTER ROLE [aspnet_Membership_BasicAccess] ADD MEMBER [aspnet_Membership_FullAccess]
GO
/****** Object:  Schema [aspnet_ChangeNotification_ReceiveNotificationsOnlyAccess]    Script Date: 21.10.2015 11:49:35 ******/
CREATE SCHEMA [aspnet_ChangeNotification_ReceiveNotificationsOnlyAccess]
GO
/****** Object:  Schema [aspnet_Membership_BasicAccess]    Script Date: 21.10.2015 11:49:35 ******/
CREATE SCHEMA [aspnet_Membership_BasicAccess]
GO
/****** Object:  Schema [aspnet_Membership_FullAccess]    Script Date: 21.10.2015 11:49:35 ******/
CREATE SCHEMA [aspnet_Membership_FullAccess]
GO
/****** Object:  Schema [aspnet_Membership_ReportingAccess]    Script Date: 21.10.2015 11:49:35 ******/
CREATE SCHEMA [aspnet_Membership_ReportingAccess]
GO
/****** Object:  Schema [aspnet_Personalization_BasicAccess]    Script Date: 21.10.2015 11:49:35 ******/
CREATE SCHEMA [aspnet_Personalization_BasicAccess]
GO
/****** Object:  Schema [aspnet_Personalization_FullAccess]    Script Date: 21.10.2015 11:49:35 ******/
CREATE SCHEMA [aspnet_Personalization_FullAccess]
GO
/****** Object:  Schema [aspnet_Personalization_ReportingAccess]    Script Date: 21.10.2015 11:49:35 ******/
CREATE SCHEMA [aspnet_Personalization_ReportingAccess]
GO
/****** Object:  Schema [aspnet_Profile_BasicAccess]    Script Date: 21.10.2015 11:49:35 ******/
CREATE SCHEMA [aspnet_Profile_BasicAccess]
GO
/****** Object:  Schema [aspnet_Profile_FullAccess]    Script Date: 21.10.2015 11:49:35 ******/
CREATE SCHEMA [aspnet_Profile_FullAccess]
GO
/****** Object:  Schema [aspnet_Profile_ReportingAccess]    Script Date: 21.10.2015 11:49:35 ******/
CREATE SCHEMA [aspnet_Profile_ReportingAccess]
GO
/****** Object:  Schema [aspnet_Roles_BasicAccess]    Script Date: 21.10.2015 11:49:35 ******/
CREATE SCHEMA [aspnet_Roles_BasicAccess]
GO
/****** Object:  Schema [aspnet_Roles_FullAccess]    Script Date: 21.10.2015 11:49:35 ******/
CREATE SCHEMA [aspnet_Roles_FullAccess]
GO
/****** Object:  Schema [aspnet_Roles_ReportingAccess]    Script Date: 21.10.2015 11:49:35 ******/
CREATE SCHEMA [aspnet_Roles_ReportingAccess]
GO
/****** Object:  Schema [aspnet_WebEvent_FullAccess]    Script Date: 21.10.2015 11:49:35 ******/
CREATE SCHEMA [aspnet_WebEvent_FullAccess]
GO
/****** Object:  StoredProcedure [dbo].[_helper_cms_remove_article]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[_helper_cms_remove_article]
	@rev_id AS BIGINT,
	@article_id AS INT
AS

SET NOCOUNT ON

IF EXISTS (SELECT * FROM dbo.cms_article_revision car WHERE article_id = @article_id AND car.rev_id = @rev_id)
BEGIN
delete from dbo.cms_language_relations WHERE article_id = @article_id
delete from dbo.cms_language_relations_revision WHERE article_id = @article_id
delete from dbo.cms_article_relation WHERE article_id = @article_id OR related_article_id = @article_id
delete from dbo.cms_article_relation_revision WHERE article_id = @article_id
delete from dbo.cms_article_revision WHERE article_id = @article_id
delete from dbo.cms_article_zones WHERE article_id = @article_id
delete from dbo.cms_article_zones_revision WHERE article_id = @article_id
delete from dbo.cms_articles WHERE article_id = @article_id
SELECT 'DELETED' AS what
END
ELSE
	BEGIN
		SELECT 'NOT DELETED' AS what
	END







GO
/****** Object:  StoredProcedure [dbo].[aspnet_AnyDataInTables]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_AnyDataInTables]
    @TablesToCheck int
AS
BEGIN
    -- Check Membership table if (@TablesToCheck & 1) is set
    IF ((@TablesToCheck & 1) <> 0 AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_MembershipUsers') AND (type = 'V'))))
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_Membership))
        BEGIN
            SELECT N'aspnet_Membership'
            RETURN
        END
    END

    -- Check aspnet_Roles table if (@TablesToCheck & 2) is set
    IF ((@TablesToCheck & 2) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_Roles') AND (type = 'V'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 RoleId FROM dbo.aspnet_Roles))
        BEGIN
            SELECT N'aspnet_Roles'
            RETURN
        END
    END

    -- Check aspnet_Profile table if (@TablesToCheck & 4) is set
    IF ((@TablesToCheck & 4) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_Profiles') AND (type = 'V'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_Profile))
        BEGIN
            SELECT N'aspnet_Profile'
            RETURN
        END
    END

    -- Check aspnet_PersonalizationPerUser table if (@TablesToCheck & 8) is set
    IF ((@TablesToCheck & 8) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_WebPartState_User') AND (type = 'V'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_PersonalizationPerUser))
        BEGIN
            SELECT N'aspnet_PersonalizationPerUser'
            RETURN
        END
    END

    -- Check aspnet_PersonalizationPerUser table if (@TablesToCheck & 16) is set
    IF ((@TablesToCheck & 16) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'aspnet_WebEvent_LogEvent') AND (type = 'P'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 * FROM dbo.aspnet_WebEvent_Events))
        BEGIN
            SELECT N'aspnet_WebEvent_Events'
            RETURN
        END
    END

    -- Check aspnet_Users table if (@TablesToCheck & 1,2,4 & 8) are all set
    IF ((@TablesToCheck & 1) <> 0 AND
        (@TablesToCheck & 2) <> 0 AND
        (@TablesToCheck & 4) <> 0 AND
        (@TablesToCheck & 8) <> 0 AND
        (@TablesToCheck & 32) <> 0 AND
        (@TablesToCheck & 128) <> 0 AND
        (@TablesToCheck & 256) <> 0 AND
        (@TablesToCheck & 512) <> 0 AND
        (@TablesToCheck & 1024) <> 0)
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_Users))
        BEGIN
            SELECT N'aspnet_Users'
            RETURN
        END
        IF (EXISTS(SELECT TOP 1 ApplicationId FROM dbo.aspnet_Applications))
        BEGIN
            SELECT N'aspnet_Applications'
            RETURN
        END
    END
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Applications_CreateApplication]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[aspnet_Applications_CreateApplication]
    @ApplicationName      nvarchar(256),
    @ApplicationId        uniqueidentifier OUTPUT
AS
BEGIN
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName

    IF(@ApplicationId IS NULL)
    BEGIN
        DECLARE @TranStarted   bit
        SET @TranStarted = 0

        IF( @@TRANCOUNT = 0 )
        BEGIN
	        BEGIN TRANSACTION
	        SET @TranStarted = 1
        END
        ELSE
    	    SET @TranStarted = 0

        SELECT  @ApplicationId = ApplicationId
        FROM dbo.aspnet_Applications WITH (UPDLOCK, HOLDLOCK)
        WHERE LOWER(@ApplicationName) = LoweredApplicationName

        IF(@ApplicationId IS NULL)
        BEGIN
            SELECT  @ApplicationId = NEWID()
            INSERT  dbo.aspnet_Applications (ApplicationId, ApplicationName, LoweredApplicationName)
            VALUES  (@ApplicationId, @ApplicationName, LOWER(@ApplicationName))
        END


        IF( @TranStarted = 1 )
        BEGIN
            IF(@@ERROR = 0)
            BEGIN
	        SET @TranStarted = 0
	        COMMIT TRANSACTION
            END
            ELSE
            BEGIN
                SET @TranStarted = 0
                ROLLBACK TRANSACTION
            END
        END
    END
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_CheckSchemaVersion]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[aspnet_CheckSchemaVersion]
    @Feature                   nvarchar(128),
    @CompatibleSchemaVersion   nvarchar(128)
AS
BEGIN
    IF (EXISTS( SELECT  *
                FROM    dbo.aspnet_SchemaVersions
                WHERE   Feature = LOWER( @Feature ) AND
                        CompatibleSchemaVersion = @CompatibleSchemaVersion ))
        RETURN 0

    RETURN 1
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_ChangePasswordQuestionAndAnswer]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_ChangePasswordQuestionAndAnswer]
    @ApplicationName       nvarchar(256),
    @UserName              nvarchar(256),
    @NewPasswordQuestion   nvarchar(256),
    @NewPasswordAnswer     nvarchar(128)
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Membership m, dbo.aspnet_Users u, dbo.aspnet_Applications a
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId
    IF (@UserId IS NULL)
    BEGIN
        RETURN(1)
    END

    UPDATE dbo.aspnet_Membership
    SET    PasswordQuestion = @NewPasswordQuestion, PasswordAnswer = @NewPasswordAnswer
    WHERE  UserId=@UserId
    RETURN(0)
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_CreateUser]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_CreateUser]
    @ApplicationName                        nvarchar(256),
    @UserName                               nvarchar(256),
    @Password                               nvarchar(128),
    @PasswordSalt                           nvarchar(128),
    @Email                                  nvarchar(256),
    @PasswordQuestion                       nvarchar(256),
    @PasswordAnswer                         nvarchar(128),
    @IsApproved                             bit,
    @CurrentTimeUtc                         datetime,
    @CreateDate                             datetime = NULL,
    @UniqueEmail                            int      = 0,
    @PasswordFormat                         int      = 0,
    @UserId                                 uniqueidentifier OUTPUT
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL

    DECLARE @NewUserId uniqueidentifier
    SELECT @NewUserId = NULL

    DECLARE @IsLockedOut bit
    SET @IsLockedOut = 0

    DECLARE @LastLockoutDate  datetime
    SET @LastLockoutDate = CONVERT( datetime, '17540101', 112 )

    DECLARE @FailedPasswordAttemptCount int
    SET @FailedPasswordAttemptCount = 0

    DECLARE @FailedPasswordAttemptWindowStart  datetime
    SET @FailedPasswordAttemptWindowStart = CONVERT( datetime, '17540101', 112 )

    DECLARE @FailedPasswordAnswerAttemptCount int
    SET @FailedPasswordAnswerAttemptCount = 0

    DECLARE @FailedPasswordAnswerAttemptWindowStart  datetime
    SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 )

    DECLARE @NewUserCreated bit
    DECLARE @ReturnValue   int
    SET @ReturnValue = 0

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    SET @CreateDate = @CurrentTimeUtc

    SELECT  @NewUserId = UserId FROM dbo.aspnet_Users WHERE LOWER(@UserName) = LoweredUserName AND @ApplicationId = ApplicationId
    IF ( @NewUserId IS NULL )
    BEGIN
        SET @NewUserId = @UserId
        EXEC @ReturnValue = dbo.aspnet_Users_CreateUser @ApplicationId, @UserName, 0, @CreateDate, @NewUserId OUTPUT
        SET @NewUserCreated = 1
    END
    ELSE
    BEGIN
        SET @NewUserCreated = 0
        IF( @NewUserId <> @UserId AND @UserId IS NOT NULL )
        BEGIN
            SET @ErrorCode = 6
            GOTO Cleanup
        END
    END

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @ReturnValue = -1 )
    BEGIN
        SET @ErrorCode = 10
        GOTO Cleanup
    END

    IF ( EXISTS ( SELECT UserId
                  FROM   dbo.aspnet_Membership
                  WHERE  @NewUserId = UserId ) )
    BEGIN
        SET @ErrorCode = 6
        GOTO Cleanup
    END

    SET @UserId = @NewUserId

    IF (@UniqueEmail = 1)
    BEGIN
        IF (EXISTS (SELECT *
                    FROM  dbo.aspnet_Membership m WITH ( UPDLOCK, HOLDLOCK )
                    WHERE ApplicationId = @ApplicationId AND LoweredEmail = LOWER(@Email)))
        BEGIN
            SET @ErrorCode = 7
            GOTO Cleanup
        END
    END

    IF (@NewUserCreated = 0)
    BEGIN
        UPDATE dbo.aspnet_Users
        SET    LastActivityDate = @CreateDate
        WHERE  @UserId = UserId
        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END

    INSERT INTO dbo.aspnet_Membership
                ( ApplicationId,
                  UserId,
                  Password,
                  PasswordSalt,
                  Email,
                  LoweredEmail,
                  PasswordQuestion,
                  PasswordAnswer,
                  PasswordFormat,
                  IsApproved,
                  IsLockedOut,
                  CreateDate,
                  LastLoginDate,
                  LastPasswordChangedDate,
                  LastLockoutDate,
                  FailedPasswordAttemptCount,
                  FailedPasswordAttemptWindowStart,
                  FailedPasswordAnswerAttemptCount,
                  FailedPasswordAnswerAttemptWindowStart )
         VALUES ( @ApplicationId,
                  @UserId,
                  @Password,
                  @PasswordSalt,
                  @Email,
                  LOWER(@Email),
                  @PasswordQuestion,
                  @PasswordAnswer,
                  @PasswordFormat,
                  @IsApproved,
                  @IsLockedOut,
                  @CreateDate,
                  @CreateDate,
                  @CreateDate,
                  @LastLockoutDate,
                  @FailedPasswordAttemptCount,
                  @FailedPasswordAttemptWindowStart,
                  @FailedPasswordAnswerAttemptCount,
                  @FailedPasswordAnswerAttemptWindowStart )

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
	    SET @TranStarted = 0
	    COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_FindUsersByEmail]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_FindUsersByEmail]
    @ApplicationName       nvarchar(256),
    @EmailToMatch          nvarchar(256),
    @PageIndex             int,
    @PageSize              int
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN 0

    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    IF( @EmailToMatch IS NULL )
        INSERT INTO #PageIndexForUsers (UserId)
            SELECT u.UserId
            FROM   dbo.aspnet_Users u, dbo.aspnet_Membership m
            WHERE  u.ApplicationId = @ApplicationId AND m.UserId = u.UserId AND m.Email IS NULL
            ORDER BY m.LoweredEmail
    ELSE
        INSERT INTO #PageIndexForUsers (UserId)
            SELECT u.UserId
            FROM   dbo.aspnet_Users u, dbo.aspnet_Membership m
            WHERE  u.ApplicationId = @ApplicationId AND m.UserId = u.UserId AND m.LoweredEmail LIKE LOWER(@EmailToMatch)
            ORDER BY m.LoweredEmail

    SELECT  u.UserName, m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate,
            m.LastLoginDate,
            u.LastActivityDate,
            m.LastPasswordChangedDate,
            u.UserId, m.IsLockedOut,
            m.LastLockoutDate
    FROM   dbo.aspnet_Membership m, dbo.aspnet_Users u, #PageIndexForUsers p
    WHERE  u.UserId = p.UserId AND u.UserId = m.UserId AND
           p.IndexId >= @PageLowerBound AND p.IndexId <= @PageUpperBound
    ORDER BY m.LoweredEmail

    SELECT  @TotalRecords = COUNT(*)
    FROM    #PageIndexForUsers
    RETURN @TotalRecords
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_FindUsersByName]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_FindUsersByName]
    @ApplicationName       nvarchar(256),
    @UserNameToMatch       nvarchar(256),
    @PageIndex             int,
    @PageSize              int
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN 0

    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    INSERT INTO #PageIndexForUsers (UserId)
        SELECT u.UserId
        FROM   dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE  u.ApplicationId = @ApplicationId AND m.UserId = u.UserId AND u.LoweredUserName LIKE LOWER(@UserNameToMatch)
        ORDER BY u.UserName


    SELECT  u.UserName, m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate,
            m.LastLoginDate,
            u.LastActivityDate,
            m.LastPasswordChangedDate,
            u.UserId, m.IsLockedOut,
            m.LastLockoutDate
    FROM   dbo.aspnet_Membership m, dbo.aspnet_Users u, #PageIndexForUsers p
    WHERE  u.UserId = p.UserId AND u.UserId = m.UserId AND
           p.IndexId >= @PageLowerBound AND p.IndexId <= @PageUpperBound
    ORDER BY u.UserName

    SELECT  @TotalRecords = COUNT(*)
    FROM    #PageIndexForUsers
    RETURN @TotalRecords
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetAllUsers]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetAllUsers]
    @ApplicationName       nvarchar(256),
    @PageIndex             int,
    @PageSize              int
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN 0


    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    INSERT INTO #PageIndexForUsers (UserId)
    SELECT u.UserId
    FROM   dbo.aspnet_Membership m, dbo.aspnet_Users u
    WHERE  u.ApplicationId = @ApplicationId AND u.UserId = m.UserId
    ORDER BY u.UserName

    SELECT @TotalRecords = @@ROWCOUNT

    SELECT u.UserName, m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate,
            m.LastLoginDate,
            u.LastActivityDate,
            m.LastPasswordChangedDate,
            u.UserId, m.IsLockedOut,
            m.LastLockoutDate
    FROM   dbo.aspnet_Membership m, dbo.aspnet_Users u, #PageIndexForUsers p
    WHERE  u.UserId = p.UserId AND u.UserId = m.UserId AND
           p.IndexId >= @PageLowerBound AND p.IndexId <= @PageUpperBound
    ORDER BY u.UserName
    RETURN @TotalRecords
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetNumberOfUsersOnline]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetNumberOfUsersOnline]
    @ApplicationName            nvarchar(256),
    @MinutesSinceLastInActive   int,
    @CurrentTimeUtc             datetime
AS
BEGIN
    DECLARE @DateActive datetime
    SELECT  @DateActive = DATEADD(minute,  -(@MinutesSinceLastInActive), @CurrentTimeUtc)

    DECLARE @NumOnline int
    SELECT  @NumOnline = COUNT(*)
    FROM    dbo.aspnet_Users u(NOLOCK),
            dbo.aspnet_Applications a(NOLOCK),
            dbo.aspnet_Membership m(NOLOCK)
    WHERE   u.ApplicationId = a.ApplicationId                  AND
            LastActivityDate > @DateActive                     AND
            a.LoweredApplicationName = LOWER(@ApplicationName) AND
            u.UserId = m.UserId
    RETURN(@NumOnline)
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetPassword]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[aspnet_Membership_GetPassword]
    @ApplicationName                nvarchar(256),
    @UserName                       nvarchar(256),
    @MaxInvalidPasswordAttempts     int,
    @PasswordAttemptWindow          int,
    @CurrentTimeUtc                 datetime,
    @PasswordAnswer                 nvarchar(128) = NULL
AS
BEGIN
    DECLARE @UserId                                 uniqueidentifier
    DECLARE @PasswordFormat                         int
    DECLARE @Password                               nvarchar(128)
    DECLARE @passAns                                nvarchar(128)
    DECLARE @IsLockedOut                            bit
    DECLARE @LastLockoutDate                        datetime
    DECLARE @FailedPasswordAttemptCount             int
    DECLARE @FailedPasswordAttemptWindowStart       datetime
    DECLARE @FailedPasswordAnswerAttemptCount       int
    DECLARE @FailedPasswordAnswerAttemptWindowStart datetime

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    SELECT  @UserId = u.UserId,
            @Password = m.Password,
            @passAns = m.PasswordAnswer,
            @PasswordFormat = m.PasswordFormat,
            @IsLockedOut = m.IsLockedOut,
            @LastLockoutDate = m.LastLockoutDate,
            @FailedPasswordAttemptCount = m.FailedPasswordAttemptCount,
            @FailedPasswordAttemptWindowStart = m.FailedPasswordAttemptWindowStart,
            @FailedPasswordAnswerAttemptCount = m.FailedPasswordAnswerAttemptCount,
            @FailedPasswordAnswerAttemptWindowStart = m.FailedPasswordAnswerAttemptWindowStart
    FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m WITH ( UPDLOCK )
    WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.ApplicationId = a.ApplicationId    AND
            u.UserId = m.UserId AND
            LOWER(@UserName) = u.LoweredUserName

    IF ( @@rowcount = 0 )
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    IF( @IsLockedOut = 1 )
    BEGIN
        SET @ErrorCode = 99
        GOTO Cleanup
    END

    IF ( NOT( @PasswordAnswer IS NULL ) )
    BEGIN
        IF( ( @passAns IS NULL ) OR ( LOWER( @passAns ) <> LOWER( @PasswordAnswer ) ) )
        BEGIN
            IF( @CurrentTimeUtc > DATEADD( minute, @PasswordAttemptWindow, @FailedPasswordAnswerAttemptWindowStart ) )
            BEGIN
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
                SET @FailedPasswordAnswerAttemptCount = 1
            END
            ELSE
            BEGIN
                SET @FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount + 1
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
            END

            BEGIN
                IF( @FailedPasswordAnswerAttemptCount >= @MaxInvalidPasswordAttempts )
                BEGIN
                    SET @IsLockedOut = 1
                    SET @LastLockoutDate = @CurrentTimeUtc
                END
            END

            SET @ErrorCode = 3
        END
        ELSE
        BEGIN
            IF( @FailedPasswordAnswerAttemptCount > 0 )
            BEGIN
                SET @FailedPasswordAnswerAttemptCount = 0
                SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 )
            END
        END

        UPDATE dbo.aspnet_Membership
        SET IsLockedOut = @IsLockedOut, LastLockoutDate = @LastLockoutDate,
            FailedPasswordAttemptCount = @FailedPasswordAttemptCount,
            FailedPasswordAttemptWindowStart = @FailedPasswordAttemptWindowStart,
            FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount,
            FailedPasswordAnswerAttemptWindowStart = @FailedPasswordAnswerAttemptWindowStart
        WHERE @UserId = UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    IF( @ErrorCode = 0 )
        SELECT @Password, @PasswordFormat

    RETURN @ErrorCode

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetPasswordWithFormat]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetPasswordWithFormat]
    @ApplicationName                nvarchar(256),
    @UserName                       nvarchar(256),
    @UpdateLastLoginActivityDate    bit,
    @CurrentTimeUtc                 datetime
AS
BEGIN
    DECLARE @IsLockedOut                        bit
    DECLARE @UserId                             uniqueidentifier
    DECLARE @Password                           nvarchar(128)
    DECLARE @PasswordSalt                       nvarchar(128)
    DECLARE @PasswordFormat                     int
    DECLARE @FailedPasswordAttemptCount         int
    DECLARE @FailedPasswordAnswerAttemptCount   int
    DECLARE @IsApproved                         bit
    DECLARE @LastActivityDate                   datetime
    DECLARE @LastLoginDate                      datetime

    SELECT  @UserId          = NULL

    SELECT  @UserId = u.UserId, @IsLockedOut = m.IsLockedOut, @Password=Password, @PasswordFormat=PasswordFormat,
            @PasswordSalt=PasswordSalt, @FailedPasswordAttemptCount=FailedPasswordAttemptCount,
		    @FailedPasswordAnswerAttemptCount=FailedPasswordAnswerAttemptCount, @IsApproved=IsApproved,
            @LastActivityDate = LastActivityDate, @LastLoginDate = LastLoginDate
    FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
    WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.ApplicationId = a.ApplicationId    AND
            u.UserId = m.UserId AND
            LOWER(@UserName) = u.LoweredUserName

    IF (@UserId IS NULL)
        RETURN 1

    IF (@IsLockedOut = 1)
        RETURN 99

    SELECT   @Password, @PasswordFormat, @PasswordSalt, @FailedPasswordAttemptCount,
             @FailedPasswordAnswerAttemptCount, @IsApproved, @LastLoginDate, @LastActivityDate

    IF (@UpdateLastLoginActivityDate = 1 AND @IsApproved = 1)
    BEGIN
        UPDATE  dbo.aspnet_Membership
        SET     LastLoginDate = @CurrentTimeUtc
        WHERE   UserId = @UserId

        UPDATE  dbo.aspnet_Users
        SET     LastActivityDate = @CurrentTimeUtc
        WHERE   @UserId = UserId
    END


    RETURN 0
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetUserByEmail]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetUserByEmail]
    @ApplicationName  nvarchar(256),
    @Email            nvarchar(256)
AS
BEGIN
    IF( @Email IS NULL )
        SELECT  u.UserName
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                u.UserId = m.UserId AND
                m.ApplicationId = a.ApplicationId AND
                m.LoweredEmail IS NULL
    ELSE
        SELECT  u.UserName
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                u.UserId = m.UserId AND
                m.ApplicationId = a.ApplicationId AND
                LOWER(@Email) = m.LoweredEmail

    IF (@@rowcount = 0)
        RETURN(1)
    RETURN(0)
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetUserByName]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetUserByName]
    @ApplicationName      nvarchar(256),
    @UserName             nvarchar(256),
    @CurrentTimeUtc       datetime,
    @UpdateLastActivity   bit = 0
AS
BEGIN
    DECLARE @UserId uniqueidentifier

    IF (@UpdateLastActivity = 1)
    BEGIN
        -- select user ID from aspnet_users table
        SELECT TOP 1 @UserId = u.UserId
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE    LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                LOWER(@UserName) = u.LoweredUserName AND u.UserId = m.UserId

        IF (@@ROWCOUNT = 0) -- Username not found
            RETURN -1

        UPDATE   dbo.aspnet_Users
        SET      LastActivityDate = @CurrentTimeUtc
        WHERE    @UserId = UserId

        SELECT m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
                m.CreateDate, m.LastLoginDate, u.LastActivityDate, m.LastPasswordChangedDate,
                u.UserId, m.IsLockedOut, m.LastLockoutDate
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE  @UserId = u.UserId AND u.UserId = m.UserId 
    END
    ELSE
    BEGIN
        SELECT TOP 1 m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
                m.CreateDate, m.LastLoginDate, u.LastActivityDate, m.LastPasswordChangedDate,
                u.UserId, m.IsLockedOut,m.LastLockoutDate
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE    LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                LOWER(@UserName) = u.LoweredUserName AND u.UserId = m.UserId

        IF (@@ROWCOUNT = 0) -- Username not found
            RETURN -1
    END

    RETURN 0
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetUserByUserId]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetUserByUserId]
    @UserId               uniqueidentifier,
    @CurrentTimeUtc       datetime,
    @UpdateLastActivity   bit = 0
AS
BEGIN
    IF ( @UpdateLastActivity = 1 )
    BEGIN
        UPDATE   dbo.aspnet_Users
        SET      LastActivityDate = @CurrentTimeUtc
        FROM     dbo.aspnet_Users
        WHERE    @UserId = UserId

        IF ( @@ROWCOUNT = 0 ) -- User ID not found
            RETURN -1
    END

    SELECT  m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate, m.LastLoginDate, u.LastActivityDate,
            m.LastPasswordChangedDate, u.UserName, m.IsLockedOut,
            m.LastLockoutDate
    FROM    dbo.aspnet_Users u, dbo.aspnet_Membership m
    WHERE   @UserId = u.UserId AND u.UserId = m.UserId

    IF ( @@ROWCOUNT = 0 ) -- User ID not found
       RETURN -1

    RETURN 0
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_ResetPassword]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_ResetPassword]
    @ApplicationName             nvarchar(256),
    @UserName                    nvarchar(256),
    @NewPassword                 nvarchar(128),
    @MaxInvalidPasswordAttempts  int,
    @PasswordAttemptWindow       int,
    @PasswordSalt                nvarchar(128),
    @CurrentTimeUtc              datetime,
    @PasswordFormat              int = 0,
    @PasswordAnswer              nvarchar(128) = NULL
AS
BEGIN
    DECLARE @IsLockedOut                            bit
    DECLARE @LastLockoutDate                        datetime
    DECLARE @FailedPasswordAttemptCount             int
    DECLARE @FailedPasswordAttemptWindowStart       datetime
    DECLARE @FailedPasswordAnswerAttemptCount       int
    DECLARE @FailedPasswordAnswerAttemptWindowStart datetime

    DECLARE @UserId                                 uniqueidentifier
    SET     @UserId = NULL

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF ( @UserId IS NULL )
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    SELECT @IsLockedOut = IsLockedOut,
           @LastLockoutDate = LastLockoutDate,
           @FailedPasswordAttemptCount = FailedPasswordAttemptCount,
           @FailedPasswordAttemptWindowStart = FailedPasswordAttemptWindowStart,
           @FailedPasswordAnswerAttemptCount = FailedPasswordAnswerAttemptCount,
           @FailedPasswordAnswerAttemptWindowStart = FailedPasswordAnswerAttemptWindowStart
    FROM dbo.aspnet_Membership WITH ( UPDLOCK )
    WHERE @UserId = UserId

    IF( @IsLockedOut = 1 )
    BEGIN
        SET @ErrorCode = 99
        GOTO Cleanup
    END

    UPDATE dbo.aspnet_Membership
    SET    Password = @NewPassword,
           LastPasswordChangedDate = @CurrentTimeUtc,
           PasswordFormat = @PasswordFormat,
           PasswordSalt = @PasswordSalt
    WHERE  @UserId = UserId AND
           ( ( @PasswordAnswer IS NULL ) OR ( LOWER( PasswordAnswer ) = LOWER( @PasswordAnswer ) ) )

    IF ( @@ROWCOUNT = 0 )
        BEGIN
            IF( @CurrentTimeUtc > DATEADD( minute, @PasswordAttemptWindow, @FailedPasswordAnswerAttemptWindowStart ) )
            BEGIN
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
                SET @FailedPasswordAnswerAttemptCount = 1
            END
            ELSE
            BEGIN
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
                SET @FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount + 1
            END

            BEGIN
                IF( @FailedPasswordAnswerAttemptCount >= @MaxInvalidPasswordAttempts )
                BEGIN
                    SET @IsLockedOut = 1
                    SET @LastLockoutDate = @CurrentTimeUtc
                END
            END

            SET @ErrorCode = 3
        END
    ELSE
        BEGIN
            IF( @FailedPasswordAnswerAttemptCount > 0 )
            BEGIN
                SET @FailedPasswordAnswerAttemptCount = 0
                SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 )
            END
        END

    IF( NOT ( @PasswordAnswer IS NULL ) )
    BEGIN
        UPDATE dbo.aspnet_Membership
        SET IsLockedOut = @IsLockedOut, LastLockoutDate = @LastLockoutDate,
            FailedPasswordAttemptCount = @FailedPasswordAttemptCount,
            FailedPasswordAttemptWindowStart = @FailedPasswordAttemptWindowStart,
            FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount,
            FailedPasswordAnswerAttemptWindowStart = @FailedPasswordAnswerAttemptWindowStart
        WHERE @UserId = UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    RETURN @ErrorCode

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_SetPassword]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_SetPassword]
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256),
    @NewPassword      nvarchar(128),
    @PasswordSalt     nvarchar(128),
    @CurrentTimeUtc   datetime,
    @PasswordFormat   int = 0
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF (@UserId IS NULL)
        RETURN(1)

    UPDATE dbo.aspnet_Membership
    SET Password = @NewPassword, PasswordFormat = @PasswordFormat, PasswordSalt = @PasswordSalt,
        LastPasswordChangedDate = @CurrentTimeUtc
    WHERE @UserId = UserId
    RETURN(0)
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_UnlockUser]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_UnlockUser]
    @ApplicationName                         nvarchar(256),
    @UserName                                nvarchar(256)
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF ( @UserId IS NULL )
        RETURN 1

    UPDATE dbo.aspnet_Membership
    SET IsLockedOut = 0,
        FailedPasswordAttemptCount = 0,
        FailedPasswordAttemptWindowStart = CONVERT( datetime, '17540101', 112 ),
        FailedPasswordAnswerAttemptCount = 0,
        FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 ),
        LastLockoutDate = CONVERT( datetime, '17540101', 112 )
    WHERE @UserId = UserId

    RETURN 0
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_UpdateUser]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_UpdateUser]
    @ApplicationName      nvarchar(256),
    @UserName             nvarchar(256),
    @Email                nvarchar(256),
    @Comment              ntext,
    @IsApproved           bit,
    @LastLoginDate        datetime,
    @LastActivityDate     datetime,
    @UniqueEmail          int,
    @CurrentTimeUtc       datetime
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId, @ApplicationId = a.ApplicationId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF (@UserId IS NULL)
        RETURN(1)

    IF (@UniqueEmail = 1)
    BEGIN
        IF (EXISTS (SELECT *
                    FROM  dbo.aspnet_Membership WITH (UPDLOCK, HOLDLOCK)
                    WHERE ApplicationId = @ApplicationId  AND @UserId <> UserId AND LoweredEmail = LOWER(@Email)))
        BEGIN
            RETURN(7)
        END
    END

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
	SET @TranStarted = 0

    UPDATE dbo.aspnet_Users WITH (ROWLOCK)
    SET
         LastActivityDate = @LastActivityDate
    WHERE
       @UserId = UserId

    IF( @@ERROR <> 0 )
        GOTO Cleanup

    UPDATE dbo.aspnet_Membership WITH (ROWLOCK)
    SET
         Email            = @Email,
         LoweredEmail     = LOWER(@Email),
         Comment          = @Comment,
         IsApproved       = @IsApproved,
         LastLoginDate    = @LastLoginDate
    WHERE
       @UserId = UserId

    IF( @@ERROR <> 0 )
        GOTO Cleanup

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN -1
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_UpdateUserInfo]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_UpdateUserInfo]
    @ApplicationName                nvarchar(256),
    @UserName                       nvarchar(256),
    @IsPasswordCorrect              bit,
    @UpdateLastLoginActivityDate    bit,
    @MaxInvalidPasswordAttempts     int,
    @PasswordAttemptWindow          int,
    @CurrentTimeUtc                 datetime,
    @LastLoginDate                  datetime,
    @LastActivityDate               datetime
AS
BEGIN
    DECLARE @UserId                                 uniqueidentifier
    DECLARE @IsApproved                             bit
    DECLARE @IsLockedOut                            bit
    DECLARE @LastLockoutDate                        datetime
    DECLARE @FailedPasswordAttemptCount             int
    DECLARE @FailedPasswordAttemptWindowStart       datetime
    DECLARE @FailedPasswordAnswerAttemptCount       int
    DECLARE @FailedPasswordAnswerAttemptWindowStart datetime

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    SELECT  @UserId = u.UserId,
            @IsApproved = m.IsApproved,
            @IsLockedOut = m.IsLockedOut,
            @LastLockoutDate = m.LastLockoutDate,
            @FailedPasswordAttemptCount = m.FailedPasswordAttemptCount,
            @FailedPasswordAttemptWindowStart = m.FailedPasswordAttemptWindowStart,
            @FailedPasswordAnswerAttemptCount = m.FailedPasswordAnswerAttemptCount,
            @FailedPasswordAnswerAttemptWindowStart = m.FailedPasswordAnswerAttemptWindowStart
    FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m WITH ( UPDLOCK )
    WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.ApplicationId = a.ApplicationId    AND
            u.UserId = m.UserId AND
            LOWER(@UserName) = u.LoweredUserName

    IF ( @@rowcount = 0 )
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    IF( @IsLockedOut = 1 )
    BEGIN
        GOTO Cleanup
    END

    IF( @IsPasswordCorrect = 0 )
    BEGIN
        IF( @CurrentTimeUtc > DATEADD( minute, @PasswordAttemptWindow, @FailedPasswordAttemptWindowStart ) )
        BEGIN
            SET @FailedPasswordAttemptWindowStart = @CurrentTimeUtc
            SET @FailedPasswordAttemptCount = 1
        END
        ELSE
        BEGIN
            SET @FailedPasswordAttemptWindowStart = @CurrentTimeUtc
            SET @FailedPasswordAttemptCount = @FailedPasswordAttemptCount + 1
        END

        BEGIN
            IF( @FailedPasswordAttemptCount >= @MaxInvalidPasswordAttempts )
            BEGIN
                SET @IsLockedOut = 1
                SET @LastLockoutDate = @CurrentTimeUtc
            END
        END
    END
    ELSE
    BEGIN
        IF( @FailedPasswordAttemptCount > 0 OR @FailedPasswordAnswerAttemptCount > 0 )
        BEGIN
            SET @FailedPasswordAttemptCount = 0
            SET @FailedPasswordAttemptWindowStart = CONVERT( datetime, '17540101', 112 )
            SET @FailedPasswordAnswerAttemptCount = 0
            SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 )
            SET @LastLockoutDate = CONVERT( datetime, '17540101', 112 )
        END
    END

    IF( @UpdateLastLoginActivityDate = 1 )
    BEGIN
        UPDATE  dbo.aspnet_Users
        SET     LastActivityDate = @LastActivityDate
        WHERE   @UserId = UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END

        UPDATE  dbo.aspnet_Membership
        SET     LastLoginDate = @LastLoginDate
        WHERE   UserId = @UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END


    UPDATE dbo.aspnet_Membership
    SET IsLockedOut = @IsLockedOut, LastLockoutDate = @LastLockoutDate,
        FailedPasswordAttemptCount = @FailedPasswordAttemptCount,
        FailedPasswordAttemptWindowStart = @FailedPasswordAttemptWindowStart,
        FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount,
        FailedPasswordAnswerAttemptWindowStart = @FailedPasswordAnswerAttemptWindowStart
    WHERE @UserId = UserId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    RETURN @ErrorCode

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Paths_CreatePath]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Paths_CreatePath]
    @ApplicationId UNIQUEIDENTIFIER,
    @Path           NVARCHAR(256),
    @PathId         UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
    BEGIN TRANSACTION
    IF (NOT EXISTS(SELECT * FROM dbo.aspnet_Paths WHERE LoweredPath = LOWER(@Path) AND ApplicationId = @ApplicationId))
    BEGIN
        INSERT dbo.aspnet_Paths (ApplicationId, Path, LoweredPath) VALUES (@ApplicationId, @Path, LOWER(@Path))
    END
    COMMIT TRANSACTION
    SELECT @PathId = PathId FROM dbo.aspnet_Paths WHERE LOWER(@Path) = LoweredPath AND ApplicationId = @ApplicationId
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Personalization_GetApplicationId]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Personalization_GetApplicationId] (
    @ApplicationName NVARCHAR(256),
    @ApplicationId UNIQUEIDENTIFIER OUT)
AS
BEGIN
    SELECT @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_DeleteAllState]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_DeleteAllState] (
    @AllUsersScope bit,
    @ApplicationName NVARCHAR(256),
    @Count int OUT)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
    BEGIN
        IF (@AllUsersScope = 1)
            DELETE FROM aspnet_PersonalizationAllUsers
            WHERE PathId IN
               (SELECT Paths.PathId
                FROM dbo.aspnet_Paths Paths
                WHERE Paths.ApplicationId = @ApplicationId)
        ELSE
            DELETE FROM aspnet_PersonalizationPerUser
            WHERE PathId IN
               (SELECT Paths.PathId
                FROM dbo.aspnet_Paths Paths
                WHERE Paths.ApplicationId = @ApplicationId)

        SELECT @Count = @@ROWCOUNT
    END
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_FindState]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_FindState] (
    @AllUsersScope bit,
    @ApplicationName NVARCHAR(256),
    @PageIndex              INT,
    @PageSize               INT,
    @Path NVARCHAR(256) = NULL,
    @UserName NVARCHAR(256) = NULL,
    @InactiveSinceDate DATETIME = NULL)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        RETURN

    -- Set the page bounds
    DECLARE @PageLowerBound INT
    DECLARE @PageUpperBound INT
    DECLARE @TotalRecords   INT
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table to store the selected results
    CREATE TABLE #PageIndex (
        IndexId int IDENTITY (0, 1) NOT NULL,
        ItemId UNIQUEIDENTIFIER
    )

    IF (@AllUsersScope = 1)
    BEGIN
        -- Insert into our temp table
        INSERT INTO #PageIndex (ItemId)
        SELECT Paths.PathId
        FROM dbo.aspnet_Paths Paths,
             ((SELECT Paths.PathId
               FROM dbo.aspnet_PersonalizationAllUsers AllUsers, dbo.aspnet_Paths Paths
               WHERE Paths.ApplicationId = @ApplicationId
                      AND AllUsers.PathId = Paths.PathId
                      AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
              ) AS SharedDataPerPath
              FULL OUTER JOIN
              (SELECT DISTINCT Paths.PathId
               FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Paths Paths
               WHERE Paths.ApplicationId = @ApplicationId
                      AND PerUser.PathId = Paths.PathId
                      AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
              ) AS UserDataPerPath
              ON SharedDataPerPath.PathId = UserDataPerPath.PathId
             )
        WHERE Paths.PathId = SharedDataPerPath.PathId OR Paths.PathId = UserDataPerPath.PathId
        ORDER BY Paths.Path ASC

        SELECT @TotalRecords = @@ROWCOUNT

        SELECT Paths.Path,
               SharedDataPerPath.LastUpdatedDate,
               SharedDataPerPath.SharedDataLength,
               UserDataPerPath.UserDataLength,
               UserDataPerPath.UserCount
        FROM dbo.aspnet_Paths Paths,
             ((SELECT PageIndex.ItemId AS PathId,
                      AllUsers.LastUpdatedDate AS LastUpdatedDate,
                      DATALENGTH(AllUsers.PageSettings) AS SharedDataLength
               FROM dbo.aspnet_PersonalizationAllUsers AllUsers, #PageIndex PageIndex
               WHERE AllUsers.PathId = PageIndex.ItemId
                     AND PageIndex.IndexId >= @PageLowerBound AND PageIndex.IndexId <= @PageUpperBound
              ) AS SharedDataPerPath
              FULL OUTER JOIN
              (SELECT PageIndex.ItemId AS PathId,
                      SUM(DATALENGTH(PerUser.PageSettings)) AS UserDataLength,
                      COUNT(*) AS UserCount
               FROM aspnet_PersonalizationPerUser PerUser, #PageIndex PageIndex
               WHERE PerUser.PathId = PageIndex.ItemId
                     AND PageIndex.IndexId >= @PageLowerBound AND PageIndex.IndexId <= @PageUpperBound
               GROUP BY PageIndex.ItemId
              ) AS UserDataPerPath
              ON SharedDataPerPath.PathId = UserDataPerPath.PathId
             )
        WHERE Paths.PathId = SharedDataPerPath.PathId OR Paths.PathId = UserDataPerPath.PathId
        ORDER BY Paths.Path ASC
    END
    ELSE
    BEGIN
        -- Insert into our temp table
        INSERT INTO #PageIndex (ItemId)
        SELECT PerUser.Id
        FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Users Users, dbo.aspnet_Paths Paths
        WHERE Paths.ApplicationId = @ApplicationId
              AND PerUser.UserId = Users.UserId
              AND PerUser.PathId = Paths.PathId
              AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
              AND (@UserName IS NULL OR Users.LoweredUserName LIKE LOWER(@UserName))
              AND (@InactiveSinceDate IS NULL OR Users.LastActivityDate <= @InactiveSinceDate)
        ORDER BY Paths.Path ASC, Users.UserName ASC

        SELECT @TotalRecords = @@ROWCOUNT

        SELECT Paths.Path, PerUser.LastUpdatedDate, DATALENGTH(PerUser.PageSettings), Users.UserName, Users.LastActivityDate
        FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Users Users, dbo.aspnet_Paths Paths, #PageIndex PageIndex
        WHERE PerUser.Id = PageIndex.ItemId
              AND PerUser.UserId = Users.UserId
              AND PerUser.PathId = Paths.PathId
              AND PageIndex.IndexId >= @PageLowerBound AND PageIndex.IndexId <= @PageUpperBound
        ORDER BY Paths.Path ASC, Users.UserName ASC
    END

    RETURN @TotalRecords
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_GetCountOfState]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_GetCountOfState] (
    @Count int OUT,
    @AllUsersScope bit,
    @ApplicationName NVARCHAR(256),
    @Path NVARCHAR(256) = NULL,
    @UserName NVARCHAR(256) = NULL,
    @InactiveSinceDate DATETIME = NULL)
AS
BEGIN

    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
        IF (@AllUsersScope = 1)
            SELECT @Count = COUNT(*)
            FROM dbo.aspnet_PersonalizationAllUsers AllUsers, dbo.aspnet_Paths Paths
            WHERE Paths.ApplicationId = @ApplicationId
                  AND AllUsers.PathId = Paths.PathId
                  AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
        ELSE
            SELECT @Count = COUNT(*)
            FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Users Users, dbo.aspnet_Paths Paths
            WHERE Paths.ApplicationId = @ApplicationId
                  AND PerUser.UserId = Users.UserId
                  AND PerUser.PathId = Paths.PathId
                  AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
                  AND (@UserName IS NULL OR Users.LoweredUserName LIKE LOWER(@UserName))
                  AND (@InactiveSinceDate IS NULL OR Users.LastActivityDate <= @InactiveSinceDate)
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_ResetSharedState]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_ResetSharedState] (
    @Count int OUT,
    @ApplicationName NVARCHAR(256),
    @Path NVARCHAR(256))
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
    BEGIN
        DELETE FROM dbo.aspnet_PersonalizationAllUsers
        WHERE PathId IN
            (SELECT AllUsers.PathId
             FROM dbo.aspnet_PersonalizationAllUsers AllUsers, dbo.aspnet_Paths Paths
             WHERE Paths.ApplicationId = @ApplicationId
                   AND AllUsers.PathId = Paths.PathId
                   AND Paths.LoweredPath = LOWER(@Path))

        SELECT @Count = @@ROWCOUNT
    END
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_ResetUserState]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_ResetUserState] (
    @Count                  int                 OUT,
    @ApplicationName        NVARCHAR(256),
    @InactiveSinceDate      DATETIME            = NULL,
    @UserName               NVARCHAR(256)       = NULL,
    @Path                   NVARCHAR(256)       = NULL)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
    BEGIN
        DELETE FROM dbo.aspnet_PersonalizationPerUser
        WHERE Id IN (SELECT PerUser.Id
                     FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Users Users, dbo.aspnet_Paths Paths
                     WHERE Paths.ApplicationId = @ApplicationId
                           AND PerUser.UserId = Users.UserId
                           AND PerUser.PathId = Paths.PathId
                           AND (@InactiveSinceDate IS NULL OR Users.LastActivityDate <= @InactiveSinceDate)
                           AND (@UserName IS NULL OR Users.LoweredUserName = LOWER(@UserName))
                           AND (@Path IS NULL OR Paths.LoweredPath = LOWER(@Path)))

        SELECT @Count = @@ROWCOUNT
    END
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAllUsers_GetPageSettings]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAllUsers_GetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @Path              NVARCHAR(256))
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL

    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    SELECT p.PageSettings FROM dbo.aspnet_PersonalizationAllUsers p WHERE p.PathId = @PathId
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAllUsers_ResetPageSettings]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAllUsers_ResetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @Path              NVARCHAR(256))
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL

    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    DELETE FROM dbo.aspnet_PersonalizationAllUsers WHERE PathId = @PathId
    RETURN 0
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAllUsers_SetPageSettings]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAllUsers_SetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @Path             NVARCHAR(256),
    @PageSettings     IMAGE,
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        EXEC dbo.aspnet_Paths_CreatePath @ApplicationId, @Path, @PathId OUTPUT
    END

    IF (EXISTS(SELECT PathId FROM dbo.aspnet_PersonalizationAllUsers WHERE PathId = @PathId))
        UPDATE dbo.aspnet_PersonalizationAllUsers SET PageSettings = @PageSettings, LastUpdatedDate = @CurrentTimeUtc WHERE PathId = @PathId
    ELSE
        INSERT INTO dbo.aspnet_PersonalizationAllUsers(PathId, PageSettings, LastUpdatedDate) VALUES (@PathId, @PageSettings, @CurrentTimeUtc)
    RETURN 0
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationPerUser_GetPageSettings]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_GetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @UserName         NVARCHAR(256),
    @Path             NVARCHAR(256),
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER
    DECLARE @UserId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL
    SELECT @UserId = NULL

    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @UserId = u.UserId FROM dbo.aspnet_Users u WHERE u.ApplicationId = @ApplicationId AND u.LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
    BEGIN
        RETURN
    END

    UPDATE   dbo.aspnet_Users WITH (ROWLOCK)
    SET      LastActivityDate = @CurrentTimeUtc
    WHERE    UserId = @UserId
    IF (@@ROWCOUNT = 0) -- Username not found
        RETURN

    SELECT p.PageSettings FROM dbo.aspnet_PersonalizationPerUser p WHERE p.PathId = @PathId AND p.UserId = @UserId
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationPerUser_ResetPageSettings]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_ResetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @UserName         NVARCHAR(256),
    @Path             NVARCHAR(256),
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER
    DECLARE @UserId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL
    SELECT @UserId = NULL

    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @UserId = u.UserId FROM dbo.aspnet_Users u WHERE u.ApplicationId = @ApplicationId AND u.LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
    BEGIN
        RETURN
    END

    UPDATE   dbo.aspnet_Users WITH (ROWLOCK)
    SET      LastActivityDate = @CurrentTimeUtc
    WHERE    UserId = @UserId
    IF (@@ROWCOUNT = 0) -- Username not found
        RETURN

    DELETE FROM dbo.aspnet_PersonalizationPerUser WHERE PathId = @PathId AND UserId = @UserId
    RETURN 0
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationPerUser_SetPageSettings]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_SetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @UserName         NVARCHAR(256),
    @Path             NVARCHAR(256),
    @PageSettings     IMAGE,
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER
    DECLARE @UserId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL
    SELECT @UserId = NULL

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        EXEC dbo.aspnet_Paths_CreatePath @ApplicationId, @Path, @PathId OUTPUT
    END

    SELECT @UserId = u.UserId FROM dbo.aspnet_Users u WHERE u.ApplicationId = @ApplicationId AND u.LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
    BEGIN
        EXEC dbo.aspnet_Users_CreateUser @ApplicationId, @UserName, 0, @CurrentTimeUtc, @UserId OUTPUT
    END

    UPDATE   dbo.aspnet_Users WITH (ROWLOCK)
    SET      LastActivityDate = @CurrentTimeUtc
    WHERE    UserId = @UserId
    IF (@@ROWCOUNT = 0) -- Username not found
        RETURN

    IF (EXISTS(SELECT PathId FROM dbo.aspnet_PersonalizationPerUser WHERE UserId = @UserId AND PathId = @PathId))
        UPDATE dbo.aspnet_PersonalizationPerUser SET PageSettings = @PageSettings, LastUpdatedDate = @CurrentTimeUtc WHERE UserId = @UserId AND PathId = @PathId
    ELSE
        INSERT INTO dbo.aspnet_PersonalizationPerUser(UserId, PathId, PageSettings, LastUpdatedDate) VALUES (@UserId, @PathId, @PageSettings, @CurrentTimeUtc)
    RETURN 0
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_DeleteInactiveProfiles]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[aspnet_Profile_DeleteInactiveProfiles]
    @ApplicationName        nvarchar(256),
    @ProfileAuthOptions     int,
    @InactiveSinceDate      datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
    BEGIN
        SELECT  0
        RETURN
    END

    DELETE
    FROM    dbo.aspnet_Profile
    WHERE   UserId IN
            (   SELECT  UserId
                FROM    dbo.aspnet_Users u
                WHERE   ApplicationId = @ApplicationId
                        AND (LastActivityDate <= @InactiveSinceDate)
                        AND (
                                (@ProfileAuthOptions = 2)
                             OR (@ProfileAuthOptions = 0 AND IsAnonymous = 1)
                             OR (@ProfileAuthOptions = 1 AND IsAnonymous = 0)
                            )
            )

    SELECT  @@ROWCOUNT
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_DeleteProfiles]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[aspnet_Profile_DeleteProfiles]
    @ApplicationName        nvarchar(256),
    @UserNames              nvarchar(4000)
AS
BEGIN
    DECLARE @UserName     nvarchar(256)
    DECLARE @CurrentPos   int
    DECLARE @NextPos      int
    DECLARE @NumDeleted   int
    DECLARE @DeletedUser  int
    DECLARE @TranStarted  bit
    DECLARE @ErrorCode    int

    SET @ErrorCode = 0
    SET @CurrentPos = 1
    SET @NumDeleted = 0
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
        BEGIN TRANSACTION
        SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    WHILE (@CurrentPos <= LEN(@UserNames))
    BEGIN
        SELECT @NextPos = CHARINDEX(N',', @UserNames,  @CurrentPos)
        IF (@NextPos = 0 OR @NextPos IS NULL)
            SELECT @NextPos = LEN(@UserNames) + 1

        SELECT @UserName = SUBSTRING(@UserNames, @CurrentPos, @NextPos - @CurrentPos)
        SELECT @CurrentPos = @NextPos+1

        IF (LEN(@UserName) > 0)
        BEGIN
            SELECT @DeletedUser = 0
            EXEC dbo.aspnet_Users_DeleteUser @ApplicationName, @UserName, 4, @DeletedUser OUTPUT
            IF( @@ERROR <> 0 )
            BEGIN
                SET @ErrorCode = -1
                GOTO Cleanup
            END
            IF (@DeletedUser <> 0)
                SELECT @NumDeleted = @NumDeleted + 1
        END
    END
    SELECT @NumDeleted
    IF (@TranStarted = 1)
    BEGIN
    	SET @TranStarted = 0
    	COMMIT TRANSACTION
    END
    SET @TranStarted = 0

    RETURN 0

Cleanup:
    IF (@TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END
    RETURN @ErrorCode
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_GetNumberOfInactiveProfiles]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[aspnet_Profile_GetNumberOfInactiveProfiles]
    @ApplicationName        nvarchar(256),
    @ProfileAuthOptions     int,
    @InactiveSinceDate      datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
    BEGIN
        SELECT 0
        RETURN
    END

    SELECT  COUNT(*)
    FROM    dbo.aspnet_Users u, dbo.aspnet_Profile p
    WHERE   ApplicationId = @ApplicationId
        AND u.UserId = p.UserId
        AND (LastActivityDate <= @InactiveSinceDate)
        AND (
                (@ProfileAuthOptions = 2)
                OR (@ProfileAuthOptions = 0 AND IsAnonymous = 1)
                OR (@ProfileAuthOptions = 1 AND IsAnonymous = 0)
            )
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_GetProfiles]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[aspnet_Profile_GetProfiles]
    @ApplicationName        nvarchar(256),
    @ProfileAuthOptions     int,
    @PageIndex              int,
    @PageSize               int,
    @UserNameToMatch        nvarchar(256) = NULL,
    @InactiveSinceDate      datetime      = NULL
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN

    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    INSERT INTO #PageIndexForUsers (UserId)
        SELECT  u.UserId
        FROM    dbo.aspnet_Users u, dbo.aspnet_Profile p
        WHERE   ApplicationId = @ApplicationId
            AND u.UserId = p.UserId
            AND (@InactiveSinceDate IS NULL OR LastActivityDate <= @InactiveSinceDate)
            AND (     (@ProfileAuthOptions = 2)
                   OR (@ProfileAuthOptions = 0 AND IsAnonymous = 1)
                   OR (@ProfileAuthOptions = 1 AND IsAnonymous = 0)
                 )
            AND (@UserNameToMatch IS NULL OR LoweredUserName LIKE LOWER(@UserNameToMatch))
        ORDER BY UserName

    SELECT  u.UserName, u.IsAnonymous, u.LastActivityDate, p.LastUpdatedDate,
            DATALENGTH(p.PropertyNames) + DATALENGTH(p.PropertyValuesString) + DATALENGTH(p.PropertyValuesBinary)
    FROM    dbo.aspnet_Users u, dbo.aspnet_Profile p, #PageIndexForUsers i
    WHERE   u.UserId = p.UserId AND p.UserId = i.UserId AND i.IndexId >= @PageLowerBound AND i.IndexId <= @PageUpperBound

    SELECT COUNT(*)
    FROM   #PageIndexForUsers

    DROP TABLE #PageIndexForUsers
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_GetProperties]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[aspnet_Profile_GetProperties]
    @ApplicationName      nvarchar(256),
    @UserName             nvarchar(256),
    @CurrentTimeUtc       datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN

    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL

    SELECT @UserId = UserId
    FROM   dbo.aspnet_Users
    WHERE  ApplicationId = @ApplicationId AND LoweredUserName = LOWER(@UserName)

    IF (@UserId IS NULL)
        RETURN
    SELECT TOP 1 PropertyNames, PropertyValuesString, PropertyValuesBinary
    FROM         dbo.aspnet_Profile
    WHERE        UserId = @UserId

    IF (@@ROWCOUNT > 0)
    BEGIN
        UPDATE dbo.aspnet_Users
        SET    LastActivityDate=@CurrentTimeUtc
        WHERE  UserId = @UserId
    END
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_SetProperties]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[aspnet_Profile_SetProperties]
    @ApplicationName        nvarchar(256),
    @PropertyNames          ntext,
    @PropertyValuesString   ntext,
    @PropertyValuesBinary   image,
    @UserName               nvarchar(256),
    @IsUserAnonymous        bit,
    @CurrentTimeUtc         datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
       BEGIN TRANSACTION
       SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    DECLARE @UserId uniqueidentifier
    DECLARE @LastActivityDate datetime
    SELECT  @UserId = NULL
    SELECT  @LastActivityDate = @CurrentTimeUtc

    SELECT @UserId = UserId
    FROM   dbo.aspnet_Users
    WHERE  ApplicationId = @ApplicationId AND LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
        EXEC dbo.aspnet_Users_CreateUser @ApplicationId, @UserName, @IsUserAnonymous, @LastActivityDate, @UserId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    UPDATE dbo.aspnet_Users
    SET    LastActivityDate=@CurrentTimeUtc
    WHERE  UserId = @UserId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF (EXISTS( SELECT *
               FROM   dbo.aspnet_Profile
               WHERE  UserId = @UserId))
        UPDATE dbo.aspnet_Profile
        SET    PropertyNames=@PropertyNames, PropertyValuesString = @PropertyValuesString,
               PropertyValuesBinary = @PropertyValuesBinary, LastUpdatedDate=@CurrentTimeUtc
        WHERE  UserId = @UserId
    ELSE
        INSERT INTO dbo.aspnet_Profile(UserId, PropertyNames, PropertyValuesString, PropertyValuesBinary, LastUpdatedDate)
             VALUES (@UserId, @PropertyNames, @PropertyValuesString, @PropertyValuesBinary, @CurrentTimeUtc)

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
    	SET @TranStarted = 0
    	COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_RegisterSchemaVersion]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[aspnet_RegisterSchemaVersion]
    @Feature                   nvarchar(128),
    @CompatibleSchemaVersion   nvarchar(128),
    @IsCurrentVersion          bit,
    @RemoveIncompatibleSchema  bit
AS
BEGIN
    IF( @RemoveIncompatibleSchema = 1 )
    BEGIN
        DELETE FROM dbo.aspnet_SchemaVersions WHERE Feature = LOWER( @Feature )
    END
    ELSE
    BEGIN
        IF( @IsCurrentVersion = 1 )
        BEGIN
            UPDATE dbo.aspnet_SchemaVersions
            SET IsCurrentVersion = 0
            WHERE Feature = LOWER( @Feature )
        END
    END

    INSERT  dbo.aspnet_SchemaVersions( Feature, CompatibleSchemaVersion, IsCurrentVersion )
    VALUES( LOWER( @Feature ), @CompatibleSchemaVersion, @IsCurrentVersion )
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_CreateRole]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Roles_CreateRole]
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
        BEGIN TRANSACTION
        SET @TranStarted = 1
    END
    ELSE
        SET @TranStarted = 0

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF (EXISTS(SELECT RoleId FROM dbo.aspnet_Roles WHERE LoweredRoleName = LOWER(@RoleName) AND ApplicationId = @ApplicationId))
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    INSERT INTO dbo.aspnet_Roles
                (ApplicationId, RoleName, LoweredRoleName)
         VALUES (@ApplicationId, @RoleName, LOWER(@RoleName))

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        COMMIT TRANSACTION
    END

    RETURN(0)

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_DeleteRole]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[aspnet_Roles_DeleteRole]
    @ApplicationName            nvarchar(256),
    @RoleName                   nvarchar(256),
    @DeleteOnlyIfRoleIsEmpty    bit
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
        BEGIN TRANSACTION
        SET @TranStarted = 1
    END
    ELSE
        SET @TranStarted = 0

    DECLARE @RoleId   uniqueidentifier
    SELECT  @RoleId = NULL
    SELECT  @RoleId = RoleId FROM dbo.aspnet_Roles WHERE LoweredRoleName = LOWER(@RoleName) AND ApplicationId = @ApplicationId

    IF (@RoleId IS NULL)
    BEGIN
        SELECT @ErrorCode = 1
        GOTO Cleanup
    END
    IF (@DeleteOnlyIfRoleIsEmpty <> 0)
    BEGIN
        IF (EXISTS (SELECT RoleId FROM dbo.aspnet_UsersInRoles  WHERE @RoleId = RoleId))
        BEGIN
            SELECT @ErrorCode = 2
            GOTO Cleanup
        END
    END


    DELETE FROM dbo.aspnet_UsersInRoles  WHERE @RoleId = RoleId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    DELETE FROM dbo.aspnet_Roles WHERE @RoleId = RoleId  AND ApplicationId = @ApplicationId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        COMMIT TRANSACTION
    END

    RETURN(0)

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_GetAllRoles]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[aspnet_Roles_GetAllRoles] (
    @ApplicationName           nvarchar(256))
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN
    SELECT RoleName
    FROM   dbo.aspnet_Roles WHERE ApplicationId = @ApplicationId
    ORDER BY RoleName
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_RoleExists]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[aspnet_Roles_RoleExists]
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(0)
    IF (EXISTS (SELECT RoleName FROM dbo.aspnet_Roles WHERE LOWER(@RoleName) = LoweredRoleName AND ApplicationId = @ApplicationId ))
        RETURN(1)
    ELSE
        RETURN(0)
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Setup_RemoveAllRoleMembers]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[aspnet_Setup_RemoveAllRoleMembers]
    @name   sysname
AS
BEGIN
    CREATE TABLE #aspnet_RoleMembers
    (
        Group_name      sysname,
        Group_id        smallint,
        Users_in_group  sysname,
        User_id         smallint
    )

    INSERT INTO #aspnet_RoleMembers
    EXEC sp_helpuser @name

    DECLARE @user_id smallint
    DECLARE @cmd nvarchar(500)
    DECLARE c1 cursor FORWARD_ONLY FOR
        SELECT User_id FROM #aspnet_RoleMembers

    OPEN c1

    FETCH c1 INTO @user_id
    WHILE (@@fetch_status = 0)
    BEGIN
        SET @cmd = 'EXEC sp_droprolemember ' + '''' + @name + ''', ''' + USER_NAME(@user_id) + ''''
        EXEC (@cmd)
        FETCH c1 INTO @user_id
    END

    CLOSE c1
    DEALLOCATE c1
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Setup_RestorePermissions]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[aspnet_Setup_RestorePermissions]
    @name   sysname
AS
BEGIN
    DECLARE @object sysname
    DECLARE @protectType char(10)
    DECLARE @action varchar(60)
    DECLARE @grantee sysname
    DECLARE @cmd nvarchar(500)
    DECLARE c1 cursor FORWARD_ONLY FOR
        SELECT Object, ProtectType, [Action], Grantee FROM #aspnet_Permissions where Object = @name

    OPEN c1

    FETCH c1 INTO @object, @protectType, @action, @grantee
    WHILE (@@fetch_status = 0)
    BEGIN
        SET @cmd = @protectType + ' ' + @action + ' on ' + @object + ' TO [' + @grantee + ']'
        EXEC (@cmd)
        FETCH c1 INTO @object, @protectType, @action, @grantee
    END

    CLOSE c1
    DEALLOCATE c1
END





GO
/****** Object:  StoredProcedure [dbo].[AspNet_SqlCachePollingStoredProcedure]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AspNet_SqlCachePollingStoredProcedure] AS
         SELECT tableName, changeId FROM dbo.AspNet_SqlCacheTablesForChangeNotification
         RETURN 0






GO
/****** Object:  StoredProcedure [dbo].[AspNet_SqlCacheQueryRegisteredTablesStoredProcedure]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AspNet_SqlCacheQueryRegisteredTablesStoredProcedure] 
         AS
         SELECT tableName FROM dbo.AspNet_SqlCacheTablesForChangeNotification   






GO
/****** Object:  StoredProcedure [dbo].[AspNet_SqlCacheRegisterTableStoredProcedure]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AspNet_SqlCacheRegisterTableStoredProcedure] 
             @tableName NVARCHAR(450) 
         AS
         BEGIN

         DECLARE @triggerName AS NVARCHAR(3000) 
         DECLARE @fullTriggerName AS NVARCHAR(3000)
         DECLARE @canonTableName NVARCHAR(3000) 
         DECLARE @quotedTableName NVARCHAR(3000) 

         /* Create the trigger name */ 
         SET @triggerName = REPLACE(@tableName, '[', '__o__') 
         SET @triggerName = REPLACE(@triggerName, ']', '__c__') 
         SET @triggerName = @triggerName + '_AspNet_SqlCacheNotification_Trigger' 
         SET @fullTriggerName = 'dbo.[' + @triggerName + ']' 

         /* Create the cannonicalized table name for trigger creation */ 
         /* Do not touch it if the name contains other delimiters */ 
         IF (CHARINDEX('.', @tableName) <> 0 OR 
             CHARINDEX('[', @tableName) <> 0 OR 
             CHARINDEX(']', @tableName) <> 0) 
             SET @canonTableName = @tableName 
         ELSE 
             SET @canonTableName = '[' + @tableName + ']' 

         /* First make sure the table exists */ 
         IF (SELECT OBJECT_ID(@tableName, 'U')) IS NULL 
         BEGIN 
             RAISERROR ('00000001', 16, 1) 
             RETURN 
         END 

         BEGIN TRAN
         /* Insert the value into the notification table */ 
         IF NOT EXISTS (SELECT tableName FROM dbo.AspNet_SqlCacheTablesForChangeNotification WITH (NOLOCK) WHERE tableName = @tableName) 
             IF NOT EXISTS (SELECT tableName FROM dbo.AspNet_SqlCacheTablesForChangeNotification WITH (TABLOCKX) WHERE tableName = @tableName) 
                 INSERT  dbo.AspNet_SqlCacheTablesForChangeNotification 
                 VALUES (@tableName, GETDATE(), 0)

         /* Create the trigger */ 
         SET @quotedTableName = QUOTENAME(@tableName, '''') 
         IF NOT EXISTS (SELECT name FROM sysobjects WITH (NOLOCK) WHERE name = @triggerName AND type = 'TR') 
             IF NOT EXISTS (SELECT name FROM sysobjects WITH (TABLOCKX) WHERE name = @triggerName AND type = 'TR') 
                 EXEC('CREATE TRIGGER ' + @fullTriggerName + ' ON ' + @canonTableName +'
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N' + @quotedTableName + '
                       END
                       ')
         COMMIT TRAN
         END
   






GO
/****** Object:  StoredProcedure [dbo].[AspNet_SqlCacheUnRegisterTableStoredProcedure]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AspNet_SqlCacheUnRegisterTableStoredProcedure] 
             @tableName NVARCHAR(450) 
         AS
         BEGIN

         BEGIN TRAN
         DECLARE @triggerName AS NVARCHAR(3000) 
         DECLARE @fullTriggerName AS NVARCHAR(3000)
         SET @triggerName = REPLACE(@tableName, '[', '__o__') 
         SET @triggerName = REPLACE(@triggerName, ']', '__c__') 
         SET @triggerName = @triggerName + '_AspNet_SqlCacheNotification_Trigger' 
         SET @fullTriggerName = 'dbo.[' + @triggerName + ']' 

         /* Remove the table-row from the notification table */ 
         IF EXISTS (SELECT name FROM sysobjects WITH (NOLOCK) WHERE name = 'AspNet_SqlCacheTablesForChangeNotification' AND type = 'U') 
             IF EXISTS (SELECT name FROM sysobjects WITH (TABLOCKX) WHERE name = 'AspNet_SqlCacheTablesForChangeNotification' AND type = 'U') 
             DELETE FROM dbo.AspNet_SqlCacheTablesForChangeNotification WHERE tableName = @tableName 

         /* Remove the trigger */ 
         IF EXISTS (SELECT name FROM sysobjects WITH (NOLOCK) WHERE name = @triggerName AND type = 'TR') 
             IF EXISTS (SELECT name FROM sysobjects WITH (TABLOCKX) WHERE name = @triggerName AND type = 'TR') 
             EXEC('DROP TRIGGER ' + @fullTriggerName) 

         COMMIT TRAN
         END
   






GO
/****** Object:  StoredProcedure [dbo].[AspNet_SqlCacheUpdateChangeIdStoredProcedure]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AspNet_SqlCacheUpdateChangeIdStoredProcedure] 
             @tableName NVARCHAR(450) 
         AS

         BEGIN 
             UPDATE dbo.AspNet_SqlCacheTablesForChangeNotification WITH (ROWLOCK) SET changeId = changeId + 1 
             WHERE tableName = @tableName
         END
   






GO
/****** Object:  StoredProcedure [dbo].[aspnet_UnRegisterSchemaVersion]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[aspnet_UnRegisterSchemaVersion]
    @Feature                   nvarchar(128),
    @CompatibleSchemaVersion   nvarchar(128)
AS
BEGIN
    DELETE FROM dbo.aspnet_SchemaVersions
        WHERE   Feature = LOWER(@Feature) AND @CompatibleSchemaVersion = CompatibleSchemaVersion
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Users_CreateUser]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[aspnet_Users_CreateUser]
    @ApplicationId    uniqueidentifier,
    @UserName         nvarchar(256),
    @IsUserAnonymous  bit,
    @LastActivityDate DATETIME,
    @UserId           uniqueidentifier OUTPUT
AS
BEGIN
    IF( @UserId IS NULL )
        SELECT @UserId = NEWID()
    ELSE
    BEGIN
        IF( EXISTS( SELECT UserId FROM dbo.aspnet_Users
                    WHERE @UserId = UserId ) )
            RETURN -1
    END

    INSERT dbo.aspnet_Users (ApplicationId, UserId, UserName, LoweredUserName, IsAnonymous, LastActivityDate)
    VALUES (@ApplicationId, @UserId, @UserName, LOWER(@UserName), @IsUserAnonymous, @LastActivityDate)

    RETURN 0
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_Users_DeleteUser]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_Users_DeleteUser]
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256),
    @TablesToDeleteFrom int,
    @NumTablesDeletedFrom int OUTPUT
AS
BEGIN
    DECLARE @UserId               uniqueidentifier
    SELECT  @UserId               = NULL
    SELECT  @NumTablesDeletedFrom = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
	SET @TranStarted = 0

    DECLARE @ErrorCode   int
    DECLARE @RowCount    int

    SET @ErrorCode = 0
    SET @RowCount  = 0

    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a
    WHERE   u.LoweredUserName       = LOWER(@UserName)
        AND u.ApplicationId         = a.ApplicationId
        AND LOWER(@ApplicationName) = a.LoweredApplicationName

    IF (@UserId IS NULL)
    BEGIN
        GOTO Cleanup
    END

    -- Delete from Membership table if (@TablesToDeleteFrom & 1) is set
    IF ((@TablesToDeleteFrom & 1) <> 0 AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_MembershipUsers') AND (type = 'V'))))
    BEGIN
        DELETE FROM dbo.aspnet_Membership WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
               @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_UsersInRoles table if (@TablesToDeleteFrom & 2) is set
    IF ((@TablesToDeleteFrom & 2) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_UsersInRoles') AND (type = 'V'))) )
    BEGIN
        DELETE FROM dbo.aspnet_UsersInRoles WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_Profile table if (@TablesToDeleteFrom & 4) is set
    IF ((@TablesToDeleteFrom & 4) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_Profiles') AND (type = 'V'))) )
    BEGIN
        DELETE FROM dbo.aspnet_Profile WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_PersonalizationPerUser table if (@TablesToDeleteFrom & 8) is set
    IF ((@TablesToDeleteFrom & 8) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_WebPartState_User') AND (type = 'V'))) )
    BEGIN
        DELETE FROM dbo.aspnet_PersonalizationPerUser WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_Users table if (@TablesToDeleteFrom & 1,2,4 & 8) are all set
    IF ((@TablesToDeleteFrom & 1) <> 0 AND
        (@TablesToDeleteFrom & 2) <> 0 AND
        (@TablesToDeleteFrom & 4) <> 0 AND
        (@TablesToDeleteFrom & 8) <> 0 AND
        (EXISTS (SELECT UserId FROM dbo.aspnet_Users WHERE @UserId = UserId)))
    BEGIN
        DELETE FROM dbo.aspnet_Users WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    IF( @TranStarted = 1 )
    BEGIN
	    SET @TranStarted = 0
	    COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:
    SET @NumTablesDeletedFrom = 0

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
	    ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_AddUsersToRoles]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_AddUsersToRoles]
	@ApplicationName  nvarchar(256),
	@UserNames		  nvarchar(4000),
	@RoleNames		  nvarchar(4000),
	@CurrentTimeUtc   datetime
AS
BEGIN
	DECLARE @AppId uniqueidentifier
	SELECT  @AppId = NULL
	SELECT  @AppId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
	IF (@AppId IS NULL)
		RETURN(2)
	DECLARE @TranStarted   bit
	SET @TranStarted = 0

	IF( @@TRANCOUNT = 0 )
	BEGIN
		BEGIN TRANSACTION
		SET @TranStarted = 1
	END

	DECLARE @tbNames	table(Name nvarchar(256) NOT NULL PRIMARY KEY)
	DECLARE @tbRoles	table(RoleId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @tbUsers	table(UserId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @Num		int
	DECLARE @Pos		int
	DECLARE @NextPos	int
	DECLARE @Name		nvarchar(256)

	SET @Num = 0
	SET @Pos = 1
	WHILE(@Pos <= LEN(@RoleNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N',', @RoleNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@RoleNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@RoleNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbRoles
	  SELECT RoleId
	  FROM   dbo.aspnet_Roles ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredRoleName AND ar.ApplicationId = @AppId

	IF (@@ROWCOUNT <> @Num)
	BEGIN
		SELECT TOP 1 Name
		FROM   @tbNames
		WHERE  LOWER(Name) NOT IN (SELECT ar.LoweredRoleName FROM dbo.aspnet_Roles ar,  @tbRoles r WHERE r.RoleId = ar.RoleId)
		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(2)
	END

	DELETE FROM @tbNames WHERE 1=1
	SET @Num = 0
	SET @Pos = 1

	WHILE(@Pos <= LEN(@UserNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N',', @UserNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@UserNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@UserNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbUsers
	  SELECT UserId
	  FROM   dbo.aspnet_Users ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredUserName AND ar.ApplicationId = @AppId

	IF (@@ROWCOUNT <> @Num)
	BEGIN
		DELETE FROM @tbNames
		WHERE LOWER(Name) IN (SELECT LoweredUserName FROM dbo.aspnet_Users au,  @tbUsers u WHERE au.UserId = u.UserId)

		INSERT dbo.aspnet_Users (ApplicationId, UserId, UserName, LoweredUserName, IsAnonymous, LastActivityDate)
		  SELECT @AppId, NEWID(), Name, LOWER(Name), 0, @CurrentTimeUtc
		  FROM   @tbNames

		INSERT INTO @tbUsers
		  SELECT  UserId
		  FROM	dbo.aspnet_Users au, @tbNames t
		  WHERE   LOWER(t.Name) = au.LoweredUserName AND au.ApplicationId = @AppId
	END

	IF (EXISTS (SELECT * FROM dbo.aspnet_UsersInRoles ur, @tbUsers tu, @tbRoles tr WHERE tu.UserId = ur.UserId AND tr.RoleId = ur.RoleId))
	BEGIN
		SELECT TOP 1 UserName, RoleName
		FROM		 dbo.aspnet_UsersInRoles ur, @tbUsers tu, @tbRoles tr, aspnet_Users u, aspnet_Roles r
		WHERE		u.UserId = tu.UserId AND r.RoleId = tr.RoleId AND tu.UserId = ur.UserId AND tr.RoleId = ur.RoleId

		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(3)
	END

	INSERT INTO dbo.aspnet_UsersInRoles (UserId, RoleId)
	SELECT UserId, RoleId
	FROM @tbUsers, @tbRoles

	IF( @TranStarted = 1 )
		COMMIT TRANSACTION
	RETURN(0)
END                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     





GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_FindUsersInRole]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_FindUsersInRole]
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256),
    @UserNameToMatch  nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)
     DECLARE @RoleId uniqueidentifier
     SELECT  @RoleId = NULL

     SELECT  @RoleId = RoleId
     FROM    dbo.aspnet_Roles
     WHERE   LOWER(@RoleName) = LoweredRoleName AND ApplicationId = @ApplicationId

     IF (@RoleId IS NULL)
         RETURN(1)

    SELECT u.UserName
    FROM   dbo.aspnet_Users u, dbo.aspnet_UsersInRoles ur
    WHERE  u.UserId = ur.UserId AND @RoleId = ur.RoleId AND u.ApplicationId = @ApplicationId AND LoweredUserName LIKE LOWER(@UserNameToMatch)
    ORDER BY u.UserName
    RETURN(0)
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_GetRolesForUser]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_GetRolesForUser]
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL

    SELECT  @UserId = UserId
    FROM    dbo.aspnet_Users
    WHERE   LoweredUserName = LOWER(@UserName) AND ApplicationId = @ApplicationId

    IF (@UserId IS NULL)
        RETURN(1)

    SELECT r.RoleName
    FROM   dbo.aspnet_Roles r, dbo.aspnet_UsersInRoles ur
    WHERE  r.RoleId = ur.RoleId AND r.ApplicationId = @ApplicationId AND ur.UserId = @UserId
    ORDER BY r.RoleName
    RETURN (0)
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_GetUsersInRoles]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_GetUsersInRoles]
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)
     DECLARE @RoleId uniqueidentifier
     SELECT  @RoleId = NULL

     SELECT  @RoleId = RoleId
     FROM    dbo.aspnet_Roles
     WHERE   LOWER(@RoleName) = LoweredRoleName AND ApplicationId = @ApplicationId

     IF (@RoleId IS NULL)
         RETURN(1)

    SELECT u.UserName
    FROM   dbo.aspnet_Users u, dbo.aspnet_UsersInRoles ur
    WHERE  u.UserId = ur.UserId AND @RoleId = ur.RoleId AND u.ApplicationId = @ApplicationId
    ORDER BY u.UserName
    RETURN(0)
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_IsUserInRole]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_IsUserInRole]
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(2)
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    DECLARE @RoleId uniqueidentifier
    SELECT  @RoleId = NULL

    SELECT  @UserId = UserId
    FROM    dbo.aspnet_Users
    WHERE   LoweredUserName = LOWER(@UserName) AND ApplicationId = @ApplicationId

    IF (@UserId IS NULL)
        RETURN(2)

    SELECT  @RoleId = RoleId
    FROM    dbo.aspnet_Roles
    WHERE   LoweredRoleName = LOWER(@RoleName) AND ApplicationId = @ApplicationId

    IF (@RoleId IS NULL)
        RETURN(3)

    IF (EXISTS( SELECT * FROM dbo.aspnet_UsersInRoles WHERE  UserId = @UserId AND RoleId = @RoleId))
        RETURN(1)
    ELSE
        RETURN(0)
END





GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_RemoveUsersFromRoles]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_RemoveUsersFromRoles]
	@ApplicationName  nvarchar(256),
	@UserNames		  nvarchar(4000),
	@RoleNames		  nvarchar(4000)
AS
BEGIN
	DECLARE @AppId uniqueidentifier
	SELECT  @AppId = NULL
	SELECT  @AppId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
	IF (@AppId IS NULL)
		RETURN(2)


	DECLARE @TranStarted   bit
	SET @TranStarted = 0

	IF( @@TRANCOUNT = 0 )
	BEGIN
		BEGIN TRANSACTION
		SET @TranStarted = 1
	END

	DECLARE @tbNames  table(Name nvarchar(256) NOT NULL PRIMARY KEY)
	DECLARE @tbRoles  table(RoleId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @tbUsers  table(UserId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @Num	  int
	DECLARE @Pos	  int
	DECLARE @NextPos  int
	DECLARE @Name	  nvarchar(256)
	DECLARE @CountAll int
	DECLARE @CountU	  int
	DECLARE @CountR	  int


	SET @Num = 0
	SET @Pos = 1
	WHILE(@Pos <= LEN(@RoleNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N',', @RoleNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@RoleNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@RoleNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbRoles
	  SELECT RoleId
	  FROM   dbo.aspnet_Roles ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredRoleName AND ar.ApplicationId = @AppId
	SELECT @CountR = @@ROWCOUNT

	IF (@CountR <> @Num)
	BEGIN
		SELECT TOP 1 N'', Name
		FROM   @tbNames
		WHERE  LOWER(Name) NOT IN (SELECT ar.LoweredRoleName FROM dbo.aspnet_Roles ar,  @tbRoles r WHERE r.RoleId = ar.RoleId)
		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(2)
	END


	DELETE FROM @tbNames WHERE 1=1
	SET @Num = 0
	SET @Pos = 1


	WHILE(@Pos <= LEN(@UserNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N',', @UserNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@UserNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@UserNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbUsers
	  SELECT UserId
	  FROM   dbo.aspnet_Users ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredUserName AND ar.ApplicationId = @AppId

	SELECT @CountU = @@ROWCOUNT
	IF (@CountU <> @Num)
	BEGIN
		SELECT TOP 1 Name, N''
		FROM   @tbNames
		WHERE  LOWER(Name) NOT IN (SELECT au.LoweredUserName FROM dbo.aspnet_Users au,  @tbUsers u WHERE u.UserId = au.UserId)

		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(1)
	END

	SELECT  @CountAll = COUNT(*)
	FROM	dbo.aspnet_UsersInRoles ur, @tbUsers u, @tbRoles r
	WHERE   ur.UserId = u.UserId AND ur.RoleId = r.RoleId

	IF (@CountAll <> @CountU * @CountR)
	BEGIN
		SELECT TOP 1 UserName, RoleName
		FROM		 @tbUsers tu, @tbRoles tr, dbo.aspnet_Users u, dbo.aspnet_Roles r
		WHERE		 u.UserId = tu.UserId AND r.RoleId = tr.RoleId AND
					 tu.UserId NOT IN (SELECT ur.UserId FROM dbo.aspnet_UsersInRoles ur WHERE ur.RoleId = tr.RoleId) AND
					 tr.RoleId NOT IN (SELECT ur.RoleId FROM dbo.aspnet_UsersInRoles ur WHERE ur.UserId = tu.UserId)
		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(3)
	END

	DELETE FROM dbo.aspnet_UsersInRoles
	WHERE UserId IN (SELECT UserId FROM @tbUsers)
	  AND RoleId IN (SELECT RoleId FROM @tbRoles)
	IF( @TranStarted = 1 )
		COMMIT TRANSACTION
	RETURN(0)
END
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        





GO
/****** Object:  StoredProcedure [dbo].[aspnet_WebEvent_LogEvent]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[aspnet_WebEvent_LogEvent]
        @EventId         char(32),
        @EventTimeUtc    datetime,
        @EventTime       datetime,
        @EventType       nvarchar(256),
        @EventSequence   decimal(19,0),
        @EventOccurrence decimal(19,0),
        @EventCode       int,
        @EventDetailCode int,
        @Message         nvarchar(1024),
        @ApplicationPath nvarchar(256),
        @ApplicationVirtualPath nvarchar(256),
        @MachineName    nvarchar(256),
        @RequestUrl      nvarchar(1024),
        @ExceptionType   nvarchar(256),
        @Details         ntext
AS
BEGIN
    INSERT
        dbo.aspnet_WebEvent_Events
        (
            EventId,
            EventTimeUtc,
            EventTime,
            EventType,
            EventSequence,
            EventOccurrence,
            EventCode,
            EventDetailCode,
            Message,
            ApplicationPath,
            ApplicationVirtualPath,
            MachineName,
            RequestUrl,
            ExceptionType,
            Details
        )
    VALUES
    (
        @EventId,
        @EventTimeUtc,
        @EventTime,
        @EventType,
        @EventSequence,
        @EventOccurrence,
        @EventCode,
        @EventDetailCode,
        @Message,
        @ApplicationPath,
        @ApplicationVirtualPath,
        @MachineName,
        @RequestUrl,
        @ExceptionType,
        @Details
    )
END





GO
/****** Object:  StoredProcedure [dbo].[cms_AccessRule_Create]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[cms_AccessRule_Create]
@RuleName NVARCHAR(100),
@ContentId VARCHAR(20),
@ContentType VARCHAR(20),
@Roles NVARCHAR(MAX),
@Users NVARCHAR(MAX),
@Permissions VARCHAR(MAX),
@CreatedBy VARCHAR(100)
AS
BEGIN

	 
	IF (NOT EXISTS(SELECT * FROM dbo.cms_sites s WITH(NOLOCK) 
			WHERE s.site_id = @ContentId and @ContentType = 'site')
		AND
		NOT EXISTS(SELECT * FROM dbo.cms_zone_groups s WITH(NOLOCK) 
			WHERE s.zone_group_id = @ContentId and @ContentType = 'zonegroup')
		AND
		NOT EXISTS(SELECT * FROM dbo.cms_zones s WITH(NOLOCK) 
			WHERE s.zone_id = @ContentId and @ContentType = 'zone')
		AND	
		NOT EXISTS(SELECT * FROM dbo.cms_articles s WITH(NOLOCK) 
			WHERE s.article_id = @ContentId and @ContentType = 'article')	
		)
	BEGIN
		RAISERROR('Content item was not found! Please try again later.', 16,1) 
		RETURN;
	END

	IF EXISTS(SELECT * FROM dbo.cms_AccessRules ar WITH(NOLOCK) 
		WHERE ar.RuleName = @RuleName)
	BEGIN
		RAISERROR('The Rule Name already defined. Please type another one.', 16,1) 
		RETURN;
	END

	IF EXISTS(SELECT * FROM dbo.cms_AccessRules ar WITH(NOLOCK) 
		WHERE ar.ContentId = @ContentId 
			AND ar.ContentType = @ContentId 
			AND ar.[Permissions] = @Permissions)
	BEGIN
		RAISERROR('Already exists such as Rule', 16,1)
		RETURN;
	END
	 
	INSERT INTO dbo.cms_AccessRules(
		RuleName,
		ContentId,
		ContentType,
		Roles,
		Users,
		[Permissions],
		CreatedBy)
	VALUES (
		@RuleName,
		@ContentId,
		@ContentType,
		@Roles,
		@Users,
		@Permissions,
		@CreatedBy)
END





GO
/****** Object:  StoredProcedure [dbo].[cms_AccessRule_Delete]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[cms_AccessRule_Delete]
@RuleId VARCHAR(20)
AS
BEGIN
	  DELETE FROM dbo.cms_AccessRules WHERE RuleId = @RuleId
END




GO
/****** Object:  StoredProcedure [dbo].[cms_AccessRule_Update]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[cms_AccessRule_Update]
@RuleId INT,
@RuleName NVARCHAR(100),
@ContentId VARCHAR(20),
@ContentType VARCHAR(20),
@Roles NVARCHAR(MAX),
@Users NVARCHAR(MAX),
@Permissions VARCHAR(MAX),
@UpdatedBy VARCHAR(100)
AS
BEGIN
 
	IF (NOT EXISTS(SELECT * FROM dbo.cms_sites s WITH(NOLOCK) 
			WHERE s.site_id = @ContentId and @ContentType = 'site')
		AND
		NOT EXISTS(SELECT * FROM dbo.cms_zone_groups s WITH(NOLOCK) 
			WHERE s.zone_group_id = @ContentId and @ContentType = 'zonegroup')
		AND
		NOT EXISTS(SELECT * FROM dbo.cms_zones s WITH(NOLOCK) 
			WHERE s.zone_id = @ContentId and @ContentType = 'zone')
		AND	
		NOT EXISTS(SELECT * FROM dbo.cms_articles s WITH(NOLOCK) 
			WHERE s.article_id = @ContentId and @ContentType = 'article')	
		)
	BEGIN
		RAISERROR('Content item was not found! Please try again later.', 16,1) 
		RETURN;
	END

	 

	IF EXISTS(SELECT * FROM dbo.cms_AccessRules ar WITH(NOLOCK) 
		WHERE ar.ContentId = @ContentId 
			AND ar.ContentType = @ContentId 
			AND ar.[Permissions] = @Permissions)
	BEGIN
		RAISERROR('Already exists such as Rule', 16,1)
		RETURN;
	END
	 
	UPDATE dbo.cms_AccessRules SET 
		RuleName = @RuleName,
		ContentId = @ContentId,
		ContentType = @ContentType,
		Roles = @Roles,
		Users = @Users,
		[Permissions] = @Permissions,
		Updated = GETDATE(),
		UpdatedBy = @UpdatedBy
	WHERE RuleId = @RuleId

END






GO
/****** Object:  StoredProcedure [dbo].[cms_AccessRules_GetAllRules]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[cms_AccessRules_GetAllRules]
@ContentType varchar(20)
AS
BEGIN
 
	IF (@ContentType <> '')
		SELECT * FROM dbo.vw_cms_AccessRules ar
			WHERE ar.ContentType = @ContentType
		ORDER BY ar.Created DESC
	ELSE
		SELECT * FROM dbo.vw_cms_AccessRules ar
		ORDER BY ar.Created DESC
	
	
END






GO
/****** Object:  StoredProcedure [dbo].[cms_AccessRules_HasPermission]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--cms_AccessRules_HasPermission 'Author,Ergo Homepage Kullanıcıları', 'create', 24, 'site'

--exec [cms_AccessRules_HasPermission] 'Author,Ergo Homepage Kullanıcıları', 'Create', '2', 'site'
--exec [cms_AccessRules_HasPermission] 'PowerUser', 'Edit', '2', 'site'
 
CREATE PROC [dbo].[cms_AccessRules_HasPermission]
@UserRoles NVARCHAR(MAX),
@Permission NVARCHAR(50),
@ContentId VARCHAR(20),
@ContentType VARCHAR(20)
AS
BEGIN

	SET NOCOUNT ON;
   
	DECLARE @site_id INT
	DECLARE @zone_group_id INT
	DECLARE @zone_id INT
	 
	IF  ( @ContentType = 'site' )
	BEGIN

		SELECT TOP 1 *
				FROM dbo.vw_cms_AccessRules ar
				WHERE ar.ContentType = @ContentType
					AND ar.ContentId = @ContentId
					AND ar.[Permissions] like '%'+ @Permission +'%'
					AND EXISTS ( SELECT String FROM dbo.Split(ar.Roles,',') WHERE String IN ( SELECT String FROM dbo.Split(@UserRoles,',') )  )
					 
		 
	END
	ELSE IF  (  @ContentType = 'zonegroup' )
	BEGIN

		SELECT @site_id = zg.site_id FROM dbo.cms_zone_groups zg WITH(NOLOCK) WHERE zg.zone_group_id = @ContentId
		 
		SELECT TOP 1 *  FROM dbo.vw_cms_AccessRules ar
				
				WHERE 
					(	(ar.ContentType = 'zonegroup' AND ar.ContentId = @ContentId) 
						OR 
						(ar.ContentType = 'site' AND ar.ContentId = @site_id)
					)
					AND ar.[Permissions] like '%'+ @Permission +'%'
					AND EXISTS ( SELECT String FROM dbo.Split(ar.Roles,',') WHERE String IN ( SELECT String FROM dbo.Split(@UserRoles,',') )  )
					 
		ORDER BY ar.AccessLevel DESC, ar.Created DESC


	END
	ELSE IF  (  @ContentType = 'zone' )
	BEGIN

		SELECT @zone_group_id = z.zone_group_id FROM dbo.cms_zones z WITH(NOLOCK) WHERE z.zone_id = @ContentId
		SELECT @site_id = zg.site_id FROM dbo.cms_zone_groups zg WITH(NOLOCK) WHERE zg.zone_group_id = @zone_group_id
		 
		SELECT TOP 1 *  FROM dbo.vw_cms_AccessRules ar
				
				WHERE 
					(	(ar.ContentType = 'zone' AND ar.ContentId = @ContentId) 
						OR 
						(ar.ContentType = 'site' AND ar.ContentId = @site_id)
						OR 
						(ar.ContentType = 'zonegroup' AND ar.ContentId = @zone_group_id)
					)
					AND ar.[Permissions] like '%'+ @Permission +'%'
					AND EXISTS ( SELECT String FROM dbo.Split(ar.Roles,',') WHERE String IN ( SELECT String FROM dbo.Split(@UserRoles,',') )  )
					 
		 
		ORDER BY ar.AccessLevel DESC, ar.Created DESC
	END
	ELSE IF  (  @ContentType = 'article' )
	BEGIN
	 
		CREATE TABLE #temp_zones ( zone_id INT )

		INSERT INTO #temp_zones SELECT az.zone_id FROM dbo.cms_article_zones az WITH(NOLOCK) WHERE az.article_id = @ContentId

		CREATE TABLE #temp_zone_groups ( zone_group_id INT )

		INSERT INTO #temp_zone_groups SELECT z.zone_group_id FROM dbo.cms_zones z WITH(NOLOCK) WHERE z.zone_id IN ( SELECT zone_id FROM #temp_zones )

		SELECT TOP 1 *  FROM dbo.vw_cms_AccessRules ar
				
				WHERE 
					(	
						( ar.ContentType = 'article' AND ar.ContentId = @ContentId) 
						OR 
						( ar.ContentType = 'zone' AND ar.ContentId IN (
								SELECT zone_id FROM #temp_zones WITH(NOLOCK)
						) ) 
						OR 
						( ar.ContentType = 'site' AND ar.ContentId IN (
							SELECT zg.site_id FROM dbo.cms_zone_groups zg WITH(NOLOCK) WHERE zg.zone_group_id IN ( 
									  SELECT zone_group_id FROM #temp_zone_groups WITH(NOLOCK)
							)
						) )
						OR 
						( ar.ContentType = 'zonegroup' AND ar.ContentId IN (
							 SELECT zone_group_id FROM #temp_zone_groups WITH(NOLOCK)
						) )
					)
					AND ar.[Permissions] like '%'+ @Permission +'%'
					
					AND EXISTS ( SELECT String FROM dbo.Split(ar.Roles,',') WHERE String IN ( SELECT String FROM dbo.Split(@UserRoles,',') )  )
					 
		 
		ORDER BY ar.AccessLevel DESC, ar.Created DESC

		DROP TABLE #temp_zones
		DROP TABLE #temp_zone_groups
	END


	 

END
 





GO
/****** Object:  StoredProcedure [dbo].[cms_Article_GetAllArticles]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[cms_Article_GetAllArticles]
	@Keyword NVARCHAR(100),
	@ClassificationId INT,
	@CreatedBy INT,
	@ArticleIds VARCHAR(200),
	@Alias VARCHAR(256),
	@LangId CHAR(2),
	@SiteId INT,
	@ZoneGroupId INT,
	@ZoneId INT,
	@Status INT,
	@StartDate DATETIME,
	@EndDate DATETIME,
	@ModificationStartDate DATETIME,
	@ModificationEndDate DATETIME,
	@ApprovedStartDate DATETIME,
	@ApprovedEndDate DATETIME,
	@RevFlag1 BIT,
	@RevFlag2 BIT,
	@RevFlag3 BIT,
	@RevFlag4 BIT,
	@RevFlag5 BIT,
	@IsRevision BIT
AS
BEGIN

	 SET NOCOUNT ON;
 
	 IF (@IsRevision = 1)
	 BEGIN

		SELECT TOP 1000  
			zr.az_order, 
			r.article_id, 
			r.rev_id, 
			r.rev_name, 
			r.created, 
			r.created_by, 
			r.rev_date, 
			r.revised_by, 
			r.menu_text, 
			r.headline, 
			r.summary, 
			r.startdate, 
			r.enddate, 
			r.approval_date, 
			r.approval_id, 
			r.revision_status, 
			r.[status], 
			a.clicks, 
			(SELECT TOP 1 cs.site_name + ' | ' + czg.zone_group_name + ' | ' + cz.zone_name 
				FROM dbo.cms_zones cz WITH (NOLOCK) 
					LEFT JOIN dbo.cms_zone_groups czg WITH (NOLOCK) ON czg.zone_group_id = cz.zone_group_id 
					LEFT JOIN dbo.cms_sites cs WITH (NOLOCK) ON cs.site_id = czg.site_id
						 WHERE cz.zone_id = zr.zone_id) 
			AS zone_name, 
			zr.zone_id, 
			a.locked_by, 
			a.locked, 
			p.UserName, 
			zr.az_alias
		FROM dbo.cms_article_revision r WITH (NOLOCK) 
			INNER JOIN dbo.cms_article_zones_revision zr WITH (NOLOCK) ON zr.rev_id = r.rev_id 
			LEFT JOIN dbo.cms_articles a WITH (NOLOCK) ON a.article_id = r.article_id 
			LEFT JOIN dbo.vw_aspnet_MembershipUsers p WITH (NOLOCK) ON a.locked_by = p.UserId 
				WHERE a.[status] <> 2 
					ORDER BY a.updated DESC, a.headline

	 END
	 ELSE
	 BEGIN
		 SELECT TOP 1000 
			zr.az_order, 
			r.article_id, 
			r.rev_id, 
			r.rev_name, 
			r.created, 
			r.created_by, 
			r.rev_date, 
			r.revised_by, 
			r.menu_text, 
			r.headline, 
			r.summary, 
			r.startdate, 
			r.enddate, 
			r.approval_date, 
			r.approval_id, 
			r.revision_status, 
			r.[status], 
			a.clicks, 
            (SELECT TOP 1 cs.site_name + ' | ' + czg.zone_group_name + ' | ' + cz.zone_name 
				FROM dbo.cms_zones cz WITH (NOLOCK) 
				LEFT JOIN dbo.cms_zone_groups czg WITH (NOLOCK) ON czg.zone_group_id = cz.zone_group_id 
				LEFT JOIN dbo.cms_sites cs WITH (NOLOCK) ON cs.site_id = czg.site_id 
					WHERE cz.zone_id = zr.zone_id) 
			AS zone_name, 
			zr.zone_id, 
			a.locked_by, 
			a.locked, 
			p.UserName, 
			zr.az_alias 
        FROM dbo.vArticlesLiveRevisions r WITH (NOLOCK) 
			INNER JOIN dbo.cms_article_zones_revision zr WITH (NOLOCK) ON zr.rev_id = r.rev_id    
			LEFT JOIN dbo.cms_articles a WITH (NOLOCK) ON a.article_id = r.article_id 
			LEFT JOIN dbo.vw_aspnet_MembershipUsers p WITH (NOLOCK)  ON a.locked_by = p.UserId   
			WHERE a.[status] <> 2  
				ORDER BY  a.updated DESC, a.headline
	 END
END







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_bulk_update_article_details]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_bulk_update_article_details]
	@article_id int,
	@orderno int,
	@headline nvarchar(350),
	@summary nvarchar(3000),
	@article_1 ntext,
	@publisher_id uniqueidentifier
AS

set nocount on
declare @rev_id int

if not exists(select * from dbo.cms_articles with (nolock) where article_id = @article_id and status <> 2)
begin
	select 'NOT_FOUND' as rCode
	return
end

begin transaction


-- Create new revision with article details and new updated fields
insert into dbo.cms_article_revision
(revised_by, revision_status, approval_date, approval_id, created_by, article_id, clsf_id, status, startdate, enddate, orderno, lang_id, navigation_display, navigation_zone_id, menu_text, headline, summary, keywords, article_type, article_type_detail, article_1, article_2, article_3, article_4, article_5, custom_1, custom_2, custom_3, custom_4, custom_5, custom_6, custom_7, custom_8, custom_9, custom_10, flag_1, flag_2, flag_3, flag_4, flag_5, date_1, date_2, rev_flag_1, rev_flag_2, rev_flag_3, rev_flag_4, rev_flag_5, cl_1, cl_2, cl_3, cl_4, cl_5, custom_body)
	select @publisher_id, 'L', getDate(), @publisher_id, @publisher_id, @article_id, clsf_id, status, startdate, enddate, orderno, lang_id, navigation_display, navigation_zone_id, @headline, @headline, @summary, keywords, article_type, article_type_detail, @article_1, article_2, article_3, article_4, article_5, custom_1, custom_2, custom_3, custom_4, custom_5, custom_6, custom_7, custom_8, custom_9, custom_10, flag_1, flag_2, flag_3, flag_4, flag_5, date_1, date_2, 0, 0, 0, 0, 0, cl_1, cl_2, cl_3, cl_4, cl_5, custom_body
	from dbo.cms_articles with (nolock)
	where article_id = @article_id

if(@@error <> 0) goto RollbackAndReturn
set @rev_id = scope_identity()
if(@@error <> 0) goto RollbackAndReturn


-- create zone revision with same zone on current article
insert into dbo.cms_article_zones_revision
(rev_id, article_id, zone_id, az_order)
	select @rev_id, @article_id, zone_id, az_order
	from dbo.cms_article_zones with (nolock)
	where article_id = @article_id

if(@@error <> 0) goto RollbackAndReturn


-- create language relation revision with same on current article
insert into dbo.cms_article_language_relation_revision
(rev_id, article_id, related_zone_id, related_article_id)
	select @rev_id, @article_id, related_zone_id, related_article_id
	from dbo.cms_article_language_relation with (nolock)
	where article_id = @article_id

if(@@error <> 0) goto RollbackAndReturn



-- create article relation revision with same on current article
insert into dbo.cms_article_relation_revision
(rev_id, article_id, related_zone_id, related_article_id)
	select @rev_id, @article_id, related_zone_id, related_article_id
	from dbo.cms_article_relation with (nolock)
	where article_id = @article_id

if(@@error <> 0) goto RollbackAndReturn



-- update original articles table with new values
update dbo.cms_articles
set
	menu_text = @headline,
	headline = @headline,
	summary = @summary,
--	orderno = @orderno,
	article_1 = @article_1
where article_id = @article_id

if(@@error <> 0) goto RollbackAndReturn

-- do NOT touch article zones, language relations, related articles..






set nocount off

commit transaction

select 'OK' as rCode
return

RollbackAndReturn:
rollback transaction
select @@ERROR as rCode
return







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_article]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_delete_article]
	@article_id int,
	@rev_id int,
	@publisher_id uniqueidentifier,
	@publisher_level int
AS

declare @headline nvarchar(350)
declare @new_rev_id int
	
set nocount on

if exists(select * from dbo.cms_articles with (nolock) where article_id = @article_id and status <> 2 )
begin
	--article found and active
	
	--check relations

	select @headline = headline from dbo.cms_articles with (nolock) where article_id = @article_id

	--article is ready for delete.. Save last revision for delete approval
	insert into dbo.cms_article_revision
	(article_id, rev_name, headline, startdate, status, revised_by, created_by)
	values
	(@article_id, 'Delete Request', @headline, getDate(), 2, @publisher_id, @publisher_id)

	set @new_rev_id = scope_identity()

	--copy current revision zones to this new revision zones
	insert into dbo.cms_article_zones_revision
	(rev_id, article_id, zone_id)
	select @new_rev_id, article_id, zone_id
	from dbo.cms_article_zones_revision
	where rev_id = @rev_id and article_id = @article_id


	select '0' as rCode, '' as found_name, @new_rev_id as rev_id
	return
end
else
begin
	--article not found or already deleted
	select '1' as rCode, '' as found_name, '0' as rev_id
	return
end



set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_article_cache]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_delete_article_cache]
	@article_id int
AS
 begin
	
		delete from dbo.cms_article_cache where article_id = @article_id
	
 end







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_article_files_revision_file]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_admin_delete_article_files_revision_file]
   @af_rf_id bigint,
   @rev_id bigint,
   @article_id int
as
	
set nocount on
  
if exists(select * from dbo.cms_article_files_revision_files with (nolock) where  af_rf_id= @af_rf_id and rev_id = @rev_id and article_id = @article_id)
begin
	--file found 
	
	delete from dbo.cms_article_files_revision_files
	where af_rf_id = @af_rf_id 	
	select 'DF' as rCode 
	
		
	return




end
else
begin
	--css not found or already deleted
	select 'NF' as rCode
	return
end



set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_article_language_relations_with_revision]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_delete_article_language_relations_with_revision]
    @rev_id bigint,
    @article_id int
as
set nocount on;
 


--and lr.rev_id = @rev_id
 
	delete from dbo.cms_language_relations_revision
where lr_id in (select lr.lr_id from dbo.cms_language_relations_revision lr with(nolock) where lr.article_id = @article_id)
 
--delete from dbo.cms_language_relations_revision
--where rev_id = @rev_id

-- delete reverse







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_article_relations_with_revision]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_delete_article_relations_with_revision]
    @rev_id bigint, 
    @article_id int
as

delete from dbo.cms_article_relation_revision
where   rev_id = @rev_id and article_id = @article_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_article_zones_with_revision]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_delete_article_zones_with_revision]
    @rev_id bigint, 
    @article_id int
as

delete from dbo.cms_article_zones_revision
where   rev_id = @rev_id and article_id = @article_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_breadcrumb]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_admin_delete_breadcrumb]
	@breadcrumb_id int
AS

set nocount on

if not exists(select * from dbo.cms_breadcrumbs with (nolock) where breadcrumb_id = @breadcrumb_id)
begin
	Select 'NOTEXIST' as rCode
end

else
begin
	delete from dbo.cms_breadcrumbs where breadcrumb_id = @breadcrumb_id

	Select 'DELETED' as rCode
end

set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_cc]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_delete_cc]
    @cc_id int, 
    @publisher_id uniqueidentifier,
    @publisher_level int
as
	
set nocount on

if @publisher_level = 100
begin
	if exists(select * from dbo.cms_custom_content with (nolock) where cc_id = @cc_id )
	begin
		--custom content is ready to delete..
		delete from dbo.cms_custom_content
		where cc_id = @cc_id
			
		select '0' as rCode, '' as found_name
		return
	end
	else
	begin
		--custom content not found or already deleted
		select '1' as rCode, '' as found_name 
		return
	end

end
else
begin
	--no access
	select '2' as rCode, '' as found_name 
	return
end


set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_classification]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_delete_classification]
    @classification_id int, 
    @publisher_id uniqueidentifier,
    @publisher_level int
as
	
set nocount on

if exists(select * from dbo.cms_classifications with (nolock) where classification_id = @classification_id)
begin
	--classification found and active
	


	if exists(select * from dbo.cms_articles with (nolock) where clsf_id = @classification_id and status <> 2)
	begin
		--classification used on articles..
		select '2' as rCode, '' as found_name
		return
	end



	-- delete classification
	delete from dbo.cms_classifications
	where classification_id = @classification_id

	select '0' as rCode, '' as found_name
	return
end
else
begin
	--classification not found or already deleted
	select '1' as rCode, '' as found_name 
	return
end



set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_classification_combo_values]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

  CREATE procedure [dbo].[cms_asp_admin_delete_classification_combo_values]

    @classification_id int, 

    @column_no tinyint

AS

IF @column_no=0

	BEGIN

		Delete From dbo.cms_classification_combo_values where classification_id=@classification_id

	END

ELSE

	BEGIN

		Delete From dbo.cms_classification_combo_values where classification_id=@classification_id AND column_no=@column_no

	END







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_config_parameter]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_delete_config_parameter]
    @config_id int, 
    @ws varchar(10),
    @publisher_id uniqueidentifier,
    @publisher_level int
as
	
set nocount on

if @publisher_level = 100
begin
	if exists(select * from dbo.cms_config with (nolock) where config_id = @config_id and isDefault = 'N')
	begin
		--parameter found
		delete from dbo.cms_config
		where config_id = @config_id
	
		select '0' as rCode, '' as found_name
		return
	end
	else
	begin
		--parameter not found or not allowed for delete
		select '1' as rCode, '' as found_name 
		return
	end
end
else
begin
	--not an administrator
	select '2' as rCode, '' as found_name 
	return
end



set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_css]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_delete_css]
    @css_id int, 
    @publisher_id uniqueidentifier,
    @publisher_level int
as
	
set nocount on

if exists(select * from dbo.cms_css with (nolock) where css_id = @css_id and css_status = 'A' )
begin
	--css found and active
	
	if exists(select * from dbo.cms_sites with (nolock) where css_id = @css_id)
	begin
		--css used on sites..
		select '2' as rCode, site_name as found_name
		from dbo.cms_sites with (nolock)
		where css_id = @css_id
		order by site_name
		
		return
	end

	if exists(select * from dbo.cms_zone_groups with (nolock) where css_id = @css_id)
	begin
		--css used on zone groups..
		select '3' as rCode, zone_group_name as found_name
		from dbo.cms_zone_groups with (nolock)
		where css_id = @css_id
		order by zone_group_name
		
		return
	end

	if exists(select * from dbo.cms_zones with (nolock) where css_id = @css_id and zone_status <> 'D')
	begin
		--css used on zones..
		select '4' as rCode, zone_name as found_name
		from dbo.cms_zones with (nolock)
		where css_id = @css_id and zone_status <> 'D'
		order by zone_name
		
		return
	end


	--css is ready for delete.. Save last revision and mark as delete
	INSERT INTO dbo.cms_css_revisions
	(css_id, css_code, css_fix, css_rel_text, css_type_text, publisher_id)
		select css_id, css_code, css_fix, css_rel_text, css_type_text, publisher_id
		from dbo.cms_css
		where css_id = @css_id

	-- update css
	update dbo.cms_css
	set
		css_status = 'D',
		updated = getDate()
	where
		css_id = @css_id

	select '0' as rCode, '' as found_name
	return
end
else
begin
	--css not found or already deleted
	select '1' as rCode, '' as found_name 
	return
end



set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_domain]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_delete_domain]
    @domain_id int, 
    @publisher_id uniqueidentifier,
    @publisher_level int
as
	
set nocount on

if @publisher_level < 100
begin
	select '2' as rCode, '' as found_name
	return
end

if exists(select * from dbo.cms_domains with (nolock) where domain_id = @domain_id and domain_status = 'A' )
begin
	--domain found and active
	
	if not exists(select * from dbo.cms_domains with (nolock) where domain_status = 'A' and domain_id <> @domain_id)
	begin
		--no other domains left..
		select '3' as rCode, '' as found_name
		return
	end


	-- update domain for delete
	update dbo.cms_domains
	set
		domain_status = 'D',
		updated = getDate()
	where
		domain_id = @domain_id

	select '0' as rCode, '' as found_name
	return
end
else
begin
	--domain not found or already deleted
	select '1' as rCode, '' as found_name 
	return
end



set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_file_type]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_admin_delete_file_type]
	@type_id int
AS
	
set nocount on

if exists(select * from dbo.cms_article_files with (nolock) where file_type_id = @type_id )
begin
	--file type used on article files
	
	--file types used on article files..
	--select '1' as rCode, zone_name as found_name
	--from dbo.cms_article_files with (nolock)
	--where file_type_id = @type_id'
	--order by zone_name
	
	--return	

	select '1' as rCode
	return
	
end
else
begin
	--file type is ready for delete..
	delete from dbo.cms_file_types
	where type_id = @type_id

	select '0' as rCode
	return
end



set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_hidden_value]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_delete_hidden_value]
    @hidden_id int, 
    @publisher_id uniqueidentifier,
    @publisher_level int
as
	
set nocount on

if @publisher_level < 100
begin
	select '2' as rCode, '' as found_name
	return
end

if exists(select * from dbo.cms_hidden_values with (nolock) where hidden_id = @hidden_id)
begin
	--hidden found and active
	
	delete from dbo.cms_hidden_values
	where hidden_id = @hidden_id

	select '0' as rCode, '' as found_name
	return
end
else
begin
	--hidden not found or already deleted
	select '1' as rCode, '' as found_name 
	return
end



set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_language]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_delete_language]
    @lang_id char(2), 
    @publisher_id uniqueidentifier,
    @publisher_level int
as
	
set nocount on

if @publisher_level = 100
begin
	if exists(select * from dbo.cms_languages with (nolock) where lang_id = @lang_id )
	begin
		--language found
		
		--if exists(select * from dbo.cms_article_language_relation with (nolock) where related_lang_id = @lang_id)
		--begin
			--lang used on relations..
		--	select '2' as rCode, '' as found_name
		--	return
		--end
	
		if exists(select * from dbo.cms_articles with (nolock) where lang_id = @lang_id)
		begin
			--language used on articles..
			select '3' as rCode, '' as found_name
			
			return
		end
	
		if not exists(select * from dbo.cms_languages with (nolock) where lang_id <> @lang_id)
		begin
			--no any other language
			select '4' as rCode, '' as found_name
			
			return
		end
	
	
		--language is ready to delete..
		delete from dbo.cms_languages
		where lang_id = @lang_id
	
		select '0' as rCode, '' as found_name
		return
	end
	else
	begin
		--language not found or already deleted
		select '1' as rCode, '' as found_name 
		return
	end

end
else
begin
	--no access
	select '5' as rCode, '' as found_name 
	return
end


set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_plugin]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_delete_plugin]
    @plugin_id int, 
    @publisher_id uniqueidentifier,
    @publisher_level int
as
	
set nocount on

declare @pstring varchar(30)
set @pstring = '%##plugin_' + cast(@plugin_id as varchar(10) ) + '%'


if exists(select * from dbo.cms_plugins with (nolock) where plugin_id = @plugin_id and plugin_status < 2 )
begin
	--plugin found and active
	
	if exists(select * from dbo.cms_templates with (nolock) where template_html like @pstring)
	begin
		--plugin used on templates..
		select '2' as rCode, template_name as found_name
		from dbo.cms_templates with (nolock)
		where template_html like @pstring
		order by template_name
		
		return
	end

	if exists(select * from dbo.cms_zones with (nolock) where zone_status <> 'D' and (article_1 like @pstring or article_2 like @pstring or article_3 like @pstring or article_4 like @pstring or article_5 like @pstring) )
	begin
		--plugin used on zones..
		select '3' as rCode, zone_name as found_name
		from dbo.cms_zones with (nolock)
		where zone_status <> 'D' and (article_1 like @pstring or article_2 like @pstring or article_3 like @pstring or article_4 like @pstring or article_5 like @pstring)
		order by zone_name
		
		return
	end


	if exists(select * from dbo.cms_articles with (nolock) where status <> 2 and (article_1 like @pstring or article_2 like @pstring or article_3 like @pstring or article_4 like @pstring or article_5 like @pstring) )
	begin
		--plugin used on articles..
		select '3' as rCode, headline as found_name
		from dbo.cms_articles with (nolock)
		where status <> 2 and (article_1 like @pstring or article_2 like @pstring or article_3 like @pstring or article_4 like @pstring or article_5 like @pstring)
		order by headline
		
		return
	end




	-- update plugin for delete
	update dbo.cms_plugins
	set
		plugin_status = 2,
		updated_by = @publisher_id,
		updated = getDate()
	where
		plugin_id = @plugin_id

	select '0' as rCode, '' as found_name
	return
end
else
begin
	--plugin not found or already deleted
	select '1' as rCode, '' as found_name 
	return
end



set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_portlet]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_delete_portlet]
    @portlet_id int, 
    @publisher_id uniqueidentifier,
    @publisher_level int
as
	
set nocount on

if @publisher_level = 100
begin
	if exists(select * from dbo.cms_portlets with (nolock) where portlet_id = @portlet_id and portlet_status < 2 )
	begin
		--portlet found
	
		--portlet is ready to delete..
		update dbo.cms_portlets
		set
			portlet_status = 2,
			updated = getDate(),
			updated_by = @publisher_id
		where portlet_id = @portlet_id
	
		select '0' as rCode, '' as found_name
		return
	end
	else
	begin
		--portlet not found or already deleted
		select '1' as rCode, '' as found_name 
		return
	end

end
else
begin
	--no access
	select '2' as rCode, '' as found_name 
	return
end


set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_redirection]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_delete_redirection]
    @redirect_id int, 
    @publisher_id uniqueidentifier,
    @publisher_level int
as
	
set nocount on

if exists(select * from dbo.cms_redirects with (nolock) where redirect_id = @redirect_id)
begin
	--redirect found and active
	delete from dbo.cms_redirects
	where
		redirect_id = @redirect_id

	select '0' as rCode, '' as found_name
	return
end
else
begin
	--redirection not found or already deleted
	select '1' as rCode, '' as found_name 
	return
end



set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_relation]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_delete_relation]
    @ID int, 
    @Publisher uniqueidentifier,
    @PublisherLevel int
as
	
set nocount on

if @PublisherLevel < 100
begin
	select '2' as rCode, '' as found_name
	return
end

if exists(select * from dbo.cms_page_redirection with (nolock) where ID = @ID  )
begin
	
	if not exists(select * from dbo.cms_page_redirection with (nolock) where  ID <> @ID)
	begin
		
		select '3' as rCode, '' as found_name
		return
	end


	-- update domain for delete
	delete from dbo.cms_page_redirection where ID=@ID


	select '0' as rCode, '' as found_name
	return
end
else
begin

	select '1' as rCode, '' as found_name 
	return
end



set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_rss_channel]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_delete_rss_channel]
    @channel_id int, 
    @publisher_id uniqueidentifier,
    @publisher_level int
as
	
set nocount on

if exists(select * from dbo.cms_rss_channels with (nolock) where channel_id = @channel_id and channel_status <> 'D' )
begin
	--rss channel found and active
	
	if exists(select * from dbo.cms_rss_content with (nolock) where channel_id = @channel_id)
	begin
		--channel has content..
		select '2' as rCode, '' as found_name
		return
	end

	-- update it for delete
	update dbo.cms_rss_channels
	set
		channel_status = 'D',
		updated = getDate()
	where
		channel_id = @channel_id

	select '0' as rCode, '' as found_name
	return
end
else
begin
	--channel not found or already deleted
	select '1' as rCode, '' as found_name 
	return
end



set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_rss_content]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_delete_rss_content]
	@channel_id int
as

delete from dbo.cms_rss_content
where channel_id = @channel_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_site]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_delete_site]
    @site_id int, 
    @publisher_id uniqueidentifier,
    @publisher_level int
as

set nocount on

if exists(select * from dbo.cms_sites with (nolock) where site_id = @site_id)
begin
	--site found and active
	if not exists(select * from dbo.cms_sites with (nolock) where site_id <> @site_id)
	begin
		-- there is no any other site?
		select '2' as rCode, '' as found_name
		return
	end


	if exists(select * from dbo.cms_zone_groups with (nolock) where site_id = @site_id)
	begin
		--site used on zone groups..
		select '3' as rCode, zone_group_name as found_name
		from dbo.cms_zone_groups with (nolock)
		where site_id = @site_id
		order by zone_group_name
		
		return
	end

	if exists(select * from dbo.cms_sitemaps with (nolock) where included_sites like CAST(@site_id as varchar(10)) OR included_sites like CAST(@site_id as varchar(10)) + ',%' OR included_sites like '%,' + CAST(@site_id as varchar(10)) + ',%')
	begin
		--site used on site maps..
		select '5' as rCode, domain_alias as found_name
		from dbo.cms_sitemaps with (nolock)
		where  included_sites like CAST(@site_id as varchar(10)) OR included_sites like CAST(@site_id as varchar(10)) + ',%' OR included_sites like '%,' + CAST(@site_id as varchar(10)) + ',%'
		order by domain_alias
		
		return
	end


	--site is ready for delete..
	delete from dbo.cms_sites
	where site_id = @site_id

	select '0' as rCode, '' as found_name
	return
end
else
begin
	--css not found or already deleted
	select '1' as rCode, '' as found_name 
	return
end



set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_sitemap]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_delete_sitemap]
	@smap_id int
AS
set nocount on

if exists(select * from dbo.cms_sitemaps with (nolock) WHERE smap_id = @smap_id AND status=2)
begin
	Select 'NOK' as rCode --Sitemap creating in progress, can not delete
	return
end
else
begin
	Delete FROM dbo.cms_sitemaps WHERE smap_id = @smap_id
	Select 'OK' as rCode
	return
end

set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_stf_template]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_delete_stf_template]
    @stft_id int, 
    @publisher_id uniqueidentifier,
    @publisher_level int
as

set nocount on

declare @stft_string as varchar(100)
set @stft_string = '%##stf_link_' + cast(@stft_id as varchar(10)) + '##%'

if exists(select * from dbo.cms_stf_templates with (nolock) where stft_id = @stft_id and stft_status = 'A' )
begin
	--template found and active
	
	if exists(select * from dbo.cms_templates with (nolock) where template_html like @stft_string and template_status <> 'D')
	begin
		--stf template used on templates.
		select '2' as rCode, template_name as found_name
		from dbo.cms_templates with (nolock)
		where template_html like @stft_string and template_status <> 'D'
		order by template_name
		
		return
	end


	if exists(select * from dbo.cms_zones with (nolock) where zone_status <> 'D' and (article_1 like @stft_string or article_2 like @stft_string or article_3 like @stft_string or article_4 like @stft_string or article_5 like @stft_string))
	begin
		--stf template used on zones..
		select '3' as rCode, zone_name as found_name
		from dbo.cms_zones with (nolock)
		where zone_status <> 'D' and (article_1 like @stft_string or article_2 like @stft_string or article_3 like @stft_string or article_4 like @stft_string or article_5 like @stft_string)
		order by zone_name
		
		return
	end


	if exists(select * from dbo.cms_articles with (nolock) where status <> 2 and (article_1 like @stft_string or article_2 like @stft_string or article_3 like @stft_string or article_4 like @stft_string or article_5 like @stft_string))
	begin
		--stf template used on articles..
		select '4' as rCode, headline + ' (' + cast(article_id as varchar(10)) + ')' as found_name
		from dbo.cms_articles with (nolock)
		where status <> 2 and (article_1 like @stft_string or article_2 like @stft_string or article_3 like @stft_string or article_4 like @stft_string or article_5 like @stft_string)
		order by headline
		
		return
	end


	-- update stf template
	update dbo.cms_stf_templates
	set
		stft_status = 'D',
		updated = getDate(),
		updated_by = @publisher_id
	where
		stft_id = @stft_id

	select '0' as rCode, '' as found_name
	return
end
else
begin
	--template not found or already deleted
	select '1' as rCode, '' as found_name 
	return
end



set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_structure_group]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_admin_delete_structure_group]
	@group_id int,
	@group_type tinyint
AS

set nocount on

if not exists(select * from dbo.cms_structure_groups with (nolock) where group_id = @group_id)
begin
	Select 'NOTEXIST' as rCode
end

else if @group_type = 1 AND exists(select * from dbo.cms_css with (nolock) where group_id = @group_id)
begin
	Select 'USED' as rCode
end

else if @group_type = 2 AND exists(select * from dbo.cms_sites with (nolock) where group_id = @group_id)
begin
	Select 'USED' as rCode
end

else if @group_type = 3 AND exists(select * from dbo.cms_templates with (nolock) where group_id = @group_id)
begin
	Select 'USED' as rCode
end

else if @group_type = 4 AND exists(select * from dbo.cms_portlets with (nolock) where group_id = @group_id)
begin
	Select 'USED' as rCode
end

else if @group_type = 5 AND exists(select * from dbo.cms_plugins with (nolock) where group_id = @group_id)
begin
	Select 'USED' as rCode
end

else if @group_type = 6 AND exists(select * from dbo.cms_xml with (nolock) where group_id = @group_id)
begin
	Select 'USED' as rCode
end

else if @group_type = 7 AND exists(select * from dbo.cms_rss_channels with (nolock) where group_id = @group_id)
begin
	Select 'USED' as rCode
end

else if @group_type = 8 AND exists(select * from dbo.cms_classifications with (nolock) where group_id = @group_id)
begin
	Select 'USED' as rCode
end

else if @group_type = 9 AND exists(select * from dbo.cms_file_types with (nolock) where group_id = @group_id)
begin
	Select 'USED' as rCode
end

else if @group_type = 10 AND exists(select * from dbo.cms_redirects with (nolock) where group_id = @group_id)
begin
	Select 'USED' as rCode
end

else if @group_type = 11 AND exists(select * from dbo.cms_custom_content with (nolock) where group_id = @group_id)
begin
	Select 'USED' as rCode
end

else
begin
	delete from dbo.cms_structure_groups where group_id = @group_id

	Select 'DELETED' as rCode
end

set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_template]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_delete_template]
    @template_id int, 
    @publisher_id uniqueidentifier,
    @publisher_level int
as

set nocount on

if exists(select * from dbo.cms_templates with (nolock) where template_id = @template_id and template_status = 'A' )
begin
	--template found and active
	
	if exists(select * from dbo.cms_sites with (nolock) where template_id = @template_id)
	begin
		--template used on sites..
		select '2' as rCode, site_name as found_name
		from dbo.cms_sites with (nolock)
		where template_id = @template_id
		order by site_name
		
		return
	end

	if exists(select * from dbo.cms_zone_groups with (nolock) where template_id = @template_id)
	begin
		--template used on zone groups..
		select '3' as rCode, zone_group_name as found_name
		from dbo.cms_zone_groups with (nolock)
		where template_id = @template_id
		order by zone_group_name
		
		return
	end

	if exists(select * from dbo.cms_zones with (nolock) where template_id = @template_id and zone_status <> 'D')
	begin
		--template used on zones..
		select '4' as rCode, zone_name as found_name
		from dbo.cms_zones with (nolock)
		where template_id = @template_id and zone_status <> 'D'
		order by zone_name
		
		return
	end


	--template is ready for delete.. Save last revision and mark as delete
	INSERT INTO dbo.cms_template_revisions
	(template_id, template_html, publisher_id)
		select template_id, template_html, publisher_id
		from dbo.cms_templates
		where template_id = @template_id

	-- update template
	update dbo.cms_templates
	set
		template_status = 'D',
		updated = getDate()
	where
		template_id = @template_id

	select '0' as rCode, '' as found_name
	return
end
else
begin
	--template not found or already deleted
	select '1' as rCode, '' as found_name 
	return
end



set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_xml]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_delete_xml]
    @xml_id int, 
    @publisher_id uniqueidentifier,
    @publisher_level int
as
	
set nocount on

if exists(select * from dbo.cms_xml with (nolock) where xml_id = @xml_id)
begin
	--xml found and active

	-- delete xml
	delete from dbo.cms_xml
	where xml_id = @xml_id

	select '0' as rCode, '' as found_name
	return
end
else
begin
	--classification not found or already deleted
	select '1' as rCode, '' as found_name 
	return
end



set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_zone]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[cms_asp_admin_delete_zone]



	@zone_id int,



	@approve_level int,



	@publisher_id uniqueidentifier,



	@publisher_level int,



	@cio char(1) --Zone Check-In Check-Out parameter



AS







declare @rev_id int




set nocount on







if exists(select * from dbo.cms_zones with (nolock) where zone_id = @zone_id and zone_status <> 'D' )



begin



	--zone found and active



	



	--check articles



	if exists(select * from dbo.cms_article_zones caz with (nolock) LEFT JOIN dbo.cms_articles ca with (nolock) ON ca.article_id = caz.article_id where caz.zone_id = @zone_id and ca.status <> 2)



	begin



		select '1' as aStat, '(' + cast(ca.article_id as varchar(20)) + ') ' + ca.headline as found_name, '0' as rev_id



		from dbo.cms_article_zones caz with (nolock)



			LEFT JOIN dbo.cms_articles ca with (nolock)



			ON ca.article_id = caz.article_id and ca.status <> 2



		where caz.zone_id = @zone_id



		order by ca.headline







		return



	end







	-- check menu relations



	if exists(select * from dbo.cms_articles with (nolock) where navigation_zone_id = @zone_id and navigation_display in (2,3) and status <> 2 )



	begin



		select '2' as aStat, '(' + cast(ca.article_id as varchar(20)) + ') ' + ca.headline as found_name, '0' as rev_id



		from dbo.cms_articles ca with (nolock)



		where ca.navigation_zone_id = @zone_id and ca.navigation_display in (2,3) and ca.status <> 2



		order by ca.headline







		return



	end







	--zone is ready for delete.. Save last revision for delete approval

declare @zone_group_id int

declare @zone_name varchar(max)

declare @created_by uniqueidentifier


	select @zone_group_id=zone_group_id from cms_zone_revision where zone_id=@zone_id

	select @zone_name=zone_name from cms_zone_revision where zone_id=@zone_id

	select @created_by=created_by from cms_zone_revision where zone_id=@zone_id

	insert into dbo.cms_zone_revision

	(zone_id, zone_status, revised_by,zone_name,zone_group_id,created_by)



	values



	(@zone_id, 'D', @publisher_id,@zone_name,@zone_group_id,@created_by)







	set @rev_id = scope_identity()







	if @publisher_level < 100



	begin



		select '0' as aStat, '' as found_name, @rev_id as rev_id



		return



	end



	else



	begin



		exec dbo.cms_asp_approval_approve_zone_revision @rev_id, @approve_level, @publisher_id, @publisher_level, @cio



		return



	end



end



else



begin



	--zone not found or already deleted



	select '4' as aStat, '' as found_name, '0' as rev_id



	return



end















set nocount off






GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_delete_zonegroup]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_delete_zonegroup]
    @zone_group_id int, 
    @publisher_id uniqueidentifier,
    @publisher_level int
as

set nocount on

declare @site_id int
--get current site_id
select @site_id = site_id from dbo.cms_zone_groups where zone_group_id = @zone_group_id


if exists(select * from dbo.cms_zone_groups with (nolock) where zone_group_id = @zone_group_id )
begin
	--zone group found and active
	
/*
	if not exists(select * from dbo.cms_zone_groups with (nolock) where zone_group_id <> @zone_group_id and site_id = @site_id)
	begin
		-- there is no any other zone group for this site?
		select '2' as rCode, '' as found_name
		return
	end
*/

	if exists(select * from dbo.cms_zones with (nolock) where zone_group_id = @zone_group_id and zone_status <> 'D')
	begin
		--zone group used on zones..
		select '3' as rCode, zone_name as found_name
		from dbo.cms_zones with (nolock)
		where zone_group_id = @zone_group_id and zone_status <> 'D'
		order by zone_name
		
		return
	end


	--zone group is ready for delete..
	delete from dbo.cms_zone_groups
	where zone_group_id = @zone_group_id

	select '0' as rCode, '' as found_name
	return
end
else
begin
	--zone group not found or already deleted
	select '1' as rCode, '' as found_name 
	return
end



set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_discard_article_files_revision]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_admin_discard_article_files_revision]
	@article_id int,
	@rev_id bigint
AS


set nocount on





-- check current status
if exists(select * from dbo.cms_article_files_revision with (nolock) where rev_id = @rev_id and revision_status in ('N','A','W') )
begin
	-- can be changed
	update dbo.cms_article_files_revision
	set
		revision_status = 'X'
	where
		rev_id = @rev_id

	

	select 'OK' as rCode
end
else
begin
	select 'NOK' as rCode
end



set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_edit_article_lock]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_admin_edit_article_lock]
	@article_id int,
	@publisher_id uniqueidentifier,
	@process char(1),
	@pubLevel int,
	@article_or_revision char(1)
AS

set nocount on

Declare @Locked datetime
Declare @Locked_by uniqueidentifier
Declare @Locked_by_name nvarchar(100)

if @article_or_revision = 'R'
begin
	Select @article_id = article_id FROM dbo.cms_article_revision with (nolock) where rev_id = @article_id
end

If @process = 'L'
Begin
	
	Select @Locked = locked, @Locked_by = locked_by From dbo.cms_articles with (nolock) Where article_id = @article_id
	
	If @Locked_by = '' Or @Locked_by is null Or @Locked_by = @publisher_id
	Begin
		Update dbo.cms_articles Set locked = getdate(), locked_by = @publisher_id Where article_id = @article_id
		Select 'Locked' AS rStatus
		return
	End
	
	Else
	Begin
		Select @Locked_by_name = publisher_name From dbo.vw_aspnet_MembershipUsers with (nolock) Where UserId = @Locked_by
		Select 'NA' AS rStatus, @Locked_by_name AS rLockedBy, @Locked AS rLocked
		return
	End
End

Else
Begin
	If @process = 'U'
	Begin
		Select @Locked = locked, @Locked_by = locked_by From dbo.cms_articles with (nolock) Where article_id = @article_id

		If @Locked_by = @publisher_id OR @pubLevel>=100
		Begin
			Update dbo.cms_articles Set locked = null, locked_by = null Where article_id = @article_id
			Select 'Unlocked' AS rStatus
			return
		End
	
		Else
		Begin
			Select 'NA' as rStatus
			return
		End
	End

	Else
	Begin
		Select @Locked = locked, @Locked_by = locked_by From dbo.cms_articles with (nolock) Where article_id = @article_id

		If @Locked_by = @publisher_id
		Begin
			Select 'Unlocked' as rStatus
			return
		End

		Else
		Begin
			Select @Locked_by_name = publisher_name From dbo.vw_aspnet_MembershipUsers with (nolock) Where UserId = @Locked_by
			Select 'Locked' AS rStatus, @Locked_by_name AS rLockedBy, @Locked AS rLocked
		End
	End
End

set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_edit_zone_lock]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_admin_edit_zone_lock]
	@zone_id int,
	@publisher_id uniqueidentifier,
	@process char(1),
	@pubLevel int,
	@zone_or_revision char(1)
AS

set nocount on

Declare @Locked datetime
Declare @Locked_by uniqueidentifier
Declare @Locked_by_name nvarchar(100)

if @zone_or_revision = 'R'
begin
	Select @zone_id = zone_id FROM dbo.cms_zone_revision with (nolock) where rev_id = @zone_id
end

If @process = 'L'
Begin
	
	Select @Locked = locked, @Locked_by = locked_by From dbo.cms_zones with (nolock) Where zone_id = @zone_id
	
	If @Locked_by = '' Or @Locked_by is null Or @Locked_by = @publisher_id
	Begin
		Update dbo.cms_zones Set locked = getdate(), locked_by = @publisher_id Where zone_id = @zone_id
		Select 'Locked' AS rStatus
		return
	End
	
	Else
	Begin
		Select @Locked_by_name = publisher_name From dbo.vw_aspnet_MembershipUsers with (nolock) Where UserId = @Locked_by
		Select 'NA' AS rStatus, @Locked_by_name AS rLockedBy, @Locked AS rLocked
		return
	End
End

Else
Begin
	If @process = 'U'
	Begin
		Select @Locked = locked, @Locked_by = locked_by From dbo.cms_zones with (nolock) Where zone_id = @zone_id

		If @Locked_by = @publisher_id OR @pubLevel>=100
		Begin
			Update dbo.cms_zones Set locked = null, locked_by = null Where zone_id = @zone_id
			Select 'Unlocked' AS rStatus
			return
		End
	
		Else
		Begin
			Select 'NA' as rStatus
			return
		End
	End

	Else
	Begin
		Select @Locked = locked, @Locked_by = locked_by From dbo.cms_zones with (nolock) Where zone_id = @zone_id

		If @Locked_by = @publisher_id
		Begin
			Select 'Unlocked' as rStatus
			return
		End

		Else
		Begin
			Select @Locked_by_name = UserName From dbo.vw_aspnet_MembershipUsers with (nolock) Where UserId = @Locked_by
			Select 'Locked' AS rStatus, @Locked_by_name AS rLockedBy, @Locked AS rLocked
		End
	End
End

set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_insert_article_language_relations_with_revision]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_insert_article_language_relations_with_revision]
    @rev_id bigint, 
    @zone_id int,
    @article_id int, 
    @related_zone_id int,
    @related_article_id int,
    @pool_id bigint = null
as
declare @lr_id bigint

select @lr_id = isnull(max(lr.lr_id), 0) + 1 from dbo.cms_language_relations_revision lr with(nolock) -- default

declare @rev_id_reverse bigint
 
EXEC @rev_id_reverse = [dbo].[cms_asp_admin_select_article_last_revision] @article_id = @related_article_id, @return_rev_id =  1

if(@pool_id is null)
begin

if (exists(select * from dbo.cms_language_relations_revision lr with(nolock) where lr.article_id = @article_id and lr.zone_id = @zone_id )) 
	begin
		select @lr_id = lr.lr_id from dbo.cms_language_relations_revision lr with(nolock) where lr.article_id = @article_id and lr.zone_id = @zone_id  
		goto addToLanguagePool;
	end

if (exists(select * from dbo.cms_language_relations_revision lr with(nolock) where lr.article_id = @related_article_id and lr.zone_id = @related_zone_id))
	begin
		select @lr_id = lr.lr_id from dbo.cms_language_relations_revision lr with(nolock) where  lr.article_id = @related_article_id and lr.zone_id = @related_zone_id 
		goto addToLanguagePool;
	end

end
else if (@pool_id > 0)
begin
	set @lr_id = @pool_id
end


addToLanguagePool:
if not exists(select * from dbo.cms_language_relations_revision where article_id = @article_id and zone_id = @zone_id and lr_id = @lr_id )
begin
	insert into dbo.cms_language_relations_revision
	(lr_id, rev_id, zone_id, article_id)
	values
	(@lr_id, @rev_id, @zone_id, @article_id)
end

 
if not exists(select * from dbo.cms_language_relations_revision where article_id = @related_article_id and zone_id = @related_zone_id and lr_id = @lr_id )
begin
  
	insert into dbo.cms_language_relations_revision
	(lr_id, rev_id, zone_id, article_id)
	values
	(@lr_id, @rev_id_reverse, @related_zone_id, @related_article_id)
end







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_insert_article_relations_with_revision]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_insert_article_relations_with_revision]
    @rev_id bigint, 
    @article_id int, 
    @related_zone_id int,
    @related_article_id int
as

if not exists(select * from dbo.cms_article_relation_revision where rev_id = @rev_id and article_id = @article_id and related_zone_id = @related_zone_id and related_article_id = @related_article_id)
begin
	insert into dbo.cms_article_relation_revision
	(rev_id, article_id, related_zone_id, related_article_id)
	values
	(@rev_id, @article_id, @related_zone_id, @related_article_id)
end







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_insert_article_zones_with_revision]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_insert_article_zones_with_revision]
    @rev_id bigint, 
    @article_id int, 
    @zone_id int,
    @az_order int,
    @az_alias nvarchar(300)
as

if not exists(select * from dbo.cms_article_zones_revision where rev_id = @rev_id and article_id = @article_id and zone_id = @zone_id)
begin
	insert into dbo.cms_article_zones_revision
	(rev_id, article_id, zone_id, az_order, az_alias)
	values
	(@rev_id, @article_id, @zone_id, @az_order, @az_alias)
end

-- Also check article zones table for this article.. if not exists create them too
if not exists(select * from dbo.cms_article_zones where article_id = @article_id)
begin
	insert into dbo.cms_article_zones
	(article_id, zone_id, az_order, az_alias)
	values
	(@article_id, @zone_id, @az_order, @az_alias)
end







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_insert_cache_server]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_insert_cache_server]
	@server_ip varchar(100)
AS


if not exists(select * from dbo.cms_cache_update where server_ip = @server_ip)
begin
	insert into dbo.cms_cache_update
	(server_ip)
	values
	(@server_ip)
end







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_insert_fop_failure_log]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_insert_fop_failure_log]
	@op_action varchar(20), 
	@source_path nvarchar(300), 
	@dest_path nvarchar(300), 
	@file_name varchar(300), 
	@summary nvarchar(300) 
as

insert into dbo.cms_fop_failure_log 
        (op_action, source_path, dest_path, [file_name], summary)
values
        (@op_action, @source_path, @dest_path, @file_name, @summary)







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_insert_log]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_insert_log]
	@publisher_id uniqueidentifier,
	@note_id bigint,
	@event_name nvarchar(50),
	@note nvarchar(500),
	@ip varchar(15),
	@idtype varchar(50)
as

declare @event_id int
declare @title nvarchar(100)

set @title = ''

if @idtype = 'arev_id'
begin
	select @title = headline from dbo.cms_article_revision with (nolock) where rev_id = @note_id
end

if @idtype = 'zrev_id'
begin
	select @title = zone_name from dbo.cms_zone_revision with (nolock) where rev_id = @note_id
end

if @idtype = 'zg_id' or @idtype = 'zone_group_id'
begin
	select @title = zone_group_name from dbo.cms_zone_groups with (nolock) where zone_group_id = @note_id
end

if @idtype = 'zone_id'
begin
	select @title = zone_name from dbo.cms_zones with (nolock) where zone_id = @note_id
end

if @idtype = 'publisher_id'
begin
	select @title = UserName from dbo.vw_aspnet_MembershipUsers with (nolock) where UserId = @note_id
end

if @idtype = 'css_id'
begin
	select @title = css_name from dbo.cms_css with (nolock) where css_id = @note_id
end

if @idtype = 'template_id'
begin
	select @title = template_name from dbo.cms_templates with (nolock) where template_id = @note_id
end

if @idtype = 'site_id'
begin
	select @title = site_name from dbo.cms_sites with (nolock) where site_id = @note_id
end

if @idtype = 'config_id'
begin
	select @title = config_name from dbo.cms_config with (nolock) where config_id = @note_id
end

if @idtype = 'article_id'
begin
	select @title = headline from dbo.cms_articles with (nolock) where article_id = @note_id
end

if @idtype = 'portlet_id'
begin
	select @title = portlet_name from dbo.cms_portlets with (nolock) where portlet_id = @note_id
end

if @idtype = 'plugin_id'
begin
	select @title = plugin_name from dbo.cms_plugins with (nolock) where plugin_id = @note_id
end

if @idtype = 'stft_id'
begin
	select @title = stft_name from dbo.cms_stf_templates with (nolock) where stft_id = @note_id
end

if @idtype = 'classification_id'
begin
	select @title = classification_name from dbo.cms_classifications with (nolock) where classification_id = @note_id
end

if @idtype = 'xml_id'
begin
	select @title = xml_name from dbo.cms_xml with (nolock) where xml_id = @note_id
end

if @idtype = 'redirect_id'
begin
	select @title = redirect_alias from dbo.cms_redirects with (nolock) where redirect_id = @note_id
end

if @idtype = 'channel_id'
begin
	select @title = channel_name from dbo.cms_rss_channels with (nolock) where channel_id = @note_id
end

if @idtype = 'cc_id'
begin
	select @title = cc_name from dbo.cms_custom_content with (nolock) where cc_id = @note_id
end

if @idtype = 'domain_id'
begin
	select @title = domain_names from dbo.cms_domains with (nolock) where domain_id = @note_id
end

if @idtype = 'type_id'
begin
	select @title = [type_name] from dbo.cms_file_types with (nolock) where type_id = @note_id
end


if @idtype = 'chistory_id'
begin
	select @title = c.css_name from dbo.cms_css_revisions cr with (nolock), dbo.cms_css c with (nolock) where c.css_id = cr.css_id and cr.history_id = @note_id
end

if @idtype = 'thistory_id'
begin
	select @title = t.template_name from dbo.cms_template_revisions tr with (nolock), dbo.cms_templates t with (nolock) where t.template_id = tr.template_id and tr.history_id = @note_id
end


select @event_id = event_id 
from dbo.cms_publisher_log_events with (nolock)
where event_name = @event_name

if @event_id is null
begin
	insert into dbo.cms_publisher_log_events
	(event_name)
	values
	(@event_name)

	set @event_id = scope_identity()
end

insert into dbo.cms_publisher_logs
(publisher_id, event_id, note_id, note, ip, title)
values
(@publisher_id, @event_id, @note_id, @note, @ip, @title)







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_insert_rss_content]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_insert_rss_content]
	@channel_id int,
	@sgz_id int,
	@sgz_type char(1),
	@sgz_exclude char(1),
	@publisher_id uniqueidentifier
as


if not exists (select * from dbo.cms_rss_content with (nolock) where channel_id = @channel_id and sgz_id = @sgz_id and sgz_type = @sgz_type)
begin

	insert into dbo.cms_rss_content
	(channel_id, sgz_id, sgz_type, sgz_exclude, created_by)
	values
	(@channel_id, @sgz_id, @sgz_type, @sgz_exclude, @publisher_id)

end







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_process_subzone_article_relation]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_process_subzone_article_relation]
	@zone_id int,
	@article_id int
as

declare @rev_id int

set nocount on

if not exists(select * from dbo.cms_articles with (nolock) where article_id = @article_id and status = 1)
begin
	select 'ARTICLE_NOT_FOUND' as aStat
	return
end

if not exists(select * from dbo.cms_zones with (nolock) where zone_id = @zone_id and zone_status = 'A')
begin
	select 'ZONE_NOT_FOUND' as aStat
	return
end


-- get proper revision.. must be 1 record..
select top 1 @rev_id = rev_id
from dbo.cms_article_revision with (nolock)
where article_id = @article_id
order by created desc

if @rev_id is null
begin
	select 'REVISION_NOT_FOUND' as aStat
	return
end


-- update revision first..
update dbo.cms_article_revision
set
	navigation_display = 3,
	navigation_zone_id = @zone_id
where
	rev_id = @rev_id

-- then update article
update dbo.cms_articles
set
	navigation_display = 3,
	navigation_zone_id = @zone_id
where
	article_id = @article_id

select 'OK' as aStat

set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_article_files_last_revision]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_admin_select_article_files_last_revision]
	@article_id int
AS

if exists(select * from dbo.cms_article_files_revision r with (nolock) where r.article_id = @article_id  and revision_status = 'L')
begin

	select top 1 r.rev_id
	from dbo.cms_article_files_revision r with (nolock)
	where r.article_id = @article_id and revision_status = 'L'
	order by r.rev_date desc

end
else
begin

	select top 1 r.rev_id
	from dbo.cms_article_files_revision r with (nolock)
	where r.article_id = @article_id
	order by revision_status asc, r.rev_date desc

end







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_article_files_last_revision_forced]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_admin_select_article_files_last_revision_forced]
	@article_id int
AS

select top 1 r.rev_id, r.revision_status
from dbo.cms_article_files_revision r with (nolock)
where r.article_id = @article_id
order by r.rev_date desc







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_article_files_revision_file_details]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_select_article_files_revision_file_details]
	@af_rf_id bigint,
	@rev_id bigint,
	@article_id int
AS


select file_type_id, file_title, file_order, file_comment, file_name_1, file_name_2, file_name_3, file_name_4, file_name_5, file_name_6, file_name_7, file_name_8, file_name_9, file_name_10, afr.revision_status
	
from dbo.cms_article_files_revision_files r with (nolock) left join dbo.cms_article_files_revision as afr with (nolock) on r.rev_id = afr.rev_id
	
where r.af_rf_id= @af_rf_id and r.rev_id = @rev_id and r.article_id = @article_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_article_files_revision_files]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_admin_select_article_files_revision_files]
	@rev_id int
	
AS


select p.*, afr.revision_status,ft.[type_name], ft.type_id
from dbo.cms_article_files_revision_files as p with (nolock)
	left join dbo.cms_article_files_revision as afr with (nolock) on p.rev_id = afr.rev_id 
	left join dbo.cms_file_types as ft with (nolock) on p.file_type_id = ft.type_id
where p.rev_id = @rev_id
order by p.file_order asc, p.file_title asc, p.af_rf_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_article_files_revision_list]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_admin_select_article_files_revision_list]
	@article_id int	
AS


select  top 50
	 r.rev_id, r.rev_date, r.revision_status, r.approval_date, 
	(select UserName from dbo.vw_aspnet_MembershipUsers with (nolock) where UserId = r.revised_by ) as revised_name,
	(select UserName from dbo.vw_aspnet_MembershipUsers with (nolock) where UserId = r.approval_id ) as approval_name
from dbo.cms_article_files_revision r with (nolock)
where r.article_id = @article_id
order by r.rev_date desc







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_article_last_revision]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_select_article_last_revision]
	@article_id int,
	@return_rev_id bit = 0
AS

declare @rev_id_out bigint

if exists(select * from dbo.cms_article_revision r with (nolock) where r.article_id = @article_id and status <> 2 and revision_status in ('L','E'))
begin

	select top 1 @rev_id_out = r.rev_id
	from dbo.cms_article_revision r with (nolock)
	where r.article_id = @article_id and status <> 2 and revision_status in ('L','E')
	order by revision_status desc, r.rev_date desc

end
else
begin

	select top 1 @rev_id_out = r.rev_id
	from dbo.cms_article_revision r with (nolock)
	where r.article_id = @article_id and status <> 2
	order by revision_status asc, r.rev_date desc

end

if(@return_rev_id = 0) 
	select @rev_id_out as 'rev_id' 
else
	return @rev_id_out







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_article_revision_details]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_select_article_revision_details]
	@rev_id int
AS


select
	r.rev_id, r.rev_date, r.revision_status, r.revised_by, r.approval_date, r.approval_id,
	a.status as current_status,
	r.article_id, r.clsf_id,  r.status, r.startdate, r.enddate, r.orderno, r.lang_id, r.headline, r.summary, r.keywords, r.article_type, r.article_type_detail,
	r.article_1, r.article_2, r.article_3, r.article_4, r.article_5,
	r.custom_1, r.custom_2, r.custom_3, r.custom_4, r.custom_5,  r.custom_6, r.custom_7, r.custom_8, r.custom_9, r.custom_10,
	r.custom_11, r.custom_12, r.custom_13, r.custom_14, r.custom_15,  r.custom_16, r.custom_17, r.custom_18, r.custom_19, r.custom_20,
	r.flag_1, r.flag_2, r.flag_3, r.flag_4, r.flag_5,
	r.date_1, r.date_2, r.date_3, r.date_4, r.date_5,
	r.rev_flag_1, r.rev_flag_2, r.rev_flag_3, r.rev_flag_4, r.rev_flag_5,
	r.cl_1, r.cl_2, r.cl_3, r.cl_4, r.cl_5, r.custom_body,
	a.publisher_id, a.created, a.updated,
	r.rev_name, r.rev_note, r.navigation_display, r.navigation_zone_id, r.menu_text,
	(select UserName from dbo.vw_aspnet_MembershipUsers with (nolock) where UserId = r.revised_by) as revised_name,
	(select UserName from dbo.vw_aspnet_MembershipUsers with (nolock) where UserId = r.approval_id) as approval_name,
	(select UserName from dbo.vw_aspnet_MembershipUsers with (nolock) where UserId = a.publisher_id) as publisher_name,
	r.meta_description,
	r.content_1_editor_type, r.content_2_editor_type, r.content_3_editor_type, r.content_4_editor_type, r.content_5_editor_type,
	r.omniture_code,
	r.custom_setting
from dbo.cms_article_revision r with (nolock)
	left join dbo.cms_articles a with (nolock) on a.article_id = r.article_id
where r.rev_id = @rev_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_article_revision_list]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_select_article_revision_list]
	@article_id int
AS


select top 50
	r.rev_id, r.rev_date, r.revision_status, r.approval_date, r.status, r.rev_name,
	(select UserName from dbo.vw_aspnet_MembershipUsers with (nolock) where UserId = r.revised_by ) as revised_name,
	(select UserName from dbo.vw_aspnet_MembershipUsers with (nolock) where UserId = r.approval_id ) as approval_name
from dbo.cms_article_revision r with (nolock)
where r.article_id = @article_id and revision_status <> 'X'
order by r.rev_date desc







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_article_zone_names]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_select_article_zone_names]
	@zone_id int,
	@article_id int
as

declare @out_zone nvarchar(4000)
declare @out_article nvarchar(500)
set @out_zone = ''
set @out_article = ''

if @zone_id > 0
begin
	select @out_zone = s.site_name + ' / ' + zg.zone_group_name + ' / ' + z.zone_name
	from dbo.cms_zones z with (nolock)
		inner join dbo.cms_zone_groups zg with (nolock)
			on zg.zone_group_id = z.zone_group_id
		inner join dbo.cms_sites s with (nolock)
			on s.site_id = zg.site_id
	where z.zone_id = @zone_id

	if @article_id > -1
	begin
		select @out_article = ' / ' + headline
		from dbo.cms_articles with (nolock)
		where article_id = @article_id
	end

end
else
begin
	if @article_id > -1
	begin
		select @out_article = headline
		from dbo.cms_articles with (nolock)
		where article_id = @article_id
	end
end
select @out_zone + @out_article as out_name
return







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_articles_max_order_by_zone]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_select_articles_max_order_by_zone]
	@zone_id int
as

/*
select top 1 orderno
from dbo.vArticlesZones
where zone_id = @zone_id
order by orderno desc
*/

select top 1 az_order as orderno
from dbo.cms_article_zones with (nolock)
where zone_id = @zone_id
order by az_order desc







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_az_check]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_admin_select_az_check]
	@article_id int,
	@zone_id int,
	@az_alias nvarchar(300)
AS

Set nocount on

declare @domain_id int
select @domain_id = d.domain_id from dbo.cms_article_zones_revision as azr with (nolock) 
		left outer join dbo.cms_article_revision as ar with (nolock)on ar.article_id = azr.article_id AND azr.rev_id = ar.rev_id 
		left outer join dbo.cms_zones as z with (nolock) on z.zone_id = azr.zone_id
		left outer join dbo.cms_zone_groups as zg with (nolock) on zg.zone_group_id = z.zone_group_id
		left outer join dbo.cms_sites as s with (nolock) on s.site_id = zg.site_id 
		left outer join dbo.cms_domains as d with (nolock) on d.domain_id = s.domain_id
where z.zone_id = @zone_id


Select 'REV' as rType, s.site_name + ' / ' + zg.zone_group_name + ' / ' + z.zone_name + ' / ' + ar.headline as rDescription, ar.rev_id as rID
	from dbo.cms_article_zones_revision as azr with (nolock) 
		left outer join dbo.cms_article_revision as ar with (nolock) on ar.article_id = azr.article_id AND azr.rev_id = ar.rev_id
		left outer join dbo.cms_zones as z with (nolock) on z.zone_id = azr.zone_id
		left outer join dbo.cms_zone_groups as zg with (nolock) on zg.zone_group_id = z.zone_group_id
		left outer join dbo.cms_sites as s with (nolock) on s.site_id = zg.site_id
		left outer join dbo.cms_domains as d with (nolock) on d.domain_id = s.domain_id
	where
		( 
		--(azr.article_id <> @article_id AND azr.zone_id <> @zone_id AND azr.az_alias = @az_alias and s.site_id = @site_id) OR 
	   --(azr.article_id = @article_id AND azr.zone_id <> @zone_id AND azr.az_alias = @az_alias and s.site_id = @site_id)  
	  --   ( azr.article_id <> @article_id AND azr.zone_id <> @zone_id and  azr.az_alias = @az_alias and d.domain_id = @domain_id) or
			--(azr.article_id = @article_id AND azr.zone_id <> @zone_id AND azr.az_alias = @az_alias and d.domain_id = @domain_id)  

			( azr.article_id <> @article_id and azr.az_alias = @az_alias and d.domain_id = @domain_id)  
		  )
		AND ar.revision_status in ('N','L','W') 
union all
Select 'URE' as rType, s.site_name + ' / ' + zg.zone_group_name + ' / ' + z.zone_name + ' / ' + a.headline as rDescription, re.redirect_id as rID
	from dbo.cms_redirects as re with (nolock) 
		left outer join dbo.cms_articles as a with (nolock) on a.article_id = re.article_id
		left outer join dbo.cms_zones as z with (nolock) on z.zone_id = re.zone_id
		left outer join dbo.cms_zone_groups as zg with (nolock) on zg.zone_group_id = z.zone_group_id
		left outer join dbo.cms_sites as s with (nolock) on s.site_id = zg.site_id
		left outer join dbo.cms_domains as d with (nolock) on d.domain_id = s.domain_id
	where
		re.redirect_alias = @az_alias and d.domain_id = @domain_id
Set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_cached_articles]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_select_cached_articles]
as

select a.article_id, z.zone_id, s.site_name + ' / ' + zg.zone_group_name + ' / ' + z.zone_name + ' / ' + a.headline as out_name
from dbo.cms_article_cache c with (nolock)
	inner join dbo.cms_articles a with (nolock)
		on a.article_id = c.article_id
	inner join dbo.cms_zones z with (nolock)
		on z.zone_id = c.zone_id
	inner join dbo.cms_zone_groups zg with (nolock)
		on zg.zone_group_id = z.zone_group_id
	inner join dbo.cms_sites s with (nolock)
		on s.site_id = zg.site_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_count_wfar]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_admin_select_count_wfar]
	@rev_id int
AS

--this sp count the waiting for approval request for an article

Select count(rev_id) as wfar_count
from dbo.cms_article_revision with (nolock) 
where article_id = (select article_id from dbo.cms_article_revision with (nolock) where rev_id=@rev_id) AND revision_status = 'W'







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_css_revisions]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_select_css_revisions]
    @css_id int
AS

SELECT  top 50
        cr.history_id, 
        cr.css_id, 
        cr.publisher_id, 
        cr.created,
        p.UserName
FROM dbo.cms_css_revisions cr with (nolock)
	LEFT JOIN dbo.vw_aspnet_MembershipUsers p with (nolock) ON cr.publisher_id = p.UserId
WHERE   cr.css_id = @css_id
ORDER BY cr.created DESC







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_file_type_details]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_select_file_type_details] @type_id int
AS 
    select  type_id,
            [type_name],
            type_alias,
            file1_name,
            file2_name,
            file3_name,
            file4_name,
            file5_name,
            file6_name,
            file7_name,
            file8_name,
            file9_name,
            file10_name,
            file1_extension,
            file2_extension,
            file3_extension,
            file4_extension,
            file5_extension,
            file6_extension,
            file7_extension,
            file8_extension,
            file9_extension,
            file10_extension,
            file1_wh,
            file2_wh,
            file3_wh,
            file4_wh,
            file5_wh,
            file6_wh,
            file7_wh,
            file8_wh,
            file9_wh,
            file10_wh,
            file1_size,
            file2_size,
            file3_size,
            file4_size,
            file5_size,
            file6_size,
            file7_size,
            file8_size,
            file9_size,
            file10_size,
            created,
            updated,
            group_id,
            structure_description
    from    dbo.cms_file_types with ( nolock )
    where   type_id = @type_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_fop_failures]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_select_fop_failures]
	@dest_path as nvarchar(300)
as

select log_id, op_action, source_path, dest_path, [file_name]
from dbo.cms_fop_failure_log with (nolock)
where
	( (op_status in ('N','R')) or (op_status = 'T' and processed < DATEADD(hh, -1, getDate()) ) )
	and retry_count < 3
	and dest_path like @dest_path
order by created







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_navigation_zone_count]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_admin_select_navigation_zone_count]
	@navigation_zone_id int,
	@article_id int
AS

set nocount on

SELECT article_id FROM dbo.cms_articles WHERE navigation_zone_id = @navigation_zone_id AND status <> 2 AND article_id <> @article_id

set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_redirection_detail]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_select_redirection_detail]
	@redirect_id int
AS


select r.redirect_id, r.redirect_alias, r.article_id, r.zone_id, s.site_name + ' / ' + zg.zone_group_name + ' / ' + z.zone_name + ' / ' + a.headline as out_name, r.group_id, r.structure_description, ISNULL(r.permanent_redirection, 0) AS permanent_redirection
from dbo.cms_redirects r with (nolock)
	left join dbo.cms_articles a with (nolock) on a.article_id = r.article_id
	left join dbo.cms_zones z with (nolock) on z.zone_id = r.zone_id
	left join dbo.cms_zone_groups zg with (nolock) on z.zone_group_id = zg.zone_group_id
	left join dbo.cms_sites s with (nolock) on s.site_id = zg.site_id
where r.redirect_id = @redirect_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_required_file_columns]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_admin_select_required_file_columns]
	@article_id int,
	@file_type_id varchar(50)
AS

If @article_id>0
BEGIN
	Select c.file_title_required_cb as file_title_required, c.file_description_required_cb AS file_description_required
	From dbo.cms_articles as a with (nolock)
	Left Outer Join dbo.cms_classifications as c with (nolock) on a.clsf_id=c.classification_id
	Where a.article_id = @article_id AND (c.required_file_types=@file_type_id OR c.required_file_types like '%,'+@file_type_id OR c.required_file_types like '%,'+@file_type_id+',%' OR c.required_file_types like @file_type_id+',%')
END

Else
BEGIN
	Select c.classification_name
	From dbo.cms_articles as a with (nolock)
	Left Outer Join dbo.cms_classifications as c with (nolock) on a.clsf_id=c.classification_id
	Where c.required_file_types=@file_type_id OR c.required_file_types like '%,'+@file_type_id OR c.required_file_types like '%,'+@file_type_id+',%' OR c.required_file_types like @file_type_id+',%'
END







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_required_file_count]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_admin_select_required_file_count]
	@rev_id int
AS

declare @clsf_id int
declare @article_id int
declare @file_required bit
declare @required_file_types varchar(8000)
declare @file_count int
declare @returner varchar(8000)
declare @delimiter varchar(1)
declare @delimiter_index INT, @file_type_id INT

Set @delimiter = ','
Set @returner = ''
select @clsf_id=clsf_id, @article_id=article_id from dbo.cms_article_revision with (nolock) where rev_id=@rev_id

If @clsf_id<>0
BEGIN
	select @file_required=file_required_cb, @required_file_types=required_file_types from dbo.cms_classifications with (nolock)  where classification_id=@clsf_id
	
	If @file_required=1
	BEGIN
		SET @delimiter_index = charindex (@delimiter, @required_file_types)
		WHILE ( @delimiter_index > 1 )
		BEGIN
			SET @file_type_id = substring(@required_file_types,1,@delimiter_index - 1)
			SET @required_file_types = ltrim(substring(@required_file_types,@delimiter_index + len(@delimiter),len(@required_file_types)))
			SET @delimiter_index = charindex(@delimiter,@required_file_types)
			
			Select @file_count = count(ar.rev_id)
			from dbo.cms_article_files_revision as ar with (nolock) inner join dbo.cms_article_files_revision_files as arf with (nolock) on ar.rev_id = arf.rev_id
			where arf.file_type_id = @file_type_id AND ar.article_id = @article_id AND ar.revision_status='L'
			If @file_count<1
			BEGIN
				Set @returner=@returner + CAST(@file_type_id as varchar(255)) + ','
			END
		END
		Select @file_count = count(ar.rev_id)
		from dbo.cms_article_files_revision as ar with (nolock) inner join dbo.cms_article_files_revision_files as arf with (nolock) on ar.rev_id = arf.rev_id
		where arf.file_type_id = @required_file_types AND ar.article_id = @article_id AND ar.revision_status='L'
		If @file_count<1
		BEGIN
			Set @returner=@returner + CAST(@file_type_id as varchar(255)) + ','
		END
	END
END

Select @returner as returner







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_rss_channel_content]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_select_rss_channel_content]
    @channel_id int
as

select  'S' as out_type, crc.sgz_id as out_id, cs.site_name as out_name, crc.sgz_exclude
from dbo.cms_rss_content crc with (nolock)
	left join dbo.cms_sites cs with (nolock) on cs.site_id = crc.sgz_id
where   crc.channel_id = @channel_id and crc.sgz_type = 'S'

union all

select  'G' as out_type, crc.sgz_id as out_id, cs.site_name + ' / ' + czg.zone_group_name as out_name, crc.sgz_exclude
from dbo.cms_rss_content crc with (nolock)
	left join dbo.cms_zone_groups czg with (nolock) on czg.zone_group_id = crc.sgz_id	
	left join dbo.cms_sites cs with (nolock) on cs.site_id = czg.site_id
where   crc.channel_id = @channel_id and crc.sgz_type = 'G'

union all

select  'Z' as out_type, crc.sgz_id as out_id, cs.site_name + ' / ' + czg.zone_group_name + ' / ' + cz.zone_name as out_name, crc.sgz_exclude
from dbo.cms_rss_content crc with (nolock)
	left join dbo.cms_zones cz with (nolock) on cz.zone_id = crc.sgz_id
	left join dbo.cms_zone_groups czg with (nolock) on czg.zone_group_id = cz.zone_group_id
	left join dbo.cms_sites cs with (nolock) on cs.site_id = czg.site_id
where   crc.channel_id = @channel_id and crc.sgz_type = 'Z'







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_sitemap_by_domain]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_select_sitemap_by_domain]
	@domain_alias varchar(255)

AS

set nocount on

	SELECT
		xml, gz,gzip_enabled
	FROM
		dbo.cms_sitemaps with (nolock)
	WHERE
		domain_alias = @domain_alias


set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_sitemap_status]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_admin_select_sitemap_status]
	@smap_id int

AS

Declare @now datetime

set nocount on

Set @now = getdate()

if exists(Select * from dbo.cms_sitemaps Where smap_id = @smap_id AND status <> 2)
begin
	Update dbo.cms_sitemaps Set status = 2, last_update = @now, last_generate_start=@now Where smap_id = @smap_id
	--Select 'OK' as rCode, @now as rDate
	Select 'OK' as rCode,* from dbo.cms_sitemaps Where smap_id = @smap_id
	return
end

else
begin
	Select 'NOK' as rCode, @now as rDate
	return
end

set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_sitemaps]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_select_sitemaps]
	@smap_id int

AS

set nocount on

if @smap_id > 0
begin
	SELECT
		d.domain_names,
		sm.smap_id, 
		sm.domain_id,
		sm.domain_alias as domain_alias,
		sm.status,
		sm.last_update,
		sm.last_generate,
		sm.notify_google,
		sm.notify_msn,
		sm.notify_ask,
		sm.notify_yahoo,
		sm.yahoo_id,
		sm.included_sites,
		sm.excluded_zonegroups,
		sm.excluded_zones,
		sm.excluded_articles,
		sm.afiles,
		sm.interval,
		sm.enabled,
		sm.created,
		sm.gzip_enabled
	FROM
		dbo.cms_sitemaps sm with (nolock)
	Inner Join
		cms_domains d with (nolock) on sm.domain_id = d.domain_id
	WHERE
		smap_id = @smap_id
end

else
begin
	SELECT
		d.domain_names,
		sm.smap_id,
		sm.domain_id,
		sm.domain_alias as domain_alias,
		sm.status,
		sm.last_update,
		sm.last_generate,
		sm.notify_google,
		sm.notify_msn,
		sm.notify_ask,
		sm.notify_yahoo,
		sm.yahoo_id,
		sm.included_sites,
		sm.excluded_zonegroups,
		sm.excluded_zones,
		sm.excluded_articles,
		sm.afiles,
		sm.interval,
		sm.enabled,
		sm.created
	FROM
		dbo.cms_sitemaps sm with (nolock)
	Inner Join
		cms_domains d with (nolock) on sm.domain_id = d.domain_id
end

set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_stf_templates]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_select_stf_templates]

AS

select t.stft_id, t.stft_name, t.created, t.created_by, t.updated, t.updated_by, p.UserName
from dbo.cms_stf_templates t with (nolock)
	left join dbo.vw_aspnet_MembershipUsers p with (nolock) on p.UserId = t.created_by
where stft_status = 'A'
order by t.stft_name







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_structure_group]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_admin_select_structure_group]
	@group_id int,
	@group_type tinyint
AS

if @group_id = -1 AND @group_type = 0
begin
	Select g.*, p.UserName as publisher_name, p.UserId 
	FROM dbo.cms_structure_groups as g with (nolock)
		left join dbo.vw_aspnet_MembershipUsers p with (nolock) on p.UserId = g.created_by
	Order By group_type ASC, group_name ASC
end

else if @group_id = -1 AND @group_type > 0
begin
	Select g.*, p.UserName as publisher_name, p.UserId 
	FROM dbo.cms_structure_groups as g with (nolock)
		left join dbo.vw_aspnet_MembershipUsers p with (nolock) on p.UserId = g.created_by
	Where group_type = @group_type
	Order By group_type ASC, group_name ASC
end

else
begin
	Select g.*, p.UserName, p.UserId
	FROM dbo.cms_structure_groups as g with (nolock)
		left join dbo.vw_aspnet_MembershipUsers p with (nolock) on p.UserId = g.created_by
	Where group_id = @group_id AND group_type = @group_type
end







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_template_revisions]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_select_template_revisions]
    @template_id int
AS

SELECT  top 50
        tr.history_id, 
        tr.template_id, 
        tr.publisher_id, 
        tr.created,
	p.UserName as publisher_name
FROM dbo.cms_template_revisions tr with (nolock)
	LEFT JOIN dbo.vw_aspnet_MembershipUsers p with (nolock) ON tr.publisher_id = p.UserId
WHERE   tr.template_id = @template_id
ORDER BY tr.created DESC







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_templates]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_select_templates]
	@group_id int
AS

if @group_id = -1
begin
	select t.template_id, t.template_name, t.publisher_id, t.created, t.updated, p.UserName as publisher_name, t.template_type, t.group_id, sg.group_name
	from dbo.cms_templates t with (nolock)
		left join dbo.vw_aspnet_MembershipUsers p with (nolock) on p.UserId = t.publisher_id
		left join dbo.cms_structure_groups sg with (nolock) on sg.group_id = t.group_id
	where template_status = 'A'
	order by sg.group_name ASC, t.template_name ASC
end

else
begin
	select t.template_id, t.template_name, t.publisher_id, t.created, t.updated, p.UserName as publisher_name, t.template_type, t.group_id, sg.group_name
	from dbo.cms_templates t with (nolock)
		left join dbo.vw_aspnet_MembershipUsers p with (nolock) on p.UserId = t.publisher_id
		left join dbo.cms_structure_groups sg with (nolock) on sg.group_id = t.group_id
	where template_status = 'A' AND t.group_id = @group_id
	order by sg.group_name ASC, t.template_name ASC
end







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_url_redirects]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_select_url_redirects]

AS


select r.redirect_id, r.redirect_alias,  r.publisher_id, r.created, r.updated, p.UserName as publisher_name, r.group_id, sg.group_name, (case 
				when charindex(char(10), d.domain_names) > 0 then substring(d.domain_names, 1, charindex(char(10), d.domain_names))  
				else d.domain_names
			 end +'/'+ r.redirect_alias) as full_url
from dbo.cms_redirects r with (nolock)
	left join dbo.vw_aspnet_MembershipUsers p with (nolock) on p.UserId = r.publisher_id
	left join dbo.cms_structure_groups sg with (nolock) on sg.group_id = r.group_id
	left join dbo.cms_article_zones a with(nolock) on a.article_id = r.article_id and a.zone_id = r.zone_id
	left join dbo.cms_zones z with(nolock) on a.zone_id = z.zone_id 
	left join dbo.cms_zone_groups zg with(nolock) on z.zone_group_id = zg.zone_group_id
	left join dbo.cms_sites s with(nolock) on s.site_id = zg.site_id
	left join dbo.cms_domains d with(nolock) on s.domain_id = d.domain_id
order by sg.group_name ASC, r.redirect_alias







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_zone_last_revision]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_select_zone_last_revision]
	@zone_id int
AS


if exists(select * from dbo.cms_zone_revision r with (nolock) where r.zone_id = @zone_id and zone_status <> 'D' and revision_status = 'L')
begin
	select top 1 r.rev_id
	from dbo.cms_zone_revision r with (nolock)
	where r.zone_id = @zone_id and zone_status <> 'D' and revision_status = 'L'
	order by r.rev_date desc
end
else
begin

	select top 1 r.rev_id
	from dbo.cms_zone_revision r with (nolock)
	where r.zone_id = @zone_id and zone_status <> 'D'
	order by revision_status asc, r.rev_date desc
end







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_zone_revision_details]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_select_zone_revision_details]
	@rev_id int
AS


select
	r.rev_id, r.rev_date, r.revision_status, r.zone_id, r.zone_name, r.zone_desc, r.zone_keywords, r.revised_by, r.approval_date, r.approval_id,
	r.css_merge, r.css_id, r.template_id, r.zone_group_id, r.zone_status, z.zone_status as current_status,
	r.css_id_mobile, r.css_id_print, r.template_id_mobile, r.custom_body,
	r.append_1, r.append_2, r.append_3, r.append_4, r.append_5, r.article_1, r.article_2, r.article_3, r.article_4, r.article_5,
	z.publisher_id, z.created, z.updated,
	r.rev_name, r.rev_note, r.zone_type_id, r.analytics,
	(select UserName from dbo.vw_aspnet_MembershipUsers with (nolock) where UserId = r.revised_by) as revised_name,
	(select UserName from dbo.vw_aspnet_MembershipUsers with (nolock) where UserId = r.approval_id) as approval_name,
	(select UserName from dbo.vw_aspnet_MembershipUsers with (nolock) where UserId = z.publisher_id) as publisher_name,
	r.meta_description,
	r.zone_name_display,
	r.content_1_editor_type, r.content_2_editor_type, r.content_3_editor_type, r.content_4_editor_type, r.content_5_editor_type,
	r.default_article,
	r.omniture_code,
	r.lang_id
from dbo.cms_zone_revision r with (nolock)
	left join dbo.cms_zones z with (nolock) on z.zone_id = r.zone_id
where r.rev_id = @rev_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_select_zone_revision_list]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_admin_select_zone_revision_list]
	@zone_id int
AS


select top 50
	r.rev_id, r.rev_date, r.revision_status, r.approval_date, r.zone_status, r.rev_name,
	(select UserName from dbo.vw_aspnet_MembershipUsers with (nolock) where UserId = r.revised_by ) as revised_name,
	(select UserName from dbo.vw_aspnet_MembershipUsers with (nolock) where UserId = r.approval_id ) as approval_name
from dbo.cms_zone_revision r with (nolock)
where r.zone_id = @zone_id and revision_status <> 'X'
order by r.rev_date desc







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_article_files_revision]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_update_article_files_revision]
    @af_rf_id bigint,	
    @rev_id bigint,   
    @article_id int, 
    @file_title nvarchar(500), 
    @file_order int, 
    @file_name_1 varchar(255), 
    @file_name_2 varchar(255), 
    @file_name_3 varchar(255), 
    @file_name_4 varchar(255), 
    @file_name_5 varchar(255), 
    @file_name_6 varchar(255), 
    @file_name_7 varchar(255), 
    @file_name_8 varchar(255), 
    @file_name_9 varchar(255), 
    @file_name_10 varchar(255), 
    @file_type_id int, 
    @file_comment ntext,
    @revised_by uniqueidentifier

AS
declare @fstat char(1)
declare @rstat char(1)
set @fstat = ''
set @rstat = ''


set nocount on

	if not exists(select * from dbo.cms_article_files_revision where rev_id = @rev_id and revision_status = 'N' )
	begin
		--Revision not exist, or not available for update, Create revision
		insert into dbo.cms_article_files_revision
		(revised_by, created_by, article_id)
		values
		(@revised_by, @revised_by, @article_id)

		set @rev_id = scope_identity()
		set @rstat = 'C'
		
		if @af_rf_id <> -2
		begin
			--Revision not exist. insert revision files	
			insert into dbo.cms_article_files_revision_files
			( rev_id, article_id, file_title, file_order, file_name_1, file_name_2, file_name_3, file_name_4, file_name_5, file_name_6, file_name_7, file_name_8, file_name_9, file_name_10, file_type_id, file_comment)
			values
			( @rev_id, @article_id, @file_title, @file_order, @file_name_1, @file_name_2, @file_name_3, @file_name_4, @file_name_5, @file_name_6, @file_name_7, @file_name_8, @file_name_9, @file_name_10, @file_type_id, @file_comment)
			
			set @fstat = 'C'
			set @af_rf_id = scope_identity()
		end 
	end
	else
	begin
		--Revision not approved yet.. Update it
		update dbo.cms_article_files_revision
		set 
		rev_date = getDate(), 
		revised_by = @revised_by     
		where
		rev_id = @rev_id
	
		set @rstat = 'U'
	
		if @af_rf_id = -1
		begin
			--Revision exist. insert revision files	
			insert into dbo.cms_article_files_revision_files 
			( rev_id,  article_id, file_title, file_order, file_name_1, file_name_2, file_name_3, file_name_4, file_name_5, file_name_6, file_name_7, file_name_8, file_name_9, file_name_10, file_type_id, file_comment)
			values
			( @rev_id,  @article_id, @file_title, @file_order, @file_name_1, @file_name_2, @file_name_3, @file_name_4, @file_name_5, @file_name_6, @file_name_7, @file_name_8, @file_name_9, @file_name_10, @file_type_id, @file_comment)
	
			set @fstat = 'C'
			set @af_rf_id = scope_identity()
		end
		else
		begin
			if @af_rf_id > 0
			begin
				--.. Update file
				update dbo.cms_article_files_revision_files
				set   
				        file_title = @file_title, 
				        file_order = @file_order, 
				        file_name_1 = @file_name_1, 
				        file_name_2 = @file_name_2, 
				        file_name_3 = @file_name_3, 
				        file_name_4 = @file_name_4, 
				        file_name_5 = @file_name_5, 
				        file_name_6 = @file_name_6, 
				        file_name_7 = @file_name_7, 
				        file_name_8 = @file_name_8, 
				        file_name_9 = @file_name_9, 
				        file_name_10 = @file_name_10, 
				        file_type_id = @file_type_id, 
				        file_comment = @file_comment
				where   af_rf_id = @af_rf_id	
				
				set @fstat = 'U'
			end
		end
	end
	
	
	
	select @fstat as fstat, @rstat as rstat, @af_rf_id as af_rf_id, @rev_id as rev_id



set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_article_revision]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_update_article_revision]
	@rev_id bigint, 
	@rev_name nvarchar(100), 
	@rev_note ntext, 
	@article_id int, 
	@clsf_id int,
	@status tinyint, 
	@startdate datetime, 
	@enddate datetime, 
	@orderno int, 
	@lang_id char(2), 
	@navigation_display tinyint, 
	@navigation_zone_id int, 
	@menu_text nvarchar(100), 
	@headline nvarchar(350), 
	@summary nvarchar(3000), 
	@keywords nvarchar(500), 
	@article_type tinyint, 
	@article_type_detail nvarchar(500), 
	@article_1 ntext, 
	@article_2 ntext, 
	@article_3 ntext, 
	@article_4 ntext, 
	@article_5 ntext, 
	@custom_1 ntext, 
	@custom_2 ntext,
	@custom_3 ntext,
	@custom_4 ntext,
	@custom_5 ntext,
	@custom_6 ntext,
	@custom_7 ntext,
	@custom_8 ntext,
	@custom_9 ntext,
	@custom_10 ntext,
	@custom_11 nvarchar(max), 
	@custom_12 nvarchar(max),
	@custom_13 nvarchar(max),
	@custom_14 nvarchar(max),
	@custom_15 nvarchar(max),
	@custom_16 nvarchar(max),
	@custom_17 nvarchar(max),
	@custom_18 nvarchar(max),
	@custom_19 nvarchar(max),
	@custom_20 nvarchar(max),
	@flag_1 bit, 
	@flag_2 bit, 
	@flag_3 bit, 
	@flag_4 bit, 
	@flag_5 bit, 
	@date_1 datetime, 
	@date_2 datetime,
	@date_3 datetime, 
	@date_4 datetime,
	@date_5 datetime,
	@rev_flag_1 bit, 
	@rev_flag_2 bit, 
	@rev_flag_3 bit, 
	@rev_flag_4 bit, 
	@rev_flag_5 bit, 
	@cl_1 tinyint, 
	@cl_2 tinyint, 
	@cl_3 tinyint, 
	@cl_4 tinyint, 
	@cl_5 tinyint, 
	@custom_body nvarchar(200),
	@revised_by uniqueidentifier,
	@cio char(1), --Article Check-In Check-Out parameter
	@meta_description nvarchar(2000),
	@content_1_editor_type char(1),
	@content_2_editor_type char(1),
	@content_3_editor_type char(1),
	@content_4_editor_type char(1),
	@content_5_editor_type char(1),
	@omniture_code ntext,
	@custom_setting varchar(4000)
AS
declare @astat char(1)
declare @rstat char(1)
declare @locked datetime
declare @locked_by nvarchar(100)

set @astat = ''
set @rstat = ''

set nocount on


if exists(select * from dbo.cms_languages where lang_id = @lang_id)
begin 
	--language exists


	if @article_id = -1
	begin
		--Article not exists. Create article with passive status
		insert into dbo.cms_articles
		(lang_id, headline, publisher_id)
		values
		(@lang_id, @headline, @revised_by)
		
		set @article_id = scope_identity()
		set @astat = 'C'
	end
	
	if @rev_id = -1
	begin
		--Revision not exist. Create revision
		insert into dbo.cms_article_revision
		(revised_by, rev_name, rev_note, created_by, article_id, clsf_id, status, startdate, enddate, orderno, lang_id, navigation_display, navigation_zone_id, menu_text, headline, summary, keywords, article_type, article_type_detail, article_1, article_2, article_3, article_4, article_5, custom_1, custom_2, custom_3, custom_4, custom_5, custom_6, custom_7, custom_8, custom_9, custom_10, custom_11, custom_12, custom_13, custom_14, custom_15, custom_16, custom_17, custom_18, custom_19, custom_20, flag_1, flag_2, flag_3, flag_4, flag_5, date_1, date_2, date_3, date_4, date_5, rev_flag_1, rev_flag_2, rev_flag_3, rev_flag_4, rev_flag_5, cl_1, cl_2, cl_3, cl_4, cl_5, custom_body, meta_description, content_1_editor_type, content_2_editor_type, content_3_editor_type, content_4_editor_type, content_5_editor_type, omniture_code, custom_setting)
		values
		(@revised_by, @rev_name, @rev_note, @revised_by, @article_id, @clsf_id, @status, @startdate, @enddate, @orderno, @lang_id, @navigation_display, @navigation_zone_id, @menu_text, @headline, @summary, @keywords, @article_type, @article_type_detail, @article_1, @article_2, @article_3, @article_4, @article_5, @custom_1, @custom_2, @custom_3, @custom_4, @custom_5, @custom_6, @custom_7, @custom_8, @custom_9, @custom_10, @custom_11, @custom_12, @custom_13, @custom_14, @custom_15, @custom_16, @custom_17, @custom_18, @custom_19, @custom_20, @flag_1, @flag_2, @flag_3, @flag_4, @flag_5, @date_1, @date_2, @date_3, @date_4, @date_5, @rev_flag_1, @rev_flag_2, @rev_flag_3, @rev_flag_4, @rev_flag_5, @cl_1, @cl_2, @cl_3, @cl_4, @cl_5, @custom_body, @meta_description, @content_1_editor_type, @content_2_editor_type, @content_3_editor_type, @content_4_editor_type, @content_5_editor_type, @omniture_code, @custom_setting)

		set @rev_id = scope_identity()
		set @rstat = 'C'
	end
	else
	begin
		--Check for article lock
		if not exists(select * from dbo.cms_articles with (nolock) where article_id = @article_id and locked_by = @revised_by) AND @cio = '1'
		begin
			select @locked = a.locked, @locked_by = p.UserName from dbo.cms_articles as a with (nolock) left outer join dbo.vw_aspnet_MembershipUsers as p  with (nolock) on a.locked_by = p.UserId where article_id = @article_id
			set @rstat = 'L'
			select @astat as astat, @rstat as rstat, @article_id as article_id, @rev_id as rev_id, @locked as locked, @locked_by as locked_by
			return
		end


		--Revision exist. Check status
		if exists(select * from dbo.cms_article_revision where rev_id = @rev_id and revision_status = 'N' )
		begin
			--Revision not approved yet.. Update it
			update dbo.cms_article_revision
			set 
			rev_date = getDate(), 
			revised_by = @revised_by, 
			rev_name = @rev_name, 
			rev_note = @rev_note, 
			article_id = @article_id, 
			clsf_id = @clsf_id,
			status = @status, 
			startdate = @startdate, 
			enddate = @enddate, 
			orderno = @orderno, 
			lang_id = @lang_id, 
			navigation_display = @navigation_display,
			navigation_zone_id = @navigation_zone_id,
			menu_text = @menu_text,
			headline = @headline, 
			summary = @summary, 
			keywords = @keywords, 
			article_type = @article_type, 
			article_type_detail = @article_type_detail, 
			article_1 = @article_1, 
			article_2 = @article_2, 
			article_3 = @article_3, 
			article_4 = @article_4, 
			article_5 = @article_5, 
			custom_1 = @custom_1, 
			custom_2 = @custom_2, 
			custom_3 = @custom_3, 
			custom_4 = @custom_4, 
			custom_5 = @custom_5, 
			custom_6 = @custom_6, 
			custom_7 = @custom_7, 
			custom_8 = @custom_8, 
			custom_9 = @custom_9, 
			custom_10 = @custom_10, 
			custom_11 = @custom_11, 
			custom_12 = @custom_12, 
			custom_13 = @custom_13, 
			custom_14 = @custom_14, 
			custom_15 = @custom_15, 
			custom_16 = @custom_16, 
			custom_17 = @custom_17, 
			custom_18 = @custom_18, 
			custom_19 = @custom_19, 
			custom_20 = @custom_20, 
			flag_1 = @flag_1, 
			flag_2 = @flag_2, 
			flag_3 = @flag_3, 
			flag_4 = @flag_4, 
			flag_5 = @flag_5, 
			date_1 = @date_1, 
			date_2 = @date_2,
			date_3 = @date_3, 
			date_4 = @date_4,
			date_5 = @date_5,
			rev_flag_1 = @rev_flag_1, 
			rev_flag_2 = @rev_flag_2, 
			rev_flag_3 = @rev_flag_3, 
			rev_flag_4 = @rev_flag_4, 
			rev_flag_5 = @rev_flag_5,
			cl_1 = @cl_1, 
			cl_2 = @cl_2, 
			cl_3 = @cl_3, 
			cl_4 = @cl_4, 
			cl_5 = @cl_5,
			custom_body = @custom_body,
			meta_description = @meta_description,
			content_1_editor_type = @content_1_editor_type,
			content_2_editor_type = @content_2_editor_type,
			content_3_editor_type = @content_3_editor_type,
			content_4_editor_type = @content_4_editor_type,
			content_5_editor_type = @content_5_editor_type,
			omniture_code = @omniture_code,
			custom_setting = @custom_setting
			where
			rev_id = @rev_id

			set @rstat = 'U'
	
		end
		else
		begin
			--Revision already approved or discarded. Save as new revision
			
			--reset revision flags first
			update dbo.cms_article_revision
			set rev_flag_1 = 0, rev_flag_2 = 0, rev_flag_3 = 0, rev_flag_4 = 0, rev_flag_5 = 0
			where rev_id = @rev_id

			-- insert as new revision
			insert into dbo.cms_article_revision
			(revised_by, rev_name, rev_note, created_by, article_id, clsf_id, status, startdate, enddate, orderno, lang_id, navigation_display, navigation_zone_id, menu_text, headline, summary, keywords, article_type, article_type_detail, article_1, article_2, article_3, article_4, article_5, custom_1, custom_2, custom_3, custom_4, custom_5, custom_6, custom_7, custom_8, custom_9, custom_10, custom_11, custom_12, custom_13, custom_14, custom_15, custom_16, custom_17, custom_18, custom_19, custom_20, flag_1, flag_2, flag_3, flag_4, flag_5, date_1, date_2, date_3, date_4, date_5, rev_flag_1, rev_flag_2, rev_flag_3, rev_flag_4, rev_flag_5, cl_1, cl_2, cl_3, cl_4, cl_5, custom_body, meta_description, content_1_editor_type, content_2_editor_type, content_3_editor_type, content_4_editor_type, content_5_editor_type, omniture_code, custom_setting)
			values
			(@revised_by, @rev_name, @rev_note, @revised_by, @article_id, @clsf_id, @status, @startdate, @enddate, @orderno, @lang_id, @navigation_display, @navigation_zone_id, @menu_text, @headline, @summary, @keywords, @article_type, @article_type_detail, @article_1, @article_2, @article_3, @article_4, @article_5, @custom_1, @custom_2, @custom_3, @custom_4, @custom_5,  @custom_6, @custom_7, @custom_8, @custom_9, @custom_10, @custom_11, @custom_12, @custom_13, @custom_14, @custom_15,  @custom_16, @custom_17, @custom_18, @custom_19, @custom_20, @flag_1, @flag_2, @flag_3, @flag_4, @flag_5, @date_1, @date_2, @date_3, @date_4, @date_5, @rev_flag_1, @rev_flag_2, @rev_flag_3, @rev_flag_4, @rev_flag_5, @cl_1, @cl_2, @cl_3, @cl_4, @cl_5, @custom_body, @meta_description, @content_1_editor_type, @content_2_editor_type, @content_3_editor_type, @content_4_editor_type, @content_5_editor_type, @omniture_code, @custom_setting)
		
			set @rev_id = scope_identity()
			set @rstat = 'N'
		end
	end
	
	
	
	select @astat as astat, @rstat as rstat, @article_id as article_id, @rev_id as rev_id, @locked as locked, @locked_by as locked_by
end
else
begin
	--language not found.. return error
	select 'D' as astat, '' as rStat, @article_id as article_id, @rev_id as rev_id
end


set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_article_revision_status]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_update_article_revision_status]
	@rev_id int,
	@revision_status char(1),
	@approval_id uniqueidentifier
AS


set nocount on

--check permissions!



-- check current status
if exists(select * from dbo.cms_article_revision with (nolock) where rev_id = @rev_id and revision_status in ('N','A','W') )
begin
	-- can be changed
	update dbo.cms_article_revision
	set
		revision_status = @revision_status,
		approval_date = getDate(),
		approval_id = @approval_id
	where
		rev_id = @rev_id


	-- mark instant messages as processed for this approval
	if @revision_status = 'A'
	begin
		update dbo.cms_instant_messaging
		set processed = getDate()
		where ims_type = 'AA' and ims_to = @approval_id and related_id = @rev_id
	end


	select 'OK' as rCode
end
else
begin
	select 'STATUS' as rCode
end



set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_breadcrumb]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_update_breadcrumb]
	@breadcrumb_id int,
	@breadcrumb_name NVARCHAR(50),
	@deep_level tinyint,
	@include_site CHAR(1),
	@include_zonegroup CHAR(1),
	@include_headline CHAR(1),
	@excluded_sites text,
	@excluded_zonegroups text,
	@excluded_zones text,
	@seperator nvarchar(50),
	@ul_class varchar(100),
	@include_submenus char(1),
	@breadcrumb_main_container varchar(10),
	@breadcrumb_main_item_container varchar(10),
	@breadcrumb_sub_container varchar(10),
	@breadcrumb_sub_item_container varchar(10),
	@pubID uniqueidentifier

AS

Set nocount on

If @breadcrumb_id = -1
begin
	INSERT INTO dbo.cms_breadcrumbs
		(
		breadcrumb_name,
		deep_level,
		include_site,
		include_zonegroup,
		include_headline,
		excluded_sites,
		excluded_zonegroups,
		excluded_zones,
		seperator,
		ul_class,
		include_submenus,
		breadcrumb_main_container,
		breadcrumb_main_item_container,
		breadcrumb_sub_container,
		breadcrumb_sub_item_container,
		created,
		created_by
		)
	VALUES
		(
		@breadcrumb_name,
		@deep_level,
		@include_site,
		@include_zonegroup,
		@include_headline,
		@excluded_sites,
		@excluded_zonegroups,
		@excluded_zones,
		@seperator,
		@ul_class,
		@include_submenus,
		@breadcrumb_main_container,
		@breadcrumb_main_item_container,
		@breadcrumb_sub_container,
		@breadcrumb_sub_item_container,
		getdate(),
		@pubID
		)
	Select 'CREATED' as rCode
end

else
begin
	Update dbo.cms_breadcrumbs
	Set
		breadcrumb_name = @breadcrumb_name,
		deep_level = @deep_level,
		include_site = @include_site,
		include_zonegroup = @include_zonegroup,
		include_headline = @include_headline,
		excluded_sites = @excluded_sites,
		excluded_zonegroups = @excluded_zonegroups,
		excluded_zones = @excluded_zones,
		seperator = @seperator,
		ul_class = @ul_class,
		include_submenus = @include_submenus,
		breadcrumb_main_container = @breadcrumb_main_container,
		breadcrumb_main_item_container = @breadcrumb_main_item_container,
		breadcrumb_sub_container = @breadcrumb_sub_container,
		breadcrumb_sub_item_container = @breadcrumb_sub_item_container,
		updated = getdate(),
		updated_by = @pubID
	Where
		breadcrumb_id = @breadcrumb_id

	Select 'UPDATED' as rCode
end

Set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_cc]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_update_cc]
	@cc_id int, 
	@cc_name nvarchar(100), 
	@cc_html ntext, 
	@publisher_id uniqueidentifier,
	@group_id int,
	@structure_description nvarchar(2000)
AS


set nocount on

if exists(select * from dbo.cms_custom_content where cc_id = @cc_id)
begin
	update dbo.cms_custom_content
	set
		cc_name = @cc_name, 
		cc_html = @cc_html,
		updated = getDate(), 
		updated_by = @publisher_id,
		group_id = @group_id,
		structure_description = @structure_description
	where
		cc_id = @cc_id		
	
	select 'U' as cStat, @cc_id as cc_id, getDate() as updated
end
else
begin
	insert into dbo.cms_custom_content
	(cc_name, created_by, updated_by, cc_html, group_id, structure_description)
	values
	(@cc_name, @publisher_id, @publisher_id, @cc_html, @group_id, @structure_description)

	select 'I' as cStat, scope_identity() as cc_id, getDate() as updated
end

set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_classification_combo_values]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_update_classification_combo_values]
    @classification_id int, 
    @column_no tinyint, 
    @combo_supid ntext,
    @combo_label nvarchar(255),
    @combo_value nvarchar(255),
    @combo_order int,
    @created_by uniqueidentifier
as



set nocount on



	insert into dbo.cms_classification_combo_values
	(classification_id, column_no, combo_supid, combo_label, combo_value, combo_order, created_by)
	values
	(@classification_id, @column_no,@combo_supid, @combo_label, @combo_value, @combo_order, @created_by)

	select 'U' as cStat, @classification_id as classification_id, getDate() as created


set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_classification_details]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_update_classification_details]
    @classification_id int,
    @classification_name nvarchar(100),
    @summary_cb bit,
    @enddate_cb bit,
    @keywords_cb bit,
    @custom1_cb bit,
    @custom2_cb bit,
    @custom3_cb bit,
    @custom4_cb bit,
    @custom5_cb bit,
    @custom6_cb bit,
    @custom7_cb bit,
    @custom8_cb bit,
    @custom9_cb bit,
    @custom10_cb bit,
    @custom11_cb bit,
    @custom12_cb bit,
    @custom13_cb bit,
    @custom14_cb bit,
    @custom15_cb bit,
    @custom16_cb bit,
    @custom17_cb bit,
    @custom18_cb bit,
    @custom19_cb bit,
    @custom20_cb bit,
    @date1_cb bit,
    @date2_cb bit,
    @date3_cb bit,
    @date4_cb bit,
    @date5_cb bit,
    @custom1_text nvarchar(50),
    @custom2_text nvarchar(50),
    @custom3_text nvarchar(50),
    @custom4_text nvarchar(50),
    @custom5_text nvarchar(50),
    @custom6_text nvarchar(50),
    @custom7_text nvarchar(50),
    @custom8_text nvarchar(50),
    @custom9_text nvarchar(50),
    @custom10_text nvarchar(50),
    @custom11_text nvarchar(50),
    @custom12_text nvarchar(50),
    @custom13_text nvarchar(50),
    @custom14_text nvarchar(50),
    @custom15_text nvarchar(50),
    @custom16_text nvarchar(50),
    @custom17_text nvarchar(50),
    @custom18_text nvarchar(50),
    @custom19_text nvarchar(50),
    @custom20_text nvarchar(50),
    @custom1_type char(1),
    @custom2_type char(1),
    @custom3_type char(1),
    @custom4_type char(1),
    @custom5_type char(1),
    @custom6_type char(1),
    @custom7_type char(1),
    @custom8_type char(1),
    @custom9_type char(1),
    @custom10_type char(1),
    @flag1_text nvarchar(50),
    @flag2_text nvarchar(50),
    @flag3_text nvarchar(50),
    @flag4_text nvarchar(50),
    @flag5_text nvarchar(50),
    @date1_text nvarchar(50),
    @date2_text nvarchar(50),
    @date3_text nvarchar(50),
    @date4_text nvarchar(50),
    @date5_text nvarchar(50),
    @summary_text nvarchar(50),
    @enddate_text nvarchar(50),
    @keywords_text nvarchar(50),
    @article1_text nvarchar(50),
    @article2_text nvarchar(50),
    @article3_text nvarchar(50),
    @article4_text nvarchar(50),
    @article5_text nvarchar(50),
    @article1_cb bit,
    @article2_cb bit,
    @article3_cb bit,
    @article4_cb bit,
    @article5_cb bit,
    @custom1_subcolumn tinyint,
    @custom2_subcolumn tinyint,
    @custom3_subcolumn tinyint,
    @custom4_subcolumn tinyint,
    @custom5_subcolumn tinyint,
    @custom6_subcolumn tinyint,
    @custom7_subcolumn tinyint,
    @custom8_subcolumn tinyint,
    @custom9_subcolumn tinyint,
    @custom10_subcolumn tinyint,
    @file_required_cb bit,
    @file_title_required_cb bit,
    @file_description_required_cb bit,
    @required_file_types varchar(8000),
    @created_by uniqueidentifier,
    @group_id int,
    @structure_description nvarchar(2000)
as 
    set nocount on

    if not exists ( select  *
                    from    dbo.cms_classifications
                    where   classification_id <> @classification_id
                            and classification_name = @classification_name ) 
        begin 
	--name not duplicate..
            if exists ( select  *
                        from    dbo.cms_classifications
                        where   classification_id = @classification_id ) 
                begin



                    update  dbo.cms_classifications
                    set     classification_name = @classification_name,
                            summary_cb = @summary_cb,
                            enddate_cb = @enddate_cb,
                            keywords_cb = @keywords_cb,
                            custom1_cb = @custom1_cb,
                            custom2_cb = @custom2_cb,
                            custom3_cb = @custom3_cb,
                            custom4_cb = @custom4_cb,
                            custom5_cb = @custom5_cb,
                            custom6_cb = @custom6_cb,
                            custom7_cb = @custom7_cb,
                            custom8_cb = @custom8_cb,
                            custom9_cb = @custom9_cb,
                            custom10_cb = @custom10_cb,
                            custom11_cb = @custom11_cb,
                            custom12_cb = @custom12_cb,
                            custom13_cb = @custom13_cb,
                            custom14_cb = @custom14_cb,
                            custom15_cb = @custom15_cb,
                            custom16_cb = @custom16_cb,
                            custom17_cb = @custom17_cb,
                            custom18_cb = @custom18_cb,
                            custom19_cb = @custom19_cb,
                            custom20_cb = @custom20_cb,
                            date1_cb = @date1_cb,
                            date2_cb = @date2_cb,
                            date3_cb = @date3_cb,
                            date4_cb = @date4_cb,
                            date5_cb = @date5_cb,
                            custom1_text = @custom1_text,
                            custom2_text = @custom2_text,
                            custom3_text = @custom3_text,
                            custom4_text = @custom4_text,
                            custom5_text = @custom5_text,
                            custom6_text = @custom6_text,
                            custom7_text = @custom7_text,
                            custom8_text = @custom8_text,
                            custom9_text = @custom9_text,
                            custom10_text = @custom10_text,
                            custom11_text = @custom11_text,
                            custom12_text = @custom12_text,
                            custom13_text = @custom13_text,
                            custom14_text = @custom14_text,
                            custom15_text = @custom15_text,
                            custom16_text = @custom16_text,
                            custom17_text = @custom17_text,
                            custom18_text = @custom18_text,
                            custom19_text = @custom19_text,
                            custom20_text = @custom20_text,
                            flag1_text = @flag1_text,
                            flag2_text = @flag2_text,
                            flag3_text = @flag3_text,
                            flag4_text = @flag4_text,
                            flag5_text = @flag5_text,
                            date1_text = @date1_text,
                            date2_text = @date2_text,
                            date3_text = @date3_text,
                            date4_text = @date4_text,
                            date5_text = @date5_text,
                            custom1_type = @custom1_type,
                            custom2_type = @custom2_type,
                            custom3_type = @custom3_type,
                            custom4_type = @custom4_type,
                            custom5_type = @custom5_type,
                            custom6_type = @custom6_type,
                            custom7_type = @custom7_type,
                            custom8_type = @custom8_type,
                            custom9_type = @custom9_type,
                            custom10_type = @custom10_type,
                            summary_text = @summary_text,
                            enddate_text = @enddate_text,
                            keywords_text = @keywords_text,
                            article1_text = @article1_text,
                            article2_text = @article2_text,
                            article3_text = @article3_text,
                            article4_text = @article4_text,
                            article5_text = @article5_text,
                            article1_cb = @article1_cb,
                            article2_cb = @article2_cb,
                            article3_cb = @article3_cb,
                            article4_cb = @article4_cb,
                            article5_cb = @article5_cb,
                            custom1_subcolumn = @custom1_subcolumn,
                            custom2_subcolumn = @custom2_subcolumn,
                            custom3_subcolumn = @custom3_subcolumn,
                            custom4_subcolumn = @custom4_subcolumn,
                            custom5_subcolumn = @custom5_subcolumn,
                            custom6_subcolumn = @custom6_subcolumn,
                            custom7_subcolumn = @custom7_subcolumn,
                            custom8_subcolumn = @custom8_subcolumn,
                            custom9_subcolumn = @custom9_subcolumn,
                            custom10_subcolumn = @custom10_subcolumn,
                            file_required_cb = @file_required_cb,
                            file_title_required_cb = @file_title_required_cb,
                            file_description_required_cb = @file_description_required_cb,
                            required_file_types = @required_file_types,
                            group_id = @group_id,
                            structure_description = @structure_description
                    where   classification_id = @classification_id

                    select  'U' as cStat,
                            @classification_id as classification_id,
                            getDate() as created
                end
            else 
                begin
                    insert  into dbo.cms_classifications
                            (
                              classification_name,
                              created_by,
                              summary_cb,
                              enddate_cb,
                              keywords_cb,
                              custom1_cb,
                              custom2_cb,
                              custom3_cb,
                              custom4_cb,
                              custom5_cb,
                              custom6_cb,
                              custom7_cb,
                              custom8_cb,
                              custom9_cb,
                              custom10_cb,
                              custom11_cb,
                              custom12_cb,
                              custom13_cb,
                              custom14_cb,
                              custom15_cb,
                              custom16_cb,
                              custom17_cb,
                              custom18_cb,
                              custom19_cb,
                              custom20_cb,
                              date1_cb,
                              date2_cb,
                              date3_cb,
                              date4_cb,
                              date5_cb,
                              custom1_text,
                              custom2_text,
                              custom3_text,
                              custom4_text,
                              custom5_text,
                              custom6_text,
                              custom7_text,
                              custom8_text,
                              custom9_text,
                              custom10_text,
                              custom11_text,
                              custom12_text,
                              custom13_text,
                              custom14_text,
                              custom15_text,
                              custom16_text,
                              custom17_text,
                              custom18_text,
                              custom19_text,
                              custom20_text,
                              flag1_text,
                              flag2_text,
                              flag3_text,
                              flag4_text,
                              flag5_text,
                              date1_text,
                              date2_text,
                              date3_text,
                              date4_text,
                              date5_text,
                              custom1_type,
                              custom2_type,
                              custom3_type,
                              custom4_type,
                              custom5_type,
                              custom6_type,
                              custom7_type,
                              custom8_type,
                              custom9_type,
                              custom10_type,
                              summary_text,
                              enddate_text,
                              keywords_text,
                              article1_text,
                              article2_text,
                              article3_text,
                              article4_text,
                              article5_text,
                              article1_cb,
                              article2_cb,
                              article3_cb,
                              article4_cb,
                              article5_cb,
                              custom1_subcolumn,
                              custom2_subcolumn,
                              custom3_subcolumn,
                              custom4_subcolumn,
                              custom5_subcolumn,
                              custom6_subcolumn,
                              custom7_subcolumn,
                              custom8_subcolumn,
                              custom9_subcolumn,
                              custom10_subcolumn,
                              file_required_cb,
                              file_title_required_cb,
                              file_description_required_cb,
                              required_file_types,
                              group_id,
                              structure_description
                            )
                    values  (
                              @classification_name,
                              @created_by,
                              @summary_cb,
                              @enddate_cb,
                              @keywords_cb,
                              @custom1_cb,
                              @custom2_cb,
                              @custom3_cb,
                              @custom4_cb,
                              @custom5_cb,
                              @custom6_cb,
                              @custom7_cb,
                              @custom8_cb,
                              @custom9_cb,
                              @custom10_cb,
                              @custom11_cb,
                              @custom12_cb,
                              @custom13_cb,
                              @custom14_cb,
                              @custom15_cb,
                              @custom16_cb,
                              @custom17_cb,
                              @custom18_cb,
                              @custom19_cb,
                              @custom20_cb,
                              @date1_cb,
                              @date2_cb,
                              @date3_cb,
                              @date4_cb,
                              @date5_cb,
                              @custom1_text,
                              @custom2_text,
                              @custom3_text,
                              @custom4_text,
                              @custom5_text,
                              @custom6_text,
                              @custom7_text,
                              @custom8_text,
                              @custom9_text,
                              @custom10_text,
                              @custom11_text,
                              @custom12_text,
                              @custom13_text,
                              @custom14_text,
                              @custom15_text,
                              @custom16_text,
                              @custom17_text,
                              @custom18_text,
                              @custom19_text,
                              @custom20_text,
                              @flag1_text,
                              @flag2_text,
                              @flag3_text,
                              @flag4_text,
                              @flag5_text,
                              @date1_text,
                              @date2_text,
                              @date3_text,
                              @date4_text,
                              @date5_text,
                              @custom1_type,
                              @custom2_type,
                              @custom3_type,
                              @custom4_type,
                              @custom5_type,
                              @custom6_type,
                              @custom7_type,
                              @custom8_type,
                              @custom9_type,
                              @custom10_type,
                              @summary_text,
                              @enddate_text,
                              @keywords_text,
                              @article1_text,
                              @article2_text,
                              @article3_text,
                              @article4_text,
                              @article5_text,
                              @article1_cb,
                              @article2_cb,
                              @article3_cb,
                              @article4_cb,
                              @article5_cb,
                              @custom1_subcolumn,
                              @custom2_subcolumn,
                              @custom3_subcolumn,
                              @custom4_subcolumn,
                              @custom5_subcolumn,
                              @custom6_subcolumn,
                              @custom7_subcolumn,
                              @custom8_subcolumn,
                              @custom9_subcolumn,
                              @custom10_subcolumn,
                              @file_required_cb,
                              @file_title_required_cb,
                              @file_description_required_cb,
                              @required_file_types,
                              @group_id,
                              @structure_description
                            )

                    select  'I' as cStat,
                            CAST( scope_identity() as int) as classification_id,
                            getDate() as created
                end
        end
    else 
        begin
	--duplicate name found.. return error
            select  'D' as cStat,
                    0 as classification_id,
                    NULL as created
        end
    set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_config_parameter]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_update_config_parameter]

	@config_id int,

	@config_name varchar(100),

	@config_value ntext,

	@ws varchar(10),

	@publisher_id uniqueidentifier

AS



set nocount on



if not exists(select * from dbo.cms_config where config_id <> @config_id and config_name = @config_name)

begin 

	--name not duplicate..

	if exists(select * from dbo.cms_config where config_id = @config_id)

	begin



		-- update parameter

		if @ws = 'remote'

		begin

			--update remote value

			update dbo.cms_config

			set

				config_name = @config_name,

				config_value_remote = @config_value,

				publisher_id = @publisher_id,

				updated = getDate()

			where

				config_id = @config_id

		end

		else

		begin

			--update local value

			update dbo.cms_config

			set

				config_name = @config_name,

				config_value_local = @config_value,

				publisher_id = @publisher_id,

				updated = getDate()

			where

				config_id = @config_id

		end

		

		select 'U' as cStat, @config_id as config_id, getDate() as updated

	

	end

	else

	begin

		--insert parameter

		if @ws = 'remote'

		begin

			insert into dbo.cms_config

			(config_name, config_value_remote, publisher_id)

			values

			(@config_name, @config_value, @publisher_id)

		end

		else

		begin

			insert into dbo.cms_config

			(config_name, config_value_local, publisher_id)

			values

			(@config_name, @config_value, @publisher_id)

		end

	

		select 'I' as cStat, scope_identity() as config_id, getDate() as updated

	

	end

end

else

begin

	--duplicate name found.. return error

	select 'D' as cStat, '' as config_id, '' as updated

end

set nocount off








GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_css]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_update_css]
	@css_id int,
	@css_name nvarchar(100),
	@css_code ntext,
	@css_fix ntext,
	@css_type tinyint,
	@css_rel_text nvarchar(100),
	@css_type_text nvarchar(100),
	@publisher_id uniqueidentifier,
	@group_id int,
	@structure_description nvarchar(2000)
AS

set nocount on

if not exists(select * from dbo.cms_css where css_id <> @css_id and css_name = @css_name and css_status <> 'D')
begin 
	--name not duplicate..
	if exists(select * from dbo.cms_css where css_id = @css_id)
	begin
		--copy to revision history first..
		INSERT INTO dbo.cms_css_revisions
		(css_id, css_type, css_code, css_fix, css_rel_text, css_type_text, publisher_id)
			select css_id, css_type, css_code, css_fix, css_rel_text, css_type_text, publisher_id
			from dbo.cms_css
			where css_id = @css_id
	
		-- update css
		update dbo.cms_css
		set
			css_name = @css_name,
			css_code = @css_code,
			css_fix = @css_fix,
			css_type = @css_type,
			css_rel_text = @css_rel_text,
			css_type_text = @css_type_text,
			publisher_id = @publisher_id,
			updated = getDate(),
			group_id = @group_id,
			structure_description = @structure_description
		where
			css_id = @css_id
		
		select 'U' as cStat, @css_id as css_id, getDate() as created
	
	end
	else
	begin
		--insert css
		insert into dbo.cms_css
		(css_name, css_code, css_fix, css_rel_text, css_type_text, css_type, publisher_id, group_id, structure_description)
		values
		(@css_name, @css_code, @css_fix, @css_rel_text, @css_type_text, @css_type, @publisher_id, @group_id, @structure_description)
	
		select 'I' as cStat, scope_identity() as css_id, getDate() as created
	
	end
end
else
begin
	--duplicate name found.. return error
	select 'D' as cStat, '' as css_id, '' as created
end
set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_domains]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_update_domains]
	@domain_id int, 
	@domain_names varchar(8000), 
	@home_page_article varchar(50), 
	@publisher_id uniqueidentifier,
	@error_page_article varchar(50)
AS


set nocount on

if exists(select * from dbo.cms_domains where domain_id = @domain_id)
begin
	update dbo.cms_domains
	set
		domain_names = @domain_names, 
		home_page_article = @home_page_article,
		error_page_article = @error_page_article,
		updated = getDate(), 
		updated_by = @publisher_id
	where
		domain_id = @domain_id		
	
	select 'U' as dStat, @domain_id as domain_id, getDate() as updated
end
else
begin
	insert into dbo.cms_domains
	(domain_names, home_page_article, error_page_article, created_by, updated_by)
	values
	(@domain_names, @home_page_article, @error_page_article, @publisher_id, @publisher_id)

	select 'I' as dStat, scope_identity() as domain_id, getDate() as updated
end

set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_file_type]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_update_file_type]
    @type_id int,
    @type_name nvarchar(100),
    @type_alias varchar(50),
    @file1_name nvarchar(200),
    @file1_extension varchar(100),
    @file1_size int,
    @file1_wh varchar(10),
    @file2_name nvarchar(200),
    @file2_extension varchar(100),
    @file2_size int,
    @file2_wh varchar(10),
    @file3_name nvarchar(200),
    @file3_extension varchar(100),
    @file3_size int,
    @file3_wh varchar(10),
    @file4_name nvarchar(200),
    @file4_extension varchar(100),
    @file4_size int,
    @file4_wh varchar(10),
    @file5_name nvarchar(200),
    @file5_extension varchar(100),
    @file5_size int,
    @file5_wh varchar(10),
    @file6_name nvarchar(200),
    @file6_extension varchar(100),
    @file6_size int,
    @file6_wh varchar(10),
    @file7_name nvarchar(200),
    @file7_extension varchar(100),
    @file7_size int,
    @file7_wh varchar(10),
    @file8_name nvarchar(200),
    @file8_extension varchar(100),
    @file8_size int,
    @file8_wh varchar(10),
    @file9_name nvarchar(200),
    @file9_extension varchar(100),
    @file9_size int,
    @file9_wh varchar(10),
    @file10_name nvarchar(200),
    @file10_extension varchar(100),
    @file10_size int,
    @file10_wh varchar(10),
    @group_id int,
    @structure_description nvarchar(2000)
AS 
    set nocount on


    if not exists ( select  *
                    from    dbo.cms_file_types
                    where   type_id <> @type_id
                            and ( type_name = @type_name
                                  or type_alias = @type_alias
                                ) ) 
        begin 
	--name not duplicate..
            if exists ( select  *
                        from    dbo.cms_file_types
                        where   type_id = @type_id ) 
                begin
                    update  dbo.cms_file_types
                    set     type_name = @type_name,
                            type_alias = @type_alias,
                            file1_name = @file1_name,
                            file1_extension = @file1_extension,
                            file1_size = @file1_size,
                            file1_wh = @file1_wh,
                            file2_name = @file2_name,
                            file2_extension = @file2_extension,
                            file2_size = @file2_size,
                            file2_wh = @file2_wh,
                            file3_name = @file3_name,
                            file3_extension = @file3_extension,
                            file3_size = @file3_size,
                            file3_wh = @file3_wh,
                            file4_name = @file4_name,
                            file4_extension = @file4_extension,
                            file4_size = @file4_size,
                            file4_wh = @file4_wh,
                            file5_name = @file5_name,
                            file5_extension = @file5_extension,
                            file5_size = @file5_size,
                            file5_wh = @file5_wh,
                            file6_name = @file6_name,
                            file6_extension = @file6_extension,
                            file6_size = @file6_size,
                            file6_wh = @file6_wh,
                            file7_name = @file7_name,
                            file7_extension = @file7_extension,
                            file7_size = @file7_size,
                            file7_wh = @file7_wh,
                            file8_name = @file8_name,
                            file8_extension = @file8_extension,
                            file8_size = @file8_size,
                            file8_wh = @file8_wh,
                            file9_name = @file9_name,
                            file9_extension = @file9_extension,
                            file9_size = @file9_size,
                            file9_wh = @file9_wh,
                            file10_name = @file10_name,
                            file10_extension = @file10_extension,
                            file10_size = @file10_size,
                            file10_wh = @file10_wh,
                            group_id = @group_id,
                            structure_description = @structure_description,
                            updated = getDate()
                    where   type_id = @type_id
                    select  'U' as sStat,
                            @type_id as type_id,
                            getDate() as created
	
                end
            else 
                begin
                    insert  into dbo.cms_file_types
                            (
                              type_name,
                              type_alias,
                              file1_name,
                              file1_extension,
                              file1_size,
                              file1_wh,
                              file2_name,
                              file2_extension,
                              file2_size,
                              file2_wh,
                              file3_name,
                              file3_extension,
                              file3_size,
                              file3_wh,
                              file4_name,
                              file4_extension,
                              file4_size,
                              file4_wh,
                              file5_name,
                              file5_extension,
                              file5_size,
                              file5_wh,
                              file6_name,
                              file6_extension,
                              file6_size,
                              file6_wh,
                              file7_name,
                              file7_extension,
                              file7_size,
                              file7_wh,
                              file8_name,
                              file8_extension,
                              file8_size,
                              file8_wh,
                              file9_name,
                              file9_extension,
                              file9_size,
                              file9_wh,
                              file10_name,
                              file10_extension,
                              file10_size,
                              file10_wh,
                              group_id,
                              structure_description
                            )
                    values  (
                              @type_name,
                              @type_alias,
                              @file1_name,
                              @file1_extension,
                              @file1_size,
                              @file1_wh,
                              @file2_name,
                              @file2_extension,
                              @file2_size,
                              @file2_wh,
                              @file3_name,
                              @file3_extension,
                              @file3_size,
                              @file3_wh,
                              @file4_name,
                              @file4_extension,
                              @file4_size,
                              @file4_wh,
                              @file5_name,
                              @file5_extension,
                              @file5_size,
                              @file5_wh,
                              @file6_name,
                              @file6_extension,
                              @file6_size,
                              @file6_wh,
                              @file7_name,
                              @file7_extension,
                              @file7_size,
                              @file7_wh,
                              @file8_name,
                              @file8_extension,
                              @file8_size,
                              @file8_wh,
                              @file9_name,
                              @file9_extension,
                              @file9_size,
                              @file9_wh,
                              @file10_name,
                              @file10_extension,
                              @file10_size,
                              @file10_wh,
                              @group_id,
                              @structure_description
                            )
	
                    select  'I' as sStat,
                            scope_identity() as type_id,
                            getDate() as created
                end
        end
    else 
        begin
	--name duplicate.. return error
            select  'D' as sStat,
                    '-1' as type_id,
                    '' as created
        end


    set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_fop_failure_status]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_update_fop_failure_status]
	@log_id as int,
	@op_status as char(1),
	@summary as nvarchar(300),
	@processed_by as int
as


if @op_status = 'R'
begin
	-- increase retry count
	update dbo.cms_fop_failure_log
	set
		processed = getDate(),
		processed_by = @processed_by,
		op_status = @op_status,
		summary = @summary,
		retry_count = retry_count + 1
	where
		log_id = @log_id
end
else
begin
	update dbo.cms_fop_failure_log
	set
		processed = getDate(),
		processed_by = @processed_by,
		op_status = @op_status
	where
		log_id = @log_id
end







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_hidden_values]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_update_hidden_values]
	@hidden_id int, 
	@hidden_value nvarchar(50),
	@hidden_type tinyint,
	@hidden_desc nvarchar(100), 
	@publisher_id uniqueidentifier
AS


set nocount on

if not exists(select * from dbo.cms_hidden_values where hidden_id <> @hidden_id and hidden_value = @hidden_value)
begin
	--duplicate not found

	if exists(select * from dbo.cms_hidden_values where hidden_id = @hidden_id)
	begin
		update dbo.cms_hidden_values
		set
			hidden_value = @hidden_value, 
			hidden_type = @hidden_type,
			hidden_desc = @hidden_desc,
			updated = getDate(), 
			updated_by = @publisher_id
		where
			hidden_id = @hidden_id		
		
		select 'U' as dStat, @hidden_id as hidden_id, getDate() as updated
	end
	else
	begin
		insert into dbo.cms_hidden_values
		(hidden_value, hidden_type, hidden_desc, created_by, updated_by)
		values
		(@hidden_value, @hidden_type, @hidden_desc, @publisher_id, @publisher_id)
	
		select 'I' as dStat, scope_identity() as hidden_id, getDate() as updated
	end
end
else
begin
	select 'D' as dStat, 0 as hidden_id, getDate() as updated
end

set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_language]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Add lang_alias to [cms_asp_admin_update_language]
CREATE procedure [dbo].[cms_asp_admin_update_language]
	@lang_id char(2),
	@lang_name nvarchar(20),
	@lang_xml ntext,
	@lang_order int,
	@publisher_id uniqueidentifier,
	@lang_alias nvarchar(300)
AS


set nocount on

if exists(select * from dbo.cms_languages where lang_id = @lang_id)
begin
	update dbo.cms_languages
	set
		lang_name = @lang_name,
		lang_xml = @lang_xml,
		lang_order = @lang_order,
		publisher_id = @publisher_id,
		lang_alias = @lang_alias,
		updated = getDate()
	where
		lang_id = @lang_id
	
	select 'U' as lStat, getDate() as updated
end
else
begin
	insert into dbo.cms_languages
	(lang_id, lang_name, lang_xml, lang_order, publisher_id,lang_alias)
	values
	(@lang_id, @lang_name, @lang_xml, @lang_order, @publisher_id,@lang_alias)

	select 'I' as lStat, getDate() as updated
end

set nocount off


GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_plugin]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_update_plugin]
	@plugin_id int,
	@plugin_name nvarchar(100),
	@plugin_code ntext,
	@plugin_status tinyint,
	@publisher_id uniqueidentifier,
	@group_id int,
	@structure_description nvarchar(2000)
AS

set nocount on

if not exists(select * from dbo.cms_plugins where plugin_id <> @plugin_id and plugin_name = @plugin_name and plugin_status <> 2)
begin 
	--name not duplicate..
	if exists(select * from dbo.cms_plugins where plugin_id = @plugin_id)
	begin
	
		-- update plugin
		update dbo.cms_plugins
		set
			plugin_name = @plugin_name,
			plugin_code = @plugin_code,
			plugin_status = @plugin_status,
			updated_by = @publisher_id,
			updated = getDate(),
			group_id = @group_id,
			structure_description = @structure_description
		where
			plugin_id = @plugin_id
		
		select 'U' as pStat, @plugin_id as plugin_id, getDate() as created
	
	end
	else
	begin
		--insert plugin
		insert into dbo.cms_plugins
		(plugin_name, plugin_code, plugin_status, created_by, updated_by, group_id, structure_description)
		values
		(@plugin_name, @plugin_code, @plugin_status, @publisher_id, @publisher_id, @group_id, @structure_description)
	
		select 'I' as pStat, scope_identity() as plugin_id, getDate() as created
	
	end
end
else
begin
	--duplicate name found.. return error
	select 'D' as pStat, '' as plugin_id, '' as created
end
set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_portlet]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_update_portlet]
	@portlet_id int, 
	@portlet_name nvarchar(100), 
	@portlet_status tinyint, 
	@portlet_html ntext, 
	@portlet_css ntext,
	@editor_type char(1),
	@portlet_header ntext,
	@portlet_footer ntext,
	@publisher_id uniqueidentifier,
	@group_id int,
	@structure_description nvarchar(2000),
	@enable_shortcut char(1)
AS


set nocount on

if exists(select * from dbo.cms_portlets where portlet_id = @portlet_id)
begin
	update dbo.cms_portlets
	set
		portlet_name = @portlet_name, 
		portlet_status = @portlet_status, 
		updated = getDate(), 
		updated_by = @publisher_id, 
		portlet_html = @portlet_html, 
		portlet_css = @portlet_css,
		content_editor_type = @editor_type,
		portlet_header = @portlet_header,
		portlet_footer = @portlet_footer,
		group_id = @group_id,
		structure_description = @structure_description,
		enable_shortcut = @enable_shortcut
	where
		portlet_id = @portlet_id		
	
	select 'U' as pStat, @portlet_id as portlet_id, getDate() as updated
end
else
begin
	insert into dbo.cms_portlets
	(portlet_name, publisher_id, portlet_status, updated_by, portlet_html, portlet_css, content_editor_type, portlet_header, portlet_footer, group_id, structure_description, enable_shortcut)
	values
	(@portlet_name, @publisher_id, @portlet_status, @publisher_id, @portlet_html, @portlet_css, @editor_type, @portlet_header, @portlet_footer, @group_id, @structure_description, @enable_shortcut)

	select 'I' as pStat, scope_identity() as portlet_id, getDate() as updated
end

set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_redirection_details]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_update_redirection_details]
	@redirect_id int,
	@redirect_alias nvarchar(100),
	@zone_id int,
	@article_id int,
	@group_id int,
	@structure_description nvarchar(2000), 
	@permanent_redirection bit,
	@publisher_id uniqueidentifier
AS


set nocount on

declare @site_id int
select @site_id = s.site_id from dbo.cms_article_zones_revision as azr with (nolock) 
		left outer join dbo.cms_article_revision as ar with (nolock)on ar.article_id = azr.article_id AND azr.rev_id = ar.rev_id 
		left outer join dbo.cms_zones as z with (nolock) on z.zone_id = azr.zone_id
		left outer join dbo.cms_zone_groups as zg with (nolock) on zg.zone_group_id = z.zone_group_id
		left outer join dbo.cms_sites as s with (nolock) on s.site_id = zg.site_id 
where z.zone_id = @zone_id and azr.article_id = @article_id

if not exists(select * from dbo.cms_article_zones where article_id=@article_id AND zone_id=@zone_id)
begin
	--article deleted in that zone. not save
	select 'F' as rStat, '' as redirect_id, '' as created	 
end

if not exists(select r.* from dbo.cms_redirects r left join dbo.cms_articles a with (nolock) on a.article_id = r.article_id
	left join dbo.cms_zones z with (nolock) on z.zone_id = r.zone_id
	left join dbo.cms_zone_groups zg with (nolock) on z.zone_group_id = zg.zone_group_id
	left join dbo.cms_sites s with (nolock) on s.site_id = zg.site_id where r.redirect_id <> @redirect_id and r.redirect_alias = @redirect_alias and s.site_id = @site_id )
begin 
	--alias not duplicate..
	if exists(select * from dbo.cms_redirects where redirect_id = @redirect_id)
	begin
	
		-- update redirection
		update dbo.cms_redirects
		set
			redirect_alias = @redirect_alias,
			zone_id = @zone_id,
			article_id = @article_id,
			updated_by = @publisher_id,
			updated = getDate(),
			group_id = @group_id,
			structure_description = @structure_description,
			permanent_redirection = @permanent_redirection
		where
			redirect_id = @redirect_id
		
		select 'U' as rStat, @redirect_id as redirect_id, getDate() as created
	
	end
	else
	begin
		--insert redirection
		insert into dbo.cms_redirects
		(redirect_alias, article_id, zone_id, publisher_id, updated_by,group_id, structure_description, permanent_redirection)
		values
		(@redirect_alias, @article_id, @zone_id, @publisher_id, @publisher_id,@group_id, @structure_description, @permanent_redirection)
	
		select 'I' as rStat, scope_identity() as redirect_id, getDate() as created
	
	end
end
else
begin
	--duplicate alias found.. return error
	select 'D' as rStat, '' as redirect_id, '' as created
end
set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_redirections]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_update_redirections]
	@ID int, 
	@RedirectFrom varchar(MAX), 
	@RedirectTo varchar(250), 
	@Publisher uniqueidentifier
AS


set nocount on

if exists(select * from dbo.cms_page_redirection where ID = @ID)
begin
	update dbo.cms_page_redirection
	set
		RedirectFrom = @RedirectFrom, 
		RedirectTo = @RedirectTo,
		Updated = getDate(), 
		UpdatedBy = @Publisher
	where
		ID = @ID		
	
	select 'U' as dStat, @ID as ID, getDate() as Updated
end
else
begin
	insert into dbo.cms_page_redirection
	(RedirectFrom, RedirectTo, CreatedBy, UpdatedBy)
	values
	(@RedirectFrom, @RedirectTo, @Publisher, @Publisher)

	select 'I' as dStat, scope_identity() as ID, getDate() as Updated
end

set nocount off






GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_rss_channel]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_update_rss_channel]
	@channel_id int, 
	@channel_name nvarchar(100), 
	@channel_status char(1),
	@url nvarchar(300), 
	@description nvarchar(300), 
	@lang_id varchar(5), 
	@managing_editor nvarchar(100), 
	@copyright nvarchar(100), 
	@publisher_id uniqueidentifier,
	@group_id int,
	@structure_description nvarchar(2000),
	@summary_content_field varchar(50),
	@content_template ntext,
	@content_template_editor_type char(1),
	@singularize_articles char(1)
as


set nocount on

if not exists(select * from dbo.cms_rss_channels where channel_id <> @channel_id and channel_name = @channel_name and channel_status <> 'D')
begin 
	--name not duplicate..
	if exists(select * from dbo.cms_rss_channels where channel_id = @channel_id)
	begin
	
		-- update channel
		update dbo.cms_rss_channels
		set
		        channel_name = @channel_name, 
		        channel_status = @channel_status, 
		        url = @url, 
		        [description] = @description, 
		        lang_id = @lang_id, 
		        managing_editor = @managing_editor, 
		        copyright = @copyright, 
		        updated = getDate(), 
		        updated_by = @publisher_id,
		        group_id = @group_id,
		        structure_description = @structure_description,
		        summary_content_field = @summary_content_field,
		        content_template = @content_template,
		        content_template_editor_type = @content_template_editor_type,
		        singularize_articles = @singularize_articles
		where
			channel_id = @channel_id
		
		select 'U' as cStat, @channel_id as channel_id, getDate() as created
	
	end
	else
	begin
		--insert channel
		insert into dbo.cms_rss_channels
		(channel_name, url, channel_status, [description], lang_id, managing_editor, copyright, created_by, updated_by, group_id, structure_description, summary_content_field, content_template, content_template_editor_type, singularize_articles)
		values
		(@channel_name, @url, @channel_status, @description, @lang_id, @managing_editor, @copyright, @publisher_id, @publisher_id, @group_id, @structure_description, @summary_content_field, @content_template, @content_template_editor_type, @singularize_articles)
	
		select 'I' as cStat, cast(scope_identity() as int) as channel_id, getDate() as created
	
	end
end
else
begin
	--duplicate name found.. return error
	select 'D' as cStat, 0 as channel_id, null as created
end
set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_site]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_update_site]
	@site_id int,
	@site_name nvarchar(100),
	@css_id int,
	@css_id_mobile int,
	@css_id_print int,
	@template_id int,
	@template_id_mobile int,
	@site_keywords nvarchar(500),
	@site_header ntext,
	@site_js ntext,
	@analytics ntext,
	@custom_body nvarchar(200),
	@site_icon nvarchar(100),
	@tag_detail_article varchar(50),
	@article_1 ntext,
	@article_2 ntext,
	@article_3 ntext,
	@article_4 ntext,
	@article_5 ntext,
	@publisher_id uniqueidentifier,
	@group_id int,
	@structure_description nvarchar(2000),
	@meta_description nvarchar(2000),
	@content_1_editor_type char(1),
	@content_2_editor_type char(1),
	@content_3_editor_type char(1),
	@content_4_editor_type char(1),
	@content_5_editor_type char(1),
	@default_article varchar(20),
	@omniture_code ntext,
	@domain_id int
AS

set nocount on

 if not exists(select * from dbo.cms_sites where site_id <> @site_id and site_name = @site_name)
begin 
	--name not duplicate..
	if exists(select * from dbo.cms_sites where site_id = @site_id)
	begin
		update dbo.cms_sites
		set
			site_name = @site_name,
			css_id = @css_id,
			css_id_mobile = @css_id_mobile,
			css_id_print = @css_id_print,
			template_id = @template_id,
			template_id_mobile = @template_id_mobile,
			publisher_id = @publisher_id,
			site_keywords = @site_keywords,
			site_header = @site_header,
			site_js = @site_js,
			analytics = @analytics,
			custom_body = @custom_body,
			tag_detail_article = @tag_detail_article,
			site_icon = @site_icon,
			article_1 = @article_1,
			article_2 = @article_2,
			article_3 = @article_3,
			article_4 = @article_4,
			article_5 = @article_5,
			updated = getDate(),
			group_id = @group_id,
			structure_description = @structure_description,
			meta_description = @meta_description,
			content_1_editor_type = @content_1_editor_type,
			content_2_editor_type = @content_2_editor_type,
			content_3_editor_type = @content_3_editor_type,
			content_4_editor_type = @content_4_editor_type,
			content_5_editor_type = @content_5_editor_type,
			default_article = @default_article,
			omniture_code = @omniture_code,
			domain_id = @domain_id
		where
			site_id = @site_id
		select 'U' as sStat, @site_id as site_id, getDate() as created
	
	end
	else
	begin
	
		insert into dbo.cms_sites
		(site_name, css_id, css_id_mobile, css_id_print, template_id, template_id_mobile, publisher_id, site_keywords, site_header, site_js, analytics, custom_body, site_icon, tag_detail_article, article_1, article_2, article_3, article_4, article_5, group_id, structure_description, meta_description, content_1_editor_type, content_2_editor_type, content_3_editor_type, content_4_editor_type, content_5_editor_type, default_article, omniture_code, domain_id)
		values
		(@site_name, @css_id, @css_id_mobile, @css_id_print, @template_id, @template_id_mobile, @publisher_id, @site_keywords, @site_header, @site_js, @analytics, @custom_body, @site_icon, @tag_detail_article, @article_1, @article_2, @article_3, @article_4, @article_5, @group_id, @structure_description, @meta_description, @content_1_editor_type, @content_2_editor_type, @content_3_editor_type, @content_4_editor_type, @content_5_editor_type, @default_article, @omniture_code, @domain_id)
	
		select 'I' as sStat, scope_identity() as site_id, getDate() as created
	end
end
 
set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_sitemap]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[cms_asp_admin_update_sitemap]
	@smap_id int,
	@domain_id int,
	@domain_alias varchar(255),
	@notify_google char(1),
	@notify_msn char(1),
	@notify_ask char(1),
	@notify_yahoo char(1),
	@yahoo_id varchar(50),
	@included_sites text,
	@excluded_zonegroups text,
	@excluded_zones text,
	@excluded_articles text,
	@afiles char(1),
	@interval int,
	@enabled char(1),
	@gzip_enabled char(1),
	@pubID uniqueidentifier

AS
set nocount on



if @smap_id = -1
begin
	INSERT INTO cms_sitemaps
		(
		domain_id,
		domain_alias,
		status,
		notify_google,
		notify_msn,
		notify_ask,
		notify_yahoo,
		yahoo_id,
		included_sites,
		excluded_zonegroups,
		excluded_zones,
		excluded_articles,
		afiles,
		interval,
		enabled,
		created_by,
		gzip_enabled
		)
	VALUES
		(
		@domain_id,
		@domain_alias,
		1,
		@notify_google,
		@notify_msn,
		@notify_ask,
		@notify_yahoo,
		@yahoo_id,
		@included_sites,
		@excluded_zonegroups,
		@excluded_zones,
		@excluded_articles,
		@afiles,
		@interval,
		@enabled,
		@pubID,
		@gzip_enabled
		)
	Select 'CREATED' as rCode
	return
end

else
begin
	UPDATE
		dbo.cms_sitemaps
	SET
		domain_id = @domain_id,
		domain_alias = @domain_alias, 
		status = 1,
		notify_google = @notify_google,
		notify_msn = @notify_msn,
		notify_ask = @notify_ask,
		notify_yahoo = @notify_yahoo,
		yahoo_id = @yahoo_id,
		included_sites = @included_sites,
		excluded_zonegroups = @excluded_zonegroups,
		excluded_zones = @excluded_zones,
		excluded_articles = @excluded_articles,
		afiles = @afiles,
		interval = @interval,
		enabled = @enabled,
		gzip_enabled = @gzip_enabled,
		updated = getdate(),
		updated_by = @pubID
	WHERE
		smap_id = @smap_id
	Select 'UPDATED' as rCode
	return
end

set nocount off

delete from cms_language_relations_revision





GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_sitemap_gz]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_update_sitemap_gz]
	@smap_id int,
	@gz image
AS
set nocount on

if exists(select * from dbo.cms_sitemaps WHERE smap_id = @smap_id)
begin
	if (select status from dbo.cms_sitemaps WHERE smap_id = @smap_id) = 0
	begin
		UPDATE
			dbo.cms_sitemaps
		SET
			gz = @gz
		WHERE
			smap_id = @smap_id
		Select 'UPDATED' as rCode
		return
	end
end

else
begin
	Select 'NOT_EXIST' as rCode, getdate() as rDate
end

set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_sitemap_status]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_update_sitemap_status]
	@smap_id int,
	@status tinyint,
	@xml ntext
AS
set nocount on

Declare @now datetime

if exists(select * from dbo.cms_sitemaps WHERE smap_id = @smap_id)
begin
	Set @now = getdate()
	if @status = 0
	begin
		UPDATE
			dbo.cms_sitemaps
		SET
			status = 0,
			last_update = @now,
			last_generate = @now,
			[xml] = @xml
		WHERE
			smap_id = @smap_id
		Select 'GENERATED' as rCode, @now as rDate
		return
	end

	if @status = 1 Or @status = 2
	begin
		UPDATE
			dbo.cms_sitemaps
		SET
			status = @status,
			last_update = @now
		WHERE
			smap_id = @smap_id
		Select 'UPDATED' as rCode, @now as rDate
		return
	end
end

else
begin
	Select 'NOT_EXIST' as rCode, getdate() as rDate
end

set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_stf_template]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_update_stf_template]
	@stft_id int,
	@stft_name nvarchar(100),
	@stft_form_html ntext,
	@stft_thanks nvarchar(200),
	@stft_mail_html ntext,
	@stft_mail_subject nvarchar(100),
	@stft_mail_from_name nvarchar(100),
	@stft_wh varchar(20),
	@omniture_function varchar(500),
	@created_by uniqueidentifier
AS

set nocount on

if not exists(select * from dbo.cms_stf_templates where stft_id <> @stft_id and stft_name = @stft_name and stft_status <> 'D')
begin 
	--name not duplicate..
	if exists(select * from dbo.cms_stf_templates where stft_id = @stft_id)
	begin
	
		-- update template
		update dbo.cms_stf_templates
		set
			stft_name = @stft_name,
			stft_form_html = @stft_form_html,
			stft_thanks = @stft_thanks,
			stft_mail_html = @stft_mail_html,
			updated_by = @created_by,
			stft_mail_subject = @stft_mail_subject,
			stft_mail_from_name = @stft_mail_from_name,
			stft_wh = @stft_wh,
			omniture_function = @omniture_function,
			updated = getDate()
		where
			stft_id = @stft_id
		
		select 'U' as tStat, @stft_id as stft_id, getDate() as created
	
	end
	else
	begin
		--insert template
		insert into dbo.cms_stf_templates
		(stft_name, stft_form_html, stft_thanks, stft_mail_html, stft_mail_subject, stft_mail_from_name, stft_wh, omniture_function, created_by, updated_by)
		values
		(@stft_name, @stft_form_html, @stft_thanks, @stft_mail_html, @stft_mail_subject, @stft_mail_from_name, @stft_wh, @omniture_function, @created_by, @created_by)
	
		select 'I' as tStat, scope_identity() as stft_id, getDate() as created
	
	end
end
else
begin
	--duplicate name found.. return error
	select 'D' as tStat, '' as stft_id, '' as created
end

set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_structure_group]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_admin_update_structure_group]
	@publisher_id uniqueidentifier,
	@group_id int,
	@group_name nvarchar(50),
	@group_type tinyint
AS

set nocount on
if exists(select * from dbo.cms_structure_groups with (nolock) where group_id <> @group_id AND group_name = @group_name AND group_type = @group_type)
begin
	Select 'ALREADYHAVE' as rCode
end

else
begin
	if not exists(select * from dbo.cms_structure_groups with (nolock) where group_id = @group_id)
	begin
		Insert Into dbo.cms_structure_groups(group_name, group_type, created_by, created) Values(@group_name, @group_type, @publisher_id, getdate())
	
		Select 'INSERTED' as rCode
	end
	
	else
	begin
		Update dbo.cms_structure_groups
		Set group_name = @group_name, group_type = @group_type
		Where group_id = @group_id
	
		Select 'UPDATED' as rCode
	end
end

set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_template]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_update_template]
	@template_id int,
	@template_name nvarchar(100),
	@template_html ntext,
	@template_type tinyint,
	@publisher_id uniqueidentifier,
	@group_id int,
	@structure_description nvarchar(2000),
	@content_1_editor_type char(1),
	@template_doctype varchar(1000)
AS

set nocount on

if not exists(select * from dbo.cms_templates where template_id <> @template_id and template_name = @template_name  and template_status <> 'D')
begin 
	--name not duplicate..
	if exists(select * from dbo.cms_templates where template_id = @template_id)
	begin
		--copy to revision history first..
		INSERT INTO dbo.cms_template_revisions
		(template_id, template_html, publisher_id, template_type, content_1_editor_type, template_doctype)
			select template_id, template_html, publisher_id, template_type, content_1_editor_type, template_doctype
			from dbo.cms_templates
			where template_id = @template_id
	
		-- update template
		update dbo.cms_templates
		set
			template_name = @template_name,
			template_html = @template_html,
			publisher_id = @publisher_id,
			template_type = @template_type,
			updated = getDate(),
			group_id = @group_id,
			structure_description = @structure_description,
			content_1_editor_type = @content_1_editor_type,
			template_doctype = @template_doctype
		where
			template_id = @template_id
		
		select 'U' as tStat, @template_id as template_id, getDate() as created
	
	end
	else
	begin
		--insert template
		insert into dbo.cms_templates
		(template_name, template_html, publisher_id, template_type, group_id, structure_description, content_1_editor_type, template_doctype)
		values
		(@template_name, @template_html, @publisher_id, @template_type, @group_id, @structure_description, @content_1_editor_type, @template_doctype)
	
		select 'I' as tStat, scope_identity() as template_id, getDate() as created
	
	end
end
else
begin
	--duplicate name found.. return error
	select 'D' as tStat, '' as template_id, '' as created
end

set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_xml_details]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_update_xml_details]
    @xml_id int, 
    @xml_name nvarchar(100), 
    @xml_main_node nvarchar(50), 
    @xml_main_node_attrib nvarchar(200), 
    @xml_per_node nvarchar(50), 
    @xml_per_node_attrib nvarchar(200), 
    @xml_sub_node nvarchar(200), 
    @xml_sub_template int,
    @xml_level int,
    @xml_related_line nvarchar(200),
    @xml_xml ntext, 
    @created_by uniqueidentifier,
    @group_id int,
    @structure_description nvarchar(2000)
as


set nocount on

if not exists(select * from dbo.cms_xml where xml_id <> @xml_id and xml_name = @xml_name)
begin 
	--name not duplicate..
	if exists(select * from dbo.cms_xml where xml_id = @xml_id)
	begin

		update dbo.cms_xml
		set 
		        xml_name = @xml_name, 
		        xml_main_node = @xml_main_node, 
		        xml_main_node_attrib = @xml_main_node_attrib, 
		        xml_per_node = @xml_per_node, 
		        xml_per_node_attrib = @xml_per_node_attrib, 
		        xml_sub_node = @xml_sub_node, 
		        xml_sub_template = @xml_sub_template,
		        xml_level = @xml_level,
		        xml_related_line = @xml_related_line,
		        xml_xml = @xml_xml,
		        group_id = @group_id,
		        structure_description = @structure_description
		where
			xml_id = @xml_id

		select 'U' as xStat, @xml_id as xml_id, getDate() as created

	end
	else
	begin
		insert into dbo.cms_xml
		(xml_name, xml_main_node, xml_main_node_attrib, xml_per_node, xml_per_node_attrib, xml_sub_node, xml_sub_template, xml_level, xml_related_line, xml_xml, created_by, group_id, structure_description)
		values
		(@xml_name, @xml_main_node, @xml_main_node_attrib, @xml_per_node, @xml_per_node_attrib, @xml_sub_node, @xml_sub_template, @xml_level, @xml_related_line, @xml_xml, @created_by, @group_id, @structure_description)

		select 'I' as xStat, scope_identity() as xml_id, getDate() as created
	end
end
else
begin
	--duplicate name found.. return error
	select 'D' as xStat, '' as xml_id, '' as created
end
set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_zone_revision]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_update_zone_revision]
	@rev_id bigint,
	@rev_name nvarchar(100), 
	@rev_note ntext, 
	@zone_id int,
	@zone_group_id int,
	@zone_type_id int,
	@zone_status char(1),
	@zone_name nvarchar(100),
	@zone_desc nvarchar(200),
	@css_merge int,
	@css_id int,
	@css_id_mobile int,
	@css_id_print int,
	@template_id int,
	@template_id_mobile int,
	@custom_body nvarchar(200),
	@zone_keywords nvarchar(500),
	@article_1 ntext,
	@article_2 ntext,
	@article_3 ntext,
	@article_4 ntext,
	@article_5 ntext,
	@append_1 tinyint,
	@append_2 tinyint,
	@append_3 tinyint,
	@append_4 tinyint,
	@append_5 tinyint,
	@analytics nvarchar(500),
	@revised_by uniqueidentifier,
	@meta_description nvarchar(2000),
	@zone_name_display nvarchar(200),
	@cio char(1), --Zone Check-In Check-Out parameter
	@content_1_editor_type char(1),
	@content_2_editor_type char(1),
	@content_3_editor_type char(1),
	@content_4_editor_type char(1),
	@content_5_editor_type char(1),
	@default_article varchar(20),
	@omniture_code NTEXT,
	@lang_id CHAR(2)
AS
declare @zstat char(1)
declare @rstat char(1)
declare @locked datetime
declare @locked_by nvarchar(100)

set @zstat = ''
set @rstat = ''

set nocount on


if not exists(select * from dbo.cms_zones where zone_id <> @zone_id and zone_name = @zone_name and zone_group_id = @zone_group_id and zone_status <> 'D')
begin 
	--name not duplicate..


	if @zone_id = -1
	begin
		--Zone not exists. Create zone with passive status
		insert into dbo.cms_zones
		(zone_group_id, zone_type_id, zone_status, zone_name, zone_desc, publisher_id)
		values
		(@zone_group_id, @zone_type_id, 'P', @zone_name, @zone_desc, @revised_by)
		
		set @zone_id = scope_identity()
		set @zstat = 'C'
	end
	
	if @rev_id = -1
	begin
		--Revision not exist. Create revision
		insert into dbo.cms_zone_revision
		(rev_name, rev_note, zone_id, zone_group_id, zone_type_id, zone_status, zone_name, zone_desc, css_merge, css_id, css_id_mobile, css_id_print, template_id, template_id_mobile, custom_body, zone_keywords, article_1, article_2, article_3, article_4, article_5, append_1, append_2, append_3, append_4, append_5, analytics, revised_by,created_by, meta_description, zone_name_display, content_1_editor_type, content_2_editor_type, content_3_editor_type, content_4_editor_type, content_5_editor_type, default_article, omniture_code, lang_id)
		values
		(@rev_name, @rev_note, @zone_id, @zone_group_id, @zone_type_id, @zone_status, @zone_name, @zone_desc, @css_merge, @css_id, @css_id_mobile, @css_id_print, @template_id, @template_id_mobile, @custom_body, @zone_keywords, @article_1, @article_2, @article_3, @article_4, @article_5, @append_1, @append_2, @append_3, @append_4, @append_5, @analytics, @revised_by, @revised_by, @meta_description, @zone_name_display, @content_1_editor_type, @content_2_editor_type, @content_3_editor_type, @content_4_editor_type, @content_5_editor_type, @default_article, @omniture_code, @lang_id)
	
		set @rev_id = scope_identity()
		set @rstat = 'C'
	end
	else
	begin
		--Check for article lock
		if not exists(select * from dbo.cms_zones with (nolock) where zone_id = @zone_id and locked_by = @revised_by) AND @cio = '1'
		begin
			select @locked = a.locked, @locked_by = p.UserName from dbo.cms_zones as a with (nolock) left outer join dbo.vw_aspnet_MembershipUsers as p  with (nolock) on a.locked_by = p.UserId where zone_id = @zone_id
			set @rstat = 'L'
			select @zstat as zstat, @rstat as rstat, @zone_id as zone_id, @rev_id as rev_id, @locked as locked, @locked_by as locked_by
			return
		end

		--Revision exist. Check status
		if exists(select * from dbo.cms_zone_revision where rev_id = @rev_id and revision_status = 'N' )
		begin
			--Revision not approved yet.. Update it
			update dbo.cms_zone_revision
			set
			rev_name = @rev_name,
			rev_note = @rev_note,
			zone_id = @zone_id,
			zone_group_id = @zone_group_id,
			zone_type_id = @zone_type_id,
			zone_status = @zone_status,
			zone_name = @zone_name,
			zone_desc = @zone_desc,
			css_merge = @css_merge,
			css_id = @css_id,
			css_id_mobile = @css_id_mobile,
			css_id_print = @css_id_print,
			template_id = @template_id,
			template_id_mobile = @template_id_mobile,
			custom_body = @custom_body,
			zone_keywords = @zone_keywords,
			article_1 = @article_1,
			article_2 = @article_2,
			article_3 = @article_3,
			article_4 = @article_4,
			article_5 = @article_5,
			append_1 = @append_1,
			append_2 = @append_2,
			append_3 = @append_3,
			append_4 = @append_4,
			append_5 = @append_5,
			analytics = @analytics,
			revised_by = @revised_by,
			rev_date = getDate(),
			meta_description = @meta_description,
			zone_name_display = @zone_name_display,
			content_1_editor_type = @content_1_editor_type,
			content_2_editor_type = @content_2_editor_type,
			content_3_editor_type = @content_3_editor_type,
			content_4_editor_type = @content_4_editor_type,
			content_5_editor_type = @content_5_editor_type,
			default_article = @default_article,
			omniture_code = @omniture_code,
			lang_id = @lang_id
			where
			rev_id = @rev_id
	
			set @rstat = 'U'
	
		end
		else
		begin
			--Revision already approved or discarded. Save as new revision
			insert into dbo.cms_zone_revision
			(rev_name, rev_note, zone_id, zone_group_id, zone_type_id, zone_status, zone_name, zone_desc, css_merge, css_id, css_id_mobile, css_id_print, template_id, template_id_mobile, custom_body, zone_keywords, article_1, article_2, article_3, article_4, article_5, append_1, append_2, append_3, append_4, append_5, analytics, revised_by, created_by, meta_description, zone_name_display, content_1_editor_type, content_2_editor_type, content_3_editor_type, content_4_editor_type, content_5_editor_type, default_article, omniture_code, lang_id)
			values
			(@rev_name, @rev_note, @zone_id, @zone_group_id, @zone_type_id, @zone_status, @zone_name, @zone_desc, @css_merge, @css_id, @css_id_mobile, @css_id_print, @template_id, @template_id_mobile, @custom_body, @zone_keywords, @article_1, @article_2, @article_3, @article_4, @article_5, @append_1, @append_2, @append_3, @append_4, @append_5, @analytics, @revised_by, @revised_by, @meta_description, @zone_name_display, @content_1_editor_type, @content_2_editor_type, @content_3_editor_type, @content_4_editor_type, @content_5_editor_type, @default_article, @omniture_code, @lang_id)
		
			set @rev_id = scope_identity()
			set @rstat = 'N'
		end
	end
	 
	select @zstat as zstat, @rstat as rstat, @zone_id as zone_id, @rev_id as rev_id
end
else
begin
	--name duplicate.. return error
	select 'D' as zStat, '' as rStat, -1 as zone_id, cast(-1 as bigint) as rev_id
end


set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_zone_revision_status]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_update_zone_revision_status]
	@rev_id int,
	@revision_status char(1),
	@approval_id uniqueidentifier
AS


set nocount on

--check permissions!



-- check current status
if exists(select * from dbo.cms_zone_revision with (nolock) where rev_id = @rev_id and revision_status in ('N','A','W') )
begin
	-- can be changed
	update dbo.cms_zone_revision
	set
		revision_status = @revision_status,
		approval_date = getDate(),
		approval_id = @approval_id
	where
		rev_id = @rev_id

	-- mark instant messages as processed for this approval
	if @revision_status = 'A'
	begin
		update dbo.cms_instant_messaging
		set processed = getDate()
		where ims_type = 'ZA' and ims_to = @approval_id and related_id = @rev_id
	end


	select 'OK' as rCode
end
else
begin
	select 'STATUS' as rCode
end



set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_admin_update_zonegroups]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_admin_update_zonegroups]
	@zone_group_id int,
	@zone_group_name nvarchar(100),
	@zone_group_keywords nvarchar(500), 
	@analytics nvarchar(500), 
	@site_id int,
	@css_merge int,
	@css_id int,
	@css_id_mobile int,
	@css_id_print int,
	@template_id int,
	@template_id_mobile int,
	@custom_body nvarchar(200),
	@tag_detail_article varchar(50),
	@article_1 ntext,
	@article_2 ntext,
	@article_3 ntext,
	@article_4 ntext,
	@article_5 ntext,
	@append_1 tinyint,
	@append_2 tinyint,
	@append_3 tinyint,
	@append_4 tinyint,
	@append_5 tinyint,
	@publisher_id uniqueidentifier,
	@meta_description nvarchar(2000),
	@zone_group_name_display nvarchar(200),
	@content_1_editor_type char(1),
	@content_2_editor_type char(1),
	@content_3_editor_type char(1),
	@content_4_editor_type char(1),
	@content_5_editor_type char(1),
	@default_article varchar(20),
	@omniture_code ntext
AS

declare @site_name nvarchar(100)

select @site_name = site_name from dbo.cms_sites with (nolock) where site_id = @site_id

set nocount on


if not exists(select * from dbo.cms_zone_groups where zone_group_id <> @zone_group_id and zone_group_name = @zone_group_name and site_id = @site_id)
begin 
	--name not duplicate..
	if exists(select * from dbo.cms_zone_groups where zone_group_id = @zone_group_id)
	begin
		--zone group exist.. update

		update dbo.cms_zone_groups
		set
			zone_group_name = @zone_group_name,
			css_merge = @css_merge,
			site_id = @site_id,
			css_id = @css_id,
			css_id_mobile = @css_id_mobile,
			css_id_print = @css_id_print,
			template_id = @template_id,
			template_id_mobile = @template_id_mobile,
			custom_body = @custom_body,
			zone_group_keywords = @zone_group_keywords,
			analytics = @analytics,
			tag_detail_article = @tag_detail_article,
			article_1 = @article_1,
			article_2 = @article_2,
			article_3 = @article_3,
			article_4 = @article_4,
			article_5 = @article_5,
			append_1 = @append_1,
			append_2 = @append_2,
			append_3 = @append_3,
			append_4 = @append_4,
			append_5 = @append_5,
			updated = getDate(),
			meta_description = @meta_description,
			zone_group_name_display = @zone_group_name_display,
			content_1_editor_type = @content_1_editor_type,
			content_2_editor_type = @content_2_editor_type,
			content_3_editor_type = @content_3_editor_type,
			content_4_editor_type = @content_4_editor_type,
			content_5_editor_type = @content_5_editor_type,
			default_article = @default_article,
			omniture_code = @omniture_code
		where
			zone_group_id = @zone_group_id
	
		select 'U' as zgStat, @zone_group_id as zone_group_id, @site_name as site_name, getDate() as created
	end
	else
	begin
		insert into dbo.cms_zone_groups
		(zone_group_name, css_merge, site_id, css_id, css_id_mobile, css_id_print, template_id, template_id_mobile, custom_body, publisher_id, zone_group_keywords, analytics, tag_detail_article, article_1, article_2, article_3, article_4, article_5, append_1, append_2, append_3, append_4, append_5, meta_description, zone_group_name_display, content_1_editor_type, content_2_editor_type, content_3_editor_type, content_4_editor_type, content_5_editor_type, default_article, omniture_code)
		values
		(@zone_group_name, @css_merge, @site_id, @css_id, @css_id_mobile, @css_id_print, @template_id, @template_id_mobile, @custom_body, @publisher_id, @zone_group_keywords, @analytics, @tag_detail_article, @article_1, @article_2, @article_3, @article_4, @article_5, @append_1, @append_2, @append_3, @append_4, @append_5, @meta_description, @zone_group_name_display, @content_1_editor_type, @content_2_editor_type, @content_3_editor_type, @content_4_editor_type, @content_5_editor_type, @default_article, @omniture_code)
	
		select 'I' as zgStat, scope_identity() as zone_group_id, @site_name as site_name, getDate() as created
	end

end
else
begin
	--duplicate name found.. return error
	select 'D' as zgStat, '' as zone_group_id, '' as site_name, '' as created
end

set nocount on







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_approval_approve_article_files_revision]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_approval_approve_article_files_revision]
	@rev_id bigint,
	@approve_level int,
	@publisher_id uniqueidentifier,
	@publisher_level int
as

declare @af_rf_id as bigint

set nocount on
if not exists (select * from dbo.cms_article_files_revision with (nolock) where rev_id = @rev_id and revision_status  in ('N','A','W') )
begin
	-- revision not available for approval
	select 'NOT_AVAILABLE' as aStat, '' as found_name
	return
end


declare @revision_status as char(1)
declare @article_id as int

select @revision_status = revision_status, @article_id = article_id  from dbo.cms_article_files_revision with (nolock) where rev_id = @rev_id



begin transaction

-- update only status if not an admin
if @publisher_level < 100 and @approve_level = 3
begin
	update dbo.cms_article_files_revision
	set
		revision_status = 'A',
		approval_date = getDate(),
		approval_id = @publisher_id
	where
		rev_id = @rev_id
	
	if(@@error <> 0) goto RollbackAndReturn

	select 'OKA' as aStat
end
else
begin
	-- set old revision to Ex-Live
	update dbo.cms_article_files_revision 
	set
		revision_status = 'E'
	where
		revision_status = 'L' and 
		article_id = @article_id

	if(@@error <> 0) goto RollbackAndReturn

	-- mark revision as approved
	update dbo.cms_article_files_revision
	set
		revision_status = 'L',
		approval_date = getDate(),
		approval_id = @publisher_id
	where
		rev_id = @rev_id
	
	if(@@error <> 0) goto RollbackAndReturn

	delete dbo.cms_article_files where article_id = @article_id

	if(@@error <> 0) goto RollbackAndReturn
	

	INSERT INTO dbo.cms_article_files
		(article_id,  file_title, file_order, file_name_1, file_name_2, file_name_3, file_name_4, file_name_5, file_name_6, file_name_7, file_name_8, file_name_9, file_name_10, file_type_id, file_comment)
			select article_id,  file_title, file_order, file_name_1, file_name_2, file_name_3, file_name_4, file_name_5, file_name_6, file_name_7, file_name_8, file_name_9, file_name_10, file_type_id, file_comment
			from dbo.cms_article_files_revision_files
			where rev_id = @rev_id

	if(@@error <> 0) goto RollbackAndReturn

	select 'OK' as aStat


end


commit transaction
return


RollbackAndReturn:
rollback transaction
select @@ERROR as aStat
return

set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_approval_approve_article_revision]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_approval_approve_article_revision]
	@rev_id bigint,
	@approve_level int,
	@publisher_id uniqueidentifier,
	@publisher_level int,
	@cio char(1) --Article Check-In Check-Out parameter
as

declare @article_id int
--declare @target_status int
declare @locked datetime
declare @locked_by nvarchar(100)
declare @revision_status char(1)
declare @article_status INT
declare @navigation_zone_id INT
DECLARE @article_revision_article_type_detail NVARCHAR(500)
DECLARE @article_revision_article_type TINYINT


set nocount on

if not exists (select * from dbo.cms_article_revision with (nolock) where rev_id = @rev_id and revision_status in ('N','A','W') )
begin
	-- revision not available for approval
	select 'NOT_AVAILABLE' as aStat
	return
end

-- read article id from revision
select @article_id = article_id, @revision_status = revision_status, @article_status = status, @article_revision_article_type_detail = article_type_detail, @article_revision_article_type = article_type, @navigation_zone_id = navigation_zone_id from dbo.cms_article_revision with (nolock) where rev_id = @rev_id

-- check zone existance for this revision
if not exists(select * from dbo.cms_article_zones_revision cazr with (nolock) left join dbo.cms_zones cz with (nolock) on cz.zone_id = cazr.zone_id where cazr.rev_id = @rev_id and cz.zone_status <> 'D')
begin
	-- revision not available for approval because zone not found
	select 'ZONE_NOT_FOUND' as aStat
	return
end


--Check for article lock
if not exists(select * from dbo.cms_articles with (nolock) where article_id = @article_id and (locked_by = @publisher_id Or locked_by is null)) AND @cio = '1'
begin
	select @locked = a.locked, @locked_by = p.UserName from dbo.cms_articles as a with (nolock) left outer join dbo.vw_aspnet_MembershipUsers as p  with (nolock) on a.locked_by = p.UserId where article_id = @article_id
	select 'LOCKED' as aStat, @locked as locked, @locked_by as locked_by
	return
end

--check for delete approval
if @article_status = 2 
begin
	--check for domain homepage article
	if exists(select * from dbo.cms_domains with (nolock) where home_page_article like '%-' + CAST(@article_id AS VARCHAR(20)))
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_DOMAIN_HOME_PAGE' as rStat, domain_names as tStat from dbo.cms_domains with (nolock) where home_page_article like '%-' + CAST(@article_id AS VARCHAR(20))
		return
	end

	--check for domain 404 article
	if exists(select * from dbo.cms_domains with (nolock) where error_page_article like '%-' + CAST(@article_id AS VARCHAR(20)))
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_DOMAIN_404_PAGE' as rStat, domain_names as tStat from dbo.cms_domains with (nolock) where home_page_article like '%-' + CAST(@article_id AS VARCHAR(20))
		return
	end

	--check for alias redirections
	if exists(select * from dbo.cms_redirects with (nolock) where article_id = @article_id)
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_ALIAS_REDIRECTION' as rStat, redirect_alias as tStat from dbo.cms_redirects with (nolock) where article_id = @article_id
		return
	end

	--check for site default article
	if exists(select * from dbo.cms_sites with (nolock) where default_article like '%-' + CAST(@article_id AS VARCHAR(20)))
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_SITE_DEFAULT_ARTICLE' as rStat, site_name as tStat from dbo.cms_sites with (nolock) where default_article like '%-' + CAST(@article_id AS VARCHAR(20))
		return
	end

	--check for site tag detail article
	if exists(select * from dbo.cms_sites with (nolock) where tag_detail_article like '%-' + CAST(@article_id AS VARCHAR(20)))
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_SITE_TAG_DETAIL_ARTICLE' as rStat, site_name as tStat from dbo.cms_sites with (nolock) where tag_detail_article like '%-' + CAST(@article_id AS VARCHAR(20))
		return
	end

	--check for zone group default article
	if exists(select * from dbo.cms_zone_groups with (nolock) where default_article like '%-' + CAST(@article_id AS VARCHAR(20)))
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_ZONE_GROUP_DEFAULT_ARTICLE' as rStat, zone_group_name as tStat from dbo.cms_zone_groups with (nolock) where default_article like '%-' + CAST(@article_id AS VARCHAR(20))
		return
	end

	--check for zone group tag detail article
	if exists(select * from dbo.cms_zone_groups with (nolock) where tag_detail_article like '%-' + CAST(@article_id AS VARCHAR(20)))
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_ZONE_GROUP_TAG_DETAIL_ARTICLE' as rStat, zone_group_name as tStat from dbo.cms_zone_groups with (nolock) where tag_detail_article like '%-' + CAST(@article_id AS VARCHAR(20))
		return
	end

	--check for zone default article
	if exists(select * from dbo.cms_zones with (nolock) where default_article like '%-' + CAST(@article_id AS VARCHAR(20)))
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_ZONE_DEFAULT_ARTICLE' as rStat, zone_name as tStat from dbo.cms_zones with (nolock) where default_article like '%-' + CAST(@article_id AS VARCHAR(20))
		return
	end

	--check for article language relation
	
	if exists(select * from dbo.cms_language_relations with (nolock) where article_id = @article_id)
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_LANGUAGE_RELATION' as rStat, CAST(article_id AS VARCHAR(20)) + '-' + (select headline from dbo.cms_articles with (nolock) where article_id = alr.article_id) as tStat from dbo.cms_language_relations alr with (nolock) where article_id = @article_id
		return
	end

	--check for article language relation reverse
	--if exists(select * from dbo.cms_language_relations with (nolock) where related_article_id = @article_id)
	--begin
	--	select 'CANT_DELETE' as aStat, 'USED_IN_LANGUAGE_RELATION' as rStat, CAST(article_id AS VARCHAR(20)) + '-' + (select headline from dbo.cms_articles with (nolock) where article_id = alr.article_id) as tStat from dbo.cms_language_relations alr with (nolock) where related_article_id = @article_id
	--	return
	--end

	--check for internal article redirection
	if exists(select * from dbo.cms_articles with (nolock) where article_type_detail like '%-' + CAST(@article_id AS VARCHAR(20)) AND article_type = 2 AND status=1)
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_INTERNAL_ARTICLE_REDIRECTION' as rStat, CAST(article_id AS VARCHAR(20)) + '-' + headline as tStat from dbo.cms_articles with (nolock) where article_type_detail like '%-' + CAST(@article_id AS VARCHAR(20)) AND article_type = 2 AND status=1
		return
	end

	--check for mapped article
	if exists(select * from dbo.cms_articles with (nolock) where article_type_detail like '%-' + CAST(@article_id AS VARCHAR(20)) AND article_type = 9 AND status=1)
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_MAPPED_ARTICLE' as rStat, CAST(article_id AS VARCHAR(20)) + '-' + headline as tStat from dbo.cms_articles with (nolock) where article_type_detail like '%-' + CAST(@article_id AS VARCHAR(20)) AND article_type = 9 AND status=1
		return
	end

end

--check relations for approval
if @article_status = 1
begin
	--check for language relation article is exist
	
	IF EXISTS(select article_id from dbo.cms_language_relations_revision with (nolock) where rev_id = @rev_id AND article_id NOT IN (SELECT article_id FROM dbo.cms_articles WITH (NOLOCK) WHERE status <> 2))
	begin
		select 'CANT_APPROVE' as aStat, 'LANGUAGE_RELATIONED_ARTICLE_NOT_FOUND' as rStat, a.headline AS tStat from dbo.cms_language_relations_revision alrr with (nolock) left join dbo.cms_articles a (nolock) on  a.article_id = alrr.article_id where rev_id = @rev_id AND alrr.article_id NOT IN (SELECT article_id FROM dbo.cms_articles WITH (NOLOCK) WHERE status <> 2)
		return
	end
	--check for related articles are exist
	
	IF EXISTS(select article_id from dbo.cms_article_relation_revision with (nolock) where rev_id = @rev_id AND related_article_id NOT IN (SELECT article_id FROM dbo.cms_articles WITH (NOLOCK) WHERE status <> 2))
	begin
		select 'CANT_APPROVE' as aStat, 'RELATIONED_ARTICLE_NOT_FOUND' as rStat, a.headline AS tStat from dbo.cms_article_relation_revision arr with (nolock) left join dbo.cms_articles a (nolock) on  a.article_id = arr.related_article_id where rev_id = @rev_id AND related_article_id NOT IN (SELECT article_id FROM dbo.cms_articles WITH (NOLOCK) WHERE status <> 2)
		return
	END

	--check for internal redirection article is exist
	IF @article_revision_article_type = 2 AND @article_revision_article_type_detail <> '' AND NOT exists(select * from dbo.cms_article_zones with (nolock) where CAST(zone_id AS VARCHAR(20)) + '-' + CAST(article_id AS VARCHAR(20)) = @article_revision_article_type_detail)
	begin
		select 'CANT_APPROVE' as aStat, 'INTERNAL_REDIRECTED_ARTICALE_NOT_FOUND' as rStat, '' AS tStat
		return
	end

	--check for mapped article is exist
	IF @article_revision_article_type = 9 AND @article_revision_article_type_detail <> '' AND NOT exists(select * from dbo.cms_article_zones with (nolock) where CAST(zone_id AS VARCHAR(20)) + '-' + CAST(article_id AS VARCHAR(20)) = @article_revision_article_type_detail)
	begin
		select 'CANT_APPROVE' as aStat, 'MAPPED_ARTICALE_NOT_FOUND' as rStat, '' AS tStat
		return
	end

	--check for navigation zone id count
	If @navigation_zone_id <> 0 AND exists(SELECT article_id FROM dbo.cms_articles WHERE navigation_zone_id = @navigation_zone_id AND status <> 2 AND article_id <> @article_id)
	begin
		select 'CANT_APPROVE' as aStat, 'NAVIGATION_ZONE_ID_USED' as rStat, (SELECT CONVERT(nvarchar(max), article_id) FROM dbo.cms_articles WHERE navigation_zone_id = @navigation_zone_id AND status <> 2 AND article_id <> @article_id) AS tStat
		return
	end
	--check for domain homepage article
	if exists(select * from dbo.cms_domains with (nolock) where home_page_article like '%-' + CAST(@article_id AS VARCHAR(20))) AND not exists(select * from dbo.cms_article_zones_revision (nolock) where CAST(zone_id AS VARCHAR(20)) + '-' + CAST(article_id AS VARCHAR(20)) in (select home_page_article from dbo.cms_domains with (nolock) where home_page_article like '%-' + CAST(@article_id AS VARCHAR(20))) AND rev_id = @rev_id)
	begin
		select 'CANT_APPROVE' as aStat, 'USED_IN_DOMAIN_HOME_PAGE' as rStat, mt.domain_names as tStat, mt.home_page_article, s.site_name + ' / ' + zg.zone_group_name + ' / ' + z.zone_name as z_name
			from dbo.cms_domains mt (nolock) 
				left join dbo.cms_zones z (nolock) on mt.home_page_article like cast(z.zone_id as varchar(10)) + '-%'
				left join dbo.cms_zone_groups zg (nolock) on z.zone_group_id = zg.zone_group_id
				left join dbo.cms_sites s (nolock) on s.site_id = zg.site_id
			where
				mt.home_page_article like '%-' + CAST(@article_id AS VARCHAR(20))
				AND mt.home_page_article NOT IN (Select (cast(zone_id as varchar(10))+'-'+cast(article_id as varchar(10))) from dbo.cms_article_zones_revision (nolock) where rev_id = @rev_id)
		return
	end
	
	--check for domain 404 article
	if exists(select * from dbo.cms_domains with (nolock) where error_page_article like '%-' + CAST(@article_id AS VARCHAR(20))) AND not exists(select * from dbo.cms_article_zones_revision (nolock) where CAST(zone_id AS VARCHAR(20)) + '-' + CAST(article_id AS VARCHAR(20)) in (select error_page_article from dbo.cms_domains with (nolock) where error_page_article like '%-' + CAST(@article_id AS VARCHAR(20))) AND rev_id = @rev_id)
	begin
		select 'CANT_APPROVE' as aStat, 'USED_IN_DOMAIN_404_PAGE' as rStat, mt.domain_names as tStat, mt.error_page_article, s.site_name + ' / ' + zg.zone_group_name + ' / ' + z.zone_name as z_name
			from dbo.cms_domains mt (nolock) 
				left join dbo.cms_zones z (nolock) on mt.error_page_article like cast(z.zone_id as varchar(10)) + '-%'
				left join dbo.cms_zone_groups zg (nolock) on z.zone_group_id = zg.zone_group_id
				left join dbo.cms_sites s (nolock) on s.site_id = zg.site_id
			where
				mt.error_page_article like '%-' + CAST(@article_id AS VARCHAR(20))
				AND mt.error_page_article NOT IN (Select (cast(zone_id as varchar(10))+'-'+cast(article_id as varchar(10))) from dbo.cms_article_zones_revision (nolock) where rev_id = @rev_id)
		return
	end

	--check for alias redirections
	if exists(select * from dbo.cms_redirects with (nolock) where article_id = @article_id) AND not exists(select * from dbo.cms_article_zones_revision (nolock) where CAST(zone_id AS VARCHAR(20)) + '-' + CAST(article_id AS VARCHAR(20)) in (select CAST(zone_id AS VARCHAR(20)) + '-' + CAST(article_id AS VARCHAR(20)) from dbo.cms_redirects with (nolock) where article_id = @article_id) AND rev_id = @rev_id)
	begin
		select 'CANT_APPROVE' as aStat, 'USED_IN_ALIAS_REDIRECTION' as rStat, mt.redirect_alias as tStat, s.site_name + ' / ' + zg.zone_group_name + ' / ' + z.zone_name as z_name
			from dbo.cms_redirects mt (nolock) 
				left join dbo.cms_zones z (nolock) on mt.zone_id = z.zone_id
				left join dbo.cms_zone_groups zg (nolock) on z.zone_group_id = zg.zone_group_id
				left join dbo.cms_sites s (nolock) on s.site_id = zg.site_id
			where
				mt.article_id = @article_id
				AND CAST(mt.zone_id AS VARCHAR(20)) + '-' + CAST(mt.article_id AS VARCHAR(20)) NOT IN (Select (cast(zone_id as varchar(10))+'-'+cast(article_id as varchar(10))) from dbo.cms_article_zones_revision (nolock) where rev_id = @rev_id)
		return
	end

	--check for site default article
	if exists(select * from dbo.cms_sites with (nolock) where default_article like '%-' + CAST(@article_id AS VARCHAR(20))) AND not exists(select * from dbo.cms_article_zones_revision (nolock) where CAST(zone_id AS VARCHAR(20)) + '-' + CAST(article_id AS VARCHAR(20)) in (select default_article from dbo.cms_sites with (nolock) where default_article like '%-' + CAST(@article_id AS VARCHAR(20))) AND rev_id = @rev_id)
	begin
		select 'CANT_APPROVE' as aStat, 'USED_IN_SITE_DEFAULT_ARTICLE' as rStat, mt.site_name as tStat, mt.site_name + ' / ' + zg.zone_group_name + ' / ' + z.zone_name as z_name
			from dbo.cms_sites mt (nolock) 
				left join dbo.cms_zones z (nolock) on mt.default_article like cast(z.zone_id as varchar(10)) + '-%'
				left join dbo.cms_zone_groups zg (nolock) on z.zone_group_id = zg.zone_group_id
			where
				mt.default_article like '%-' + CAST(@article_id AS VARCHAR(20))
				AND mt.default_article NOT IN (Select (cast(zone_id as varchar(10))+'-'+cast(article_id as varchar(10))) from dbo.cms_article_zones_revision (nolock) where rev_id = @rev_id)
		return
	end

	--check for site tag detail article
	if exists(select * from dbo.cms_sites with (nolock) where tag_detail_article like '%-' + CAST(@article_id AS VARCHAR(20))) AND not exists(select * from dbo.cms_article_zones_revision (nolock) where CAST(zone_id AS VARCHAR(20)) + '-' + CAST(article_id AS VARCHAR(20)) in (select tag_detail_article from dbo.cms_sites with (nolock) where tag_detail_article like '%-' + CAST(@article_id AS VARCHAR(20))) AND rev_id = @rev_id)
	begin
		select 'CANT_APPROVE' as aStat, 'USED_IN_SITE_TAG_DETAIL_ARTICLE' as rStat, mt.site_name as tStat, mt.site_name + ' / ' + zg.zone_group_name + ' / ' + z.zone_name as z_name
			from dbo.cms_sites mt (nolock) 
				left join dbo.cms_zones z (nolock) on mt.tag_detail_article like cast(z.zone_id as varchar(10)) + '-%'
				left join dbo.cms_zone_groups zg (nolock) on z.zone_group_id = zg.zone_group_id
			where
				mt.tag_detail_article like '%-' + CAST(@article_id AS VARCHAR(20))
				AND mt.tag_detail_article NOT IN (Select (cast(zone_id as varchar(10))+'-'+cast(article_id as varchar(10))) from dbo.cms_article_zones_revision (nolock) where rev_id = @rev_id)
		return
	end

	--check for zone group default article
	if exists(select * from dbo.cms_zone_groups with (nolock) where default_article like '%-' + CAST(@article_id AS VARCHAR(20))) AND not exists(select * from dbo.cms_article_zones_revision (nolock) where CAST(zone_id AS VARCHAR(20)) + '-' + CAST(article_id AS VARCHAR(20)) in (select default_article from dbo.cms_zone_groups with (nolock) where default_article like '%-' + CAST(@article_id AS VARCHAR(20))) AND rev_id = @rev_id)
	begin
		select 'CANT_APPROVE' as aStat, 'USED_IN_ZONE_GROUP_DEFAULT_ARTICLE' as rStat, mt.zone_group_name as tStat, s.site_name + ' / ' + mt.zone_group_name + ' / ' + z.zone_name as z_name
			from dbo.cms_zone_groups mt (nolock) 
				left join dbo.cms_zones z (nolock) on mt.default_article like cast(z.zone_id as varchar(10)) + '-%'
				left join dbo.cms_sites s (nolock) on s.site_id = mt.site_id
			where
				mt.default_article like '%-' + CAST(@article_id AS VARCHAR(20))
				AND mt.default_article NOT IN (Select (cast(zone_id as varchar(10))+'-'+cast(article_id as varchar(10))) from dbo.cms_article_zones_revision (nolock) where rev_id = @rev_id)
		return
	end

	--check for zone group tag detail article
	if exists(select * from dbo.cms_zone_groups with (nolock) where tag_detail_article like '%-' + CAST(@article_id AS VARCHAR(20))) AND not exists(select * from dbo.cms_article_zones_revision (nolock) where CAST(zone_id AS VARCHAR(20)) + '-' + CAST(article_id AS VARCHAR(20)) in (select tag_detail_article from dbo.cms_sites with (nolock) where tag_detail_article like '%-' + CAST(@article_id AS VARCHAR(20))) AND rev_id = @rev_id)
	begin
		select 'CANT_APPROVE' as aStat, 'USED_IN_ZONE_GROUP_TAG_DETAIL_ARTICLE' as rStat, mt.zone_group_name as tStat, s.site_name + ' / ' + mt.zone_group_name + ' / ' + z.zone_name as z_name
			from dbo.cms_zone_groups mt (nolock) 
				left join dbo.cms_zones z (nolock) on mt.tag_detail_article like cast(z.zone_id as varchar(10)) + '-%'
				left join dbo.cms_sites s (nolock) on s.site_id = mt.site_id
			where
				mt.tag_detail_article like '%-' + CAST(@article_id AS VARCHAR(20))
				AND mt.tag_detail_article NOT IN (Select (cast(zone_id as varchar(10))+'-'+cast(article_id as varchar(10))) from dbo.cms_article_zones_revision (nolock) where rev_id = @rev_id)
		return
	end

	--check for zone default article
	if exists(select * from dbo.cms_zones with (nolock) where default_article like '%-' + CAST(@article_id AS VARCHAR(20))) AND not exists(select * from dbo.cms_article_zones_revision (nolock) where CAST(zone_id AS VARCHAR(20)) + '-' + CAST(article_id AS VARCHAR(20)) in (select default_article from dbo.cms_zones with (nolock) where default_article like '%-' + CAST(@article_id AS VARCHAR(20))) AND rev_id = @rev_id)
	begin
		select 'CANT_APPROVE' as aStat, 'USED_IN_ZONE_DEFAULT_ARTICLE' as rStat, mt.zone_name as tStat, s.site_name + ' / ' + zg.zone_group_name + ' / ' + mt.zone_name as z_name
			from dbo.cms_zones mt (nolock) 
				left join dbo.cms_zone_groups zg (nolock) on mt.zone_group_id = zg.zone_group_id
				left join dbo.cms_sites s (nolock) on s.site_id = zg.site_id
			where
				mt.default_article like '%-' + CAST(@article_id AS VARCHAR(20))
				AND mt.default_article NOT IN (Select (cast(zone_id as varchar(10))+'-'+cast(article_id as varchar(10))) from dbo.cms_article_zones_revision (nolock) where rev_id = @rev_id)
		return
	end

	----check for article language relation reverse
	--if exists(select * from dbo.cms_article_language_relation with (nolock) where related_article_id = @article_id) AND not exists(select * from dbo.cms_article_zones_revision (nolock) where CAST(zone_id AS VARCHAR(20)) + '-' + CAST(article_id AS VARCHAR(20)) in (select CAST(related_zone_id AS VARCHAR(20)) + '-' + CAST(related_article_id AS VARCHAR(20)) from dbo.cms_article_language_relation with (nolock) where related_article_id = @article_id) AND rev_id = @rev_id)
	--begin
	--	select 'CANT_APPROVE' as aStat, 'USED_IN_LANGUAGE_RELATION' as rStat, CAST(mt.article_id AS VARCHAR(20)) + '-' + (select headline from dbo.cms_articles with (nolock) where article_id = mt.article_id) as tStat, s.site_name + ' / ' + zg.zone_group_name + ' / ' + z.zone_name as z_name
	--		from dbo.cms_article_language_relation mt (nolock) 
	--			left join dbo.cms_zones z (nolock) on mt.related_zone_id = z.zone_id
	--			left join dbo.cms_zone_groups zg (nolock) on z.zone_group_id = zg.zone_group_id
	--			left join dbo.cms_sites s (nolock) on s.site_id = zg.site_id
	--		where
	--			mt.related_article_id = @article_id
	--			AND CAST(mt.related_zone_id AS VARCHAR(20)) + '-' + CAST(mt.related_article_id AS VARCHAR(20)) NOT IN (Select (cast(zone_id as varchar(10))+'-'+cast(article_id as varchar(10))) from dbo.cms_article_zones_revision (nolock) where rev_id = @rev_id)
	--	return
	--end

	--check for internal article redirection
	if exists(select * from dbo.cms_articles with (nolock) where article_type_detail like '%-' + CAST(@article_id AS VARCHAR(20)) AND article_type = 2 AND status=1) AND not exists(select * from dbo.cms_article_zones_revision (nolock) where CAST(zone_id AS VARCHAR(20)) + '-' + CAST(article_id AS VARCHAR(20)) in (select article_type_detail from dbo.cms_articles with (nolock) where article_type_detail like '%-' + CAST(@article_id AS VARCHAR(20)) AND article_type = 2 AND status=1) AND rev_id = @rev_id)
	begin
		select 'CANT_APPROVE' as aStat, 'USED_IN_INTERNAL_ARTICLE_REDIRECTION' as rStat, CAST(mt.article_id AS VARCHAR(20)) + '-' + mt.headline as tStat, s.site_name + ' / ' + zg.zone_group_name + ' / ' + z.zone_name as z_name
			from dbo.cms_articles mt (nolock)
				left join dbo.cms_zones z (nolock) on mt.article_type_detail like cast(z.zone_id as varchar(10)) + '-%'
				left join dbo.cms_zone_groups zg (nolock) on z.zone_group_id = zg.zone_group_id
				left join dbo.cms_sites s (nolock) on s.site_id = zg.site_id
			where
				mt.article_type_detail like '%-' + CAST(@article_id AS VARCHAR(20))
				AND mt.article_type_detail NOT IN (Select (cast(zone_id as varchar(10))+'-'+cast(article_id as varchar(10))) from dbo.cms_article_zones_revision (nolock) where rev_id = @rev_id)
				AND mt.article_type = 2
				AND mt.status=1
		return
	end

	--check for mapped article
	if exists(select * from dbo.cms_articles with (nolock) where article_type_detail like '%-' + CAST(@article_id AS VARCHAR(20)) AND article_type = 9 AND status=1) AND not exists(select * from dbo.cms_article_zones_revision (nolock) where CAST(zone_id AS VARCHAR(20)) + '-' + CAST(article_id AS VARCHAR(20)) in (select article_type_detail from dbo.cms_articles with (nolock) where article_type_detail like '%-' + CAST(@article_id AS VARCHAR(20)) AND article_type = 9 AND status=1) AND rev_id = @rev_id)
	begin
		select 'CANT_APPROVE' as aStat, 'USED_IN_MAPPED_ARTICLE' as rStat, CAST(mt.article_id AS VARCHAR(20)) + '-' + mt.headline as tStat, s.site_name + ' / ' + zg.zone_group_name + ' / ' + z.zone_name as z_name
			from dbo.cms_articles mt (nolock)
				left join dbo.cms_zones z (nolock) on mt.article_type_detail like cast(z.zone_id as varchar(10)) + '-%'
				left join dbo.cms_zone_groups zg (nolock) on z.zone_group_id = zg.zone_group_id
				left join dbo.cms_sites s (nolock) on s.site_id = zg.site_id
			where
				mt.article_type_detail like '%-' + CAST(@article_id AS VARCHAR(20))
				AND mt.article_type_detail NOT IN (Select (cast(zone_id as varchar(10))+'-'+cast(article_id as varchar(10))) from dbo.cms_article_zones_revision (nolock) where rev_id = @rev_id)
				AND mt.article_type = 9
				AND mt.status=1
		return
	end
end

-- update only status if not an admin
if @publisher_level < 100 and @approve_level = 3
begin
	update dbo.cms_article_revision
	set
		revision_status = 'A',
		approval_date = getDate(),
		approval_id = @publisher_id
	where
		rev_id = @rev_id

	select 'OKA' as aStat
	return
end

-- read target status for articles zone update
--select @target_status = status from dbo.cms_article_revision with (nolock) where rev_id = @rev_id




begin transaction

-- can be approved..
-- update original table
update dbo.cms_articles
set
	clsf_id = ar.clsf_id,
	status = ar.status, 
	updated = ar.rev_date,
	startdate = ar.startdate, 
	enddate = ar.enddate, 
	orderno = ar.orderno, 
	lang_id = ar.lang_id,
	menu_text = ar.menu_text,
	navigation_display = ar.navigation_display,
	navigation_zone_id = ar.navigation_zone_id,
	headline = ar.headline, 
	summary = ar.summary, 
	keywords = ar.keywords, 
	article_type = ar.article_type, 
	article_type_detail = ar.article_type_detail, 
	article_1 = ar.article_1, 
	article_2 = ar.article_2, 
	article_3 = ar.article_3, 
	article_4 = ar.article_4, 
	article_5 = ar.article_5, 
	custom_1 = ar.custom_1, 
	custom_2 = ar.custom_2, 
	custom_3 = ar.custom_3, 
	custom_4 = ar.custom_4, 
	custom_5 = ar.custom_5, 
	custom_6 = ar.custom_6, 
	custom_7 = ar.custom_7, 
	custom_8 = ar.custom_8, 
	custom_9 = ar.custom_9, 
	custom_10 = ar.custom_10, 
	custom_11 = ar.custom_11, 
	custom_12 = ar.custom_12, 
	custom_13 = ar.custom_13, 
	custom_14 = ar.custom_14, 
	custom_15 = ar.custom_15, 
	custom_16 = ar.custom_16, 
	custom_17 = ar.custom_17, 
	custom_18 = ar.custom_18, 
	custom_19 = ar.custom_19, 
	custom_20 = ar.custom_20, 
	flag_1 = ar.flag_1, 
	flag_2 = ar.flag_2, 
	flag_3 = ar.flag_3, 
	flag_4 = ar.flag_4, 
	flag_5 = ar.flag_5, 
	date_1 = ar.date_1, 
	date_2 = ar.date_2,
	date_3 = ar.date_3, 
	date_4 = ar.date_4,
	date_5 = ar.date_5,
	cl_1 = ar.cl_1, 
	cl_2 = ar.cl_2, 
	cl_3 = ar.cl_3, 
	cl_4 = ar.cl_4, 
	cl_5 = ar.cl_5, before_head = ar.before_head, before_body = ar.before_body,no_index_no_follow = ar.no_index_no_follow,custom_html_attr = ar.custom_html_attr,meta_title = ar.meta_title,canonical_url = ar.canonical_url,
	custom_body = ar.custom_body,
	meta_description = ar.meta_description,
	omniture_code = ar.omniture_code,
	custom_setting = ar.custom_setting
from dbo.cms_article_revision ar
where ar.rev_id = @rev_id and dbo.cms_articles.article_id = ar.article_id

if(@@error <> 0) goto RollbackAndReturn


-- mark other old approved revisions as ex-approved
update dbo.cms_article_revision
set
	revision_status = 'E'
where
	article_id = @article_id and revision_status = 'L'
if(@@error <> 0) goto RollbackAndReturn


-- mark this revision as approved
update dbo.cms_article_revision
set
	revision_status = 'L',
	approval_date = getDate(),
	approval_id = @publisher_id
where
	rev_id = @rev_id
if(@@error <> 0) goto RollbackAndReturn


-- mark revision requests as processed & deleted
update dbo.cms_instant_messaging
set
	processed = getDate(),
	deleted = getDate()
where
	related_id = @rev_id and ims_type = 'AA'
if(@@error <> 0) goto RollbackAndReturn





-- update related articles for this revision
delete from dbo.cms_article_relation
where article_id = @article_id

if(@@error <> 0) goto RollbackAndReturn

insert into dbo.cms_article_relation
(article_id, related_zone_id, related_article_id)
select article_id, related_zone_id, related_article_id
from dbo.cms_article_relation_revision
where rev_id = @rev_id

if(@@error <> 0) goto RollbackAndReturn






-- update article zones for this revision
delete from dbo.cms_article_zones
where article_id = @article_id

if(@@error <> 0) goto RollbackAndReturn

if @article_status <> 2
begin
	--if its not a delete request then save new article zones
	insert into dbo.cms_article_zones
	(article_id, zone_id, az_order, az_alias,is_alias_protected)
	select article_id, zone_id, az_order, az_alias,is_alias_protected
	from dbo.cms_article_zones_revision
	where rev_id = @rev_id
end

if(@@error <> 0) goto RollbackAndReturn




-- update article language relations for this revision

--declare @lr_id bigint
--select @lr_id = lr.lr_id from dbo.cms_language_relations_revision lr with(nolock) where lr.rev_id = @rev_id

--delete from dbo.cms_language_relations
--where lr_id in (select distinct lr_id from dbo.cms_language_relations  where zone_id in(
--	select distinct zone_id from  dbo.cms_article_zones where article_id = @article_id
--))

delete from dbo.cms_language_relations where lr_id in(
select lr.lr_id from dbo.cms_language_relations_revision lr with(nolock) where article_id = @article_id
)

 
if(@@error <> 0) goto RollbackAndReturn

insert into dbo.cms_language_relations
(lr_id, zone_id, article_id)
select lr_id, zone_id, article_id
from dbo.cms_language_relations_revision
where lr_id in ( select lr.lr_id from dbo.cms_language_relations_revision lr with(nolock) where article_id = @article_id )

if(@@error <> 0) goto RollbackAndReturn




set nocount off

commit transaction

if @article_status = 2
begin
	select 'DELETED' as aStat
end
else
begin
	select 'OK' as aStat, @article_id AS article_id
end
return

RollbackAndReturn:
rollback transaction
select @@ERROR as aStat
return







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_approval_approve_incoming_news]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_approval_approve_incoming_news]
	@news_id int,
	@zone_id int,
	@publisher_id uniqueidentifier
as

set nocount on
declare @rCode varchar(50)
declare @fileName varchar(50)
declare @rev_id bigint
declare @article_id int


if NOT exists(select * from dbo.cms_incoming_news with (nolock) where news_id = @news_id and status = 0 )
begin
	select 'NOT_EXIST' as rCode
	return
end

begin transaction


--insert new article
set @rCode = 'INSERT_ARTICLE'
insert into dbo.cms_articles
(status, startdate, enddate, created, publisher_id, clsf_id, lang_id, headline, summary, keywords, article_1, article_2, article_3, article_4, article_5, custom_1, custom_2, custom_3, custom_4, custom_5, custom_6, custom_7, custom_8, custom_9, custom_10, flag_1, flag_2, flag_3, flag_4, flag_5, date_1, date_2)
	select 1, getDate(), NULL, created, @publisher_id, clsf_id, lang_id, headline, summary, keywords, article_1, article_2, article_3, article_4, article_5, custom_1, custom_2, custom_3, custom_4, custom_5, custom_6, custom_7, custom_8, custom_9, custom_10, flag_1, flag_2, flag_3, flag_4, flag_5, date_1, date_2
	from dbo.cms_incoming_news
	where news_id = @news_id

if(@@error <> 0) goto RollbackAndReturn

set @rCode = 'GET_ARTICLE_ID'
set @article_id = scope_identity()
if(@@error <> 0) goto RollbackAndReturn


--insert article zone
set @rCode = 'INSERT_ARTICLE_ZONE'
insert into dbo.cms_article_zones
(article_id, zone_id, az_order)
values
(@article_id, @zone_id, 0)

if(@@error <> 0) goto RollbackAndReturn




--insert new revision with live revision status
set @rCode = 'INSERT_ARTICLE_REVISION'
insert into dbo.cms_article_revision
(revision_status, article_id, status, startdate, enddate, created, created_by, revised_by, approval_id, approval_date, clsf_id, lang_id, headline, summary, keywords, article_1, article_2, article_3, article_4, article_5, custom_1, custom_2, custom_3, custom_4, custom_5, custom_6, custom_7, custom_8, custom_9, custom_10, flag_1, flag_2, flag_3, flag_4, flag_5, date_1, date_2)
	select 'L', @article_id, 1, getDate(), NULL, created, @publisher_id, @publisher_id, @publisher_id, getDate(), clsf_id, lang_id, headline, summary, keywords, article_1, article_2, article_3, article_4, article_5, custom_1, custom_2, custom_3, custom_4, custom_5, custom_6, custom_7, custom_8, custom_9, custom_10, flag_1, flag_2, flag_3, flag_4, flag_5, date_1, date_2
	from dbo.cms_incoming_news
	where news_id = @news_id

if(@@error <> 0) goto RollbackAndReturn


set @rCode = 'GET_ARTICLE_REVISION_ID'
set @rev_id = scope_identity()
if(@@error <> 0) goto RollbackAndReturn


--insert article zone revision
set @rCode = 'INSERT_ARTICLE_ZONE_REVISION'
insert into dbo.cms_article_zones_revision
(rev_id, article_id, zone_id, az_order)
values
(@rev_id, @article_id, @zone_id, 0)

if(@@error <> 0) goto RollbackAndReturn



--update news with article_id and status
set @rCode = 'UPDATE_NEWS'
update dbo.cms_incoming_news
set
	article_id = @article_id,
	status = 2,
	updated = getDate(),
	updated_by = @publisher_id
where @news_id = news_id

if(@@error <> 0) goto RollbackAndReturn


-- default value.. if no file exists
set @rev_id = 0

--insert file if exists
-- currently only one file supported
if exists(select * from dbo.cms_incoming_news_files with (nolock) where news_id = @news_id)
begin
	-- insert file
	set @rCode = 'INSERT_FILE'
	insert into dbo.cms_article_files
	(article_id, file_title, file_order, file_name_1, file_name_2, file_name_3, file_name_4, file_name_5, file_type_id, file_comment)
		select top 1 @article_id, file_title, file_order, '1_'+ file_name_1, file_name_2, file_name_3, file_name_4, file_name_5, file_type_id, file_comment
		from dbo.cms_incoming_news_files with (nolock)
		where news_id = @news_id
		order by file_order asc
	
	if(@@error <> 0) goto RollbackAndReturn


	--insert file revision
	set @rCode = 'INSERT_FILE_REVISION'
	insert into dbo.cms_article_files_revision
	(created_by, rev_date, revision_status, revised_by, approval_date, approval_id, article_id)
	values
	(@publisher_id, getDate(), 'L', @publisher_id, getDate(), @publisher_id, @article_id)

	if(@@error <> 0) goto RollbackAndReturn


	set @rCode = 'GET_FILE_REVISION'
	set @rev_id = scope_identity()

	if(@@error <> 0) goto RollbackAndReturn


	-- insert file revision file
	set @rCode = 'INSERT_FILE_REVISION_FILE'
	insert into dbo.cms_article_files_revision_files
	(rev_id, article_id, file_title, file_order, file_name_1, file_name_2, file_name_3, file_name_4, file_name_5, file_type_id, file_comment)
		select top 1 @rev_id, @article_id, file_title, file_order, '1_'+ file_name_1, file_name_2, file_name_3, file_name_4, file_name_5, file_type_id, file_comment
		from dbo.cms_incoming_news_files with (nolock)
		where news_id = @news_id
		order by file_order asc

	if(@@error <> 0) goto RollbackAndReturn

	--select file name
	select top 1  @fileName = file_name_1
		from dbo.cms_incoming_news_files with (nolock)
		where news_id = @news_id

	if(@@error <> 0) goto RollbackAndReturn

end


-- success
commit transaction
select 'OK' as rCode, @rev_id as rev_id, @article_id as article_id, @fileName as fName
return

RollbackAndReturn:
rollback transaction
select @rCode as rCode
return


set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_approval_approve_incoming_news_v2]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_approval_approve_incoming_news_v2]
	@news_id int,
	@zone_id int,
	@publisher_id uniqueidentifier
as

set nocount on
declare @rCode varchar(50)
declare @fileName varchar(1000)
declare @rev_id bigint
declare @article_id int

if NOT exists(select * from dbo.cms_incoming_news with (nolock) where news_id = @news_id and status = 0 )
begin
	select 'NOT_EXIST' as rCode
	return
end

begin transaction


--insert new article
set @rCode = 'INSERT_ARTICLE'
insert into dbo.cms_articles
(status, startdate, enddate, created, publisher_id, clsf_id, lang_id, headline, summary, keywords, article_1, article_2, article_3, article_4, article_5, custom_1, custom_2, custom_3, custom_4, custom_5, custom_6, custom_7, custom_8, custom_9, custom_10, flag_1, flag_2, flag_3, flag_4, flag_5, date_1, date_2)
	select 1, getDate(), NULL, created, @publisher_id, clsf_id, lang_id, headline, summary, keywords, article_1, article_2, article_3, article_4, article_5, custom_1, custom_2, custom_3, custom_4, custom_5, custom_6, custom_7, custom_8, custom_9, custom_10, flag_1, flag_2, flag_3, flag_4, flag_5, date_1, date_2
	from dbo.cms_incoming_news
	where news_id = @news_id

if(@@error <> 0) goto RollbackAndReturn

set @rCode = 'GET_ARTICLE_ID'
set @article_id = scope_identity()
if(@@error <> 0) goto RollbackAndReturn


--insert article zone
set @rCode = 'INSERT_ARTICLE_ZONE'
insert into dbo.cms_article_zones
(article_id, zone_id, az_order)
values
(@article_id, @zone_id, 0)

if(@@error <> 0) goto RollbackAndReturn




--insert new revision with live revision status
set @rCode = 'INSERT_ARTICLE_REVISION'
insert into dbo.cms_article_revision
(revision_status, article_id, status, startdate, enddate, created, created_by, revised_by, approval_id, approval_date, clsf_id, lang_id, headline, summary, keywords, article_1, article_2, article_3, article_4, article_5, custom_1, custom_2, custom_3, custom_4, custom_5, custom_6, custom_7, custom_8, custom_9, custom_10, flag_1, flag_2, flag_3, flag_4, flag_5, date_1, date_2)
	select 'L', @article_id, 1, getDate(), NULL, created, @publisher_id, @publisher_id, @publisher_id, getDate(), clsf_id, lang_id, headline, summary, keywords, article_1, article_2, article_3, article_4, article_5, custom_1, custom_2, custom_3, custom_4, custom_5, custom_6, custom_7, custom_8, custom_9, custom_10, flag_1, flag_2, flag_3, flag_4, flag_5, date_1, date_2
	from dbo.cms_incoming_news
	where news_id = @news_id

if(@@error <> 0) goto RollbackAndReturn


set @rCode = 'GET_ARTICLE_REVISION_ID'
set @rev_id = scope_identity()
if(@@error <> 0) goto RollbackAndReturn


--insert article zone revision
set @rCode = 'INSERT_ARTICLE_ZONE_REVISION'
insert into dbo.cms_article_zones_revision
(rev_id, article_id, zone_id, az_order)
values
(@rev_id, @article_id, @zone_id, 0)

if(@@error <> 0) goto RollbackAndReturn



--update news with article_id and status
set @rCode = 'UPDATE_NEWS'
update dbo.cms_incoming_news
set
	article_id = @article_id,
	status = 2,
	updated = getDate(),
	updated_by = @publisher_id
where @news_id = news_id

if(@@error <> 0) goto RollbackAndReturn


-- default value.. if no file exists
set @rev_id = 0

--insert file if exists
-- currently only one file supported
if exists(select * from dbo.cms_incoming_news_files with (nolock) where news_id = @news_id)
begin
	--insert file revision
	set @rCode = 'INSERT_FILE_REVISION'
	insert into dbo.cms_article_files_revision
	(created_by, rev_date, revision_status, revised_by, approval_date, approval_id, article_id)
	values
	(@publisher_id, getDate(), 'L', @publisher_id, getDate(), @publisher_id, @article_id)

	if(@@error <> 0) goto RollbackAndReturn


	set @rCode = 'GET_FILE_REVISION'
	set @rev_id = scope_identity()

	if(@@error <> 0) goto RollbackAndReturn


	
	set @fileName = ''
	declare @tmp_fileName varchar(50)	
	declare @file_id int

	declare articlefilescursor cursor for
	select [file_id] from dbo.cms_incoming_news_files with (nolock) where news_id = @news_id order by file_order
	
	open articlefilescursor
	fetch next from articlefilescursor into @file_id
	
	while @@FETCH_STATUS = 0
	begin
	    	-- insert file
		set @rCode = 'INSERT_FILE'
		insert into dbo.cms_article_files
		(article_id, file_title, file_order, file_name_1, file_name_2, file_name_3, file_name_4, file_name_5, file_type_id, file_comment)
			select @article_id, file_title, file_order, '1_'+ file_name_1, file_name_2, file_name_3, file_name_4, file_name_5, file_type_id, file_comment
			from dbo.cms_incoming_news_files with (nolock)
			where [file_id] = @file_id 
			
		if(@@error <> 0) goto RollbackAndReturn
		
		
		
		-- insert file revision file
		set @rCode = 'INSERT_FILE_REVISION_FILE'
		insert into dbo.cms_article_files_revision_files
		(rev_id, article_id, file_title, file_order, file_name_1, file_name_2, file_name_3, file_name_4, file_name_5, file_type_id, file_comment)
			select @rev_id, @article_id, file_title, file_order, '1_'+ file_name_1, file_name_2, file_name_3, file_name_4, file_name_5, file_type_id, file_comment
			from dbo.cms_incoming_news_files with (nolock)
			where [file_id] = @file_id		
		
		if(@@error <> 0) goto RollbackAndReturn
		
		
		--select file_name and file_comment
		select @tmp_fileName = file_name_1
			from dbo.cms_incoming_news_files with (nolock)
			where [file_id] = @file_id
		set @fileName = @fileName + '|' + @tmp_fileName
		if(@@error <> 0) goto RollbackAndReturn

		/*set @rCode = 'UPDATE_ARTICLES_ARTICLE_1_WITH_FILES_COMMENT'
		update dbo.cms_articles 
			set dbo.cms_articles.article_1 = dbo.cms_articles.article_1 + dbo.cms_incoming_news_files.file_comment
					 from dbo.cms_incoming_news_files with (nolock), dbo.cms_articles with (nolock)
			where dbo.cms_articles.article_id = @article_id and dbo.cms_incoming_news_files.[file_id] = @file_id
		if(@@error <> 0) goto RollbackAndReturn*/

		/*set @rCode = 'UPDATE_ARTICLES_ARTICLES_REVISION_1_WITH_FILES_COMMENT'
		update dbo.cms_article_revision 
			set article_1 = article_1 + (select @ptrval = TEXTPTR(file_comment) from dbo.cms_incoming_news_files with (nolock) where [file_id] = @file_id)
			where article_id = @article_id
 		if(@@error <> 0) goto RollbackAndReturn
		*/
		
		fetch next from articlefilescursor into @file_id
	end
	
	close articlefilescursor
	deallocate articlefilescursor
	
end


-- success
commit transaction
select 'OK' as rCode, @rev_id as rev_id, @article_id as article_id, @fileName as fName
return

RollbackAndReturn:
rollback transaction
select @rCode as rCode
return


set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_approval_approve_zone_revision]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_approval_approve_zone_revision]
	@rev_id bigint,
	@approve_level int,
	@publisher_id uniqueidentifier,
	@publisher_level int,
	@cio char(1) --Zone Check-In Check-Out parameter
as

set nocount on
if not exists (select * from dbo.cms_zone_revision with (nolock) where rev_id = @rev_id and revision_status  in ('N','A','W') )
begin
	-- revision not available for approval
	select 'NOT_AVAILABLE' as aStat, '' as found_name, @rev_id as rev_id
	return
end


declare @zone_status as char(1)
declare @zone_id as int
declare @locked datetime
declare @locked_by nvarchar(100)

select @zone_status = zone_status, @zone_id = zone_id from dbo.cms_zone_revision with (nolock) where rev_id = @rev_id

--Check for zone lock
if not exists(select * from dbo.cms_zones with (nolock) where zone_id = @zone_id and (locked_by = @publisher_id Or locked_by is null)) AND @cio = '1'
begin
	select @locked = a.locked, @locked_by = p.UserName from dbo.cms_zones as a with (nolock) left outer join dbo.vw_aspnet_MembershipUsers as p  with (nolock) on a.locked_by = p.UserId where zone_id = @zone_id
	select 'LOCKED' as aStat, @locked as locked, @locked_by as locked_by, '' as found_name
	return
end

if (@zone_status = 'D')
begin

	-- this is delete approval.. so check related articles first..
	if exists(select * from dbo.cms_article_zones caz with (nolock) LEFT JOIN dbo.cms_articles ca with (nolock) ON ca.article_id = caz.article_id where caz.zone_id = @zone_id and ca.status <> 2)
	begin
		select 'ARTICLE_EXIST' as aStat, '(' + ca.article_id + ') ' + ca.headline as found_name, @rev_id as rev_id
		from dbo.cms_article_zones caz with (nolock)
			LEFT JOIN dbo.cms_articles ca with (nolock)
			ON ca.article_id = caz.article_id and ca.status <> 2
		where caz.zone_id = @zone_id
		order by ca.headline

		return
	end

	-- also check for menu structure relations
	if exists(select * from dbo.cms_articles with (nolock) where navigation_zone_id = @zone_id and navigation_display in (2,3) and status <> 2 )
	begin
		select 'MENU_EXIST' as aStat, '(' + ca.article_id + ') ' + ca.headline as found_name, @rev_id as rev_id
		from dbo.cms_articles ca with (nolock)
		where ca.navigation_zone_id = @zone_id and ca.navigation_display in (2,3) and ca.status <> 2
		order by ca.headline

		return
	end

	--check for domain homepage article
	if exists(select * from dbo.cms_domains with (nolock) where home_page_article like CAST(@zone_id AS VARCHAR(20)) + '-%')
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_DOMAIN_HOME_PAGE' as rStat, domain_names as tStat from dbo.cms_domains with (nolock) where home_page_article like CAST(@zone_id AS VARCHAR(20)) + '-%'
		return
	end

	--check for domain 404 article
	if exists(select * from dbo.cms_domains with (nolock) where error_page_article like CAST(@zone_id AS VARCHAR(20)) + '-%')
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_DOMAIN_404_PAGE' as rStat, domain_names as tStat from dbo.cms_domains with (nolock) where home_page_article like CAST(@zone_id AS VARCHAR(20)) + '-%'
		return
	end

	--check for alias redirections
	if exists(select * from dbo.cms_redirects with (nolock) where zone_id = @zone_id)
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_ALIAS_REDIRECTION' as rStat, redirect_alias as tStat from dbo.cms_redirects with (nolock) where zone_id = @zone_id
		return
	end

	--check for site default article
	if exists(select * from dbo.cms_sites with (nolock) where default_article like CAST(@zone_id AS VARCHAR(20)) + '-%')
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_SITE_DEFAULT_ARTICLE' as rStat, site_name as tStat from dbo.cms_sites with (nolock) where default_article like CAST(@zone_id AS VARCHAR(20)) + '-%'
		return
	end

	--check for site tag detail article
	if exists(select * from dbo.cms_sites with (nolock) where tag_detail_article like '%-' + CAST(@zone_id AS VARCHAR(20)))
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_SITE_TAG_DETAIL_ARTICLE' as rStat, site_name as tStat from dbo.cms_sites with (nolock) where tag_detail_article like '%-' + CAST(@zone_id AS VARCHAR(20))
		return
	end

	--check for zone group default article
	if exists(select * from dbo.cms_zone_groups with (nolock) where default_article like CAST(@zone_id AS VARCHAR(20)) + '-%')
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_ZONE_GROUP_DEFAULT_ARTICLE' as rStat, zone_group_name as tStat from dbo.cms_zone_groups with (nolock) where default_article like CAST(@zone_id AS VARCHAR(20)) + '-%'
		return
	end

	--check for zone group tag detail article
	if exists(select * from dbo.cms_zone_groups with (nolock) where tag_detail_article like CAST(@zone_id AS VARCHAR(20)) + '-%')
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_ZONE_GROUP_TAG_DETAIL_ARTICLE' as rStat, zone_group_name as tStat from dbo.cms_zone_groups with (nolock) where tag_detail_article like CAST(@zone_id AS VARCHAR(20)) + '-%'
		return
	end

	--check for zone default article
	if exists(select * from dbo.cms_zones with (nolock) where default_article like CAST(@zone_id AS VARCHAR(20)) + '-%')
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_ZONE_DEFAULT_ARTICLE' as rStat, zone_name as tStat from dbo.cms_zones with (nolock) where default_article like CAST(@zone_id AS VARCHAR(20)) + '-%'
		return
	end

	--check for article language relation reverse
	--if exists(select * from dbo.cms_article_language_relation with (nolock) where related_zone_id = @zone_id)
	--begin
	--	select 'CANT_DELETE' as aStat, 'USED_IN_LANGUAGE_RELATION' as rStat, CAST(article_id AS VARCHAR(20)) + '-' + (select headline from dbo.cms_articles with (nolock) where article_id = alr.article_id) as tStat from dbo.cms_article_language_relation alr with (nolock) where related_zone_id = @zone_id
	--	return
	--end

	--check for internal article redirection
	if exists(select * from dbo.cms_articles with (nolock) where article_type_detail like CAST(@zone_id AS VARCHAR(20)) + '-%' AND article_type = 2 AND status=1)
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_INTERNAL_ARTICLE_REDIRECTION' as rStat, CAST(article_id AS VARCHAR(20)) + '-' + headline as tStat from dbo.cms_articles with (nolock) where article_type_detail like CAST(@zone_id AS VARCHAR(20)) + '-%' AND article_type = 2 AND status=1
		return
	end

	--check for mapped article
	if exists(select * from dbo.cms_articles with (nolock) where article_type_detail like CAST(@zone_id AS VARCHAR(20)) + '-%' AND article_type = 9 AND status=1)
	begin
		select 'CANT_DELETE' as aStat, 'USED_IN_MAPPED_ARTICLE' as rStat, CAST(article_id AS VARCHAR(20)) + '-' + headline as tStat from dbo.cms_articles with (nolock) where article_type_detail like CAST(@zone_id AS VARCHAR(20)) + '-%' AND article_type = 9 AND status=1
		return
	end
end



-- update only status if not an admin
if @publisher_level < 100 and @approve_level = 3
begin
	update dbo.cms_zone_revision
	set
		revision_status = 'A',
		approval_date = getDate(),
		approval_id = @publisher_id
	where
		rev_id = @rev_id

	select 'OKA' as aStat, '' as found_name, @rev_id as rev_id
	return
end






begin transaction

-- can be approved..
-- update original table
update dbo.cms_zones
set
	zone_group_id = zr.zone_group_id,
	zone_type_id = zr.zone_type_id, 
	zone_status = zr.zone_status, 
	zone_name = zr.zone_name, 
	zone_desc = zr.zone_desc, 
	css_merge = zr.css_merge, 
	css_id = zr.css_id, 
	css_id_mobile = zr.css_id_mobile, 
	css_id_print = zr.css_id_print, 
	template_id = zr.template_id, 
	template_id_mobile = zr.template_id_mobile, 
	custom_body = zr.custom_body,
	zone_keywords = zr.zone_keywords,
	article_1 = zr.article_1, 
	article_2 = zr.article_2, 
	article_3 = zr.article_3, 
	article_4 = zr.article_4, 
	article_5 = zr.article_5, 
	append_1 = zr.append_1, 
	append_2 = zr.append_2, 
	append_3 = zr.append_3, 
	append_4 = zr.append_4, 
	append_5 = zr.append_5, before_head = zr.before_head, before_body = zr.before_body,
	analytics = zr.analytics,zone_alias = zr.zone_alias, 
	updated = zr.rev_date,
	meta_description = zr.meta_description,
	zone_name_display = zr.zone_name_display,
	default_article = zr.default_article,
	omniture_code = zr.omniture_code,
	lang_id = zr.lang_id
from dbo.cms_zone_revision zr
where zr.rev_id = @rev_id and dbo.cms_zones.zone_id = zr.zone_id

if(@@error <> 0) goto RollbackAndReturn

-- mark other old approved revisions as ex-approved
update dbo.cms_zone_revision
set
	revision_status = 'E'
where
	zone_id = @zone_id and revision_status = 'L'
if(@@error <> 0) goto RollbackAndReturn

-- mark revision as approved
update dbo.cms_zone_revision
set
	revision_status = 'L',
	approval_date = getDate(),
	approval_id = @publisher_id
where
	rev_id = @rev_id
if(@@error <> 0) goto RollbackAndReturn



-- mark revision requests as processed & deleted
update dbo.cms_instant_messaging
set
	processed = getDate(),
	deleted = getDate()
where
	related_id = @rev_id and ims_type = 'ZA'
if(@@error <> 0) goto RollbackAndReturn


-- update zone language relations for this revision
--delete from dbo.cms_zone_language_relation
--where zone_id = @zone_id

--if(@@error <> 0) goto RollbackAndReturn

--insert into dbo.cms_zone_language_relation
--(zone_id, related_zone_id)
--select zone_id, related_zone_id
--from dbo.cms_zone_language_relation_revision
--where rev_id = @rev_id

--if(@@error <> 0) goto RollbackAndReturn

set nocount off

commit transaction
if (@zone_status = 'D')
begin
	select 'DELETED' as aStat, '' as found_name, @rev_id as rev_id
end
else
begin
	select 'OK' as aStat, '' as found_name, @rev_id as rev_id
end
return

RollbackAndReturn:
rollback transaction
select @@ERROR as aStat, '' as found_name, @rev_id as rev_id
return







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_cache_add_article_to_cache]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_cache_add_article_to_cache]
	@zone_id int,
	@article_id int
as

if not exists(select * from dbo.cms_article_cache where zone_id = @zone_id and article_id = @article_id)
begin

	insert into dbo.cms_article_cache
	(zone_id, article_id)
	values
	(@zone_id, @article_id)

end







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_cache_check_update_status]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_cache_check_update_status]
	@server_ip varchar(100)
AS


select count(*) as UpdateStatus
from dbo.cms_cache_update with (nolock)
where server_ip = @server_ip and (status = 1 or (status = 2 and timeout < getDate() ) )

if not exists (select * from dbo.cms_cache_update with (nolock) where server_ip = @server_ip)
begin
	insert into dbo.cms_cache_update (server_ip) values (@server_ip)
end







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_cache_remove_article_from_cache]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_cache_remove_article_from_cache]
	@zone_id int,
	@article_id int
as

	delete from dbo.cms_article_cache
	where zone_id = @zone_id and article_id = @article_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_cache_update_update_status]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_cache_update_update_status]
	@server_ip varchar(100),
	@status tinyint,
	@timeout tinyint
AS


if (@timeout = 100)
begin
	-- Setting status for update
	update dbo.cms_cache_update
	set
		status = 1,
		timeout = getDate(),
		updated = getDate()
	where
		status = 0

end
else
begin
	-- changing status
	update dbo.cms_cache_update
	set
		status = @status,
		timeout = DATEADD(mi,@timeout,getDate()),
		updated = getDate()
	where
		server_ip = @server_ip
end







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_config_check_done_status]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_config_check_done_status]
	@server_ip varchar(100)

AS

select
(select count(*) as publishers from dbo.vw_aspnet_MembershipUsers with (nolock) ) as publishers,
(select count(*) as languages from dbo.cms_languages with (nolock) ) as languages,
(select count(*) as css from dbo.cms_css with (nolock) ) as css,
(select count(*) as templates from dbo.cms_templates with (nolock) ) as templates,
(select count(*) as sites from dbo.cms_sites with (nolock) ) as sites,
(select count(*) as zone_groups from dbo.cms_zone_groups with (nolock) ) as zone_groups,
(select count(*) as cache_update from dbo.cms_cache_update with (nolock) where server_ip = @server_ip ) as cache_update,
'Evil Never Die' as status







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_config_select_config_parameters]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_config_select_config_parameters]
AS

select  config_id, 
        config_name, 
        config_value_local, 
        config_value_remote, 
        publisher_id, 
        updated
from dbo.cms_config with (nolock)
order by config_name






GO
/****** Object:  StoredProcedure [dbo].[cms_asp_config_update_local_value]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[cms_asp_config_update_local_value]
	@config_name varchar(30),
	@config_value_local ntext,
	@publisher_id uniqueidentifier
AS

if exists (select * from dbo.cms_config where config_name = @config_name)
begin
	update dbo.cms_config
	set
		config_value_local = @config_value_local,
		updated = getDate(),
		isDefault = 'Y',
		publisher_id = @publisher_id
	where
		config_name = @config_name
end
else
begin
	insert into dbo.cms_config
	(config_name, config_value_local, publisher_id, isDefault)
	values
	(@config_name, @config_value_local, @publisher_id, 'Y')
end







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_config_update_remote_value]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[cms_asp_config_update_remote_value]
	@config_name varchar(30),
	@config_value_remote ntext,
	@publisher_id uniqueidentifier
AS

if exists (select * from dbo.cms_config where config_name = @config_name)
begin
	update dbo.cms_config
	set
		config_value_remote = @config_value_remote,
		updated = getDate(),
		isDefault = 'Y',	
		publisher_id = @publisher_id
	where
		config_name = @config_name
end
else
begin
	insert into dbo.cms_config
	(config_name, config_value_remote, publisher_id, isDefault)
	values
	(@config_name, @config_value_remote, @publisher_id, 'Y')
end







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_copy_article_files_revision_files]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_copy_article_files_revision_files]	
	@old_rev_id bigint,
	@rev_id bigint,
	@af_rf_id bigint,
	@article_id int
as



set nocount on

begin transaction
-- insert original table
	
	insert into dbo.cms_article_files_revision_files 
	( rev_id,  article_id, file_title, file_order, file_name_1, file_name_2, file_name_3, file_name_4, file_name_5, file_name_6, file_name_7, file_name_8, file_name_9, file_name_10, file_type_id, file_comment)
	
		select  @rev_id,  article_id, file_title, file_order, file_name_1, file_name_2, file_name_3, file_name_4, file_name_5, file_name_6, file_name_7, file_name_8, file_name_9, file_name_10, file_type_id, file_comment
		from dbo.cms_article_files_revision_files
		where rev_id = @old_rev_id  and af_rf_id <> @af_rf_id

if(@@error <> 0) goto RollbackAndReturn


set nocount off

commit transaction

return

RollbackAndReturn:
rollback transaction
select @@ERROR as aStat
return







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_delete_article_files_for_new_revision]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_delete_article_files_for_new_revision]
    	 @article_id int,
	@rev_id bigint,	
	 @approval_id uniqueidentifier
as
	
	delete dbo.cms_article_files where article_id = @article_id

	/*if exists(select * from dbo.cms_article_files_revision where rev_id = @rev_id and revision_status = 'N' )
	begin
		--Revision not approved yet.. Apprve  it
		update dbo.cms_article_files_revision
		set 
		approval_date = getDate(), 
		approval_id = @approval_id,	
		revision_status = 'L'
		where rev_id = @rev_id	
	end*/







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_delete_article_search_text]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_delete_article_search_text]
	@article_id int
AS

delete from dbo.cms_article_search where article_id=@article_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_get_new_guid]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_get_new_guid]
AS
Select replace(newID(), '-', '') as guid







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_im_check_im]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_im_check_im]
    @ims_to int
as


declare @ims_count as int
declare @due_count as int

if exists(select * from dbo.vw_aspnet_MembershipUsers with (nolock) where UserId = @ims_to and publisher_status = 'A' and publisher_level > 0)
begin
	select @ims_count = count(*)
	from dbo.cms_instant_messaging with (nolock)
	where ims_to = @ims_to
	and readed is NULL and deleted is NULL

	select @due_count = count(*)
	from dbo.cms_instant_messaging with (nolock)
	where ims_to = @ims_to
	and processed is NULL and deleted is NULL and due < getDate()

	select @ims_count as ims_count, @due_count as due_count

end
else
begin
	select '-1' as ims_count, '-1' as due_count
end







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_im_delete_im]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_im_delete_im]
	@ims_id int,
	@ims_to int,
	@publisher_level int
as

set nocount on

if exists(select * from dbo.cms_instant_messaging with (nolock) where ims_id = @ims_id and ims_to = @ims_to)
begin
	update dbo.cms_instant_messaging
	set deleted = getDate()
	where ims_id = @ims_id and ims_to = @ims_to

	select 0 as rCode
	return
end
else
begin
	select 1 as rCode
	return
end



set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_im_insert_im]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_im_insert_im]
    @ims_from int, 
    @ims_to int, 
    @ims_subject nvarchar(100), 
    @ims_message ntext, 
    @ims_type char(2),
    @zaid int,
    @related_name nvarchar(500),
    @related_id bigint
as

set nocount on

declare @az_name as nvarchar(500)
declare @ims_id as int

--valid from?
if not exists(select * from dbo.vw_aspnet_MembershipUsers with (nolock) where UserId = @ims_from and publisher_status = 'A' and publisher_level > 0)
begin
	select 'FROM_NOTVALID' as im_stat, '' as im_id, '' as publisher_name, '' as publisher_email
	return
end

--valid to?
if not exists(select * from dbo.vw_aspnet_MembershipUsers with (nolock) where UserId = @ims_to and publisher_status = 'A' and publisher_level > 0)
begin
	select 'TO_NOTVALID' as im_stat, '' as im_id, '' as publisher_name, '' as publisher_email
	return
end

--valid zone or article / revision?
if @ims_type = 'ZA'
begin
	if NOT exists(select * from dbo.cms_zone_revision with (nolock) where zone_id = @zaid and rev_id = @related_id and revision_status in ('N','A','W') )
	begin
		select 'ZONE_NOTFOUND_OR_APPROVED' as im_stat, '' as im_id, '' as publisher_name, '' as publisher_email
		return
	end

	if exists(select * from dbo.cms_zone_revision with (nolock) where zone_id = @zaid and rev_id = @related_id and revision_status = 'N' )
	begin
		update dbo.cms_zone_revision Set revision_status = 'W', rev_date = getdate() , revised_by = @ims_from where rev_id = @related_id
	end

	select @az_name = zone_name from dbo.cms_zone_revision with (nolock) where zone_id = @zaid and rev_id = @related_id

end

if @ims_type = 'AA'
begin
	if NOT exists(select * from dbo.cms_article_revision with (nolock) where article_id = @zaid and rev_id = @related_id and revision_status in ('N','A','W') )
	begin
		select 'ARTICLE_NOTFOUND_OR_APPROVED' as im_stat, '' as im_id, '' as publisher_name, '' as publisher_email
		return
	end
	
	if exists(select * from dbo.cms_article_revision with (nolock) where article_id = @zaid and rev_id = @related_id and revision_status = 'N' )
	begin
		update dbo.cms_article_revision Set revision_status = 'W', rev_date = getdate() , revised_by = @ims_from where rev_id = @related_id
	end

	select @az_name = headline from dbo.cms_article_revision with (nolock) where article_id = @zaid and rev_id = @related_id

end

if @ims_type = 'FA'
begin
	if NOT exists(select * from dbo.cms_article_files_revision with (nolock) where article_id = @zaid and rev_id = @related_id and revision_status in ('N','A','W') )
	begin
		select 'ARTICLE_FILE_NOTFOUND_OR_APPROVED' as im_stat, '' as im_id, '' as publisher_name, '' as publisher_email
		return
	end

	
	if exists(select * from dbo.cms_article_files_revision with (nolock) where article_id = @zaid and rev_id = @related_id and revision_status = 'N' )
	begin
		update dbo.cms_article_files_revision Set revision_status = 'W', rev_date = getdate() , revised_by = @ims_from where rev_id = @related_id
	end
	
	select @az_name = headline from dbo.cms_articles with (nolock) where article_id = @zaid

end

--duplicate request?
if @ims_type = 'ZA' or @ims_type = 'AA' or @ims_type = 'FA'
begin
	if exists(select * from dbo.cms_instant_messaging with (nolock) where ims_from = @ims_from and ims_to = @ims_to and ims_type = @ims_type and related_id = @related_id and processed is NULL)
	begin
		select 'ALREADY' as im_stat, '' as im_id, '' as publisher_name, '' as publisher_email
		return
	end

end

-- for other types set article or zone_name as is
if @az_name = '' or @az_name is null
begin
	set @az_name = @related_name
end



--well, everything looks fine..


insert into dbo.cms_instant_messaging
(ims_from, ims_to, ims_subject, ims_message, ims_type, related_id, related_name)
values
(@ims_from, @ims_to, @ims_subject, @ims_message, @ims_type, @related_id, @az_name)

set @ims_id = scope_identity()

select 'OK' as im_stat, @ims_id as im_id, publisher_name, publisher_email
from dbo.vw_aspnet_MembershipUsers with (nolock)
where UserId = @ims_to


set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_im_open_im]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_im_open_im]
	@ims_id int,
	@ims_to int,
	@publisher_level int
as

set nocount on

if exists (select * from dbo.cms_instant_messaging with (nolock) where ims_id = @ims_id and ims_to = @ims_to )
begin
	--mark message as read
	update dbo.cms_instant_messaging
	set readed = getDate()
	where ims_id = @ims_id and ims_to = @ims_to
end

select cim.*, p1.publisher_name as from_name, p2.publisher_name as to_name
from dbo.cms_instant_messaging cim with (nolock)
	left join dbo.vw_aspnet_MembershipUsers p1 with (nolock) on p1.UserId = cim.ims_from
	left join dbo.vw_aspnet_MembershipUsers p2 with (nolock) on p2.UserId = cim.ims_to
where
	cim.ims_id = @ims_id and (cim.ims_to = @ims_to or cim.ims_from = @ims_to)


set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_insert_article_bulk_cache]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_insert_article_bulk_cache]
AS
 
 
insert into dbo.cms_article_cache(zone_id, article_id) 
	select a.zone_id, a.article_id from dbo.vArticlesZones a with(nolock) where a.zone_id + '-' + a.article_id not in (select ac.zone_id + '-' + ac.article_id from dbo.cms_article_cache ac with(nolock))







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_insert_article_cache]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_insert_article_cache]
	@zone_id int,
	@article_id int
AS
 
set nocount on
 
 
if(not exists(select * from dbo.cms_article_cache ac with(nolock) where ac.zone_id = @zone_id and ac.article_id = @article_id))
	insert into dbo.cms_article_cache(zone_id, article_id) values(@zone_id, @article_id)
 
 
set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_insert_article_search_text]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_insert_article_search_text]
	@article_id INT,
	@zone_id INT,
	@search_text NTEXT,
	@headline NVARCHAR(350),
	@summary NVARCHAR(3000),
	@keywords NVARCHAR(2000),
	@description NVARCHAR(4000)
AS
	INSERT INTO dbo.cms_article_search
	  (
	    article_id,
	    zone_id,
	    search_text,
	    headline,
	    summary,
	    keywords,
	    [description]
	  )
	VALUES
	  (
	    @article_id,
	    @zone_id,
	    @search_text,
	    @headline,
	    @summary,
	    @keywords,
	    @description
	  )







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_insert_error]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[cms_asp_insert_error]
@SessionID varchar(50),
@RequestMethod varchar(5),
@ServerPort varchar(50),
@HTTPS varchar(5),
@LocalAddr varchar(255),
@HostAddress varchar(255),
@UserAgent varchar(500),
@URL varchar(100),
@CustomerRefID varchar(4000),
@FormData nvarchar(max),
@AllHTTP varchar(max),
@ErrASPCode varchar(4000),
@ErrNumber varchar(4000),
@ErrSource varchar(4000),
@ErrCategory varchar(4000),
@ErrFile varchar(4000),
@ErrLine int,
@ErrColumn int,
@ErrDescription varchar(1000),
@ErrAspDescription varchar(1000), 
@InsertDate varchar(250)
as
begin
INSERT [dbo].[cms_asp_errors] 
( [MonitorSent], 
[SessionID], 
[RequestMethod], 
[ServerPort],
 [HTTPS], 
 [LocalAddr], 
 [HostAddress], 
 [UserAgent], 
 [URL],
  [CustomerRefID], 
  [FormData], 
  [AllHTTP],
   [ErrASPCode],
    [ErrNumber], 
	[ErrSource],
	 [ErrCategory], 
	 [ErrFile], [ErrLine], [ErrColumn], [ErrDescription], [ErrAspDescription], [InsertDate], [AAS_Checked]) 

VALUES ('N',@SessionID,@RequestMethod,@ServerPort,@HTTPS,@LocalAddr,@HostAddress,@UserAgent,@URL,@CustomerRefID,@FormData,@AllHTTP,@ErrASPCode,@ErrNumber,@ErrSource,@ErrCategory,@ErrFile,@ErrLine,@ErrColumn,@ErrDescription,@ErrAspDescription, CONVERT(varchar,@InsertDate,112),'N')
end






GO
/****** Object:  StoredProcedure [dbo].[cms_asp_insert_search_log]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_insert_search_log]
    @client_ip varchar(15), 
    @server_ip varchar(100), 
    @search_query nvarchar(100), 
    @search_in nvarchar(100),
    @result_count int
as

insert into dbo.cms_search_log 
        (client_ip, server_ip, search_query, search_in, result_count)
values
        (@client_ip, @server_ip, @search_query, @search_in, @result_count)







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_insert_stf_emails]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_insert_stf_emails]
    @from_name nvarchar(100), 
    @from_email varchar(100), 
    @from_ip varchar(15), 
    @to_name nvarchar(100), 
    @to_email varchar(100), 
    @to_note nvarchar(500),
    @stft_id int, 
    @zone_id int, 
    @article_id int
as

insert into dbo.cms_stf_emails
(from_name, from_email, from_ip, to_name, to_email, to_note, stft_id, zone_id, article_id)
values
(@from_name, @from_email, @from_ip, @to_name, @to_email, @to_note, @stft_id, @zone_id, @article_id)







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_mbuilder_select_upper_level_azid]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_mbuilder_select_upper_level_azid]
	@zone_id int
as



select article_id, zone_id
from dbo.vArticlesZonesNav_1 with (nolock)
where navigation_zone_id = @zone_id

union all

select article_id, zone_id
from dbo.vArticlesZonesNav_1 with (nolock)
where navigation_zone_id in (
	select zone_id
	from dbo.vArticlesZonesNav_1 with (nolock)
	where navigation_zone_id = @zone_id
	)

union all

select article_id, zone_id
from dbo.vArticlesZonesNav_1 with (nolock)
where navigation_zone_id in (
	select zone_id
	from dbo.vArticlesZonesNav_1 with (nolock)
	where navigation_zone_id in (
		select zone_id
		from dbo.vArticlesZonesNav_1 with (nolock)
		where navigation_zone_id = @zone_id
		)
	)


union all

select article_id, zone_id
from dbo.vArticlesZonesNav_1 with (nolock)
where navigation_zone_id in (
	select zone_id
	from dbo.vArticlesZonesNav_1 with (nolock)
	where navigation_zone_id in (
		select zone_id
		from dbo.vArticlesZonesNav_1 with (nolock)
		where navigation_zone_id in (
			select zone_id
			from dbo.vArticlesZonesNav_1 with (nolock)
			where navigation_zone_id = @zone_id
			)
		)
	)


union all

select article_id, zone_id
from dbo.vArticlesZonesNav_1 with (nolock)
where navigation_zone_id in (
	select zone_id
	from dbo.vArticlesZonesNav_1 with (nolock)
	where navigation_zone_id in (
		select zone_id
		from dbo.vArticlesZonesNav_1 with (nolock)
		where navigation_zone_id in (
			select zone_id
			from dbo.vArticlesZonesNav_1 with (nolock)
			where navigation_zone_id in (
				select zone_id
				from dbo.vArticlesZonesNav_1 with (nolock)
				where navigation_zone_id = @zone_id
				)
			)
		)
	)







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_mbuilder_select_upper_level_azid_chain]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_mbuilder_select_upper_level_azid_chain]
	@article_id int
as

set nocount on

-- create temporary table
create table #ulazidTable (
	[article_id] [int] NOT NULL ,
	[zone_id] [int] NOT NULL ,
)


insert into #ulazidTable
(article_id, zone_id)
	select article_id, zone_id
	from dbo.vArticlesZonesNav_1 with (nolock)
	where navigation_zone_id in (select zone_id from dbo.cms_article_zones with (nolock) where article_id = @article_id )

insert into #ulazidTable
(article_id, zone_id)
	select article_id, zone_id
	from dbo.vArticlesZonesNav_1 with (nolock)
	where navigation_zone_id in (select zone_id from #ulazidTable with (nolock))

insert into #ulazidTable
(article_id, zone_id)
	select article_id, zone_id
	from dbo.vArticlesZonesNav_1 with (nolock)
	where navigation_zone_id in (select zone_id from #ulazidTable with (nolock))

insert into #ulazidTable
(article_id, zone_id)
	select article_id, zone_id
	from dbo.vArticlesZonesNav_1 with (nolock)
	where navigation_zone_id in (select zone_id from #ulazidTable with (nolock))

insert into #ulazidTable
(article_id, zone_id)
	select article_id, zone_id
	from dbo.vArticlesZonesNav_1 with (nolock)
	where navigation_zone_id in (select zone_id from #ulazidTable with (nolock))

insert into #ulazidTable
(article_id, zone_id)
	select article_id, zone_id
	from dbo.vArticlesZonesNav_1 with (nolock)
	where navigation_zone_id in (select zone_id from #ulazidTable with (nolock))

insert into #ulazidTable
(article_id, zone_id)
	select article_id, zone_id
	from dbo.vArticlesZonesNav_1 with (nolock)
	where navigation_zone_id in (select zone_id from #ulazidTable with (nolock))

select distinct article_id, zone_id from #ulazidTable with (nolock)

drop table #ulazidTable

set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_all_css]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_all_css]

AS


select c.css_id, c.css_name, c.css_code, c.css_fix, c.css_rel_text, c.css_type_text
from dbo.cms_css c with (nolock)
where css_status = 'A'







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_all_plugins]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_select_all_plugins]

AS


select plugin_id, plugin_name, plugin_code
from dbo.cms_plugins with (nolock)
where plugin_status = 1







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_all_portlets]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_all_portlets]
as

select  p.portlet_id, p.portlet_name, p.portlet_html, p.portlet_css, p.portlet_header, p.portlet_footer, p.enable_shortcut
from dbo.cms_portlets p with (nolock)
where portlet_status < 2







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_all_templates]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_all_templates]

AS

select t.template_id, t.template_name, t.template_html, t.template_doctype
from dbo.cms_templates t with (nolock)
where template_status = 'A'
order by t.template_name







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_all_zones]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_all_zones]
AS

	select z.zone_id, s.site_name, g.zone_group_name, z.zone_name
	from dbo.cms_zones z with (nolock)
		left join dbo.cms_zone_groups g with (nolock) on z.zone_group_id = g.zone_group_id
		left join dbo.cms_sites s with (nolock) on g.site_id = s.site_id
	where zone_status <> 'D'
	order by s.site_name, s.site_id, g.zone_group_name, g.zone_group_id, z.zone_name







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_article_by_alias]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Add CHAR(13)
CREATE procedure [dbo].[cms_asp_select_article_by_alias]
    @article_alias nvarchar(300),
    @domain_name varchar(150)
as

select az.article_id, az.zone_id, s.site_name, zg.zone_group_name, z.zone_name, a.headline, d.domain_id
from dbo.cms_article_zones az with (nolock)
	left join dbo.cms_articles a with (nolock) on a.article_id = az.article_id
	left join dbo.cms_zones z with (nolock) on z.zone_id = az.zone_id
	left join dbo.cms_zone_groups zg with (nolock) on z.zone_group_id = zg.zone_group_id
	left join dbo.cms_sites s with (nolock) on s.site_id = zg.site_id
	left join dbo.cms_domains d with (nolock) on d.domain_id = s.domain_id
where lower(az.az_alias) = lower(@article_alias) 
and (select String from dbo.Split(d.domain_names, CHAR(13) + CHAR(10)) where String = @domain_name) is not null

GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_article_cache]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_article_cache]
	@zone_id int,
	@article_id int
AS
 
set nocount on
 
 
if(exists(select * from dbo.cms_article_cache ac with(nolock) where ac.zone_id = @zone_id and ac.article_id = @article_id))
	select 'checked' as out_val
else
	select '' as out_val
 
set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_article_details]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_article_details]
	@zone_id int,
	@article_id int
as

if @zone_id = 0
begin
	select top 1 *
	from dbo.vArticlesZonesFull with (nolock)
	where article_id = @article_id
end
else
begin
	select *
	from dbo.vArticlesZonesFull with (nolock)
	where article_id = @article_id and zone_id = @zone_id
end







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_article_details_for_all_zones]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_select_article_details_for_all_zones]
	@article_id int
as
	select *
	from dbo.vArticlesZonesFull with (nolock)
	where article_id = @article_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_article_details_for_breadcrumb]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_select_article_details_for_breadcrumb]  
 @zone_id int,  
 @article_id int  
as  
  
if @zone_id = 0 AND @article_id > 0  
begin  
 select top 1 site_name, zone_group_name, zone_group_name_display, zone_name, zone_name_display, headline, menu_text, site_id, zone_group_id, zone_id, article_id, navigation_zone_id, az_alias, zone_default_article, article_type, article_type_detail, navigation_display  
 from dbo.vArticlesZonesFull with (nolock)  
 where article_id = @article_id  
end  
  
else if @zone_id > 0 AND @article_id = 0  
begin  
 select top 1 site_name, zone_group_name, zone_group_name_display, zone_name, zone_name_display, headline, menu_text, site_id, zone_group_id, zone_id, article_id, navigation_zone_id, az_alias, zone_default_article, article_type, article_type_detail, navigation_display  
 from dbo.vArticlesZonesFull with (nolock)  
 where navigation_zone_id = @zone_id AND zone_type_id <> 1  
end  
  
else   
begin  
 select top 1 site_name, zone_group_name, zone_group_name_display, zone_name, zone_name_display, headline, menu_text, site_id, zone_group_id, zone_id, article_id, navigation_zone_id, az_alias, zone_default_article, article_type, article_type_detail, navigation_display  
 from dbo.vArticlesZonesFull with (nolock)  
 where article_id = @article_id and zone_id = @zone_id  
end  







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_article_files]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_article_files]
	@article_id int
as

select caf.[file_id], caf.file_title, caf.file_order, caf.file_name_1, caf.file_name_2, caf.file_name_3, caf.file_name_4, caf.file_name_5, caf.file_name_6, caf.file_name_7, caf.file_name_8, caf.file_name_9, caf.file_name_10, caf.file_type_id, caf.file_comment, cft.type_alias
from dbo.cms_article_files caf with (nolock)
	left join dbo.cms_file_types cft with (nolock) on cft.type_id = caf.file_type_id
where article_id = @article_id
order by file_order







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_article_language_relations]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_article_language_relations]
	@article_id INT,
	@zone_id INT
as

declare @lr_id bigint
declare @lr_id_reverse bigint

select @lr_id = lr.lr_id from dbo.cms_language_relations lr with(nolock) where lr.article_id = @article_id and lr.zone_id = @zone_id
 
select distinct z.zone_id, a.article_id, s.site_name, zg.zone_group_name, z.zone_name, a.headline, la.lang_name, a.lang_id, la.lang_order as lang_order,
	'normal' as dir, s.site_name + ' / ' + zg.zone_group_name + ' / ' + z.zone_name + ' / ' + a.headline as out_name
	,az.az_alias
from dbo.cms_language_relations r with (nolock)
	inner join dbo.cms_articles a with (nolock)
		on a.article_id = r.article_id
	inner join dbo.cms_zones z with (nolock)
		on z.zone_id = r.zone_id
	inner join dbo.cms_zone_groups zg with (nolock)
		on zg.zone_group_id = z.zone_group_id
	inner join dbo.cms_sites s with (nolock)
		on s.site_id = zg.site_id
	left join dbo.cms_languages la with (nolock)
		on la.lang_id = a.lang_id
	left join dbo.cms_article_zones az with (nolock)
		on az.zone_id = r.zone_id and az.article_id = r.article_id
where (r.lr_id = @lr_id) and r.article_id <> @article_id
 
order by lang_order







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_article_language_relations_by_revision]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_article_language_relations_by_revision]
	@rev_id bigint,
	@zone_id INT,
	@article_id int
as
 
declare @lr_id bigint
declare @lr_id_reverse bigint

select @lr_id = lr.lr_id from dbo.cms_language_relations lr with(nolock) where lr.article_id = @article_id and lr.zone_id = @zone_id
 
select distinct z.zone_id, a.article_id, s.site_name, zg.zone_group_name, z.zone_name, a.headline, la.lang_name, a.lang_id, la.lang_order as lang_order,
	'normal' as dir, s.site_name + ' / ' + zg.zone_group_name + ' / ' + z.zone_name + ' / ' + a.headline as out_name
	,az.az_alias
from dbo.cms_language_relations r with (nolock)
	inner join dbo.cms_articles a with (nolock)
		on a.article_id = r.article_id
	inner join dbo.cms_zones z with (nolock)
		on z.zone_id = r.zone_id
	inner join dbo.cms_zone_groups zg with (nolock)
		on zg.zone_group_id = z.zone_group_id
	inner join dbo.cms_sites s with (nolock)
		on s.site_id = zg.site_id
	left join dbo.cms_languages la with (nolock)
		on la.lang_id = a.lang_id
	left join dbo.cms_article_zones az with (nolock)
		on az.zone_id = r.zone_id and az.article_id = r.article_id
where (r.lr_id = @lr_id) and r.article_id <> @article_id
 
order by lang_order







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_article_relateds_by_revision]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_article_relateds_by_revision]
	@rev_id int
as



select a.article_id, z.zone_id, s.site_name + ' / ' + zg.zone_group_name + ' / ' + z.zone_name + ' / ' + a.headline as out_name
from dbo.cms_article_relation_revision r with (nolock)
	inner join dbo.cms_articles a with (nolock)
		on a.article_id = r.related_article_id
	inner join dbo.cms_zones z with (nolock)
		on z.zone_id = r.related_zone_id
	inner join dbo.cms_zone_groups zg with (nolock)
		on zg.zone_group_id = z.zone_group_id
	inner join dbo.cms_sites s with (nolock)
		on s.site_id = zg.site_id
where r.rev_id = @rev_id
order by s.site_name, zg.zone_group_name, z.zone_name, a.headline







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_article_revision_details]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_article_revision_details]
	@rev_id int
as

	select top 1 *
	from dbo.vArticlesRevisionsZonesFull with (nolock)
	where rev_id = @rev_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_article_tags]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_article_tags]
	@article_id int
as

select cz.zone_id, cz.zone_name, zg.tag_detail_article as zg_tag_detail_article, s.tag_detail_article as s_tag_detail_article
from dbo.cms_article_zones az with (nolock), dbo.cms_zones cz with (nolock), dbo.cms_zone_groups zg with (nolock), dbo.cms_sites s with (nolock)
where
	cz.zone_id = az.zone_id
	and zg.zone_group_id = cz.zone_group_id
	and s.site_id = zg.site_id
	and az.article_id = @article_id
	and cz.zone_type_id = 2
order by cz.zone_name







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_article_zones_by_article]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_select_article_zones_by_article]
	@article_id int
as

select az.zone_id, z.zone_type_id, (select publisher_id from dbo.cms_articles with (nolock) where article_id=@article_id) as publisher_id
from dbo.cms_article_zones az with (nolock), dbo.cms_zones z with (nolock)
where z.zone_id = az.zone_id and az.article_id = @article_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_article_zones_by_revision]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_article_zones_by_revision]
	@rev_id int
as

select z.zone_id as out_id, s.site_name + ' / ' + zg.zone_group_name + ' / ' + z.zone_name as out_name, azr.az_order, z.zone_type_id, azr.az_alias
from dbo.cms_article_zones_revision azr with (nolock)
	inner join dbo.cms_zones z with (nolock)
		on z.zone_id = azr.zone_id
	inner join dbo.cms_zone_groups zg with (nolock)
		on zg.zone_group_id = z.zone_group_id
	inner join dbo.cms_sites s with (nolock)
		on s.site_id = zg.site_id
where azr.rev_id = @rev_id
order by s.site_name, zg.zone_group_name, z.zone_name







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_articles_by_zone]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_articles_by_zone]
	@zone_id int
as

set nocount off
if @zone_id=-1
begin
	select az.article_id, az.zone_id, az.headline, az.zone_name, az.zone_group_name, az.site_name, az.menu_text, az.az_alias, navigation_display
	from dbo.vArticlesZones az with (nolock)
	where status = 1
	order by headline
end

else
begin
	select az.article_id, az.zone_id, az.headline, az.zone_name, az.zone_group_name, az.site_name, az.menu_text, az.az_alias, navigation_display
	from dbo.vArticlesZones az with (nolock)
	where zone_id = @zone_id
	and status = 1
	order by headline
end

set nocount on







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_articles_by_zone_for_breadcrumb]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_select_articles_by_zone_for_breadcrumb]
	@zone_id int
as

set nocount off
if @zone_id=-1
begin
	select az.article_id, az.zone_id, az.headline, az.zone_name, az.zone_group_name, az.site_name, az.menu_text, az.az_alias, navigation_display
	from dbo.vArticlesZones az with (nolock)
	where status = 1
	order by az_order ASC
end

else
begin
	select az.article_id, az.zone_id, az.headline, az.zone_name, az.zone_group_name, az.site_name, az.menu_text, az.az_alias, navigation_display
	from dbo.vArticlesZones az with (nolock)
	where zone_id = @zone_id
	and status = 1
	order by az_order ASC
end

set nocount on







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_breadcrumb]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_select_breadcrumb]
	@breadcrumb_id int
AS

Set nocount on

If @breadcrumb_id > 0
begin
	Select * from dbo.cms_breadcrumbs with (nolock) where breadcrumb_id = @breadcrumb_id
end

else
begin
	Select bc.*, p.UserName as publisher_name from dbo.cms_breadcrumbs bc with (nolock) left join dbo.vw_aspnet_MembershipUsers p with (nolock) on p.UserId = bc.created_by order by breadcrumb_name asc
end

Set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_cc_details]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_select_cc_details]
    @cc_id int
as

select  c.cc_id, 
        c.cc_name, 
        c.cc_html,
        c.created, 
        c.updated, 
        c.created_by, 
        c.updated_by,
        c.group_id,
        c.structure_description,
	p.UserName
from dbo.cms_custom_content c with (nolock)
	left join dbo.vw_aspnet_MembershipUsers p with (nolock) on p.UserId = c.created_by
where   c.cc_id = @cc_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_classification_details]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_classification_details]
    @classification_id int
as 
    select  classification_name,
            summary_cb,
            enddate_cb,
            keywords_cb,
            custom1_cb,
            custom2_cb,
            custom3_cb,
            custom4_cb,
            custom5_cb,
            custom6_cb,
            custom7_cb,
            custom8_cb,
            custom9_cb,
            custom10_cb,
            custom11_cb,
            custom12_cb,
            custom13_cb,
            custom14_cb,
            custom15_cb,
            custom16_cb,
            custom17_cb,
            custom18_cb,
            custom19_cb,
            custom20_cb,
            date1_cb,
            date2_cb,
            date3_cb,
            date4_cb,
            date5_cb,
            custom1_text,
            custom2_text,
            custom3_text,
            custom4_text,
            custom5_text,
            custom6_text,
            custom7_text,
            custom8_text,
            custom9_text,
            custom10_text,
            custom11_text,
            custom12_text,
            custom13_text,
            custom14_text,
            custom15_text,
            custom16_text,
            custom17_text,
            custom18_text,
            custom19_text,
            custom20_text,
            flag1_text,
            flag2_text,
            flag3_text,
            flag4_text,
            flag5_text,
            date1_text,
            date2_text,
            date3_text,
            date4_text,
            date5_text,
            custom1_type,
            custom2_type,
            custom3_type,
            custom4_type,
            custom5_type,
            custom6_type,
            custom7_type,
            custom8_type,
            custom9_type,
            custom10_type,
            custom1_cb,
            custom2_cb,
            custom3_cb,
            custom4_cb,
            custom5_cb,
            custom6_cb,
            custom7_cb,
            custom8_cb,
            custom9_cb,
            custom10_cb,
            summary_text,
            enddate_text,
            keywords_text,
            article1_text,
            article2_text,
            article3_text,
            article4_text,
            article5_text,
            article1_cb,
            article2_cb,
            article3_cb,
            article4_cb,
            article5_cb,
            date1_text,
            date2_text,
            date1_cb,
            date2_cb,
            summary_cb,
            enddate_cb,
            keywords_cb,
            custom1_subcolumn,
            custom2_subcolumn,
            custom3_subcolumn,
            custom4_subcolumn,
            custom5_subcolumn,
            custom6_subcolumn,
            custom7_subcolumn,
            custom8_subcolumn,
            custom9_subcolumn,
            custom10_subcolumn,
            file_required_cb,
            file_title_required_cb,
            file_description_required_cb,
            required_file_types,
            group_id,
            structure_description
    from    dbo.cms_classifications with ( nolock )
    where   classification_id = @classification_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_classifications]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_classifications]
	@group_id int
as

if @group_id = -1
begin
	select cc.classification_id, cc.classification_name, cc.created, cc.created_by, cp.UserName as publisher_name, cc.group_id, sg.group_name
	from dbo.cms_classifications cc with (nolock)
		left join dbo.vw_aspnet_MembershipUsers cp with (nolock) on cp.UserId = cc.created_by
		left join dbo.cms_structure_groups sg with (nolock) on sg.group_id = cc.group_id
	order by sg.group_name ASC, cc.classification_name ASC
end

else
begin
	select cc.classification_id, cc.classification_name, cc.created, cc.created_by, cp.UserName as publisher_name, cc.group_id, sg.group_name
	from dbo.cms_classifications cc with (nolock)
		left join dbo.vw_aspnet_MembershipUsers cp with (nolock) on cp.UserId = cc.created_by
		left join dbo.cms_structure_groups sg with (nolock) on sg.group_id = cc.group_id
	where cc.group_id = @group_id
	order by sg.group_name ASC, cc.classification_name ASC
end







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_combo_chained_values]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_select_combo_chained_values] 
	@classification_id int,
	@subValue ntext,
	@column_no int
AS
	Select 
		* 
	FROM 
		cms_classification_combo_values
	WHERE
		classification_id=@classification_id AND
		combo_supid like @subValue AND
		column_no=@column_no
	Order By
		combo_order ASC







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_combo_values]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_select_combo_values] 
	@classification_id int,
	@column_no int
AS
	Select 
		* 
	FROM 
		cms_classification_combo_values
	WHERE
		classification_id=@classification_id AND
		column_no=@column_no
	Order By
		combo_order ASC







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_css]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_css]
	@group_id int
AS

if @group_id = -1
begin
	select c.css_id, c.css_name, c.publisher_id, c.created, c.updated, p.UserName as publisher_name, c.css_type, c.group_id, sg.group_name
	from dbo.cms_css c with (nolock)
		left join dbo.vw_aspnet_MembershipUsers p with (nolock) on p.UserId = c.publisher_id
		left join dbo.cms_structure_groups sg with (nolock) on sg.group_id = c.group_id
	where css_status = 'A'
	order by sg.group_name ASC, c.css_name
end

else
begin
	select c.css_id, c.css_name, c.publisher_id, c.created, c.updated, p.UserName as publisher_name, c.css_type, c.group_id, sg.group_name
	from dbo.cms_css c with (nolock)
		left join dbo.vw_aspnet_MembershipUsers p with (nolock) on p.UserId = c.publisher_id
		left join dbo.cms_structure_groups sg with (nolock) on sg.group_id = c.group_id
	where css_status = 'A' AND c.group_id = @group_id
	order by sg.group_name ASC, c.css_name
end







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_css_code]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_css_code]
    @css_id int
AS

SELECT  css_name, css_code, css_type, css_fix, css_rel_text, css_type_text, group_id, structure_description
FROM dbo.cms_css with (nolock)
WHERE   css_id = @css_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_css_history_code]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_css_history_code]
    @history_id int
AS

SELECT  c.css_name, cr.css_code, cr.css_type, cr.css_fix, cr.css_rel_text, cr.css_type_text, group_id, structure_description
FROM dbo.cms_css_revisions cr with (nolock)
	LEFT JOIN dbo.cms_css c with (nolock) on c.css_id = cr.css_id
WHERE   history_id = @history_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_css_rel_type]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_css_rel_type]
	@id1 int,
	@id2 int,
	@id3 int,
	@id4 int,
	@id5 int,
	@id6 int
as

select css_id, 'rel="' + css_rel_text + '" type="' + css_type_text + '"' as rel_type
from dbo.cms_css with (nolock)
where css_id in (@id1,@id2,@id3,@id4,@id5,@id6)







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_default_article_for_breadcrumb]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_select_default_article_for_breadcrumb]
	@daType varchar(2), -- daType: Default Article Type
	@structureID int
AS

set nocount on

if @daType = 'S'
begin
	Select default_article from dbo.cms_sites (nolock) where site_id = @structureID
end

if @daType = 'ZG'
begin
	Select default_article from dbo.cms_zone_groups (nolock) where zone_group_id = @structureID
end

if @daType = 'Z'
begin
	Select default_article from dbo.cms_zones (nolock) where zone_id = @structureID
end

set nocount off







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_domain_details]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_domain_details]
	@domain_id as int
AS


select domain_id, domain_names, home_page_article, created_by, created, updated, error_page_article
from dbo.cms_domains with (nolock)
where domain_status = 'A' and domain_id = @domain_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_domains]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_domains]

AS


select d.domain_id, d.domain_names, d.home_page_article, d.created_by, d.created, d.updated, p.UserName as publisher_name
from dbo.cms_domains d with (nolock)
	left join dbo.vw_aspnet_MembershipUsers p with (nolock) on p.UserId = d.created_by
where domain_status = 'A'
order by d.domain_names







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_file_types]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_file_types]
	
AS


select ft.type_id, ft.type_name, ft.type_alias, ft.created, ft.updated, ft.group_id, sg.group_name
from dbo.cms_file_types as ft with (nolock)
	left join dbo.cms_structure_groups as sg with (nolock) on sg.group_id = ft.group_id
	
order by sg.group_name ASC, updated desc







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_file_types_with_id]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_select_file_types_with_id]
	@inID varchar(8000),
	@withORwithout varchar(50)
AS

declare @dynaSQL nvarchar(400)
declare @lngResult int

set @dynaSQL = '' +
N'select type_id, type_name from dbo.cms_file_types with (nolock) '


if @withORwithout='IN'
begin
	set @dynaSQL = @dynaSQL + N'Where type_id in (' + @inID + ')'
end

if @withORwithout='NOT IN'
begin
	set @dynaSQL = @dynaSQL + N'Where type_id not in (' + @inID + ')'
end

set @dynaSQL = @dynaSQL + N'order by updated desc'
exec @lngResult = sp_executesql @dynaSQL







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_full_template_details_by_zone_id]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_full_template_details_by_zone_id]
	@zone_id int
as

select 
	z.zone_id, z.zone_group_id, z.zone_status, z.zone_name, z.zone_desc, z.css_merge as zone_css_merge, z.css_id as zone_css_id, z.css_id_mobile as zone_css_id_mobile, z.css_id_print as zone_css_id_print, z.template_id as zone_template_id, z.template_id_mobile as zone_template_id_mobile, z.zone_keywords, z.article_1 as zone_article_1, z.article_2 as zone_article_2, z.article_3 as zone_article_3, z.article_4 as zone_article_4, z.article_5 as zone_article_5, z.append_1, z.append_2, z.append_3, z.append_4, z.append_5, z.publisher_id as zone_publisher_id, z.created as zone_created, z.updated as zone_updated, z.custom_body as zone_custom_body, 
	zg.zone_group_name, zg.zone_group_keywords, zg.site_id, zg.css_merge as zg_css_merge, zg.css_id as zg_css_id, zg.css_id_mobile as zg_css_id_mobile, zg.css_id_print as zg_css_id_print, zg.template_id as zg_template_id, zg.template_id_mobile as zg_template_id_mobile, zg.publisher_id as zg_publisher_id, zg.created as zg_created, zg.updated as zg_updated, zg.article_1 as zg_article_1, zg.article_2 as zg_article_2, zg.article_3 as zg_article_3, zg.article_4 as zg_article_4, zg.article_5 as zg_article_5, zg.append_1 as zg_append_1, zg.append_2 as zg_append_2, zg.append_3 as zg_append_3, zg.append_4 as zg_append_4, zg.append_5 as zg_append_5, zg.custom_body as zg_custom_body,
	s.site_name, s.css_id as site_css_id, s.css_id_mobile as site_css_id_mobile, s.css_id_print as site_css_id_print, s.template_id as site_template_id, s.template_id_mobile as site_template_id_mobile, s.publisher_id as site_publisher_id, s.site_keywords, s.site_header, s.site_js, s.site_icon, s.created as site_created, s.updated as site_updated, s.article_1 as s_article_1, s.article_2 as s_article_2, s.article_3 as s_article_3, s.article_4 as s_article_4, s.article_5 as s_article_5, s.custom_body as s_custom_body
from dbo.cms_zones z with (nolock)
inner join dbo.cms_zone_groups zg with (nolock)
	on zg.zone_group_id = z.zone_group_id
	inner join dbo.cms_sites s with (nolock)
	on s.site_id = zg.site_id
where z.zone_id = @zone_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_hidden_details]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_hidden_details]
	@hidden_id as int
AS


select h.hidden_id, h.hidden_value, h.hidden_type, h.hidden_desc, h.created_by, h.created, h.updated
from dbo.cms_hidden_values h with (nolock)
where hidden_id = @hidden_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_hidden_values]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_hidden_values]

AS


select h.hidden_id, h.hidden_value, h.hidden_type, h.hidden_desc, h.created_by, h.created, h.updated, p.UserName
from dbo.cms_hidden_values h with (nolock)
	left join dbo.vw_aspnet_MembershipUsers p with (nolock) on p.UserId = h.created_by
order by h.hidden_value







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_language_details]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--add lang_alias to [cms_asp_select_language_details]
CREATE procedure [dbo].[cms_asp_select_language_details]
    @lang_id char(2)
as

select  lang_id, 
        lang_name, 
        lang_xml,
        lang_order, 
        created, 
        updated,
		lang_alias
from dbo.cms_languages with (nolock)
where   lang_id = @lang_id

GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_languages]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_languages]
AS

select lang_id, lang_name, created, updated
from dbo.cms_languages with (nolock)
order by lang_order, lang_name







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_log_event_types]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_log_event_types]
as

select  event_id, 
        event_name, 
        event_description, 
        event_type, 
        created
from dbo.cms_publisher_log_events with (nolock)
order by event_name







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_permission_object_by_group]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_permission_object_by_group]
	@zone_group_id int
AS


if @zone_group_id = -1
begin
	select site_id as out_id, site_name as out_name, 'S' as out_type
	from dbo.cms_sites with (nolock)
	union all
	select g.zone_group_id as out_id, s.site_name + ' / ' + g.zone_group_name as out_name, 'G' as out_type
	from dbo.cms_zone_groups g with (nolock)
		left join dbo.cms_sites s with (nolock) on g.site_id = s.site_id
	order by out_name
	
end
else
begin
	select z.zone_id as out_id, s.site_name + ' / ' + g.zone_group_name + ' / ' + z.zone_name as out_name, 'Z' as out_type
	from dbo.cms_zones z with (nolock)
		left join dbo.cms_zone_groups g with (nolock) on z.zone_group_id = g.zone_group_id
		left join dbo.cms_sites s with (nolock) on g.site_id = s.site_id
		left join dbo.vw_aspnet_MembershipUsers p with (nolock) on z.publisher_id = p.UserId
	where z.zone_group_id = @zone_group_id and zone_status <> 'D'
	order by z.zone_name
end







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_plugins]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_plugins]
	@group_id int
as

if @group_id = -1
begin
	select  p.plugin_id, 
		p.plugin_status, 
		p.plugin_name, 
		p.created, 
		p.created_by, 
		p.updated, 
		p.updated_by,
		pu.UserName as publisher_name,
		p.group_id,
		sg.group_name
	from dbo.cms_plugins p with (nolock)
		left join dbo.vw_aspnet_MembershipUsers pu with (nolock) on pu.UserId = p.created_by
		left join dbo.cms_structure_groups sg with (nolock) on sg.group_id = p.group_id
	where p.plugin_status < 2
	order by sg.group_name ASC, p.plugin_name asc
end

else
begin
	select  p.plugin_id, 
		p.plugin_status, 
		p.plugin_name, 
		p.created, 
		p.created_by, 
		p.updated, 
		p.updated_by,
		pu.UserName as publisher_name,
		p.group_id,
		sg.group_name
	from dbo.cms_plugins p with (nolock)
		left join dbo.vw_aspnet_MembershipUsers pu with (nolock) on pu.UserId = p.created_by
		left join dbo.cms_structure_groups sg with (nolock) on sg.group_id = p.group_id
	where p.plugin_status < 2 AND p.group_id = @group_id
	order by p.plugin_name asc
end







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_portlet_details]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_portlet_details]
    @portlet_id int
as

select  portlet_id, 
        portlet_name, 
        publisher_id, 
        portlet_status, 
        created, 
        updated, 
        updated_by, 
        portlet_html, 
        portlet_css,
        content_editor_type,
        portlet_header,
        portlet_footer,
        group_id,
        structure_description,
        enable_shortcut
from dbo.cms_portlets with (nolock)
where   portlet_id = @portlet_id
and portlet_status < 2







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_portlets]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_portlets]
	@group_id int
AS

if @group_id = -1
begin
	select  p.portlet_id, p.portlet_name, p.publisher_id, p.portlet_status, p.created, p.updated, p.updated_by, p1.UserName as updated_name, p.group_id, sg.group_name
	from dbo.cms_portlets p with (nolock)
		left join dbo.vw_aspnet_MembershipUsers p1 with (nolock) on p1.UserId = p.updated_by
		left join dbo.cms_structure_groups sg with (nolock) on sg.group_id = p.group_id
	where portlet_status < 2
	order by sg.group_name ASC, p.portlet_name asc
end

else
begin
	select  p.portlet_id, p.portlet_name, p.publisher_id, p.portlet_status, p.created, p.updated, p.updated_by, p1.UserName as updated_name, p.group_id, sg.group_name
	from dbo.cms_portlets p with (nolock)
		left join dbo.vw_aspnet_MembershipUsers p1 with (nolock) on p1.UserId = p.updated_by
		left join dbo.cms_structure_groups sg with (nolock) on sg.group_id = p.group_id
	where portlet_status < 2 AND p.group_id = @group_id
	order by sg.group_name ASC, p.portlet_name asc
end







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_redirection_by_alias]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_redirection_by_alias]
    @redirect_alias nvarchar(100),
    @domain_name varchar(150)
as
 

select r.article_id, r.zone_id, s.site_name, zg.zone_group_name, z.zone_name, a.headline, ISNULL(r.permanent_redirection, 0) AS permanent_redirection
from dbo.cms_redirects r with (nolock)
	left join dbo.cms_articles a with (nolock) on a.article_id = r.article_id
	left join dbo.cms_zones z with (nolock) on z.zone_id = r.zone_id
	left join dbo.cms_zone_groups zg with (nolock) on z.zone_group_id = zg.zone_group_id
	left join dbo.cms_sites s with (nolock) on s.site_id = zg.site_id
	left join dbo.cms_domains d with (nolock) on d.domain_id = s.domain_id
where lower(r.redirect_alias) = lower(@redirect_alias)
and (select String from dbo.Split(d.domain_names, CHAR(10)) where String like @domain_name +'%') is not null







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_redirections]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_redirections]

AS

select r.ID,r.RedirectFrom, r.RedirectTo, r.CreatedBy, r.Created, r.Updated, p.UserName as publisher_name
from dbo.cms_page_redirection r with (nolock)
	left join dbo.vw_aspnet_MembershipUsers p with (nolock) on p.UserId = r.CreatedBy
order by r.RedirectFrom







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_redirections_details]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[cms_asp_select_redirections_details]
	@ID as int
AS


select ID, RedirectFrom, RedirectTo, CreatedBy, Created, Updated
from dbo.cms_page_redirection with (nolock)
where  ID = @ID







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_rss_channel_contents]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_rss_channel_contents]
	@channel_id int
as

select b.article_1, b.az_alias, b.summary, b.article_2, b.article_3, b.article_4, b.article_5, b.custom_1, b.custom_2, b.custom_3, b.custom_4, b.custom_5, b.custom_6, b.custom_7, b.custom_8, b.custom_9, b.custom_10, b.custom_11, b.custom_12, b.custom_13, b.custom_14, b.custom_15, b.custom_16, b.custom_17, b.custom_18, b.custom_19, b.custom_20, b.menu_text, a.* from 
(
select article_id, site_id, zone_group_id, zone_id, headline, site_name, zone_group_name, zone_name, summary, startdate, updated, status, zone_type_id, enddate
from dbo.vArticlesZones with (nolock)
where site_id in (select sgz_id from dbo.cms_rss_content with (nolock) where channel_id = @channel_id and sgz_type = 'S' and sgz_exclude = 'I') and status = 1 AND zone_type_id = 0 and ((startdate < getDate() and enddate is null) or (startdate < getDate() and enddate > getDate()))

union

select article_id, site_id, zone_group_id, zone_id, headline, site_name, zone_group_name, zone_name, summary, startdate, updated, status, zone_type_id, enddate
from dbo.vArticlesZones with (nolock)
where zone_group_id in (select sgz_id from dbo.cms_rss_content with (nolock) where channel_id = @channel_id and sgz_type = 'G' and sgz_exclude = 'I') and status = 1 AND zone_type_id = 0 and ((startdate < getDate() and enddate is null) or (startdate < getDate() and enddate > getDate()))

union

select article_id, site_id, zone_group_id, zone_id, headline, site_name, zone_group_name, zone_name, summary, startdate, updated, status, zone_type_id, enddate
from dbo.vArticlesZones with (nolock)
where zone_id in (select sgz_id from dbo.cms_rss_content with (nolock) where channel_id = @channel_id and sgz_type = 'Z' and sgz_exclude = 'I') and status = 1 AND zone_type_id = 0 and ((startdate < getDate() and enddate is null) or (startdate < getDate() and enddate > getDate()))


) a,  dbo.vArticlesZones b with (nolock)
where a.article_id = b.article_id and a.zone_id = b.zone_id

and a.site_id not in (select sgz_id from dbo.cms_rss_content with (nolock) where channel_id = @channel_id and sgz_type = 'S' and sgz_exclude = 'E')
and a.zone_group_id not in (select sgz_id from dbo.cms_rss_content with (nolock) where channel_id = @channel_id and sgz_type = 'G' and sgz_exclude = 'E')
and a.zone_id not in (select sgz_id from dbo.cms_rss_content with (nolock) where channel_id = @channel_id and sgz_type = 'Z' and sgz_exclude = 'E')
and a.status = 1 AND a.zone_type_id = 0 and ((a.startdate < getDate() and a.enddate is null) or (a.startdate < getDate() and a.enddate > getDate()))
order by a.updated desc, a.headline







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_rss_channel_details]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_rss_channel_details]
	@channel_id int
as

select  channel_id, 
        channel_name, 
        url, 
        [description], 
        lang_id, 
        managing_editor, 
        copyright, 
        created, 
        created_by, 
        updated, 
        updated_by,
        channel_status,
        group_id,
        structure_description,
        summary_content_field,
        content_template,
        content_template_editor_type,
        singularize_articles
from dbo.cms_rss_channels with (nolock)
where channel_id = @channel_id and channel_status <> 'D'







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_rss_channels]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_rss_channels]
	@group_id int,
	@site_id bigint,
	@zone_group_id bigint,
	@zone_id bigint
as

if @group_id = -1
begin
	if @site_id > 0 OR @zone_group_id > 0 OR @zone_id > 0
	begin
		select  c.channel_id, 
		        c.channel_name, 
		        c.created, 
		        c.created_by, 
		        c.updated, 
		        c.updated_by,
			p.UserName as publisher_name,
			c.channel_status,
			c.group_id,
			sg.group_name
		from dbo.cms_rss_channels c with (nolock)
			left join dbo.vw_aspnet_MembershipUsers p with (nolock) ON p.UserId = c.created_by
			left join dbo.cms_structure_groups sg with (nolock) on sg.group_id = c.group_id
		where
			channel_status <> 'D'
			AND channel_id in (select channel_id from dbo.cms_rss_content with (nolock) where ((sgz_id = @site_id AND sgz_type = 'S') OR (sgz_id = @zone_group_id AND sgz_type = 'G') OR (sgz_id = @zone_id AND sgz_type = 'Z')) AND (sgz_exclude in ('I','D')))
		order by sg.group_name ASC, channel_name ASC
	end
	else
	begin
		select  c.channel_id, 
		        c.channel_name, 
		        c.created, 
		        c.created_by, 
		        c.updated, 
		        c.updated_by,
			p.UserName as publisher_name,
			c.channel_status,
			c.group_id,
			sg.group_name
		from dbo.cms_rss_channels c with (nolock)
			left join dbo.vw_aspnet_MembershipUsers p with (nolock) ON p.UserId = c.created_by
			left join dbo.cms_structure_groups sg with (nolock) on sg.group_id = c.group_id
		where
			channel_status <> 'D'
		order by sg.group_name ASC, channel_name ASC
	end
end

else
begin
	if @site_id > 0 OR @zone_group_id > 0 OR @zone_id > 0
	begin
		select  c.channel_id, 
		        c.channel_name, 
		        c.created, 
		        c.created_by, 
		        c.updated, 
		        c.updated_by,
			p.UserName,
			c.channel_status,
			c.group_id,
			sg.group_name
		from dbo.cms_rss_channels c with (nolock)
			left join dbo.vw_aspnet_MembershipUsers p with (nolock) ON p.UserId = c.created_by
			left join dbo.cms_structure_groups sg with (nolock) on sg.group_id = c.group_id
		where
			channel_status <> 'D'
			AND c.group_id = @group_id
			AND channel_id in (select channel_id from dbo.cms_rss_content with (nolock) where ((sgz_id = @site_id AND sgz_type = 'S') OR (sgz_id = @zone_group_id AND sgz_type = 'G') OR (sgz_id = @zone_id AND sgz_type = 'Z')) AND (sgz_exclude in ('I','D')))
		order by sg.group_name ASC, channel_name ASC
	end

	else
	begin
		select  c.channel_id, 
		        c.channel_name, 
		        c.created, 
		        c.created_by, 
		        c.updated, 
		        c.updated_by,
			p.UserName,
			c.channel_status,
			c.group_id,
			sg.group_name
		from dbo.cms_rss_channels c with (nolock)
			left join dbo.vw_aspnet_MembershipUsers p with (nolock) ON p.UserId = c.created_by
			left join dbo.cms_structure_groups sg with (nolock) on sg.group_id = c.group_id
		where
			channel_status <> 'D' AND c.group_id = @group_id
		order by sg.group_name ASC, channel_name ASC
	end
end







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_site_details]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_site_details]
    @site_id int
as

select  site_id, 
        site_name, 
        css_id,
        css_id_mobile, 
        css_id_print,
        template_id,
        template_id_mobile, 
        publisher_id, 
        site_keywords, 
        site_header,
        site_js,
        custom_body,
        site_icon,
        created, 
        updated,
        article_1,
        article_2,
        article_3,
        article_4,
        article_5,
        analytics,
        tag_detail_article,
        group_id,
        structure_description,
        meta_description,
        content_1_editor_type, content_2_editor_type, content_3_editor_type, content_4_editor_type, content_5_editor_type,
        default_article,
        omniture_code,
        domain_id
from dbo.cms_sites as s with (nolock)
where   site_id = @site_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_sites]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_sites]
	@group_id int
AS

if @group_id = -1
begin
	select s.site_id, s.site_name, s.publisher_id, s.created, s.updated, p.UserName as publisher_name, s.group_id, sg.group_name
	from dbo.cms_sites s with (nolock)
		left join dbo.vw_aspnet_MembershipUsers p with (nolock) on p.UserId = s.publisher_id
		left join dbo.cms_structure_groups sg with (nolock) on sg.group_id = s.group_id
	 
	order by sg.group_name ASC, s.site_name
end

else
begin
	select s.site_id, s.site_name, s.publisher_id, s.created, s.updated, p.UserName as publisher_name, s.group_id, sg.group_name
	from dbo.cms_sites s with (nolock)
		left join dbo.vw_aspnet_MembershipUsers p with (nolock) on p.UserId = s.publisher_id
		left join dbo.cms_structure_groups sg with (nolock) on sg.group_id = s.group_id
		 
	where s.group_id = @group_id  
	order by sg.group_name ASC, s.site_name
end







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_stf_template_html]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_stf_template_html]
    @stft_id int
AS

SELECT  stft_name, stft_status, stft_form_html, stft_thanks, stft_mail_html, stft_mail_subject, stft_mail_from_name, stft_wh, omniture_function
FROM dbo.cms_stf_templates with (nolock)
WHERE   stft_id = @stft_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_template_history_html]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_template_history_html]
    @history_id int
AS

SELECT  t.template_name, tr.template_html, tr.template_type , t.group_id, t.structure_description, tr.content_1_editor_type, tr.template_doctype, t.template_id,t.created,t.updated
FROM dbo.cms_template_revisions tr with (nolock)
	LEFT JOIN dbo.cms_templates t with (nolock) on t.template_id = tr.template_id
WHERE   history_id = @history_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_template_html]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_template_html]
    @template_id int
AS

SELECT  template_name, template_html, template_type , group_id, structure_description, content_1_editor_type, template_doctype
FROM dbo.cms_templates with (nolock)
WHERE   template_id = @template_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_xml_details]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_xml_details]
	@xml_id int
as


select xml_id, xml_name, xml_main_node, xml_main_node_attrib, xml_per_node, xml_per_node_attrib, xml_sub_node, xml_sub_template, xml_level, xml_related_line,  xml_xml, created, created_by, group_id, structure_description
from dbo.cms_xml with (nolock)
where xml_id = @xml_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_xml_list]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_xml_list]
	@group_id int
as

if @group_id = -1
begin
	select x.xml_id, x.xml_name, x.created, x.created_by, cp.UserName as publisher_name, x.group_id, sg.group_name
	from dbo.cms_xml x with (nolock)
		left join dbo.vw_aspnet_MembershipUsers cp with (nolock) on cp.UserId = x.created_by
		left join dbo.cms_structure_groups sg with (nolock) on sg.group_id = x.group_id
	order by sg.group_name ASC, x.xml_name ASC
end

else
begin
	select x.xml_id, x.xml_name, x.created, x.created_by, cp.UserName as publisher_name, x.group_id, sg.group_name
	from dbo.cms_xml x with (nolock)
		left join dbo.vw_aspnet_MembershipUsers cp with (nolock) on cp.UserId = x.created_by
		left join dbo.cms_structure_groups sg with (nolock) on sg.group_id = x.group_id
	where
		x.group_id = @group_id
	order by sg.group_name ASC, x.xml_name ASC
end







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_zone_details_by_id]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_select_zone_details_by_id]
	@zone_id int
as

select z.zone_id, s.site_name + ' / ' + zg.zone_group_name + ' / ' + z.zone_name as out_name, z.zone_name, z.zone_type_id
from dbo.cms_zones z with (nolock)
inner join dbo.cms_zone_groups zg with (nolock)
	on zg.zone_group_id = z.zone_group_id
	inner join dbo.cms_sites s with (nolock)
	on s.site_id = zg.site_id
where z.zone_id = @zone_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_zone_group_details]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_zone_group_details]
    @zone_group_id int
as

select  zone_group_id, 
        zone_group_name, 
        zone_group_keywords, 
        site_id, 
        css_merge, 
        css_id, 
        css_id_mobile, 
        css_id_print,
        template_id,
        template_id_mobile, 
        custom_body,
        publisher_id, 
        created, 
        updated,
        article_1,
        article_2,
        article_3,
        article_4,
        article_5,
        append_1,
        append_2,
        append_3,
        append_4,
        append_5,
        analytics,
        tag_detail_article,
        meta_description,
        zone_group_name_display,
        content_1_editor_type, content_2_editor_type, content_3_editor_type, content_4_editor_type, content_5_editor_type,
        default_article, 
        omniture_code
from dbo.cms_zone_groups with (nolock)
where   zone_group_id = @zone_group_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_zone_revision_details]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_zone_revision_details] @rev_id int
as 
    declare @lorem as varchar(500)
    set @lorem = 'Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Fusce vitae erat a lectus blandit ultricies. Etiam sit amet odio. In ac ante gravida augue fermentum tincidunt. Nullam nec orci sed urna vestibulum faucibus. Vestibulum id ipsum non justo porta aliquam. Etiam pulvinar, leo feugiat convallis luctus, erat ante feugiat diam, eu tristique turpis mauris quis ante. Maecenas fermentum nunc in enim scelerisque imperdiet.'


    select  '0' as article_id,
            '2' as status,
            getDate() as created,
            getDate() as updated,
            getDate() as startdate,
            getDate() as enddate,
            '0' as publisher_id,
            '0' as clicks,
            '0' as orderno,
            '0' as az_order,
            '' as lang_id,
            '0' as navigation_display,
            '0' as navigation_zone_id,
            z.zone_type_id,
            '' as menu_text,
            'Lorem ipsum dolor sit amet' as headline,
            @lorem as summary,
            @lorem as keywords,
            '0' as article_type,
            '' as article_type_detail,
            @lorem as article_1,
            @lorem as article_2,
            @lorem as article_3,
            @lorem as article_4,
            @lorem as article_5,
            @lorem as custom_1,
            @lorem as custom_2,
            @lorem as custom_3,
            @lorem as custom_4,
            @lorem as custom_5,
            @lorem as custom_6,
            @lorem as custom_7,
            @lorem as custom_8,
            @lorem as custom_9,
            @lorem as custom_10,
            @lorem as custom_11,
            @lorem as custom_12,
            @lorem as custom_13,
            @lorem as custom_14,
            @lorem as custom_15,
            @lorem as custom_16,
            @lorem as custom_17,
            @lorem as custom_18,
            @lorem as custom_19,
            @lorem as custom_20,
            '0' as flag_1,
            '0' as flag_2,
            '0' as flag_3,
            '0' as flag_4,
            '0' as flag_5,
            getDate() as date_1,
            getDate() as date_2,
            getDate() as date_3,
            getDate() as date_4,
            getDate() as date_5,
            0 as cl_1,
            0 as cl_2,
            0 as cl_3,
            0 as cl_4,
            0 as cl_5,
            '' as a_custom_body,
            z.zone_id,
            z.zone_group_id,
            z.zone_status,
            z.zone_name,
            z.zone_desc,
            z.css_merge as zone_css_merge,
            z.css_id as zone_css_id,
            z.css_id_mobile as zone_css_id_mobile,
            z.css_id_print as zone_css_id_print,
            z.template_id as zone_template_id,
            z.template_id_mobile as zone_template_id_mobile,
            z.zone_keywords,
            z.article_1 as zone_article_1,
            z.article_2 as zone_article_2,
            z.article_3 as zone_article_3,
            z.article_4 as zone_article_4,
            z.article_5 as zone_article_5,
            z.append_1,
            z.append_2,
            z.append_3,
            z.append_4,
            z.append_5,
            z.revised_by as zone_publisher_id,
            z.created as zone_created,
            z.rev_date as zone_updated,
            z.custom_body as zone_custom_body,
            zg.zone_group_name,
            zg.zone_group_keywords,
            zg.site_id,
            zg.css_merge as zg_css_merge,
            zg.css_id as zg_css_id,
            zg.css_id_mobile as zg_css_id_mobile,
            zg.css_id_print as zg_css_id_print,
            zg.template_id as zg_template_id,
            zg.template_id_mobile as zg_template_id_mobile,
            zg.publisher_id as zg_publisher_id,
            zg.created as zg_created,
            zg.updated as zg_updated,
            zg.article_1 as zg_article_1,
            zg.article_2 as zg_article_2,
            zg.article_3 as zg_article_3,
            zg.article_4 as zg_article_4,
            zg.article_5 as zg_article_5,
            zg.append_1 as zg_append_1,
            zg.append_2 as zg_append_2,
            zg.append_3 as zg_append_3,
            zg.append_4 as zg_append_4,
            zg.append_5 as zg_append_5,
            zg.custom_body as zg_custom_body,
            s.site_name,
            s.css_id as site_css_id,
            s.css_id_mobile as site_css_id_mobile,
            s.css_id_print as site_css_id_print,
            s.template_id as site_template_id,
            s.template_id_mobile as site_template_id_mobile,
            s.publisher_id as site_publisher_id,
            s.site_keywords,
            s.site_header,
            s.site_icon,
            s.created as site_created,
            s.updated as site_updated,
            s.article_1 as s_article_1,
            s.article_2 as s_article_2,
            s.article_3 as s_article_3,
            s.article_4 as s_article_4,
            s.article_5 as s_article_5,
            s.custom_body as s_custom_body,
            s.site_js,
            s.analytics as site_analytics,
            zg.analytics as zg_analytics,
            z.analytics as zone_analytics,
            z.meta_description as zone_meta_description,
            zg.meta_description as zone_group_meta_description,
            s.meta_description as site_meta_description,
            zg.zone_group_name_display,
            z.zone_name_display,
            '' as az_alias,
            '' as article_omniture_code,
            z.omniture_code as zone_omniture_code,
            zg.omniture_code as zone_group_omniture_code,
            s.omniture_code as site_omniture_code
    FROM    dbo.cms_zone_revision z with ( nolock )
            LEFT JOIN dbo.cms_zone_groups zg with ( nolock ) on zg.zone_group_id = z.zone_group_id
            LEFT JOIN dbo.cms_sites s with ( nolock ) on s.site_id = zg.site_id
    WHERE   z.rev_id = @rev_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_zones_by_group]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_zones_by_group]
	@zone_group_id int
AS


if @zone_group_id = -1
begin
	select z.zone_id, z.zone_desc, z.zone_status, s.site_name, g.zone_group_name, z.zone_name, p.UserName as publisher_name, z.created, z.updated, z.zone_type_id, z.locked, z.locked_by
	from dbo.cms_zones z with (nolock)
		left join dbo.cms_zone_groups g with (nolock) on z.zone_group_id = g.zone_group_id
		left join dbo.cms_sites s with (nolock) on g.site_id = s.site_id
		left join dbo.vw_aspnet_MembershipUsers p with (nolock) on z.publisher_id = p.UserId
    where zone_status <> 'D'
	order by z.zone_name
end
else
begin
	select z.zone_id, z.zone_desc, z.zone_status, s.site_name, g.zone_group_name, z.zone_name, p.UserName as publisher_name, z.created, z.updated, z.zone_type_id, z.locked, z.locked_by
	from dbo.cms_zones z with (nolock)
		left join dbo.cms_zone_groups g with (nolock) on z.zone_group_id = g.zone_group_id
		left join dbo.cms_sites s with (nolock) on g.site_id = s.site_id
		left join dbo.vw_aspnet_MembershipUsers p with (nolock) on z.publisher_id = p.UserId
	where z.zone_group_id = @zone_group_id and zone_status <> 'D'
	order by z.zone_name
end







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_select_zones_groups_by_site]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_select_zones_groups_by_site]
	@site_id int
AS


if @site_id = -1
begin
	select zg.zone_group_id, zg.zone_group_name, zg.publisher_id, s.site_name, p.UserName as publisher_name, zg.created, zg.updated
	from dbo.cms_zone_groups zg with (nolock)
		left join dbo.cms_sites s with (nolock) on zg.site_id = s.site_id
		left join dbo.vw_aspnet_MembershipUsers p with (nolock) on zg.publisher_id = p.UserId
	order by s.site_name, zg.zone_group_name
end
else
begin
	select zg.zone_group_id, zg.zone_group_name, zg.publisher_id, s.site_name, p.UserName as publisher_name, zg.created, zg.updated
	from dbo.cms_zone_groups zg with (nolock)
		left join dbo.cms_sites s with (nolock) on zg.site_id = s.site_id
		left join dbo.vw_aspnet_MembershipUsers p with (nolock) on zg.publisher_id = p.UserId
	where zg.site_id = @site_id
	order by s.site_name, zg.zone_group_name
end







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_update_article_clicks]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[cms_asp_update_article_clicks]
	@article_id int
as

update dbo.cms_articles
set
	clicks = clicks + 1
where article_id = @article_id







GO
/****** Object:  StoredProcedure [dbo].[cms_asp_update_article_rating]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[cms_asp_update_article_rating]
	@article_id int,
	@rate int
as

update dbo.cms_articles
set
	rating = rating + @rate, 
	ratingcount = ratingcount + 1
where article_id = @article_id







GO
/****** Object:  StoredProcedure [dbo].[cms_Site_GetAllSites]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 
CREATE PROC [dbo].[cms_Site_GetAllSites]
@GroupId INT
AS
BEGIN

	SET NOCOUNT ON;

	IF (@GroupId = -1)
	BEGIN

		SELECT 
			s.site_id, 
			s.site_name, 
			s.publisher_id, 
			s.created, 
			s.updated, 
			p.UserName AS publisher_name, 
			s.group_id, 
			sg.group_name
		FROM dbo.cms_sites s WITH (NOLOCK)
			LEFT JOIN dbo.vw_aspnet_MembershipUsers p WITH (NOLOCK) ON p.UserId = s.publisher_id
			LEFT JOIN  dbo.cms_structure_groups sg WITH (NOLOCK) ON sg.group_id = s.group_id
		ORDER BY sg.group_name ASC, s.site_name

	END
	ELSE
	BEGIN

		SELECT 
			s.site_id, 
			s.site_name, 
			s.publisher_id, 
			s.created, 
			s.updated, 
			p.UserName AS publisher_name, 
			s.group_id, 
			sg.group_name
		FROM dbo.cms_sites s WITH (NOLOCK)
			LEFT JOIN dbo.vw_aspnet_MembershipUsers p WITH (NOLOCK) ON p.UserId = s.publisher_id
			LEFT JOIN  dbo.cms_structure_groups sg WITH (NOLOCK) ON sg.group_id = s.group_id
		WHERE s.group_id = @GroupId  
		ORDER BY sg.group_name ASC, s.site_name

	END
	
END






GO
/****** Object:  StoredProcedure [dbo].[cms_WebEvent_ClearAllEvent]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[cms_WebEvent_ClearAllEvent] AS
    DELETE 
    FROM dbo.aspnet_WebEvent_Events





GO
/****** Object:  StoredProcedure [dbo].[cms_WebEvent_GetLogEvent]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[cms_WebEvent_GetLogEvent]
@WebEventType		   varchar(50),
@BeginDate			   datetime,
@EndDate			   datetime,
@PageIndex             int,
@PageSize              int,
@TotalRecords          int OUTPUT 
AS
	-- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForWebEvents
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        EventId char(32)
    )
 
	IF (@WebEventType <> '')
	BEGIN

		-- Insert into our temp table
		INSERT INTO #PageIndexForWebEvents (EventId)
		SELECT e.EventId
		FROM   dbo.aspnet_WebEvent_Events e 
		WHERE e.Message = @WebEventType AND
			e.EventTime >= @BeginDate and e.EventTime <= @EndDate
		ORDER BY e.EventTime DESC

		SELECT @TotalRecords = @@ROWCOUNT

		SELECT e.*
		FROM   dbo.aspnet_WebEvent_Events e, #PageIndexForWebEvents p
		WHERE  e.EventId = p.EventId  AND e.Message = @WebEventType AND
			   p.IndexId >= @PageLowerBound AND p.IndexId <= @PageUpperBound AND
			   e.EventTime >= @BeginDate and e.EventTime <= @EndDate
		ORDER BY e.EventTime DESC
	END
	ELSE
	BEGIN

		-- Insert into our temp table
		INSERT INTO #PageIndexForWebEvents (EventId)
		SELECT e.EventId
		FROM   dbo.aspnet_WebEvent_Events e 
		WHERE e.EventTime >= @BeginDate and e.EventTime <= @EndDate
		ORDER BY e.EventTime DESC

		SELECT @TotalRecords = @@ROWCOUNT

		SELECT e.*
		FROM   dbo.aspnet_WebEvent_Events e, #PageIndexForWebEvents p
		WHERE  e.EventId = p.EventId AND
			   p.IndexId >= @PageLowerBound AND p.IndexId <= @PageUpperBound AND
			   e.EventTime >= @BeginDate and e.EventTime <= @EndDate
		ORDER BY e.EventTime DESC
	END

   -- RETURN @TotalRecords





GO
/****** Object:  StoredProcedure [dbo].[cms_Zone_GetAllZones]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[cms_Zone_GetAllZones]
	@ZoneGroupId INT
AS
BEGIN


	IF @ZoneGroupId = -1
	BEGIN

		SELECT 
			z.zone_id, 
			z.zone_desc, 
			z.zone_status, 
			s.site_name, 
			g.zone_group_name, 
			z.zone_name, 
			p.UserName AS publisher_name, 
			z.created, 
			z.updated, 
			z.zone_type_id, 
			z.locked, 
			z.locked_by
		FROM dbo.cms_zones z WITH (NOLOCK)
			LEFT JOIN dbo.cms_zone_groups g WITH (NOLOCK) ON z.zone_group_id = g.zone_group_id
			LEFT JOIN dbo.cms_sites s WITH (NOLOCK) ON g.site_id = s.site_id
			LEFT JOIN dbo.vw_aspnet_MembershipUsers p WITH (NOLOCK) ON z.publisher_id = p.UserId
		WHERE zone_status <> 'D'
		ORDER BY z.zone_name

	END
	ELSE
	BEGIN

		SELECT 
			z.zone_id, 
			z.zone_desc, 
			z.zone_status, 
			s.site_name, 
			g.zone_group_name, 
			z.zone_name, 
			p.UserName AS publisher_name, 
			z.created, 
			z.updated, 
			z.zone_type_id, 
			z.locked, 
			z.locked_by
		FROM dbo.cms_zones z WITH (nolock)
			LEFT JOIN dbo.cms_zone_groups g WITH (NOLOCK) ON z.zone_group_id = g.zone_group_id
			LEFT JOIN dbo.cms_sites s WITH (NOLOCK) ON g.site_id = s.site_id
			LEFT JOIN dbo.vw_aspnet_MembershipUsers p WITH (NOLOCK) ON z.publisher_id = p.UserId
		WHERE z.zone_group_id = @ZoneGroupId AND zone_status <> 'D'
		ORDER BY z.zone_name

	END

END





GO
/****** Object:  StoredProcedure [dbo].[cms_ZoneGroup_GetAllZoneGroups]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[cms_ZoneGroup_GetAllZoneGroups]
	@SiteId INT
AS
BEGIN

	SET NOCOUNT ON;

	IF @SiteId = -1
	BEGIN

		SELECT 
			zg.zone_group_id, 
			zg.zone_group_name, 
			zg.publisher_id, 
			s.site_name, 
			p.UserName AS publisher_name, 
			zg.created, 
			zg.updated
		FROM dbo.cms_zone_groups zg WITH (NOLOCK)
			LEFT JOIN dbo.cms_sites s WITH (NOLOCK) ON zg.site_id = s.site_id
			LEFT JOIN dbo.vw_aspnet_MembershipUsers p WITH (NOLOCK) ON zg.publisher_id = p.UserId
		ORDER by s.site_name, zg.zone_group_name

	END
	ELSE
	BEGIN

		SELECT 
			zg.zone_group_id, 
			zg.zone_group_name, 
			zg.publisher_id, 
			s.site_name, 
			p.UserName AS publisher_name, 
			zg.created, 
			zg.updated
		FROM dbo.cms_zone_groups zg WITH (NOLOCK)
			LEFT JOIN dbo.cms_sites s WITH (NOLOCK) ON zg.site_id = s.site_id
			LEFT JOIN dbo.vw_aspnet_MembershipUsers p WITH (NOLOCK) ON zg.publisher_id = p.UserId
		WHERE zg.site_id = @SiteId
		ORDER BY s.site_name, zg.zone_group_name

	END

END







GO
/****** Object:  UserDefinedFunction [dbo].[fn_cms_AccessRules_GetUserRules]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fn_cms_AccessRules_GetUserRules] (@UserName varchar(100))
RETURNS @ReturnTable TABLE 
(
    ContentId int NOT NULL,
    SecurityAction tinyint NOT NULL
)
AS
BEGIN
 
 INSERT INTO @ReturnTable
	 SELECT ar.ContentId, ar.SecurityAction
		FROM dbo.vw_aspnet_UsersInRoles uir 
			INNER JOIN dbo.aspnet_Users u ON u.UserId = uir.UserId
			INNER JOIN dbo.vw_aspnet_Roles r ON r.RoleId = uir.RoleId
			INNER JOIN dbo.vw_cms_AccessRules_Site ar 
					ON ar.Roles like '%'+ r.RoleName+'%'  OR ar.Users like '%'+ @UserName +'%'
		WHERE u.UserName = @UserName
 
 RETURN
END





GO
/****** Object:  Table [dbo].[aspnet_Applications]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Applications](
	[ApplicationName] [nvarchar](256) NOT NULL,
	[LoweredApplicationName] [nvarchar](256) NOT NULL,
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](256) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[aspnet_Membership]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Membership](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordFormat] [int] NOT NULL,
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[MobilePIN] [nvarchar](16) NULL,
	[Email] [nvarchar](256) NULL,
	[LoweredEmail] [nvarchar](256) NULL,
	[PasswordQuestion] [nvarchar](256) NULL,
	[PasswordAnswer] [nvarchar](128) NULL,
	[IsApproved] [bit] NOT NULL,
	[IsLockedOut] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastLoginDate] [datetime] NOT NULL,
	[LastPasswordChangedDate] [datetime] NOT NULL,
	[LastLockoutDate] [datetime] NOT NULL,
	[FailedPasswordAttemptCount] [int] NOT NULL,
	[FailedPasswordAttemptWindowStart] [datetime] NOT NULL,
	[FailedPasswordAnswerAttemptCount] [int] NOT NULL,
	[FailedPasswordAnswerAttemptWindowStart] [datetime] NOT NULL,
	[Comment] [ntext] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[aspnet_Paths]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Paths](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[PathId] [uniqueidentifier] NOT NULL,
	[Path] [nvarchar](256) NOT NULL,
	[LoweredPath] [nvarchar](256) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[aspnet_PersonalizationAllUsers]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_PersonalizationAllUsers](
	[PathId] [uniqueidentifier] NOT NULL,
	[PageSettings] [image] NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PathId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[aspnet_PersonalizationPerUser]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_PersonalizationPerUser](
	[Id] [uniqueidentifier] NOT NULL,
	[PathId] [uniqueidentifier] NULL,
	[UserId] [uniqueidentifier] NULL,
	[PageSettings] [image] NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[aspnet_Profile]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Profile](
	[UserId] [uniqueidentifier] NOT NULL,
	[PropertyNames] [ntext] NOT NULL,
	[PropertyValuesString] [ntext] NOT NULL,
	[PropertyValuesBinary] [image] NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[aspnet_Roles]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Roles](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[RoleName] [nvarchar](256) NOT NULL,
	[LoweredRoleName] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](256) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[aspnet_SchemaVersions]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_SchemaVersions](
	[Feature] [nvarchar](128) NOT NULL,
	[CompatibleSchemaVersion] [nvarchar](128) NOT NULL,
	[IsCurrentVersion] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Feature] ASC,
	[CompatibleSchemaVersion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[aspNet_SqlCacheTablesForChangeNotification]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspNet_SqlCacheTablesForChangeNotification](
	[tableName] [nvarchar](450) NOT NULL,
	[notificationCreated] [datetime] NOT NULL,
	[changeId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[tableName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[aspnet_Users]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Users](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[LoweredUserName] [nvarchar](256) NOT NULL,
	[MobileAlias] [nvarchar](16) NULL,
	[IsAnonymous] [bit] NOT NULL,
	[LastActivityDate] [datetime] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[aspnet_UsersInRoles]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_UsersInRoles](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[aspnet_WebEvent_Events]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[aspnet_WebEvent_Events](
	[EventId] [char](32) NOT NULL,
	[EventTimeUtc] [datetime] NOT NULL,
	[EventTime] [datetime] NOT NULL,
	[EventType] [nvarchar](256) NOT NULL,
	[EventSequence] [decimal](19, 0) NOT NULL,
	[EventOccurrence] [decimal](19, 0) NOT NULL,
	[EventCode] [int] NOT NULL,
	[EventDetailCode] [int] NOT NULL,
	[Message] [nvarchar](1024) NULL,
	[ApplicationPath] [nvarchar](256) NULL,
	[ApplicationVirtualPath] [nvarchar](256) NULL,
	[MachineName] [nvarchar](256) NOT NULL,
	[RequestUrl] [nvarchar](1024) NULL,
	[ExceptionType] [nvarchar](256) NULL,
	[Details] [ntext] NULL,
PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_AccessRules]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_AccessRules](
	[RuleId] [int] IDENTITY(1,1) NOT NULL,
	[RuleName] [nvarchar](100) NULL,
	[ContentId] [varchar](20) NOT NULL,
	[ContentType] [varchar](20) NOT NULL,
	[Roles] [varchar](max) NULL,
	[Users] [varchar](max) NULL,
	[Permissions] [varchar](max) NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [varchar](100) NOT NULL,
	[Updated] [datetime] NULL,
	[UpdatedBy] [varchar](100) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_article_cache]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_article_cache](
	[cache_id] [bigint] IDENTITY(1,1) NOT NULL,
	[zone_id] [int] NOT NULL,
	[article_id] [int] NOT NULL,
	[created] [datetime] NULL,
 CONSTRAINT [PK_cms_article_cache] PRIMARY KEY CLUSTERED 
(
	[cache_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cms_article_files]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_article_files](
	[file_id] [bigint] IDENTITY(1,1) NOT NULL,
	[article_id] [int] NOT NULL,
	[file_title] [nvarchar](500) NOT NULL,
	[file_order] [int] NOT NULL,
	[file_name_1] [varchar](255) NOT NULL,
	[file_name_2] [varchar](255) NOT NULL,
	[file_name_3] [varchar](255) NOT NULL,
	[file_name_4] [varchar](255) NOT NULL,
	[file_name_5] [varchar](255) NOT NULL,
	[file_name_6] [varchar](255) NOT NULL,
	[file_name_7] [varchar](255) NOT NULL,
	[file_name_8] [varchar](255) NOT NULL,
	[file_name_9] [varchar](255) NOT NULL,
	[file_name_10] [varchar](255) NOT NULL,
	[file_type_id] [int] NOT NULL,
	[file_comment] [ntext] NOT NULL,
 CONSTRAINT [PK_cms_article_files_1] PRIMARY KEY CLUSTERED 
(
	[file_id] ASC,
	[article_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_article_files_revision]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_article_files_revision](
	[rev_id] [bigint] IDENTITY(1,1) NOT NULL,
	[created] [datetime] NOT NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[rev_date] [datetime] NOT NULL,
	[revision_status] [char](1) NOT NULL,
	[revised_by] [uniqueidentifier] NOT NULL,
	[approval_date] [datetime] NULL,
	[approval_id] [uniqueidentifier] NULL,
	[article_id] [int] NOT NULL,
 CONSTRAINT [PK_cms_article_files_revision] PRIMARY KEY CLUSTERED 
(
	[rev_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_article_files_revision_files]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_article_files_revision_files](
	[af_rf_id] [bigint] IDENTITY(1,1) NOT NULL,
	[rev_id] [bigint] NOT NULL,
	[article_id] [int] NOT NULL,
	[file_title] [nvarchar](500) NOT NULL,
	[file_order] [int] NOT NULL,
	[file_name_1] [varchar](255) NOT NULL,
	[file_name_2] [varchar](255) NOT NULL,
	[file_name_3] [varchar](255) NOT NULL,
	[file_name_4] [varchar](255) NOT NULL,
	[file_name_5] [varchar](255) NOT NULL,
	[file_name_6] [varchar](255) NOT NULL,
	[file_name_7] [varchar](255) NOT NULL,
	[file_name_8] [varchar](255) NOT NULL,
	[file_name_9] [varchar](255) NOT NULL,
	[file_name_10] [varchar](255) NOT NULL,
	[file_type_id] [int] NOT NULL,
	[file_comment] [ntext] NOT NULL,
 CONSTRAINT [PK_cms_article_files_revision_files] PRIMARY KEY CLUSTERED 
(
	[af_rf_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_article_relation]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_article_relation](
	[ar_id] [bigint] IDENTITY(1,1) NOT NULL,
	[article_id] [int] NOT NULL,
	[related_zone_id] [int] NOT NULL,
	[related_article_id] [int] NOT NULL,
 CONSTRAINT [PK_cms_article_relation] PRIMARY KEY CLUSTERED 
(
	[ar_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cms_article_relation_revision]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_article_relation_revision](
	[arr_id] [bigint] IDENTITY(1,1) NOT NULL,
	[rev_id] [bigint] NOT NULL,
	[article_id] [int] NOT NULL,
	[related_zone_id] [int] NOT NULL,
	[related_article_id] [int] NOT NULL,
 CONSTRAINT [PK_cms_article_relation_revision] PRIMARY KEY CLUSTERED 
(
	[arr_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cms_article_revision]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_article_revision](
	[rev_id] [bigint] IDENTITY(1,1) NOT NULL,
	[created] [datetime] NOT NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[rev_date] [datetime] NOT NULL,
	[revision_status] [char](1) NOT NULL,
	[revised_by] [uniqueidentifier] NOT NULL,
	[rev_name] [nvarchar](100) NOT NULL,
	[rev_note] [ntext] NOT NULL,
	[rev_flag_1] [bit] NOT NULL,
	[rev_flag_2] [bit] NOT NULL,
	[rev_flag_3] [bit] NOT NULL,
	[rev_flag_4] [bit] NOT NULL,
	[rev_flag_5] [bit] NOT NULL,
	[approval_date] [datetime] NULL,
	[approval_id] [uniqueidentifier] NULL,
	[article_id] [int] NOT NULL,
	[clsf_id] [int] NOT NULL,
	[status] [tinyint] NOT NULL,
	[startdate] [datetime] NOT NULL,
	[enddate] [datetime] NULL,
	[orderno] [int] NOT NULL,
	[lang_id] [char](2) NOT NULL,
	[navigation_display] [tinyint] NOT NULL,
	[navigation_zone_id] [int] NOT NULL,
	[menu_text] [nvarchar](100) NOT NULL,
	[headline] [nvarchar](350) NOT NULL,
	[summary] [nvarchar](3000) NOT NULL,
	[keywords] [nvarchar](500) NOT NULL,
	[article_type] [tinyint] NOT NULL,
	[article_type_detail] [nvarchar](500) NOT NULL,
	[article_1] [ntext] NOT NULL,
	[article_2] [ntext] NOT NULL,
	[article_3] [ntext] NOT NULL,
	[article_4] [ntext] NOT NULL,
	[article_5] [ntext] NOT NULL,
	[custom_1] [ntext] NOT NULL,
	[custom_2] [ntext] NOT NULL,
	[custom_3] [ntext] NOT NULL,
	[custom_4] [ntext] NOT NULL,
	[custom_5] [ntext] NOT NULL,
	[custom_6] [ntext] NOT NULL,
	[custom_7] [ntext] NOT NULL,
	[custom_8] [ntext] NOT NULL,
	[custom_9] [ntext] NOT NULL,
	[custom_10] [ntext] NOT NULL,
	[custom_11] [nvarchar](max) NOT NULL,
	[custom_12] [nvarchar](max) NOT NULL,
	[custom_13] [nvarchar](max) NOT NULL,
	[custom_14] [nvarchar](max) NOT NULL,
	[custom_15] [nvarchar](max) NOT NULL,
	[custom_16] [nvarchar](max) NOT NULL,
	[custom_17] [nvarchar](max) NOT NULL,
	[custom_18] [nvarchar](max) NOT NULL,
	[custom_19] [nvarchar](max) NOT NULL,
	[custom_20] [nvarchar](max) NOT NULL,
	[flag_1] [bit] NOT NULL,
	[flag_2] [bit] NOT NULL,
	[flag_3] [bit] NOT NULL,
	[flag_4] [bit] NOT NULL,
	[flag_5] [bit] NOT NULL,
	[date_1] [datetime] NULL,
	[date_2] [datetime] NULL,
	[date_3] [datetime] NULL,
	[date_4] [datetime] NULL,
	[date_5] [datetime] NULL,
	[cl_1] [tinyint] NOT NULL,
	[cl_2] [tinyint] NOT NULL,
	[cl_3] [tinyint] NOT NULL,
	[cl_4] [tinyint] NOT NULL,
	[cl_5] [tinyint] NOT NULL,
	[custom_body] [nvarchar](200) NOT NULL,
	[meta_description] [nvarchar](2000) NOT NULL,
	[content_1_editor_type] [char](1) NOT NULL,
	[content_2_editor_type] [char](1) NOT NULL,
	[content_3_editor_type] [char](1) NOT NULL,
	[content_4_editor_type] [char](1) NOT NULL,
	[content_5_editor_type] [char](1) NOT NULL,
	[omniture_code] [ntext] NOT NULL,
	[custom_setting] [varchar](4000) NOT NULL,
	[before_head] [nvarchar](max) NOT NULL,
	[before_body] [nvarchar](max) NOT NULL,
	[no_index_no_follow] [bit] NOT NULL,
	[custom_html_attr] [nvarchar](max) NOT NULL,
	[meta_title] [nvarchar](max) NOT NULL,
	[canonical_url] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_cms_article_revision] PRIMARY KEY CLUSTERED 
(
	[rev_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_article_search]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_article_search](
	[search_id] [int] IDENTITY(1,1) NOT NULL,
	[article_id] [int] NOT NULL,
	[zone_id] [int] NOT NULL,
	[search_text] [ntext] NOT NULL,
	[headline] [nvarchar](350) NOT NULL,
	[summary] [nvarchar](3000) NOT NULL,
	[keywords] [nvarchar](2000) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_cms_article_search] PRIMARY KEY CLUSTERED 
(
	[search_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cms_article_zones]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_article_zones](
	[rel_id] [bigint] IDENTITY(1,1) NOT NULL,
	[article_id] [int] NOT NULL,
	[zone_id] [int] NOT NULL,
	[az_order] [int] NOT NULL,
	[az_alias] [nvarchar](300) NOT NULL,
	[is_alias_protected] [bit] NOT NULL,
 CONSTRAINT [PK_cms_article_zones] PRIMARY KEY CLUSTERED 
(
	[rel_id] ASC,
	[article_id] ASC,
	[zone_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cms_article_zones_revision]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_article_zones_revision](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[rev_id] [bigint] NOT NULL,
	[article_id] [int] NOT NULL,
	[zone_id] [int] NOT NULL,
	[az_order] [int] NOT NULL,
	[az_alias] [nvarchar](300) NOT NULL,
	[is_alias_protected] [bit] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cms_articles]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_articles](
	[article_id] [int] IDENTITY(1,1) NOT NULL,
	[clsf_id] [int] NOT NULL,
	[status] [tinyint] NOT NULL,
	[created] [datetime] NOT NULL,
	[updated] [datetime] NOT NULL,
	[startdate] [datetime] NULL,
	[enddate] [datetime] NULL,
	[publisher_id] [uniqueidentifier] NOT NULL,
	[clicks] [int] NOT NULL,
	[orderno] [int] NOT NULL,
	[lang_id] [char](2) NOT NULL,
	[navigation_display] [tinyint] NOT NULL,
	[navigation_zone_id] [int] NOT NULL,
	[menu_text] [nvarchar](100) NOT NULL,
	[headline] [nvarchar](350) NOT NULL,
	[summary] [nvarchar](3000) NOT NULL,
	[keywords] [nvarchar](500) NOT NULL,
	[article_type] [tinyint] NOT NULL,
	[article_type_detail] [nvarchar](500) NOT NULL,
	[article_1] [ntext] NOT NULL,
	[article_2] [ntext] NOT NULL,
	[article_3] [ntext] NOT NULL,
	[article_4] [ntext] NOT NULL,
	[article_5] [ntext] NOT NULL,
	[custom_1] [ntext] NOT NULL,
	[custom_2] [ntext] NOT NULL,
	[custom_3] [ntext] NOT NULL,
	[custom_4] [ntext] NOT NULL,
	[custom_5] [ntext] NOT NULL,
	[custom_6] [ntext] NOT NULL,
	[custom_7] [ntext] NOT NULL,
	[custom_8] [ntext] NOT NULL,
	[custom_9] [ntext] NOT NULL,
	[custom_10] [ntext] NOT NULL,
	[custom_11] [nvarchar](max) NOT NULL,
	[custom_12] [nvarchar](max) NOT NULL,
	[custom_13] [nvarchar](max) NOT NULL,
	[custom_14] [nvarchar](max) NOT NULL,
	[custom_15] [nvarchar](max) NOT NULL,
	[custom_16] [nvarchar](max) NOT NULL,
	[custom_17] [nvarchar](max) NOT NULL,
	[custom_18] [nvarchar](max) NOT NULL,
	[custom_19] [nvarchar](max) NOT NULL,
	[custom_20] [nvarchar](max) NOT NULL,
	[flag_1] [bit] NOT NULL,
	[flag_2] [bit] NOT NULL,
	[flag_3] [bit] NOT NULL,
	[flag_4] [bit] NOT NULL,
	[flag_5] [bit] NOT NULL,
	[date_1] [datetime] NULL,
	[date_2] [datetime] NULL,
	[date_3] [datetime] NULL,
	[date_4] [datetime] NULL,
	[date_5] [datetime] NULL,
	[cl_1] [tinyint] NOT NULL,
	[cl_2] [tinyint] NOT NULL,
	[cl_3] [tinyint] NOT NULL,
	[cl_4] [tinyint] NOT NULL,
	[cl_5] [tinyint] NOT NULL,
	[custom_body] [nvarchar](200) NOT NULL,
	[rating] [int] NOT NULL,
	[ratingcount] [int] NOT NULL,
	[locked] [datetime] NULL,
	[locked_by] [uniqueidentifier] NULL,
	[meta_description] [nvarchar](2000) NOT NULL,
	[omniture_code] [ntext] NOT NULL,
	[custom_setting] [varchar](4000) NOT NULL,
	[before_head] [nvarchar](max) NOT NULL,
	[before_body] [nvarchar](max) NOT NULL,
	[no_index_no_follow] [bit] NOT NULL,
	[custom_html_attr] [nvarchar](max) NOT NULL,
	[meta_title] [nvarchar](max) NOT NULL,
	[canonical_url] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_cms_articles] PRIMARY KEY CLUSTERED 
(
	[article_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_asp_errors]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_asp_errors](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MonitorSent] [char](1) NOT NULL,
	[SessionID] [varchar](50) NULL,
	[RequestMethod] [varchar](5) NULL,
	[ServerPort] [varchar](50) NULL,
	[HTTPS] [varchar](5) NULL,
	[LocalAddr] [varchar](255) NULL,
	[HostAddress] [varchar](255) NULL,
	[UserAgent] [varchar](500) NULL,
	[URL] [varchar](1000) NULL,
	[CustomerRefID] [varchar](4000) NULL,
	[FormData] [nvarchar](max) NULL,
	[AllHTTP] [varchar](max) NULL,
	[ErrASPCode] [varchar](4000) NULL,
	[ErrNumber] [varchar](4000) NULL,
	[ErrSource] [varchar](4000) NULL,
	[ErrCategory] [varchar](4000) NULL,
	[ErrFile] [varchar](4000) NULL,
	[ErrLine] [int] NULL,
	[ErrColumn] [int] NULL,
	[ErrDescription] [varchar](1000) NULL,
	[ErrAspDescription] [varchar](1000) NULL,
	[InsertDate] [datetime] NOT NULL,
	[AAS_Checked] [char](1) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_breadcrumbs]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_breadcrumbs](
	[breadcrumb_id] [int] IDENTITY(1,1) NOT NULL,
	[breadcrumb_name] [nvarchar](50) NOT NULL,
	[deep_level] [tinyint] NOT NULL,
	[include_site] [char](1) NOT NULL,
	[include_zonegroup] [char](1) NOT NULL,
	[include_headline] [char](1) NOT NULL,
	[created] [datetime] NOT NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[updated] [datetime] NULL,
	[updated_by] [uniqueidentifier] NULL,
	[excluded_sites] [text] NOT NULL,
	[excluded_zonegroups] [text] NOT NULL,
	[excluded_zones] [text] NOT NULL,
	[seperator] [nvarchar](50) NOT NULL,
	[ul_class] [varchar](100) NOT NULL,
	[include_submenus] [char](1) NOT NULL,
	[breadcrumb_main_container] [varchar](10) NOT NULL,
	[breadcrumb_main_item_container] [varchar](10) NOT NULL,
	[breadcrumb_sub_container] [varchar](10) NOT NULL,
	[breadcrumb_sub_item_container] [varchar](10) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_cache_update]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_cache_update](
	[id] [tinyint] IDENTITY(1,1) NOT NULL,
	[status] [tinyint] NOT NULL,
	[timeout] [datetime] NOT NULL,
	[server_ip] [varchar](100) NOT NULL,
	[updated] [datetime] NOT NULL,
 CONSTRAINT [PK_cacheupdate] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_classification_combo_values]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_classification_combo_values](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[classification_id] [int] NOT NULL,
	[column_no] [tinyint] NOT NULL,
	[combo_supid] [ntext] NOT NULL,
	[combo_label] [nvarchar](255) NOT NULL,
	[combo_value] [nvarchar](255) NOT NULL,
	[combo_order] [int] NOT NULL,
	[created] [datetime] NOT NULL,
	[created_by] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_cms_classification_combo_values] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cms_classifications]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_classifications](
	[classification_id] [int] IDENTITY(1,1) NOT NULL,
	[classification_name] [nvarchar](100) NOT NULL,
	[created] [datetime] NOT NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[summary_cb] [bit] NOT NULL,
	[enddate_cb] [bit] NOT NULL,
	[keywords_cb] [bit] NOT NULL,
	[custom1_cb] [bit] NOT NULL,
	[custom2_cb] [bit] NOT NULL,
	[custom3_cb] [bit] NOT NULL,
	[custom4_cb] [bit] NOT NULL,
	[custom5_cb] [bit] NOT NULL,
	[custom6_cb] [bit] NOT NULL,
	[custom7_cb] [bit] NOT NULL,
	[custom8_cb] [bit] NOT NULL,
	[custom9_cb] [bit] NOT NULL,
	[custom10_cb] [bit] NOT NULL,
	[custom11_cb] [bit] NOT NULL,
	[custom12_cb] [bit] NOT NULL,
	[custom13_cb] [bit] NOT NULL,
	[custom14_cb] [bit] NOT NULL,
	[custom15_cb] [bit] NOT NULL,
	[custom16_cb] [bit] NOT NULL,
	[custom17_cb] [bit] NOT NULL,
	[custom18_cb] [bit] NOT NULL,
	[custom19_cb] [bit] NOT NULL,
	[custom20_cb] [bit] NOT NULL,
	[flag1_cb] [bit] NOT NULL,
	[flag2_cb] [bit] NOT NULL,
	[flag3_cb] [bit] NOT NULL,
	[flag4_cb] [bit] NOT NULL,
	[flag5_cb] [bit] NOT NULL,
	[date1_cb] [bit] NOT NULL,
	[date2_cb] [bit] NOT NULL,
	[date3_cb] [bit] NOT NULL,
	[date4_cb] [bit] NOT NULL,
	[date5_cb] [bit] NOT NULL,
	[custom1_text] [nvarchar](50) NOT NULL,
	[custom2_text] [nvarchar](50) NOT NULL,
	[custom3_text] [nvarchar](50) NOT NULL,
	[custom4_text] [nvarchar](50) NOT NULL,
	[custom5_text] [nvarchar](50) NOT NULL,
	[custom6_text] [nvarchar](50) NOT NULL,
	[custom7_text] [nvarchar](50) NOT NULL,
	[custom8_text] [nvarchar](50) NOT NULL,
	[custom9_text] [nvarchar](50) NOT NULL,
	[custom10_text] [nvarchar](50) NOT NULL,
	[custom11_text] [nvarchar](50) NOT NULL,
	[custom12_text] [nvarchar](50) NOT NULL,
	[custom13_text] [nvarchar](50) NOT NULL,
	[custom14_text] [nvarchar](50) NOT NULL,
	[custom15_text] [nvarchar](50) NOT NULL,
	[custom16_text] [nvarchar](50) NOT NULL,
	[custom17_text] [nvarchar](50) NOT NULL,
	[custom18_text] [nvarchar](50) NOT NULL,
	[custom19_text] [nvarchar](50) NOT NULL,
	[custom20_text] [nvarchar](50) NOT NULL,
	[flag1_text] [nvarchar](50) NOT NULL,
	[flag2_text] [nvarchar](50) NOT NULL,
	[flag3_text] [nvarchar](50) NOT NULL,
	[flag4_text] [nvarchar](50) NOT NULL,
	[flag5_text] [nvarchar](50) NOT NULL,
	[date1_text] [nvarchar](50) NOT NULL,
	[date2_text] [nvarchar](50) NOT NULL,
	[date3_text] [nvarchar](50) NOT NULL,
	[date4_text] [nvarchar](50) NOT NULL,
	[date5_text] [nvarchar](50) NOT NULL,
	[custom1_type] [char](1) NOT NULL,
	[custom2_type] [char](1) NOT NULL,
	[custom3_type] [char](1) NOT NULL,
	[custom4_type] [char](1) NOT NULL,
	[custom5_type] [char](1) NOT NULL,
	[custom6_type] [char](1) NOT NULL,
	[custom7_type] [char](1) NOT NULL,
	[custom8_type] [char](1) NOT NULL,
	[custom9_type] [char](1) NOT NULL,
	[custom10_type] [char](1) NOT NULL,
	[summary_text] [nvarchar](50) NOT NULL,
	[enddate_text] [nvarchar](50) NOT NULL,
	[keywords_text] [nvarchar](50) NOT NULL,
	[article1_text] [nvarchar](50) NOT NULL,
	[article2_text] [nvarchar](50) NOT NULL,
	[article3_text] [nvarchar](50) NOT NULL,
	[article4_text] [nvarchar](50) NOT NULL,
	[article5_text] [nvarchar](50) NOT NULL,
	[article1_cb] [bit] NOT NULL,
	[article2_cb] [bit] NOT NULL,
	[article3_cb] [bit] NOT NULL,
	[article4_cb] [bit] NOT NULL,
	[article5_cb] [bit] NOT NULL,
	[custom1_subcolumn] [tinyint] NOT NULL,
	[custom2_subcolumn] [tinyint] NOT NULL,
	[custom3_subcolumn] [tinyint] NOT NULL,
	[custom4_subcolumn] [tinyint] NOT NULL,
	[custom5_subcolumn] [tinyint] NOT NULL,
	[custom6_subcolumn] [tinyint] NOT NULL,
	[custom7_subcolumn] [tinyint] NOT NULL,
	[custom8_subcolumn] [tinyint] NOT NULL,
	[custom9_subcolumn] [tinyint] NOT NULL,
	[custom10_subcolumn] [tinyint] NOT NULL,
	[file_required_cb] [bit] NOT NULL,
	[file_title_required_cb] [bit] NOT NULL,
	[file_description_required_cb] [bit] NOT NULL,
	[required_file_types] [varchar](8000) NOT NULL,
	[group_id] [int] NOT NULL,
	[structure_description] [nvarchar](2000) NOT NULL,
 CONSTRAINT [PK_cms_classifications] PRIMARY KEY CLUSTERED 
(
	[classification_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_config]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_config](
	[config_id] [int] IDENTITY(1,1) NOT NULL,
	[config_name] [varchar](30) NOT NULL,
	[config_value_local] [ntext] NOT NULL,
	[config_value_remote] [ntext] NOT NULL,
	[isDefault] [char](1) NOT NULL,
	[publisher_id] [uniqueidentifier] NOT NULL,
	[updated] [datetime] NOT NULL,
 CONSTRAINT [PK_CmsConfig] PRIMARY KEY CLUSTERED 
(
	[config_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_css]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_css](
	[css_id] [int] IDENTITY(1,1) NOT NULL,
	[css_name] [nvarchar](50) NOT NULL,
	[css_status] [char](1) NOT NULL,
	[css_type] [tinyint] NOT NULL,
	[css_code] [ntext] NOT NULL,
	[css_fix] [ntext] NOT NULL,
	[css_rel_text] [nvarchar](100) NOT NULL,
	[css_type_text] [nvarchar](100) NOT NULL,
	[publisher_id] [uniqueidentifier] NOT NULL,
	[created] [datetime] NOT NULL,
	[updated] [datetime] NOT NULL,
	[group_id] [int] NOT NULL,
	[structure_description] [nvarchar](2000) NOT NULL,
 CONSTRAINT [PK_css] PRIMARY KEY CLUSTERED 
(
	[css_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_css_revisions]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_css_revisions](
	[history_id] [int] IDENTITY(1,1) NOT NULL,
	[css_id] [int] NOT NULL,
	[css_code] [ntext] NOT NULL,
	[css_fix] [ntext] NOT NULL,
	[css_rel_text] [nvarchar](100) NOT NULL,
	[css_type_text] [nvarchar](100) NOT NULL,
	[publisher_id] [uniqueidentifier] NOT NULL,
	[created] [datetime] NOT NULL,
	[css_type] [tinyint] NOT NULL,
 CONSTRAINT [PK_cms_css_revisions] PRIMARY KEY CLUSTERED 
(
	[history_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cms_custom_content]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_custom_content](
	[cc_id] [int] IDENTITY(1,1) NOT NULL,
	[cc_name] [nvarchar](50) NOT NULL,
	[cc_html] [ntext] NOT NULL,
	[created] [datetime] NOT NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[updated] [datetime] NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[group_id] [int] NOT NULL,
	[structure_description] [nvarchar](2000) NOT NULL,
 CONSTRAINT [PK_cms_custom_content] PRIMARY KEY CLUSTERED 
(
	[cc_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cms_custom_form]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_custom_form](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[sender_name] [varchar](100) NOT NULL,
	[sender_surname] [varchar](100) NOT NULL,
	[sender_mail] [varchar](100) NOT NULL,
	[sender_company] [varchar](250) NULL,
	[sender_phone] [varchar](20) NULL,
	[info_type] [varchar](50) NOT NULL,
	[subject] [varchar](200) NULL,
	[sub_subject] [varchar](200) NULL,
	[opinion] [text] NULL,
	[to_name] [varchar](100) NULL,
	[to_surname] [varchar](100) NULL,
	[to_mail] [varchar](100) NULL,
	[ip] [varchar](15) NULL,
	[created] [datetime] NOT NULL,
	[job] [varchar](200) NULL,
	[title] [varchar](200) NULL,
	[department] [varchar](200) NULL,
 CONSTRAINT [PK_cms_custom_form] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_domains]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_domains](
	[domain_id] [int] IDENTITY(1,1) NOT NULL,
	[domain_names] [varchar](8000) NOT NULL,
	[home_page_article] [varchar](50) NOT NULL,
	[domain_status] [char](1) NOT NULL,
	[created] [datetime] NOT NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[updated] [datetime] NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[error_page_article] [varchar](50) NOT NULL,
 CONSTRAINT [PK_cms_domains] PRIMARY KEY CLUSTERED 
(
	[domain_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_error_logs]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_error_logs](
	[ErrorId] [int] IDENTITY(1,1) NOT NULL,
	[ControllerName] [nvarchar](100) NULL,
	[ActionName] [nvarchar](100) NULL,
	[UserId] [uniqueidentifier] NULL,
	[LogDate] [datetime] NULL,
	[Comment] [nvarchar](max) NULL,
	[IP] [nvarchar](50) NULL,
	[Message] [nvarchar](max) NOT NULL,
	[InnerException] [nvarchar](max) NULL,
	[IsInCms] [bit] NOT NULL,
	[StackTrace] [nvarchar](max) NOT NULL,
	[AbsoluteUrl] [nvarchar](max) NOT NULL,
	[LineNumber] [int] NOT NULL,
 CONSTRAINT [PK_cms_error_logs] PRIMARY KEY CLUSTERED 
(
	[ErrorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cms_file_types]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_file_types](
	[type_id] [int] IDENTITY(1,1) NOT NULL,
	[type_name] [nvarchar](100) NOT NULL,
	[type_alias] [varchar](50) NOT NULL,
	[file1_name] [nvarchar](200) NOT NULL,
	[file2_name] [nvarchar](200) NOT NULL,
	[file3_name] [nvarchar](200) NOT NULL,
	[file4_name] [nvarchar](200) NOT NULL,
	[file5_name] [nvarchar](200) NOT NULL,
	[file6_name] [nvarchar](200) NOT NULL,
	[file7_name] [nvarchar](200) NOT NULL,
	[file8_name] [nvarchar](200) NOT NULL,
	[file9_name] [nvarchar](200) NOT NULL,
	[file10_name] [nvarchar](200) NOT NULL,
	[file1_extension] [varchar](100) NOT NULL,
	[file2_extension] [varchar](100) NOT NULL,
	[file3_extension] [varchar](100) NOT NULL,
	[file4_extension] [varchar](100) NOT NULL,
	[file5_extension] [varchar](100) NOT NULL,
	[file6_extension] [varchar](100) NOT NULL,
	[file7_extension] [varchar](100) NOT NULL,
	[file8_extension] [varchar](100) NOT NULL,
	[file9_extension] [varchar](100) NOT NULL,
	[file10_extension] [varchar](100) NOT NULL,
	[file1_wh] [varchar](10) NOT NULL,
	[file2_wh] [varchar](10) NOT NULL,
	[file3_wh] [varchar](10) NOT NULL,
	[file4_wh] [varchar](10) NOT NULL,
	[file5_wh] [varchar](10) NOT NULL,
	[file6_wh] [varchar](10) NOT NULL,
	[file7_wh] [varchar](10) NOT NULL,
	[file8_wh] [varchar](10) NOT NULL,
	[file9_wh] [varchar](10) NOT NULL,
	[file10_wh] [varchar](10) NOT NULL,
	[file1_size] [int] NOT NULL,
	[file2_size] [int] NOT NULL,
	[file3_size] [int] NOT NULL,
	[file4_size] [int] NOT NULL,
	[file5_size] [int] NOT NULL,
	[file6_size] [int] NOT NULL,
	[file7_size] [int] NOT NULL,
	[file8_size] [int] NOT NULL,
	[file9_size] [int] NOT NULL,
	[file10_size] [int] NOT NULL,
	[created] [datetime] NOT NULL,
	[updated] [datetime] NOT NULL,
	[group_id] [int] NOT NULL,
	[structure_description] [nvarchar](2000) NOT NULL,
 CONSTRAINT [PK_cms_file_types] PRIMARY KEY CLUSTERED 
(
	[type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_fop_failure_log]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_fop_failure_log](
	[log_id] [int] IDENTITY(1,1) NOT NULL,
	[op_action] [varchar](20) NOT NULL,
	[source_path] [nvarchar](300) NOT NULL,
	[dest_path] [nvarchar](300) NOT NULL,
	[file_name] [varchar](300) NOT NULL,
	[summary] [nvarchar](300) NOT NULL,
	[created] [datetime] NOT NULL,
	[retry_count] [tinyint] NOT NULL,
	[processed] [datetime] NULL,
	[processed_by] [int] NULL,
	[op_status] [char](1) NOT NULL,
 CONSTRAINT [PK_cms_fop_failure_log] PRIMARY KEY CLUSTERED 
(
	[log_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_hidden_values]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_hidden_values](
	[hidden_id] [int] IDENTITY(1,1) NOT NULL,
	[hidden_value] [nvarchar](50) NOT NULL,
	[hidden_type] [tinyint] NOT NULL,
	[hidden_desc] [nvarchar](100) NOT NULL,
	[created] [datetime] NOT NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[updated] [datetime] NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_cms_hidden_values] PRIMARY KEY CLUSTERED 
(
	[hidden_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cms_instant_messaging]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_instant_messaging](
	[ims_id] [bigint] IDENTITY(1,1) NOT NULL,
	[ims_from] [uniqueidentifier] NOT NULL,
	[ims_to] [uniqueidentifier] NOT NULL,
	[ims_subject] [nvarchar](100) NOT NULL,
	[ims_message] [ntext] NOT NULL,
	[ims_type] [char](2) NOT NULL,
	[related_id] [bigint] NOT NULL,
	[related_name] [nvarchar](500) NOT NULL,
	[created] [datetime] NOT NULL,
	[readed] [datetime] NULL,
	[processed] [datetime] NULL,
	[deleted] [datetime] NULL,
	[due] [datetime] NULL,
 CONSTRAINT [PK_cms_instant_messaging] PRIMARY KEY CLUSTERED 
(
	[ims_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_language_relations]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_language_relations](
	[lr_id] [bigint] NOT NULL,
	[zone_id] [int] NOT NULL,
	[article_id] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cms_language_relations_revision]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_language_relations_revision](
	[lr_id] [bigint] NOT NULL,
	[rev_id] [bigint] NOT NULL,
	[zone_id] [int] NOT NULL,
	[article_id] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cms_languages]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_languages](
	[lang_id] [char](2) NOT NULL,
	[lang_name] [nvarchar](50) NOT NULL,
	[lang_xml] [ntext] NOT NULL,
	[lang_order] [int] NOT NULL,
	[publisher_id] [uniqueidentifier] NOT NULL,
	[created] [datetime] NOT NULL,
	[updated] [datetime] NOT NULL,
	[lang_alias] [nvarchar](300) NOT NULL,
 CONSTRAINT [PK_languages] PRIMARY KEY CLUSTERED 
(
	[lang_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_page_redirection]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_page_redirection](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RedirectFrom] [nvarchar](max) NOT NULL,
	[RedirectTo] [nvarchar](250) NOT NULL,
	[Created] [datetime] NULL,
	[Updated] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_cms_page_redirection] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cms_plugins]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_plugins](
	[plugin_id] [int] IDENTITY(1,1) NOT NULL,
	[plugin_status] [tinyint] NOT NULL,
	[plugin_name] [nvarchar](100) NOT NULL,
	[plugin_code] [ntext] NOT NULL,
	[created] [datetime] NOT NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[updated] [datetime] NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[group_id] [int] NOT NULL,
	[structure_description] [nvarchar](2000) NOT NULL,
 CONSTRAINT [PK_cms_plugins] PRIMARY KEY CLUSTERED 
(
	[plugin_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cms_portlets]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_portlets](
	[portlet_id] [int] IDENTITY(1,1) NOT NULL,
	[portlet_name] [nvarchar](100) NOT NULL,
	[publisher_id] [uniqueidentifier] NOT NULL,
	[portlet_status] [tinyint] NOT NULL,
	[created] [datetime] NOT NULL,
	[updated] [datetime] NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[portlet_html] [ntext] NOT NULL,
	[portlet_css] [ntext] NOT NULL,
	[editor_type] [bit] NOT NULL,
	[portlet_header] [ntext] NOT NULL,
	[portlet_footer] [ntext] NOT NULL,
	[group_id] [int] NOT NULL,
	[structure_description] [nvarchar](2000) NOT NULL,
	[content_editor_type] [char](1) NOT NULL,
	[enable_shortcut] [char](1) NOT NULL,
 CONSTRAINT [PK_cms_portlets] PRIMARY KEY CLUSTERED 
(
	[portlet_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_publisher_log_events]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_publisher_log_events](
	[event_id] [int] IDENTITY(1,1) NOT NULL,
	[event_name] [nvarchar](50) NOT NULL,
	[event_description] [nvarchar](200) NOT NULL,
	[event_type] [tinyint] NOT NULL,
	[created] [datetime] NOT NULL,
 CONSTRAINT [PK_cms_publisher_log_events] PRIMARY KEY CLUSTERED 
(
	[event_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cms_publisher_logs]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_publisher_logs](
	[log_id] [int] IDENTITY(1,1) NOT NULL,
	[publisher_id] [uniqueidentifier] NOT NULL,
	[event_id] [int] NOT NULL,
	[note_id] [bigint] NOT NULL,
	[title] [nvarchar](100) NOT NULL,
	[note] [nvarchar](500) NOT NULL,
	[ip] [varchar](15) NOT NULL,
	[created] [datetime] NOT NULL,
 CONSTRAINT [PK_publisher_log] PRIMARY KEY CLUSTERED 
(
	[log_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_redirects]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_redirects](
	[redirect_id] [int] IDENTITY(1,1) NOT NULL,
	[redirect_alias] [nvarchar](100) NOT NULL,
	[article_id] [int] NOT NULL,
	[zone_id] [int] NOT NULL,
	[created] [datetime] NOT NULL,
	[publisher_id] [uniqueidentifier] NOT NULL,
	[updated] [datetime] NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[group_id] [int] NOT NULL,
	[structure_description] [nvarchar](2000) NOT NULL,
	[permanent_redirection] [bit] NULL,
 CONSTRAINT [PK_cms_redirects] PRIMARY KEY CLUSTERED 
(
	[redirect_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cms_relations]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_relations](
	[rel_id] [bigint] IDENTITY(1,1) NOT NULL,
	[rel_type] [tinyint] NOT NULL,
	[article_id] [int] NOT NULL,
	[classification_id] [int] NOT NULL,
	[publisher_id] [uniqueidentifier] NOT NULL,
	[created] [datetime] NOT NULL,
 CONSTRAINT [PK_cms_zone_article_relations] PRIMARY KEY CLUSTERED 
(
	[rel_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cms_rss_channels]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_rss_channels](
	[channel_id] [int] IDENTITY(1,1) NOT NULL,
	[channel_status] [char](1) NOT NULL,
	[channel_name] [nvarchar](100) NOT NULL,
	[url] [nvarchar](300) NOT NULL,
	[description] [nvarchar](300) NOT NULL,
	[lang_id] [varchar](5) NOT NULL,
	[managing_editor] [nvarchar](100) NOT NULL,
	[copyright] [nvarchar](100) NOT NULL,
	[created] [datetime] NOT NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[updated] [datetime] NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[group_id] [int] NOT NULL,
	[structure_description] [nvarchar](2000) NOT NULL,
	[summary_content_field] [varchar](50) NOT NULL,
	[content_template] [ntext] NOT NULL,
	[content_template_editor_type] [char](1) NOT NULL,
	[singularize_articles] [char](1) NOT NULL,
 CONSTRAINT [PK_cms_rss_channels] PRIMARY KEY CLUSTERED 
(
	[channel_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_rss_content]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_rss_content](
	[rss_zone_id] [int] IDENTITY(1,1) NOT NULL,
	[channel_id] [int] NOT NULL,
	[sgz_id] [int] NOT NULL,
	[sgz_type] [char](1) NOT NULL,
	[sgz_exclude] [char](1) NOT NULL,
	[created] [datetime] NOT NULL,
	[created_by] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_cms_rss_zones] PRIMARY KEY CLUSTERED 
(
	[rss_zone_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_search_log]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_search_log](
	[sid] [bigint] IDENTITY(1,1) NOT NULL,
	[created] [datetime] NOT NULL,
	[server_ip] [varchar](15) NOT NULL,
	[client_ip] [varchar](15) NOT NULL,
	[search_query] [nvarchar](100) NOT NULL,
	[search_in] [nvarchar](100) NOT NULL,
	[result_count] [int] NOT NULL,
 CONSTRAINT [PK_cms_search_log] PRIMARY KEY CLUSTERED 
(
	[sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_sitemaps]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_sitemaps](
	[smap_id] [int] IDENTITY(1,1) NOT NULL,
	[domain_id] [int] NOT NULL,
	[domain_alias] [varchar](255) NOT NULL,
	[status] [tinyint] NOT NULL,
	[last_update] [datetime] NULL,
	[last_generate] [datetime] NULL,
	[last_generate_start] [datetime] NULL,
	[notify_google] [char](1) NOT NULL,
	[notify_msn] [char](1) NOT NULL,
	[notify_ask] [char](1) NOT NULL,
	[notify_yahoo] [char](1) NOT NULL,
	[yahoo_id] [varchar](50) NOT NULL,
	[included_sites] [text] NOT NULL,
	[excluded_zonegroups] [text] NOT NULL,
	[excluded_zones] [text] NOT NULL,
	[excluded_articles] [text] NOT NULL,
	[afiles] [char](1) NOT NULL,
	[interval] [int] NOT NULL,
	[enabled] [char](1) NOT NULL,
	[xml] [ntext] NOT NULL,
	[gz] [image] NULL,
	[created] [datetime] NOT NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[updated] [datetime] NULL,
	[updated_by] [uniqueidentifier] NULL,
	[gzip_enabled] [char](1) NOT NULL,
 CONSTRAINT [PK_cms_sitemaps] PRIMARY KEY CLUSTERED 
(
	[smap_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_sites]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_sites](
	[site_id] [int] IDENTITY(1,1) NOT NULL,
	[site_name] [nvarchar](100) NOT NULL,
	[css_id] [int] NOT NULL,
	[css_id_mobile] [int] NOT NULL,
	[css_id_print] [int] NOT NULL,
	[template_id] [int] NOT NULL,
	[template_id_mobile] [int] NOT NULL,
	[publisher_id] [uniqueidentifier] NOT NULL,
	[site_keywords] [nvarchar](500) NOT NULL,
	[site_header] [ntext] NOT NULL,
	[site_js] [ntext] NOT NULL,
	[custom_body] [nvarchar](200) NOT NULL,
	[site_icon] [nvarchar](100) NOT NULL,
	[created] [datetime] NOT NULL,
	[updated] [datetime] NOT NULL,
	[article_1] [ntext] NOT NULL,
	[article_2] [ntext] NOT NULL,
	[article_3] [ntext] NOT NULL,
	[article_4] [ntext] NOT NULL,
	[article_5] [ntext] NOT NULL,
	[analytics] [ntext] NOT NULL,
	[tag_detail_article] [varchar](50) NOT NULL,
	[group_id] [int] NULL,
	[structure_description] [nvarchar](2000) NOT NULL,
	[meta_description] [nvarchar](2000) NOT NULL,
	[content_1_editor_type] [char](1) NOT NULL,
	[content_2_editor_type] [char](1) NOT NULL,
	[content_3_editor_type] [char](1) NOT NULL,
	[content_4_editor_type] [char](1) NOT NULL,
	[content_5_editor_type] [char](1) NOT NULL,
	[default_article] [varchar](20) NOT NULL,
	[omniture_code] [ntext] NOT NULL,
	[domain_id] [int] NULL,
	[site_alias] [nvarchar](300) NOT NULL,
 CONSTRAINT [PK_cms_sites] PRIMARY KEY CLUSTERED 
(
	[site_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_stf_emails]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_stf_emails](
	[stf_id] [bigint] IDENTITY(1,1) NOT NULL,
	[from_name] [nvarchar](100) NOT NULL,
	[from_email] [varchar](100) NOT NULL,
	[from_ip] [varchar](15) NOT NULL,
	[to_name] [nvarchar](100) NOT NULL,
	[to_email] [varchar](100) NOT NULL,
	[to_note] [nvarchar](500) NOT NULL,
	[stft_id] [int] NOT NULL,
	[zone_id] [int] NOT NULL,
	[article_id] [int] NOT NULL,
	[created] [datetime] NOT NULL,
 CONSTRAINT [PK_cms_stf_emails] PRIMARY KEY CLUSTERED 
(
	[stf_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_stf_templates]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_stf_templates](
	[stft_id] [int] IDENTITY(1,1) NOT NULL,
	[stft_status] [char](1) NOT NULL,
	[stft_name] [nvarchar](100) NOT NULL,
	[stft_form_html] [ntext] NOT NULL,
	[stft_thanks] [nvarchar](200) NOT NULL,
	[stft_mail_html] [ntext] NOT NULL,
	[stft_mail_from_name] [nvarchar](100) NOT NULL,
	[stft_mail_subject] [nvarchar](100) NOT NULL,
	[stft_wh] [varchar](20) NOT NULL,
	[created] [datetime] NOT NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[updated] [datetime] NOT NULL,
	[updated_by] [uniqueidentifier] NOT NULL,
	[omniture_function] [varchar](500) NOT NULL,
 CONSTRAINT [PK_cms_send_to_friend_templates] PRIMARY KEY CLUSTERED 
(
	[stft_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_structure_groups]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_structure_groups](
	[group_id] [int] IDENTITY(1,1) NOT NULL,
	[group_type] [tinyint] NOT NULL,
	[group_name] [nvarchar](50) NOT NULL,
	[created] [datetime] NOT NULL,
	[created_by] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_cms_structure_groups] PRIMARY KEY CLUSTERED 
(
	[group_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cms_template_revisions]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_template_revisions](
	[history_id] [int] IDENTITY(1,1) NOT NULL,
	[template_id] [int] NOT NULL,
	[template_html] [ntext] NOT NULL,
	[publisher_id] [uniqueidentifier] NOT NULL,
	[created] [datetime] NOT NULL,
	[template_type] [tinyint] NOT NULL,
	[content_1_editor_type] [char](1) NOT NULL,
	[template_doctype] [varchar](1000) NOT NULL,
 CONSTRAINT [PK_cms_template_revisions] PRIMARY KEY CLUSTERED 
(
	[history_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_templates]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_templates](
	[template_id] [int] IDENTITY(1,1) NOT NULL,
	[template_status] [char](1) NOT NULL,
	[template_type] [tinyint] NOT NULL,
	[template_name] [nvarchar](100) NOT NULL,
	[template_html] [ntext] NOT NULL,
	[publisher_id] [uniqueidentifier] NOT NULL,
	[created] [datetime] NOT NULL,
	[updated] [datetime] NOT NULL,
	[group_id] [int] NOT NULL,
	[structure_description] [nvarchar](2000) NOT NULL,
	[content_1_editor_type] [char](1) NOT NULL,
	[template_doctype] [varchar](1000) NOT NULL,
 CONSTRAINT [PK_template] PRIMARY KEY CLUSTERED 
(
	[template_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_tmp]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_tmp](
	[tmp_id] [int] NOT NULL,
	[tmp] [ntext] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cms_url_structure]    Script Date: 21.10.2015 11:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_url_structure](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DomainId] [int] NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[StructureTypeId] [int] NOT NULL,
	[Structure] [nvarchar](500) NOT NULL,
	[Prefix] [nvarchar](100) NOT NULL,
	[IsProtect] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[UpdatedBy] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_cms_url_structure] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cms_widget_configs]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_widget_configs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[WidgetUserId] [int] NOT NULL,
	[ParamKey] [nvarchar](max) NOT NULL,
	[ParamValue] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_cms_widget_configs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cms_widget_users]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_widget_users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[WidgetID] [int] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_cms_widget_users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cms_xml]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_xml](
	[xml_id] [int] IDENTITY(1,1) NOT NULL,
	[xml_name] [nvarchar](100) NOT NULL,
	[xml_main_node] [nvarchar](50) NOT NULL,
	[xml_main_node_attrib] [nvarchar](200) NOT NULL,
	[xml_per_node] [nvarchar](50) NOT NULL,
	[xml_per_node_attrib] [nvarchar](200) NOT NULL,
	[xml_sub_node] [nvarchar](200) NOT NULL,
	[xml_sub_template] [int] NOT NULL,
	[xml_level] [int] NOT NULL,
	[xml_related_line] [nvarchar](200) NOT NULL,
	[xml_xml] [text] NOT NULL,
	[created] [datetime] NOT NULL,
	[created_by] [uniqueidentifier] NOT NULL,
	[group_id] [int] NOT NULL,
	[structure_description] [nvarchar](2000) NOT NULL,
 CONSTRAINT [PK_cms_xml] PRIMARY KEY CLUSTERED 
(
	[xml_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cms_zone_groups]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_zone_groups](
	[zone_group_id] [int] IDENTITY(1,1) NOT NULL,
	[zone_group_name] [nvarchar](100) NOT NULL,
	[zone_group_keywords] [nvarchar](500) NOT NULL,
	[site_id] [int] NOT NULL,
	[css_merge] [int] NOT NULL,
	[css_id] [int] NOT NULL,
	[css_id_mobile] [int] NOT NULL,
	[css_id_print] [int] NOT NULL,
	[template_id] [int] NOT NULL,
	[template_id_mobile] [int] NOT NULL,
	[custom_body] [nvarchar](200) NOT NULL,
	[publisher_id] [uniqueidentifier] NOT NULL,
	[created] [datetime] NOT NULL,
	[updated] [datetime] NOT NULL,
	[article_1] [ntext] NOT NULL,
	[article_2] [ntext] NOT NULL,
	[article_3] [ntext] NOT NULL,
	[article_4] [ntext] NOT NULL,
	[article_5] [ntext] NOT NULL,
	[append_1] [tinyint] NOT NULL,
	[append_2] [tinyint] NOT NULL,
	[append_3] [tinyint] NOT NULL,
	[append_4] [tinyint] NOT NULL,
	[append_5] [tinyint] NOT NULL,
	[analytics] [nvarchar](500) NOT NULL,
	[tag_detail_article] [varchar](50) NOT NULL,
	[meta_description] [nvarchar](2000) NOT NULL,
	[zone_group_name_display] [nvarchar](200) NOT NULL,
	[content_1_editor_type] [char](1) NOT NULL,
	[content_2_editor_type] [char](1) NOT NULL,
	[content_3_editor_type] [char](1) NOT NULL,
	[content_4_editor_type] [char](1) NOT NULL,
	[content_5_editor_type] [char](1) NOT NULL,
	[default_article] [varchar](20) NOT NULL,
	[omniture_code] [ntext] NOT NULL,
	[before_head] [nvarchar](max) NOT NULL,
	[before_body] [nvarchar](max) NOT NULL,
	[zg_alias] [nvarchar](300) NOT NULL,
 CONSTRAINT [PK_cms_zone_groups] PRIMARY KEY CLUSTERED 
(
	[zone_group_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_zone_revision]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_zone_revision](
	[rev_id] [bigint] IDENTITY(1,1) NOT NULL,
	[created] [datetime] NOT NULL,
	[created_by] [uniqueidentifier] NULL,
	[rev_date] [datetime] NOT NULL,
	[revision_status] [char](1) NOT NULL,
	[revised_by] [uniqueidentifier] NOT NULL,
	[rev_name] [nvarchar](100) NOT NULL,
	[rev_note] [ntext] NOT NULL,
	[approval_date] [datetime] NULL,
	[approval_id] [uniqueidentifier] NULL,
	[zone_id] [int] NOT NULL,
	[zone_group_id] [int] NOT NULL,
	[zone_type_id] [int] NOT NULL,
	[zone_status] [char](1) NOT NULL,
	[zone_name] [nvarchar](100) NOT NULL,
	[zone_desc] [nvarchar](200) NOT NULL,
	[css_merge] [int] NOT NULL,
	[css_id] [int] NOT NULL,
	[css_id_mobile] [int] NOT NULL,
	[css_id_print] [int] NOT NULL,
	[template_id] [int] NOT NULL,
	[template_id_mobile] [int] NOT NULL,
	[custom_body] [nvarchar](200) NOT NULL,
	[zone_keywords] [nvarchar](500) NOT NULL,
	[article_1] [ntext] NOT NULL,
	[article_2] [ntext] NOT NULL,
	[article_3] [ntext] NOT NULL,
	[article_4] [ntext] NOT NULL,
	[article_5] [ntext] NOT NULL,
	[append_1] [tinyint] NOT NULL,
	[append_2] [tinyint] NOT NULL,
	[append_3] [tinyint] NOT NULL,
	[append_4] [tinyint] NOT NULL,
	[append_5] [tinyint] NOT NULL,
	[analytics] [nvarchar](500) NOT NULL,
	[meta_description] [nvarchar](2000) NOT NULL,
	[zone_name_display] [nvarchar](200) NOT NULL,
	[content_1_editor_type] [char](1) NOT NULL,
	[content_2_editor_type] [char](1) NOT NULL,
	[content_3_editor_type] [char](1) NOT NULL,
	[content_4_editor_type] [char](1) NOT NULL,
	[content_5_editor_type] [char](1) NOT NULL,
	[default_article] [varchar](20) NOT NULL,
	[omniture_code] [ntext] NOT NULL,
	[lang_id] [char](2) NOT NULL,
	[before_head] [nvarchar](max) NOT NULL,
	[before_body] [nvarchar](max) NOT NULL,
	[zone_alias] [nvarchar](300) NOT NULL,
 CONSTRAINT [PK_cms_zone_revision] PRIMARY KEY CLUSTERED 
(
	[rev_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cms_zones]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cms_zones](
	[zone_id] [int] IDENTITY(1,1) NOT NULL,
	[zone_group_id] [int] NOT NULL,
	[zone_type_id] [int] NOT NULL,
	[zone_status] [char](1) NOT NULL,
	[zone_name] [nvarchar](100) NOT NULL,
	[zone_desc] [nvarchar](200) NOT NULL,
	[css_merge] [int] NOT NULL,
	[css_id] [int] NOT NULL,
	[css_id_mobile] [int] NOT NULL,
	[css_id_print] [int] NOT NULL,
	[template_id] [int] NOT NULL,
	[template_id_mobile] [int] NOT NULL,
	[custom_body] [nvarchar](200) NOT NULL,
	[zone_keywords] [nvarchar](500) NOT NULL,
	[article_1] [ntext] NOT NULL,
	[article_2] [ntext] NOT NULL,
	[article_3] [ntext] NOT NULL,
	[article_4] [ntext] NOT NULL,
	[article_5] [ntext] NOT NULL,
	[append_1] [tinyint] NOT NULL,
	[append_2] [tinyint] NOT NULL,
	[append_3] [tinyint] NOT NULL,
	[append_4] [tinyint] NOT NULL,
	[append_5] [tinyint] NOT NULL,
	[publisher_id] [uniqueidentifier] NOT NULL,
	[created] [datetime] NOT NULL,
	[updated] [datetime] NOT NULL,
	[analytics] [nvarchar](500) NOT NULL,
	[meta_description] [nvarchar](2000) NOT NULL,
	[zone_name_display] [nvarchar](200) NOT NULL,
	[locked] [datetime] NULL,
	[locked_by] [uniqueidentifier] NULL,
	[default_article] [varchar](20) NOT NULL,
	[omniture_code] [ntext] NOT NULL,
	[lang_id] [char](2) NOT NULL,
	[before_head] [nvarchar](max) NOT NULL,
	[before_body] [nvarchar](max) NOT NULL,
	[zone_alias] [nvarchar](300) NOT NULL,
 CONSTRAINT [PK_cms_zones] PRIMARY KEY CLUSTERED 
(
	[zone_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[vArticleFilesApprovalDates]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[vArticleFilesApprovalDates]
as

select article_id, MAX(approval_date) as approval_date
from dbo.cms_article_files_revision  with (nolock)
where revision_status = 'L'
group by article_id







GO
/****** Object:  View [dbo].[vArticleFilesLiveRevisions]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[vArticleFilesLiveRevisions]
as

select
	r.rev_id, r.created, r.created_by, r.rev_date, r.revision_status, r.revised_by, r.approval_date, r.approval_id, r.article_id
from dbo.vArticleFilesApprovalDates aad with (nolock)
	LEFT JOIN dbo.cms_article_files_revision r with (nolock)
	ON r.article_id = aad.article_id and r.approval_date = aad.approval_date







GO
/****** Object:  View [dbo].[vArticlesApprovalDates]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[vArticlesApprovalDates]
as

select article_id, MAX(approval_date) as approval_date
from dbo.cms_article_revision  with (nolock)
where revision_status = 'L'
group by article_id







GO
/****** Object:  View [dbo].[vArticlesLiveRevisions]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[vArticlesLiveRevisions]
as  select  r.rev_id,
            r.created,
            r.created_by,
            r.rev_date,
            r.revision_status,
            r.revised_by,
            r.rev_name,
            r.rev_note,
            rev_flag_1,
            rev_flag_2,
            rev_flag_3,
            rev_flag_4,
            rev_flag_5,
            r.approval_date,
            r.approval_id,
            r.article_id,
            r.clsf_id,
            r.status,
            r.startdate,
            r.enddate,
            r.orderno,
            r.lang_id,
            r.navigation_display,
            r.navigation_zone_id,
            r.menu_text,
            r.headline,
            r.summary,
            r.keywords,
            r.article_type,
            r.article_type_detail,
            r.article_1,
            r.article_2,
            r.article_3,
            r.article_4,
            r.article_5,
            r.custom_1,
            r.custom_2,
            r.custom_3,
            r.custom_4,
            r.custom_5,
            r.custom_6,
            r.custom_7,
            r.custom_8,
            r.custom_9,
            r.custom_10,
            r.custom_11,
            r.custom_12,
            r.custom_13,
            r.custom_14,
            r.custom_15,
            r.custom_16,
            r.custom_17,
            r.custom_18,
            r.custom_19,
            r.custom_20,
            r.flag_1,
            r.flag_2,
            r.flag_3,
            r.flag_4,
            r.flag_5,
            r.date_1,
            r.date_2,
            r.date_3,
            r.date_4,
            r.date_5,
            r.cl_1,
            r.cl_2,
            r.cl_3,
            r.cl_4,
            r.cl_5,
            r.custom_body
    from    dbo.vArticlesApprovalDates aad with ( nolock )
            LEFT JOIN dbo.cms_article_revision r with ( nolock ) ON r.article_id = aad.article_id
                                                                    and r.approval_date = aad.approval_date







GO
/****** Object:  UserDefinedFunction [dbo].[Split]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[Split] 
(
	@StringToSplit varchar(2048),
	@Separator varchar(128))
RETURNS TABLE AS RETURN WITH indices AS
(
	SELECT 0 S, 1 E UNION all
	SELECT E, charindex(@Separator, @StringToSplit, E) + len(@Separator)
	FROM indices
	WHERE E > S
)
	SELECT 
		SUBSTRING(@StringToSplit,S, CASE WHEN E > len(@Separator) THEN 
		e-s-len(@Separator) ELSE len(@StringToSplit) - s + 1 END) String ,S StartIndex        
		FROM indices 
	WHERE S >0







GO
/****** Object:  View [dbo].[vArticlesRevisionsZonesFull]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vArticlesRevisionsZonesFull]
AS  SELECT TOP 100 PERCENT
            az.rev_id,
            az.az_order,
            z.zone_type_id,
            a.article_id,
            a.clsf_id,
            a.status,
            a.created,
            a.rev_date as updated,
            a.startdate,
            a.enddate,
            a.revised_by as publisher_id,
            '0' as clicks,
            a.orderno,
            a.lang_id,
            a.navigation_display,
            a.navigation_zone_id,
            a.menu_text,
            a.headline,
            a.summary,
            a.keywords,
            a.article_type,
            a.article_type_detail,
            a.article_1,
            a.article_2,
            a.article_3,
            a.article_4,
            a.article_5,
            a.custom_1,
            a.custom_2,
            a.custom_3,
            a.custom_4,
            a.custom_5,
            a.custom_6,
            a.custom_7,
            a.custom_8,
            a.custom_9,
            a.custom_10,
            a.custom_11,
            a.custom_12,
            a.custom_13,
            a.custom_14,
            a.custom_15,
            a.custom_16,
            a.custom_17,
            a.custom_18,
            a.custom_19,
            a.custom_20,
            a.flag_1,
            a.flag_2,
            a.flag_3,
            a.flag_4,
            a.flag_5,
            a.date_1,
            a.date_2,
            a.date_3,
            a.date_4,
            a.date_5,
            a.cl_1,
            a.cl_2,
            a.cl_3,
            a.cl_4,
            a.cl_5,
            a.custom_body as a_custom_body,
            z.zone_id,
            z.zone_group_id,
            z.zone_status,
            z.zone_name,
            z.zone_desc,
            z.css_merge as zone_css_merge,
            z.css_id as zone_css_id,
            z.css_id_mobile as zone_css_id_mobile,
            z.css_id_print as zone_css_id_print,
            z.template_id as zone_template_id,
            z.template_id_mobile as zone_template_id_mobile,
            z.zone_keywords,
            z.article_1 as zone_article_1,
            z.article_2 as zone_article_2,
            z.article_3 as zone_article_3,
            z.article_4 as zone_article_4,
            z.article_5 as zone_article_5,
            z.append_1,
            z.append_2,
            z.append_3,
            z.append_4,
            z.append_5,
            z.publisher_id as zone_publisher_id,
            z.created as zone_created,
            z.updated as zone_updated,
            z.custom_body as zone_custom_body,
            zg.zone_group_name,
            zg.zone_group_keywords,
            zg.site_id,
            zg.css_merge as zg_css_merge,
            zg.css_id as zg_css_id,
            zg.css_id_mobile as zg_css_id_mobile,
            zg.css_id_print as zg_css_id_print,
            zg.template_id as zg_template_id,
            zg.template_id_mobile as zg_template_id_mobile,
            zg.publisher_id as zg_publisher_id,
            zg.created as zg_created,
            zg.updated as zg_updated,
            zg.article_1 as zg_article_1,
            zg.article_2 as zg_article_2,
            zg.article_3 as zg_article_3,
            zg.article_4 as zg_article_4,
            zg.article_5 as zg_article_5,
            zg.append_1 as zg_append_1,
            zg.append_2 as zg_append_2,
            zg.append_3 as zg_append_3,
            zg.append_4 as zg_append_4,
            zg.append_5 as zg_append_5,
            zg.custom_body as zg_custom_body,
            s.site_name,
            s.css_id as site_css_id,
            s.css_id_mobile as site_css_id_mobile,
            s.css_id_print as site_css_id_print,
            s.template_id as site_template_id,
            s.template_id_mobile as site_template_id_mobile,
            s.publisher_id as site_publisher_id,
            s.site_keywords,
            s.site_header,
            s.site_js,
            s.site_icon,
            s.created as site_created,
            s.updated as site_updated,
            s.article_1 as s_article_1,
            s.article_2 as s_article_2,
            s.article_3 as s_article_3,
            s.article_4 as s_article_4,
            s.article_5 as s_article_5,
            s.custom_body as s_custom_body,
            s.analytics as site_analytics,
            zg.analytics as zg_analytics,
            z.analytics as zone_analytics,
            '0' as rating,
            '0' as ratingcount,
            a.meta_description as meta_description,
            z.meta_description as zone_meta_description,
            zg.meta_description as zone_group_meta_description,
            s.meta_description as site_meta_description,
            zg.zone_group_name_display,
            z.zone_name_display,
            az.az_alias,
            a.content_1_editor_type,
            a.content_2_editor_type,
            a.content_3_editor_type,
            a.content_4_editor_type,
            a.content_5_editor_type,
            a.omniture_code as article_omniture_code,
            z.omniture_code as zone_omniture_code,
            zg.omniture_code as zone_group_omniture_code,
            s.omniture_code as site_omniture_code,
            s.default_article as site_default_article,
            zg.default_article zone_group_default_article,
            z.default_article as zone_default_article,
            a.custom_setting
    FROM    dbo.cms_zones z with ( nolock )
            INNER JOIN dbo.cms_article_revision a with ( nolock )
            INNER JOIN dbo.cms_article_zones_revision az with ( nolock ) ON a.rev_id = az.rev_id
                                                                            and a.article_id = az.article_id ON z.zone_id = az.zone_id
            LEFT JOIN dbo.cms_zone_groups zg with ( nolock ) on zg.zone_group_id = z.zone_group_id
            LEFT JOIN dbo.cms_sites s with ( nolock ) on s.site_id = zg.site_id







GO
/****** Object:  View [dbo].[vArticlesZones]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vArticlesZones]
AS
SELECT TOP 100 PERCENT
            az.az_order,
            z.zone_type_id,
            a.article_id,
            z.zone_id,
            s.site_id,
            a.headline,
            z.zone_name,
            zg.zone_group_name,
            s.site_name,
            a.clsf_id,
            a.status,
            a.created,
            a.updated,
            a.startdate,
            a.enddate,
            a.publisher_id,
            a.clicks,
            a.orderno,
            a.lang_id,
            a.summary,
            a.keywords,
            a.article_type,
            a.article_type_detail,
            a.article_1,
            a.article_2,
            a.article_3,
            a.article_4,
            a.article_5,
            a.custom_1,
            a.custom_2,
            a.custom_3,
            a.custom_4,
            a.custom_5,
            a.custom_6,
            a.custom_7,
            a.custom_8,
            a.custom_9,
            a.custom_10,
            a.custom_11,
            a.custom_12,
            a.custom_13,
            a.custom_14,
            a.custom_15,
            a.custom_16,
            a.custom_17,
            a.custom_18,
            a.custom_19,
            a.custom_20,
            a.flag_1,
            a.flag_2,
            a.flag_3,
            a.flag_4,
            a.flag_5,
            a.date_1,
            a.date_2,
            a.date_3,
            a.date_4,
            a.date_5,
            a.menu_text,
            a.navigation_display,
            a.navigation_zone_id,
            a.cl_1,
            a.cl_2,
            a.cl_3,
            a.cl_4,
            a.cl_5,
            z.zone_group_id,
            z.zone_status,
            a.rating,
            a.ratingcount,
            a.meta_description as meta_description,
            z.meta_description as zone_meta_description,
            zg.meta_description as zone_group_meta_description,
            s.meta_description as site_meta_description,
            zg.zone_group_name_display,
            z.zone_name_display,
            az.az_alias,
            a.omniture_code as article_omniture_code,
            z.omniture_code as zone_omniture_code,
            zg.omniture_code as zone_group_omniture_code,
            s.omniture_code as site_omniture_code,
            s.default_article as site_default_article,
            zg.default_article zone_group_default_article,
            z.default_article as zone_default_article,
            a.custom_setting,
			domain_name = 
             case 
				when charindex(char(10), d.domain_names) > 0 then substring(d.domain_names, 1, charindex(char(10), d.domain_names))  
				else d.domain_names
			 end
    FROM    dbo.cms_zones z with ( nolock )
            INNER JOIN dbo.cms_articles a with ( nolock )
            INNER JOIN dbo.cms_article_zones az with ( nolock ) ON a.article_id = az.article_id ON z.zone_id = az.zone_id
            LEFT JOIN dbo.cms_zone_groups zg with ( nolock ) on zg.zone_group_id = z.zone_group_id
            LEFT JOIN dbo.cms_sites s with ( nolock ) on s.site_id = zg.site_id
			left join dbo.cms_domains d with(nolock) on s.domain_id = d.domain_id







GO
/****** Object:  View [dbo].[vArticlesZonesFull]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Add before_head,before_body to cms_zonegroups
CREATE VIEW [dbo].[vArticlesZonesFull]
AS
SELECT     TOP (100) PERCENT az.az_order,
                          (SELECT     lang_alias
                            FROM          dbo.cms_languages AS ln
                            WHERE      (lang_id = a.lang_id)) AS lang_alias, z.zone_alias, z.zone_type_id, a.article_id, a.clsf_id, a.status, a.created, a.updated, a.startdate, a.enddate, 
                      a.publisher_id, a.clicks, a.orderno, a.lang_id, a.navigation_display, a.navigation_zone_id, a.menu_text, a.headline, a.summary, a.keywords, a.article_type, 
                      a.article_type_detail, a.article_1, a.article_2, a.article_3, a.article_4, a.article_5, a.custom_1, a.custom_2, a.custom_3, a.custom_4, a.custom_5, a.custom_6, 
                      a.custom_7, a.custom_8, a.custom_9, a.custom_10, a.custom_11, a.custom_12, a.custom_13, a.custom_14, a.custom_15, a.custom_16, a.custom_17, a.custom_18, 
                      a.custom_19, a.custom_20, a.flag_1, a.flag_2, a.flag_3, a.flag_4, a.flag_5, a.date_1, a.date_2, a.date_3, a.date_4, a.date_5, a.cl_1, a.cl_2, a.cl_3, a.cl_4, a.cl_5, 
                      a.custom_body AS a_custom_body, a.before_head AS a_before_head, a.before_body AS a_before_body, a.no_index_no_follow, a.custom_html_attr, a.meta_title, 
                      a.canonical_url, z.zone_id, z.zone_group_id, z.zone_status, z.zone_name, z.zone_desc, z.css_merge AS zone_css_merge, z.css_id AS zone_css_id, 
                      z.css_id_mobile AS zone_css_id_mobile, z.css_id_print AS zone_css_id_print, z.template_id AS zone_template_id, 
                      z.template_id_mobile AS zone_template_id_mobile, z.zone_keywords, z.article_1 AS zone_article_1, z.article_2 AS zone_article_2, z.article_3 AS zone_article_3, 
                      z.article_4 AS zone_article_4, z.article_5 AS zone_article_5, z.append_1, z.append_2, z.append_3, z.append_4, z.append_5, z.publisher_id AS zone_publisher_id, 
                      z.created AS zone_created, z.updated AS zone_updated, z.custom_body AS zone_custom_body, z.before_head AS zone_before_head, 
                      z.before_body AS zone_before_body, zg.zone_group_name, zg.zone_group_keywords, zg.site_id, zg.css_merge AS zg_css_merge, zg.css_id AS zg_css_id, 
                      zg.css_id_mobile AS zg_css_id_mobile, zg.css_id_print AS zg_css_id_print, zg.template_id AS zg_template_id, zg.template_id_mobile AS zg_template_id_mobile, 
                      zg.publisher_id AS zg_publisher_id, zg.created AS zg_created, zg.updated AS zg_updated, zg.article_1 AS zg_article_1, zg.article_2 AS zg_article_2, 
                      zg.article_3 AS zg_article_3, zg.article_4 AS zg_article_4, zg.article_5 AS zg_article_5, zg.append_1 AS zg_append_1, zg.append_2 AS zg_append_2, 
                      zg.append_3 AS zg_append_3, zg.append_4 AS zg_append_4, zg.append_5 AS zg_append_5, zg.custom_body AS zg_custom_body, 
                      zg.before_head AS zg_before_head, zg.before_body AS zg_before_body, s.site_name, s.css_id AS site_css_id, s.css_id_mobile AS site_css_id_mobile, 
                      s.css_id_print AS site_css_id_print, s.template_id AS site_template_id, s.template_id_mobile AS site_template_id_mobile, s.publisher_id AS site_publisher_id, 
                      s.site_keywords, s.site_header, s.site_js, s.site_icon, s.created AS site_created, s.updated AS site_updated, s.article_1 AS s_article_1, s.article_2 AS s_article_2, 
                      s.article_3 AS s_article_3, s.article_4 AS s_article_4, s.article_5 AS s_article_5, s.custom_body AS s_custom_body, s.analytics AS site_analytics, 
                      zg.analytics AS zg_analytics, z.analytics AS zone_analytics, a.rating, a.ratingcount, a.meta_description, z.meta_description AS zone_meta_description, 
                      zg.meta_description AS zone_group_meta_description, s.meta_description AS site_meta_description, zg.zone_group_name_display, z.zone_name_display, 
                      az.az_alias, a.omniture_code AS article_omniture_code, z.omniture_code AS zone_omniture_code, zg.omniture_code AS zone_group_omniture_code, 
                      s.omniture_code AS site_omniture_code, s.default_article AS site_default_article, zg.default_article AS zone_group_default_article, 
                      z.default_article AS zone_default_article, a.custom_setting, s.site_alias, zg.zg_alias, az.is_alias_protected, CASE WHEN charindex(char(10), d .domain_names) 
                      > 0 THEN substring(d .domain_names, 1, charindex(char(10), d .domain_names)) ELSE d .domain_names END AS domain_name
FROM         dbo.cms_zones AS z WITH (nolock) INNER JOIN
                      dbo.cms_articles AS a WITH (nolock) INNER JOIN
                      dbo.cms_article_zones AS az WITH (nolock) ON a.article_id = az.article_id ON z.zone_id = az.zone_id LEFT OUTER JOIN
                      dbo.cms_zone_groups AS zg WITH (nolock) ON zg.zone_group_id = z.zone_group_id LEFT OUTER JOIN
                      dbo.cms_sites AS s WITH (nolock) ON s.site_id = zg.site_id LEFT OUTER JOIN
                      dbo.cms_domains AS d WITH (nolock) ON s.domain_id = d.domain_id 

GO
/****** Object:  View [dbo].[vArticlesZonesNav_1]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vArticlesZonesNav_1]
as

select az.article_id, az.zone_id, a.navigation_zone_id
from dbo.cms_article_zones az with (nolock)
	INNER JOIN dbo.cms_articles a with (nolock)
		on az.article_id = a.article_id
where a.status = 1 and a.navigation_zone_id > 0







GO
/****** Object:  View [dbo].[vw_aspnet_Applications]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

  CREATE VIEW [dbo].[vw_aspnet_Applications]
  AS SELECT [dbo].[aspnet_Applications].[ApplicationName], [dbo].[aspnet_Applications].[LoweredApplicationName], [dbo].[aspnet_Applications].[ApplicationId], [dbo].[aspnet_Applications].[Description]
  FROM [dbo].[aspnet_Applications]
  





GO
/****** Object:  View [dbo].[vw_aspnet_MembershipUsers]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

  CREATE VIEW [dbo].[vw_aspnet_MembershipUsers]
  AS SELECT [dbo].[aspnet_Membership].[UserId],
            [dbo].[aspnet_Membership].[PasswordFormat],
            [dbo].[aspnet_Membership].[MobilePIN],
            [dbo].[aspnet_Membership].[Email],
            [dbo].[aspnet_Membership].[LoweredEmail],
            [dbo].[aspnet_Membership].[PasswordQuestion],
            [dbo].[aspnet_Membership].[PasswordAnswer],
            [dbo].[aspnet_Membership].[IsApproved],
            [dbo].[aspnet_Membership].[IsLockedOut],
            [dbo].[aspnet_Membership].[CreateDate],
            [dbo].[aspnet_Membership].[LastLoginDate],
            [dbo].[aspnet_Membership].[LastPasswordChangedDate],
            [dbo].[aspnet_Membership].[LastLockoutDate],
            [dbo].[aspnet_Membership].[FailedPasswordAttemptCount],
            [dbo].[aspnet_Membership].[FailedPasswordAttemptWindowStart],
            [dbo].[aspnet_Membership].[FailedPasswordAnswerAttemptCount],
            [dbo].[aspnet_Membership].[FailedPasswordAnswerAttemptWindowStart],
            [dbo].[aspnet_Membership].[Comment],
            [dbo].[aspnet_Users].[ApplicationId],
            [dbo].[aspnet_Users].[UserName],
            [dbo].[aspnet_Users].[MobileAlias],
            [dbo].[aspnet_Users].[IsAnonymous],
            [dbo].[aspnet_Users].[LastActivityDate]
  FROM [dbo].[aspnet_Membership] INNER JOIN [dbo].[aspnet_Users]
      ON [dbo].[aspnet_Membership].[UserId] = [dbo].[aspnet_Users].[UserId]
  





GO
/****** Object:  View [dbo].[vw_aspnet_Profiles]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

  CREATE VIEW [dbo].[vw_aspnet_Profiles]
  AS SELECT [dbo].[aspnet_Profile].[UserId], [dbo].[aspnet_Profile].[LastUpdatedDate],
      [DataSize]=  DATALENGTH([dbo].[aspnet_Profile].[PropertyNames])
                 + DATALENGTH([dbo].[aspnet_Profile].[PropertyValuesString])
                 + DATALENGTH([dbo].[aspnet_Profile].[PropertyValuesBinary])
  FROM [dbo].[aspnet_Profile]
  





GO
/****** Object:  View [dbo].[vw_aspnet_Roles]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

  CREATE VIEW [dbo].[vw_aspnet_Roles]
  AS SELECT [dbo].[aspnet_Roles].[ApplicationId], [dbo].[aspnet_Roles].[RoleId], [dbo].[aspnet_Roles].[RoleName], [dbo].[aspnet_Roles].[LoweredRoleName], [dbo].[aspnet_Roles].[Description]
  FROM [dbo].[aspnet_Roles]
  





GO
/****** Object:  View [dbo].[vw_aspnet_Users]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

  CREATE VIEW [dbo].[vw_aspnet_Users]
  AS SELECT [dbo].[aspnet_Users].[ApplicationId], [dbo].[aspnet_Users].[UserId], [dbo].[aspnet_Users].[UserName], [dbo].[aspnet_Users].[LoweredUserName], [dbo].[aspnet_Users].[MobileAlias], [dbo].[aspnet_Users].[IsAnonymous], [dbo].[aspnet_Users].[LastActivityDate]
  FROM [dbo].[aspnet_Users]
  





GO
/****** Object:  View [dbo].[vw_aspnet_UsersInRoles]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

  CREATE VIEW [dbo].[vw_aspnet_UsersInRoles]
  AS SELECT [dbo].[aspnet_UsersInRoles].[UserId], [dbo].[aspnet_UsersInRoles].[RoleId]
  FROM [dbo].[aspnet_UsersInRoles]
  





GO
/****** Object:  View [dbo].[vw_aspnet_WebPartState_Paths]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

  CREATE VIEW [dbo].[vw_aspnet_WebPartState_Paths]
  AS SELECT [dbo].[aspnet_Paths].[ApplicationId], [dbo].[aspnet_Paths].[PathId], [dbo].[aspnet_Paths].[Path], [dbo].[aspnet_Paths].[LoweredPath]
  FROM [dbo].[aspnet_Paths]
  





GO
/****** Object:  View [dbo].[vw_aspnet_WebPartState_Shared]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

  CREATE VIEW [dbo].[vw_aspnet_WebPartState_Shared]
  AS SELECT [dbo].[aspnet_PersonalizationAllUsers].[PathId], [DataSize]=DATALENGTH([dbo].[aspnet_PersonalizationAllUsers].[PageSettings]), [dbo].[aspnet_PersonalizationAllUsers].[LastUpdatedDate]
  FROM [dbo].[aspnet_PersonalizationAllUsers]
  





GO
/****** Object:  View [dbo].[vw_aspnet_WebPartState_User]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

  CREATE VIEW [dbo].[vw_aspnet_WebPartState_User]
  AS SELECT [dbo].[aspnet_PersonalizationPerUser].[PathId], [dbo].[aspnet_PersonalizationPerUser].[UserId], [DataSize]=DATALENGTH([dbo].[aspnet_PersonalizationPerUser].[PageSettings]), [dbo].[aspnet_PersonalizationPerUser].[LastUpdatedDate]
  FROM [dbo].[aspnet_PersonalizationPerUser]
  





GO
/****** Object:  View [dbo].[vw_cms_AccessRules]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_cms_AccessRules]
AS
SELECT ar.RuleId, ar.RuleName, ar.ContentId, ar.ContentType, ar.Roles, ar.Users, ar.Permissions, ar.Created, ar.CreatedBy, 
                  CASE WHEN ar.ContentType = 'site' THEN s.site_name WHEN ar.ContentType = 'zonegroup' THEN zg.zone_group_name WHEN ar.ContentType = 'zone' THEN z.zone_name WHEN
                   ar.ContentType = 'article' THEN a.headline END AS ContentItemName,
				   CASE WHEN ar.ContentType = 'site' THEN 0 WHEN ar.ContentType = 'zonegroup' THEN 1 WHEN ar.ContentType = 'zone' THEN 2 WHEN
                   ar.ContentType = 'article' THEN 3 END AS AccessLevel
FROM     dbo.cms_AccessRules AS ar WITH (NOLOCK) LEFT OUTER JOIN
                  dbo.cms_sites AS s ON s.site_id = ar.ContentId LEFT OUTER JOIN
                  dbo.cms_zone_groups AS zg ON zg.zone_group_id = ar.ContentId LEFT OUTER JOIN
                  dbo.cms_zones AS z ON z.zone_id = ar.ContentId LEFT OUTER JOIN
                  dbo.cms_articles AS a ON a.article_id = ar.ContentId






GO
/****** Object:  View [dbo].[vw_cms_AccessRules_Article]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_cms_AccessRules_Article]
AS
SELECT 
	ar.RuleId, 
	ar.RuleName, 
	ar.ContentId, 
	ar.ContentType, 
	ar.Roles, 
	ar.Users, 
	ar.Permissions, 
	ar.Created, 
	ar.CreatedBy
FROM     dbo.cms_AccessRules AS ar WITH (NOLOCK) LEFT OUTER JOIN
                  dbo.cms_articles AS a ON a.article_id = ar.ContentId
			WHERE ar.ContentType = 'article'





GO
/****** Object:  View [dbo].[vw_cms_AccessRules_Site]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_cms_AccessRules_Site]
AS
SELECT 
	ar.RuleId, 
	ar.RuleName, 
	ar.ContentId, 
	ar.ContentType, 
	ar.Roles, 
	ar.Users, 
	ar.Permissions, 
	ar.Created, 
	ar.CreatedBy
FROM     dbo.cms_AccessRules AS ar WITH (NOLOCK) LEFT OUTER JOIN
                  dbo.cms_articles AS a ON a.article_id = ar.ContentId
			WHERE ar.ContentType = 'site'





GO
/****** Object:  View [dbo].[vw_cms_AccessRules_Zone]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_cms_AccessRules_Zone]
AS
SELECT 
	ar.RuleId, 
	ar.RuleName, 
	ar.ContentId, 
	ar.ContentType, 
	ar.Roles, 
	ar.Users, 
	ar.Permissions, 
	ar.Created, 
	ar.CreatedBy
FROM     dbo.cms_AccessRules AS ar WITH (NOLOCK) LEFT OUTER JOIN
                  dbo.cms_articles AS a ON a.article_id = ar.ContentId
			WHERE ar.ContentType = 'zone'





GO
/****** Object:  View [dbo].[vw_cms_AccessRules_ZoneGroup]    Script Date: 21.10.2015 11:49:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_cms_AccessRules_ZoneGroup]
AS
SELECT ar.RuleId, ar.RuleName, ar.ContentId, ar.ContentType, ar.Roles, ar.Users, ar.Permissions, ar.Created, ar.CreatedBy
FROM     dbo.cms_AccessRules AS ar WITH (NOLOCK) LEFT OUTER JOIN
                  dbo.cms_articles AS a ON a.article_id = ar.ContentId
WHERE  (ar.ContentType = 'zonegroup')
GO
INSERT [dbo].[aspnet_Applications] ([ApplicationName], [LoweredApplicationName], [ApplicationId], [Description]) VALUES (N'cms', N'cms', N'779a2d88-c8f8-45ba-bfae-170f7d6bd695', NULL)
GO
INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'779a2d88-c8f8-45ba-bfae-170f7d6bd695', N'3b303d4e-4639-42f1-b382-a8c71f640443', N'GvAStBZpWSOKhr/aVbd34d6yTTovYMbjQpZscF8FgtSWt31biGcM+O5YTUb/kXG3', 2, N'pOCHCNZg5RtpsiGj5so/Xg==', NULL, N'kerem.demir@madebycat.com', N'kerem.demir@madebycat.com', NULL, NULL, 1, 0, CAST(0x0000A395009E5F5C AS DateTime), CAST(0x0000A3B5007D3666 AS DateTime), CAST(0x0000A39A00D99FB0 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), N'')
GO
INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'779a2d88-c8f8-45ba-bfae-170f7d6bd695', N'00be2157-f095-4eee-b0e6-e7e6ffd3e4b1', N'QfTbL9yFXnUB3m/hhbJTdTChHoFiUEERCkJn8r23KGD8KvN1+VI7wT1Af8/S6s1L', 2, N'swBdX7laNldKTQcclkgLBw==', NULL, N'murat.bandakcioglu@madebycat.com', N'murat.bandakcioglu@madebycat.com', NULL, NULL, 1, 0, CAST(0x0000A395009E8860 AS DateTime), CAST(0x0000A3B800C230ED AS DateTime), CAST(0x0000A395009FE9B8 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), N'')
GO
INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'779a2d88-c8f8-45ba-bfae-170f7d6bd695', N'34e61752-f22a-4b6b-bf4a-b808b50c8bcd', N'eSyQAnVmEMdgPgQTaWMn/Vr3r82VVLn/wySK5skmmFJa6NZOA1CqJgWEVmnIZwAh', 2, N'JOBxaIfzgvUpLH2y6DyNRw==', NULL, N'ufuk.ates@madebycat.com', N'ufuk.ates@madebycat.com', NULL, NULL, 1, 0, CAST(0x0000A64A009EC8D4 AS DateTime), CAST(0x0000A68800CB5716 AS DateTime), CAST(0x0000A64A009EC8D4 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), N'')

GO
INSERT [dbo].[aspnet_Profile] ([UserId], [PropertyNames], [PropertyValuesString], [PropertyValuesBinary], [LastUpdatedDate]) VALUES (N'3b303d4e-4639-42f1-b382-a8c71f640443', N'System.FullName:S:0:11:', N'Kerem Demir', 0x, CAST(0x0000A395009E6009 AS DateTime))
GO
INSERT [dbo].[aspnet_Profile] ([UserId], [PropertyNames], [PropertyValuesString], [PropertyValuesBinary], [LastUpdatedDate]) VALUES (N'00be2157-f095-4eee-b0e6-e7e6ffd3e4b1', N'System.FullName:S:0:18:', N'Murat Bandakçıoğlu', 0x, CAST(0x0000A395009E8963 AS DateTime))
GO
INSERT [dbo].[aspnet_Roles] ([ApplicationId], [RoleId], [RoleName], [LoweredRoleName], [Description]) VALUES (N'779a2d88-c8f8-45ba-bfae-170f7d6bd695', N'5344c95e-085b-4ad2-8956-d6b373ca848d', N'Administrator', N'administrator', NULL)

GO
INSERT [dbo].[aspnet_Roles] ([ApplicationId], [RoleId], [RoleName], [LoweredRoleName], [Description]) VALUES (N'779a2d88-c8f8-45ba-bfae-170f7d6bd695', N'f81beaf6-f6d1-4b1e-8131-db8e239948d8', N'Editor', N'editor', NULL)
GO
INSERT [dbo].[aspnet_Roles] ([ApplicationId], [RoleId], [RoleName], [LoweredRoleName], [Description]) VALUES (N'779a2d88-c8f8-45ba-bfae-170f7d6bd695', N'4afe2e75-4629-4d44-874f-a33896d7372a', N'Author', N'author', NULL)
GO
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'common', N'1', 1)
GO
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'health monitoring', N'1', 1)
GO
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'membership', N'1', 1)
GO
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'personalization', N'1', 1)
GO
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'profile', N'1', 1)
GO
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'role manager', N'1', 1)
GO
INSERT [dbo].[aspNet_SqlCacheTablesForChangeNotification] ([tableName], [notificationCreated], [changeId]) VALUES (N'cms_articles', CAST(0x0000A39500AC2DA3 AS DateTime), 1707)

GO
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'779a2d88-c8f8-45ba-bfae-170f7d6bd695', N'34e61752-f22a-4b6b-bf4a-b808b50c8bcd', N'ufuka', N'ufuka', NULL, 0, CAST(0x0000A68F0082A84D AS DateTime))
GO
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'779a2d88-c8f8-45ba-bfae-170f7d6bd695', N'3b303d4e-4639-42f1-b382-a8c71f640443', N'keremd', N'keremd', NULL, 0, CAST(0x0000A3B600ED7052 AS DateTime))
GO
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'779a2d88-c8f8-45ba-bfae-170f7d6bd695', N'00be2157-f095-4eee-b0e6-e7e6ffd3e4b1', N'muratb', N'muratb', NULL, 0, CAST(0x0000A3B800C31789 AS DateTime))

GO
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'34e61752-f22a-4b6b-bf4a-b808b50c8bcd', N'5344c95e-085b-4ad2-8956-d6b373ca848d')
GO
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'3b303d4e-4639-42f1-b382-a8c71f640443', N'5344c95e-085b-4ad2-8956-d6b373ca848d')
GO
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'00be2157-f095-4eee-b0e6-e7e6ffd3e4b1', N'5344c95e-085b-4ad2-8956-d6b373ca848d')
GO
INSERT [dbo].[aspnet_WebEvent_Events] ([EventId], [EventTimeUtc], [EventTime], [EventType], [EventSequence], [EventOccurrence], [EventCode], [EventDetailCode], [Message], [ApplicationPath], [ApplicationVirtualPath], [MachineName], [RequestUrl], [ExceptionType], [Details]) VALUES (N'00a224be87994d9aa3b78d5cd5d10e59', CAST(0x0000A4C900C17FF4 AS DateTime), CAST(0x0000A4C900F2F034 AS DateTime), N'EuroCMS.Management.CmsWebEvent', CAST(405 AS Decimal(19, 0)), CAST(1 AS Decimal(19, 0)), 100056, 0, N'FILE_TYPE_DELETE', N'D:\wwwroot\CATCMS\v13Test\cms\', N'/cms', N'MADEBYCAT2008', NULL, NULL, N'Event code: 100056
Event message: FILE_TYPE_DELETE
Event time: 7/2/2015 2:44:30 PM
Event time (UTC): 7/2/2015 11:44:30 AM
Event ID: 00a224be87994d9aa3b78d5cd5d10e59
Event sequence: 405
Event occurrence: 1
Event detail code: 0

Application information:
    Application domain: /LM/W3SVC/8/ROOT/cms-3-130803109919762340
    Trust level: Full
    Application Virtual Path: /cms
    Application Path: D:\wwwroot\CATCMS\v13Test\cms    Machine name: MADEBYCAT2008

Custom event details: 
        100056 created at: 02.07.2015 14:44:30
        
        *********************************
        Header Data:
        Cache-Control: max-age=0
Connection: keep-alive
Content-Length: 162
Content-Type: application/x-www-form-urlencoded
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8
Accept-Encoding: gzip, deflate
Accept-Language: tr-TR,tr;q=0.8,en-US;q=0.6,en;q=0.4
Cookie: ASP.NET_SessionId=xg5bqxesb33vwx2rxasd4rol; __RequestVerificationToken_L2Ntcw2=b3goBHikmxSui8Lozf_EJEN9uLoAdxaHuiwGqye6fXabqc2BmYoJfldSExyOng8q37SHYFU3msZTmAZdUXbamzXPyRKSonwtWY6vESbYMmOXljKFuPLFxlQY9fevpoGdTPjmeA2; .CmsAuthCookie=F5A747CEF006AB2197B80F51F8FEC849A2CDD6EC15843D84712629AEBDEBAB16C7D369278ECF8BF5F80329E4A064A89EC30DF26736E9FA09BAD33845CA4B86D812076418A27F587518C5252097D787B349BA1013641BB8BACB4D71D9D725778E23D65D4D93EE740AB2A8E666B5BA116C448ACBA9; _ga=GA1.2.328243790.1433928098
Host: firmaadi.cmsv1.madebycat.com
Referer: http://firmaadi.cmsv1.madebycat.com/cms/FileType?quickAdd=true
User-Agent: Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.130 Safari/537.36
Origin: http://firmaadi.cmsv1.madebycat.com

        *********************************
        Body Data:
        
        *********** USER DATA **********************
        User ID: tahsins
        Authentication Type: Forms
        User Authenticated: True
        User Roles: Administrator
')
GO
INSERT [dbo].[aspnet_WebEvent_Events] ([EventId], [EventTimeUtc], [EventTime], [EventType], [EventSequence], [EventOccurrence], [EventCode], [EventDetailCode], [Message], [ApplicationPath], [ApplicationVirtualPath], [MachineName], [RequestUrl], [ExceptionType], [Details]) VALUES (N'2a6f39d300524a768afaf5d62fcd8585', CAST(0x0000A4C900C11863 AS DateTime), CAST(0x0000A4C900F288A3 AS DateTime), N'System.Web.Management.WebRequestErrorEvent', CAST(25 AS Decimal(19, 0)), CAST(1 AS Decimal(19, 0)), 3005, 0, N'An unhandled exception has occurred.', N'D:\wwwroot\CATCMS\v13Test\', N'/', N'MADEBYCAT2008', N'http://firmaadi.cmsv1.madebycat.com/Page.aspx', N'System.IndexOutOfRangeException', N'Event code: 3005
Event message: An unhandled exception has occurred.
Event time: 7/2/2015 2:43:02 PM
Event time (UTC): 7/2/2015 11:43:02 AM
Event ID: 2a6f39d300524a768afaf5d62fcd8585
Event sequence: 25
Event occurrence: 1
Event detail code: 0

Application information:
    Application domain: /LM/W3SVC/8/ROOT-2-130803109735832445
    Trust level: Full
    Application Virtual Path: /
    Application Path: D:\wwwroot\CATCMS\v13Test    Machine name: MADEBYCAT2008

Process information:
    Process ID: 5072
    Process name: w3wp.exe
    Account name: IIS APPPOOL\Testv13

Exception information:
    Exception type: System.IndexOutOfRangeException
    Exception message: There is no row at position 0.

Request information:
    Request URL: http://firmaadi.cmsv1.madebycat.com/Page.aspx
    Request path: /Page.aspx
    User host address: 172.26.43.28
    User: tahsins
    Is authenticated: True
    Authentication Type: Forms
    Thread account name: IIS APPPOOL\Testv13

Thread information:
    Thread ID: 6
    Thread account name: IIS APPPOOL\Testv13
    Is impersonating: False
    Stack trace:    at System.Data.RBTree`1.GetNodeByIndex(Int32 userIndex)
   at System.Data.DataRowCollection.get_Item(Int32 index)
   at EuroCMS.FrontEnd.Page.renderPage(String[] QS)
   at EuroCMS.FrontEnd.Page.renderHome(String errorText)
   at EuroCMS.FrontEnd.Page.Page_Load(Object sender, EventArgs e)
   at System.Web.UI.Control.LoadRecursive()
   at System.Web.UI.Page.ProcessRequestMain(Boolean includeStagesBeforeAsyncPoint, Boolean includeStagesAfterAsyncPoint)

')
GO
INSERT [dbo].[aspnet_WebEvent_Events] ([EventId], [EventTimeUtc], [EventTime], [EventType], [EventSequence], [EventOccurrence], [EventCode], [EventDetailCode], [Message], [ApplicationPath], [ApplicationVirtualPath], [MachineName], [RequestUrl], [ExceptionType], [Details]) VALUES (N'635d4651de0946d5ae03409a13e0a8e8', CAST(0x0000A4C900C1620C AS DateTime), CAST(0x0000A4C900F2D24C AS DateTime), N'EuroCMS.Management.CmsWebEvent', CAST(210 AS Decimal(19, 0)), CAST(1 AS Decimal(19, 0)), 100103, 0, N'ACCESS_RULE_DELETE', N'D:\wwwroot\CATCMS\v13Test\cms\', N'/cms', N'MADEBYCAT2008', NULL, NULL, N'Event code: 100103
Event message: ACCESS_RULE_DELETE
Event time: 7/2/2015 2:44:05 PM
Event time (UTC): 7/2/2015 11:44:05 AM
Event ID: 635d4651de0946d5ae03409a13e0a8e8
Event sequence: 210
Event occurrence: 1
Event detail code: 0

Application information:
    Application domain: /LM/W3SVC/8/ROOT/cms-3-130803109919762340
    Trust level: Full
    Application Virtual Path: /cms
    Application Path: D:\wwwroot\CATCMS\v13Test\cms    Machine name: MADEBYCAT2008

Custom event details: 
        100103 created at: 02.07.2015 14:44:05
        
        *********************************
        Header Data:
        Cache-Control: max-age=0
Connection: keep-alive
Content-Length: 162
Content-Type: application/x-www-form-urlencoded
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8
Accept-Encoding: gzip, deflate
Accept-Language: tr-TR,tr;q=0.8,en-US;q=0.6,en;q=0.4
Cookie: ASP.NET_SessionId=xg5bqxesb33vwx2rxasd4rol; __RequestVerificationToken_L2Ntcw2=b3goBHikmxSui8Lozf_EJEN9uLoAdxaHuiwGqye6fXabqc2BmYoJfldSExyOng8q37SHYFU3msZTmAZdUXbamzXPyRKSonwtWY6vESbYMmOXljKFuPLFxlQY9fevpoGdTPjmeA2; .CmsAuthCookie=F5A747CEF006AB2197B80F51F8FEC849A2CDD6EC15843D84712629AEBDEBAB16C7D369278ECF8BF5F80329E4A064A89EC30DF26736E9FA09BAD33845CA4B86D812076418A27F587518C5252097D787B349BA1013641BB8BACB4D71D9D725778E23D65D4D93EE740AB2A8E666B5BA116C448ACBA9; _ga=GA1.2.328243790.1433928098
Host: firmaadi.cmsv1.madebycat.com
Referer: http://firmaadi.cmsv1.madebycat.com/cms/Account/AccessRules
User-Agent: Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.130 Safari/537.36
Origin: http://firmaadi.cmsv1.madebycat.com

        *********************************
        Body Data:
        
        *********** USER DATA **********************
        User ID: tahsins
        Authentication Type: Forms
        User Authenticated: True
        User Roles: Administrator
')
GO
INSERT [dbo].[aspnet_WebEvent_Events] ([EventId], [EventTimeUtc], [EventTime], [EventType], [EventSequence], [EventOccurrence], [EventCode], [EventDetailCode], [Message], [ApplicationPath], [ApplicationVirtualPath], [MachineName], [RequestUrl], [ExceptionType], [Details]) VALUES (N'd5c9633e1ea14c1cad48a851a6a4ac43', CAST(0x0000A4C900C11876 AS DateTime), CAST(0x0000A4C900F288B6 AS DateTime), N'System.Web.Management.WebRequestErrorEvent', CAST(26 AS Decimal(19, 0)), CAST(2 AS Decimal(19, 0)), 3005, 0, N'An unhandled exception has occurred.', N'D:\wwwroot\CATCMS\v13Test\', N'/', N'MADEBYCAT2008', N'http://firmaadi.cmsv1.madebycat.com/Page.aspx?404;http://firmaadi.cmsv1.madebycat.com:80/favicon.ico', N'System.IndexOutOfRangeException', N'Event code: 3005
Event message: An unhandled exception has occurred.
Event time: 7/2/2015 2:43:02 PM
Event time (UTC): 7/2/2015 11:43:02 AM
Event ID: d5c9633e1ea14c1cad48a851a6a4ac43
Event sequence: 26
Event occurrence: 2
Event detail code: 0

Application information:
    Application domain: /LM/W3SVC/8/ROOT-2-130803109735832445
    Trust level: Full
    Application Virtual Path: /
    Application Path: D:\wwwroot\CATCMS\v13Test    Machine name: MADEBYCAT2008

Process information:
    Process ID: 5072
    Process name: w3wp.exe
    Account name: IIS APPPOOL\Testv13

Exception information:
    Exception type: System.IndexOutOfRangeException
    Exception message: There is no row at position 0.

Request information:
    Request URL: http://firmaadi.cmsv1.madebycat.com/Page.aspx?404;http://firmaadi.cmsv1.madebycat.com:80/favicon.ico
    Request path: /Page.aspx
    User host address: 77.79.84.83
    User: 
    Is authenticated: False
    Authentication Type: 
    Thread account name: IIS APPPOOL\Testv13

Thread information:
    Thread ID: 8
    Thread account name: IIS APPPOOL\Testv13
    Is impersonating: False
    Stack trace:    at System.Data.RBTree`1.GetNodeByIndex(Int32 userIndex)
   at System.Data.DataRowCollection.get_Item(Int32 index)
   at EuroCMS.FrontEnd.Page.renderPage(String[] QS)
   at EuroCMS.FrontEnd.Page.render404(String[] QS)
   at EuroCMS.FrontEnd.Page.checkRedirectionAlias(String[] QS)
   at EuroCMS.FrontEnd.Page.Page_Load(Object sender, EventArgs e)
   at System.Web.UI.Control.LoadRecursive()
   at System.Web.UI.Page.ProcessRequestMain(Boolean includeStagesBeforeAsyncPoint, Boolean includeStagesAfterAsyncPoint)

')
GO
SET IDENTITY_INSERT [dbo].[cms_cache_update] ON 

GO
INSERT [dbo].[cms_cache_update] ([id], [status], [timeout], [server_ip], [updated]) VALUES (1, 1, CAST(0x0000A39500D42837 AS DateTime), N'EUROMSG0017 - 22', CAST(0x0000A39500D42837 AS DateTime))
GO
INSERT [dbo].[cms_cache_update] ([id], [status], [timeout], [server_ip], [updated]) VALUES (2, 1, CAST(0x0000A39500D42837 AS DateTime), N'MADEBYCAT2008 - 29', CAST(0x0000A39500D42837 AS DateTime))
GO
INSERT [dbo].[cms_cache_update] ([id], [status], [timeout], [server_ip], [updated]) VALUES (3, 1, CAST(0x0000A39500D42837 AS DateTime), N'EUROMSG0017 - 31', CAST(0x0000A39500D42837 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[cms_cache_update] OFF
GO
SET IDENTITY_INSERT [dbo].[cms_config] ON 

GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (1, N'CHARSET', N'utf-8', N'utf-8', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773D6 AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (2, N'ADMIN_CONTACT', N'System Administrator', N'System Administrator', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773D6 AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (3, N'CACHE_ACTIVE', N'1', N'1', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773D6 AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (4, N'APPROVE_LEVEL', N'3', N'3', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773D6 AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (5, N'APPROVE_FILE_WITH_ARTICLE', N'Y', N'N', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500ECE2B6 AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (6, N'CHILKAT_CRYPT_KEY', N'', N'', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773DA AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (7, N'MAIL_CHARSET', N'utf-8', N'utf-8', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773DA AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (8, N'SEARCH_NEW_WINDOW', N'0', N'0', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773DA AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (9, N'SMTP_USE_SSL', N'0', N'0', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773DA AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (10, N'ZONE_PERMISSION_TYPE', N'AND', N'AND', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773DA AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (11, N'TITLE_PREFIX', N'', N'', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39C00FCED3C AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (12, N'TITLE_SUFFIX', N' - SİTE NAME', N'', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39C00FD0505 AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (13, N'CSRF_WARNING', N'Y', N'Y', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773DF AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (14, N'CSRF_WARNING_EMAILS', N'it@euromsg.com', N'it@euromsg.com', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773DF AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (15, N'FORCE_HTTPS', N'N', N'N', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773DF AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (16, N'FORCE_HTTPS_ONLY_LOGIN', N'N', N'N', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773DF AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (17, N'HTTPS_DETECTION', N'HTTPS', N'HTTPS', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773E4 AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (18, N'AUTO_RELOAD_CACHE', N'N', N'N', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A3AA010AA13B AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (19, N'ADMIN_PATH', N'/cms/', N'/cms/', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773E4 AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (20, N'DEBUG_MODE', N'Y', N'Y', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773E4 AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (21, N'CLEAR_EMPTY_LINES', N'N', N'N', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773E4 AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (22, N'CLEAR_TABS_AND_SPACES', N'N', N'N', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773E8 AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (23, N'EDITOR_CSS', N'', N'', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773E8 AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (24, N'ENABLE_CHECK_OUT', N'0', N'0', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773E8 AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (25, N'RC4_KEY', N'', N'', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773E8 AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (26, N'ERRORS_ALLOWED_IPS', N'', N'', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773E8 AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (27, N'CHILKAT_ZIP_KEY', N'', N'', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773ED AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (28, N'PROXY_USE', N'N', N'N', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773ED AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (29, N'PROXY_SERVER', N'', N'', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773ED AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (30, N'PROXY_LOGIN', N'N', N'N', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773ED AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (31, N'PROXY_USERNAME', N'', N'', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773ED AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (32, N'PROXY_PASSWORD', N'', N'', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773F2 AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (33, N'404_ERROR_LOG', N'Y', N'Y', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773F2 AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (34, N'OMNITURE_PAGE_CODE', N'', N'', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773F7 AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (35, N'PREVIEW_ALLOWED_IPS', N'', N'', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773F7 AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (36, N'BREADCRUMB_CACHE_ACTIVE', N'Y', N'Y', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773F7 AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (37, N'OMNITURE_SCODE_FILENAME', N's_code.js', N's_code.js', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773F7 AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (38, N'404_ERROR_EXTENSIONS', N'ico,js,css', N'ico,js,css', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773F7 AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (39, N'REMOVE_EDITOR_LINKS', N'N', N'N', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773FB AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (40, N'NO_DEFAULT_META', N'N', N'N', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773FB AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (41, N'AUTO_DAILY_RELOAD_CACHE', N'Y', N'N', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A3B10138577D AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (42, N'OMNITURE_TESTNTARGET_FILENAME', N'', N'', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500C773FB AS DateTime))
GO
INSERT [dbo].[cms_config] ([config_id], [config_name], [config_value_local], [config_value_remote], [isDefault], [publisher_id], [updated]) VALUES (43, N'CONFIG_DONE', N'YEAH', N'YEAH', N'Y', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500EC817D AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[cms_config] OFF
GO
SET IDENTITY_INSERT [dbo].[cms_css] ON 

GO
INSERT [dbo].[cms_css] ([css_id], [css_name], [css_status], [css_type], [css_code], [css_fix], [css_rel_text], [css_type_text], [publisher_id], [created], [updated], [group_id], [structure_description]) VALUES (1, N'Default CSS', N'A', 1, N'', N'', N'', N'', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A394012FD578 AS DateTime), CAST(0x0000A394012FD578 AS DateTime), 0, N'')
GO
SET IDENTITY_INSERT [dbo].[cms_css] OFF
GO
SET IDENTITY_INSERT [dbo].[cms_domains] ON 

GO
INSERT [dbo].[cms_domains] ([domain_id], [domain_names], [home_page_article], [domain_status], [created], [created_by], [updated], [updated_by], [error_page_article]) VALUES (5, N'firmaadi.cmsv1.madebycat.com', N'1-1', N'A', CAST(0x0000A39500B89B33 AS DateTime), N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A3B6011EB915 AS DateTime), N'3b303d4e-4639-42f1-b382-a8c71f640443', N'1-1')
GO
SET IDENTITY_INSERT [dbo].[cms_domains] OFF
GO
SET IDENTITY_INSERT [dbo].[cms_file_types] ON 

GO
INSERT [dbo].[cms_file_types] ([type_id], [type_name], [type_alias], [file1_name], [file2_name], [file3_name], [file4_name], [file5_name], [file6_name], [file7_name], [file8_name], [file9_name], [file10_name], [file1_extension], [file2_extension], [file3_extension], [file4_extension], [file5_extension], [file6_extension], [file7_extension], [file8_extension], [file9_extension], [file10_extension], [file1_wh], [file2_wh], [file3_wh], [file4_wh], [file5_wh], [file6_wh], [file7_wh], [file8_wh], [file9_wh], [file10_wh], [file1_size], [file2_size], [file3_size], [file4_size], [file5_size], [file6_size], [file7_size], [file8_size], [file9_size], [file10_size], [created], [updated], [group_id], [structure_description]) VALUES (3, N'Haberler', N'news', N'Gorsel (215x303)', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'jpg,png,jpeg,gif', N'', N'', N'', N'', N'', N'', N'', N'', N'null', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, CAST(0x0000A3950127D66F AS DateTime), CAST(0x0000A3950127E4B6 AS DateTime), 0, N'')
GO
SET IDENTITY_INSERT [dbo].[cms_file_types] OFF
GO
INSERT [dbo].[cms_languages] ([lang_id], [lang_name], [lang_xml], [lang_order], [publisher_id], [created], [updated], [lang_alias]) VALUES (N'TR', N'Türkçe', N'', 1, N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A394012FA935 AS DateTime), CAST(0x0000A39500C77738 AS DateTime), N'')
GO

SET IDENTITY_INSERT [dbo].[cms_sites] ON 

GO
INSERT [dbo].[cms_sites] ([site_id], [site_name], [css_id], [css_id_mobile], [css_id_print], [template_id], [template_id_mobile], [publisher_id], [site_keywords], [site_header], [site_js], [custom_body], [site_icon], [created], [updated], [article_1], [article_2], [article_3], [article_4], [article_5], [analytics], [tag_detail_article], [group_id], [structure_description], [meta_description], [content_1_editor_type], [content_2_editor_type], [content_3_editor_type], [content_4_editor_type], [content_5_editor_type], [default_article], [omniture_code], [domain_id], [site_alias]) VALUES (6, N'Demo TR', 0, 0, 0, 1, 0, N'3b303d4e-4639-42f1-b382-a8c71f640443', N'', N'<!-- Basic Page Needs -->
	<meta charset="utf-8" />
	<meta http-equiv="X-UA-Compatible" content="IE=11">
	<meta name="author" content="madebycat.com">
	
	<!-- Mobile Specific Metas -->
	<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
	<meta name="apple-mobile-web-app-capable" content="yes">
	
	<!-- Fav and touch icons -->
<link rel="apple-touch-icon" href="i/assets/images/touch-icon-iphone-60.png">
	<link rel="apple-touch-icon" sizes="76x76" href="i/assets/images/touch-icon-ipad-76.png">
	<link rel="apple-touch-icon" sizes="120x120" href="i/assets/images/touch-icon-iphone-retina-120.png">
	<link rel="apple-touch-icon" sizes="152x152" href="i/assets/images/touch-icon-ipad-retina-152.png">

	<!-- styles -->
	<link  media="screen,projection" rel="stylesheet" type="text/css" href="i/assets/styles/skeleton_12cols.css">
	<link  media="screen,projection" rel="stylesheet" type="text/css" href="i/assets/styles/screen.css">
	<link  media="screen,projection" rel="stylesheet" type="text/css" href="i/assets/styles/responsive.css">
	<link media="print" rel="stylesheet" type="text/css" href="i/assets/styles/print.css">
	
	<!-- Javascripts -->
	<script type="text/javascript" src="i/assets/scripts/jquery-1-8-3.js"></script>
	<script type="text/javascript" src="i/assets/scripts/jquery.validate.min.js"></script>
	<script type="text/javascript" src="i/assets/scripts/jquery.validate.messages_tr.js"></script>
	<script type="text/javascript" src="i/assets/scripts/ilightbox.js"></script>
	<script type="text/javascript" src="i/assets/scripts/owl.carousel.min.js"></script>
	<script type="text/javascript" src="i/assets/scripts/jquery.easing.1.3.js"></script>
	<script type="text/javascript" src="i/assets/scripts/jquery.mobile.just-touch.js"></script>
	<script type="text/javascript" src="i/assets/scripts/tweenlite.js"></script>
	<script type="text/javascript" src="i/assets/scripts/mightyslider.min.js"></script>
	<script type="text/javascript" src="i/assets/scripts/jquery.customSelect.min.js"></script>
	<script type="text/javascript" src="i/assets/scripts/jquery.flexslider.js"></script>
	<script type="text/javascript" src="i/assets/scripts/jquery-ui.min.js"></script>
	<script type="text/javascript" src="i/assets/scripts/jquery.beforeafter-1.4.js"></script>
	<script type="text/javascript" src="i/assets/scripts/jquery.matchHeight-min.js"></script>
	<script type="text/javascript" src="i/assets/scripts/jquery.anystretch.min.js"></script>
	<script type="text/javascript" src="i/assets/scripts/skrollr.js"></script>
	<script type="text/javascript" src="i/assets/scripts/global.js"></script>', N'', N'', N'i/assets/images/favicon.png', CAST(0x0000A302011B300E AS DateTime), CAST(0x0000A3AE010699D2 AS DateTime), N'', N'', N'', N'', N'', N'<script>
  (function(i,s,o,g,r,a,m){i[''GoogleAnalyticsObject'']=r;i[r]=i[r]||function(){
  (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
  m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
  })(window,document,''script'',''//www.google-analytics.com/analytics.js'',''ga'');

  ga(''create'', ''UA-54938639-1'', ''auto'');
  ga(''send'', ''pageview'');

</script>', N'', 0, N'', N'', N'H', N'H', N' ', N' ', N' ', N'', N'', 5, N'')
GO
SET IDENTITY_INSERT [dbo].[cms_sites] OFF
GO
SET IDENTITY_INSERT [dbo].[cms_template_revisions] ON 

GO
INSERT [dbo].[cms_template_revisions] ([history_id], [template_id], [template_html], [publisher_id], [created], [template_type], [content_1_editor_type], [template_doctype]) VALUES (1, 1, N'Please Change This Default Template', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A39500D232F5 AS DateTime), 0, N'H', N'')
GO
SET IDENTITY_INSERT [dbo].[cms_template_revisions] OFF
GO
SET IDENTITY_INSERT [dbo].[cms_templates] ON 

GO
INSERT [dbo].[cms_templates] ([template_id], [template_status], [template_type], [template_name], [template_html], [publisher_id], [created], [updated], [group_id], [structure_description], [content_1_editor_type], [template_doctype]) VALUES (1, N'A', 0, N'TR - Anasayfa Template', N'Please Change This Template!', N'3b303d4e-4639-42f1-b382-a8c71f640443', CAST(0x0000A394012FD594 AS DateTime), CAST(0x0000A3AE0105AD60 AS DateTime), 0, N'', N'H', N'&lt;!DOCTYPE html&gt;')
GO
SET IDENTITY_INSERT [dbo].[cms_templates] OFF
GO
/****** Object:  Index [PK__aspnet_A__C93A4C984BA6FAA4]    Script Date: 21.10.2015 11:49:37 ******/
ALTER TABLE [dbo].[aspnet_Applications] ADD PRIMARY KEY NONCLUSTERED 
(
	[ApplicationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__aspnet_A__17477DE4CA1789F5]    Script Date: 21.10.2015 11:49:37 ******/
ALTER TABLE [dbo].[aspnet_Applications] ADD UNIQUE NONCLUSTERED 
(
	[LoweredApplicationName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__aspnet_A__30910331D529FA1A]    Script Date: 21.10.2015 11:49:37 ******/
ALTER TABLE [dbo].[aspnet_Applications] ADD UNIQUE NONCLUSTERED 
(
	[ApplicationName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK__aspnet_M__1788CC4DABEFB0C6]    Script Date: 21.10.2015 11:49:37 ******/
ALTER TABLE [dbo].[aspnet_Membership] ADD PRIMARY KEY NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK__aspnet_P__CD67DC587314C2CC]    Script Date: 21.10.2015 11:49:37 ******/
ALTER TABLE [dbo].[aspnet_Paths] ADD PRIMARY KEY NONCLUSTERED 
(
	[PathId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK__aspnet_P__3214EC06D94E46DD]    Script Date: 21.10.2015 11:49:37 ******/
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser] ADD PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK__aspnet_R__8AFACE1B1B37A776]    Script Date: 21.10.2015 11:49:37 ******/
ALTER TABLE [dbo].[aspnet_Roles] ADD PRIMARY KEY NONCLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK__aspnet_U__1788CC4D9B8E9422]    Script Date: 21.10.2015 11:49:37 ******/
ALTER TABLE [dbo].[aspnet_Users] ADD PRIMARY KEY NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK_cms_AccessRules]    Script Date: 21.10.2015 11:49:37 ******/
ALTER TABLE [dbo].[cms_AccessRules] ADD  CONSTRAINT [PK_cms_AccessRules] PRIMARY KEY NONCLUSTERED 
(
	[RuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[aspnet_Applications] ADD  DEFAULT (newid()) FOR [ApplicationId]
GO
ALTER TABLE [dbo].[aspnet_Membership] ADD  DEFAULT ((0)) FOR [PasswordFormat]
GO
ALTER TABLE [dbo].[aspnet_Paths] ADD  DEFAULT (newid()) FOR [PathId]
GO
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[aspnet_Roles] ADD  DEFAULT (newid()) FOR [RoleId]
GO
ALTER TABLE [dbo].[aspNet_SqlCacheTablesForChangeNotification] ADD  DEFAULT (getdate()) FOR [notificationCreated]
GO
ALTER TABLE [dbo].[aspNet_SqlCacheTablesForChangeNotification] ADD  DEFAULT ((0)) FOR [changeId]
GO
ALTER TABLE [dbo].[aspnet_Users] ADD  DEFAULT (newid()) FOR [UserId]
GO
ALTER TABLE [dbo].[aspnet_Users] ADD  DEFAULT (NULL) FOR [MobileAlias]
GO
ALTER TABLE [dbo].[aspnet_Users] ADD  DEFAULT ((0)) FOR [IsAnonymous]
GO
ALTER TABLE [dbo].[cms_AccessRules] ADD  CONSTRAINT [DF_cms_AccessRules_ContentId]  DEFAULT ((0)) FOR [ContentId]
GO
ALTER TABLE [dbo].[cms_AccessRules] ADD  CONSTRAINT [DF_cms_AccessRules_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[cms_article_cache] ADD  CONSTRAINT [DF_cms_article_cache_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_article_files] ADD  CONSTRAINT [DF_cms_article_files_file_title]  DEFAULT ('') FOR [file_title]
GO
ALTER TABLE [dbo].[cms_article_files] ADD  CONSTRAINT [DF_cms_article_files_file_order]  DEFAULT ((0)) FOR [file_order]
GO
ALTER TABLE [dbo].[cms_article_files] ADD  CONSTRAINT [DF_cms_article_files_file_name_1]  DEFAULT ('') FOR [file_name_1]
GO
ALTER TABLE [dbo].[cms_article_files] ADD  CONSTRAINT [DF_cms_article_files_file_name_2]  DEFAULT ('') FOR [file_name_2]
GO
ALTER TABLE [dbo].[cms_article_files] ADD  CONSTRAINT [DF_cms_article_files_file_name_3]  DEFAULT ('') FOR [file_name_3]
GO
ALTER TABLE [dbo].[cms_article_files] ADD  CONSTRAINT [DF_cms_article_files_file_name_4]  DEFAULT ('') FOR [file_name_4]
GO
ALTER TABLE [dbo].[cms_article_files] ADD  CONSTRAINT [DF_cms_article_files_file_name_5]  DEFAULT ('') FOR [file_name_5]
GO
ALTER TABLE [dbo].[cms_article_files] ADD  CONSTRAINT [DF_cms_article_files_file_name_6]  DEFAULT ('') FOR [file_name_6]
GO
ALTER TABLE [dbo].[cms_article_files] ADD  CONSTRAINT [DF_cms_article_files_file_name_7]  DEFAULT ('') FOR [file_name_7]
GO
ALTER TABLE [dbo].[cms_article_files] ADD  CONSTRAINT [DF_cms_article_files_file_name_8]  DEFAULT ('') FOR [file_name_8]
GO
ALTER TABLE [dbo].[cms_article_files] ADD  CONSTRAINT [DF_cms_article_files_file_name_9]  DEFAULT ('') FOR [file_name_9]
GO
ALTER TABLE [dbo].[cms_article_files] ADD  CONSTRAINT [DF_cms_article_files_file_name_10]  DEFAULT ('') FOR [file_name_10]
GO
ALTER TABLE [dbo].[cms_article_files] ADD  CONSTRAINT [DF_cms_article_files_file_type_id]  DEFAULT ((0)) FOR [file_type_id]
GO
ALTER TABLE [dbo].[cms_article_files] ADD  CONSTRAINT [DF_cms_article_files_file_comment]  DEFAULT ('') FOR [file_comment]
GO
ALTER TABLE [dbo].[cms_article_files_revision] ADD  CONSTRAINT [DF_cms_article_files_revision_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_article_files_revision] ADD  CONSTRAINT [DF_cms_article_files_revision_rev_date]  DEFAULT (getdate()) FOR [rev_date]
GO
ALTER TABLE [dbo].[cms_article_files_revision] ADD  CONSTRAINT [DF_cms_article_files_revision_revision_status]  DEFAULT ('N') FOR [revision_status]
GO
ALTER TABLE [dbo].[cms_article_files_revision_files] ADD  CONSTRAINT [DF_cms_article_files_revision_files_file_title]  DEFAULT ('') FOR [file_title]
GO
ALTER TABLE [dbo].[cms_article_files_revision_files] ADD  CONSTRAINT [DF_cms_article_files_revision_files_file_order]  DEFAULT ((0)) FOR [file_order]
GO
ALTER TABLE [dbo].[cms_article_files_revision_files] ADD  CONSTRAINT [DF_cms_article_files_revision_files_file_name_1]  DEFAULT ('') FOR [file_name_1]
GO
ALTER TABLE [dbo].[cms_article_files_revision_files] ADD  CONSTRAINT [DF_cms_article_files_revision_files_file_name_2]  DEFAULT ('') FOR [file_name_2]
GO
ALTER TABLE [dbo].[cms_article_files_revision_files] ADD  CONSTRAINT [DF_cms_article_files_revision_files_file_name_3]  DEFAULT ('') FOR [file_name_3]
GO
ALTER TABLE [dbo].[cms_article_files_revision_files] ADD  CONSTRAINT [DF_cms_article_files_revision_files_file_name_4]  DEFAULT ('') FOR [file_name_4]
GO
ALTER TABLE [dbo].[cms_article_files_revision_files] ADD  CONSTRAINT [DF_cms_article_files_revision_files_file_name_5]  DEFAULT ('') FOR [file_name_5]
GO
ALTER TABLE [dbo].[cms_article_files_revision_files] ADD  CONSTRAINT [DF_cms_article_files_revision_files_file_name_6]  DEFAULT ('') FOR [file_name_6]
GO
ALTER TABLE [dbo].[cms_article_files_revision_files] ADD  CONSTRAINT [DF_cms_article_files_revision_files_file_name_7]  DEFAULT ('') FOR [file_name_7]
GO
ALTER TABLE [dbo].[cms_article_files_revision_files] ADD  CONSTRAINT [DF_cms_article_files_revision_files_file_name_8]  DEFAULT ('') FOR [file_name_8]
GO
ALTER TABLE [dbo].[cms_article_files_revision_files] ADD  CONSTRAINT [DF_cms_article_files_revision_files_file_name_9]  DEFAULT ('') FOR [file_name_9]
GO
ALTER TABLE [dbo].[cms_article_files_revision_files] ADD  CONSTRAINT [DF_cms_article_files_revision_files_file_name_10]  DEFAULT ('') FOR [file_name_10]
GO
ALTER TABLE [dbo].[cms_article_files_revision_files] ADD  CONSTRAINT [DF_cms_article_files_revision_files_file_comment]  DEFAULT ('') FOR [file_comment]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_rev_date]  DEFAULT (getdate()) FOR [rev_date]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_revision_status]  DEFAULT ('N') FOR [revision_status]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_rev_name]  DEFAULT ('') FOR [rev_name]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_rev_note]  DEFAULT ('') FOR [rev_note]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_rev_flag]  DEFAULT ((0)) FOR [rev_flag_1]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_rev_flag_11]  DEFAULT ((0)) FOR [rev_flag_2]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_rev_flag_21]  DEFAULT ((0)) FOR [rev_flag_3]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_rev_flag_23]  DEFAULT ((0)) FOR [rev_flag_4]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_rev_flag_22]  DEFAULT ((0)) FOR [rev_flag_5]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_clsf_id]  DEFAULT ((0)) FOR [clsf_id]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_status]  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_orderno]  DEFAULT ((0)) FOR [orderno]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_lang_id]  DEFAULT ('TR') FOR [lang_id]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_navigation_display]  DEFAULT ((1)) FOR [navigation_display]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_navigation_zone_id]  DEFAULT ((0)) FOR [navigation_zone_id]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_menu_text]  DEFAULT ('') FOR [menu_text]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_headline]  DEFAULT ('') FOR [headline]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_summary]  DEFAULT ('') FOR [summary]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_keywords]  DEFAULT ('') FOR [keywords]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_article_type]  DEFAULT ((0)) FOR [article_type]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_article_type_detail]  DEFAULT ('') FOR [article_type_detail]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_article_1]  DEFAULT ('') FOR [article_1]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_article_2]  DEFAULT ('') FOR [article_2]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_article_3]  DEFAULT ('') FOR [article_3]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_article_4]  DEFAULT ('') FOR [article_4]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_article_5]  DEFAULT ('') FOR [article_5]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_custom_1]  DEFAULT ('') FOR [custom_1]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_custom_2]  DEFAULT ('') FOR [custom_2]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_custom_3]  DEFAULT ('') FOR [custom_3]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_custom_4]  DEFAULT ('') FOR [custom_4]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_custom_5]  DEFAULT ('') FOR [custom_5]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_custom_11]  DEFAULT ('') FOR [custom_6]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_custom_21]  DEFAULT ('') FOR [custom_7]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_custom_31]  DEFAULT ('') FOR [custom_8]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_custom_41]  DEFAULT ('') FOR [custom_9]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_custom_51]  DEFAULT ('') FOR [custom_10]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_custom_11_1]  DEFAULT ('') FOR [custom_11]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_custom_12]  DEFAULT ('') FOR [custom_12]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_custom_13]  DEFAULT ('') FOR [custom_13]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_custom_14]  DEFAULT ('') FOR [custom_14]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_custom_15]  DEFAULT ('') FOR [custom_15]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_custom_16]  DEFAULT ('') FOR [custom_16]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_custom_17]  DEFAULT ('') FOR [custom_17]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_custom_18]  DEFAULT ('') FOR [custom_18]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_custom_19]  DEFAULT ('') FOR [custom_19]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_custom_20]  DEFAULT ('') FOR [custom_20]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_flag_1]  DEFAULT ((0)) FOR [flag_1]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_flag_2]  DEFAULT ((0)) FOR [flag_2]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_flag_3]  DEFAULT ((0)) FOR [flag_3]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_flag_4]  DEFAULT ((0)) FOR [flag_4]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_flag_5]  DEFAULT ((0)) FOR [flag_5]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_cl_1]  DEFAULT ((0)) FOR [cl_1]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_cl_2]  DEFAULT ((0)) FOR [cl_2]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_cl_3]  DEFAULT ((0)) FOR [cl_3]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_cl_4]  DEFAULT ((0)) FOR [cl_4]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_cl_5]  DEFAULT ((0)) FOR [cl_5]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_custom_body]  DEFAULT ('') FOR [custom_body]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_meta_description]  DEFAULT ('') FOR [meta_description]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_content_1_editor_type]  DEFAULT ('H') FOR [content_1_editor_type]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_content_2_editor_type]  DEFAULT ('H') FOR [content_2_editor_type]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_content_3_editor_type]  DEFAULT ('H') FOR [content_3_editor_type]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_content_4_editor_type]  DEFAULT ('H') FOR [content_4_editor_type]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_content_5_editor_type]  DEFAULT ('H') FOR [content_5_editor_type]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_omniture_code]  DEFAULT ('') FOR [omniture_code]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  CONSTRAINT [DF_cms_article_revision_custom_setting]  DEFAULT ('') FOR [custom_setting]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  DEFAULT ('') FOR [before_head]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  DEFAULT ('') FOR [before_body]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  DEFAULT ((0)) FOR [no_index_no_follow]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  DEFAULT ('') FOR [custom_html_attr]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  DEFAULT ('') FOR [meta_title]
GO
ALTER TABLE [dbo].[cms_article_revision] ADD  DEFAULT ('') FOR [canonical_url]
GO
ALTER TABLE [dbo].[cms_article_search] ADD  CONSTRAINT [DF_cms_article_search_article_id]  DEFAULT ((0)) FOR [article_id]
GO
ALTER TABLE [dbo].[cms_article_search] ADD  CONSTRAINT [DF_cms_article_search_zone_id]  DEFAULT ((0)) FOR [zone_id]
GO
ALTER TABLE [dbo].[cms_article_search] ADD  CONSTRAINT [DF_cms_article_search_search_text]  DEFAULT ('') FOR [search_text]
GO
ALTER TABLE [dbo].[cms_article_search] ADD  CONSTRAINT [DF_cms_article_search_headline]  DEFAULT ('') FOR [headline]
GO
ALTER TABLE [dbo].[cms_article_search] ADD  CONSTRAINT [DF_cms_article_search_summary]  DEFAULT ('') FOR [summary]
GO
ALTER TABLE [dbo].[cms_article_search] ADD  CONSTRAINT [DF_cms_article_search_keywords]  DEFAULT ('') FOR [keywords]
GO
ALTER TABLE [dbo].[cms_article_search] ADD  CONSTRAINT [DF_cms_article_search_description]  DEFAULT ('') FOR [description]
GO
ALTER TABLE [dbo].[cms_article_zones] ADD  CONSTRAINT [DF_cms_article_zones_az_order]  DEFAULT ((0)) FOR [az_order]
GO
ALTER TABLE [dbo].[cms_article_zones] ADD  CONSTRAINT [DF_cms_article_zones_az_alias]  DEFAULT ('') FOR [az_alias]
GO
ALTER TABLE [dbo].[cms_article_zones] ADD  DEFAULT ((0)) FOR [is_alias_protected]
GO
ALTER TABLE [dbo].[cms_article_zones_revision] ADD  CONSTRAINT [DF_cms_article_zones_revision_az_order]  DEFAULT ((0)) FOR [az_order]
GO
ALTER TABLE [dbo].[cms_article_zones_revision] ADD  CONSTRAINT [DF_cms_article_zones_revision_az_alias]  DEFAULT ('') FOR [az_alias]
GO
ALTER TABLE [dbo].[cms_article_zones_revision] ADD  DEFAULT ((0)) FOR [is_alias_protected]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_clsf_id]  DEFAULT ((0)) FOR [clsf_id]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_status]  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_updated]  DEFAULT (getdate()) FOR [updated]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_clicks]  DEFAULT ((0)) FOR [clicks]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_orderno]  DEFAULT ((0)) FOR [orderno]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_lang_id]  DEFAULT ('TR') FOR [lang_id]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_navigation_display]  DEFAULT ((1)) FOR [navigation_display]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_navigation_zone_id]  DEFAULT ((0)) FOR [navigation_zone_id]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_menu_text]  DEFAULT ('') FOR [menu_text]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_headline]  DEFAULT ('') FOR [headline]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_summary]  DEFAULT ('') FOR [summary]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_keywords]  DEFAULT ('') FOR [keywords]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_article_type]  DEFAULT ((0)) FOR [article_type]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_article_type_detail]  DEFAULT ('') FOR [article_type_detail]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_article_1]  DEFAULT ('') FOR [article_1]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_article_2]  DEFAULT ('') FOR [article_2]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_article_3]  DEFAULT ('') FOR [article_3]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_article_4]  DEFAULT ('') FOR [article_4]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_article_5]  DEFAULT ('') FOR [article_5]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_custom_1]  DEFAULT ('') FOR [custom_1]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_custom_11]  DEFAULT ('') FOR [custom_2]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_custom_12]  DEFAULT ('') FOR [custom_3]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_custom_13]  DEFAULT ('') FOR [custom_4]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_custom_14]  DEFAULT ('') FOR [custom_5]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_custom_11_1]  DEFAULT ('') FOR [custom_6]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_custom_21]  DEFAULT ('') FOR [custom_7]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_custom_31]  DEFAULT ('') FOR [custom_8]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_custom_41]  DEFAULT ('') FOR [custom_9]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_custom_51]  DEFAULT ('') FOR [custom_10]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_custom_11_2]  DEFAULT ('') FOR [custom_11]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_custom_12_1]  DEFAULT ('') FOR [custom_12]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_custom_13_1]  DEFAULT ('') FOR [custom_13]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_custom_14_1]  DEFAULT ('') FOR [custom_14]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_custom_15]  DEFAULT ('') FOR [custom_15]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_custom_16]  DEFAULT ('') FOR [custom_16]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_custom_17]  DEFAULT ('') FOR [custom_17]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_custom_18]  DEFAULT ('') FOR [custom_18]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_custom_19]  DEFAULT ('') FOR [custom_19]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_custom_20]  DEFAULT ('') FOR [custom_20]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_flag_1]  DEFAULT ((0)) FOR [flag_1]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_flag_11]  DEFAULT ((0)) FOR [flag_2]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_flag_12]  DEFAULT ((0)) FOR [flag_3]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_flag_11_1]  DEFAULT ((0)) FOR [flag_4]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_flag_12_1]  DEFAULT ((0)) FOR [flag_5]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_cl_1]  DEFAULT ((0)) FOR [cl_1]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_cl_11]  DEFAULT ((0)) FOR [cl_2]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_cl_12]  DEFAULT ((0)) FOR [cl_3]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_cl_13]  DEFAULT ((0)) FOR [cl_4]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_cl_14]  DEFAULT ((0)) FOR [cl_5]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_custom_body1]  DEFAULT ('') FOR [custom_body]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_rating]  DEFAULT ((0)) FOR [rating]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_ratingcount]  DEFAULT ((0)) FOR [ratingcount]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_meta_description]  DEFAULT ('') FOR [meta_description]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_omniture_code]  DEFAULT ('') FOR [omniture_code]
GO
ALTER TABLE [dbo].[cms_articles] ADD  CONSTRAINT [DF_cms_articles_custom_setting]  DEFAULT ('') FOR [custom_setting]
GO
ALTER TABLE [dbo].[cms_articles] ADD  DEFAULT ('') FOR [before_head]
GO
ALTER TABLE [dbo].[cms_articles] ADD  DEFAULT ('') FOR [before_body]
GO
ALTER TABLE [dbo].[cms_articles] ADD  DEFAULT ((0)) FOR [no_index_no_follow]
GO
ALTER TABLE [dbo].[cms_articles] ADD  DEFAULT ('') FOR [custom_html_attr]
GO
ALTER TABLE [dbo].[cms_articles] ADD  DEFAULT ('') FOR [meta_title]
GO
ALTER TABLE [dbo].[cms_articles] ADD  DEFAULT ('') FOR [canonical_url]
GO
ALTER TABLE [dbo].[cms_asp_errors] ADD  CONSTRAINT [DF_ASP_ERRORS_MonitorSent]  DEFAULT ('N') FOR [MonitorSent]
GO
ALTER TABLE [dbo].[cms_asp_errors] ADD  CONSTRAINT [DF_ASP_ERRORS_AAS_Checked]  DEFAULT ('N') FOR [AAS_Checked]
GO
ALTER TABLE [dbo].[cms_breadcrumbs] ADD  CONSTRAINT [DF_cms_breadcrumbs_alias]  DEFAULT ('') FOR [breadcrumb_name]
GO
ALTER TABLE [dbo].[cms_breadcrumbs] ADD  CONSTRAINT [DF_cms_breadcrumbs_deep_level]  DEFAULT ((0)) FOR [deep_level]
GO
ALTER TABLE [dbo].[cms_breadcrumbs] ADD  CONSTRAINT [DF_cms_breadcrumbs_include_site]  DEFAULT ('Y') FOR [include_site]
GO
ALTER TABLE [dbo].[cms_breadcrumbs] ADD  CONSTRAINT [DF_cms_breadcrumbs_include_zonegroup]  DEFAULT ('N') FOR [include_zonegroup]
GO
ALTER TABLE [dbo].[cms_breadcrumbs] ADD  CONSTRAINT [DF_cms_breadcrumbs_include_headline]  DEFAULT ('Y') FOR [include_headline]
GO
ALTER TABLE [dbo].[cms_breadcrumbs] ADD  CONSTRAINT [DF_cms_breadcrumbs_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_breadcrumbs] ADD  CONSTRAINT [DF_cms_breadcrumbs_excluded_sites]  DEFAULT ('') FOR [excluded_sites]
GO
ALTER TABLE [dbo].[cms_breadcrumbs] ADD  CONSTRAINT [DF_cms_breadcrumbs_excluded_zone_groups]  DEFAULT ('') FOR [excluded_zonegroups]
GO
ALTER TABLE [dbo].[cms_breadcrumbs] ADD  CONSTRAINT [DF_cms_breadcrumbs_excluded_zones]  DEFAULT ('') FOR [excluded_zones]
GO
ALTER TABLE [dbo].[cms_breadcrumbs] ADD  CONSTRAINT [DF_cms_breadcrumbs_seperator]  DEFAULT ('') FOR [seperator]
GO
ALTER TABLE [dbo].[cms_breadcrumbs] ADD  CONSTRAINT [DF_cms_breadcrumbs_ul_class]  DEFAULT ('') FOR [ul_class]
GO
ALTER TABLE [dbo].[cms_breadcrumbs] ADD  CONSTRAINT [DF_cms_breadcrumbs_include_submenus]  DEFAULT ('Y') FOR [include_submenus]
GO
ALTER TABLE [dbo].[cms_breadcrumbs] ADD  CONSTRAINT [DF_cms_breadcrumbs_breadcrumb_main_container]  DEFAULT ('ul') FOR [breadcrumb_main_container]
GO
ALTER TABLE [dbo].[cms_breadcrumbs] ADD  CONSTRAINT [DF_cms_breadcrumbs_breadcrumb_main_item_container]  DEFAULT ('li') FOR [breadcrumb_main_item_container]
GO
ALTER TABLE [dbo].[cms_breadcrumbs] ADD  CONSTRAINT [DF_cms_breadcrumbs_breadcrumb_sub_container]  DEFAULT ('ul') FOR [breadcrumb_sub_container]
GO
ALTER TABLE [dbo].[cms_breadcrumbs] ADD  CONSTRAINT [DF_cms_breadcrumbs_breadcrumb_sub_item_container]  DEFAULT ('li') FOR [breadcrumb_sub_item_container]
GO
ALTER TABLE [dbo].[cms_cache_update] ADD  CONSTRAINT [DF_cms_cache_update_status]  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[cms_cache_update] ADD  CONSTRAINT [DF_cacheupdate_updated]  DEFAULT (getdate()) FOR [timeout]
GO
ALTER TABLE [dbo].[cms_cache_update] ADD  CONSTRAINT [DF_CacheUpdate_server_ip]  DEFAULT ('') FOR [server_ip]
GO
ALTER TABLE [dbo].[cms_cache_update] ADD  CONSTRAINT [DF_CacheUpdate_updated_1]  DEFAULT (getdate()) FOR [updated]
GO
ALTER TABLE [dbo].[cms_classification_combo_values] ADD  CONSTRAINT [DF_cms_classification_combo_values_combo_supid]  DEFAULT ('') FOR [combo_supid]
GO
ALTER TABLE [dbo].[cms_classification_combo_values] ADD  CONSTRAINT [DF_cms_classification_combo_values_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_summary_cb]  DEFAULT ((0)) FOR [summary_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_enddate_cb]  DEFAULT ((0)) FOR [enddate_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_keywords_cb]  DEFAULT ((0)) FOR [keywords_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom1]  DEFAULT ((0)) FOR [custom1_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom2]  DEFAULT ((0)) FOR [custom2_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom3]  DEFAULT ((0)) FOR [custom3_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom4]  DEFAULT ((0)) FOR [custom4_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom5]  DEFAULT ((0)) FOR [custom5_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom6]  DEFAULT ((0)) FOR [custom6_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom7]  DEFAULT ((0)) FOR [custom7_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom8]  DEFAULT ((0)) FOR [custom8_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom9]  DEFAULT ((0)) FOR [custom9_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom10]  DEFAULT ((0)) FOR [custom10_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom1_cb1]  DEFAULT ((0)) FOR [custom11_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom2_cb1]  DEFAULT ((0)) FOR [custom12_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom3_cb1]  DEFAULT ((0)) FOR [custom13_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom4_cb1]  DEFAULT ((0)) FOR [custom14_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom5_cb1]  DEFAULT ((0)) FOR [custom15_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom6_cb1]  DEFAULT ((0)) FOR [custom16_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom7_cb1]  DEFAULT ((0)) FOR [custom17_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom8_cb1]  DEFAULT ((0)) FOR [custom18_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom9_cb1]  DEFAULT ((0)) FOR [custom19_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom10_cb1]  DEFAULT ((0)) FOR [custom20_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_flag1]  DEFAULT ((0)) FOR [flag1_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_flag2]  DEFAULT ((0)) FOR [flag2_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_flag3]  DEFAULT ((0)) FOR [flag3_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_flag4]  DEFAULT ((0)) FOR [flag4_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_flag5]  DEFAULT ((0)) FOR [flag5_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_date1_cb]  DEFAULT ((0)) FOR [date1_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_date2_cb]  DEFAULT ((0)) FOR [date2_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_date1_cb1]  DEFAULT ((0)) FOR [date3_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_date2_cb1]  DEFAULT ((0)) FOR [date4_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_date4_cb1]  DEFAULT ((0)) FOR [date5_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom1_1]  DEFAULT ('') FOR [custom1_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom2_1]  DEFAULT ('') FOR [custom2_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom3_1]  DEFAULT ('') FOR [custom3_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom4_1]  DEFAULT ('') FOR [custom4_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom5_1]  DEFAULT ('') FOR [custom5_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom6_1]  DEFAULT ('') FOR [custom6_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom7_1]  DEFAULT ('') FOR [custom7_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom8_1]  DEFAULT ('') FOR [custom8_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom9_1]  DEFAULT ('') FOR [custom9_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom10_1]  DEFAULT ('') FOR [custom10_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom1_text1]  DEFAULT ('') FOR [custom11_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom2_text1]  DEFAULT ('') FOR [custom12_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom3_text1]  DEFAULT ('') FOR [custom13_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom4_text1]  DEFAULT ('') FOR [custom14_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom5_text1]  DEFAULT ('') FOR [custom15_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom6_text1]  DEFAULT ('') FOR [custom16_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom7_text1]  DEFAULT ('') FOR [custom17_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom8_text1]  DEFAULT ('') FOR [custom18_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom9_text1]  DEFAULT ('') FOR [custom19_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom10_text1]  DEFAULT ('') FOR [custom20_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_flag1_1]  DEFAULT ('') FOR [flag1_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_flag2_1]  DEFAULT ('') FOR [flag2_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_flag3_1]  DEFAULT ('') FOR [flag3_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_flag4_1]  DEFAULT ('') FOR [flag4_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_flag5_1]  DEFAULT ('') FOR [flag5_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_date1]  DEFAULT ('') FOR [date1_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_date2]  DEFAULT ('') FOR [date2_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_date1_text1]  DEFAULT ('') FOR [date3_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_date2_text1]  DEFAULT ('') FOR [date4_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_date4_text1]  DEFAULT ('') FOR [date5_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom1_type]  DEFAULT ('t') FOR [custom1_type]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom2_type]  DEFAULT ('t') FOR [custom2_type]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom3_type]  DEFAULT ('t') FOR [custom3_type]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom4_type]  DEFAULT ('t') FOR [custom4_type]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom5_type]  DEFAULT ('t') FOR [custom5_type]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom6_type]  DEFAULT ('t') FOR [custom6_type]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom7_type]  DEFAULT ('t') FOR [custom7_type]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom8_type]  DEFAULT ('t') FOR [custom8_type]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom9_type]  DEFAULT ('t') FOR [custom9_type]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom10_type]  DEFAULT ('t') FOR [custom10_type]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_summary_text]  DEFAULT ('') FOR [summary_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_enddate_text]  DEFAULT ('') FOR [enddate_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_keywords_text]  DEFAULT ('') FOR [keywords_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_article1_text]  DEFAULT ('') FOR [article1_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_article2_text]  DEFAULT ('') FOR [article2_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_article3_text]  DEFAULT ('') FOR [article3_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_article4_text]  DEFAULT ('') FOR [article4_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_article5_text]  DEFAULT ('') FOR [article5_text]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_article1_cb]  DEFAULT ((0)) FOR [article1_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_article2_cb]  DEFAULT ((0)) FOR [article2_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_article3_cb]  DEFAULT ((0)) FOR [article3_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_article4_cb]  DEFAULT ((0)) FOR [article4_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_article5_cb]  DEFAULT ((0)) FOR [article5_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom1_subcolumn]  DEFAULT ((0)) FOR [custom1_subcolumn]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom2_subcolumn]  DEFAULT ((0)) FOR [custom2_subcolumn]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom3_subcolumn]  DEFAULT ((0)) FOR [custom3_subcolumn]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom4_subcolumn]  DEFAULT ((0)) FOR [custom4_subcolumn]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom5_subcolumn]  DEFAULT ((0)) FOR [custom5_subcolumn]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom6_subcolumn]  DEFAULT ((0)) FOR [custom6_subcolumn]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom7_subcolumn]  DEFAULT ((0)) FOR [custom7_subcolumn]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom8_subcolumn]  DEFAULT ((0)) FOR [custom8_subcolumn]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom9_subcolumn]  DEFAULT ((0)) FOR [custom9_subcolumn]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_custom10_subcolumn]  DEFAULT ((0)) FOR [custom10_subcolumn]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_file_required]  DEFAULT ((0)) FOR [file_required_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_file_title_required]  DEFAULT ((0)) FOR [file_title_required_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_file_description]  DEFAULT ((0)) FOR [file_description_required_cb]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_required_file_types]  DEFAULT ('') FOR [required_file_types]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_group_id]  DEFAULT ((0)) FOR [group_id]
GO
ALTER TABLE [dbo].[cms_classifications] ADD  CONSTRAINT [DF_cms_classifications_structure_description]  DEFAULT ('') FOR [structure_description]
GO
ALTER TABLE [dbo].[cms_config] ADD  CONSTRAINT [DF_cms_config_config_value_local]  DEFAULT ('') FOR [config_value_local]
GO
ALTER TABLE [dbo].[cms_config] ADD  CONSTRAINT [DF_CmsConfig_config_value_remote]  DEFAULT ('') FOR [config_value_remote]
GO
ALTER TABLE [dbo].[cms_config] ADD  CONSTRAINT [DF_cms_config_isDefault]  DEFAULT ('N') FOR [isDefault]
GO
ALTER TABLE [dbo].[cms_config] ADD  CONSTRAINT [DF_cms_config_updated]  DEFAULT (getdate()) FOR [updated]
GO
ALTER TABLE [dbo].[cms_css] ADD  CONSTRAINT [DF_css_css_name]  DEFAULT ('') FOR [css_name]
GO
ALTER TABLE [dbo].[cms_css] ADD  CONSTRAINT [DF_cms_css_css_status]  DEFAULT ('A') FOR [css_status]
GO
ALTER TABLE [dbo].[cms_css] ADD  CONSTRAINT [DF_cms_css_css_type]  DEFAULT ((0)) FOR [css_type]
GO
ALTER TABLE [dbo].[cms_css] ADD  CONSTRAINT [DF_css_css_code]  DEFAULT ('') FOR [css_code]
GO
ALTER TABLE [dbo].[cms_css] ADD  CONSTRAINT [DF_cms_css_css_fix]  DEFAULT ('') FOR [css_fix]
GO
ALTER TABLE [dbo].[cms_css] ADD  CONSTRAINT [DF_cms_css_css_rel_text]  DEFAULT ('stylesheet') FOR [css_rel_text]
GO
ALTER TABLE [dbo].[cms_css] ADD  CONSTRAINT [DF_cms_css_css_type_text]  DEFAULT ('text/css') FOR [css_type_text]
GO
ALTER TABLE [dbo].[cms_css] ADD  CONSTRAINT [DF_css_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_css] ADD  CONSTRAINT [DF_css_updated]  DEFAULT (getdate()) FOR [updated]
GO
ALTER TABLE [dbo].[cms_css] ADD  CONSTRAINT [DF_cms_css_group_id]  DEFAULT ((0)) FOR [group_id]
GO
ALTER TABLE [dbo].[cms_css] ADD  CONSTRAINT [DF_cms_css_structure_description]  DEFAULT ('') FOR [structure_description]
GO
ALTER TABLE [dbo].[cms_css_revisions] ADD  CONSTRAINT [DF_cms_css_revisions_css_id]  DEFAULT ((0)) FOR [css_id]
GO
ALTER TABLE [dbo].[cms_css_revisions] ADD  CONSTRAINT [DF_cms_css_revisions_css_code]  DEFAULT ('') FOR [css_code]
GO
ALTER TABLE [dbo].[cms_css_revisions] ADD  CONSTRAINT [DF_cms_css_revisions_css_fix]  DEFAULT ('') FOR [css_fix]
GO
ALTER TABLE [dbo].[cms_css_revisions] ADD  CONSTRAINT [DF_cms_css_revisions_css_rel_text]  DEFAULT ('stylesheet') FOR [css_rel_text]
GO
ALTER TABLE [dbo].[cms_css_revisions] ADD  CONSTRAINT [DF_cms_css_revisions_css_type_text]  DEFAULT ('text/css') FOR [css_type_text]
GO
ALTER TABLE [dbo].[cms_css_revisions] ADD  CONSTRAINT [DF_cms_css_revisions_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_css_revisions] ADD  CONSTRAINT [DF_cms_css_revisions_css_type]  DEFAULT ((0)) FOR [css_type]
GO
ALTER TABLE [dbo].[cms_custom_content] ADD  CONSTRAINT [DF_cms_custom_content_cc_name]  DEFAULT ('') FOR [cc_name]
GO
ALTER TABLE [dbo].[cms_custom_content] ADD  CONSTRAINT [DF_cms_custom_content_cc_html]  DEFAULT ('') FOR [cc_html]
GO
ALTER TABLE [dbo].[cms_custom_content] ADD  CONSTRAINT [DF_cms_custom_content_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_custom_content] ADD  CONSTRAINT [DF_cms_custom_content_updated]  DEFAULT (getdate()) FOR [updated]
GO
ALTER TABLE [dbo].[cms_custom_content] ADD  CONSTRAINT [DF_cms_custom_content_group_id]  DEFAULT ((0)) FOR [group_id]
GO
ALTER TABLE [dbo].[cms_custom_content] ADD  CONSTRAINT [DF_cms_custom_content_structure_description]  DEFAULT ('') FOR [structure_description]
GO
ALTER TABLE [dbo].[cms_custom_form] ADD  CONSTRAINT [DF_cms_custom_form_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_domains] ADD  CONSTRAINT [DF_cms_domains_domain_names]  DEFAULT ('') FOR [domain_names]
GO
ALTER TABLE [dbo].[cms_domains] ADD  CONSTRAINT [DF_cms_domains_home_page_article]  DEFAULT ('') FOR [home_page_article]
GO
ALTER TABLE [dbo].[cms_domains] ADD  CONSTRAINT [DF_cms_domains_domain_status]  DEFAULT ('A') FOR [domain_status]
GO
ALTER TABLE [dbo].[cms_domains] ADD  CONSTRAINT [DF_cms_domains_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_domains] ADD  CONSTRAINT [DF_cms_domains_updated]  DEFAULT (getdate()) FOR [updated]
GO
ALTER TABLE [dbo].[cms_domains] ADD  CONSTRAINT [DF_cms_domains_error_page_article]  DEFAULT ('') FOR [error_page_article]
GO
ALTER TABLE [dbo].[cms_error_logs] ADD  CONSTRAINT [DF_cms_error_logs_LogDate]  DEFAULT (getdate()) FOR [LogDate]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file1_name]  DEFAULT ('') FOR [file1_name]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file2_name]  DEFAULT ('') FOR [file2_name]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file3_name]  DEFAULT ('') FOR [file3_name]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file4_name]  DEFAULT ('') FOR [file4_name]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file5_name]  DEFAULT ('') FOR [file5_name]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file1_name1]  DEFAULT ('') FOR [file6_name]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file2_name1]  DEFAULT ('') FOR [file7_name]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file3_name1]  DEFAULT ('') FOR [file8_name]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file4_name1]  DEFAULT ('') FOR [file9_name]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file5_name1]  DEFAULT ('') FOR [file10_name]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file1_extension]  DEFAULT ('') FOR [file1_extension]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file2_extension]  DEFAULT ('') FOR [file2_extension]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file3_extension]  DEFAULT ('') FOR [file3_extension]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file4_extension]  DEFAULT ('') FOR [file4_extension]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file5_extension]  DEFAULT ('') FOR [file5_extension]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file1_extension1]  DEFAULT ('') FOR [file6_extension]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file2_extension1]  DEFAULT ('') FOR [file7_extension]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file3_extension1]  DEFAULT ('') FOR [file8_extension]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file4_extension1]  DEFAULT ('') FOR [file9_extension]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file5_extension1]  DEFAULT ('') FOR [file10_extension]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file1_wh]  DEFAULT ('') FOR [file1_wh]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file2_wh]  DEFAULT ('') FOR [file2_wh]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file3_wh]  DEFAULT ('') FOR [file3_wh]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file4_wh]  DEFAULT ('') FOR [file4_wh]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file5_wh]  DEFAULT ('') FOR [file5_wh]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file1_wh1]  DEFAULT ('') FOR [file6_wh]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file2_wh1]  DEFAULT ('') FOR [file7_wh]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file3_wh1]  DEFAULT ('') FOR [file8_wh]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file4_wh1]  DEFAULT ('') FOR [file9_wh]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file5_wh1]  DEFAULT ('') FOR [file10_wh]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file1_size]  DEFAULT ((0)) FOR [file1_size]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file2_size]  DEFAULT ((0)) FOR [file2_size]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file3_size]  DEFAULT ((0)) FOR [file3_size]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file4_size]  DEFAULT ((0)) FOR [file4_size]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file5_size]  DEFAULT ((0)) FOR [file5_size]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file1_size1]  DEFAULT ((0)) FOR [file6_size]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file2_size1]  DEFAULT ((0)) FOR [file7_size]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file3_size1]  DEFAULT ((0)) FOR [file8_size]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file4_size1]  DEFAULT ((0)) FOR [file9_size]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_file5_size1]  DEFAULT ((0)) FOR [file10_size]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_updated]  DEFAULT (getdate()) FOR [updated]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_group_id]  DEFAULT ((0)) FOR [group_id]
GO
ALTER TABLE [dbo].[cms_file_types] ADD  CONSTRAINT [DF_cms_file_types_structure_description]  DEFAULT ('') FOR [structure_description]
GO
ALTER TABLE [dbo].[cms_fop_failure_log] ADD  CONSTRAINT [DF_cms_fop_failure_log_op_action]  DEFAULT ('') FOR [op_action]
GO
ALTER TABLE [dbo].[cms_fop_failure_log] ADD  CONSTRAINT [DF_cms_fop_failure_log_source_path]  DEFAULT ('') FOR [source_path]
GO
ALTER TABLE [dbo].[cms_fop_failure_log] ADD  CONSTRAINT [DF_cms_fop_failure_log_dest_path]  DEFAULT ('') FOR [dest_path]
GO
ALTER TABLE [dbo].[cms_fop_failure_log] ADD  CONSTRAINT [DF_cms_fop_failure_log_file_name]  DEFAULT ('') FOR [file_name]
GO
ALTER TABLE [dbo].[cms_fop_failure_log] ADD  CONSTRAINT [DF_cms_fop_failure_log_summary]  DEFAULT ('') FOR [summary]
GO
ALTER TABLE [dbo].[cms_fop_failure_log] ADD  CONSTRAINT [DF_cms_fop_failure_log_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_fop_failure_log] ADD  CONSTRAINT [DF_cms_fop_failure_log_retry_count]  DEFAULT ((0)) FOR [retry_count]
GO
ALTER TABLE [dbo].[cms_fop_failure_log] ADD  CONSTRAINT [DF_cms_fop_failure_log_op_status]  DEFAULT ('N') FOR [op_status]
GO
ALTER TABLE [dbo].[cms_hidden_values] ADD  CONSTRAINT [DF_cms_hidden_values_hidden_type]  DEFAULT ((0)) FOR [hidden_type]
GO
ALTER TABLE [dbo].[cms_hidden_values] ADD  CONSTRAINT [DF_cms_hidden_values_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_hidden_values] ADD  CONSTRAINT [DF_cms_hidden_values_updated]  DEFAULT (getdate()) FOR [updated]
GO
ALTER TABLE [dbo].[cms_instant_messaging] ADD  CONSTRAINT [DF_cms_instant_messaging_ims_subject]  DEFAULT ('') FOR [ims_subject]
GO
ALTER TABLE [dbo].[cms_instant_messaging] ADD  CONSTRAINT [DF_cms_instant_messaging_ims_type]  DEFAULT ('ME') FOR [ims_type]
GO
ALTER TABLE [dbo].[cms_instant_messaging] ADD  CONSTRAINT [DF_cms_instant_messaging_related_id]  DEFAULT ((0)) FOR [related_id]
GO
ALTER TABLE [dbo].[cms_instant_messaging] ADD  CONSTRAINT [DF_cms_instant_messaging_related_name]  DEFAULT ('') FOR [related_name]
GO
ALTER TABLE [dbo].[cms_instant_messaging] ADD  CONSTRAINT [DF_cms_instant_messaging_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_languages] ADD  CONSTRAINT [DF_languages_lang_id]  DEFAULT ('TR') FOR [lang_id]
GO
ALTER TABLE [dbo].[cms_languages] ADD  CONSTRAINT [DF_languages_lang_name]  DEFAULT ('') FOR [lang_name]
GO
ALTER TABLE [dbo].[cms_languages] ADD  CONSTRAINT [DF_cms_languages_lang_xml]  DEFAULT ('') FOR [lang_xml]
GO
ALTER TABLE [dbo].[cms_languages] ADD  CONSTRAINT [DF_cms_languages_lang_order]  DEFAULT ((0)) FOR [lang_order]
GO
ALTER TABLE [dbo].[cms_languages] ADD  CONSTRAINT [DF_cms_languages_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_languages] ADD  CONSTRAINT [DF_cms_languages_updated]  DEFAULT (getdate()) FOR [updated]
GO
ALTER TABLE [dbo].[cms_languages] ADD  DEFAULT ('') FOR [lang_alias]
GO
ALTER TABLE [dbo].[cms_page_redirection] ADD  CONSTRAINT [DF_cms_page_redirection_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[cms_page_redirection] ADD  CONSTRAINT [DF_cms_page_redirection_Updated]  DEFAULT (getdate()) FOR [Updated]
GO
ALTER TABLE [dbo].[cms_plugins] ADD  CONSTRAINT [DF_cms_plugins_plugin_status]  DEFAULT ((0)) FOR [plugin_status]
GO
ALTER TABLE [dbo].[cms_plugins] ADD  CONSTRAINT [DF_cms_plugins_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_plugins] ADD  CONSTRAINT [DF_cms_plugins_updated]  DEFAULT (getdate()) FOR [updated]
GO
ALTER TABLE [dbo].[cms_plugins] ADD  CONSTRAINT [DF_cms_plugins_group_id]  DEFAULT ((0)) FOR [group_id]
GO
ALTER TABLE [dbo].[cms_plugins] ADD  CONSTRAINT [DF_cms_plugins_structure_description]  DEFAULT ('') FOR [structure_description]
GO
ALTER TABLE [dbo].[cms_portlets] ADD  CONSTRAINT [DF_cms_portlets_portlet_name]  DEFAULT ('') FOR [portlet_name]
GO
ALTER TABLE [dbo].[cms_portlets] ADD  CONSTRAINT [DF_cms_portlets_portlet_locked]  DEFAULT ((0)) FOR [portlet_status]
GO
ALTER TABLE [dbo].[cms_portlets] ADD  CONSTRAINT [DF_cms_portlets_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_portlets] ADD  CONSTRAINT [DF_cms_portlets_updated]  DEFAULT (getdate()) FOR [updated]
GO
ALTER TABLE [dbo].[cms_portlets] ADD  CONSTRAINT [DF_cms_portlets_portlet_html]  DEFAULT ('') FOR [portlet_html]
GO
ALTER TABLE [dbo].[cms_portlets] ADD  CONSTRAINT [DF_cms_portlets_portlet_css]  DEFAULT ('') FOR [portlet_css]
GO
ALTER TABLE [dbo].[cms_portlets] ADD  CONSTRAINT [DF_cms_portlets_editor_type]  DEFAULT ((0)) FOR [editor_type]
GO
ALTER TABLE [dbo].[cms_portlets] ADD  CONSTRAINT [DF_cms_portlets_header]  DEFAULT ('') FOR [portlet_header]
GO
ALTER TABLE [dbo].[cms_portlets] ADD  CONSTRAINT [DF_cms_portlets_footer]  DEFAULT ('') FOR [portlet_footer]
GO
ALTER TABLE [dbo].[cms_portlets] ADD  CONSTRAINT [DF_cms_portlets_group_id]  DEFAULT ((0)) FOR [group_id]
GO
ALTER TABLE [dbo].[cms_portlets] ADD  CONSTRAINT [DF_cms_portlets_structure_description]  DEFAULT ('') FOR [structure_description]
GO
ALTER TABLE [dbo].[cms_portlets] ADD  CONSTRAINT [DF_cms_portlets_content_editor_type]  DEFAULT ('H') FOR [content_editor_type]
GO
ALTER TABLE [dbo].[cms_portlets] ADD  CONSTRAINT [DF_cms_portlets_enable_shortcut]  DEFAULT ('Y') FOR [enable_shortcut]
GO
ALTER TABLE [dbo].[cms_publisher_log_events] ADD  CONSTRAINT [DF_cms_publisher_log_events_event_name]  DEFAULT ('') FOR [event_name]
GO
ALTER TABLE [dbo].[cms_publisher_log_events] ADD  CONSTRAINT [DF_cms_publisher_log_events_event_description]  DEFAULT ('') FOR [event_description]
GO
ALTER TABLE [dbo].[cms_publisher_log_events] ADD  CONSTRAINT [DF_cms_publisher_log_events_event_type]  DEFAULT ((0)) FOR [event_type]
GO
ALTER TABLE [dbo].[cms_publisher_log_events] ADD  CONSTRAINT [DF_cms_publisher_log_events_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_publisher_logs] ADD  CONSTRAINT [DF_cms_publisher_logs_note_id]  DEFAULT ((0)) FOR [note_id]
GO
ALTER TABLE [dbo].[cms_publisher_logs] ADD  CONSTRAINT [DF_cms_publisher_logs_title]  DEFAULT ('') FOR [title]
GO
ALTER TABLE [dbo].[cms_publisher_logs] ADD  CONSTRAINT [DF_publisher_log_log_note]  DEFAULT ('') FOR [note]
GO
ALTER TABLE [dbo].[cms_publisher_logs] ADD  CONSTRAINT [DF_cms_publisher_logs_ip]  DEFAULT ('') FOR [ip]
GO
ALTER TABLE [dbo].[cms_publisher_logs] ADD  CONSTRAINT [DF_cms_publisher_logs_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_redirects] ADD  CONSTRAINT [DF_cms_redirects_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_redirects] ADD  CONSTRAINT [DF_cms_redirects_updated]  DEFAULT (getdate()) FOR [updated]
GO
ALTER TABLE [dbo].[cms_redirects] ADD  CONSTRAINT [DF_cms_redirects_group_id]  DEFAULT ((0)) FOR [group_id]
GO
ALTER TABLE [dbo].[cms_redirects] ADD  CONSTRAINT [DF_cms_redirects_structure_description]  DEFAULT ('') FOR [structure_description]
GO
ALTER TABLE [dbo].[cms_redirects] ADD  CONSTRAINT [DF_cms_redirects_permanent_redirection]  DEFAULT ((0)) FOR [permanent_redirection]
GO
ALTER TABLE [dbo].[cms_relations] ADD  CONSTRAINT [DF_cms_relations_rel_type]  DEFAULT ((0)) FOR [rel_type]
GO
ALTER TABLE [dbo].[cms_relations] ADD  CONSTRAINT [DF_cms_zone_article_relations_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_rss_channels] ADD  CONSTRAINT [DF_cms_rss_channels_channel_status]  DEFAULT ('A') FOR [channel_status]
GO
ALTER TABLE [dbo].[cms_rss_channels] ADD  CONSTRAINT [DF_cms_rss_channels_channel_name]  DEFAULT ('') FOR [channel_name]
GO
ALTER TABLE [dbo].[cms_rss_channels] ADD  CONSTRAINT [DF_cms_rss_channels_url]  DEFAULT ('') FOR [url]
GO
ALTER TABLE [dbo].[cms_rss_channels] ADD  CONSTRAINT [DF_cms_rss_channels_description]  DEFAULT ('') FOR [description]
GO
ALTER TABLE [dbo].[cms_rss_channels] ADD  CONSTRAINT [DF_cms_rss_channels_lang_id]  DEFAULT ('') FOR [lang_id]
GO
ALTER TABLE [dbo].[cms_rss_channels] ADD  CONSTRAINT [DF_cms_rss_channels_managingEditor]  DEFAULT ('') FOR [managing_editor]
GO
ALTER TABLE [dbo].[cms_rss_channels] ADD  CONSTRAINT [DF_cms_rss_channels_copyright]  DEFAULT ('') FOR [copyright]
GO
ALTER TABLE [dbo].[cms_rss_channels] ADD  CONSTRAINT [DF_cms_rss_channels_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_rss_channels] ADD  CONSTRAINT [DF_cms_rss_channels_updated]  DEFAULT (getdate()) FOR [updated]
GO
ALTER TABLE [dbo].[cms_rss_channels] ADD  CONSTRAINT [DF_cms_rss_channels_group_id]  DEFAULT ((0)) FOR [group_id]
GO
ALTER TABLE [dbo].[cms_rss_channels] ADD  CONSTRAINT [DF_cms_rss_channels_structure_description]  DEFAULT ('') FOR [structure_description]
GO
ALTER TABLE [dbo].[cms_rss_channels] ADD  CONSTRAINT [DF_cms_rss_channels_content_area]  DEFAULT ('summary') FOR [summary_content_field]
GO
ALTER TABLE [dbo].[cms_rss_channels] ADD  CONSTRAINT [DF_cms_rss_channels_content_template]  DEFAULT ('##article_1##') FOR [content_template]
GO
ALTER TABLE [dbo].[cms_rss_channels] ADD  CONSTRAINT [DF_cms_rss_channels_content_template_editor_type]  DEFAULT ('H') FOR [content_template_editor_type]
GO
ALTER TABLE [dbo].[cms_rss_channels] ADD  CONSTRAINT [DF_cms_rss_channels_singularize_articles]  DEFAULT ('N') FOR [singularize_articles]
GO
ALTER TABLE [dbo].[cms_rss_content] ADD  CONSTRAINT [DF_cms_rss_zones_sgz_type]  DEFAULT ('') FOR [sgz_type]
GO
ALTER TABLE [dbo].[cms_rss_content] ADD  CONSTRAINT [DF_cms_rss_content_sgz_exclude]  DEFAULT ('I') FOR [sgz_exclude]
GO
ALTER TABLE [dbo].[cms_rss_content] ADD  CONSTRAINT [DF_cms_rss_zones_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_search_log] ADD  CONSTRAINT [DF_cms_search_log_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_search_log] ADD  CONSTRAINT [DF_cms_search_log_server_ip]  DEFAULT ('') FOR [server_ip]
GO
ALTER TABLE [dbo].[cms_search_log] ADD  CONSTRAINT [DF_cms_search_log_ip]  DEFAULT ('') FOR [client_ip]
GO
ALTER TABLE [dbo].[cms_search_log] ADD  CONSTRAINT [DF_cms_search_log_search_query]  DEFAULT ('') FOR [search_query]
GO
ALTER TABLE [dbo].[cms_search_log] ADD  CONSTRAINT [DF_cms_search_log_search_in]  DEFAULT ('') FOR [search_in]
GO
ALTER TABLE [dbo].[cms_search_log] ADD  CONSTRAINT [DF_cms_search_log_result_count]  DEFAULT ((0)) FOR [result_count]
GO
ALTER TABLE [dbo].[cms_sitemaps] ADD  CONSTRAINT [DF_cms_sitemaps_DOMAIN_IE]  DEFAULT ((0)) FOR [domain_id]
GO
ALTER TABLE [dbo].[cms_sitemaps] ADD  CONSTRAINT [DF_cms_sitemaps_domain_alias]  DEFAULT ('') FOR [domain_alias]
GO
ALTER TABLE [dbo].[cms_sitemaps] ADD  CONSTRAINT [DF_cms_sitemaps_STATUS]  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[cms_sitemaps] ADD  CONSTRAINT [DF_cms_sitemaps_NOTIFY_GOOGLE]  DEFAULT ('Y') FOR [notify_google]
GO
ALTER TABLE [dbo].[cms_sitemaps] ADD  CONSTRAINT [DF_cms_sitemaps_NOTIFY_MSN]  DEFAULT ('Y') FOR [notify_msn]
GO
ALTER TABLE [dbo].[cms_sitemaps] ADD  CONSTRAINT [DF_cms_sitemaps_NOTIFY_ASK]  DEFAULT ('Y') FOR [notify_ask]
GO
ALTER TABLE [dbo].[cms_sitemaps] ADD  CONSTRAINT [DF_cms_sitemaps_NOTIFY_YAHOO]  DEFAULT ('Y') FOR [notify_yahoo]
GO
ALTER TABLE [dbo].[cms_sitemaps] ADD  CONSTRAINT [DF_cms_sitemaps_YAHOO_ID]  DEFAULT ('') FOR [yahoo_id]
GO
ALTER TABLE [dbo].[cms_sitemaps] ADD  CONSTRAINT [DF_cms_sitemaps_EXCLUDED_SITES]  DEFAULT ('') FOR [included_sites]
GO
ALTER TABLE [dbo].[cms_sitemaps] ADD  CONSTRAINT [DF_cms_sitemaps_EXCLUDED_ZONEGROUPS]  DEFAULT ('') FOR [excluded_zonegroups]
GO
ALTER TABLE [dbo].[cms_sitemaps] ADD  CONSTRAINT [DF_cms_sitemaps_EXCLUDED_ZONES]  DEFAULT ('') FOR [excluded_zones]
GO
ALTER TABLE [dbo].[cms_sitemaps] ADD  CONSTRAINT [DF_cms_sitemaps_EXCLUDED_ARTICLES]  DEFAULT ('') FOR [excluded_articles]
GO
ALTER TABLE [dbo].[cms_sitemaps] ADD  CONSTRAINT [DF_cms_sitemaps_AFILES]  DEFAULT ('N') FOR [afiles]
GO
ALTER TABLE [dbo].[cms_sitemaps] ADD  CONSTRAINT [DF_cms_sitemaps_INTERVAL]  DEFAULT ((24)) FOR [interval]
GO
ALTER TABLE [dbo].[cms_sitemaps] ADD  CONSTRAINT [DF_cms_sitemaps_ENABLED]  DEFAULT ('Y') FOR [enabled]
GO
ALTER TABLE [dbo].[cms_sitemaps] ADD  CONSTRAINT [DF_cms_sitemaps_XML]  DEFAULT ('') FOR [xml]
GO
ALTER TABLE [dbo].[cms_sitemaps] ADD  CONSTRAINT [DF_cms_sitemaps_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_sitemaps] ADD  CONSTRAINT [DF_cms_sitemaps_gzip_enabled]  DEFAULT ('N') FOR [gzip_enabled]
GO
ALTER TABLE [dbo].[cms_sites] ADD  CONSTRAINT [DF_cms_sites_css_id]  DEFAULT ((0)) FOR [css_id]
GO
ALTER TABLE [dbo].[cms_sites] ADD  CONSTRAINT [DF_cms_sites_css_id_mobile]  DEFAULT ((0)) FOR [css_id_mobile]
GO
ALTER TABLE [dbo].[cms_sites] ADD  CONSTRAINT [DF_cms_sites_css_id_print]  DEFAULT ((0)) FOR [css_id_print]
GO
ALTER TABLE [dbo].[cms_sites] ADD  CONSTRAINT [DF_cms_sites_template_id]  DEFAULT ((0)) FOR [template_id]
GO
ALTER TABLE [dbo].[cms_sites] ADD  CONSTRAINT [DF_cms_sites_template_id_mobile]  DEFAULT ((0)) FOR [template_id_mobile]
GO
ALTER TABLE [dbo].[cms_sites] ADD  CONSTRAINT [DF_cms_sites_site_keywords]  DEFAULT ('') FOR [site_keywords]
GO
ALTER TABLE [dbo].[cms_sites] ADD  CONSTRAINT [DF_cms_sites_site_header]  DEFAULT ('') FOR [site_header]
GO
ALTER TABLE [dbo].[cms_sites] ADD  CONSTRAINT [DF_cms_sites_site_js]  DEFAULT ('') FOR [site_js]
GO
ALTER TABLE [dbo].[cms_sites] ADD  CONSTRAINT [DF_cms_sites_custom_body]  DEFAULT ('') FOR [custom_body]
GO
ALTER TABLE [dbo].[cms_sites] ADD  CONSTRAINT [DF_cms_sites_site_icon]  DEFAULT ('') FOR [site_icon]
GO
ALTER TABLE [dbo].[cms_sites] ADD  CONSTRAINT [DF_cms_sites_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_sites] ADD  CONSTRAINT [DF_cms_sites_updated]  DEFAULT (getdate()) FOR [updated]
GO
ALTER TABLE [dbo].[cms_sites] ADD  CONSTRAINT [DF_cms_sites_article_1]  DEFAULT ('') FOR [article_1]
GO
ALTER TABLE [dbo].[cms_sites] ADD  CONSTRAINT [DF_cms_sites_article_2]  DEFAULT ('') FOR [article_2]
GO
ALTER TABLE [dbo].[cms_sites] ADD  CONSTRAINT [DF_cms_sites_article_3]  DEFAULT ('') FOR [article_3]
GO
ALTER TABLE [dbo].[cms_sites] ADD  CONSTRAINT [DF_cms_sites_article_4]  DEFAULT ('') FOR [article_4]
GO
ALTER TABLE [dbo].[cms_sites] ADD  CONSTRAINT [DF_cms_sites_article_5]  DEFAULT ('') FOR [article_5]
GO
ALTER TABLE [dbo].[cms_sites] ADD  CONSTRAINT [DF_cms_sites_analytics]  DEFAULT ('') FOR [analytics]
GO
ALTER TABLE [dbo].[cms_sites] ADD  CONSTRAINT [DF_cms_sites_tag_detail_article]  DEFAULT ('') FOR [tag_detail_article]
GO
ALTER TABLE [dbo].[cms_sites] ADD  CONSTRAINT [DF_cms_sites_structure_description]  DEFAULT ('') FOR [structure_description]
GO
ALTER TABLE [dbo].[cms_sites] ADD  CONSTRAINT [DF_cms_sites_meta_description]  DEFAULT ('') FOR [meta_description]
GO
ALTER TABLE [dbo].[cms_sites] ADD  CONSTRAINT [DF_cms_sites_content_1_editor_type]  DEFAULT ('H') FOR [content_1_editor_type]
GO
ALTER TABLE [dbo].[cms_sites] ADD  CONSTRAINT [DF_cms_sites_content_2_editor_type]  DEFAULT ('H') FOR [content_2_editor_type]
GO
ALTER TABLE [dbo].[cms_sites] ADD  CONSTRAINT [DF_cms_sites_content_3_editor_type]  DEFAULT ('H') FOR [content_3_editor_type]
GO
ALTER TABLE [dbo].[cms_sites] ADD  CONSTRAINT [DF_cms_sites_content_4_editor_type]  DEFAULT ('H') FOR [content_4_editor_type]
GO
ALTER TABLE [dbo].[cms_sites] ADD  CONSTRAINT [DF_cms_sites_content_5_editor_type]  DEFAULT ('H') FOR [content_5_editor_type]
GO
ALTER TABLE [dbo].[cms_sites] ADD  CONSTRAINT [DF_cms_sites_default_article]  DEFAULT ('') FOR [default_article]
GO
ALTER TABLE [dbo].[cms_sites] ADD  CONSTRAINT [DF_cms_sites_omniture_code]  DEFAULT ('') FOR [omniture_code]
GO
ALTER TABLE [dbo].[cms_sites] ADD  DEFAULT ('') FOR [site_alias]
GO
ALTER TABLE [dbo].[cms_stf_emails] ADD  CONSTRAINT [DF_cms_stf_emails_from_name]  DEFAULT ('') FOR [from_name]
GO
ALTER TABLE [dbo].[cms_stf_emails] ADD  CONSTRAINT [DF_cms_stf_emails_from_email]  DEFAULT ('') FOR [from_email]
GO
ALTER TABLE [dbo].[cms_stf_emails] ADD  CONSTRAINT [DF_cms_stf_emails_from_ip]  DEFAULT ('') FOR [from_ip]
GO
ALTER TABLE [dbo].[cms_stf_emails] ADD  CONSTRAINT [DF_cms_stf_emails_to_name]  DEFAULT ('') FOR [to_name]
GO
ALTER TABLE [dbo].[cms_stf_emails] ADD  CONSTRAINT [DF_cms_stf_emails_to_email]  DEFAULT ('') FOR [to_email]
GO
ALTER TABLE [dbo].[cms_stf_emails] ADD  CONSTRAINT [DF_cms_stf_emails_to_note]  DEFAULT ('') FOR [to_note]
GO
ALTER TABLE [dbo].[cms_stf_emails] ADD  CONSTRAINT [DF_cms_stf_emails_stft_id]  DEFAULT ((0)) FOR [stft_id]
GO
ALTER TABLE [dbo].[cms_stf_emails] ADD  CONSTRAINT [DF_cms_stf_emails_zone_id]  DEFAULT ((0)) FOR [zone_id]
GO
ALTER TABLE [dbo].[cms_stf_emails] ADD  CONSTRAINT [DF_cms_stf_emails_article_id]  DEFAULT ((0)) FOR [article_id]
GO
ALTER TABLE [dbo].[cms_stf_emails] ADD  CONSTRAINT [DF_cms_stf_emails_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_stf_templates] ADD  CONSTRAINT [DF_cms_stf_templates_stft_status]  DEFAULT ('A') FOR [stft_status]
GO
ALTER TABLE [dbo].[cms_stf_templates] ADD  CONSTRAINT [DF_cms_send_to_friend_templates_stft_name]  DEFAULT ('') FOR [stft_name]
GO
ALTER TABLE [dbo].[cms_stf_templates] ADD  CONSTRAINT [DF_cms_send_to_friend_templates_stft_form_html]  DEFAULT ('') FOR [stft_form_html]
GO
ALTER TABLE [dbo].[cms_stf_templates] ADD  CONSTRAINT [DF_cms_stf_templates_stft_thanks]  DEFAULT ('') FOR [stft_thanks]
GO
ALTER TABLE [dbo].[cms_stf_templates] ADD  CONSTRAINT [DF_cms_send_to_friend_templates_stft_mail_html]  DEFAULT ('') FOR [stft_mail_html]
GO
ALTER TABLE [dbo].[cms_stf_templates] ADD  CONSTRAINT [DF_cms_stf_templates_stft_mail_from_name]  DEFAULT ('') FOR [stft_mail_from_name]
GO
ALTER TABLE [dbo].[cms_stf_templates] ADD  CONSTRAINT [DF_cms_send_to_friend_templates_stft_mail_subject]  DEFAULT ('') FOR [stft_mail_subject]
GO
ALTER TABLE [dbo].[cms_stf_templates] ADD  CONSTRAINT [DF_cms_stf_templates_stft_wh]  DEFAULT ('') FOR [stft_wh]
GO
ALTER TABLE [dbo].[cms_stf_templates] ADD  CONSTRAINT [DF_cms_send_to_friend_templates_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_stf_templates] ADD  CONSTRAINT [DF_cms_send_to_friend_templates_updated]  DEFAULT (getdate()) FOR [updated]
GO
ALTER TABLE [dbo].[cms_stf_templates] ADD  CONSTRAINT [DF_cms_stf_templates_omniture_function]  DEFAULT ('') FOR [omniture_function]
GO
ALTER TABLE [dbo].[cms_structure_groups] ADD  CONSTRAINT [DF_cms_structure_groups_group_type]  DEFAULT ((0)) FOR [group_type]
GO
ALTER TABLE [dbo].[cms_structure_groups] ADD  CONSTRAINT [DF_cms_structure_groups_group_name]  DEFAULT ('') FOR [group_name]
GO
ALTER TABLE [dbo].[cms_structure_groups] ADD  CONSTRAINT [DF_cms_structure_groups_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_template_revisions] ADD  CONSTRAINT [DF_cms_template_revisions_template_id]  DEFAULT ((0)) FOR [template_id]
GO
ALTER TABLE [dbo].[cms_template_revisions] ADD  CONSTRAINT [DF_cms_template_revisions_template_html]  DEFAULT ('') FOR [template_html]
GO
ALTER TABLE [dbo].[cms_template_revisions] ADD  CONSTRAINT [DF_cms_template_revisions_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_template_revisions] ADD  CONSTRAINT [DF_cms_template_revisions_template_type]  DEFAULT ((0)) FOR [template_type]
GO
ALTER TABLE [dbo].[cms_template_revisions] ADD  CONSTRAINT [DF_cms_template_revisions_content_1_editor_type]  DEFAULT ('H') FOR [content_1_editor_type]
GO
ALTER TABLE [dbo].[cms_template_revisions] ADD  CONSTRAINT [DF_cms_template_revisions_template_doctype]  DEFAULT ('') FOR [template_doctype]
GO
ALTER TABLE [dbo].[cms_templates] ADD  CONSTRAINT [DF_cms_templates_template_status]  DEFAULT ('A') FOR [template_status]
GO
ALTER TABLE [dbo].[cms_templates] ADD  CONSTRAINT [DF_cms_templates_template_type]  DEFAULT ((0)) FOR [template_type]
GO
ALTER TABLE [dbo].[cms_templates] ADD  CONSTRAINT [DF_templates_template_name]  DEFAULT ('') FOR [template_name]
GO
ALTER TABLE [dbo].[cms_templates] ADD  CONSTRAINT [DF_templates_template_html]  DEFAULT ('') FOR [template_html]
GO
ALTER TABLE [dbo].[cms_templates] ADD  CONSTRAINT [DF_templates_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_templates] ADD  CONSTRAINT [DF_templates_updated]  DEFAULT (getdate()) FOR [updated]
GO
ALTER TABLE [dbo].[cms_templates] ADD  CONSTRAINT [DF_cms_templates_group_id]  DEFAULT ((0)) FOR [group_id]
GO
ALTER TABLE [dbo].[cms_templates] ADD  CONSTRAINT [DF_cms_templates_structure_description]  DEFAULT ('') FOR [structure_description]
GO
ALTER TABLE [dbo].[cms_templates] ADD  CONSTRAINT [DF_cms_templates_content_1_editor_type]  DEFAULT ('H') FOR [content_1_editor_type]
GO
ALTER TABLE [dbo].[cms_templates] ADD  CONSTRAINT [DF_cms_templates_template_doctype]  DEFAULT ('') FOR [template_doctype]
GO
ALTER TABLE [dbo].[cms_url_structure] ADD  CONSTRAINT [DF_cms_url_structure_DOMAINID]  DEFAULT ((0)) FOR [DomainId]
GO
ALTER TABLE [dbo].[cms_url_structure] ADD  CONSTRAINT [DF_cms_url_structure_STRUCTUREID]  DEFAULT ((0)) FOR [StructureTypeId]
GO
ALTER TABLE [dbo].[cms_url_structure] ADD  CONSTRAINT [DF_cms_url_structure_STRUCTURE]  DEFAULT ('') FOR [Structure]
GO
ALTER TABLE [dbo].[cms_url_structure] ADD  CONSTRAINT [DF_cms_url_structure_Prefix]  DEFAULT ('') FOR [Prefix]
GO
ALTER TABLE [dbo].[cms_url_structure] ADD  CONSTRAINT [DF_cms_url_structure_IsProtect]  DEFAULT ((0)) FOR [IsProtect]
GO
ALTER TABLE [dbo].[cms_url_structure] ADD  CONSTRAINT [DF_cms_url_structure_UPDATEDATE]  DEFAULT (getdate()) FOR [UpdateDate]
GO
ALTER TABLE [dbo].[cms_widget_configs] ADD  CONSTRAINT [DF_cms_widget_configs_ParamKey]  DEFAULT ('') FOR [ParamKey]
GO
ALTER TABLE [dbo].[cms_widget_configs] ADD  CONSTRAINT [DF_cms_widget_configs_ParamValue]  DEFAULT ('') FOR [ParamValue]
GO
ALTER TABLE [dbo].[cms_widget_configs] ADD  CONSTRAINT [DF_cms_widget_configs_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[cms_widget_users] ADD  CONSTRAINT [DF_cms_widget_users_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[cms_xml] ADD  CONSTRAINT [DF_cms_xml_xml_name]  DEFAULT ('') FOR [xml_name]
GO
ALTER TABLE [dbo].[cms_xml] ADD  CONSTRAINT [DF_cms_xml_xml_main_node]  DEFAULT ('') FOR [xml_main_node]
GO
ALTER TABLE [dbo].[cms_xml] ADD  CONSTRAINT [DF_cms_xml_xml_main_node_attrib]  DEFAULT ('') FOR [xml_main_node_attrib]
GO
ALTER TABLE [dbo].[cms_xml] ADD  CONSTRAINT [DF_cms_xml_xml_per_node]  DEFAULT ('') FOR [xml_per_node]
GO
ALTER TABLE [dbo].[cms_xml] ADD  CONSTRAINT [DF_cms_xml_xml_per_node_attrib]  DEFAULT ('') FOR [xml_per_node_attrib]
GO
ALTER TABLE [dbo].[cms_xml] ADD  CONSTRAINT [DF_cms_xml_xml_sub_node]  DEFAULT ('') FOR [xml_sub_node]
GO
ALTER TABLE [dbo].[cms_xml] ADD  CONSTRAINT [DF_cms_xml_xml_sub_template]  DEFAULT ((0)) FOR [xml_sub_template]
GO
ALTER TABLE [dbo].[cms_xml] ADD  CONSTRAINT [DF_cms_xml_xml_level]  DEFAULT ((1)) FOR [xml_level]
GO
ALTER TABLE [dbo].[cms_xml] ADD  CONSTRAINT [DF_cms_xml_xml_related_line]  DEFAULT ('') FOR [xml_related_line]
GO
ALTER TABLE [dbo].[cms_xml] ADD  CONSTRAINT [DF_cms_xml_xml_xml]  DEFAULT ('') FOR [xml_xml]
GO
ALTER TABLE [dbo].[cms_xml] ADD  CONSTRAINT [DF_cms_xml_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_xml] ADD  CONSTRAINT [DF_cms_xml_group_id]  DEFAULT ((0)) FOR [group_id]
GO
ALTER TABLE [dbo].[cms_xml] ADD  CONSTRAINT [DF_cms_xml_structure_description]  DEFAULT ('') FOR [structure_description]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_zone_group_keywords]  DEFAULT ('') FOR [zone_group_keywords]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_site_id]  DEFAULT ((0)) FOR [site_id]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_css_merge]  DEFAULT ((0)) FOR [css_merge]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_css_id]  DEFAULT ((0)) FOR [css_id]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_css_id_mobile]  DEFAULT ((0)) FOR [css_id_mobile]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_css_id_print]  DEFAULT ((0)) FOR [css_id_print]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_template_id]  DEFAULT ((0)) FOR [template_id]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_template_id_mobile]  DEFAULT ((0)) FOR [template_id_mobile]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_custom_body]  DEFAULT ('') FOR [custom_body]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_updated]  DEFAULT (getdate()) FOR [updated]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_article_1]  DEFAULT ('') FOR [article_1]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_article_2]  DEFAULT ('') FOR [article_2]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_article_3]  DEFAULT ('') FOR [article_3]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_article_4]  DEFAULT ('') FOR [article_4]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_article_5]  DEFAULT ('') FOR [article_5]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_append_1]  DEFAULT ((0)) FOR [append_1]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_append_2]  DEFAULT ((0)) FOR [append_2]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_append_3]  DEFAULT ((0)) FOR [append_3]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_append_4]  DEFAULT ((0)) FOR [append_4]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_append_5]  DEFAULT ((0)) FOR [append_5]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_analytics]  DEFAULT ('') FOR [analytics]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_tag_detail_article]  DEFAULT ('') FOR [tag_detail_article]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_meta_description]  DEFAULT ('') FOR [meta_description]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_zone_group_name_display]  DEFAULT ('') FOR [zone_group_name_display]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_content_1_editor_type]  DEFAULT ('H') FOR [content_1_editor_type]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_content_2_editor_type]  DEFAULT ('H') FOR [content_2_editor_type]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_content_3_editor_type]  DEFAULT ('H') FOR [content_3_editor_type]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_content_4_editor_type]  DEFAULT ('H') FOR [content_4_editor_type]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_content_5_editor_type]  DEFAULT ('H') FOR [content_5_editor_type]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_default_article]  DEFAULT ('') FOR [default_article]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  CONSTRAINT [DF_cms_zone_groups_omniture_code]  DEFAULT ('') FOR [omniture_code]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  DEFAULT ('') FOR [before_head]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  DEFAULT ('') FOR [before_body]
GO
ALTER TABLE [dbo].[cms_zone_groups] ADD  DEFAULT ('') FOR [zg_alias]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_rev_date]  DEFAULT (getdate()) FOR [rev_date]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_revision_status]  DEFAULT ('N') FOR [revision_status]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_rev_name]  DEFAULT ('') FOR [rev_name]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_rev_note]  DEFAULT ('') FOR [rev_note]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_zone_group_id]  DEFAULT ((0)) FOR [zone_group_id]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_zone_type_id]  DEFAULT ((0)) FOR [zone_type_id]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_zone_status]  DEFAULT ('A') FOR [zone_status]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_zone_name]  DEFAULT ('') FOR [zone_name]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_zone_desc]  DEFAULT ('') FOR [zone_desc]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_css_merge]  DEFAULT ((0)) FOR [css_merge]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_css_id]  DEFAULT ((0)) FOR [css_id]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_css_id_mobile]  DEFAULT ((0)) FOR [css_id_mobile]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_css_id_print]  DEFAULT ((0)) FOR [css_id_print]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_template_id]  DEFAULT ((0)) FOR [template_id]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_template_id_mobile]  DEFAULT ((0)) FOR [template_id_mobile]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_custom_body]  DEFAULT ('') FOR [custom_body]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_zone_keywords]  DEFAULT ('') FOR [zone_keywords]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_article_1]  DEFAULT ('') FOR [article_1]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_article_2]  DEFAULT ('') FOR [article_2]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_article_3]  DEFAULT ('') FOR [article_3]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_article_4]  DEFAULT ('') FOR [article_4]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_article_5]  DEFAULT ('') FOR [article_5]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_append_1]  DEFAULT ((0)) FOR [append_1]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_append_2]  DEFAULT ((0)) FOR [append_2]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_append_3]  DEFAULT ((0)) FOR [append_3]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_append_4]  DEFAULT ((0)) FOR [append_4]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_append_5]  DEFAULT ((0)) FOR [append_5]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_analytics]  DEFAULT ('') FOR [analytics]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_meta_description]  DEFAULT ('') FOR [meta_description]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_zone_name_display_1]  DEFAULT ('') FOR [zone_name_display]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_content_1_editor_type]  DEFAULT ('H') FOR [content_1_editor_type]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_content_2_editor_type]  DEFAULT ('H') FOR [content_2_editor_type]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_content_3_editor_type]  DEFAULT ('H') FOR [content_3_editor_type]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_content_4_editor_type]  DEFAULT ('H') FOR [content_4_editor_type]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_content_5_editor_type]  DEFAULT ('H') FOR [content_5_editor_type]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_default_article]  DEFAULT ('') FOR [default_article]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_omniture_code]  DEFAULT ('') FOR [omniture_code]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  CONSTRAINT [DF_cms_zone_revision_lang_id]  DEFAULT ('TR') FOR [lang_id]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  DEFAULT ('') FOR [before_head]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  DEFAULT ('') FOR [before_body]
GO
ALTER TABLE [dbo].[cms_zone_revision] ADD  DEFAULT ('') FOR [zone_alias]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_zone_group_id]  DEFAULT ((0)) FOR [zone_group_id]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_zone_type_id]  DEFAULT ((0)) FOR [zone_type_id]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_zone_status]  DEFAULT ('N') FOR [zone_status]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_zone_desc]  DEFAULT ('') FOR [zone_desc]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_css_type]  DEFAULT ((0)) FOR [css_merge]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_css_id]  DEFAULT ((0)) FOR [css_id]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_css_id_mobile]  DEFAULT ((0)) FOR [css_id_mobile]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_css_id_print]  DEFAULT ((0)) FOR [css_id_print]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_template_id]  DEFAULT ((0)) FOR [template_id]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_template_id_mobile]  DEFAULT ((0)) FOR [template_id_mobile]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_custom_body]  DEFAULT ('') FOR [custom_body]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_zone_keywords]  DEFAULT ('') FOR [zone_keywords]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_article_1]  DEFAULT ('') FOR [article_1]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_article_2]  DEFAULT ('') FOR [article_2]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_article_3]  DEFAULT ('') FOR [article_3]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_article_4]  DEFAULT ('') FOR [article_4]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_article_5]  DEFAULT ('') FOR [article_5]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_append_1]  DEFAULT ((0)) FOR [append_1]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_append_2]  DEFAULT ((0)) FOR [append_2]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_append_3]  DEFAULT ((0)) FOR [append_3]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_append_4]  DEFAULT ((0)) FOR [append_4]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_append_5]  DEFAULT ((0)) FOR [append_5]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_updated]  DEFAULT (getdate()) FOR [updated]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_analytics]  DEFAULT ('') FOR [analytics]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_meta_description]  DEFAULT ('') FOR [meta_description]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_zone_name_display]  DEFAULT ('') FOR [zone_name_display]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_default_article]  DEFAULT ('') FOR [default_article]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_omniture_code]  DEFAULT ('') FOR [omniture_code]
GO
ALTER TABLE [dbo].[cms_zones] ADD  CONSTRAINT [DF_cms_zones_lang_id]  DEFAULT ('TR') FOR [lang_id]
GO
ALTER TABLE [dbo].[cms_zones] ADD  DEFAULT ('') FOR [before_head]
GO
ALTER TABLE [dbo].[cms_zones] ADD  DEFAULT ('') FOR [before_body]
GO
ALTER TABLE [dbo].[cms_zones] ADD  DEFAULT ('') FOR [zone_alias]
GO
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[aspnet_Paths]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
ALTER TABLE [dbo].[aspnet_PersonalizationAllUsers]  WITH CHECK ADD FOREIGN KEY([PathId])
REFERENCES [dbo].[aspnet_Paths] ([PathId])
GO
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser]  WITH CHECK ADD FOREIGN KEY([PathId])
REFERENCES [dbo].[aspnet_Paths] ([PathId])
GO
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[aspnet_Profile]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
ALTER TABLE [dbo].[aspnet_Users]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[cms_article_files]  WITH CHECK ADD  CONSTRAINT [FK_cms_article_files_cms_articles] FOREIGN KEY([article_id])
REFERENCES [dbo].[cms_articles] ([article_id])
GO
ALTER TABLE [dbo].[cms_article_files] CHECK CONSTRAINT [FK_cms_article_files_cms_articles]
GO
ALTER TABLE [dbo].[cms_article_files_revision_files]  WITH CHECK ADD  CONSTRAINT [FK_cms_article_files_revision_files_cms_article_files_revision] FOREIGN KEY([rev_id])
REFERENCES [dbo].[cms_article_files_revision] ([rev_id])
GO
ALTER TABLE [dbo].[cms_article_files_revision_files] CHECK CONSTRAINT [FK_cms_article_files_revision_files_cms_article_files_revision]
GO
ALTER TABLE [dbo].[cms_article_revision]  WITH CHECK ADD  CONSTRAINT [FK_cms_article_revision_cms_articles] FOREIGN KEY([article_id])
REFERENCES [dbo].[cms_articles] ([article_id])
GO
ALTER TABLE [dbo].[cms_article_revision] CHECK CONSTRAINT [FK_cms_article_revision_cms_articles]
GO
ALTER TABLE [dbo].[cms_article_zones]  WITH CHECK ADD  CONSTRAINT [FK_cms_article_zones_cms_articles] FOREIGN KEY([article_id])
REFERENCES [dbo].[cms_articles] ([article_id])
GO
ALTER TABLE [dbo].[cms_article_zones] CHECK CONSTRAINT [FK_cms_article_zones_cms_articles]
GO
ALTER TABLE [dbo].[cms_article_zones]  WITH CHECK ADD  CONSTRAINT [FK_cms_article_zones_cms_zones] FOREIGN KEY([zone_id])
REFERENCES [dbo].[cms_zones] ([zone_id])
GO
ALTER TABLE [dbo].[cms_article_zones] CHECK CONSTRAINT [FK_cms_article_zones_cms_zones]
GO
ALTER TABLE [dbo].[cms_sites]  WITH CHECK ADD  CONSTRAINT [FK_cms_sites_aspnet_Users] FOREIGN KEY([publisher_id])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[cms_sites] CHECK CONSTRAINT [FK_cms_sites_aspnet_Users]
GO
ALTER TABLE [dbo].[cms_sites]  WITH CHECK ADD  CONSTRAINT [FK_cms_sites_cms_domains] FOREIGN KEY([domain_id])
REFERENCES [dbo].[cms_domains] ([domain_id])
GO
ALTER TABLE [dbo].[cms_sites] CHECK CONSTRAINT [FK_cms_sites_cms_domains]
GO
ALTER TABLE [dbo].[cms_sites]  WITH NOCHECK ADD  CONSTRAINT [FK_cms_sites_cms_structure_groups] FOREIGN KEY([group_id])
REFERENCES [dbo].[cms_structure_groups] ([group_id])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[cms_sites] NOCHECK CONSTRAINT [FK_cms_sites_cms_structure_groups]
GO
ALTER TABLE [dbo].[cms_zone_groups]  WITH CHECK ADD  CONSTRAINT [FK_cms_zone_groups_aspnet_Users] FOREIGN KEY([publisher_id])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[cms_zone_groups] CHECK CONSTRAINT [FK_cms_zone_groups_aspnet_Users]
GO
ALTER TABLE [dbo].[cms_zone_groups]  WITH CHECK ADD  CONSTRAINT [FK_cms_zone_groups_cms_sites] FOREIGN KEY([site_id])
REFERENCES [dbo].[cms_sites] ([site_id])
GO
ALTER TABLE [dbo].[cms_zone_groups] CHECK CONSTRAINT [FK_cms_zone_groups_cms_sites]
GO
ALTER TABLE [dbo].[cms_zone_revision]  WITH CHECK ADD  CONSTRAINT [FK_cms_zone_revision_aspnet_Users] FOREIGN KEY([revised_by])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[cms_zone_revision] CHECK CONSTRAINT [FK_cms_zone_revision_aspnet_Users]
GO
ALTER TABLE [dbo].[cms_zone_revision]  WITH CHECK ADD  CONSTRAINT [FK_cms_zone_revision_aspnet_Users1] FOREIGN KEY([approval_id])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[cms_zone_revision] CHECK CONSTRAINT [FK_cms_zone_revision_aspnet_Users1]
GO
ALTER TABLE [dbo].[cms_zone_revision]  WITH NOCHECK ADD  CONSTRAINT [FK_cms_zone_revision_cms_zones] FOREIGN KEY([zone_id])
REFERENCES [dbo].[cms_zones] ([zone_id])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[cms_zone_revision] NOCHECK CONSTRAINT [FK_cms_zone_revision_cms_zones]
GO
ALTER TABLE [dbo].[cms_zones]  WITH NOCHECK ADD  CONSTRAINT [FK_cms_zones_aspnet_Users] FOREIGN KEY([publisher_id])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[cms_zones] CHECK CONSTRAINT [FK_cms_zones_aspnet_Users]
GO
ALTER TABLE [dbo].[cms_zones]  WITH NOCHECK ADD  CONSTRAINT [FK_cms_zones_cms_zone_groups] FOREIGN KEY([zone_group_id])
REFERENCES [dbo].[cms_zone_groups] ([zone_group_id])
GO
ALTER TABLE [dbo].[cms_zones] CHECK CONSTRAINT [FK_cms_zones_cms_zone_groups]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=NULL , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_article_files', @level2type=N'COLUMN',@level2name=N'article_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'N: Not Approved, A: Approved, X: Discarded, W: Waiting for Approval' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_article_files_revision', @level2type=N'COLUMN',@level2name=N'revision_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'N: Not Approved, A: Approved, X: Discarded, W: Waiting for Approval' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_article_revision', @level2type=N'COLUMN',@level2name=N'revision_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Passive, 1: Active, 2: Deleted' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_article_revision', @level2type=N'COLUMN',@level2name=N'status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Hide, 1: Item, 2: Folder, 3: Folder Click' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_article_revision', @level2type=N'COLUMN',@level2name=N'navigation_display'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Internal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_article_revision', @level2type=N'COLUMN',@level2name=N'article_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Zone, 1: Zone Group, 2: Site, 3: Template' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_article_revision', @level2type=N'COLUMN',@level2name=N'cl_1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Zone, 1: Zone Group, 2: Site, 3: Template' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_article_revision', @level2type=N'COLUMN',@level2name=N'cl_2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Zone, 1: Zone Group, 2: Site, 3: Template' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_article_revision', @level2type=N'COLUMN',@level2name=N'cl_3'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Zone, 1: Zone Group, 2: Site, 3: Template' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_article_revision', @level2type=N'COLUMN',@level2name=N'cl_4'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Zone, 1: Zone Group, 2: Site, 3: Template' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_article_revision', @level2type=N'COLUMN',@level2name=N'cl_5'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Passive, 1: Active, 2: Deleted' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_articles', @level2type=N'COLUMN',@level2name=N'status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Hide, 1: Item, 2: Folder, 3: Folder Click' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_articles', @level2type=N'COLUMN',@level2name=N'navigation_display'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Internal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_articles', @level2type=N'COLUMN',@level2name=N'article_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Zone, 1: Zone Group, 2: Site, 3: Template' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_articles', @level2type=N'COLUMN',@level2name=N'cl_1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Zone, 1: Zone Group, 2: Site, 3: Template' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_articles', @level2type=N'COLUMN',@level2name=N'cl_2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Zone, 1: Zone Group, 2: Site, 3: Template' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_articles', @level2type=N'COLUMN',@level2name=N'cl_3'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Zone, 1: Zone Group, 2: Site, 3: Template' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_articles', @level2type=N'COLUMN',@level2name=N'cl_4'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Zone, 1: Zone Group, 2: Site, 3: Template' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_articles', @level2type=N'COLUMN',@level2name=N'cl_5'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Y/N' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_asp_errors', @level2type=N'COLUMN',@level2name=N'MonitorSent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 for unlimited' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_breadcrumbs', @level2type=N'COLUMN',@level2name=N'deep_level'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Y: Yes, N: No' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_breadcrumbs', @level2type=N'COLUMN',@level2name=N'include_site'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Y: Yes, N: No' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_breadcrumbs', @level2type=N'COLUMN',@level2name=N'include_zonegroup'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Y: Yes, N: No' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_breadcrumbs', @level2type=N'COLUMN',@level2name=N'include_headline'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=NULL , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_classification_combo_values', @level2type=N'COLUMN',@level2name=N'created'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Y: Yes, N: No' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_config', @level2type=N'COLUMN',@level2name=N'isDefault'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'A:Active, D:Deleted' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_css', @level2type=N'COLUMN',@level2name=N'css_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Screen, 1: Mobile, 2: Print' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_css', @level2type=N'COLUMN',@level2name=N'css_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Screen, 1: Mobile, 2: Print' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_css_revisions', @level2type=N'COLUMN',@level2name=N'css_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N's:şikayet;o:öneri;t:talep' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_custom_form', @level2type=N'COLUMN',@level2name=N'info_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'A: Active, P: Passive' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_domains', @level2type=N'COLUMN',@level2name=N'domain_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'file''s width - height' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_file_types', @level2type=N'COLUMN',@level2name=N'file1_wh'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'file''s width - height' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_file_types', @level2type=N'COLUMN',@level2name=N'file2_wh'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'file''s width - height' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_file_types', @level2type=N'COLUMN',@level2name=N'file3_wh'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'file''s width - height' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_file_types', @level2type=N'COLUMN',@level2name=N'file4_wh'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'file''s width - height' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_file_types', @level2type=N'COLUMN',@level2name=N'file5_wh'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'file''s width - height' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_file_types', @level2type=N'COLUMN',@level2name=N'file6_wh'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'file''s width - height' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_file_types', @level2type=N'COLUMN',@level2name=N'file7_wh'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'file''s width - height' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_file_types', @level2type=N'COLUMN',@level2name=N'file8_wh'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'file''s width - height' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_file_types', @level2type=N'COLUMN',@level2name=N'file9_wh'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'file''s width - height' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_file_types', @level2type=N'COLUMN',@level2name=N'file10_wh'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'kb' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_file_types', @level2type=N'COLUMN',@level2name=N'file1_size'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'kb' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_file_types', @level2type=N'COLUMN',@level2name=N'file2_size'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'kb' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_file_types', @level2type=N'COLUMN',@level2name=N'file3_size'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'kb' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_file_types', @level2type=N'COLUMN',@level2name=N'file4_size'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'kb' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_file_types', @level2type=N'COLUMN',@level2name=N'file5_size'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'kb' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_file_types', @level2type=N'COLUMN',@level2name=N'file6_size'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'kb' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_file_types', @level2type=N'COLUMN',@level2name=N'file7_size'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'kb' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_file_types', @level2type=N'COLUMN',@level2name=N'file8_size'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'kb' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_file_types', @level2type=N'COLUMN',@level2name=N'file9_size'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'kb' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_file_types', @level2type=N'COLUMN',@level2name=N'file10_size'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'N:New, T:Taken, R:Retry Later, P:Proccessed' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_fop_failure_log', @level2type=N'COLUMN',@level2name=N'op_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Querystring, 1: Cookie, 2: Session' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_hidden_values', @level2type=N'COLUMN',@level2name=N'hidden_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ME: Message, ZA: Zone Approval, AA: Article Approval' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_instant_messaging', @level2type=N'COLUMN',@level2name=N'ims_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Passive, 1: Active' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_plugins', @level2type=N'COLUMN',@level2name=N'plugin_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Passive, 1: Active, 2: Deleted' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_portlets', @level2type=N'COLUMN',@level2name=N'portlet_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'For HTML editor: 0, For text Editor: 1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_portlets', @level2type=N'COLUMN',@level2name=N'editor_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Default, 1:Attempt, 2:Fail, 3: Success' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_publisher_log_events', @level2type=N'COLUMN',@level2name=N'event_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:Zone Relation' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_relations', @level2type=N'COLUMN',@level2name=N'rel_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'A: Active, P:Passive, D: Deleted' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_rss_channels', @level2type=N'COLUMN',@level2name=N'channel_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'S: Site, G: Zone Group, Z: Zone' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_rss_content', @level2type=N'COLUMN',@level2name=N'sgz_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'E: Exclude, I: Include' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_rss_content', @level2type=N'COLUMN',@level2name=N'sgz_exclude'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: ready, 1: waiting for create order, 2: creating' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_sitemaps', @level2type=N'COLUMN',@level2name=N'status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hours' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_sitemaps', @level2type=N'COLUMN',@level2name=N'interval'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'A: Active, D: Deleted' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_stf_templates', @level2type=N'COLUMN',@level2name=N'stft_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1: CSS; 2: Sites; 3: Templates; 4: Portlets; 5: Plugins; 6: XML; 7: File Types' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_structure_groups', @level2type=N'COLUMN',@level2name=N'group_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Screen, 1: Mobile' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_template_revisions', @level2type=N'COLUMN',@level2name=N'template_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'A:Active, D:Deleted' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_templates', @level2type=N'COLUMN',@level2name=N'template_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Screen, 1: Mobile' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_templates', @level2type=N'COLUMN',@level2name=N'template_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: None, 1: Overwrite, 2: Append' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_zone_groups', @level2type=N'COLUMN',@level2name=N'css_merge'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'N: Not Approved, A: Approved, X: Discarded, W: Waiting for Approval' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_zone_revision', @level2type=N'COLUMN',@level2name=N'revision_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'A: Active, P: Passive, D: Delete' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_zone_revision', @level2type=N'COLUMN',@level2name=N'zone_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: None, 1: Overwrite, 2: Append' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_zone_revision', @level2type=N'COLUMN',@level2name=N'css_merge'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'A: Active, P: Passive, N: New, D: Deleted' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_zones', @level2type=N'COLUMN',@level2name=N'zone_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: None, 1: Overwrite, 2: Append' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'cms_zones', @level2type=N'COLUMN',@level2name=N'css_merge'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ar"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 168
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "s"
            Begin Extent = 
               Top = 7
               Left = 290
               Bottom = 168
               Right = 530
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "zg"
            Begin Extent = 
               Top = 7
               Left = 578
               Bottom = 168
               Right = 847
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "z"
            Begin Extent = 
               Top = 168
               Left = 48
               Bottom = 329
               Right = 275
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "a"
            Begin Extent = 
               Top = 168
               Left = 323
               Bottom = 329
               Right = 545
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_cms_AccessRules'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_cms_AccessRules'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ar"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 168
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "a"
            Begin Extent = 
               Top = 168
               Left = 48
               Bottom = 329
               Right = 270
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_cms_AccessRules_ZoneGroup'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_cms_AccessRules_ZoneGroup'
GO
USE [master]
GO
ALTER DATABASE [TEST] SET  READ_WRITE 
GO
