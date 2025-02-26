using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Selu383.SP25.P02.Api.Features.UserRole;

#nullable disable

namespace Selu383.SP25.P02.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Theaters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeatCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theaters", x => x.Id);
                });


            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1,1"),
                    Username = table.Column<string>(type: "nvar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvar(max)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                }
                );

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new 
                {
                    Id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1,1" ),
                    Name = table.Column<string>(type:"nvar(max)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                }
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Theaters");
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    var userRoleBuilder = builder.Entity<UserRole>();
        //    userRoleBuilder.HasKey(x => new { x.UserId, x.RoleId });
        //    userRoleBuilder.HasOne(x => x.Role).WithMany(x => x.User).HasForeignKey(x => x.RoleId);

        //    userRoleBuilder.HasOne(x => x.User).WithMany(x => x.Role).HasForeignKey(x => x.UserId);
        //}
    }
}
