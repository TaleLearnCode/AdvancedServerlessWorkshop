namespace SLS.Porfolio.Repository;

internal static partial class CreateModel
{

	internal static void CommunityAttribute(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<CommunityAttribute>(entity =>
		{
			entity.ToTable("CommunityAttribute", "PM");

			entity.HasComment("Represents an attribute to be associated to a community.");

			entity.Property(e => e.CommunityAttributeId).HasComment("Identifier for the community attribute record.");

			entity.Property(e => e.CommunityAttributeName).HasMaxLength(100);

			entity.Property(e => e.CommunityAttributeTypeId).HasComment("Identifier of the type of community attribute is being represented by the record.");

			entity.Property(e => e.ExternalId)
				.HasMaxLength(100)
				.HasComment("The tenant's identifier for the community attribute.");

			entity.Property(e => e.IconId).HasComment("Identifier of the DigitalAsset record representing the icon for the Community Attribute.");

			entity.Property(e => e.LabelId).HasComment("Identifier of the Content record representing the label for the Community Attribute.");

			entity.Property(e => e.RowStatusId)
				.HasDefaultValueSql("((1))")
				.HasComment("Identifier of the community attribute type record status (i.e. enabled, disabled, etc).");

			entity.HasOne(d => d.CommunityAttributeType)
				.WithMany(p => p.CommunityAttributes)
				.HasForeignKey(d => d.CommunityAttributeTypeId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("fkCommunityAttribute_CommunityAttributeType");

			entity.HasOne(d => d.Icon)
				.WithMany(p => p.CommunityAttributes)
				.HasForeignKey(d => d.IconId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("fkCommunityAttribute_DigitalAsset");

			entity.HasOne(d => d.Label)
				.WithMany(p => p.CommunityAttributes)
				.HasForeignKey(d => d.LabelId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("fkCommunityAttribute_Content");

		});
	}

}