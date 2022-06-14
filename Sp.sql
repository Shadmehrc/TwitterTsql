USE TwitterTsql;
GO
CREATE PROCEDURE CreatePhotoTweet
    @PhotoAddress NVARCHAR(MAX),
    @UserId NVARCHAR(450)
AS
BEGIN
    INSERT INTO dbo.PhotoTweets
    (
        PhotoAddress,
        UserId
    )
    VALUES
    (@PhotoAddress, @UserId);
    SELECT @@ROWCOUNT;
END;
GO

------------------------------------------------------------------------------------------
USE TwitterTsql;
GO
CREATE PROCEDURE CreateTag @Word NVARCHAR(MAX)
AS
BEGIN
    IF EXISTS (SELECT * FROM dbo.Tags WHERE Word = @Word)
    BEGIN
        SELECT Id,
               Word
        FROM dbo.Tags
        WHERE Word = @Word;
        RETURN;
    END;

    INSERT INTO dbo.Tags
    (
        Word
    )
    VALUES
    (@Word -- Word - nvarchar(max)
        );

    SELECT Id,
           Word
    FROM dbo.Tags
    WHERE Id = @@IDENTITY;
END;
GO

------------------------------------------------------------------------------------------
USE TwitterTsql;
GO
CREATE PROCEDURE dbo.CreateTextTweet
    @Text NVARCHAR(MAX),
    @UserId VARCHAR(150),
    @Hashtag NVARCHAR(MAX),
    @UserTagIds VARCHAR(MAX)
AS
BEGIN
    INSERT INTO dbo.Tweets
    (
        Text,
        UserId,
        TagCount,
        TweetViewCount,
        Likes,
        IsRemoved
    )
    VALUES
    (   @Text,   -- Text - nvarchar(max)
        @UserId, -- UserId - nvarchar(450)
        0,       -- TagCount - int
        0,       -- TweetViewCount - int
        0,       -- Likes - int
        0        -- IsRemoved - bit
        );

    DECLARE @Identity INT;
    SET @Identity = @@IDENTITY;

    IF (@Hashtag <> '')
    BEGIN
        SELECT value
        INTO #Hashtags
        FROM STRING_SPLIT(@Hashtag, ',');
        ALTER TABLE #Hashtags ADD TagId INT NULL;


        MERGE dbo.Tags AS Target
        USING #Hashtags AS Source
        ON Source.value = Target.Word
        WHEN NOT MATCHED BY TARGET THEN
            INSERT
            (
                Word
            )
            VALUES
            (Source.value);

        UPDATE #Hashtags
        SET TagId =
            (
                SELECT Id FROM dbo.Tags WHERE Word = #Hashtags.value
            );
        SELECT *
        FROM #Hashtags;

        INSERT INTO dbo.TweetTags
        SELECT @Identity TweetId,
               H.TagId,
               H.value
        FROM #Hashtags H;;

        SELECT COUNT(T.Id)
        FROM dbo.TweetTags T
        WHERE T.TweetId = @Identity;

        UPDATE dbo.Tweets
        SET TagCount =
            (
                SELECT COUNT(T.Id)FROM dbo.TweetTags T WHERE T.TweetId = @Identity
            )
        WHERE Tweets.Id = @Identity;


        DROP TABLE IF EXISTS #Hashtags;
    END;


    --IF @UserTagIds  NOT NULL
    IF (@UserTagIds <> '')
    BEGIN
        SELECT value
        INTO #TagUsers
        FROM STRING_SPLIT(@UserTagIds, ',');

        INSERT INTO dbo.UserTaggeds
        SELECT U.value UserId,
               @Identity TweetId
        FROM #TagUsers AS U;

        DROP TABLE IF EXISTS #TagUsers;
    END;
END;
GO

------------------------------------------------------------------------------------------
USE TwitterTsql;
GO
CREATE PROCEDURE DeleteTag @Id INT
AS
BEGIN
    UPDATE dbo.Tags
    SET IsRemoved = 1
    WHERE Id = @Id;
    SELECT @@ROWCOUNT;
END;
GO

------------------------------------------------------------------------------------------
USE TwitterTsql;
GO
CREATE PROCEDURE DeleteTweet @Id INT
AS
BEGIN
    UPDATE dbo.Tweets
    SET IsRemoved = 1
    WHERE Id = @Id;
    SELECT @@ROWCOUNT;
END;
GO

------------------------------------------------------------------------------------------
USE TwitterTsql;
GO
CREATE PROCEDURE EditTag
    @Word NVARCHAR(MAX),
    @id INT
AS
BEGIN
    UPDATE dbo.Tags
    SET Word = @Word
    WHERE Id = @id;
    SELECT @@ROWCOUNT;
END;
GO

------------------------------------------------------------------------------------------
USE TwitterTsql;
GO
CREATE PROCEDURE EditTweet
    @Word NVARCHAR(MAX),
    @Id INT
AS
BEGIN
    UPDATE dbo.Tweets
    SET Text = @Word
    WHERE Id = @Id;
    SELECT @@ROWCOUNT;
END;
GO

------------------------------------------------------------------------------------------
USE TwitterTsql;
GO
CREATE PROCEDURE FindListName @ids VARCHAR(100)
AS
BEGIN
    SELECT FullName
    FROM dbo.AspNetUsers AS T
    WHERE T.Id IN
          (
              SELECT * FROM STRING_SPLIT(@ids, ',')
          );
END;
GO

------------------------------------------------------------------------------------------
USE TwitterTsql;
GO
CREATE PROCEDURE FindTag @Id INT
AS
BEGIN
    SELECT *
    FROM dbo.Tags AS T
    WHERE T.Id = @Id;
END;
GO

------------------------------------------------------------------------------------------
USE TwitterTsql;
GO
CREATE PROCEDURE dbo.FindTextTweet @TweetId INT
AS
BEGIN
    SELECT Id,
           Text,
           UserId,
           TagCount,
           TweetViewCount,
           Likes
    FROM dbo.Tweets
    WHERE Id = @TweetId
          AND IsRemoved <> 1;

    SELECT T.Word
    FROM dbo.TweetTags T
    WHERE T.TweetId = @TweetId;



    SELECT FullName
    FROM dbo.AspNetUsers
    WHERE Id IN
          (
              SELECT G.UserId FROM dbo.UserTaggeds AS G WHERE G.TweetId = @TweetId
          );

    UPDATE dbo.Tweets
    SET TweetViewCount += 1
    WHERE Id = @TweetId;

END;
GO

------------------------------------------------------------------------------------------
USE TwitterTsql;
GO
CREATE PROCEDURE dbo.FindUserFullName @id NVARCHAR(MAX)
AS
BEGIN
    SELECT FullName
    FROM dbo.AspNetUsers AS T
    WHERE T.Id = @id;
END;
GO

------------------------------------------------------------------------------------------
USE TwitterTsql;
GO
CREATE PROCEDURE dbo.FindUserTaggedTweets @TweetId NVARCHAR(MAX)
AS
BEGIN
    SELECT Text
    FROM Tweets AS T
    WHERE T.Id IN
          (
              SELECT TweetId FROM dbo.UserTaggeds WHERE UserId = @TweetId
          );
END;
GO

------------------------------------------------------------------------------------------
USE TwitterTsql;
GO
CREATE PROCEDURE dbo.GetPhotoTweet @Id INT
AS
BEGIN
    SELECT Id,
           PhotoAddress,
           UserId
    FROM dbo.PhotoTweets
    WHERE Id = @Id
          AND ISNULL(IsRemoved, 0) <> 1;
END;
GO
------------------------------------------------------------------------------------------
USE TwitterTsql;
GO
CREATE PROCEDURE GetTweetWithNMostTag
AS
BEGIN
    DECLARE @TweetId INT;
    SET @TweetId =
    (
        SELECT Tweet.Id
        FROM dbo.Tweets AS Tweet
        WHERE Tweet.TagCount =
        (
            SELECT MAX(TagCount)FROM dbo.Tweets
        )
    );
    SELECT Tweet.Id,
           Tweet.Text,
           Tweet.TagCount
    FROM dbo.Tweets AS Tweet
    WHERE Id = @TweetId;

    SELECT Tag.Word
    FROM dbo.TweetTags AS Tag
    WHERE Tag.TweetId = @TweetId;
END;
GO

------------------------------------------------------------------------------------------
USE TwitterTsql;
GO
CREATE PROCEDURE LikeTweet @Id INT
AS
BEGIN
    UPDATE dbo.Tweets
    SET Likes += 1
    WHERE Id = @Id;
    SELECT @@ROWCOUNT;
END;
GO

------------------------------------------------------------------------------------------
USE TwitterTsql;
GO
CREATE PROCEDURE ReTweet
    @UserId NVARCHAR(MAX),
    @TweetId INT
AS
BEGIN

    IF NOT EXISTS (SELECT * FROM dbo.Tweets AS T WHERE T.Id = @TweetId)
    BEGIN
        SELECT 0;
        RETURN;
    END;
    INSERT INTO dbo.Tweets
    (
        Text,
        UserId,
        TagCount,
        TweetViewCount,
        Likes,
        IsRemoved
    )
    VALUES
    (
        (
            SELECT TOP 1 Text FROM dbo.Tweets WHERE Id = @TweetId
        ), @UserId,
        (
            SELECT TOP 1 TagCount FROM dbo.Tweets WHERE Id = @TweetId
        ), 0,
           -- TweetViewCount - int
        0, -- Likes - int
        0  -- IsRemoved - bit
        );


    DECLARE @Identity INT;
    SET @Identity = @@IDENTITY;


    SELECT T.TagId,
           T.Word
    INTO #TMPTag
    FROM dbo.TweetTags T
    WHERE TweetId = 3;

    INSERT INTO dbo.TweetTags
    SELECT @Identity TweetId,
           m.TagId TagId,
           m.Word Word
    FROM #TMPTag AS m;

    DROP TABLE #TMPTag;



    SELECT TG.UserId
    INTO #TmpUserTagged
    FROM dbo.UserTaggeds TG
    WHERE TG.TweetId = 3;


    INSERT INTO dbo.UserTaggeds
    SELECT tm.UserId UserId,
           @Identity TweetId
    FROM #TmpUserTagged AS tm;

    DROP TABLE #TmpUserTagged;

END;
GO

------------------------------------------------------------------------------------------
USE TwitterTsql;
GO
CREATE PROCEDURE dbo.SendUserTaggedNotification
    @UserId NVARCHAR(MAX),
    @NotifReceiverId NVARCHAR(MAX)
AS
BEGIN
    DECLARE @User NVARCHAR(MAX);
    SET @User =
    (
        SELECT U.UserName FROM dbo.AspNetUsers AS U WHERE U.Id = @UserId
    );


    DECLARE @NotificationMessage NVARCHAR(MAX);
    SET @NotificationMessage = @User + N'  is tagged you in a tweet';

    INSERT INTO dbo.Notifications
    (
        Text,
        UserId
    )
    VALUES
    (@NotificationMessage, @NotifReceiverId);
END;
GO

------------------------------------------------------------------------------------------
USE TwitterTsql;
GO
CREATE PROCEDURE MostLikedTweet
AS
BEGIN
    SELECT Id,
           Text,
           Likes,
           UserId,
           TweetViewCount
    FROM dbo.Tweets
    WHERE Likes =
    (
        SELECT MAX(Likes)FROM dbo.Tweets
    );
END;
GO

------------------------------------------------------------------------------------------
USE TwitterTsql;
GO
CREATE PROCEDURE MostTaggedTweet
AS
BEGIN
    SELECT Tweet.Id,
           Tweet.Text,
           Tweet.TagCount,
           Tag.Word
    FROM dbo.Tweets AS Tweet WITH (NOLOCK)
        INNER JOIN dbo.TweetTags Tag WITH (NOLOCK)
            ON Tweet.Id = Tag.TweetId
    WHERE Tweet.TagCount =
    (
        SELECT MAX(TagCount)FROM dbo.Tweets
    );
END;
GO

------------------------------------------------------------------------------------------
USE TwitterTsql;
GO
CREATE PROCEDURE MostViewedTweet
AS
BEGIN
    SELECT Tweet.Id,
           Tweet.Text,
           Tweet.UserId,
           Tweet.TagCount,
           Tweet.TweetViewCount
    FROM dbo.Tweets AS Tweet
    WHERE TweetViewCount =
    (
        SELECT MAX(TweetViewCount)FROM dbo.Tweets
    );
END;
GO

------------------------------------------------------------------------------------------
USE TwitterTsql;
GO
CREATE PROCEDURE UserNotifications @UserId AS VARCHAR(50)
AS
BEGIN
    SELECT N.Text
    FROM dbo.Notifications AS N
    WHERE N.UserId = @UserId;
END;
GO


