using System;
using System.Collections.Generic;

namespace SLS.Porfolio.Repository.Entiies
{
    /// <summary>
    /// Represents a type of community attribute used to group community attributes.
    /// </summary>
    public partial class CommunityAttributeType
    {
        public CommunityAttributeType()
        {
            CommunityAttributes = new HashSet<CommunityAttribute>();
        }

        /// <summary>
        /// Identifier for the community attribute type record.
        /// </summary>
        public int CommunityAttributeTypeId { get; set; }
        /// <summary>
        /// The tenant&apos;s identifier for the community attribute type.
        /// </summary>
        public string? ExternalId { get; set; }
        /// <summary>
        /// Name of the community attribute type.
        /// </summary>
        public string? CommunityAttributeTypeName { get; set; }
        /// <summary>
        /// Sorting order of the community attribute type.
        /// </summary>
        public int? SortOrder { get; set; }
        /// <summary>
        /// Identifier of the community attribute type record status (i.e. enabled, disabled, etc).
        /// </summary>
        public int RowStatusId { get; set; }

        public virtual ICollection<CommunityAttribute> CommunityAttributes { get; set; }
    }
}
