CREATE TABLE PM.CommunityDigitalAsset
(
  CommunityDigitalAssetId INT NOT NULL IDENTITY(1,1),
  CommunityId             INT NOT NULL,
  DigitalAssetId          INT NOT NULL,
  SortOrder               INT     NULL,
  IsFeatured              BIT NOT NULL CONSTRAINT dfCommunityDigitalAsset_IsFeatured DEFAULT(0),
  CONSTRAINT pkcCommunityDigitlAsset PRIMARY KEY CLUSTERED (CommunityDigitalAssetId),
  CONSTRAINT fkCommunityDigitalAsset_Community FOREIGN KEY (CommunityId) REFERENCES PM.Community (CommunityId),
  CONSTRAINT fkCommunityDigitalAsset_DigitalAsset FOREIGN KEY (DigitalAssetId) REFERENCES PM.DigitalAsset (DigitalAssetId)
)
GO

EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityDigitalAsset',                                                      @value=N'Links a digital asset to a community.',                                                                               @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityDigitalAsset', @level2name=N'CommunityDigitalAssetId',              @value=N'Identifier for the CommunityDigitalAsset record.',                                                                    @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityDigitalAsset', @level2name=N'CommunityId',                          @value=N'Identifier for the associated community.',                                                                            @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityDigitalAsset', @level2name=N'DigitalAssetId',                       @value=N'Identifier for the associated digital asset.',                                                                        @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityDigitalAsset', @level2name=N'SortOrder',                            @value=N'The sorting order of the digital asset within the community.',                                                        @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityDigitalAsset', @level2name=N'IsFeatured',                           @value=N'Flag indicating whether the digital asset is to be featured for the community.',                                      @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityDigitalAsset', @level2name=N'pkcCommunityDigitlAsset',              @value=N'Defines the primary key for the CommunityDigitalAsset record using the CommunityDigitalAssetId column.',              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityDigitalAsset', @level2name=N'fkCommunityDigitalAsset_Community',    @value=N'Defines the relationship between the CommunityDigitalAsset and Community tables using the CommunityId column.',       @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityDigitalAsset', @level2name=N'fkCommunityDigitalAsset_DigitalAsset', @value=N'Defines the relationship between the CommunityDigitalAsset and DigitalAsset tables using the DigitalAssetId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityDigitalAsset', @level2name=N'dfCommunityDigitalAsset_IsFeatured',   @value=N'Defines the default value for the IsFetured column as 0 (false).',                                                    @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO