namespace SLS.Porfolio.Repository;

internal static partial class CreateModel
{

	internal static void CommunityCommunityAttribute(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<CommunityCommunityAttribute>(entity =>
		{
			entity.ToTable("CommunityCommunityAttribute", "PM");

			entity.HasComment("Links a digital asset to a community.");

			entity.Property(e => e.CommunityCommunityAttributeId).HasComment("Identifier for the CommunityCommunityAttribute record.");

			entity.Property(e => e.CommunityAttributeId).HasComment("Identifier for the associated community attribute.");

			entity.Property(e => e.CommunityId).HasComment("Identifier for the associated community.");

			entity.Property(e => e.IsFeatured).HasComment("Flag indicating whether the community attribute is to be featured for the community.");

			entity.HasOne(d => d.CommunityAttribute)
								.WithMany(p => p.CommunityCommunityAttributes)
								.HasForeignKey(d => d.CommunityAttributeId)
								.OnDelete(DeleteBehavior.ClientSetNull)
								.HasConstraintName("fkCommunityCommunityAttribute_CommunityAttribute");

			entity.HasOne(d => d.Community)
								.WithMany(p => p.CommunityCommunityAttributes)
								.HasForeignKey(d => d.CommunityId)
								.OnDelete(DeleteBehavior.ClientSetNull)
								.HasConstraintName("fkCommunityCommunityAttribute_Community");
		});
	}

}