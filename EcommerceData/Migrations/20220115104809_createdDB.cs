using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EcommerceData.Migrations
{
    public partial class createdDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: true),
                    OtherBrand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    CostPrice = table.Column<double>(type: "float", nullable: true),
                    IsDiscount = table.Column<bool>(type: "bit", nullable: false),
                    DiscountPrice = table.Column<double>(type: "float", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LengthWithoutPackaging = table.Column<double>(type: "float", nullable: true),
                    WidthWithoutPackaging = table.Column<double>(type: "float", nullable: true),
                    HeightWithoutPackaging = table.Column<double>(type: "float", nullable: true),
                    LengthWithPackaging = table.Column<double>(type: "float", nullable: true),
                    WidthWithPackaging = table.Column<double>(type: "float", nullable: true),
                    HeightWithPackaging = table.Column<double>(type: "float", nullable: true),
                    WeightGross = table.Column<int>(type: "int", nullable: true),
                    WeightNetto = table.Column<int>(type: "int", nullable: true),
                    PackagingBoxCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "UserTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductReviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTags_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Tax = table.Column<double>(type: "float", nullable: false),
                    ShippingPrice = table.Column<double>(type: "float", nullable: false),
                    SubTotal = table.Column<double>(type: "float", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelivered = table.Column<bool>(type: "bit", nullable: false),
                    DeliveredAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shipments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "MetaTitle", "Title" },
                values: new object[,]
                {
                    { 1, null, "Adidas" },
                    { 2, null, "Armani" },
                    { 3, null, "Asics" },
                    { 4, null, "Cabba" },
                    { 5, null, "Calvin Klein" },
                    { 6, null, "Columbia" },
                    { 7, null, "Diesel" },
                    { 8, null, "H&M" },
                    { 9, null, "Zara" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Content", "MetaTitle", "ParentId", "Slug", "Title" },
                values: new object[,]
                {
                    { 47, null, null, 39, null, "Vaiko kambario baldai" },
                    { 46, null, null, 39, null, "Virtuvės baldai" },
                    { 45, null, null, 39, null, "Biuro baldai" },
                    { 44, null, null, 39, null, "Miegamojo baldai" },
                    { 43, null, null, 39, null, "Lauko baldai" },
                    { 42, null, null, 39, null, "Svetainės baldai" },
                    { 37, null, null, 30, null, "Marškinėliai berniukams" },
                    { 40, null, null, 0, null, "Sportas, laisvalaikis, turizmas" },
                    { 39, null, null, 0, null, "Baldai ir namų interjeras" },
                    { 38, null, null, 30, null, "Striukės berniukams" },
                    { 48, null, null, 39, null, "Vonios kambario baldai" },
                    { 36, null, null, 30, null, "Kelnės berniukams" },
                    { 35, null, null, 30, null, "Megztiniai, blizonai, švarkai berniukams" },
                    { 41, null, null, 0, null, "Kompiuterinė technika" },
                    { 49, null, null, 39, null, "Kilimai, kilimėliai" },
                    { 53, null, null, 40, null, "Laisvalaikis" },
                    { 51, null, null, 40, null, "Treniruokliai, treniruočių įranga" },
                    { 52, null, null, 40, null, "Sporto prekės" },
                    { 34, null, null, 29, null, "Suknelės mergaitėms" },
                    { 54, null, null, 40, null, "Dviračiai, paspirtukai, riedučiai, riedlentės" },
                    { 55, null, null, 40, null, "Turizmas" },
                    { 56, null, null, 40, null, "Žiemos sportas" },
                    { 57, null, null, 40, null, "Maisto papildai, preparatai, funkcinis maistas" },
                    { 58, null, null, 41, null, "Išoriniai kompiuterių aksesuarai" },
                    { 59, null, null, 41, null, "Nešiojami kompiuteriai, priedai" },
                    { 60, null, null, 41, null, "Planšetiniai kompiuteriai, el. skaityklės" },
                    { 61, null, null, 41, null, "Žaidimų kompiuteriai, priedai" },
                    { 62, null, null, 41, null, "Orgtechnika, priedai" },
                    { 63, null, null, 41, null, "Monitoriai kompiuteriams ir laikikliai" },
                    { 64, null, null, 41, null, "Duomenų laikmenos" },
                    { 50, null, null, 39, null, "Veidrodžiai" },
                    { 33, null, null, 29, null, "Kelnės mergaitėms" },
                    { 29, null, null, 4, null, "Drabužiai mergaitėms" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Content", "MetaTitle", "ParentId", "Slug", "Title" },
                values: new object[,]
                {
                    { 31, null, null, 29, null, "Megztiniai, blizonai, švarkai mergaitėms" },
                    { 1, "Kokybiški, stilingi ir madingi aksesuarai, apranga, avalynė moterims, vyrams bei vaikams Pigu.lt el. parduotuvės asortimente siūlomi itin palankiomis sąlygomis. Šios stiliaus detalės leidžia mums susikurti pageidaujamą įvaizdį ir būtent taip išreikšti save. Kokybiška apranga ir avalynė užtikrina komfortą net ir nepatogiose situacijose, o aksesuarai dar labiau pabrėžia mūsų asmenybę. Kuo daugiau skirtingų detalių turėsite, tuo daugiau unikalių derinių pavyks sukurti. Gausi jų įvairovė užtikrins, kad kiekvienam čia pavyks atrasti sau tinkamas įvaizdžio detales. Nesvarbu, ar ieškosite rūbų laisvalaikiui, patogių batų vaikams ar unikalių aksesuarų ypatingai progai, čia visuomet bus iš ko pasirinkti. Įsitikinkite tuo patys – apranga, avalynė ir aksesuarai bet kuriuo metu pasiekiami internetu! ", null, 0, null, "Apranga, avalynė aksesuarai" },
                    { 2, null, null, 1, null, "Moterims" },
                    { 3, null, null, 1, null, "Vyrams" },
                    { 4, null, null, 1, null, "Vaikams" },
                    { 5, null, null, 2, null, "Drabužiai moterims" },
                    { 6, null, null, 2, null, "Avalynė moterims" },
                    { 7, null, null, 2, null, "Apatinis trikotažas" },
                    { 8, null, null, 3, null, "Vyriški drabužiai" },
                    { 9, null, null, 3, null, "Avalynė vyrams" },
                    { 10, null, null, 3, null, "Apatinis trikotažas vyrams" },
                    { 11, null, null, 5, null, "Striukės moterims" },
                    { 12, null, null, 5, null, "Suknelės" },
                    { 13, null, null, 5, null, "Sportinė apranga moterims" },
                    { 14, null, null, 5, null, "Megztiniai moterimis" },
                    { 32, null, null, 29, null, "Marškinėliai mergaitėms" },
                    { 15, null, null, 5, null, "Kelnės moterims" },
                    { 17, null, null, 6, null, "Šlepetės moterims" },
                    { 18, null, null, 6, null, "Sportiniai bateliai" },
                    { 19, null, null, 6, null, "Bateliai moterims" },
                    { 20, null, null, 6, null, "Šlepetės moterims" },
                    { 21, null, null, 8, null, "Vyriškos striukės" },
                    { 22, null, null, 8, null, "Sportinė apranga vyrams" },
                    { 23, null, null, 8, null, "Džemperiai vyrams" },
                    { 24, null, null, 8, null, "Džinsai vyrams" },
                    { 25, null, null, 8, null, "Vyriški marškinėliai" },
                    { 26, null, null, 9, null, "Vyriški batai" },
                    { 27, null, null, 9, null, "Kedai vyrams" },
                    { 28, null, null, 9, null, "Šlepetės vyrams" },
                    { 30, null, null, 4, null, "Drabužiai berniukams" },
                    { 16, null, null, 5, null, "Džinsai moterims" }
                });

            migrationBuilder.InsertData(
                table: "UserTypes",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "ADMINISTRATOR" },
                    { 2, "USER" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "PhoneNumber", "TypeId", "Username" },
                values: new object[] { 1, "Lukas", "Songulija", "$2a$11$bJmRUsTpO1BcBXPsBquLRe/PMS5HUY/ZqOIjC263csyGgQxjzJ212", "860855183", 1, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProductId",
                table: "Comments",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_CategoryId",
                table: "ProductCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ProductId",
                table: "ProductCategories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_ProductId",
                table: "ProductReviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_ProductId",
                table: "ProductTags",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_TagId",
                table: "ProductTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_OrderId",
                table: "Shipments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TypeId",
                table: "Users",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "ProductReviews");

            migrationBuilder.DropTable(
                name: "ProductTags");

            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserTypes");
        }
    }
}
