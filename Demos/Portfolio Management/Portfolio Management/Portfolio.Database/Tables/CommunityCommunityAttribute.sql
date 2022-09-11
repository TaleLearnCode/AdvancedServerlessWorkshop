CREATE TABLE PM.CommunityCommunityAttribute
(
  CommunityCommunityAttributeId INT NOT NULL IDENTITY(1,1),
  CommunityId                   INT NOT NULL,
  CommunityAttributeId          INT NOT NULL,
  IsFeatured                    BIT NOT NULL CONSTRAINT dfCommunityCommunityAttribute_IsFeatured DEFAULT (0),
  CONSTRAINT pkcCommunityCommunityAttribute PRIMARY KEY CLUSTERED (CommunityCommunityAttributeId),
  CONSTRAINT fkCommunityCommunityAttribute_Community          FOREIGN KEY (CommunityId)          REFERENCES PM.Community (CommunityId),
  CONSTRAINT fkCommunityCommunityAttribute_CommunityAttribute FOREIGN KEY (CommunityAttributeId) REFERENCES PM.CommunityAttribute (CommunityAttributeId)
)
GO

EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityCommunityAttribute',                                                          @value=N'Links a digital asset to a community.',                                                                              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityCommunityAttribute', @level2name=N'CommunityCommunityAttributeId',            @value=N'Identifier for the CommunityCommunityAttribute record.',                                                             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityCommunityAttribute', @level2name=N'CommunityId',                              @value=N'Identifier for the associated community.',                                                                           @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityCommunityAttribute', @level2name=N'CommunityAttributeId',                     @value=N'Identifier for the associated community attribute.',                                                                 @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityCommunityAttribute', @level2name=N'IsFeatured',                               @value=N'Flag indicating whether the community attribute is to be featured for the community.',                               @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityCommunityAttribute', @level2name=N'pkcCommunityCommunityAttribute',           @value=N'Defines the primary key for the CommunityCommunityAttribute record using the CommunityCommunityAttributeId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityCommunityAttribute', @level2name=N'dfCommunityCommunityAttribute_IsFeatured', @value=N'Defines the default value for the IsFetured column as 0 (false).',                                                   @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO