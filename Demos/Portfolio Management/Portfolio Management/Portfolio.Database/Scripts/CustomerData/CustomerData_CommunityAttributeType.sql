SET IDENTITY_INSERT PM.CommunityAttributeType ON
GO

MERGE PM.CommunityAttributeType AS TARGET
USING (VALUES (16, 1, 1, 'amenities', 'Amenities'),
              (17, 2, 1, 'services',  'Services'),
              (18, 3, 1, 'features',  'Features'))
AS SOURCE (CommunityAttributeTypeId,
           SortOrder,
           RowStatusId,
           ExternalId,
           CommunityAttributeTypeName)
ON TARGET.CommunityAttributeTypeId = SOURCE.CommunityAttributeTypeId
WHEN MATCHED THEN UPDATE SET TARGET.ExternalId                 = SOURCE.ExternalId,
                             TARGET.CommunityAttributeTypeName = SOURCE.CommunityAttributeTypeName,
                             TARGET.SortOrder                  = SOURCE.SortOrder,
                             TARGET.RowStatusId                = SOURCE.RowStatusId
WHEN NOT MATCHED THEN INSERT (CommunityAttributeTypeId,
                              ExternalId,
                              CommunityAttributeTypeName,
                              SortOrder,
                              RowStatusId)
                      VALUES (SOURCE.CommunityAttributeTypeId,
                              SOURCE.ExternalId,
                              SOURCE.CommunityAttributeTypeName,
                              SOURCE.SortOrder,
                              SOURCE.RowStatusId);
GO

SET IDENTITY_INSERT PM.CommunityAttributeType OFF
GO