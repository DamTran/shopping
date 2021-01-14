-- ----------------------------------------------------------------------------
-- MySQL Workbench Migration
-- Migrated Schemata: 2KfyYKMJaF
-- Source Schemata: 2KfyYKMJaF
-- Created: Sun Jan  3 21:06:13 2021
-- Workbench Version: 8.0.20
-- ----------------------------------------------------------------------------
SET GLOBAL default_storage_engine = 'InnoDB';
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------------------------------------------------------
-- Schema 2KfyYKMJaF
-- ----------------------------------------------------------------------------
DROP SCHEMA IF EXISTS `2KfyYKMJaF` ;
CREATE SCHEMA IF NOT EXISTS `2KfyYKMJaF` ;

-- ----------------------------------------------------------------------------
-- Table 2KfyYKMJaF.__EFMigrationsHistory
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `2KfyYKMJaF`.`__EFMigrationsHistory` (
  `MigrationId` VARCHAR(150) CHARACTER SET 'utf8mb4' NOT NULL,
  `ProductVersion` VARCHAR(32) CHARACTER SET 'utf8mb4' NOT NULL,
  PRIMARY KEY (`MigrationId`));

-- ----------------------------------------------------------------------------
-- Table 2KfyYKMJaF.AspNetRoles
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `2KfyYKMJaF`.`AspNetRoles` (
  `Id` VARCHAR(36) CHARACTER SET 'utf8mb4' NOT NULL,
  `Name` VARCHAR(36) CHARACTER SET 'utf8mb4' NULL,
  `NormalizedName` VARCHAR(36) CHARACTER SET 'utf8mb4' NULL,
  `ConcurrencyStamp` LONGTEXT CHARACTER SET 'utf8mb4' NULL,
  PRIMARY KEY (`Id`(36)),
  UNIQUE INDEX `RoleNameIndex` (`NormalizedName`(36) ASC));

-- ----------------------------------------------------------------------------
-- Table 2KfyYKMJaF.AspNetUsers
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `2KfyYKMJaF`.`AspNetUsers` (
  `Id` VARCHAR(36) CHARACTER SET 'utf8mb4' NOT NULL,
  `UserName` VARCHAR(36) CHARACTER SET 'utf8mb4' NULL,
  `NormalizedUserName` VARCHAR(36) CHARACTER SET 'utf8mb4' NULL,
  `Email` VARCHAR(36) CHARACTER SET 'utf8mb4' NULL,
  `NormalizedEmail` VARCHAR(36) CHARACTER SET 'utf8mb4' NULL,
  `EmailConfirmed` TINYINT(1) NOT NULL,
  `PasswordHash` LONGTEXT CHARACTER SET 'utf8mb4' NULL,
  `SecurityStamp` LONGTEXT CHARACTER SET 'utf8mb4' NULL,
  `ConcurrencyStamp` LONGTEXT CHARACTER SET 'utf8mb4' NULL,
  `PhoneNumber` LONGTEXT CHARACTER SET 'utf8mb4' NULL,
  `PhoneNumberConfirmed` TINYINT(1) NOT NULL,
  `TwoFactorEnabled` TINYINT(1) NOT NULL,
  `LockoutEnd` DATETIME(6) NULL,
  `LockoutEnabled` TINYINT(1) NOT NULL,
  `AccessFailedCount` INT NOT NULL,
  `Discriminator` LONGTEXT CHARACTER SET 'utf8mb4' NOT NULL,
  `FirstName` LONGTEXT CHARACTER SET 'utf8mb4' NULL,
  `LastName` LONGTEXT CHARACTER SET 'utf8mb4' NULL,
  PRIMARY KEY (`Id`(36)),
  INDEX `EmailIndex` (`NormalizedEmail`(36) ASC),
  UNIQUE INDEX `UserNameIndex` (`NormalizedUserName`(36) ASC));

-- ----------------------------------------------------------------------------
-- Table 2KfyYKMJaF.Category
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `2KfyYKMJaF`.`Category` (
  `Id` INT NOT NULL,
  `Name` LONGTEXT CHARACTER SET 'utf8mb4' NOT NULL,
  `DisplayOrder` INT NOT NULL,
  PRIMARY KEY (`Id`));

-- ----------------------------------------------------------------------------
-- Table 2KfyYKMJaF.AspNetRoleClaims
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `2KfyYKMJaF`.`AspNetRoleClaims` (
  `Id` INT NOT NULL,
  `RoleId` VARCHAR(36) CHARACTER SET 'utf8mb4' NOT NULL,
  `ClaimType` LONGTEXT CHARACTER SET 'utf8mb4' NULL,
  `ClaimValue` LONGTEXT CHARACTER SET 'utf8mb4' NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_AspNetRoleClaims_RoleId` (`RoleId`(36) ASC));

-- ----------------------------------------------------------------------------
-- Table 2KfyYKMJaF.AspNetUserClaims
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `2KfyYKMJaF`.`AspNetUserClaims` (
  `Id` INT NOT NULL,
  `UserId` VARCHAR(36) CHARACTER SET 'utf8mb4' NOT NULL,
  `ClaimType` LONGTEXT CHARACTER SET 'utf8mb4' NULL,
  `ClaimValue` LONGTEXT CHARACTER SET 'utf8mb4' NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_AspNetUserClaims_UserId` (`UserId`(36) ASC));

-- ----------------------------------------------------------------------------
-- Table 2KfyYKMJaF.AspNetUserLogins
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `2KfyYKMJaF`.`AspNetUserLogins` (
  `LoginProvider` VARCHAR(36) CHARACTER SET 'utf8mb4' NOT NULL,
  `ProviderKey` VARCHAR(36) CHARACTER SET 'utf8mb4' NOT NULL,
  `ProviderDisplayName` LONGTEXT CHARACTER SET 'utf8mb4' NULL,
  `UserId` VARCHAR(36) CHARACTER SET 'utf8mb4' NOT NULL,
  PRIMARY KEY (`LoginProvider`(36), `ProviderKey`(36)),
  INDEX `IX_AspNetUserLogins_UserId` (`UserId`(36) ASC));

-- ----------------------------------------------------------------------------
-- Table 2KfyYKMJaF.AspNetUserRoles
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `2KfyYKMJaF`.`AspNetUserRoles` (
  `UserId` VARCHAR(36) CHARACTER SET 'utf8mb4' NOT NULL,
  `RoleId` VARCHAR(36) CHARACTER SET 'utf8mb4' NOT NULL,
  PRIMARY KEY (`UserId`(36), `RoleId`(36)),
  INDEX `IX_AspNetUserRoles_RoleId` (`RoleId`(36) ASC));

-- ----------------------------------------------------------------------------
-- Table 2KfyYKMJaF.AspNetUserTokens
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `2KfyYKMJaF`.`AspNetUserTokens` (
  `UserId` VARCHAR(36) CHARACTER SET 'utf8mb4' NOT NULL,
  `LoginProvider` VARCHAR(36) CHARACTER SET 'utf8mb4' NOT NULL,
  `Name` VARCHAR(36) CHARACTER SET 'utf8mb4' NOT NULL,
  `Value` LONGTEXT CHARACTER SET 'utf8mb4' NULL,
  PRIMARY KEY (`UserId`(36), `LoginProvider`(36), `Name`(36)));

-- ----------------------------------------------------------------------------
-- Table 2KfyYKMJaF.FoodType
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `2KfyYKMJaF`.`FoodType` (
  `Id` INT NOT NULL,
  `Name` LONGTEXT CHARACTER SET 'utf8mb4' NOT NULL,
  PRIMARY KEY (`Id`));

-- ----------------------------------------------------------------------------
-- Table 2KfyYKMJaF.MenuItem
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `2KfyYKMJaF`.`MenuItem` (
  `Id` INT NOT NULL,
  `Name` LONGTEXT CHARACTER SET 'utf8mb4' NOT NULL,
  `Description` LONGTEXT CHARACTER SET 'utf8mb4' NULL,
  `Image` LONGTEXT CHARACTER SET 'utf8mb4' NULL,
  `Price` DOUBLE NOT NULL,
  `CategoryId` INT NOT NULL,
  `FoodTypeId` INT NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_MenuItem_CategoryId` (`CategoryId` ASC),
  INDEX `IX_MenuItem_FoodTypeId` (`FoodTypeId` ASC));

-- ----------------------------------------------------------------------------
-- Table 2KfyYKMJaF.ShoppingCart
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `2KfyYKMJaF`.`ShoppingCart` (
  `Id` INT NOT NULL,
  `MenuItemId` INT NOT NULL,
  `ApplicationUserId` LONGTEXT CHARACTER SET 'utf8mb4' NULL,
  `Count` INT NOT NULL,
  PRIMARY KEY (`Id`));

-- ----------------------------------------------------------------------------
-- Table 2KfyYKMJaF.OrderHeader
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `2KfyYKMJaF`.`OrderHeader` (
  `Id` INT NOT NULL,
  `UserId` VARCHAR(36) CHARACTER SET 'utf8mb4' NOT NULL,
  `OrderDate` DATETIME(6) NOT NULL,
  `OrderTotal` DOUBLE NOT NULL,
  `PickUpTime` DATETIME(6) NOT NULL,
  `Status` LONGTEXT CHARACTER SET 'utf8mb4' NULL,
  `PaymentStatus` LONGTEXT CHARACTER SET 'utf8mb4' NULL,
  `Comments` LONGTEXT CHARACTER SET 'utf8mb4' NULL,
  `PickupName` LONGTEXT CHARACTER SET 'utf8mb4' NULL,
  `PhoneNumber` LONGTEXT CHARACTER SET 'utf8mb4' NULL,
  `TransactionId` LONGTEXT CHARACTER SET 'utf8mb4' NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_OrderHeader_UserId` (`UserId`(36) ASC));

-- ----------------------------------------------------------------------------
-- Table 2KfyYKMJaF.OrderDetails
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `2KfyYKMJaF`.`OrderDetails` (
  `Id` INT NOT NULL,
  `OrderId` INT NOT NULL,
  `MenuItemId` INT NOT NULL,
  `Count` INT NOT NULL,
  `Name` LONGTEXT CHARACTER SET 'utf8mb4' NULL,
  `Description` LONGTEXT CHARACTER SET 'utf8mb4' NULL,
  `Price` DOUBLE NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_OrderDetails_MenuItemId` (`MenuItemId` ASC),
  INDEX `IX_OrderDetails_OrderId` (`OrderId` ASC));
SET FOREIGN_KEY_CHECKS = 1;
