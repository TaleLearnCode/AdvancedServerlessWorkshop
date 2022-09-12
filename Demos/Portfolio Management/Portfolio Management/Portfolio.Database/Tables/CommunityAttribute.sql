CREATE TABLE PM.CommunityAttribute
(
  CommunityAttributeId     INT           NOT NULL IDENTITY(1,1),
  ExternalId               NVARCHAR(100)     NULL,
  CommunityAttributeTypeId INT           NOT NULL,
  CommunityAttributeName   NVARCHAR(100) NOT NULL,
  LabelId                  INT               NULL,
  IconId                   INT               NULL,
  RowStatusId              INT           NOT NULL CONSTRAINT dfCommunityAttribute_RowStatus DEFAULT (1),
  CONSTRAINT pkcCommunityAttribute PRIMARY KEY CLUSTERED (CommunityAttributeId),
  CONSTRAINT fkCommunityAttribute_CommunityAttributeType FOREIGN KEY (CommunityAttributeTypeId) REFERENCES PM.CommunityAttributeType (CommunityAttributeTypeId),
  CONSTRAINT fkCommunityAttribute_Content                FOREIGN KEY (LabelId)                  REFERENCES PM.Content (ContentId),
  CONSTRAINT fkCommunityAttribute_DigitalAsset           FOREIGN KEY (IconId)                   REFERENCES PM.DigitalAsset (DigitalAssetId)
)
GO

EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityAttribute',                                                              @value=N'Represents an attribute to be associated to a community.',                                                                             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityAttribute', @level2name=N'CommunityAttributeId',                         @value=N'Identifier for the community attribute record.',                                                                                       @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityAttribute', @level2name=N'ExternalId',                                   @value=N'The tenant''s identifier for the community attribute.',                                                                                @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityAttribute', @level2name=N'CommunityAttributeTypeId',                     @value=N'Identifier of the type of community attribute is being represented by the record.',                                                    @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityAttribute', @level2name=N'LabelId',                                      @value=N'Identifier of the Content record representing the label for the Community Attribute.',                                                 @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityAttribute', @level2name=N'IconId',                                       @value=N'Identifier of the DigitalAsset record representing the icon for the Community Attribute.',                                             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityAttribute', @level2name=N'RowStatusId',                                  @value=N'Identifier of the community attribute type record status (i.e. enabled, disabled, etc).',                                              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityAttribute', @level2name=N'pkcCommunityAttribute',                        @value=N'Defines the primary key for the CommunityAttribute record using the CommunityAttributeId column.',                                     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityAttribute', @level2name=N'fkCommunityAttribute_CommunityAttributeType',  @value=N'Defines the relationship between the CommunityAttribute and CommunityAttributeType record using the CommunityAttributeTypeId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO