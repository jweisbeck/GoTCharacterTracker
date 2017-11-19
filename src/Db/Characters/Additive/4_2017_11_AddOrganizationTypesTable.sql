DROP TABLE IF EXISTS organizationTypes;

CREATE TABLE `organizationTypes` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) NOT NULL,
  `description` TEXT,
  PRIMARY KEY (`id`)
) COMMENT='Adds storage of organization types';

