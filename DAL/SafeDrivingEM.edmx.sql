
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 06/03/2011 02:29:33
-- Generated from EDMX file: C:\Users\damienp\Desktop\DevProject\SafeDriving\SafeDriving\DAL\SafeDrivingEM.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SafeDriving_DB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_EmployeArticle]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Articles] DROP CONSTRAINT [FK_EmployeArticle];
GO
IF OBJECT_ID(N'[dbo].[FK_CircuitDate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Dates] DROP CONSTRAINT [FK_CircuitDate];
GO
IF OBJECT_ID(N'[dbo].[FK_SessionCircuit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sessions] DROP CONSTRAINT [FK_SessionCircuit];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeDate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Dates] DROP CONSTRAINT [FK_EmployeDate];
GO
IF OBJECT_ID(N'[dbo].[FK_SessionDate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sessions] DROP CONSTRAINT [FK_SessionDate];
GO
IF OBJECT_ID(N'[dbo].[FK_VehiculeDate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Dates] DROP CONSTRAINT [FK_VehiculeDate];
GO
IF OBJECT_ID(N'[dbo].[FK_ForfaitReferenceOffresReference]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OffresReferences] DROP CONSTRAINT [FK_ForfaitReferenceOffresReference];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientOffre]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Offres] DROP CONSTRAINT [FK_ClientOffre];
GO
IF OBJECT_ID(N'[dbo].[FK_OffreOffreSession]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OffreSessions] DROP CONSTRAINT [FK_OffreOffreSession];
GO
IF OBJECT_ID(N'[dbo].[FK_OffreSessionSession]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OffreSessions] DROP CONSTRAINT [FK_OffreSessionSession];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeSession]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sessions] DROP CONSTRAINT [FK_EmployeSession];
GO
IF OBJECT_ID(N'[dbo].[FK_SessionVehicule_Session]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SessionVehicule] DROP CONSTRAINT [FK_SessionVehicule_Session];
GO
IF OBJECT_ID(N'[dbo].[FK_SessionVehicule_Vehicule]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SessionVehicule] DROP CONSTRAINT [FK_SessionVehicule_Vehicule];
GO
IF OBJECT_ID(N'[dbo].[FK_SessionTypeSession]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sessions] DROP CONSTRAINT [FK_SessionTypeSession];
GO
IF OBJECT_ID(N'[dbo].[FK_Utilisateurs_Roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Utilisateurs] DROP CONSTRAINT [FK_Utilisateurs_Roles];
GO
IF OBJECT_ID(N'[dbo].[FK_Employe_inherits_Utilisateur]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Utilisateurs_Employe] DROP CONSTRAINT [FK_Employe_inherits_Utilisateur];
GO
IF OBJECT_ID(N'[dbo].[FK_Client_inherits_Utilisateur]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Utilisateurs_Client] DROP CONSTRAINT [FK_Client_inherits_Utilisateur];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Articles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Articles];
GO
IF OBJECT_ID(N'[dbo].[Circuits]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Circuits];
GO
IF OBJECT_ID(N'[dbo].[Dates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Dates];
GO
IF OBJECT_ID(N'[dbo].[ForfaitReferences]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ForfaitReferences];
GO
IF OBJECT_ID(N'[dbo].[Offres]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Offres];
GO
IF OBJECT_ID(N'[dbo].[OffreSessions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OffreSessions];
GO
IF OBJECT_ID(N'[dbo].[OffresReferences]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OffresReferences];
GO
IF OBJECT_ID(N'[dbo].[Sessions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sessions];
GO
IF OBJECT_ID(N'[dbo].[Utilisateurs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Utilisateurs];
GO
IF OBJECT_ID(N'[dbo].[Vehicules]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Vehicules];
GO
IF OBJECT_ID(N'[dbo].[TypeSessions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TypeSessions];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[Utilisateurs_Employe]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Utilisateurs_Employe];
GO
IF OBJECT_ID(N'[dbo].[Utilisateurs_Client]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Utilisateurs_Client];
GO
IF OBJECT_ID(N'[dbo].[SessionVehicule]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SessionVehicule];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Articles'
CREATE TABLE [dbo].[Articles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EmployeId] int  NOT NULL,
    [Titre] nvarchar(50)  NOT NULL,
    [Date] datetime  NOT NULL,
    [Contenu] nvarchar(4000)  NOT NULL
);
GO

-- Creating table 'Circuits'
CREATE TABLE [dbo].[Circuits] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nom] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Dates'
CREATE TABLE [dbo].[Dates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [HeureDebut] datetime  NOT NULL,
    [HeureFin] datetime  NOT NULL,
    [Jour] datetime  NOT NULL,
    [VehiculeId] int  NOT NULL,
    [CircuitId] int  NOT NULL,
    [EmployeId] int  NOT NULL,
    [SessionId] int  NOT NULL
);
GO

-- Creating table 'ForfaitReferences'
CREATE TABLE [dbo].[ForfaitReferences] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nom] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Offres'
CREATE TABLE [dbo].[Offres] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ClientId] int  NOT NULL
);
GO

-- Creating table 'OffreSessions'
CREATE TABLE [dbo].[OffreSessions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OffreId] int  NOT NULL,
    [SessionId] int  NOT NULL,
    [Note] real  NOT NULL
);
GO

-- Creating table 'OffresReferences'
CREATE TABLE [dbo].[OffresReferences] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nom] nvarchar(50)  NOT NULL,
    [Prix] real  NOT NULL,
    [ForfaitReferenceId] int  NOT NULL
);
GO

-- Creating table 'Sessions'
CREATE TABLE [dbo].[Sessions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TypeSessionId] int  NOT NULL,
    [EmployeId] int  NOT NULL,
    [Nom] nvarchar(max)  NOT NULL,
    [CircuitId] int  NOT NULL,
    [Date_Id] int  NOT NULL
);
GO

-- Creating table 'Utilisateurs'
CREATE TABLE [dbo].[Utilisateurs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nom] nvarchar(30)  NOT NULL,
    [Prenom] nvarchar(30)  NOT NULL,
    [Adresse] nvarchar(150)  NOT NULL,
    [Ville] nvarchar(50)  NOT NULL,
    [CodePostal] nvarchar(10)  NOT NULL,
    [Telephone] nvarchar(20)  NOT NULL,
    [Mail] nvarchar(50)  NOT NULL,
    [Password] nchar(100)  NULL,
    [RoleId] int  NOT NULL
);
GO

-- Creating table 'Vehicules'
CREATE TABLE [dbo].[Vehicules] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nom] nvarchar(50)  NOT NULL,
    [SessionId] int  NOT NULL
);
GO

-- Creating table 'TypeSessions'
CREATE TABLE [dbo].[TypeSessions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nom] nvarchar(255)  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Privilege] nchar(20)  NOT NULL
);
GO

-- Creating table 'Utilisateurs_Employe'
CREATE TABLE [dbo].[Utilisateurs_Employe] (
    [Status] nvarchar(255)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'Utilisateurs_Client'
CREATE TABLE [dbo].[Utilisateurs_Client] (
    [DateNaissance] datetime  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'SessionVehicule'
CREATE TABLE [dbo].[SessionVehicule] (
    [Sessions_Id] int  NOT NULL,
    [Vehicules_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Articles'
ALTER TABLE [dbo].[Articles]
ADD CONSTRAINT [PK_Articles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Circuits'
ALTER TABLE [dbo].[Circuits]
ADD CONSTRAINT [PK_Circuits]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Dates'
ALTER TABLE [dbo].[Dates]
ADD CONSTRAINT [PK_Dates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ForfaitReferences'
ALTER TABLE [dbo].[ForfaitReferences]
ADD CONSTRAINT [PK_ForfaitReferences]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Offres'
ALTER TABLE [dbo].[Offres]
ADD CONSTRAINT [PK_Offres]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OffreSessions'
ALTER TABLE [dbo].[OffreSessions]
ADD CONSTRAINT [PK_OffreSessions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OffresReferences'
ALTER TABLE [dbo].[OffresReferences]
ADD CONSTRAINT [PK_OffresReferences]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sessions'
ALTER TABLE [dbo].[Sessions]
ADD CONSTRAINT [PK_Sessions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Utilisateurs'
ALTER TABLE [dbo].[Utilisateurs]
ADD CONSTRAINT [PK_Utilisateurs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Vehicules'
ALTER TABLE [dbo].[Vehicules]
ADD CONSTRAINT [PK_Vehicules]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TypeSessions'
ALTER TABLE [dbo].[TypeSessions]
ADD CONSTRAINT [PK_TypeSessions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Utilisateurs_Employe'
ALTER TABLE [dbo].[Utilisateurs_Employe]
ADD CONSTRAINT [PK_Utilisateurs_Employe]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Utilisateurs_Client'
ALTER TABLE [dbo].[Utilisateurs_Client]
ADD CONSTRAINT [PK_Utilisateurs_Client]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Sessions_Id], [Vehicules_Id] in table 'SessionVehicule'
ALTER TABLE [dbo].[SessionVehicule]
ADD CONSTRAINT [PK_SessionVehicule]
    PRIMARY KEY NONCLUSTERED ([Sessions_Id], [Vehicules_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [EmployeId] in table 'Articles'
ALTER TABLE [dbo].[Articles]
ADD CONSTRAINT [FK_EmployeArticle]
    FOREIGN KEY ([EmployeId])
    REFERENCES [dbo].[Utilisateurs_Employe]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeArticle'
CREATE INDEX [IX_FK_EmployeArticle]
ON [dbo].[Articles]
    ([EmployeId]);
GO

-- Creating foreign key on [CircuitId] in table 'Dates'
ALTER TABLE [dbo].[Dates]
ADD CONSTRAINT [FK_CircuitDate]
    FOREIGN KEY ([CircuitId])
    REFERENCES [dbo].[Circuits]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CircuitDate'
CREATE INDEX [IX_FK_CircuitDate]
ON [dbo].[Dates]
    ([CircuitId]);
GO

-- Creating foreign key on [CircuitId] in table 'Sessions'
ALTER TABLE [dbo].[Sessions]
ADD CONSTRAINT [FK_SessionCircuit]
    FOREIGN KEY ([CircuitId])
    REFERENCES [dbo].[Circuits]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SessionCircuit'
CREATE INDEX [IX_FK_SessionCircuit]
ON [dbo].[Sessions]
    ([CircuitId]);
GO

-- Creating foreign key on [EmployeId] in table 'Dates'
ALTER TABLE [dbo].[Dates]
ADD CONSTRAINT [FK_EmployeDate]
    FOREIGN KEY ([EmployeId])
    REFERENCES [dbo].[Utilisateurs_Employe]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeDate'
CREATE INDEX [IX_FK_EmployeDate]
ON [dbo].[Dates]
    ([EmployeId]);
GO

-- Creating foreign key on [Date_Id] in table 'Sessions'
ALTER TABLE [dbo].[Sessions]
ADD CONSTRAINT [FK_SessionDate]
    FOREIGN KEY ([Date_Id])
    REFERENCES [dbo].[Dates]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SessionDate'
CREATE INDEX [IX_FK_SessionDate]
ON [dbo].[Sessions]
    ([Date_Id]);
GO

-- Creating foreign key on [VehiculeId] in table 'Dates'
ALTER TABLE [dbo].[Dates]
ADD CONSTRAINT [FK_VehiculeDate]
    FOREIGN KEY ([VehiculeId])
    REFERENCES [dbo].[Vehicules]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_VehiculeDate'
CREATE INDEX [IX_FK_VehiculeDate]
ON [dbo].[Dates]
    ([VehiculeId]);
GO

-- Creating foreign key on [ForfaitReferenceId] in table 'OffresReferences'
ALTER TABLE [dbo].[OffresReferences]
ADD CONSTRAINT [FK_ForfaitReferenceOffresReference]
    FOREIGN KEY ([ForfaitReferenceId])
    REFERENCES [dbo].[ForfaitReferences]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ForfaitReferenceOffresReference'
CREATE INDEX [IX_FK_ForfaitReferenceOffresReference]
ON [dbo].[OffresReferences]
    ([ForfaitReferenceId]);
GO

-- Creating foreign key on [ClientId] in table 'Offres'
ALTER TABLE [dbo].[Offres]
ADD CONSTRAINT [FK_ClientOffre]
    FOREIGN KEY ([ClientId])
    REFERENCES [dbo].[Utilisateurs_Client]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientOffre'
CREATE INDEX [IX_FK_ClientOffre]
ON [dbo].[Offres]
    ([ClientId]);
GO

-- Creating foreign key on [OffreId] in table 'OffreSessions'
ALTER TABLE [dbo].[OffreSessions]
ADD CONSTRAINT [FK_OffreOffreSession]
    FOREIGN KEY ([OffreId])
    REFERENCES [dbo].[Offres]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OffreOffreSession'
CREATE INDEX [IX_FK_OffreOffreSession]
ON [dbo].[OffreSessions]
    ([OffreId]);
GO

-- Creating foreign key on [SessionId] in table 'OffreSessions'
ALTER TABLE [dbo].[OffreSessions]
ADD CONSTRAINT [FK_OffreSessionSession]
    FOREIGN KEY ([SessionId])
    REFERENCES [dbo].[Sessions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OffreSessionSession'
CREATE INDEX [IX_FK_OffreSessionSession]
ON [dbo].[OffreSessions]
    ([SessionId]);
GO

-- Creating foreign key on [EmployeId] in table 'Sessions'
ALTER TABLE [dbo].[Sessions]
ADD CONSTRAINT [FK_EmployeSession]
    FOREIGN KEY ([EmployeId])
    REFERENCES [dbo].[Utilisateurs_Employe]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeSession'
CREATE INDEX [IX_FK_EmployeSession]
ON [dbo].[Sessions]
    ([EmployeId]);
GO

-- Creating foreign key on [Sessions_Id] in table 'SessionVehicule'
ALTER TABLE [dbo].[SessionVehicule]
ADD CONSTRAINT [FK_SessionVehicule_Session]
    FOREIGN KEY ([Sessions_Id])
    REFERENCES [dbo].[Sessions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Vehicules_Id] in table 'SessionVehicule'
ALTER TABLE [dbo].[SessionVehicule]
ADD CONSTRAINT [FK_SessionVehicule_Vehicule]
    FOREIGN KEY ([Vehicules_Id])
    REFERENCES [dbo].[Vehicules]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SessionVehicule_Vehicule'
CREATE INDEX [IX_FK_SessionVehicule_Vehicule]
ON [dbo].[SessionVehicule]
    ([Vehicules_Id]);
GO

-- Creating foreign key on [TypeSessionId] in table 'Sessions'
ALTER TABLE [dbo].[Sessions]
ADD CONSTRAINT [FK_SessionTypeSession]
    FOREIGN KEY ([TypeSessionId])
    REFERENCES [dbo].[TypeSessions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SessionTypeSession'
CREATE INDEX [IX_FK_SessionTypeSession]
ON [dbo].[Sessions]
    ([TypeSessionId]);
GO

-- Creating foreign key on [RoleId] in table 'Utilisateurs'
ALTER TABLE [dbo].[Utilisateurs]
ADD CONSTRAINT [FK_Utilisateurs_Roles]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Utilisateurs_Roles'
CREATE INDEX [IX_FK_Utilisateurs_Roles]
ON [dbo].[Utilisateurs]
    ([RoleId]);
GO

-- Creating foreign key on [Id] in table 'Utilisateurs_Employe'
ALTER TABLE [dbo].[Utilisateurs_Employe]
ADD CONSTRAINT [FK_Employe_inherits_Utilisateur]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Utilisateurs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Utilisateurs_Client'
ALTER TABLE [dbo].[Utilisateurs_Client]
ADD CONSTRAINT [FK_Client_inherits_Utilisateur]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Utilisateurs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------