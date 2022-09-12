namespace SLS.Porfolio.Repository;

internal static partial class CreateModel
{

	internal static void CommunityAttributeType(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<CommunityAttributeType>(entity =>
		{
			entity.ToTable("CommunityAttributeType", "PM");

			entity.HasComment("Represents a type of community attribute used to group community attributes.");

			entity.Property(e => e.CommunityAttributeTypeId).HasComment("Identifier for the community attribute type record.");

			entity.Property(e => e.CommunityAttributeTypeName)
								.HasMaxLength(100)
								.HasComment("Name of the community attribute type.");

			entity.Property(e => e.ExternalId)
								.HasMaxLength(100)
								.HasComment("The tenant's identifier for the community attribute type.");

			entity.Property(e => e.RowStatusId)
								.HasDefaultValueSql("((1))")
								.HasComment("Identifier of the community attribute type record status (i.e. enabled, disabled, etc).");

			entity.Property(e => e.SortOrder).HasComment("Sorting order of the community attribute type.");
		});
	}

}