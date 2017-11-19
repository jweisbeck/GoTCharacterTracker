DROP TABLE IF EXISTS orgs_orgTypes;

CREATE TABLE `orgs_orgTypes` (
	`id` int(11) NOT NULL AUTO_INCREMENT,
	`organizationId` INT NOT NULL,
	`organizationTypeId` INT NOT NULL,
	PRIMARY KEY (`id`),
	CONSTRAINT `orgs_orgTypes_ibfk_1` FOREIGN KEY (`organizationId`) REFERENCES `organizations` (`id`) ON DELETE CASCADE,
	CONSTRAINT `orgs_orgTypes_ibfk_2` FOREIGN KEY (`organizationTypeId`) REFERENCES `organizationTypes` (`id`) ON DELETE CASCADE
) COMMENT='Many-to-many storage of organizations to type of organizations';

