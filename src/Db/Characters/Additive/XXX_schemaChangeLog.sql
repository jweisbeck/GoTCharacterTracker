
CREATE TABLE `schemaChangeLog` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `releaseNumber` INT NOT NULL,
  `scriptName` varchar(255) NOT NULL,
  `dateApplie` DATETIME NOT NULL,
  PRIMARY KEY (`id`)
) COMMENT='Stores schema versions via migrations/updates';
