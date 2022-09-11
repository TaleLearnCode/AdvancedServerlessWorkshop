CREATE TABLE PM.CommunityAttributeType
(
  CommunityAttributeTypeId   INT           NOT NULL IDENTITY(1,1),
  ExternalId                 NVARCHAR(100)     NULL,
  CommunityAttributeTypeName NVARCHAR(100)     NULL,
  SortOrder                  INT               NULL,
  RowStatusId                INT           NOT NULL CONSTRAINT dfCommunityAttributeType_RowStatusId DEFAULT(1),
  CONSTRAINT pkcCommunityAttributeType PRIMARY KEY CLUSTERED (CommunityAttributeTypeId)
)
GO

EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityAttributeType',                                            @value=N'Represents a type of community attribute used to group community attributes.',                             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityAttributeType', @level2name=N'CommunityAttributeTypeId',   @value=N'Identifier for the community attribute type record.',                                                      @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityAttributeType', @level2name=N'ExternalId',                 @value=N'The tenant''s identifier for the community attribute type.',                                               @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityAttributeType', @level2name=N'CommunityAttributeTypeName', @value=N'Name of the community attribute type.',                                                                    @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityAttributeType', @level2name=N'SortOrder',                  @value=N'Sorting order of the community attribute type.',                                                           @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityAttributeType', @level2name=N'RowStatusId',                @value=N'Identifier of the community attribute type record status (i.e. enabled, disabled, etc).',                  @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityAttributeType', @level2name=N'pkcCommunityAttributeType',  @value=N'Defines the primary key for the CommunityAttributeType record using the CommunityAttributeTypeId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO