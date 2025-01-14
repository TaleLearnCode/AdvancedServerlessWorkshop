﻿SET IDENTITY_INSERT PM.CommunityDigitalAsset ON
GO

MERGE PM.CommunityDigitalAsset AS TARGET
USING (VALUES 
              -- Northstar Moon Over Nowhere
              (  1, 1, 165,  1, 0),
              (  2, 1, 166,  1, 0),
              (  3, 1, 167,  2, 0),
              (  4, 1, 168,  2, 0),
              (  5, 1, 169,  3, 0),
              (  6, 1, 170,  3, 0),
              (  7, 1, 171,  4, 0),
              (  8, 1, 172,  4, 0),
              (  9, 1, 173,  5, 0),
              ( 10, 1, 174,  5, 0),
              ( 11, 1, 175,  6, 1),
              ( 12, 1, 176,  6, 1),

              -- Northstar Homeowners Realty
              ( 13, 2, 177, 10, 1),
              ( 14, 2, 178, 10, 1),
              ( 15, 2, 179,  1, 0),
              ( 16, 2, 180,  1, 0),
              ( 17, 2, 181,  2, 0),
              ( 18, 2, 182,  2, 0),
              ( 19, 2, 183,  3, 0),
              ( 20, 2, 184,  3, 0),
              ( 21, 2, 185,  4, 0),
              ( 22, 2, 186,  4, 0),
              ( 23, 2, 187,  5, 0),
              ( 24, 2, 188,  5, 0),
              ( 25, 2, 189,  6, 0),
              ( 26, 2, 190,  6, 0),
              ( 27, 2, 191,  7, 0),
              ( 28, 2, 192,  7, 0),
              ( 29, 2, 193,  8, 0),
              ( 30, 2, 194,  8, 0),
              ( 31, 2, 195,  9, 0),
              ( 32, 2, 196,  9, 0),

              -- Northstar La Casa Grande
              ( 33, 3, 197,  1, 0),
              ( 34, 3, 198,  1, 0),
              ( 35, 3, 199,  2, 0),
              ( 36, 3, 200,  2, 0),
              ( 37, 3, 201,  3, 0),
              ( 38, 3, 202,  3, 0),
              ( 39, 3, 203,  4, 0),
              ( 40, 3, 204,  4, 0),
              ( 41, 3, 205,  5, 1),
              ( 42, 3, 206,  5, 1),

              -- Northstar Tower of Hope
              ( 43, 6, 207,  2, 0),
              ( 44, 6, 208,  2, 0),
              ( 45, 6, 209,  3, 0),
              ( 46, 6, 210,  3, 0),
              ( 47, 6, 211,  4, 0),
              ( 48, 6, 212,  4, 0),
              ( 49, 6, 213,  5, 0),
              ( 50, 6, 214,  5, 0),
              ( 51, 6, 215,  6, 0),
              ( 52, 6, 216,  6, 0),
              ( 53, 6, 217,  7, 0),
              ( 54, 6, 218,  7, 0),
              ( 55, 6, 219,  8, 0),
              ( 56, 6, 220,  8, 0),
              ( 57, 6, 221,  9, 0),
              ( 58, 6, 222,  9, 0),
              ( 59, 6, 223, 15, 1),
              ( 60, 6, 224, 15, 1),
              ( 61, 6, 225,  1, 0),
              ( 62, 6, 226,  1, 0),
              ( 63, 6, 227, 10, 0),
              ( 64, 6, 228, 10, 0),
              ( 65, 6, 229, 11, 0),
              ( 66, 6, 230, 11, 0),
              ( 67, 6, 231, 12, 0),
              ( 68, 6, 232, 12, 0),
              ( 69, 6, 233, 13, 0),
              ( 70, 6, 234, 13, 0),
              ( 71, 6, 235, 16, 1),
              ( 72, 6, 236, 16, 1),

              -- Northstar Exuberanch
              ( 73, 7, 237,  1, 0),
              ( 74, 7, 238,  1, 0),
              ( 75, 7, 239,  2, 0),
              ( 76, 7, 240,  2, 0),
              ( 77, 7, 241,  3, 0),
              ( 78, 7, 242,  3, 0),
              ( 79, 7, 243,  6, 1),
              ( 80, 7, 244,  6, 1),
              ( 81, 7, 245,  4, 0),
              ( 82, 7, 246,  4, 0),
              ( 83, 7, 247,  5, 1),
              ( 84, 7, 248,  5, 1),

              -- Northstar Stone Ranch
              ( 85, 9, 249,  1, 0),
              ( 86, 9, 250,  1, 0),
              ( 87, 9, 251,  2, 0),
              ( 88, 9, 252,  2, 0),
              ( 89, 9, 253,  6, 1),
              ( 90, 9, 254,  6, 1),
              ( 91, 9, 255,  7, 1),
              ( 92, 9, 256,  7, 1),
              ( 93, 9, 257,  3, 0),
              ( 94, 9, 258,  3, 0),
              ( 95, 9, 259,  4, 0),
              ( 96, 9, 260,  4, 0),
              ( 97, 9, 261,  8, 1),
              ( 98, 9, 262,  8, 1),
              ( 99, 9, 263,  5, 0),
              (100, 9, 264,  5, 0))
AS SOURCE (CommunityDigitalAssetId,
           CommunityId,
           DigitalAssetId,
           SortOrder,
           IsFeatured)
ON TARGET.CommunityDigitalAssetId = SOURCE.CommunityDigitalAssetId
WHEN MATCHED THEN UPDATE SET TARGET.CommunityId    = SOURCE.CommunityId,
                             TARGET.DigitalAssetId = SOURCE.DigitalAssetId,
                             TARGET.IsFeatured     = SOURCE.IsFeatured
WHEN NOT MATCHED THEN INSERT (CommunityDigitalAssetId,
                              CommunityId,
                              DigitalAssetId,
                              SortOrder,
                              IsFeatured)
                      VALUES (SOURCE.CommunityDigitalAssetId,
                              SOURCE.CommunityId,
                              SOURCE.DigitalAssetId,
                              SOURCE.SortOrder,
                              SOURCE.IsFeatured);

SET IDENTITY_INSERT PM.CommunityDigitalAsset OFF
GO