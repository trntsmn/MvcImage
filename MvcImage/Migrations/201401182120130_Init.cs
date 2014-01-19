namespace MvcImage.Migrations
{
	using System;
	using System.Data.Entity.Migrations;

	public partial class Init : DbMigration
	{
		public override void Up()
		{
			CreateTable(
				"dbo.Images",
				c => new
					{
						Id = c.Int(nullable: false, identity: true),
						filename = c.String(),
						type = c.String(),
						size = c.String(),
					})
				.PrimaryKey(t => t.Id);

		}

		public override void Down()
		{
			DropTable("dbo.Images");
		}
	}
}
