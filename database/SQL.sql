
CREATE DATABASE `Web06.TEST.MF917.PTDuyen`

CREATE TABLE Department(
  DepartmentId char(36) NOT NULL COMMENT 'Id don v? nh�n vi�n',
  DepartmentName varchar(255) NOT NULL COMMENT 't�n don v?',
  Description varchar(255) DEFAULT NULL COMMENT 'm� t? b? ph?n',
  CreatedBy varchar(100) DEFAULT NULL COMMENT 'ngu?i t?o',
  CreatedDate datetime DEFAULT NULL COMMENT 'ng�y t?o',
  ModifiedBy varchar(100) DEFAULT NULL COMMENT 'ngu?i s?a',
  ModifiedDate datetime DEFAULT NULL COMMENT 'ng�y s?a',
  PRIMARY KEY (DepartmentId)  
)

CREATE TABLE Employee (
  EmployeeId char(36) NOT NULL COMMENT 'Id nh�n vi�n',
  EmployeeCode varchar(20) NOT NULL COMMENT 'm� nh�n vi�n',
  EmployeeName varchar(100) NOT NULL COMMENT 't�n nh�n vi�n',
  DateOfBirth date DEFAULT NULL COMMENT 'ng�y sinh',
  Gender int(11) DEFAULT NULL COMMENT 'gi?i t�nh: 0-nam; 1-n?; 2-kh�c',
  DepartmentId char(36) NOT NULL COMMENT 'Id c?a don v? nh�n vi�n',
  IdentityNumber varchar(20) DEFAULT NULL COMMENT 's? ch?ng minh thu nh�n d�n/ th? can cu?c c�ng d�n',
  IdentityDate date DEFAULT NULL COMMENT 'ng�y c?p CMTND/ th? can cu?c c�ng d�n',
  IdentityPlace varchar(255) DEFAULT NULL COMMENT 'noi c?p CMTND/ th? can cu?c c�ng d�n',
  EmployeePosition varchar(255) DEFAULT NULL COMMENT 'ch?c danh nh�n vi�n',
  Address varchar(255) DEFAULT NULL COMMENT 'd?a ch? nh�n vi�n',
  BankAccountNumber varchar(20) DEFAULT NULL,
  BankName varchar(255) DEFAULT NULL,
  BankBranchName varchar(255) DEFAULT NULL COMMENT 'T�n chi nh�nh  ng�n h�ng',
  BankProvinceName varchar(255) DEFAULT NULL,
  PhoneNumber varchar(20) DEFAULT NULL COMMENT 's? di?n tho?i di d?ng',
  TelephoneNumber varchar(20) DEFAULT NULL COMMENT 's? di?n tho?i c? d?nh',
  Email varchar(50) DEFAULT NULL COMMENT 'email nh�n vi�n',
  CreatedBy varchar(100) DEFAULT NULL COMMENT 'ngu?i t?o',
  CreatedDate date DEFAULT NULL COMMENT 'ng�y t?o',
  ModifiedBy varchar(100) DEFAULT NULL COMMENT 'ngu?i s?a',
  ModifiedDate date DEFAULT NULL COMMENT 'ng�y s?a',
  PRIMARY KEY (EmployeeId)  
)

ALTER TABLE Employee
ADD UNIQUE INDEX UK_Employee_EmployeeCode (EmployeeCode);

ALTER TABLE Employee
ADD CONSTRAINT FK_Employee_DepartmentId FOREIGN KEY (DepartmentId)
REFERENCES Department (DepartmentId) ON DELETE NO ACTION;

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

CREATE PROCEDURE Proc_GetNewEmployeeCode()
    BEGIN
      SELECT CONCAT('NV-',(MAX(CAST(SUBSTR(EmployeeCode, 4, LENGTH(EmployeeCode) - 3) AS INT)+1))) AS newCode FROM View_Employee;
    END


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
    ELSE SELECT 'Kh�ng c� d? li?u ph� h?p';
    END IF;
  END

CREATE PROCEDURE Proc_GetEmployee()
  BEGIN
    SELECT * FROM View_Employee ;
  END

CREATE PROCEDURE Proc_PostDepartment(
  IN DepartmentName varchar(255),
  IN Description varchar(255),
  IN CreatedBy varchar(100))
  BEGIN
    INSERT INTO Department (DepartmentId, DepartmentName, Description, CreatedDate, CreatedBy)
            VALUES(UUID(), DepartmentName, Description, CURDATE(), CreatedBy);
  END

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

CREATE PROCEDURE Proc_DeleteEmployeeById(IN EmployeeId char(36))
BEGIN 
  DELETE FROM Employee
    WHERE Employee.EmployeeId = EmployeeId;
  END

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

  CALL PROCEDURE Proc_GetEmployeeFilterPaging('n',)
