using System;
using System.Collections.Generic;

namespace SLS.Porfolio.Repository.Entiies
{
    /// <summary>
    /// Represents an attribute to be associated to a community.
    /// </summary>
    public partial class CommunityAttribute
    {
        public CommunityAttribute()
        {
            CommunityCommunityAttributes = new HashSet<CommunityCommunityAttribute>();
        }

        /// <summary>
        /// Identifier for the community attribute record.
        /// </summary>
        public int CommunityAttributeId { get; set; }
        /// <summary>
        /// The tenant&apos;s identifier for the community attribute.
        /// </summary>
        public string? ExternalId { get; set; }
        /// <summary>
        /// Identifier of the type of community attribute is being represented by the record.
        /// </summary>
        public int CommunityAttributeTypeId { get; set; }
        public string CommunityAttributeName { get; set; } = null!;
        /// <summary>
        /// Identifier of the Content record representing the label for the Community Attribute.
        /// </summary>
        public int? LabelId { get; set; }
        /// <summary>
        /// Identifier of the DigitalAsset record representing the icon for the Community Attribute.
        /// </summary>
        public int? IconId { get; set; }
        /// <summary>
        /// Identifier of the community attribute type record status (i.e. enabled, disabled, etc).
        /// </summary>
        public int RowStatusId { get; set; }

        public virtual CommunityAttributeType CommunityAttributeType { get; set; } = null!;
        public virtual ICollection<CommunityCommunityAttribute> CommunityCommunityAttributes { get; set; }
    }
}
