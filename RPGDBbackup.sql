-- MySQL dump 10.13  Distrib 8.0.13, for Win64 (x86_64)
--
-- Host: localhost    Database: game
-- ------------------------------------------------------
-- Server version	8.0.13

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `inventoryitemdb`
--

DROP TABLE IF EXISTS `inventoryitemdb`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `inventoryitemdb` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `inventoryid` int(11) DEFAULT NULL,
  `count` int(11) DEFAULT NULL,
  `isdressed` tinyint(4) DEFAULT NULL,
  `roleid` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_roleid_idx` (`roleid`),
  CONSTRAINT `fk_inventoryroleid` FOREIGN KEY (`roleid`) REFERENCES `role` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `inventoryitemdb`
--

LOCK TABLES `inventoryitemdb` WRITE;
/*!40000 ALTER TABLE `inventoryitemdb` DISABLE KEYS */;
INSERT INTO `inventoryitemdb` VALUES (1,1018,1,0,5),(2,1003,1,0,5),(3,1005,4,0,5),(4,1017,0,0,5),(5,1012,1,0,5),(6,1013,2,0,5),(7,1010,2,0,5),(8,1002,2,0,5),(9,1015,1,1,5),(10,1014,2,0,5),(11,1007,2,0,5),(12,1011,2,0,5),(13,1009,4,0,5),(14,1004,2,0,5),(15,1006,0,0,5),(16,1008,1,0,6);
/*!40000 ALTER TABLE `inventoryitemdb` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `result`
--

DROP TABLE IF EXISTS `result`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `result` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `userid` int(11) NOT NULL,
  `totalcount` int(11) NOT NULL DEFAULT '0',
  `wincount` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `fk_userid_idx` (`userid`),
  CONSTRAINT `fk_userid` FOREIGN KEY (`userid`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `result`
--

LOCK TABLES `result` WRITE;
/*!40000 ALTER TABLE `result` DISABLE KEYS */;
INSERT INTO `result` VALUES (1,1,4,0),(2,2,4,4);
/*!40000 ALTER TABLE `result` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role`
--

DROP TABLE IF EXISTS `role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `role` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  `level` int(11) DEFAULT NULL,
  `roleId` int(11) DEFAULT NULL,
  `userid` int(11) DEFAULT NULL,
  `exp` int(11) DEFAULT NULL,
  `coin` int(11) DEFAULT NULL,
  `atk` int(11) DEFAULT NULL,
  `def` int(11) DEFAULT NULL,
  `hp` int(11) DEFAULT NULL,
  `maxhp` int(11) DEFAULT '100',
  PRIMARY KEY (`id`),
  KEY `fk_userid_idx` (`userid`),
  KEY `fk_uid_idx` (`userid`),
  CONSTRAINT `fk_uid` FOREIGN KEY (`userid`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role`
--

LOCK TABLES `role` WRITE;
/*!40000 ALTER TABLE `role` DISABLE KEYS */;
INSERT INTO `role` VALUES (5,'123',2,0,1,0,1680,110,5,120,120),(6,'sww123',1,0,2,0,1000,10,5,100,100),(7,'darkif123',1,0,5,0,1000,10,5,100,100);
/*!40000 ALTER TABLE `role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `skilldb`
--

DROP TABLE IF EXISTS `skilldb`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `skilldb` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `skillid` int(11) DEFAULT NULL,
  `roleid` int(11) DEFAULT NULL,
  `level` int(11) DEFAULT '1',
  `damage` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_skill_roleid_idx` (`roleid`),
  CONSTRAINT `fk_skill_roleid` FOREIGN KEY (`roleid`) REFERENCES `role` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `skilldb`
--

LOCK TABLES `skilldb` WRITE;
/*!40000 ALTER TABLE `skilldb` DISABLE KEYS */;
INSERT INTO `skilldb` VALUES (3,1002,5,2,230);
/*!40000 ALTER TABLE `skilldb` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `task`
--

DROP TABLE IF EXISTS `task`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `task` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `taskid` int(11) DEFAULT NULL,
  `taskState` int(11) DEFAULT NULL,
  `taskType` int(11) DEFAULT NULL,
  `lastUpdateTime` datetime DEFAULT NULL,
  `roleId` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `foreignK_userid_idx` (`roleId`),
  CONSTRAINT `fk_roleid` FOREIGN KEY (`roleId`) REFERENCES `role` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `task`
--

LOCK TABLES `task` WRITE;
/*!40000 ALTER TABLE `task` DISABLE KEYS */;
INSERT INTO `task` VALUES (1,1001,1,0,'2019-02-17 23:12:36',6),(2,1002,0,1,'2019-02-15 20:24:00',5),(3,1003,0,2,'2019-02-15 20:24:00',5),(6,1001,1,0,'2019-02-17 23:12:36',6),(7,1001,1,0,'2019-02-17 23:17:41',5),(8,1001,1,0,'2019-02-17 23:19:12',7);
/*!40000 ALTER TABLE `task` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(45) NOT NULL,
  `password` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `username_UNIQUE` (`username`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'123','123'),(2,'sww','123'),(3,'SWD','123'),(5,'darkif','123');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-02-17 23:31:29
