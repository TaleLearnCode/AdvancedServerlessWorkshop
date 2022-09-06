﻿SET IDENTITY_INSERT PM.Community ON
GO

MERGE PM.Community AS TARGET
USING (VALUES ( 1, '30100', 'Moon Over Nowhere', 'Northstar Moon Over Nowhere', 'en-US', 'sq ft', 555, 'US', '-54.567', '49.2969', 265, 2, 0, 1),
              ( 2, '30101', 'Homeowners Realty', 'Northstar Homeowners Realty', 'en-US', 'sq ft', 556, 'US', '-20.3845', '43.9132', 266, 2, 0, 1),
              ( 3, '30102', 'La Casa Grande', 'Northstar La Casa Grande', 'en-US', 'sq ft', 557, 'US', '63.0826', '-130.8007', 267, 2, 0, 1),
              ( 4, '30103', 'Fashion-I-Best', 'Northstar Fashion-I-Best', 'en-US', 'sq ft', NULL, 'US', '-67.6693', '-154.3146', NULL, 2, 0, 1),
              ( 5, '30104', 'Wise and in the Way', 'Northstar Wise and in the Way', 'en-US', 'sq ft', NULL, 'US', '22.492', '61.3031', NULL, 2, 0, 1),
              ( 6, '30105', 'Tower of Hope', 'Northstar Tower of Hope', 'en-US', 'sq ft', 558, 'US', '8.5465', '-125.1281', 268, 2, 0, 1),
              ( 7, '30106', 'Exuberanch', 'Northstar Exuberanch', 'en-US', 'sq ft', 559, 'US', '-28.1082', '-67.4955', 269, 2, 0, 1),
              ( 8, '30107', 'The Enchanted Garden', 'Northstar The Enchanted Garden', 'en-US', 'sq ft', NULL, 'US', '-51.2392', '92.6853', NULL, 2, 0, 1),
              ( 9, '30108', 'Stone Ranch', 'Northstar Stone Ranch', 'en-US', 'sq ft', 560, 'US', '-58.9132', '-17.2829', 270, 2, 0, 1),
              (10, '30109', 'Mild Manor', 'Northstar Mild Manor', 'en-US', 'sq ft', NULL, 'US', '30.8979', '81.2034', NULL, 2, 0, 1),
              (11, '30110', 'Jen & Barry’s', 'Northstar Jen & Barry’s', 'en-US', 'sq ft', NULL, 'US', '45.0338', '-172.4458', NULL, 2, 0, 1),
              (12, '30111', 'Rancho Pleasanton', 'Northstar Rancho Pleasanton', 'en-US', 'sq ft', NULL, 'US', '-11.1606', '104.0436', NULL, 2, 0, 1),
              (13, '30112', 'Sweet Virginia', 'Northstar Sweet Virginia', 'en-US', 'sq ft', NULL, 'US', '81.6293', '-159.0821', NULL, 2, 0, 1),
              (14, '30113', 'Retro Salts', 'Northstar Retro Salts', 'en-US', 'sq ft', NULL, 'US', '-0.4438', '-70.8062', NULL, 2, 0, 1),
              (15, '30114', 'The Land of Meat', 'Northstar The Land of Meat', 'en-US', 'sq ft', NULL, 'US', '78.611', '-149.2954', NULL, 2, 0, 1),
              (16, '30115', 'Coherent Community', 'Northstar Coherent Community', 'en-US', 'sq ft', NULL, 'US', '-21.059', '-31.3864', NULL, 2, 0, 1),
              (17, '30116', 'SeaSky', 'Northstar SeaSky', 'en-US', 'sq ft', NULL, 'US', '-79.3915', '150.0104', NULL, 2, 0, 1),
              (18, '30117', 'Rancho Pleasant Tan', 'Northstar Rancho Pleasant Tan', 'en-US', 'sq ft', NULL, 'US', '76.2536', '71.14', NULL, 2, 0, 1),
              (19, '30118', 'Tradeworks Inc', 'Northstar Tradeworks Inc', 'en-US', 'sq ft', NULL, 'US', '42.7786', '-17.6716', NULL, 2, 0, 1),
              (20, '30119', 'The Gluck', 'Northstar The Gluck', 'en-US', 'sq ft', NULL, 'US', '10.234', '-43.729', NULL, 2, 0, 1),
              (21, '30120', 'Gnarly Knolls', 'Northstar Gnarly Knolls', 'en-US', 'sq ft', NULL, 'US', '-50.9422', '-121.8081', NULL, 2, 0, 1),
              (22, '30121', 'The Sibbala Group', 'Northstar The Sibbala Group', 'en-US', 'sq ft', NULL, 'US', '3.8433', '-167.3958', NULL, 2, 0, 1),
              (23, '30122', 'Ezy Retirement AZ', 'Northstar Ezy Retirement AZ', 'en-US', 'sq ft', NULL, 'US', '4.5618', '12.1957', NULL, 2, 0, 1),
              (24, '30123', 'Retreat With Purpose', 'Northstar Retreat With Purpose', 'en-US', 'sq ft', NULL, 'US', '-30.8223', '-28.0189', NULL, 2, 0, 1),
              (25, '30124', 'SpireNest', 'Northstar SpireNest', 'en-US', 'sq ft', NULL, 'US', '81.4709', '-68.804', NULL, 2, 0, 1),
              (26, '30125', 'Amygdala House', 'Northstar Amygdala House', 'en-US', 'sq ft', NULL, 'US', '-30.3769', '100.8025', NULL, 2, 0, 1),
              (27, '30126', 'Flower Whispers', 'Northstar Flower Whispers', 'en-US', 'sq ft', NULL, 'US', '50.8506', '-122.5137', NULL, 2, 0, 1),
              (28, '30127', 'Virtuctura', 'Northstar Virtuctura', 'en-US', 'sq ft', NULL, 'US', '-77.7867', '127.5243', NULL, 2, 1, 1),
              (29, '30128', 'Stepford Steppes', 'Northstar Stepford Steppes', 'en-US', 'sq ft', NULL, 'US', '6.7998', '52.5193', NULL, 2, 0, 1),
              (30, '30129', 'Denver Hamlet', 'Northstar Denver Hamlet', 'en-US', 'sq ft', NULL, 'US', '-48.3476', '63.9262', NULL, 2, 0, 1),
              (31, '30130', 'Vegetable Farm', 'Northstar Vegetable Farm', 'en-US', 'sq ft', NULL, 'US', '73.5626', '78.1586', NULL, 2, 0, 1),
              (32, '30131', 'Worthridge Estates', 'Northstar Worthridge Estates', 'en-US', 'sq ft', NULL, 'US', '55.7357', '170.5941', NULL, 2, 0, 1),
              (33, '30132', 'Creative Endures', 'Northstar Creative Endures', 'en-US', 'sq ft', NULL, 'US', '-28.3476', '-4.143', NULL, 2, 0, 1),
              (34, '30133', 'Below the Beltway', 'Northstar Below the Beltway', 'en-US', 'sq ft', NULL, 'US', '-69.6391', '164.1546', NULL, 2, 0, 1),
              (35, '30134', 'Godzilla’s Playground', 'Northstar Godzilla’s Playground', 'en-US', 'sq ft', NULL, 'US', '31.7425', '153.2636', NULL, 2, 0, 1),
              (36, '30135', 'Bedlam and Broomsticks', 'Northstar Bedlam and Broomsticks', 'en-US', 'sq ft', NULL, 'US', '68.9711', '-165.9325', NULL, 2, 0, 1),
              (37, '30136', 'Boil Heights', 'Northstar Boil Heights', 'en-US', 'sq ft', NULL, 'US', '-53.8313', '97.9351', NULL, 2, 0, 1),
              (38, '30137', 'Experienced Goods', 'Northstar Experienced Goods', 'en-US', 'sq ft', NULL, 'US', '76.1136', '24.9779', NULL, 2, 0, 1),
              (39, '30138', 'The Homes Business', 'Northstar The Homes Business', 'en-US', 'sq ft', NULL, 'US', '-86.1041', '129.1317', NULL, 2, 0, 1),
              (40, '30139', 'The Art Of Shaping', 'Northstar The Art Of Shaping', 'en-US', 'sq ft', NULL, 'US', '56.6252', '142.3265', NULL, 2, 0, 1),
              (41, '30140', 'Burly Gates', 'Northstar Burly Gates', 'en-US', 'sq ft', NULL, 'US', '-17.2447', '-145.9722', NULL, 2, 0, 1),
              (42, '30141', 'Retiree’s Corner', 'Northstar Retiree’s Corner', 'en-US', 'sq ft', NULL, 'US', '10.3553', '-13.1647', NULL, 2, 0, 1),
              (43, '30142', 'Snail Trails', 'Northstar Snail Trails', 'en-US', 'sq ft', NULL, 'US', '-19.8342', '43.14', NULL, 2, 0, 1),
              (44, '30143', 'The Francis Bacon', 'Northstar The Francis Bacon', 'en-US', 'sq ft', NULL, 'US', '72.5834', '-2.2313', NULL, 2, 0, 1),
              (45, '30144', 'Constant Comment', 'Northstar Constant Comment', 'en-US', 'sq ft', NULL, 'US', '-31.9595', '-119.369', NULL, 2, 0, 1),
              (46, '30145', 'The Wedge', 'Northstar The Wedge', 'en-US', 'sq ft', NULL, 'US', '13.2485', '-99.1398', NULL, 2, 0, 1),
              (47, '30146', 'C-3 Residences', 'Northstar C-3 Residences', 'en-US', 'sq ft', NULL, 'US', '6.5501', '-53.9531', NULL, 2, 0, 1),
              (48, '30147', 'Smart-5-7 Retirement', 'Northstar Smart-5-7 Retirement', 'en-US', 'sq ft', NULL, 'US', '9.0902', '-103.1791', NULL, 2, 0, 1),
              (49, '30148', 'Casa Guana', 'Northstar Casa Guana', 'en-US', 'sq ft', NULL, 'US', '-39.7994', '18.4506', NULL, 2, 0, 1),
              (50, '30149', 'Days n Daze', 'Northstar Days n Daze', 'en-US', 'sq ft', NULL, 'US', '-14.5894', '131.9698', NULL, 2, 0, 1))
AS SOURCE (CommunityId,
           CommunityNumber,
           CommunityName,
           ExternalName,
           LanguageCultureCode,
           RoomMeasurement,
           OverviewId,
           CountryCode,
           Longitude,
           Latitude,
           ProfileImageId,
           CommunityStatusId,
           IsFeatured,
           RowStatusId)
ON TARGET.CommunityId = SOURCE.CommunityId
WHEN MATCHED THEN UPDATE SET TARGET.CommunityNumber     = SOURCE.CommunityNumber,
                             TARGET.CommunityName       = SOURCE.CommunityName,
                             TARGET.ExternalName        = SOURCE.ExternalName,
                             TARGET.LanguageCultureCode = SOURCE.LanguageCultureCode,
                             TARGET.RoomMeasurement     = SOURCE.RoomMeasurement,
                             TARGET.OverviewId          = SOURCE.OverviewId,
                             TARGET.CountryCode         = SOURCE.CountryCode,
                             TARGET.Longitude           = SOURCE.Longitude,
                             TARGET.Latitude            = SOURCE.Latitude,
                             TARGET.ProfileImageId      = SOURCE.ProfileImageId,
                             TARGET.CommunityStatusId   = SOURCE.CommunityStatusId,
                             TARGET.IsFeatured          = SOURCE.IsFeatured,
                             TARGET.RowStatusId         = SOURCE.RowStatusId
WHEN NOT MATCHED THEN INSERT (CommunityId,
                              CommunityNumber,
                              CommunityName,
                              ExternalName,
                              LanguageCultureCode,
                              RoomMeasurement,
                              OverviewId,
                              CountryCode,
                              Longitude,
                              Latitude,
                              ProfileImageId,
                              CommunityStatusId,
                              IsFeatured,
                              RowStatusId)
                      VALUES (SOURCE.CommunityId,
                              SOURCE.CommunityNumber,
                              SOURCE.CommunityName,
                              SOURCE.ExternalName,
                              SOURCE.LanguageCultureCode,
                              SOURCE.RoomMeasurement,
                              SOURCE.OverviewId,
                              SOURCE.CountryCode,
                              SOURCE.Longitude,
                              SOURCE.Latitude,
                              SOURCE.ProfileImageId,
                              SOURCE.CommunityStatusId,
                              SOURCE.IsFeatured,
                              SOURCE.RowStatusId);
GO

SET IDENTITY_INSERT PM.Community OFF
GO