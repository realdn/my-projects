namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMovieGenre : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO MovieGenres (Id,Genre) Values(1,'Comedy')");
            Sql("INSERT INTO MovieGenres (Id,Genre) Values(2,'Action')");
            Sql("INSERT INTO MovieGenres (Id,Genre) Values(3,'Family')");
            Sql("INSERT INTO MovieGenres (Id,Genre) Values(4,'Romance')");
            

        }
        
        public override void Down()
        {
        }
    }
}
