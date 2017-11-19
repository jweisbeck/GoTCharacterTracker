DROP TABLE IF EXISTS people_orgs;

CREATE TABLE `people_orgs` (
	`id` int(11) NOT NULL AUTO_INCREMENT,
	`personId` INT NOT NULL,
	`organizationId` INT NOT NULL,
	PRIMARY KEY (`id`),
	FOREIGN KEY (`personId`) REFERENCES `people` (`id`) ON DELETE CASCADE,
	FOREIGN KEY (`organizationId`) REFERENCES `organizations` (`id`) ON DELETE CASCADE
) COMMENT='Many-to-many storage of relationships between people and organizations';
