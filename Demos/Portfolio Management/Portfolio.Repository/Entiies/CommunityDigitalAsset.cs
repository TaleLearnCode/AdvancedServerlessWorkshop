using System;
using System.Collections.Generic;

namespace SLS.Porfolio.Repository.Entiies
{
    /// <summary>
    /// Links a digital asset to a community.
    /// </summary>
    public partial class CommunityDigitalAsset
    {
        /// <summary>
        /// Identifier for the CommunityDigitalAsset record.
        /// </summary>
        public int CommunityDigitalAssetId { get; set; }
        /// <summary>
        /// Identifier for the associated community.
        /// </summary>
        public int CommunityId { get; set; }
        /// <summary>
        /// Identifier for the associated digital asset.
        /// </summary>
        public int DigitalAssetId { get; set; }
        /// <summary>
        /// The sorting order of the digital asset within the community.
        /// </summary>
        public int? SortOrder { get; set; }
        /// <summary>
        /// Flag indicating whether the digital asset is to be featured for the community.
        /// </summary>
        public bool IsFeatured { get; set; }

        public virtual Community Community { get; set; } = null!;
        public virtual DigitalAsset DigitalAsset { get; set; } = null!;
    }
}
