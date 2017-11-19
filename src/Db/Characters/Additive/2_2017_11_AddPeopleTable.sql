DROP TABLE IF EXISTS people;
CREATE TABLE `people` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `surname` varchar(255) DEFAULT NULL,
  `isAlive` int(11) NOT NULL,
  `houseId` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `person_ibfk_1` (`houseId`),
  CONSTRAINT `people_ibfk_1` FOREIGN KEY (`houseId`) REFERENCES `houses` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COMMENT='Stores central entity representing GoT Characters';
