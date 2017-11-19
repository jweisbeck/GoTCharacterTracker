CREATE TABLE `houses` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `houseWords` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) COMMENT='Stores GoT families';
