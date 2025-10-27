using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionObrasArte.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artistas",
                columns: table => new
                {
                    IdArtista = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NombreArtista = table.Column<string>(type: "TEXT", nullable: false),
                    ApellidosArtista = table.Column<string>(type: "TEXT", nullable: false),
                    AñoNacimientoArtista = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artistas", x => x.IdArtista);
                });

            migrationBuilder.CreateTable(
                name: "TiposPintura",
                columns: table => new
                {
                    IdTipoPintura = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TítuloTipoPintura = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposPintura", x => x.IdTipoPintura);
                });

            migrationBuilder.CreateTable(
                name: "Pinturas",
                columns: table => new
                {
                    IdPintura = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TituloPintura = table.Column<string>(type: "TEXT", nullable: false),
                    Fk_IdArtista = table.Column<int>(type: "INTEGER", nullable: false),
                    FK_IdTipoPintura = table.Column<int>(type: "INTEGER", nullable: false),
                    Precio = table.Column<decimal>(type: "DECIMAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pinturas", x => x.IdPintura);
                    table.ForeignKey(
                        name: "FK_Pinturas_Artistas_Fk_IdArtista",
                        column: x => x.Fk_IdArtista,
                        principalTable: "Artistas",
                        principalColumn: "IdArtista",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pinturas_TiposPintura_FK_IdTipoPintura",
                        column: x => x.FK_IdTipoPintura,
                        principalTable: "TiposPintura",
                        principalColumn: "IdTipoPintura",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pinturas_Fk_IdArtista",
                table: "Pinturas",
                column: "Fk_IdArtista");

            migrationBuilder.CreateIndex(
                name: "IX_Pinturas_FK_IdTipoPintura",
                table: "Pinturas",
                column: "FK_IdTipoPintura");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pinturas");

            migrationBuilder.DropTable(
                name: "Artistas");

            migrationBuilder.DropTable(
                name: "TiposPintura");
        }
    }
}
