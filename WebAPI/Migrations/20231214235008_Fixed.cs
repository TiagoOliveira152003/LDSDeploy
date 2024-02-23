using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Fixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContaUtilizador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContaUtilizador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngredienteAtribuidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredienteAtribuidos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContaUtilizadorId = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_ContaUtilizador_ContaUtilizadorId",
                        column: x => x.ContaUtilizadorId,
                        principalTable: "ContaUtilizador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContaUtilizadorId = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Texto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_ContaUtilizador_ContaUtilizadorId",
                        column: x => x.ContaUtilizadorId,
                        principalTable: "ContaUtilizador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemCompras",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Preco = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCompras", x => x.Name);
                    table.ForeignKey(
                        name: "FK_ItemCompras_Menus_MenuName",
                        column: x => x.MenuName,
                        principalTable: "Menus",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateTable(
                name: "Ingredientes",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TypeComida = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemName = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredientes", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Ingredientes_ItemCompras_ItemName",
                        column: x => x.ItemName,
                        principalTable: "ItemCompras",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateTable(
                name: "ItemPedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemCompraName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Preco = table.Column<int>(type: "int", nullable: false),
                    PedidoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemPedidos_ItemCompras_ItemCompraName",
                        column: x => x.ItemCompraName,
                        principalTable: "ItemCompras",
                        principalColumn: "Name");
                    table.ForeignKey(
                        name: "FK_ItemPedidos_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EditIngredientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    IngredienteName = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ItemPedidoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditIngredientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EditIngredientes_Ingredientes_IngredienteName",
                        column: x => x.IngredienteName,
                        principalTable: "Ingredientes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EditIngredientes_ItemCompras_ItemName",
                        column: x => x.ItemName,
                        principalTable: "ItemCompras",
                        principalColumn: "Name");
                    table.ForeignKey(
                        name: "FK_EditIngredientes_ItemPedidos_ItemPedidoId",
                        column: x => x.ItemPedidoId,
                        principalTable: "ItemPedidos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EditIngredientes_IngredienteName",
                table: "EditIngredientes",
                column: "IngredienteName");

            migrationBuilder.CreateIndex(
                name: "IX_EditIngredientes_ItemName",
                table: "EditIngredientes",
                column: "ItemName");

            migrationBuilder.CreateIndex(
                name: "IX_EditIngredientes_ItemPedidoId",
                table: "EditIngredientes",
                column: "ItemPedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredientes_ItemName",
                table: "Ingredientes",
                column: "ItemName");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCompras_MenuName",
                table: "ItemCompras",
                column: "MenuName");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedidos_ItemCompraName",
                table: "ItemPedidos",
                column: "ItemCompraName");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedidos_PedidoId",
                table: "ItemPedidos",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ContaUtilizadorId",
                table: "Pedidos",
                column: "ContaUtilizadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_ContaUtilizadorId",
                table: "Ticket",
                column: "ContaUtilizadorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EditIngredientes");

            migrationBuilder.DropTable(
                name: "IngredienteAtribuidos");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Ingredientes");

            migrationBuilder.DropTable(
                name: "ItemPedidos");

            migrationBuilder.DropTable(
                name: "ItemCompras");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "ContaUtilizador");
        }
    }
}
