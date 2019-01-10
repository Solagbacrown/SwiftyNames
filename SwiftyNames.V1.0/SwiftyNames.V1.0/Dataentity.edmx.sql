
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/17/2015 16:02:52
-- Generated from EDMX file: C:\Users\gbenga\Documents\E-Resolver Runtime Technologies\SwiftyNames.V1.0\SwiftyNames.V1.0\Dataentity.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SwiftyNames];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Deliveries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Deliveries];
GO
IF OBJECT_ID(N'[dbo].[NewsPapersPrice]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NewsPapersPrice];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Deliveries'
CREATE TABLE [dbo].[Deliveries] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PhoneNumber] nvarchar(50)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [DateOrdered] datetime  NOT NULL
);
GO

-- Creating table 'NewsPapersPrices'
CREATE TABLE [dbo].[NewsPapersPrices] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NewsPaper] nvarchar(max)  NOT NULL,
    [Price] decimal(18,0)  NOT NULL
);
GO

-- Creating table 'Forms'
CREATE TABLE [dbo].[Forms] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [CountryId] nvarchar(max)  NOT NULL,
    [CurrentAddress] nvarchar(max)  NOT NULL,
    [CurrentCountryId] int  NOT NULL,
    [IsMarried] bit  NOT NULL,
    [IsDivorced] bit  NOT NULL,
    [IsCourtAvoid] bit  NOT NULL,
    [IsConvicted] bit  NOT NULL,
    [CurrentName] nvarchar(max)  NOT NULL,
    [Street] nvarchar(max)  NOT NULL,
    [AddressCountryId] int  NOT NULL,
    [StateId] int  NOT NULL,
    [PhoneNumbers] nvarchar(max)  NOT NULL,
    [ProposedName] nvarchar(max)  NOT NULL,
    [ReasonId] int  NOT NULL,
    [DailiesID] int  NOT NULL,
    [DeliveryphoneAddress] nvarchar(max)  NOT NULL,
    [DeliveryAddress] nvarchar(max)  NOT NULL,
    [DateOrdered] nvarchar(max)  NOT NULL,
    [IsDeliveredOutside] nvarchar(max)  NOT NULL,
    [EmbassyDelivery] nvarchar(max)  NOT NULL,
    [DeliveryCountry] int  NOT NULL,
    [FullAddress] nvarchar(max)  NOT NULL,
    [EmbassyAdrress] nvarchar(max)  NOT NULL,
    [HowUHear] nvarchar(max)  NOT NULL,
    [ServiceComment] nvarchar(max)  NOT NULL,
    [Nationality_NationalityId] int  NOT NULL,
    [State_Id] int  NOT NULL,
    [Reason_Id] int  NOT NULL,
    [NewsPapersPrice_Id] int  NOT NULL,
    [Delivery_Id] int  NOT NULL
);
GO

-- Creating table 'Nationalities'
CREATE TABLE [dbo].[Nationalities] (
    [NationalityId] int IDENTITY(1,1) NOT NULL,
    [Country] varbinary(max)  NOT NULL,
    [Abbreviation] nvarchar(max)  NOT NULL,
    [Adjective] nvarchar(max)  NOT NULL,
    [Person] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'States'
CREATE TABLE [dbo].[States] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StateName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Reasons'
CREATE TABLE [dbo].[Reasons] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NameChangeReasons] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Deliveries'
ALTER TABLE [dbo].[Deliveries]
ADD CONSTRAINT [PK_Deliveries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NewsPapersPrices'
ALTER TABLE [dbo].[NewsPapersPrices]
ADD CONSTRAINT [PK_NewsPapersPrices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Forms'
ALTER TABLE [dbo].[Forms]
ADD CONSTRAINT [PK_Forms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [NationalityId] in table 'Nationalities'
ALTER TABLE [dbo].[Nationalities]
ADD CONSTRAINT [PK_Nationalities]
    PRIMARY KEY CLUSTERED ([NationalityId] ASC);
GO

-- Creating primary key on [Id] in table 'States'
ALTER TABLE [dbo].[States]
ADD CONSTRAINT [PK_States]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Reasons'
ALTER TABLE [dbo].[Reasons]
ADD CONSTRAINT [PK_Reasons]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Nationality_NationalityId] in table 'Forms'
ALTER TABLE [dbo].[Forms]
ADD CONSTRAINT [FK_NationalityForm]
    FOREIGN KEY ([Nationality_NationalityId])
    REFERENCES [dbo].[Nationalities]
        ([NationalityId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NationalityForm'
CREATE INDEX [IX_FK_NationalityForm]
ON [dbo].[Forms]
    ([Nationality_NationalityId]);
GO

-- Creating foreign key on [State_Id] in table 'Forms'
ALTER TABLE [dbo].[Forms]
ADD CONSTRAINT [FK_StateForm]
    FOREIGN KEY ([State_Id])
    REFERENCES [dbo].[States]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StateForm'
CREATE INDEX [IX_FK_StateForm]
ON [dbo].[Forms]
    ([State_Id]);
GO

-- Creating foreign key on [Reason_Id] in table 'Forms'
ALTER TABLE [dbo].[Forms]
ADD CONSTRAINT [FK_ReasonForm]
    FOREIGN KEY ([Reason_Id])
    REFERENCES [dbo].[Reasons]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ReasonForm'
CREATE INDEX [IX_FK_ReasonForm]
ON [dbo].[Forms]
    ([Reason_Id]);
GO

-- Creating foreign key on [NewsPapersPrice_Id] in table 'Forms'
ALTER TABLE [dbo].[Forms]
ADD CONSTRAINT [FK_NewsPapersPriceForm]
    FOREIGN KEY ([NewsPapersPrice_Id])
    REFERENCES [dbo].[NewsPapersPrices]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NewsPapersPriceForm'
CREATE INDEX [IX_FK_NewsPapersPriceForm]
ON [dbo].[Forms]
    ([NewsPapersPrice_Id]);
GO

-- Creating foreign key on [Delivery_Id] in table 'Forms'
ALTER TABLE [dbo].[Forms]
ADD CONSTRAINT [FK_DeliveryForm]
    FOREIGN KEY ([Delivery_Id])
    REFERENCES [dbo].[Deliveries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DeliveryForm'
CREATE INDEX [IX_FK_DeliveryForm]
ON [dbo].[Forms]
    ([Delivery_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------