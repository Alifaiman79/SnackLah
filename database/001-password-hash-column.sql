-- Back up the database before applying this change.
-- Run this migration before deploying password hashing changes.
ALTER TABLE dbo.Users ALTER COLUMN [Password] NVARCHAR(256) NOT NULL;
