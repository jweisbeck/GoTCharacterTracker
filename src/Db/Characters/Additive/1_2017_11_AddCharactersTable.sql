DROP TABLE IF EXISTS `character`;

CREATE TABLE `character` (
	`id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `name` VARCHAR(255) NOT NULL,
    `surname` VARCHAR(255),
    `isAlive` INT  NOT NULL,
    `familyId` INT,
    FOREIGN KEY (`familyId`) REFERENCES `family` (`id`) ON DELETE CASCADE 
) COMMENT='Stores central entity representing GoT Characters';