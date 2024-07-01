USE [master]
GO
/****** Object:  Database [DiamondStore]    Script Date: 7/3/2024 3:00:00 AM ******/
CREATE DATABASE [DiamondStore]

USE [DiamondStore]
GO

-- Create Role table
CREATE TABLE Role (
    RoleId UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    Id INT IDENTITY(1,1) NOT NULL,
    RoleName VARCHAR(50) NOT NULL
);

-- Create User table
CREATE TABLE "User" (
    UserId UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    Id INT IDENTITY(1,1) NOT NULL,
    Username VARCHAR(50) NOT NULL,
    Password VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    RoleId UNIQUEIDENTIFIER,
    FOREIGN KEY (RoleId) REFERENCES Role(RoleId)
);

-- Create Voucher table
CREATE TABLE Voucher (
    VoucherId UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    Id INT IDENTITY(1,1) NOT NULL,
    Name VARCHAR(50) NOT NULL,
    DiscountPercentage DECIMAL(5,2) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL
);

-- Create CustomerVoucher table
CREATE TABLE CustomerVoucher (
    CustomerVoucherId UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    Id INT IDENTITY(1,1) NOT NULL,
    UserId UNIQUEIDENTIFIER,
    VoucherId UNIQUEIDENTIFIER,
    Status VARCHAR(50) NOT NULL,
    FOREIGN KEY (UserId) REFERENCES "User"(UserId),
    FOREIGN KEY (VoucherId) REFERENCES Voucher(VoucherId)
);

-- Create Order table
CREATE TABLE "Order" (
    OrderId UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    Id INT IDENTITY(1,1) NOT NULL,
    UserId UNIQUEIDENTIFIER,
    OrderDate DATE NOT NULL,
    TotalAmount DECIMAL(10,2) NOT NULL,
    Status VARCHAR(50) NOT NULL,
    VoucherId UNIQUEIDENTIFIER,
    TotalPrice DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (UserId) REFERENCES "User"(UserId),
    FOREIGN KEY (VoucherId) REFERENCES Voucher(VoucherId)
);

-- Create Distributor table
CREATE TABLE Distributor (
    DistributorId UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    Id INT IDENTITY(1,1) NOT NULL,
    DistributorName VARCHAR(100) NOT NULL,
    Locate VARCHAR(100) NOT NULL
);

-- Create Category table
CREATE TABLE Category (
    CategoryId UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    Id INT IDENTITY(1,1) NOT NULL,
    GiaCertificate VARCHAR(50),
    Cut VARCHAR(50),
    Color VARCHAR(50),
    Clarity VARCHAR(50),
    Carat DECIMAL(5,2),
    DistributorId UNIQUEIDENTIFIER,
    FOREIGN KEY (DistributorId) REFERENCES Distributor(DistributorId)
);

-- Create Product table
CREATE TABLE Product (
    ProductId UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    Id INT IDENTITY(1,1) NOT NULL,
    Name VARCHAR(50) NOT NULL,
    Description TEXT,
    Price DECIMAL(10,2) NOT NULL,
    CategoryId UNIQUEIDENTIFIER,
    FOREIGN KEY (CategoryId) REFERENCES Category(CategoryId)
);

-- Create OrderItem table
CREATE TABLE OrderItem (
    OrderItemId UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    Id INT IDENTITY(1,1) NOT NULL,
    OrderId UNIQUEIDENTIFIER,
    ProductId UNIQUEIDENTIFIER,
    FOREIGN KEY (OrderId) REFERENCES "Order"(OrderId),
    FOREIGN KEY (ProductId) REFERENCES Product(ProductId)
);

-- Create Delivery table
CREATE TABLE Delivery (
    DeliveryId UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    Id INT IDENTITY(1,1) NOT NULL,
    OrderId UNIQUEIDENTIFIER,
    ShiperId UNIQUEIDENTIFIER,
    ManagerId UNIQUEIDENTIFIER,
    FOREIGN KEY (OrderId) REFERENCES "Order"(OrderId),
    FOREIGN KEY (ShiperId) REFERENCES "User"(UserId),
    FOREIGN KEY (ManagerId) REFERENCES "User"(UserId)
);

-- Create Warranty table
CREATE TABLE Warranty (
    WarrantyId UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    Id INT IDENTITY(1,1) NOT NULL,
    ProductId UNIQUEIDENTIFIER,
    UserId UNIQUEIDENTIFIER,
    IssueDate DATE NOT NULL,
    ExpirationDate DATE NOT NULL,
    FOREIGN KEY (ProductId) REFERENCES Product(ProductId),
    FOREIGN KEY (UserId) REFERENCES "User"(UserId)
);

-- Insert sample data into Role table
INSERT INTO Role (RoleName) VALUES
    ('Customer'),
    ('Shiper'),
    ('Manager'),
    ('Admin'),
    ('Support');

-- Insert sample data into User table
INSERT INTO "User" (Username, Password, Email, RoleId) VALUES
    ('customer1', 'password1', 'customer1@example.com', (SELECT RoleId FROM Role WHERE RoleName='Customer')),
    ('customer2', 'password2', 'customer2@example.com', (SELECT RoleId FROM Role WHERE RoleName='Customer')),
    ('shiper1', 'password1', 'shiper1@example.com', (SELECT RoleId FROM Role WHERE RoleName='Shiper')),
    ('manager1', 'password1', 'manager1@example.com', (SELECT RoleId FROM Role WHERE RoleName='Manager')),
    ('admin1', 'password1', 'admin1@example.com', (SELECT RoleId FROM Role WHERE RoleName='Admin'));

-- Insert sample data into Voucher table
INSERT INTO Voucher (Name, DiscountPercentage, StartDate, EndDate) VALUES
    ('New Year Discount', 10.00, '2024-01-01', '2024-01-31'),
    ('Summer Sale', 15.00, '2024-06-01', '2024-06-30'),
    ('Black Friday', 20.00, '2024-11-23', '2024-11-27'),
    ('Christmas Special', 25.00, '2024-12-20', '2024-12-31'),
    ('Valentine''s Day', 30.00, '2024-02-10', '2024-02-14'); -- Note the escape here

-- Insert sample data into CustomerVoucher table
INSERT INTO CustomerVoucher (UserId, VoucherId, Status) VALUES
    ((SELECT TOP 1 UserId FROM "User" WHERE Username='customer1'), (SELECT TOP 1 VoucherId FROM Voucher WHERE Name='New Year Discount'), 'Used'),
    ((SELECT TOP 1 UserId FROM "User" WHERE Username='customer2'), (SELECT TOP 1 VoucherId FROM Voucher WHERE Name='Summer Sale'), 'Unused'),
    ((SELECT TOP 1 UserId FROM "User" WHERE Username='customer1'), (SELECT TOP 1 VoucherId FROM Voucher WHERE Name='Black Friday'), 'Expired'),
    ((SELECT TOP 1 UserId FROM "User" WHERE Username='customer2'), (SELECT TOP 1 VoucherId FROM Voucher WHERE Name='Christmas Special'), 'Unused'),
    ((SELECT TOP 1 UserId FROM "User" WHERE Username='customer1'), (SELECT TOP 1 VoucherId FROM Voucher WHERE Name='Valentine''s Day'), 'Unused'); -- Note the escape here

-- Insert sample data into Distributor table
INSERT INTO Distributor (DistributorName, Locate) VALUES
    ('Diamond Distributors Inc.', '123 Diamond St.'),
    ('Gemstone Wholesalers', '456 Gem Ave.'),
    ('Precious Stones Ltd.', '789 Precious Rd.'),
    ('Jewel Supply Co.', '321 Jewel Ln.'),
    ('Luxury Gems', '654 Luxury Blvd.');

-- Insert sample data into Category table
INSERT INTO Category (GiaCertificate, Cut, Color, Clarity, Carat, DistributorId) VALUES
    ('GIA123456', 'Round', 'D', 'VS1', 1.50, (SELECT DistributorId FROM Distributor WHERE DistributorName='Diamond Distributors Inc.')),
    ('GIA654321', 'Princess', 'F', 'VVS2', 2.00, (SELECT DistributorId FROM Distributor WHERE DistributorName='Gemstone Wholesalers')),
    ('GIA987654', 'Emerald', 'E', 'SI1', 1.75, (SELECT DistributorId FROM Distributor WHERE DistributorName='Precious Stones Ltd.')),
    ('GIA456789', 'Oval', 'G', 'VS2', 2.50, (SELECT DistributorId FROM Distributor WHERE DistributorName='Jewel Supply Co.')),
    ('GIA112233', 'Cushion', 'H', 'IF', 1.25, (SELECT DistributorId FROM Distributor WHERE DistributorName='Luxury Gems'));

-- Insert sample data into Product table
INSERT INTO Product (Name, Description, Price, CategoryId) VALUES
    ('Round Cut Diamond', 'A beautiful round cut diamond', 15000.00, (SELECT CategoryId FROM Category WHERE GiaCertificate='GIA123456')),
    ('Princess Cut Diamond', 'A stunning princess cut diamond', 20000.00, (SELECT CategoryId FROM Category WHERE GiaCertificate='GIA654321')),
    ('Emerald Cut Diamond', 'A magnificent emerald cut diamond', 17500.00, (SELECT CategoryId FROM Category WHERE GiaCertificate='GIA987654')),
    ('Oval Cut Diamond', 'An exquisite oval cut diamond', 25000.00, (SELECT CategoryId FROM Category WHERE GiaCertificate='GIA456789')),
    ('Cushion Cut Diamond', 'A gorgeous cushion cut diamond', 12500.00, (SELECT CategoryId FROM Category WHERE GiaCertificate='GIA112233'));

-- Insert sample data into Order table
INSERT INTO "Order" (UserId, OrderDate, TotalAmount, Status, VoucherId, TotalPrice) VALUES
    ((SELECT UserId FROM "User" WHERE Username='customer1'), '2024-06-15', 45000.00, 'Completed', (SELECT VoucherId FROM Voucher WHERE Name='Summer Sale'), 38250.00),
    ((SELECT UserId FROM "User" WHERE Username='customer2'), '2024-12-22', 30000.00, 'Pending', (SELECT VoucherId FROM Voucher WHERE Name='Christmas Special'), 22500.00);

-- Insert sample data into OrderItem table
INSERT INTO OrderItem (OrderId, ProductId) VALUES
    ((SELECT OrderId FROM "Order" WHERE UserId=(SELECT UserId FROM "User" WHERE Username='customer1')), (SELECT ProductId FROM Product WHERE Name='Round Cut Diamond')),
    ((SELECT OrderId FROM "Order" WHERE UserId=(SELECT UserId FROM "User" WHERE Username='customer1')), (SELECT ProductId FROM Product WHERE Name='Princess Cut Diamond')),
    ((SELECT OrderId FROM "Order" WHERE UserId=(SELECT UserId FROM "User" WHERE Username='customer2')), (SELECT ProductId FROM Product WHERE Name='Emerald Cut Diamond'));

-- Insert sample data into Delivery table
INSERT INTO Delivery (OrderId, ShiperId, ManagerId) VALUES
    ((SELECT OrderId FROM "Order" WHERE UserId=(SELECT UserId FROM "User" WHERE Username='customer1')), (SELECT UserId FROM "User" WHERE Username='shiper1'), (SELECT UserId FROM "User" WHERE Username='manager1')),
    ((SELECT OrderId FROM "Order" WHERE UserId=(SELECT UserId FROM "User" WHERE Username='customer2')), (SELECT UserId FROM "User" WHERE Username='shiper1'), (SELECT UserId FROM "User" WHERE Username='manager1'));

-- Insert sample data into Warranty table
INSERT INTO Warranty (ProductId, UserId, IssueDate, ExpirationDate) VALUES
    ((SELECT ProductId FROM Product WHERE Name='Round Cut Diamond'), (SELECT UserId FROM "User" WHERE Username='customer1'), '2024-06-15', '2025-06-15'),
    ((SELECT ProductId FROM Product WHERE Name='Princess Cut Diamond'), (SELECT UserId FROM "User" WHERE Username='customer1'), '2024-06-15', '2025-06-15'),
    ((SELECT ProductId FROM Product WHERE Name='Emerald Cut Diamond'), (SELECT UserId FROM "User" WHERE Username='customer2'), '2024-12-22', '2025-12-22');
