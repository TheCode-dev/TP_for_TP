-- --------------------------------------------------------
-- Хост:                         81.4.243.184
-- Версия сервера:               8.0.31 - MySQL Community Server - GPL
-- Операционная система:         Win64
-- HeidiSQL Версия:              12.3.0.6589
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Дамп структуры базы данных kp_belyshev
CREATE DATABASE IF NOT EXISTS `kp_belyshev` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `kp_belyshev`;

-- Дамп структуры для таблица kp_belyshev.cat_product
CREATE TABLE IF NOT EXISTS `cat_product` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Type` varchar(50) DEFAULT NULL,
  `Name` varchar(50) DEFAULT NULL,
  `Description` varchar(3000) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb3;

-- Экспортируемые данные не выделены.

-- Дамп структуры для таблица kp_belyshev.countries
CREATE TABLE IF NOT EXISTS `countries` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb3;

-- Экспортируемые данные не выделены.

-- Дамп структуры для таблица kp_belyshev.discounts
CREATE TABLE IF NOT EXISTS `discounts` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Percent` int DEFAULT NULL,
  `Description` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb3;

-- Экспортируемые данные не выделены.

-- Дамп структуры для таблица kp_belyshev.groups
CREATE TABLE IF NOT EXISTS `groups` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) DEFAULT NULL,
  `Role` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb3;

-- Экспортируемые данные не выделены.

-- Дамп структуры для таблица kp_belyshev.orders
CREATE TABLE IF NOT EXISTS `orders` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Date` timestamp NULL DEFAULT NULL,
  `ProductID` int NOT NULL,
  `UserID` int NOT NULL,
  `Quantity` int NOT NULL,
  `Price` int NOT NULL,
  `ManagerID` int NOT NULL,
  `DiscountID` int NOT NULL,
  `Bonus` int DEFAULT NULL,
  `OrderStatus` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_Orders_Discounts` (`DiscountID`),
  KEY `FK_Orders_Products` (`ProductID`),
  KEY `FK_Orders_Users` (`UserID`),
  KEY `FK_Orders_Users_2` (`ManagerID`),
  CONSTRAINT `FK_Orders_Discounts` FOREIGN KEY (`DiscountID`) REFERENCES `discounts` (`ID`),
  CONSTRAINT `FK_Orders_Products` FOREIGN KEY (`ProductID`) REFERENCES `products` (`ID`),
  CONSTRAINT `FK_Orders_Users` FOREIGN KEY (`UserID`) REFERENCES `users` (`ID`),
  CONSTRAINT `FK_Orders_Users_2` FOREIGN KEY (`ManagerID`) REFERENCES `users` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=44 DEFAULT CHARSET=utf8mb3;

-- Экспортируемые данные не выделены.

-- Дамп структуры для таблица kp_belyshev.products
CREATE TABLE IF NOT EXISTS `products` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Type` varchar(50) DEFAULT NULL,
  `Name` varchar(50) DEFAULT NULL,
  `Description` varchar(3000) DEFAULT NULL,
  `Price` int DEFAULT NULL,
  `Count` int DEFAULT NULL,
  `DataEdit` timestamp NULL DEFAULT NULL,
  `VendorID` int DEFAULT NULL,
  `CategoryID` int DEFAULT NULL,
  `CountriesID` int DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `VendorID` (`VendorID`) USING BTREE,
  KEY `CategoryID` (`CategoryID`),
  KEY `FK_Products_Countries` (`CountriesID`),
  CONSTRAINT `FK_Products_Cat_Product` FOREIGN KEY (`CategoryID`) REFERENCES `cat_product` (`ID`),
  CONSTRAINT `FK_Products_Countries` FOREIGN KEY (`CountriesID`) REFERENCES `countries` (`ID`),
  CONSTRAINT `FK_Products_Vendor` FOREIGN KEY (`VendorID`) REFERENCES `vendor` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb3;

-- Экспортируемые данные не выделены.

-- Дамп структуры для таблица kp_belyshev.users
CREATE TABLE IF NOT EXISTS `users` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) DEFAULT NULL,
  `Gender` varchar(15) DEFAULT NULL,
  `Birthday` date DEFAULT NULL,
  `CreateDate` timestamp NULL DEFAULT NULL,
  `LastLogin` timestamp NULL DEFAULT NULL,
  `Status` varchar(50) DEFAULT NULL,
  `DiscontID` int DEFAULT NULL,
  `Bonus` int DEFAULT NULL,
  `Email` varchar(100) DEFAULT NULL,
  `Password` varchar(100) DEFAULT NULL,
  `IsManager` int DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_Users_Discounts` (`DiscontID`),
  CONSTRAINT `FK_Users_Discounts` FOREIGN KEY (`DiscontID`) REFERENCES `discounts` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb3;

-- Экспортируемые данные не выделены.

-- Дамп структуры для таблица kp_belyshev.user_group
CREATE TABLE IF NOT EXISTS `user_group` (
  `UserID` int DEFAULT NULL,
  `GroupID` int DEFAULT NULL,
  KEY `UserID` (`UserID`),
  KEY `GroupID` (`GroupID`),
  CONSTRAINT `User_Group_ibfk_1` FOREIGN KEY (`UserID`) REFERENCES `users` (`ID`),
  CONSTRAINT `User_Group_ibfk_2` FOREIGN KEY (`GroupID`) REFERENCES `groups` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- Экспортируемые данные не выделены.

-- Дамп структуры для таблица kp_belyshev.vendor
CREATE TABLE IF NOT EXISTS `vendor` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) DEFAULT NULL,
  `CreateDate` date DEFAULT NULL,
  `Description` varchar(1000) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb3;

-- Экспортируемые данные не выделены.

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
