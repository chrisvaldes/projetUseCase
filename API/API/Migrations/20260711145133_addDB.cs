using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class addDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bkmvtis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeMag = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodeAgence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sequence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeDevise = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstActif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroCompte = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeBeneficiaire = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceBeneficiaire = table.Column<int>(type: "int", nullable: false),
                    CleBeneficiaire = table.Column<int>(type: "int", nullable: false),
                    DatePrelevement = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PrixUnitCarte = table.Column<long>(type: "bigint", nullable: true),
                    ReferenceOperation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeOperation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeEmetteur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndicateurDomiciliation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LibelleCarte = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesignationCarte = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Carte = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateValiditeCarte = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateCreationCarte = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CodeTarif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Basculer = table.Column<bool>(type: "bit", nullable: false),
                    NomClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeCarte = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartPeriod = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    EndPeriod = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bkmvtis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComptesDebiteRedevCartes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ncp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mon = table.Column<long>(type: "bigint", nullable: false),
                    Dco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lib = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComptesDebiteRedevCartes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComptesOuvert",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ncp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cfe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Clc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Inti = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComptesOuvert", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeMags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeMags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isAlreadyDownload = table.Column<bool>(type: "bit", nullable: false),
                    PeriodeDebut = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PeriodeFin = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeMags", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bkmvtis");

            migrationBuilder.DropTable(
                name: "ComptesDebiteRedevCartes");

            migrationBuilder.DropTable(
                name: "ComptesOuvert");

            migrationBuilder.DropTable(
                name: "TypeMags");
        }
    }
}
