﻿-- 
-- Script was generated by Devart dbForge Studio 2019 for MySQL, Version 8.2.23.0
-- Product Home Page: http://www.devart.com/dbforge/mysql/studio
-- Script date 8/21/2021 8:33:00 PM
-- Server version: 10.1.47
-- Run this script against `Web06.TEST.MF917.PTDuyen` to synchronize it with Web06_TEST_MF917_PTDuyen
-- 

--
-- Disable foreign keys
--
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;

SET NAMES 'utf8';

--
-- Set default database
--
USE `Web06.TEST.MF917.PTDuyen`;

--
-- Create table `Department`
--
CREATE TABLE Department (
  DepartmentId CHAR(36) NOT NULL COMMENT 'Id đơn vị nhân viên',
  DepartmentName VARCHAR(255) NOT NULL COMMENT 'tên đơn vị',
  Description VARCHAR(255) DEFAULT NULL COMMENT 'mô tả bộ phận',
  CreatedBy VARCHAR(100) DEFAULT NULL COMMENT 'người tạo',
  CreatedDate DATETIME DEFAULT NULL COMMENT 'ngày tạo',
  ModifiedBy VARCHAR(100) DEFAULT NULL COMMENT 'người sửa',
  ModifiedDate DATETIME DEFAULT NULL COMMENT 'ngày sửa',
  PRIMARY KEY (DepartmentId)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

DELIMITER $$

--
-- Create procedure `Proc_UpdateDepartment`
--
CREATE PROCEDURE Proc_UpdateDepartment(
  IN DepartmentId char(36),
  IN DepartmentName varchar(255),
  IN Description varchar(255),
  IN ModifiedBy varchar(100))
BEGIN
    UPDATE Department d
      set
      d.DepartmentName = DepartmentName,
      d.Description = Description,
      d.ModifiedDate = CURDATE(),
      d.ModifiedBy = ModifiedBy
      WHERE d.DepartmentId = DepartmentId;
  END
$$

--
-- Create procedure `Proc_PostDepartment`
--
CREATE PROCEDURE Proc_PostDepartment(
  IN DepartmentName varchar(255),
  IN Description varchar(255),
  IN CreatedBy varchar(100))
BEGIN
    INSERT INTO Department (DepartmentId, DepartmentName, Description, CreatedDate, CreatedBy)
            VALUES(UUID(), DepartmentName, Description, CURDATE(), CreatedBy);
  END
$$

--
-- Create procedure `Proc_GetDepartmentById`
--
CREATE PROCEDURE Proc_GetDepartmentById(IN DepartmentId char(36))
BEGIN
    SELECT * FROM Department d WHERE d.DepartmentId = DepartmentId;
  END
$$

--
-- Create procedure `Proc_GetDepartment`
--
CREATE PROCEDURE Proc_GetDepartment()
BEGIN
    SELECT * FROM Department ;
  END
$$

--
-- Create procedure `Proc_DeleteDepartmentById`
--
CREATE PROCEDURE Proc_DeleteDepartmentById(IN DepartmentId char(36))
BEGIN 
  DELETE FROM Department
    WHERE Department.DepartmentId = DepartmentId;
  END
$$

DELIMITER ;

--
-- Create table `Employee`
--
CREATE TABLE Employee (
  EmployeeId CHAR(36) NOT NULL COMMENT 'Id nhân viên',
  EmployeeCode VARCHAR(20) NOT NULL COMMENT 'mã nhân viên',
  EmployeeName VARCHAR(100) NOT NULL COMMENT 'tên nhân viên',
  DateOfBirth DATE DEFAULT NULL COMMENT 'ngày sinh',
  Gender INT(11) DEFAULT NULL COMMENT 'giới tính: 0-nam; 1-nữ; 2-khác',
  DepartmentId CHAR(36) NOT NULL COMMENT 'Id của đơn vị nhân viên',
  IdentityNumber VARCHAR(20) DEFAULT NULL COMMENT 'số chứng minh thư nhân dân/ thẻ căn cước công dân',
  IdentityDate DATE DEFAULT NULL COMMENT 'ngày cấp CMTND/ thẻ căn cước công dân',
  IdentityPlace VARCHAR(255) DEFAULT NULL COMMENT 'nơi cấp CMTND/ thẻ căn cước công dân',
  EmployeePosition VARCHAR(255) DEFAULT NULL COMMENT 'chức danh nhân viên',
  Address VARCHAR(255) DEFAULT NULL COMMENT 'địa chỉ nhân viên',
  BankAccountNumber VARCHAR(20) DEFAULT NULL,
  BankName VARCHAR(255) DEFAULT NULL,
  BankBranchName VARCHAR(255) DEFAULT NULL COMMENT 'Tên chi nhánh  ngân hàng',
  BankProvinceName VARCHAR(255) DEFAULT NULL,
  PhoneNumber VARCHAR(20) DEFAULT NULL COMMENT 'số điện thoại di động',
  TelephoneNumber VARCHAR(20) DEFAULT NULL COMMENT 'số điện thoại cố định',
  Email VARCHAR(50) DEFAULT NULL COMMENT 'email nhân viên',
  CreatedBy VARCHAR(100) DEFAULT NULL COMMENT 'người tạo',
  CreatedDate DATE DEFAULT NULL COMMENT 'ngày tạo',
  ModifiedBy VARCHAR(100) DEFAULT NULL COMMENT 'người sửa',
  ModifiedDate DATE DEFAULT NULL COMMENT 'ngày sửa',
  PRIMARY KEY (EmployeeId)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Create index `UK_Employee_EmployeeCode` on table `Employee`
--
ALTER TABLE Employee 
  ADD UNIQUE INDEX UK_Employee_EmployeeCode(EmployeeCode);

--
-- Create foreign key
--
ALTER TABLE Employee 
  ADD CONSTRAINT FK_Employee_DepartmentId FOREIGN KEY (DepartmentId)
    REFERENCES Department(DepartmentId) ON DELETE NO ACTION;

DELIMITER $$

--
-- Create procedure `Proc_UpdateEmployee`
--
CREATE PROCEDURE Proc_UpdateEmployee(
  IN EmployeeId CHAR(36), 
  IN EmployeeCode varchar(20),
  IN EmployeeName varchar(100),
  IN DateOfBirth date,
  IN Gender int(11),
  IN DepartmentId char(36),
  IN IdentityNumber varchar(20),
  IN IdentityDate date ,
  IN IdentityPlace varchar(255),
  IN EmployeePosition varchar(255),
  IN Address varchar(255),
  IN BankAccountNumber varchar(20),
  IN BankName varchar(255),
  IN BankBranchName varchar(255),
  IN BankProvinceName varchar(255),
  IN PhoneNumber varchar(20),
  IN TelephoneNumber varchar(20),
  IN Email varchar(50),
  IN ModifiedBy varchar(100))
BEGIN
  UPDATE Employee e
  SET 
  e.EmployeeCode = EmployeeCode,
  e.EmployeeName = EmployeeName,
  e.Gender = Gender,
  e.DateOfBirth = DateOfBirth,
  e.PhoneNumber = PhoneNumber,
  e.Email = Email,
  e.Address = Address,
  e.EmployeePosition = EmployeePosition,
  e.TelephoneNumber = TelephoneNumber,
  e.BankAccountNumber = BankAccountNumber,
  e.BankName = BankName,
  e.BankProvinceName = BankProvinceName,
  e.BankBranchName = BankBranchName,
  e.IdentityNumber = IdentityNumber,
  e.DepartmentId = DepartmentId,
  e.ModifiedBy = ModifiedBy,
  e.ModifiedDate = CURDATE(),
  e.IdentityDate = IdentityDate,
  e.IdentityPlace = IdentityPlace
  WHERE e.EmployeeId = EmployeeId;
END
$$

--
-- Create procedure `Proc_PostEmployee`
--
CREATE PROCEDURE Proc_PostEmployee(
  IN EmployeeCode varchar(20),
  IN EmployeeName varchar(100),
  IN DateOfBirth date,
  IN Gender int(11),
  IN DepartmentId char(36),
  IN IdentityNumber varchar(20),
  IN IdentityDate date ,
  IN IdentityPlace varchar(255),
  IN EmployeePosition varchar(255),
  IN Address varchar(255),
  IN BankAccountNumber varchar(20),
  IN BankName varchar(255),
  IN BankBranchName varchar(255),
  IN BankProvinceName varchar(255),
  IN PhoneNumber varchar(20),
  IN TelephoneNumber varchar(20),
  IN Email varchar(50),
  IN CreatedBy varchar(100))
BEGIN
    INSERT INTO Employee (EmployeeId, EmployeeCode, EmployeeName, DateOfBirth, Gender, DepartmentId, IdentityNumber, IdentityDate, IdentityPlace, 
                          EmployeePosition, Address, BankAccountNumber, BankName, BankBranchName, BankProvinceName, PhoneNumber, TelephoneNumber, Email,
                          CreatedDate, CreatedBy) 
  VALUES (UUID(),EmployeeCode, EmployeeName, DateOfBirth, Gender, DepartmentId, IdentityNumber, IdentityDate, IdentityPlace, 
          EmployeePosition, Address, BankAccountNumber, BankName, BankBranchName, BankProvinceName, PhoneNumber, TelephoneNumber, Email,
          CURDATE(), CreatedBy);
  END
$$

--
-- Create procedure `Proc_DeleteEmployeeById`
--
CREATE PROCEDURE Proc_DeleteEmployeeById(IN EmployeeId char(36))
BEGIN 
  DELETE FROM Employee
    WHERE Employee.EmployeeId = EmployeeId;
  END
$$

DELIMITER ;

--
-- Create view `View_Employee`
--
CREATE
VIEW View_Employee
AS
SELECT
  `e`.`EmployeeId` AS `EmployeeId`,
  `e`.`EmployeeCode` AS `EmployeeCode`,
  `e`.`EmployeeName` AS `EmployeeName`,
  `e`.`Gender` AS `Gender`,
  `e`.`DateOfBirth` AS `DateOfBirth`,
  `e`.`PhoneNumber` AS `PhoneNumber`,
  `e`.`Email` AS `Email`,
  `e`.`Address` AS `Address`,
  `e`.`EmployeePosition` AS `EmployeePosition`,
  `e`.`TelephoneNumber` AS `TelephoneNumber`,
  `e`.`BankAccountNumber` AS `BankAccountNumber`,
  `e`.`BankName` AS `BankName`,
  `e`.`BankProvinceName` AS `BankProvinceName`,
  `e`.`BankBranchName` AS `BankBranchName`,
  `e`.`IdentityNumber` AS `IdentityNumber`,
  `e`.`DepartmentId` AS `DepartmentId`,
  `d`.`DepartmentName` AS `DepartmentName`,
  `e`.`CreatedBy` AS `CreatedBy`,
  `e`.`CreatedDate` AS `CreatedDate`,
  `e`.`ModifiedBy` AS `ModifiedBy`,
  `e`.`ModifiedDate` AS `ModifiedDate`,
  `e`.`IdentityDate` AS `IdentityDate`,
  `e`.`IdentityPlace` AS `IdentityPlace`
FROM (`Employee` `e`
  LEFT JOIN `Department` `d`
    ON ((`e`.`DepartmentId` = `d`.`DepartmentId`)))
ORDER BY `e`.`CreatedDate` DESC, `e`.`EmployeeCode` DESC;

DELIMITER $$

--
-- Create procedure `Proc_GetNewEmployeeCode`
--
CREATE PROCEDURE Proc_GetNewEmployeeCode()
BEGIN
      SELECT CONCAT('NV-',(MAX(CAST(SUBSTR(EmployeeCode, 4, LENGTH(EmployeeCode) - 3) AS INT)+1))) AS newCode FROM View_Employee;
    END
$$

--
-- Create procedure `Proc_GetEmployeeFilterPaging`
--
CREATE PROCEDURE Proc_GetEmployeeFilterPaging(IN EmployeeFilter varchar(255), 
                                        IN PageNumber int, IN PageSize int, OUT TotalRecord int, OUT TotalPage int)
BEGIN
    DECLARE allRecord int;
    DECLARE recordLastPage int;
    DECLARE row int;
    DECLARE recordsPerPage int;
    IF PageNumber > 0 THEN 
      set allRecord =(select COUNT(*) FROM View_Employee ve);
      CREATE TEMPORARY TABLE FilterEmployee
        (SELECT * FROM View_Employee ve 
         WHERE ((INSTR( ve.EmployeeCode, EmployeeFilter) > 0) OR (INSTR( ve.EmployeeName, EmployeeFilter) > 0)));
      set TotalRecord = (SELECT COUNT(*) FROM FilterEmployee);
      DROP TABLE FilterEmployee;
      set row = (PageNumber - 1)*PageSize;
      set recordLastPage = allRecord % PageSize;
      IF recordLastPage > 0 THEN
        set TotalPage = FLOOR(allRecord/PageSize) + 1;
        IF PageNumber < TotalPage THEN
          SET recordsPerPage = PageSize;
        ELSE 
          SET recordsPerPage = recordLastPage;
        END IF; 
      ELSE 
        set TotalPage = allRecord/PageSize; 
        SET recordsPerPage = PageSize;
      END IF;
      CREATE TEMPORARY TABLE EmployeePage(SELECT * FROM View_Employee ve LIMIT row, recordsPerPage);
      SELECT * FROM EmployeePage ep WHERE ((INSTR(ep.EmployeeCode, EmployeeFilter) > 0) OR (INSTR( ep.EmployeeName, EmployeeFilter) > 0));
      DROP TABLE EmployeePage;
    ELSE SELECT 'Không có dữ liệu phù hợp';
    END IF;
  END
$$

--
-- Create procedure `Proc_GetEmployeeById`
--
CREATE PROCEDURE Proc_GetEmployeeById(IN EmployeeId char(36))
BEGIN
    SELECT * FROM View_Employee ve WHERE ve.EmployeeId = EmployeeId;
  END
$$

--
-- Create procedure `Proc_GetEmployeeByCode`
--
CREATE PROCEDURE Proc_GetEmployeeByCode(IN EmployeeCode varchar(20))
BEGIN
    SELECT * FROM View_Employee ve WHERE ve.EmployeeCode = EmployeeCode;
  END
$$

--
-- Create procedure `Proc_GetEmployee`
--
CREATE PROCEDURE Proc_GetEmployee()
BEGIN
    SELECT * FROM View_Employee ;
  END
$$

DELIMITER ;

--
-- Enable foreign keys
--
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;