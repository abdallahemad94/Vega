using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vega.Migrations
{
    public partial class VegaDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Makes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Makes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    MakeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Models_Makes_MakeId",
                        column: x => x.MakeId,
                        principalTable: "Makes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModelId = table.Column<int>(nullable: false),
                    IsRegistered = table.Column<bool>(nullable: false),
                    ContactName = table.Column<string>(maxLength: 255, nullable: false),
                    ContactPhone = table.Column<string>(maxLength: 255, nullable: false),
                    ContactEmail = table.Column<string>(maxLength: 255, nullable: true),
                    LastUpdated = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehiclesFeatures",
                columns: table => new
                {
                    VehicleId = table.Column<int>(nullable: false),
                    FeatureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiclesFeatures", x => new { x.VehicleId, x.FeatureId });
                    table.ForeignKey(
                        name: "FK_VehiclesFeatures_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehiclesFeatures_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Models_MakeId",
                table: "Models",
                column: "MakeId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_ModelId",
                table: "Vehicles",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_VehiclesFeatures_FeatureId",
                table: "VehiclesFeatures",
                column: "FeatureId");

            //******************######### insert data to Makes table ############****************************
            migrationBuilder.Sql("INSERT INTO [Makes](Name) VALUES('Make')");
            migrationBuilder.Sql("INSERT INTO [Makes](Name) VALUES('NewMake')");
            migrationBuilder.Sql("INSERT INTO [Makes](Name) VALUES('OldMake')");
            migrationBuilder.Sql("INSERT INTO [Makes](Name) VALUES('GoodMake')");
            migrationBuilder.Sql("INSERT INTO [Makes](Name) VALUES('BadMake')");
            migrationBuilder.Sql("INSERT INTO [Makes](Name) VALUES('CommonMake')");

            //******************#########  insert data to Models table ############****************************
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('Model', (SELECT [Id] FROM [Makes] WHERE [Name] = 'Make'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('NewModel', (SELECT [Id] FROM [Makes] WHERE [Name] = 'Make'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('OldModel', (SELECT [Id] FROM [Makes] WHERE [Name] = 'Make'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('GoodModel', (SELECT [Id] FROM [Makes] WHERE [Name] = 'Make'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('BadModel', (SELECT [Id] FROM [Makes] WHERE [Name] = 'Make'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('CommonModel', (SELECT [Id] FROM [Makes] WHERE [Name] = 'Make'))");

            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('Model1', (SELECT [Id] FROM [Makes] WHERE [Name] = 'NewMake'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('NewModel1', (SELECT [Id] FROM [Makes] WHERE [Name] = 'NewMake'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('OldModel1', (SELECT [Id] FROM [Makes] WHERE [Name] = 'NewMake'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('GoodModel1', (SELECT [Id] FROM [Makes] WHERE [Name] = 'NewMake'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('BadModel1', (SELECT [Id] FROM [Makes] WHERE [Name] = 'NewMake'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('CommonModel1', (SELECT [Id] FROM [Makes] WHERE [Name] = 'NewMake'))");

            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('Model2', (SELECT [Id] FROM [Makes] WHERE [Name] = 'OldMake'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('NewModel2', (SELECT [Id] FROM [Makes] WHERE [Name] = 'OldMake'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('OldModel2', (SELECT [Id] FROM [Makes] WHERE [Name] = 'OldMake'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('GoodModel2', (SELECT [Id] FROM [Makes] WHERE [Name] = 'OldMake'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('BadModel2', (SELECT [Id] FROM [Makes] WHERE [Name] = 'OldMake'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('CommonModel2', (SELECT [Id] FROM [Makes] WHERE [Name] = 'OldMake'))");

            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('Model3', (SELECT [Id] FROM [Makes] WHERE [Name] = 'GoodMake'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('NewModel3', (SELECT [Id] FROM [Makes] WHERE [Name] = 'GoodMake'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('OldModel3', (SELECT [Id] FROM [Makes] WHERE [Name] = 'GoodMake'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('GoodModel3', (SELECT [Id] FROM [Makes] WHERE [Name] = 'GoodMake'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('BadModel3', (SELECT [Id] FROM [Makes] WHERE [Name] = 'GoodMake'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('CommonModel3', (SELECT [Id] FROM [Makes] WHERE [Name] = 'GoodMake'))");

            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('Model4', (SELECT [Id] FROM [Makes] WHERE [Name] = 'BadMake'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('NewModel4', (SELECT [Id] FROM [Makes] WHERE [Name] = 'BadMake'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('OldModel4', (SELECT [Id] FROM [Makes] WHERE [Name] = 'BadMake'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('GoodModel4', (SELECT [Id] FROM [Makes] WHERE [Name] = 'BadMake'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('BadModel4', (SELECT [Id] FROM [Makes] WHERE [Name] = 'BadMake'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('CommonModel4', (SELECT [Id] FROM [Makes] WHERE [Name] = 'BadMake'))");

            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('Model5', (SELECT [Id] FROM [Makes] WHERE [Name] = 'CommonMake'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('NewModel5', (SELECT [Id] FROM [Makes] WHERE [Name] = 'CommonMake'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('OldModel5', (SELECT [Id] FROM [Makes] WHERE [Name] = 'CommonMake'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('GoodModel5', (SELECT [Id] FROM [Makes] WHERE [Name] = 'CommonMake'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('BadModel5', (SELECT [Id] FROM [Makes] WHERE [Name] = 'CommonMake'))");
            migrationBuilder.Sql("INSERT INTO [Models](Name, MakeId) VALUES('CommonModel5', (SELECT [Id] FROM [Makes] WHERE [Name] = 'CommonMake'))");

            //******************#########  insert data to Features table ############****************************
            migrationBuilder.Sql("INSERT INTO [Features](Name) VALUES('Feature')");
            migrationBuilder.Sql("INSERT INTO [Features](Name) VALUES('NewFeature')");
            migrationBuilder.Sql("INSERT INTO [Features](Name) VALUES('OldFeature')");
            migrationBuilder.Sql("INSERT INTO [Features](Name) VALUES('GoodFeature')");
            migrationBuilder.Sql("INSERT INTO [Features](Name) VALUES('BadFeature')");
            migrationBuilder.Sql("INSERT INTO [Features](Name) VALUES('CommonFeature')");

            //******************#########  insert data to Vehicles table ############****************************
            migrationBuilder.Sql("INSERT INTO [Vehicles](ModelId, IsRegistered, ContactName, ContactPhone, ContactEmail, LastUpdated) VALUES((SELECT [Id] FROM [Models] WHERE [Name] = 'Model'), 1, 'John Smith', '012345', 'JohnSmith@test.Com', GETDATE())");
            migrationBuilder.Sql("INSERT INTO [Vehicles](ModelId, IsRegistered, ContactName, ContactPhone, ContactEmail, LastUpdated) VALUES((SELECT [Id] FROM [Models] WHERE [Name] = 'NewModel'), 1, 'Julia Smith', '123456', 'JuliaSmith@test.Com', GETDATE())");
            migrationBuilder.Sql("INSERT INTO [Vehicles](ModelId, IsRegistered, ContactName, ContactPhone, ContactEmail, LastUpdated) VALUES((SELECT [Id] FROM [Models] WHERE [Name] = 'OldModel'), 1, 'Mick Smith', '234567', 'MickSmith@test.Com', GETDATE())");
            migrationBuilder.Sql("INSERT INTO [Vehicles](ModelId, IsRegistered, ContactName, ContactPhone, ContactEmail, LastUpdated) VALUES((SELECT [Id] FROM [Models] WHERE [Name] = 'GoodModel'), 1, 'Jodi Smith', '345678', 'JodiSmith@test.Com', GETDATE())");
            migrationBuilder.Sql("INSERT INTO [Vehicles](ModelId, IsRegistered, ContactName, ContactPhone, ContactEmail, LastUpdated) VALUES((SELECT [Id] FROM [Models] WHERE [Name] = 'BadModel'), 1, 'Riley Smith', '456789', 'RileySmith@test.Com', GETDATE())");
            migrationBuilder.Sql("INSERT INTO [Vehicles](ModelId, IsRegistered, ContactName, ContactPhone, ContactEmail, LastUpdated) VALUES((SELECT [Id] FROM [Models] WHERE [Name] = 'CommonModel'), 1, 'Smith', '567890', 'Smith@test.Com', GETDATE())");

            migrationBuilder.Sql("INSERT INTO [Vehicles](ModelId, IsRegistered, ContactName, ContactPhone, ContactEmail, LastUpdated) VALUES((SELECT [Id] FROM [Models] WHERE [Name] = 'Model'), 1, 'John Smith1', '012345', 'JohnSmith@test.Com', GETDATE())");
            migrationBuilder.Sql("INSERT INTO [Vehicles](ModelId, IsRegistered, ContactName, ContactPhone, ContactEmail, LastUpdated) VALUES((SELECT [Id] FROM [Models] WHERE [Name] = 'NewModel'), 1, 'Julia Smith1', '123456', 'JuliaSmith@test.Com', GETDATE())");
            migrationBuilder.Sql("INSERT INTO [Vehicles](ModelId, IsRegistered, ContactName, ContactPhone, ContactEmail, LastUpdated) VALUES((SELECT [Id] FROM [Models] WHERE [Name] = 'OldModel'), 1, 'Mick Smith1', '234567', 'MickSmith@test.Com', GETDATE())");
            migrationBuilder.Sql("INSERT INTO [Vehicles](ModelId, IsRegistered, ContactName, ContactPhone, ContactEmail, LastUpdated) VALUES((SELECT [Id] FROM [Models] WHERE [Name] = 'GoodModel'), 1, 'Jodi Smith1', '345678', 'JodiSmith@test.Com', GETDATE())");
            migrationBuilder.Sql("INSERT INTO [Vehicles](ModelId, IsRegistered, ContactName, ContactPhone, ContactEmail, LastUpdated) VALUES((SELECT [Id] FROM [Models] WHERE [Name] = 'BadModel'), 1, 'Riley Smith1', '456789', 'RileySmith@test.Com', GETDATE())");
            migrationBuilder.Sql("INSERT INTO [Vehicles](ModelId, IsRegistered, ContactName, ContactPhone, ContactEmail, LastUpdated) VALUES((SELECT [Id] FROM [Models] WHERE [Name] = 'CommonModel'), 1, 'Smith1', '567890', 'Smith@test.Com', GETDATE())");

            migrationBuilder.Sql("INSERT INTO [Vehicles](ModelId, IsRegistered, ContactName, ContactPhone, ContactEmail, LastUpdated) VALUES((SELECT [Id] FROM [Models] WHERE [Name] = 'Model'), 1, 'John Smith2', '012345', 'JohnSmith@test.Com', GETDATE())");
            migrationBuilder.Sql("INSERT INTO [Vehicles](ModelId, IsRegistered, ContactName, ContactPhone, ContactEmail, LastUpdated) VALUES((SELECT [Id] FROM [Models] WHERE [Name] = 'NewModel'), 1, 'Julia Smith2', '123456', 'JuliaSmith@test.Com', GETDATE())");
            migrationBuilder.Sql("INSERT INTO [Vehicles](ModelId, IsRegistered, ContactName, ContactPhone, ContactEmail, LastUpdated) VALUES((SELECT [Id] FROM [Models] WHERE [Name] = 'OldModel'), 1, 'Mick Smith2', '234567', 'MickSmith@test.Com', GETDATE())");
            migrationBuilder.Sql("INSERT INTO [Vehicles](ModelId, IsRegistered, ContactName, ContactPhone, ContactEmail, LastUpdated) VALUES((SELECT [Id] FROM [Models] WHERE [Name] = 'GoodModel'), 1, 'Jodi Smith2', '345678', 'JodiSmith@test.Com', GETDATE())");
            migrationBuilder.Sql("INSERT INTO [Vehicles](ModelId, IsRegistered, ContactName, ContactPhone, ContactEmail, LastUpdated) VALUES((SELECT [Id] FROM [Models] WHERE [Name] = 'BadModel'), 1, 'Riley Smith2', '456789', 'RileySmith@test.Com', GETDATE())");
            migrationBuilder.Sql("INSERT INTO [Vehicles](ModelId, IsRegistered, ContactName, ContactPhone, ContactEmail, LastUpdated) VALUES((SELECT [Id] FROM [Models] WHERE [Name] = 'CommonModel'), 1, 'Smith2', '567890', 'Smith@test.Com', GETDATE())");

            //******************#########  insert data to VehiclesFeatures table ############****************************
            migrationBuilder.Sql("INSERT INTO [VehiclesFeatures](VehicleId, FeatureId) VALUES((SELECT [Id] FROM [Vehicles] WHERE [ContactName] = 'John Smith'), (SELECT [Id] FROM [Features] WHERE [Name] = 'Feature'))");
            migrationBuilder.Sql("INSERT INTO [VehiclesFeatures](VehicleId, FeatureId) VALUES((SELECT [Id] FROM [Vehicles] WHERE [ContactName] = 'John Smith'), (SELECT [Id] FROM [Features] WHERE [Name] = 'NewFeature'))");
            migrationBuilder.Sql("INSERT INTO [VehiclesFeatures](VehicleId, FeatureId) VALUES((SELECT [Id] FROM [Vehicles] WHERE [ContactName] = 'John Smith'), (SELECT [Id] FROM [Features] WHERE [Name] = 'BadFeature'))");
            migrationBuilder.Sql("INSERT INTO [VehiclesFeatures](VehicleId, FeatureId) VALUES((SELECT [Id] FROM [Vehicles] WHERE [ContactName] = 'Mick Smith'), (SELECT [Id] FROM [Features] WHERE [Name] = 'NewFeature'))");
            migrationBuilder.Sql("INSERT INTO [VehiclesFeatures](VehicleId, FeatureId) VALUES((SELECT [Id] FROM [Vehicles] WHERE [ContactName] = 'John Smith'), (SELECT [Id] FROM [Features] WHERE [Name] = 'CommonFeature'))");
            migrationBuilder.Sql("INSERT INTO [VehiclesFeatures](VehicleId, FeatureId) VALUES((SELECT [Id] FROM [Vehicles] WHERE [ContactName] = 'Jodi Smith'), (SELECT [Id] FROM [Features] WHERE [Name] = 'Feature'))");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehiclesFeatures");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "Makes");
        }
    }
}
