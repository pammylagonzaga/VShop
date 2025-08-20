﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VShop.ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Products (Name, Price, Description, Stock, ImageURL, CategoryId) " + 
                "Values('Caderno',7.55,'Caderno',10,'caderno1.jpg',1)");

            mb.Sql("Insert into Products (Name, Price, Description, Stock, ImageURL, CategoryId) " +
                       "Values('Lápis',3.45,'Lápis',20,'lapis1.jpg',1)");

            mb.Sql("Insert into Products (Name, Price, Description, Stock, ImageURL, CategoryId) " +
                       "Values('Clips',5.33,'Clips para papel',50,'clips.jpg',2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {

        }
    }
}
