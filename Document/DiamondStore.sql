-- Create DiamondStore Database
CREATE DATABASE DiamondStore;

-- Use DiamondStore Database
USE DiamondStore;

-- Create Users Table
CREATE TABLE Users (
    UserId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    Id INT IDENTITY(1,1),
    Username VARCHAR(255) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    Role VARCHAR(50) NOT NULL,
    PRIMARY KEY (UserId),
    UNIQUE (Id)
);

-- Create Distributer Table
CREATE TABLE Distributer (
    DistributerId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    Id INT IDENTITY(1,1),
    Location VARCHAR(255) NOT NULL,
    DistributerName VARCHAR(255) NOT NULL,
    PRIMARY KEY (DistributerId),
    UNIQUE (Id)
);

-- Create Product Table
CREATE TABLE Product (
    ProductId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    Id INT IDENTITY(1,1),
    ProductName VARCHAR(255) NOT NULL,
    Description TEXT NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    GiaCertificate VARCHAR(255),
    Cut VARCHAR(50),
    Color VARCHAR(50),
    Clarity VARCHAR(50),
    Carat DECIMAL(5, 2),
    Category VARCHAR(50) NOT NULL,
    DistributerId UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY (ProductId),
    UNIQUE (Id),
    FOREIGN KEY (DistributerId) REFERENCES Distributer(DistributerId)
);

-- Create Voucher Table
CREATE TABLE Voucher (
    VoucherId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    Id INT IDENTITY(1,1),
    VoucherName VARCHAR(255) NOT NULL,
    DiscountPercentage DECIMAL(5, 2) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    PRIMARY KEY (VoucherId),
    UNIQUE (Id)
);

-- Create CustomerVoucher Table
CREATE TABLE CustomerVoucher (
    CustomerVoucherId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    Id INT IDENTITY(1,1),
    UserId UNIQUEIDENTIFIER NOT NULL,
    VoucherId UNIQUEIDENTIFIER NOT NULL,
    Status VARCHAR(50) NOT NULL,
    PRIMARY KEY (CustomerVoucherId),
    UNIQUE (Id),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (VoucherId) REFERENCES Voucher(VoucherId)
);

-- Create Order Table
CREATE TABLE Orders (
    OrderId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    Id INT IDENTITY(1,1),
    UserId UNIQUEIDENTIFIER NOT NULL,
    OrderDate DATE NOT NULL,
    TotalAmount DECIMAL(10, 2) NOT NULL,
    Status VARCHAR(50) NOT NULL,
    PRIMARY KEY (OrderId),
    UNIQUE (Id),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

-- Create OrderItem Table
CREATE TABLE OrderItem (
    OrderItemId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    Id INT IDENTITY(1,1),
    OrderId UNIQUEIDENTIFIER NOT NULL,
    ProductId UNIQUEIDENTIFIER NOT NULL,
    Quantity INT NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    PRIMARY KEY (OrderItemId),
    UNIQUE (Id),
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    FOREIGN KEY (ProductId) REFERENCES Product(ProductId)
);

-- Create Warranty Table
CREATE TABLE Warranty (
    WarrantyId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    Id INT IDENTITY(1,1),
    ProductId UNIQUEIDENTIFIER NOT NULL,
    UserId UNIQUEIDENTIFIER NOT NULL,
    IssueDate DATE NOT NULL,
    ExpirationDate DATE NOT NULL,
    Description TEXT NOT NULL,
    PRIMARY KEY (WarrantyId),
    UNIQUE (Id),
    FOREIGN KEY (ProductId) REFERENCES Product(ProductId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

-- Create Cart Table
CREATE TABLE Cart (
    CartId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    Id INT IDENTITY(1,1),
    UserId UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY (CartId),
    UNIQUE (Id),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

-- Create CartItem Table
CREATE TABLE CartItem (
    CartItemId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    Id INT IDENTITY(1,1),
    CartId UNIQUEIDENTIFIER NOT NULL,
    ProductId UNIQUEIDENTIFIER NOT NULL,
    Quantity INT NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    PRIMARY KEY (CartItemId),
    UNIQUE (Id),
    FOREIGN KEY (CartId) REFERENCES Cart(CartId),
    FOREIGN KEY (ProductId) REFERENCES Product(ProductId)
);
