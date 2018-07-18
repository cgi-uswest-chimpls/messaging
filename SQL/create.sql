CREATE TABLE `messages` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `FromId` int(11) NOT NULL,
  `FromUserType` tinyint(4) NOT NULL,
  `ToId` int(11) NOT NULL,
  `ToUserType` tinyint(4) NOT NULL,
  `Title` varchar(45) NOT NULL,
  `Content` varchar(1000) DEFAULT NULL,
  `Attachment` varchar(500) DEFAULT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `DeletedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  KEY `From` (`FromId`,`FromUserType`,`DeletedDate`),
  KEY `To` (`ToId`,`ToUserType`,`DeletedDate`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;