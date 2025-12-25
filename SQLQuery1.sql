-- 1. Таблица Ролей
CREATE TABLE Roles (
    RoleID INT IDENTITY(1,1) PRIMARY KEY,
    RoleName VARCHAR(50) NOT NULL UNIQUE
);

-- 2. Таблица Пользователей
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    FIO VARCHAR(120) NOT NULL,
    UserLogin VARCHAR(50) NOT NULL UNIQUE,
    UserPassword VARCHAR(50) NOT NULL,
    RoleID INT NOT NULL,
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);

-- 3. Таблица Товаров (все характеристики в одной таблице)
CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    Article VARCHAR(50) NOT NULL UNIQUE,
    ProductName VARCHAR(200) NOT NULL,
    Category VARCHAR(100) NOT NULL, -- Категория как текст, а не отдельная таблица
    Manufacturer VARCHAR(100) NOT NULL, -- Производитель как текст
    Price DECIMAL(10,2) NOT NULL,
    Quantity INT NOT NULL DEFAULT 0,
    Status VARCHAR(20) DEFAULT 'в наличии'
);

-- 4. Таблица Пунктов выдачи
CREATE TABLE PickupPoints (
    PointID INT IDENTITY(1,1) PRIMARY KEY,
    Address VARCHAR(300) NOT NULL,
    Phone VARCHAR(20) NULL
);

-- 5. Таблица Заказов
CREATE TABLE Orders (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    OrderDate DATETIME DEFAULT GETDATE(),
    Status VARCHAR(50) DEFAULT 'ожидает обработки',
    PickupPointID INT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (PickupPointID) REFERENCES PickupPoints(PointID)
);

-- 6. Таблица Состава заказа (что заказано)
CREATE TABLE OrderItems (
    OrderItemID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);