DROP TABLE IF EXISTS objects;

CREATE TABLE `objects` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`name` VARCHAR(255) NOT NULL,
	`personId` INT NOT NULL,
	PRIMARY KEY (`id`),
	FOREIGN KEY (`personId`) REFERENCES `people` (`id`) ON DELETE CASCADE
) COMMENT='Stores precious objects belonging to a character';