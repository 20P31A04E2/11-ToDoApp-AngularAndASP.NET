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

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705124127_Initial'
)
BEGIN
    CREATE TABLE [Status] (
        [StatusId] int NOT NULL IDENTITY,
        [Status] nvarchar(10) NOT NULL,
        CONSTRAINT [PK__Status__C8EE20636B7F4021] PRIMARY KEY ([StatusId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705124127_Initial'
)
BEGIN
    CREATE TABLE [Users] (
        [UserId] int NOT NULL IDENTITY,
        [UserName] nvarchar(50) NOT NULL,
        [Password] nvarchar(50) NOT NULL,
        CONSTRAINT [PK__Users__1788CC4CF450A1FA] PRIMARY KEY ([UserId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705124127_Initial'
)
BEGIN
    CREATE TABLE [TodoTasks] (
        [TaskId] int NOT NULL IDENTITY,
        [TaskName] nvarchar(100) NOT NULL,
        [TaskDescription] nvarchar(200) NULL,
        [CreatedOn] datetime NOT NULL DEFAULT ((getdate())),
        [CompletedOn] datetime NULL,
        [UserId] int NOT NULL,
        [StatusId] int NOT NULL,
        CONSTRAINT [PK__TodoTask__7C6949B102D6F9DC] PRIMARY KEY ([TaskId]),
        CONSTRAINT [FK__TodoTasks__Statu__47DBAE45] FOREIGN KEY ([StatusId]) REFERENCES [Status] ([StatusId]),
        CONSTRAINT [FK__TodoTasks__UserI__46E78A0C] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705124127_Initial'
)
BEGIN
    CREATE INDEX [IX_TodoTasks_StatusId] ON [TodoTasks] ([StatusId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705124127_Initial'
)
BEGIN
    CREATE INDEX [IX_TodoTasks_UserId] ON [TodoTasks] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705124127_Initial'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240705124127_Initial', N'8.0.4');
END;
GO

COMMIT;
GO

