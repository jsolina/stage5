-- MySQL dump 10.13  Distrib 8.0.22, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: todo_app
-- ------------------------------------------------------
-- Server version	8.0.22

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `idempotent_request`
--

DROP TABLE IF EXISTS `idempotent_request`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `idempotent_request` (
  `key` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `request` binary(20) NOT NULL,
  `response` json NOT NULL,
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `exception_type` varchar(260) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `idempotent_request`
--

LOCK TABLES `idempotent_request` WRITE;
/*!40000 ALTER TABLE `idempotent_request` DISABLE KEYS */;
INSERT INTO `idempotent_request` VALUES ('f4f6cee2-7857-49f8-af84-e393ccbc7347',_binary '5¸±Ÿ≠ß\‚]ÉOæÙ#‘ñP\0','{\"Email\": \"task1@gmail.com\", \"TaskName\": \"Feeding Program\", \"TaskDetails\": \"........\"}','2021-02-10 09:50:15',NULL),('c806e9f5-670e-4f05-9c92-b3e0ca9370d5',_binary '¡^-\”&˙Nú\”\”7ªvJduH∏Ù','{\"Email\": \"janus@gmail.com\", \"TaskName\": \"Ninjutsu Training\", \"TaskDetails\": \".......\"}','2021-02-10 09:50:43',NULL),('dde96cc3-e6e1-487e-be98-586316d56326',_binary 'á∑8ì\‹˜NÒı\”\‰ic,=B√Ö','{\"IdTask\": 1, \"ItemName\": \"Nature Chackra Training\", \"ItemStatus\": \"Pending\", \"ItemDetails\": \".....\"}','2021-02-10 09:52:12',NULL),('a3f47688-9c00-4272-97c0-56d0366bd2bb',_binary 'h\ z≥6IΩ∂q~¸{ºz_¬ó}¶','{\"IdTask\": 1, \"ItemName\": \"Genjutsu Training\", \"ItemStatus\": \"Pending\", \"ItemDetails\": \"...\"}','2021-02-10 09:52:28',NULL),('06b6f974-f22b-4e83-9665-b046dcdd5d43',_binary '*Î≥Ω\01´\Õyê*ò4¡$ΩU','{\"Email\": \"aa\", \"TaskName\": \"aaaa\", \"TaskDetails\": \"aaa\"}','2021-02-10 09:52:43',NULL);
/*!40000 ALTER TABLE `idempotent_request` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `itemlist`
--

DROP TABLE IF EXISTS `itemlist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `itemlist` (
  `id` int NOT NULL AUTO_INCREMENT,
  `id_task` int NOT NULL,
  `item_name` varchar(65) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `item_details` varchar(65) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `item_status` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `last_modified` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `idTask` (`id_task`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `itemlist`
--

LOCK TABLES `itemlist` WRITE;
/*!40000 ALTER TABLE `itemlist` DISABLE KEYS */;
INSERT INTO `itemlist` VALUES (1,1,'Nature Chackra Training','.....','Done','2021-02-10 01:52:12','2021-02-10 01:52:35'),(2,1,'Genjutsu Training','...','Pending','2021-02-10 01:52:28','2021-02-10 01:52:28');
/*!40000 ALTER TABLE `itemlist` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tasklist`
--

DROP TABLE IF EXISTS `tasklist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tasklist` (
  `id` int NOT NULL AUTO_INCREMENT,
  `task_name` varchar(65) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `task_details` varchar(65) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `email` varchar(85) DEFAULT NULL,
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `last_modified` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tasklist`
--

LOCK TABLES `tasklist` WRITE;
/*!40000 ALTER TABLE `tasklist` DISABLE KEYS */;
INSERT INTO `tasklist` VALUES (1,'Ninjutsu Training','........','konoha@gmail.com','2021-02-10 01:50:16','2021-02-10 01:51:31'),(2,'Feeding Program','.......','janus@gmail.com','2021-02-10 01:50:43','2021-02-10 01:51:17');
/*!40000 ALTER TABLE `tasklist` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-02-10 17:53:28
