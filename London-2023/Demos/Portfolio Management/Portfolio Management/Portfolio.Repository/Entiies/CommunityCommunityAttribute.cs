namespace SLS.Porfolio.Repository.Entiies
{
	/// <summary>
	/// Links a digital asset to a community.
	/// </summary>
	public partial class CommunityCommunityAttribute
	{
		/// <summary>
		/// Identifier for the CommunityCommunityAttribute record.
		/// </summary>
		public int CommunityCommunityAttributeId { get; set; }
		/// <summary>
		/// Identifier for the associated community.
		/// </summary>
		public int CommunityId { get; set; }
		/// <summary>
		/// Identifier for the associated community attribute.
		/// </summary>
		public int CommunityAttributeId { get; set; }
		/// <summary>
		/// Flag indicating whether the community attribute is to be featured for the community.
		/// </summary>
		public bool IsFeatured { get; set; }

		public virtual Community Community { get; set; } = null!;
		public virtual CommunityAttribute CommunityAttribute { get; set; } = null!;
	}
}
