namespace SLS.Porfolio.Repository;

internal static partial class CreateModel
{

	internal static void CommunityDigitalAsset(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<CommunityDigitalAsset>(entity =>
		{
			entity.ToTable("CommunityDigitalAsset", "PM");

			entity.HasComment("Links a digital asset to a community.");

			entity.Property(e => e.CommunityDigitalAssetId).HasComment("Identifier for the CommunityDigitalAsset record.");

			entity.Property(e => e.CommunityId).HasComment("Identifier for the associated community.");

			entity.Property(e => e.DigitalAssetId).HasComment("Identifier for the associated digital asset.");

			entity.Property(e => e.IsFeatured).HasComment("Flag indicating whether the digital asset is to be featured for the community.");

			entity.Property(e => e.SortOrder).HasComment("The sorting order of the digital asset within the community.");

			entity.HasOne(d => d.Community)
								.WithMany(p => p.CommunityDigitalAssets)
								.HasForeignKey(d => d.CommunityId)
								.OnDelete(DeleteBehavior.ClientSetNull)
								.HasConstraintName("fkCommunityDigitalAsset_Community");

			entity.HasOne(d => d.DigitalAsset)
								.WithMany(p => p.CommunityDigitalAssets)
								.HasForeignKey(d => d.DigitalAssetId)
								.OnDelete(DeleteBehavior.ClientSetNull)
								.HasConstraintName("fkCommunityDigitalAsset_DigitalAsset");
		});
	}

}