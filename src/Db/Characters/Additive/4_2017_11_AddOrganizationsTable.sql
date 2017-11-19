DROP TABLE IF EXISTS `organizations`;

CREATE TABLE `organizations` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `description` text,
  `typeId` Int(11) NOT NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `person_ibfk_1` FOREIGN KEY (`typeId`) REFERENCES `organizationTypes` (`id`) ON DELETE CASCADE
) COMMENT='Stores various organizations present in the series';
