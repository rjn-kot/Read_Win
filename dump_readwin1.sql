-- MySQL dump 10.13  Distrib 9.2.0, for Win64 (x86_64)
--
-- Host: localhost    Database: readwin
-- ------------------------------------------------------
-- Server version	9.2.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `class_rating`
--

DROP TABLE IF EXISTS `class_rating`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `class_rating` (
  `id` int NOT NULL AUTO_INCREMENT,
  `class_id` int NOT NULL,
  `year_id` int NOT NULL,
  `points` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `class_year_unique` (`class_id`,`year_id`),
  KEY `class_rating_ibfk_2` (`year_id`),
  CONSTRAINT `class_rating_ibfk_1` FOREIGN KEY (`class_id`) REFERENCES `school_class` (`id`),
  CONSTRAINT `class_rating_ibfk_2` FOREIGN KEY (`year_id`) REFERENCES `school_year` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `class_rating`
--

LOCK TABLES `class_rating` WRITE;
/*!40000 ALTER TABLE `class_rating` DISABLE KEYS */;
INSERT INTO `class_rating` VALUES (1,1,2,100),(2,2,2,90),(3,17,2,85),(4,18,1,95);
/*!40000 ALTER TABLE `class_rating` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `event`
--

DROP TABLE IF EXISTS `event`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `event` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `date` datetime NOT NULL,
  `type` enum('Индивидуальный','Групповой') NOT NULL,
  `status` enum('Будет','Прошел') NOT NULL DEFAULT 'Будет',
  `description` text,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `event`
--

LOCK TABLES `event` WRITE;
/*!40000 ALTER TABLE `event` DISABLE KEYS */;
INSERT INTO `event` VALUES (1,'Школьная олимпиада','2025-06-15 10:00:00','Групповой','Будет',NULL),(2,'Научная конференция','2026-05-10 09:00:00','Групповой','Будет','Конференция для школьников.'),(3,'Название мероприятия','2023-12-15 10:00:00','Групповой','Прошел','Описание мероприятия'),(7,'Имя','2025-06-11 12:36:33','Индивидуальный','Прошел',NULL),(8,'Новый год','2025-06-12 23:38:15','Индивидуальный','Прошел',NULL),(9,'fff','2025-06-13 15:19:34','Групповой','Прошел','аааа'),(10,'Сказка на ночь','2025-06-27 15:28:05','Индивидуальный','Будет',''),(11,'Сказка на ночь','2025-06-27 15:40:45','Групповой','Будет',''),(12,'Сказка на ночь','2025-07-04 15:43:26','Индивидуальный','Будет',''),(13,'Тест','2025-06-21 15:47:55','Индивидуальный','Будет',''),(14,'ТЕСМИ','2025-06-20 15:53:40','Индивидуальный','Будет',''),(15,'ТЕСМИ','2025-06-28 15:50:37','Групповой','Будет','ывпывп'),(16,'ТЕСМИ','2026-06-13 15:53:09','Индивидуальный','Будет',''),(17,'Сказка на ночь','2026-06-28 16:01:23','Индивидуальный','Будет',''),(18,'вава','2025-06-18 16:37:39','Групповой','Будет',''),(19,'вава','2025-06-18 16:37:39','Групповой','Будет',''),(20,'Ещё','2026-07-04 16:41:44','Групповой','Будет','');
/*!40000 ALTER TABLE `event` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = cp866 */ ;
/*!50003 SET character_set_results = cp866 */ ;
/*!50003 SET collation_connection  = cp866_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `update_event_status_on_insert` BEFORE INSERT ON `event` FOR EACH ROW BEGIN
    IF NEW.date < NOW() THEN
        SET NEW.status = '��襫';
    ELSE
        SET NEW.status = '�㤥�';
    END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = cp866 */ ;
/*!50003 SET character_set_results = cp866 */ ;
/*!50003 SET collation_connection  = cp866_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `update_event_status_on_update` BEFORE UPDATE ON `event` FOR EACH ROW BEGIN
    IF NEW.date < NOW() THEN
        SET NEW.status = '��襫';
    ELSE
        SET NEW.status = '�㤥�';
    END IF;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `event_class`
--

DROP TABLE IF EXISTS `event_class`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `event_class` (
  `event_id` int NOT NULL,
  `class_parallel` int NOT NULL,
  `book_name` varchar(255) NOT NULL COMMENT 'Название книги для параллели классов',
  PRIMARY KEY (`event_id`,`class_parallel`),
  CONSTRAINT `fk_event_parallel` FOREIGN KEY (`event_id`) REFERENCES `event` (`id`) ON DELETE CASCADE,
  CONSTRAINT `check_parallel_range` CHECK ((`class_parallel` between 1 and 4)),
  CONSTRAINT `chk_class_parallel` CHECK ((`class_parallel` > 0))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `event_class`
--

LOCK TABLES `event_class` WRITE;
/*!40000 ALTER TABLE `event_class` DISABLE KEYS */;
INSERT INTO `event_class` VALUES (1,1,'Сказка о рыбаке и рыбке'),(1,2,'Муха-Цокотуха'),(1,3,'Алиса в Стране чудес'),(2,1,'Волшебник Изумрудного города'),(2,2,'Приключения Незнайки'),(3,1,'Название книги для 1 параллели'),(3,2,'Название книги для 2 параллели'),(3,3,'Название книги для 3 параллели'),(7,1,'dd'),(8,1,'аааааааааа'),(9,1,'аааааааааа'),(18,2,'ааа'),(18,4,'аавава'),(19,2,'ааа'),(19,4,'аавава'),(20,2,'пароиз');
/*!40000 ALTER TABLE `event_class` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `school_class`
--

DROP TABLE IF EXISTS `school_class`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `school_class` (
  `id` int NOT NULL AUTO_INCREMENT,
  `class_number` tinyint NOT NULL,
  `class_letter` char(1) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `class_number` (`class_number`,`class_letter`),
  CONSTRAINT `school_class_chk_1` CHECK ((`class_number` between 1 and 4))
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `school_class`
--

LOCK TABLES `school_class` WRITE;
/*!40000 ALTER TABLE `school_class` DISABLE KEYS */;
INSERT INTO `school_class` VALUES (1,1,'А'),(2,1,'Б'),(3,1,'В'),(4,1,'Г'),(17,1,'Д'),(5,2,'А'),(6,2,'Б'),(7,2,'В'),(8,2,'Г'),(18,2,'Е'),(9,3,'А'),(10,3,'Б'),(11,3,'В'),(12,3,'Г'),(19,3,'Ж'),(13,4,'А'),(14,4,'Б'),(15,4,'В'),(16,4,'Г'),(20,4,'И');
/*!40000 ALTER TABLE `school_class` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `school_year`
--

DROP TABLE IF EXISTS `school_year`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `school_year` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `period` varchar(10) NOT NULL,
  `start_date` date NOT NULL,
  `end_date` date NOT NULL,
  `is_current` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  CONSTRAINT `date_check` CHECK ((`end_date` > `start_date`))
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `school_year`
--

LOCK TABLES `school_year` WRITE;
/*!40000 ALTER TABLE `school_year` DISABLE KEYS */;
INSERT INTO `school_year` VALUES (1,'Год семьи','2024-2025','2024-09-01','2025-08-31',0),(2,'Год образования','2025-2026','2025-09-01','2026-08-31',1),(3,'Год науки','2027-2028','2027-09-01','2028-08-31',0),(4,'Год будущего','2028-2029','2028-09-01','2029-08-31',0);
/*!40000 ALTER TABLE `school_year` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `student`
--

DROP TABLE IF EXISTS `student`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `student` (
  `id` int NOT NULL AUTO_INCREMENT,
  `first_name` varchar(50) NOT NULL,
  `last_name` varchar(50) NOT NULL,
  `enrollment_date` date NOT NULL COMMENT 'Дата зачисления студента (не может быть NULL)',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `student`
--

LOCK TABLES `student` WRITE;
/*!40000 ALTER TABLE `student` DISABLE KEYS */;
INSERT INTO `student` VALUES (1,'Иван','Иванов','2024-09-01'),(2,'Петр','Петров','2024-09-01'),(3,'Мария','Сидорова','2024-09-01'),(4,'Анна','Смирнова','2024-09-01'),(5,'Алексей','Кузнецов','2024-09-01'),(6,'Елена','Попова','2024-09-01'),(7,'Сергей','Васильев','2024-09-01'),(8,'Ольга','Павлова','2024-09-01'),(9,'Дмитрий','Семенов','2024-09-01'),(10,'Наталья','Голубева','2024-09-01'),(11,'Андрей','Виноградов','2024-09-01'),(12,'Татьяна','Белова','2024-09-01'),(13,'Михаил','Федоров','2024-09-01'),(14,'Екатерина','Морозова','2024-09-01'),(15,'Артем','Николаев','2024-09-01'),(16,'Юлия','Волкова','2024-09-01'),(17,'Александр','Лебедев','2024-09-01'),(18,'Виктория','Соколова','2024-09-01'),(19,'Георгий','Михайлов','2024-09-01'),(20,'Дарья','Новикова','2024-09-01'),(21,'Евгений','Фролов','2024-09-01'),(22,'Жанна','Воробьева','2024-09-01'),(23,'Захар','Поляков','2024-09-01'),(24,'Ирина','Цветкова','2024-09-01'),(25,'Константин','Дмитриев','2024-09-01'),(26,'Лариса','Данилова','2024-09-01'),(27,'Максим','Жуков','2024-09-01'),(28,'Надежда','Крылова','2024-09-01'),(29,'Олег','Тихонов','2024-09-01'),(30,'Полина','Власова','2024-09-01'),(31,'Роман','Маслов','2024-09-01'),(32,'Светлана','Широкова','2024-09-01'),(33,'Владимир','Комаров','2023-09-01'),(34,'Галина','Орлова','2023-09-01'),(35,'Борис','Захаров','2023-09-01'),(36,'Зоя','Соловьева','2023-09-01'),(37,'Валентин','Баранов','2023-09-01'),(38,'Лидия','Романова','2023-09-01'),(39,'Станислав','Козлов','2023-09-01'),(40,'Раиса','Ильина','2023-09-01'),(41,'Анастасия','Кузьмина','2024-09-01'),(42,'Виктор','Петров','2024-09-01');
/*!40000 ALTER TABLE `student` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `student_class_mapping`
--

DROP TABLE IF EXISTS `student_class_mapping`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `student_class_mapping` (
  `id` int NOT NULL AUTO_INCREMENT,
  `student_id` int NOT NULL,
  `class_id` int NOT NULL,
  `year_id` int NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `student_id` (`student_id`,`year_id`),
  KEY `class_id` (`class_id`),
  KEY `year_id` (`year_id`),
  CONSTRAINT `student_class_mapping_ibfk_1` FOREIGN KEY (`student_id`) REFERENCES `student` (`id`),
  CONSTRAINT `student_class_mapping_ibfk_2` FOREIGN KEY (`class_id`) REFERENCES `school_class` (`id`),
  CONSTRAINT `student_class_mapping_ibfk_3` FOREIGN KEY (`year_id`) REFERENCES `school_year` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=85 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `student_class_mapping`
--

LOCK TABLES `student_class_mapping` WRITE;
/*!40000 ALTER TABLE `student_class_mapping` DISABLE KEYS */;
INSERT INTO `student_class_mapping` VALUES (1,1,1,2),(2,17,1,2),(3,2,2,2),(4,18,2,2),(5,3,3,2),(6,19,3,2),(7,4,4,2),(8,20,4,2),(9,5,5,2),(10,21,5,2),(11,6,6,2),(12,22,6,2),(13,7,7,2),(14,23,7,2),(15,8,8,2),(16,24,8,2),(17,9,9,2),(18,25,9,2),(19,10,10,2),(20,26,10,2),(21,11,11,2),(22,27,11,2),(23,12,12,2),(24,28,12,2),(25,13,13,2),(26,29,13,2),(27,14,14,2),(28,30,14,2),(29,15,15,2),(30,31,15,2),(31,16,16,2),(32,32,16,2),(41,33,1,1),(42,34,1,1),(43,35,6,1),(44,36,6,1),(45,37,11,1),(46,38,11,1),(47,39,16,1),(48,40,16,1),(52,5,1,1),(53,21,1,1),(54,6,1,1),(55,22,1,1),(56,7,1,1),(57,23,1,1),(58,8,1,1),(59,24,1,1),(60,9,5,1),(61,25,5,1),(62,10,5,1),(63,26,5,1),(64,11,5,1),(65,27,5,1),(66,12,5,1),(67,28,5,1),(68,13,9,1),(69,29,9,1),(70,14,9,1),(71,30,9,1),(72,15,9,1),(73,31,9,1),(74,16,9,1),(75,32,9,1),(83,41,1,2),(84,42,2,2);
/*!40000 ALTER TABLE `student_class_mapping` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `student_rating`
--

DROP TABLE IF EXISTS `student_rating`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `student_rating` (
  `id` int NOT NULL AUTO_INCREMENT,
  `mapping_id` int NOT NULL,
  `points` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `mapping_id` (`mapping_id`),
  CONSTRAINT `student_rating_ibfk_1` FOREIGN KEY (`mapping_id`) REFERENCES `student_class_mapping` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `student_rating`
--

LOCK TABLES `student_rating` WRITE;
/*!40000 ALTER TABLE `student_rating` DISABLE KEYS */;
INSERT INTO `student_rating` VALUES (1,83,75),(2,84,90);
/*!40000 ALTER TABLE `student_rating` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-06-13 17:41:20
