CREATE TABLE `people` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `surname` varchar(255) DEFAULT NULL,
  `isAlive` int(11) NOT NULL,
  `houseId` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `person_ibfk_1` (`houseId`),
  FOREIGN KEY (`houseId`) REFERENCES `houses` (`id`) ON DELETE CASCADE
) COMMENT='Stores central entity representing GoT Characters';
