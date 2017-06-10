
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/08/2017 05:13:13
-- Generated from EDMX file: C:\Users\john\Documents\Visual Studio 2017\Projects\WLA\WLA\Models\WLAEntity.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [wla];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ActivityActivityGroupList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ActivityGroupLists] DROP CONSTRAINT [FK_ActivityActivityGroupList];
GO
IF OBJECT_ID(N'[dbo].[FK_ActivityGroupActivityGroupList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ActivityGroupLists] DROP CONSTRAINT [FK_ActivityGroupActivityGroupList];
GO
IF OBJECT_ID(N'[dbo].[FK_PelaksanaWLA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WLATrx] DROP CONSTRAINT [FK_PelaksanaWLA];
GO
IF OBJECT_ID(N'[dbo].[FK_PeriodeWLA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WLATrx] DROP CONSTRAINT [FK_PeriodeWLA];
GO
IF OBJECT_ID(N'[dbo].[FK_ActivityGroupWLATrx]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WLATrx] DROP CONSTRAINT [FK_ActivityGroupWLATrx];
GO
IF OBJECT_ID(N'[dbo].[FK_ActivityWLATrx]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WLATrx] DROP CONSTRAINT [FK_ActivityWLATrx];
GO
IF OBJECT_ID(N'[dbo].[FK_FungsiWLAHeader]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WLAHeaders] DROP CONSTRAINT [FK_FungsiWLAHeader];
GO
IF OBJECT_ID(N'[dbo].[FK_JabatanWLAHeader]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WLAHeaders] DROP CONSTRAINT [FK_JabatanWLAHeader];
GO
IF OBJECT_ID(N'[dbo].[FK_WLAHeaderWLATrx]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WLATrx] DROP CONSTRAINT [FK_WLAHeaderWLATrx];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Activities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Activities];
GO
IF OBJECT_ID(N'[dbo].[ActivityGroupLists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActivityGroupLists];
GO
IF OBJECT_ID(N'[dbo].[ActivityGroups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActivityGroups];
GO
IF OBJECT_ID(N'[dbo].[Fungsi]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Fungsi];
GO
IF OBJECT_ID(N'[dbo].[Jabatan]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Jabatan];
GO
IF OBJECT_ID(N'[dbo].[Pelaksana]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pelaksana];
GO
IF OBJECT_ID(N'[dbo].[Periode]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Periode];
GO
IF OBJECT_ID(N'[dbo].[WLATrx]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WLATrx];
GO
IF OBJECT_ID(N'[dbo].[Standard_Time]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Standard_Time];
GO
IF OBJECT_ID(N'[dbo].[WLAHeaders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WLAHeaders];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Activities'
CREATE TABLE [dbo].[Activities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Type] int  NOT NULL
);
GO

-- Creating table 'ActivityGroupLists'
CREATE TABLE [dbo].[ActivityGroupLists] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Activity_Id] int  NOT NULL,
    [ActivityGroup_Id] int  NOT NULL
);
GO

-- Creating table 'ActivityGroups'
CREATE TABLE [dbo].[ActivityGroups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Fungsi'
CREATE TABLE [dbo].[Fungsi] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Jabatan'
CREATE TABLE [dbo].[Jabatan] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Pelaksana'
CREATE TABLE [dbo].[Pelaksana] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Periode'
CREATE TABLE [dbo].[Periode] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'WLATrx'
CREATE TABLE [dbo].[WLATrx] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Process_Time] int  NOT NULL,
    [Frequency] int  NOT NULL,
    [Sub_Total_Aktivitas] float  NOT NULL,
    [Effective_Working_Hours] float  NOT NULL,
    [FTE] float  NOT NULL,
    [Presentase] float  NOT NULL,
    [Pelaksana_Id] int  NOT NULL,
    [Periode_Id] int  NOT NULL,
    [ActivityGroup_Id] int  NOT NULL,
    [Activity_Id] int  NOT NULL,
    [WLAHeader_Id] int  NOT NULL
);
GO

-- Creating table 'Standard_Time'
CREATE TABLE [dbo].[Standard_Time] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Tahun] int  NOT NULL,
    [Day] int  NOT NULL,
    [Saturday] int  NOT NULL,
    [Sunday] int  NOT NULL,
    [Holiday] int  NOT NULL,
    [Annual_Leaves] int  NOT NULL,
    [Sick_Permission] int  NOT NULL,
    [Working_Days] int  NOT NULL,
    [Effective_Working_Days] int  NOT NULL,
    [Working_Hours] int  NOT NULL,
    [Utilitation_Level] float  NOT NULL,
    [Effective_Working_Hours] float  NOT NULL
);
GO

-- Creating table 'WLAHeaders'
CREATE TABLE [dbo].[WLAHeaders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Tahun] nvarchar(max)  NOT NULL,
    [FTE] float  NOT NULL,
    [Fungsi_Id] int  NOT NULL,
    [Jabatan_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Activities'
ALTER TABLE [dbo].[Activities]
ADD CONSTRAINT [PK_Activities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ActivityGroupLists'
ALTER TABLE [dbo].[ActivityGroupLists]
ADD CONSTRAINT [PK_ActivityGroupLists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ActivityGroups'
ALTER TABLE [dbo].[ActivityGroups]
ADD CONSTRAINT [PK_ActivityGroups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Fungsi'
ALTER TABLE [dbo].[Fungsi]
ADD CONSTRAINT [PK_Fungsi]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Jabatan'
ALTER TABLE [dbo].[Jabatan]
ADD CONSTRAINT [PK_Jabatan]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Pelaksana'
ALTER TABLE [dbo].[Pelaksana]
ADD CONSTRAINT [PK_Pelaksana]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Periode'
ALTER TABLE [dbo].[Periode]
ADD CONSTRAINT [PK_Periode]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WLATrx'
ALTER TABLE [dbo].[WLATrx]
ADD CONSTRAINT [PK_WLATrx]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Standard_Time'
ALTER TABLE [dbo].[Standard_Time]
ADD CONSTRAINT [PK_Standard_Time]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WLAHeaders'
ALTER TABLE [dbo].[WLAHeaders]
ADD CONSTRAINT [PK_WLAHeaders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Activity_Id] in table 'ActivityGroupLists'
ALTER TABLE [dbo].[ActivityGroupLists]
ADD CONSTRAINT [FK_ActivityActivityGroupList]
    FOREIGN KEY ([Activity_Id])
    REFERENCES [dbo].[Activities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ActivityActivityGroupList'
CREATE INDEX [IX_FK_ActivityActivityGroupList]
ON [dbo].[ActivityGroupLists]
    ([Activity_Id]);
GO

-- Creating foreign key on [ActivityGroup_Id] in table 'ActivityGroupLists'
ALTER TABLE [dbo].[ActivityGroupLists]
ADD CONSTRAINT [FK_ActivityGroupActivityGroupList]
    FOREIGN KEY ([ActivityGroup_Id])
    REFERENCES [dbo].[ActivityGroups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ActivityGroupActivityGroupList'
CREATE INDEX [IX_FK_ActivityGroupActivityGroupList]
ON [dbo].[ActivityGroupLists]
    ([ActivityGroup_Id]);
GO

-- Creating foreign key on [Pelaksana_Id] in table 'WLATrx'
ALTER TABLE [dbo].[WLATrx]
ADD CONSTRAINT [FK_PelaksanaWLA]
    FOREIGN KEY ([Pelaksana_Id])
    REFERENCES [dbo].[Pelaksana]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PelaksanaWLA'
CREATE INDEX [IX_FK_PelaksanaWLA]
ON [dbo].[WLATrx]
    ([Pelaksana_Id]);
GO

-- Creating foreign key on [Periode_Id] in table 'WLATrx'
ALTER TABLE [dbo].[WLATrx]
ADD CONSTRAINT [FK_PeriodeWLA]
    FOREIGN KEY ([Periode_Id])
    REFERENCES [dbo].[Periode]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PeriodeWLA'
CREATE INDEX [IX_FK_PeriodeWLA]
ON [dbo].[WLATrx]
    ([Periode_Id]);
GO

-- Creating foreign key on [ActivityGroup_Id] in table 'WLATrx'
ALTER TABLE [dbo].[WLATrx]
ADD CONSTRAINT [FK_ActivityGroupWLATrx]
    FOREIGN KEY ([ActivityGroup_Id])
    REFERENCES [dbo].[ActivityGroups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ActivityGroupWLATrx'
CREATE INDEX [IX_FK_ActivityGroupWLATrx]
ON [dbo].[WLATrx]
    ([ActivityGroup_Id]);
GO

-- Creating foreign key on [Activity_Id] in table 'WLATrx'
ALTER TABLE [dbo].[WLATrx]
ADD CONSTRAINT [FK_ActivityWLATrx]
    FOREIGN KEY ([Activity_Id])
    REFERENCES [dbo].[Activities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ActivityWLATrx'
CREATE INDEX [IX_FK_ActivityWLATrx]
ON [dbo].[WLATrx]
    ([Activity_Id]);
GO

-- Creating foreign key on [Fungsi_Id] in table 'WLAHeaders'
ALTER TABLE [dbo].[WLAHeaders]
ADD CONSTRAINT [FK_FungsiWLAHeader]
    FOREIGN KEY ([Fungsi_Id])
    REFERENCES [dbo].[Fungsi]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FungsiWLAHeader'
CREATE INDEX [IX_FK_FungsiWLAHeader]
ON [dbo].[WLAHeaders]
    ([Fungsi_Id]);
GO

-- Creating foreign key on [Jabatan_Id] in table 'WLAHeaders'
ALTER TABLE [dbo].[WLAHeaders]
ADD CONSTRAINT [FK_JabatanWLAHeader]
    FOREIGN KEY ([Jabatan_Id])
    REFERENCES [dbo].[Jabatan]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JabatanWLAHeader'
CREATE INDEX [IX_FK_JabatanWLAHeader]
ON [dbo].[WLAHeaders]
    ([Jabatan_Id]);
GO

-- Creating foreign key on [WLAHeader_Id] in table 'WLATrx'
ALTER TABLE [dbo].[WLATrx]
ADD CONSTRAINT [FK_WLAHeaderWLATrx]
    FOREIGN KEY ([WLAHeader_Id])
    REFERENCES [dbo].[WLAHeaders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WLAHeaderWLATrx'
CREATE INDEX [IX_FK_WLAHeaderWLATrx]
ON [dbo].[WLATrx]
    ([WLAHeader_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------