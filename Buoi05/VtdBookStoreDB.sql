--=========================================================
-- TẠO DATABASE
--=========================================================
CREATE DATABASE VtdBookStoreDB;
GO

USE VtdBookStoreDB;
GO

--=========================================================
-- CATEGORY
--=========================================================
CREATE TABLE VtdCategory
(
    VtdCategoryId INT IDENTITY(1,1) PRIMARY KEY,
    VtdCategoryName NVARCHAR(100) NOT NULL
);
GO

--=========================================================
-- PUBLISHER
--=========================================================
CREATE TABLE VtdPublisher
(
    VtdPublisherId INT IDENTITY(1,1) PRIMARY KEY,
    VtdPublisherName NVARCHAR(200) NOT NULL,
    VtdPhone VARCHAR(30),
    VtdAddress NVARCHAR(200)
);
GO

--=========================================================
-- ACCOUNT
--=========================================================
CREATE TABLE VtdAccount
(
    VtdAccountId VARCHAR(36) PRIMARY KEY,
    VtdUsername VARCHAR(64) NOT NULL,
    VtdPassword VARCHAR(256) NOT NULL,
    VtdFullName NVARCHAR(100),
    VtdPicture NVARCHAR(512),
    VtdEmail VARCHAR(64),
    VtdAddress NVARCHAR(512),
    VtdPhone VARCHAR(64),
    VtdIsAdmin BIT,
    VtdActive BIT
);
GO

--=========================================================
-- BOOK
--=========================================================
CREATE TABLE VtdBook
(
    VtdBookId VARCHAR(10) PRIMARY KEY,
    VtdTitle NVARCHAR(200) NOT NULL,
    VtdAuthor NVARCHAR(100),
    VtdRelease INT,
    VtdPrice FLOAT,
    VtdDescription NTEXT,
    VtdPicture NVARCHAR(100),

    VtdPublisherId INT,
    VtdCategoryId INT,

    CONSTRAINT FK_VtdBook_VtdPublisher
        FOREIGN KEY(VtdPublisherId)
        REFERENCES VtdPublisher(VtdPublisherId),

    CONSTRAINT FK_VtdBook_VtdCategory
        FOREIGN KEY(VtdCategoryId)
        REFERENCES VtdCategory(VtdCategoryId)
);
GO

--=========================================================
-- ORDERBOOK
--=========================================================
CREATE TABLE VtdOrderBook
(
    VtdOrderId VARCHAR(16) PRIMARY KEY,
    VtdOrderDate DATETIME,
    VtdAccountId VARCHAR(36),
    VtdReceiveAddress NVARCHAR(512),
    VtdReceivePhone VARCHAR(64),
    VtdOrderReceive DATETIME,
    VtdNote NVARCHAR(512),
    VtdStatus VARCHAR(16),

    CONSTRAINT FK_VtdOrderBook_VtdAccount
        FOREIGN KEY(VtdAccountId)
        REFERENCES VtdAccount(VtdAccountId)
);
GO

--=========================================================
-- ORDERDETAIL
--=========================================================
CREATE TABLE VtdOrderDetail
(
    VtdOrderDetailId INT IDENTITY(1,1) PRIMARY KEY,

    VtdOrderId VARCHAR(16),
    VtdBookId VARCHAR(10),

    VtdQuantity INT,
    VtdPrice INT,
    VtdTotalMoney INT,

    CONSTRAINT FK_VtdOrderDetail_Order
        FOREIGN KEY(VtdOrderId)
        REFERENCES VtdOrderBook(VtdOrderId),

    CONSTRAINT FK_VtdOrderDetail_Book
        FOREIGN KEY(VtdBookId)
        REFERENCES VtdBook(VtdBookId)
);
GO

--=========================================================
-- INSERT CATEGORY
--=========================================================
INSERT INTO VtdCategory(VtdCategoryName)
VALUES
(N'Lập trình'),
(N'Kinh tế'),
(N'Ngoại ngữ'),
(N'Tiểu thuyết');
GO

--=========================================================
-- INSERT PUBLISHER
--=========================================================
INSERT INTO VtdPublisher
(
    VtdPublisherName,
    VtdPhone,
    VtdAddress
)
VALUES
(N'NXB Kim Đồng','02438220612',N'Hà Nội'),
(N'NXB Trẻ','02839316289',N'TP Hồ Chí Minh'),
(N'NXB Giáo Dục','02439717189',N'Hà Nội');
GO

--=========================================================
-- INSERT ACCOUNT
--=========================================================
INSERT INTO VtdAccount
VALUES
('ACC001','admin','123456',N'Quản trị',
'/images/admin.jpg',
'admin@gmail.com',
N'Hà Nội',
'0901111111',
1,
1),

('ACC002','user1','123456',N'Nguyễn Văn A',
'/images/a.jpg',
'a@gmail.com',
N'Hà Nội',
'0902222222',
0,
1),

('ACC003','user2','123456',N'Trần Thị B',
'/images/b.jpg',
'b@gmail.com',
N'Hải Phòng',
'0903333333',
0,
1);
GO

--=========================================================
-- INSERT BOOK
--=========================================================
INSERT INTO VtdBook
(
VtdBookId,
VtdTitle,
VtdAuthor,
VtdRelease,
VtdPrice,
VtdDescription,
VtdPicture,
VtdPublisherId,
VtdCategoryId
)
VALUES
('B001',
N'Lập trình C#',
N'Nguyễn Văn C',
2024,
250000,
N'Sách học C#',
'/images/book1.jpg',
1,
1),

('B002',
N'ASP.NET Core MVC',
N'Trần Văn D',
2023,
320000,
N'Lập trình Web MVC',
'/images/book2.jpg',
2,
1),

('B003',
N'Kinh tế học',
N'Lê Văn E',
2022,
180000,
N'Sách kinh tế',
'/images/book3.jpg',
3,
2);
GO

--=========================================================
-- INSERT ORDERBOOK
--=========================================================
INSERT INTO VtdOrderBook
(
VtdOrderId,
VtdOrderDate,
VtdAccountId,
VtdReceiveAddress,
VtdReceivePhone,
VtdOrderReceive,
VtdNote,
VtdStatus
)
VALUES
('OD001',
GETDATE(),
'ACC002',
N'Hà Nội',
'0902222222',
GETDATE()+2,
N'Giao giờ hành chính',
'NEW'),

('OD002',
GETDATE(),
'ACC003',
N'Hải Phòng',
'0903333333',
GETDATE()+3,
N'Giao buổi sáng',
'NEW');
GO

--=========================================================
-- INSERT ORDERDETAIL
--=========================================================
INSERT INTO VtdOrderDetail
(
VtdOrderId,
VtdBookId,
VtdQuantity,
VtdPrice,
VtdTotalMoney
)
VALUES
('OD001','B001',2,250000,500000),
('OD001','B002',1,320000,320000),
('OD002','B003',3,180000,540000);
GO


