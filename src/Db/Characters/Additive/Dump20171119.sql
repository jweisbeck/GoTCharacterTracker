CREATE DATABASE  IF NOT EXISTS `gotCharacterTracker` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `gotCharacterTracker`;
-- MySQL dump 10.13  Distrib 5.7.17, for macos10.12 (x86_64)
--
-- Host: localhost    Database: gotCharacterTracker
-- ------------------------------------------------------
-- Server version	5.6.25

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `houses`
--

DROP TABLE IF EXISTS `houses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `houses` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `houseWords` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COMMENT='Stores GoT families';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `houses`
--

LOCK TABLES `houses` WRITE;
/*!40000 ALTER TABLE `houses` DISABLE KEYS */;
INSERT INTO `houses` VALUES (1,'Lannister','A Lannister always pays his debts'),(2,'Stark','Winter is coming'),(3,'Targareyn','Fire and Blood');
/*!40000 ALTER TABLE `houses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `objects`
--

DROP TABLE IF EXISTS `objects`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `objects` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `personId` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `personId` (`personId`),
  CONSTRAINT `objects_ibfk_1` FOREIGN KEY (`personId`) REFERENCES `people` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COMMENT='Stores precious objects belonging to a character';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `objects`
--

LOCK TABLES `objects` WRITE;
/*!40000 ALTER TABLE `objects` DISABLE KEYS */;
INSERT INTO `objects` VALUES (1,'Longclaw',5),(2,'Needle',4);
/*!40000 ALTER TABLE `objects` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `organizationTypes`
--

DROP TABLE IF EXISTS `organizationTypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `organizationTypes` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `description` text,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8 COMMENT='Adds storage of organization types';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `organizationTypes`
--

LOCK TABLES `organizationTypes` WRITE;
/*!40000 ALTER TABLE `organizationTypes` DISABLE KEYS */;
INSERT INTO `organizationTypes` VALUES (1,'Religious',NULL),(2,'Military',NULL),(3,'Social justice',NULL),(4,'Commercial',NULL),(5,'Political',NULL);
/*!40000 ALTER TABLE `organizationTypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `organizations`
--

DROP TABLE IF EXISTS `organizations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `organizations` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `description` text,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8 COMMENT='Stores various organizations present in the series';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `organizations`
--

LOCK TABLES `organizations` WRITE;
/*!40000 ALTER TABLE `organizations` DISABLE KEYS */;
INSERT INTO `organizations` VALUES (1,'Brotherhood WIthout Banners',NULL),(2,'Faith Militant',NULL),(3,'Faceless Men',NULL),(4,'Lord of Light',NULL),(5,'Nights Watch',NULL);
/*!40000 ALTER TABLE `organizations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orgs_orgTypes`
--

DROP TABLE IF EXISTS `orgs_orgTypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `orgs_orgTypes` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `organizationId` int(11) NOT NULL,
  `organizationTypeId` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `orgs_orgTypes_ibfk_1` (`organizationId`),
  KEY `orgs_orgTypes_ibfk_2` (`organizationTypeId`),
  CONSTRAINT `orgs_orgTypes_ibfk_1` FOREIGN KEY (`organizationId`) REFERENCES `organizations` (`id`) ON DELETE CASCADE,
  CONSTRAINT `orgs_orgTypes_ibfk_2` FOREIGN KEY (`organizationTypeId`) REFERENCES `organizationTypes` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8 COMMENT='Many-to-many storage of organizations to type of organizations';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orgs_orgTypes`
--

LOCK TABLES `orgs_orgTypes` WRITE;
/*!40000 ALTER TABLE `orgs_orgTypes` DISABLE KEYS */;
INSERT INTO `orgs_orgTypes` VALUES (1,1,1),(2,1,2),(3,2,1),(4,4,1),(5,4,5),(6,5,2),(7,2,3);
/*!40000 ALTER TABLE `orgs_orgTypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `people`
--

DROP TABLE IF EXISTS `people`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `people` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `surname` varchar(255) DEFAULT NULL,
  `isAlive` int(11) NOT NULL,
  `houseId` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `person_ibfk_1` (`houseId`),
  CONSTRAINT `people_ibfk_1` FOREIGN KEY (`houseId`) REFERENCES `houses` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8 COMMENT='Stores central entity representing GoT Characters';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `people`
--

LOCK TABLES `people` WRITE;
/*!40000 ALTER TABLE `people` DISABLE KEYS */;
INSERT INTO `people` VALUES (1,'Tyrion','Lannister',1,1),(2,'Cersi','Lannister',1,1),(3,'Jaimie','Lannister',1,1),(4,'Sansa','Stark',1,2),(5,'Jon','Snow',0,2),(6,'Danarys','Targaryen',1,3),(7,'Beric','Dondarrion',0,NULL);
/*!40000 ALTER TABLE `people` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `people_orgs`
--

DROP TABLE IF EXISTS `people_orgs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `people_orgs` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `personId` int(11) NOT NULL,
  `organizationId` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `personId` (`personId`),
  KEY `organizationId` (`organizationId`),
  CONSTRAINT `people_orgs_ibfk_1` FOREIGN KEY (`personId`) REFERENCES `people` (`id`) ON DELETE CASCADE,
  CONSTRAINT `people_orgs_ibfk_2` FOREIGN KEY (`organizationId`) REFERENCES `organizations` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COMMENT='Many-to-many storage of relationships between people and organizations';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `people_orgs`
--

LOCK TABLES `people_orgs` WRITE;
/*!40000 ALTER TABLE `people_orgs` DISABLE KEYS */;
INSERT INTO `people_orgs` VALUES (1,7,4),(2,5,5);
/*!40000 ALTER TABLE `people_orgs` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-11-19 11:44:57
