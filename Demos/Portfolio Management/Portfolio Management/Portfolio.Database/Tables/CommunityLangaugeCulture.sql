CREATE TABLE PM.CommunityLangaugeCulture
(
  CommunityLangaugeCultureId INT         NOT NULL IDENTITY(1,1),
  CommunityId                INT         NOT NULL,
  LanguageCultureCode        VARCHAR(15) NOT NULL,
  CONSTRAINT pkcCommunityLanguageCulture PRIMARY KEY CLUSTERED (CommunityLangaugeCultureId),
  CONSTRAINT fkCommunityLanguageCulture_Community       FOREIGN KEY (CommunityId)         REFERENCES PM.Community (CommunityId),
  CONSTRAINT fkCommunityLanguageCulture_LanguageCulture FOREIGN KEY (LanguageCultureCode) REFERENCES PM.LanguageCulture (LanguageCultureCode)
)
GO

EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityLangaugeCulture',                                                            @value=N'Associates a community with language/cultures used for marketing of the community.',                                             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityLangaugeCulture', @level2name=N'CommunityLangaugeCultureId',                 @value=N'Identifier of the community language/culture record.',                                                                           @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityLangaugeCulture', @level2name=N'CommunityId',                                @value=N'Identifier of the associated community.',                                                                                        @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityLangaugeCulture', @level2name=N'LanguageCultureCode',                        @value=N'Identifier of the associated language/culture.',                                                                                 @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityLangaugeCulture', @level2name=N'pkcCommunityLanguageCulture',                @value=N'Defines the primary key for the CommunityLangaugeCulture table using the CommunityLangaugeCultureId column.',                    @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityLangaugeCulture', @level2name=N'fkCommunityLanguageCulture_Community',       @value=N'Defines the relationship between the CommunityLanguageCulture and Community tables using the CommunityId column.',               @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'PM', @level1name=N'CommunityLangaugeCulture', @level2name=N'fkCommunityLanguageCulture_LanguageCulture', @value=N'Defines the relationship between the CommunityLanguageCulture and LanguageCulture tables using the LanguageCultureCode column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO