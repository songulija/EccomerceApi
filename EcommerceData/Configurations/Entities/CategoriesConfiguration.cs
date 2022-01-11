using EcommerceData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceData.Configurations.Entities
{
    class CategoriesConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category
                {
                    Id = 1,
                    Title = "Apranga, avalynė aksesuarai",
                    Content = "Kokybiški, stilingi ir madingi aksesuarai, apranga, avalynė moterims, vyrams bei vaikams Pigu.lt el. parduotuvės asortimente siūlomi itin palankiomis sąlygomis. Šios stiliaus detalės leidžia mums susikurti pageidaujamą įvaizdį ir būtent taip išreikšti save. Kokybiška apranga ir avalynė užtikrina komfortą net ir nepatogiose situacijose, o aksesuarai dar labiau pabrėžia mūsų asmenybę. Kuo daugiau skirtingų detalių turėsite, tuo daugiau unikalių derinių pavyks sukurti. Gausi jų įvairovė užtikrins, kad kiekvienam čia pavyks atrasti sau tinkamas įvaizdžio detales. Nesvarbu, ar ieškosite rūbų laisvalaikiui, patogių batų vaikams ar unikalių aksesuarų ypatingai progai, čia visuomet bus iš ko pasirinkti. Įsitikinkite tuo patys – apranga, avalynė ir aksesuarai bet kuriuo metu pasiekiami internetu! "
                },
                new Category
                {
                    Id = 2,
                    Title = "Moterims",
                    ParentId = 1
                },
                new Category
                {
                    Id = 3,
                    Title = "Vyrams",
                    ParentId = 1
                },
                new Category
                {
                    Id = 4,
                    Title = "Vaikams",
                    ParentId = 1
                },
                //for women
                new Category
                {
                    Id = 5,
                    Title = "Drabužiai moterims",
                    ParentId = 2
                },
                new Category
                {
                    Id = 6,
                    Title = "Avalynė moterims",
                    ParentId = 2
                },
                new Category
                {
                    Id = 7,
                    Title = "Apatinis trikotažas",
                    ParentId = 2
                },
                new Category
                {
                    Id = 8,
                    Title = "Vyriški drabužiai",
                    ParentId = 3
                },
                new Category
                {
                    Id = 9,
                    Title = "Avalynė vyrams",
                    ParentId = 3
                },
                new Category
                {
                    Id = 10,
                    Title = "Apatinis trikotažas vyrams",
                    ParentId = 3
                },
                new Category
                {
                    Id = 11,
                    Title = "Striukės moterims",
                    ParentId = 5
                },
                new Category
                {
                    Id = 12,
                    Title = "Suknelės",
                    ParentId = 5
                },
                new Category
                {
                    Id = 13,
                    Title = "Sportinė apranga moterims",
                    ParentId = 5
                }, new Category
                {
                    Id = 14,
                    Title = "Megztiniai moterimis",
                    ParentId = 5
                },
                new Category
                {
                    Id = 15,
                    Title = "Kelnės moterims",
                    ParentId = 5
                },
                 new Category
                 {
                     Id = 16,
                     Title = "Džinsai moterims",
                     ParentId = 5
                 },
                 new Category
                 {
                     Id = 17,
                     Title = "Šlepetės moterims",
                     ParentId = 6
                 },
                 new Category
                 {
                     Id = 18,
                     Title = "Sportiniai bateliai",
                     ParentId = 6
                 },
                 new Category
                 {
                     Id = 19,
                     Title = "Bateliai moterims",
                     ParentId = 6
                 },
                 //vyrams
                 new Category
                 {
                     Id = 20,
                     Title = "Šlepetės moterims",
                     ParentId = 6
                 },
                 new Category
                 {
                     Id = 21,
                     Title = "Vyriškos striukės",
                     ParentId = 8
                 },
                 new Category
                 {
                     Id = 22,
                     Title = "Sportinė apranga vyrams",
                     ParentId = 8
                 },
                 new Category
                 {
                     Id = 23,
                     Title = "Džemperiai vyrams",
                     ParentId = 8
                 },
                 new Category
                 {
                     Id = 24,
                     Title = "Džinsai vyrams",
                     ParentId = 8
                 },
                 new Category
                 {
                     Id = 25,
                     Title = "Vyriški marškinėliai",
                     ParentId = 8
                 },
                 new Category
                 {
                     Id = 26,
                     Title = "Vyriški batai",
                     ParentId = 9
                 },
                 new Category
                 {
                     Id = 27,
                     Title = "Kedai vyrams",
                     ParentId = 9
                 },
                 new Category
                 {
                     Id = 28,
                     Title = "Šlepetės vyrams",
                     ParentId = 9
                 },
                 //vaikams
                 new Category
                 {
                     Id = 29,
                     Title = "Drabužiai mergaitėms",
                     ParentId = 4
                 },
                 new Category
                 {
                     Id = 30,
                     Title = "Drabužiai berniukams",
                     ParentId = 4
                 },
                 new Category
                 {
                     Id = 31,
                     Title = "Megztiniai, blizonai, švarkai mergaitėms",
                     ParentId = 29
                 },
                 new Category
                 {
                     Id = 32,
                     Title = "Marškinėliai mergaitėms",
                     ParentId = 29
                 },
                 new Category
                 {
                     Id = 33,
                     Title = "Kelnės mergaitėms",
                     ParentId = 29
                 },
                 new Category
                 {
                     Id = 34,
                     Title = "Suknelės mergaitėms",
                     ParentId = 29
                 },
                 new Category
                 {
                     Id = 35,
                     Title = "Megztiniai, blizonai, švarkai berniukams",
                     ParentId = 30
                 },
                 new Category
                 {
                     Id = 36,
                     Title = "Kelnės berniukams",
                     ParentId = 30
                 },
                 new Category
                 {
                     Id = 37,
                     Title = "Marškinėliai berniukams",
                     ParentId = 30
                 },
                 new Category
                 {
                     Id = 38,
                     Title = "Striukės berniukams",
                     ParentId = 30
                 },
                 new Category
                 {
                     Id = 39,
                     Title = "Baldai ir namų interjeras"
                 },
                 new Category
                 {
                     Id = 40,
                     Title = "Sportas, laisvalaikis, turizmas"
                 },
                 new Category
                 {
                     Id = 41,
                     Title = "Kompiuterinė technika"
                 },
                 new Category
                 {
                     Id = 42,
                     Title = "Svetainės baldai",
                     ParentId = 39
                 },
                 new Category
                 {
                     Id = 43,
                     Title = "Lauko baldai",
                     ParentId = 39
                 },
                 new Category
                 {
                     Id = 44,
                     Title = "Miegamojo baldai",
                     ParentId = 39
                 },
                 new Category
                 {
                     Id = 45,
                     Title = "Biuro baldai",
                     ParentId = 39
                 },
                 new Category
                 {
                     Id = 46,
                     Title = "Virtuvės baldai",
                     ParentId = 39
                 },
                 new Category
                 {
                     Id = 47,
                     Title = "Vaiko kambario baldai",
                     ParentId = 39
                 },
                 new Category
                 {
                     Id = 48,
                     Title = "Vonios kambario baldai",
                     ParentId = 39
                 },
                 new Category
                 {
                     Id = 49,
                     Title = "Kilimai, kilimėliai",
                     ParentId = 39
                 },
                 new Category
                 {
                     Id = 50,
                     Title = "Veidrodžiai",
                     ParentId = 39
                 },
                 new Category
                 {
                     Id = 51,
                     Title = "Treniruokliai, treniruočių įranga",
                     ParentId = 40
                 },
                 new Category
                 {
                     Id = 52,
                     Title = "Sporto prekės",
                     ParentId = 40
                 },
                 new Category
                 {
                     Id = 53,
                     Title = "Laisvalaikis",
                     ParentId = 40
                 },
                 new Category
                 {
                     Id = 54,
                     Title = "Dviračiai, paspirtukai, riedučiai, riedlentės",
                     ParentId = 40
                 },
                 new Category
                 {
                     Id = 55,
                     Title = "Turizmas",
                     ParentId = 40
                 },
                 new Category
                 {
                     Id = 56,
                     Title = "Žiemos sportas",
                     ParentId = 40
                 },
                 new Category
                 {
                     Id = 57,
                     Title = "Maisto papildai, preparatai, funkcinis maistas",
                     ParentId = 40
                 },
                 new Category
                 {
                     Id = 58,
                     Title = "Išoriniai kompiuterių aksesuarai",
                     ParentId = 41
                 },
                 new Category
                 {
                     Id = 59,
                     Title = "Nešiojami kompiuteriai, priedai",
                     ParentId = 41
                 },
                 new Category
                 {
                     Id = 60,
                     Title = "Planšetiniai kompiuteriai, el. skaityklės",
                     ParentId = 41
                 },
                 new Category
                 {
                     Id = 61,
                     Title = "Žaidimų kompiuteriai, priedai",
                     ParentId = 41
                 },
                 new Category
                 {
                     Id = 62,
                     Title = "Orgtechnika, priedai",
                     ParentId = 41
                 },
                 new Category
                 {
                     Id = 63,
                     Title = "Monitoriai kompiuteriams ir laikikliai",
                     ParentId = 41
                 },
                 new Category
                 {
                     Id = 64,
                     Title = "Duomenų laikmenos",
                     ParentId = 41
                 }
            );
        }
    }
}
