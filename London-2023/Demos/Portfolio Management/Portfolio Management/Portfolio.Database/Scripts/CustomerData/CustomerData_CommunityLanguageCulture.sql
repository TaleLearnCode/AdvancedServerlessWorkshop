SET IDENTITY_INSERT PM.CommunityLanguageCulture ON
GO

MERGE PM.CommunityLanguageCulture AS TARGET
USING (VALUES ( 1, 1, 'en-US'),
              ( 2, 2, 'en-US'),
              ( 3, 2, 'en-US'),
              ( 4, 2, 'en-US'),
              ( 5, 2, 'en-US'),
              ( 6, 2, 'en-US'),
              ( 7, 2, 'en-US'),
              ( 8, 2, 'en-US'),
              ( 9, 2, 'en-US'),
              (10, 2, 'en-US'),
              (11, 2, 'en-US'),
              (12, 2, 'en-US'),
              (13, 2, 'en-US'),
              (14, 2, 'en-US'),
              (15, 2, 'en-US'),
              (16, 2, 'en-US'),
              (17, 2, 'en-US'),
              (18, 2, 'en-US'),
              (19, 2, 'en-US'),
              (20, 2, 'en-US'),
              (21, 2, 'en-US'),
              (22, 2, 'en-US'),
              (23, 2, 'en-US'),
              (24, 2, 'en-US'),
              (25, 2, 'en-US'),
              (26, 2, 'en-US'),
              (27, 2, 'en-US'),
              (28, 2, 'en-US'),
              (29, 2, 'en-US'),
              (30, 2, 'en-US'),
              (31, 2, 'en-US'),
              (32, 2, 'en-US'),
              (33, 2, 'en-US'),
              (34, 2, 'en-US'),
              (35, 2, 'en-US'),
              (36, 2, 'en-US'),
              (37, 2, 'en-US'),
              (38, 2, 'en-US'),
              (39, 2, 'en-US'),
              (40, 2, 'en-US'),
              (41, 2, 'en-US'),
              (42, 2, 'en-US'),
              (43, 2, 'en-US'),
              (44, 2, 'en-US'),
              (45, 2, 'en-US'),
              (46, 2, 'en-US'),
              (47, 2, 'en-US'),
              (48, 2, 'en-US'),
              (49, 2, 'en-US'),
              (50, 2, 'en-US'))
AS SOURCE (CommunityLanguageCultureId,
           CommunityId,
           LanguageCultureCode)
ON TARGET.CommunityLanguageCultureId = SOURCE.CommunityLanguageCultureId
WHEN MATCHED THEN UPDATE SET TARGET.CommunityId         = SOURCE.ExternalId,
                             TARGET.LanguageCultureCode = SOURCE.LanguageCultureCode
WHEN NOT MATCHED THEN INSERT (CommunityLanguageCultureId,
                              CommunityId,
                              LanguageCultureCode)
                      VALUES (SOURCE.CommunityLanguageCultureId,
                              SOURCE.CommunityId,
                              SOURCE.LanguageCultureCode);
GO

SET IDENTITY_INSERT PM.CommunityLanguageCulture OFF
GO