CREATE TABLE `gotCharacterTracker`.`objects` (
	`id` INT NOT NULL,
	`name` VARCHAR(45) NOT NULL,
	`personId` INT NULL,
	PRIMARY KEY (`id`),
	CONSTRAINT `objects_ibfk_1` FOREIGN KEY (`personId`) REFERENCES `people` (`id`) ON DELETE CASCADE
) COMMENT='Stores precious objects belonging to a character';