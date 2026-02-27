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
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260226214747_InitialSetup'
)
BEGIN
    CREATE TABLE [tasks] (
        [id] uniqueidentifier NOT NULL,
        [title] nvarchar(max) NOT NULL,
        [description] nvarchar(max) NULL,
        [status] nvarchar(450) NOT NULL DEFAULT N'pending',
        [create_date] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [due_date] datetime2 NULL,
        CONSTRAINT [PK_tasks] PRIMARY KEY ([id]),
        CONSTRAINT [CK_tasks_status] CHECK (status IN ('pending', 'in_progress', 'completed'))
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260226214747_InitialSetup'
)
BEGIN
    CREATE INDEX [idx_tasks_status] ON [tasks] ([status]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260226214747_InitialSetup'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260226214747_InitialSetup', N'9.0.1');
END;

COMMIT;
GO

