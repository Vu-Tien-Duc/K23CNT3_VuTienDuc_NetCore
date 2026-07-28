CREATE DATABASE VtdBookStoreDB22;
GO

USE VtdBookStoreDB22;
GO

/*=========================================
    VtdCategory
=========================================*/
CREATE TABLE VtdCategory
(
    VtdId INT IDENTITY(1,1) PRIMARY KEY,
    VtdName NVARCHAR(100) NOT NULL,
    VtdStatus TINYINT NOT NULL DEFAULT 1,
    VtdCreatedDate DATE NOT NULL DEFAULT GETDATE()
);
GO

/*=========================================
    VtdProduct
=========================================*/
CREATE TABLE VtdProduct
(
    VtdId INT IDENTITY(1,1) PRIMARY KEY,

    VtdName NVARCHAR(100) NOT NULL,

    VtdPrice FLOAT NOT NULL,

    VtdSalePrice FLOAT NULL DEFAULT 0,

    VtdStatus TINYINT NOT NULL DEFAULT 1,

    VtdCategoryId INT NOT NULL,

    VtdCreatedDate DATE NOT NULL DEFAULT GETDATE(),

    VtdImage VARCHAR(100) NULL,

    VtdDescription NVARCHAR(350) NULL,

    CONSTRAINT FK_VtdProduct_VtdCategory
        FOREIGN KEY (VtdCategoryId)
        REFERENCES VtdCategory(VtdId)
);
GO

/*=========================================
    VtdBanner
=========================================*/
CREATE TABLE VtdBanner
(
    VtdId INT IDENTITY(1,1) PRIMARY KEY,

    VtdName NVARCHAR(100) NOT NULL,

    VtdStatus TINYINT NOT NULL DEFAULT 1,

    VtdPriority INT NOT NULL DEFAULT 0,

    VtdImage VARCHAR(100) NULL,

    VtdDescription NVARCHAR(350) NULL
);
GO

/*=========================================
    VtdBlog
=========================================*/
CREATE TABLE VtdBlog
(
    VtdId INT IDENTITY(1,1) PRIMARY KEY,

    VtdName NVARCHAR(100) NOT NULL,

    VtdStatus TINYINT NOT NULL DEFAULT 1,

    VtdCreatedDate DATE NOT NULL DEFAULT GETDATE(),

    VtdImage VARCHAR(100) NULL,

    VtdDescription NVARCHAR(350) NULL
);
GO

/*=========================================
    Dữ liệu mẫu
=========================================*/

INSERT INTO VtdCategory(VtdName)
VALUES
(N'Tiểu thuyết'),
(N'Công nghệ'),
(N'Khoa học');

INSERT INTO VtdProduct
(
    VtdName,
    VtdPrice,
    VtdSalePrice,
    VtdStatus,
    VtdCategoryId,
    VtdImage,
    VtdDescription
)
VALUES
(N'ASP.NET Core MVC',250000,220000,1,2,'aspnet.jpg',N'Sách ASP.NET Core'),
(N'Clean Code',320000,290000,1,2,'cleancode.jpg',N'Sách lập trình'),
(N'Dế Mèn Phiêu Lưu Ký',120000,100000,1,1,'demen.jpg',N'Truyện thiếu nhi');

INSERT INTO VtdBanner
(
    VtdName,
    VtdStatus,
    VtdPriority,
    VtdImage,
    VtdDescription
)
VALUES
(N'Banner Trang Chủ',1,1,'banner1.jpg',N'Banner chính'),
(N'Khuyến mãi',1,2,'banner2.jpg',N'Banner giảm giá');

INSERT INTO VtdBlog
(
    VtdName,
    VtdStatus,
    VtdImage,
    VtdDescription
)
VALUES
(N'Giới thiệu Website',1,'blog1.jpg',N'Bài viết đầu tiên'),
(N'Tin khuyến mãi',1,'blog2.jpg',N'Khuyến mãi cuối tuần');
GO