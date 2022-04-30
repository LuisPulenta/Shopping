using Shopping.Helpers;
using Shopping.Data;
using Shopping.Data.Entities;
using Shopping.Enums;
using Microsoft.EntityFrameworkCore;

namespace Shopping.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCategoriesAsync();
            await CheckCountriesAsync();
            await CheckProductsAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Luis", "Núñez", "luis@yopmail.com", "351 681 4963", "Espora 2052","luis.jpg", UserType.Admin);
            await CheckUserAsync("2020", "Pablo", "Lacuadri", "pablo@yopmail.com", "351 555 412234", "Villa Santa Ana", "pablo.jpg", UserType.Admin);
            await CheckUserAsync("3030", "Diego", "Maradona", "maradona@yopmail.com", "311 322 4620", "Villa Fiorito", "maradona.jpg", UserType.User);
            await CheckUserAsync("4040", "Lionel", "Messi", "messi@yopmail.com", "311 322 4620", "París", "messi.jpg", UserType.User);
            await CheckUserAsync("5050", "Gabriel", "Batistuta", "batistuta@yopmail.com", "311 322 4620", "Rosario", "batistuta.jpg", UserType.User);
            await CheckUserAsync("6060", "Roger", "Federer", "federer@yopmail.com", "311 322 4620", "Zurich", "federer.jpg", UserType.User);
            await CheckUserAsync("7070", "Mario", "Kempes", "kempes@yopmail.com", "311 322 4620", "Bell VIlle", "kempes.jpg", UserType.User);
            await CheckUserAsync("8080", "Lucas", "Martinez", "lucas@yopmail.com", "311 322 4620", "Córdoba", "lucas.png", UserType.User);
            await CheckUserAsync("9090", "Marina", "Martinez", "marina@yopmail.com", "311 322 4620", "Valencia", "marina.png", UserType.User);
            await CheckUserAsync("9999", "Rafael", "Nadal", "nadal@yopmail.com", "311 322 4620", "Madrid", "nadal.jpg", UserType.User);

        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task CheckUserAsync(string document, string firstName, string lastName, string email, string phoneNumber, string address, string image, UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    Address = address,
                    Document = document,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                    UserName = email,
                    UserType = userType,
                    City=_context.Cities.FirstOrDefault(),
                    ImageId= $"~/images/users/{image}"
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);

            }
        }
        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Belleza" });
                _context.Categories.Add(new Category { Name = "Calzado" });
                _context.Categories.Add(new Category { Name = "Bebidas" });
                _context.Categories.Add(new Category { Name = "Carnicería" });
                _context.Categories.Add(new Category { Name = "Electrónica" });
                _context.Categories.Add(new Category { Name = "Indumentaria" });
                _context.Categories.Add(new Category { Name = "Juguetería" });
                _context.Categories.Add(new Category { Name = "Lácteos  " });
                _context.Categories.Add(new Category { Name = "Librería" });
                _context.Categories.Add(new Category { Name = "Limpieza" });
                _context.Categories.Add(new Category { Name = "Nutrición" });
                _context.Categories.Add(new Category { Name = "Panadería" });
                _context.Categories.Add(new Category { Name = "Pescadería" });
                _context.Categories.Add(new Category { Name = "Quesos y Fiambres" });
                _context.Categories.Add(new Category { Name = "Tecnología" });
                _context.Categories.Add(new Category { Name = "Verdulería" });
                _context.Categories.Add(new Category { Name = "Deportes" });
                _context.Categories.Add(new Category { Name = "Apple" });
                _context.Categories.Add(new Category { Name = "Mascotas" });
                _context.Categories.Add(new Category { Name = "Gamer" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country {
                    Name = "Argentina",
                    States = new List<State>()
                    {
                        new State()
                        {
                            Name="Córdoba",
                            Cities=new List<City>
                            {
                                new City (){Name ="Córdoba"},
                                new City(){Name ="Río Cuarto"},
                                new City (){Name ="Villa María"},
                                new City (){Name ="San Francisco"},
                                new City () {Name ="Carlos Paz"},}
                        },
                        new State()
                        {
                            Name="Buenos Aires",
                            Cities=new List<City>
                            {new City(){Name ="La Plata"},
                                new City(){Name ="Mar del Plata"},
                                new City(){Name ="Tandil"},
                                new City(){Name ="Bahía Blanca"},
                            },
                        },
                        new State()
                        {
                            Name="Santa Fe",
                            Cities=new List<City>
                            {
                                new City(){Name ="Santa Fe"},
                                new City(){Name ="Rosario"},
                                new City(){Name ="Rafaela"},
                                new City(){Name ="Venado Tuerto"},
                            },
                        }
                    }
                });
                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos",
                    States = new List<State>()
                    {
                        new State()
                        {
                            Name="Florida",
                            Cities=new List<City>
                            {
                                new City{Name ="Miami"},
                                new City{Name ="Orlando"},
                                new City{Name ="Tampa"},
                                new City{Name ="Key West"},
                                new City{Name ="Fort Lauderdale"},
                            }
                        },
                        new State()
                        {
                            Name="Texas",
                            Cities=new List<City>
                            {
                                new City(){Name ="Houston"},
                                new City(){Name ="San Antonio"},
                                new City(){Name ="Dallas"},
                                new City(){Name ="Austin"},
                                new City(){Name ="El paso"},
                            },
                        },
                        new State()
                        {
                            Name="California",
                            Cities=new List<City>
                            {
                                new City(){Name ="Los Angeles",},
                                new City(){Name ="San Francisco",},
                                new City(){Name ="Santa Mónica",},
                            },
                        }
                    }
                });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckProductsAsync()
        {
            if (!_context.Products.Any())
            {
                await AddProductAsync("Apple Pencil", 270000M, 12F, new List<string>() { "Tecnología", "Apple" }, new List<string>() { "ApplePencil.jpg", "ApplePencil2.jpg" });
                await AddProductAsync("IPad", 250000M, 12F, new List<string>() { "Tecnología", "Apple" }, new List<string>() { "IPad.jpg", "IPad2.jpg", "IPad3.jpg" });
                await AddProductAsync("IPhoneX", 1300000M, 12F, new List<string>() { "Tecnología", "Apple" }, new List<string>() { "IPhoneX.jpg", "IPhoneX2.jpg", "IPhoneX3.jpg", "IPhoneX4.jpg" });
                await AddProductAsync("Notebook", 870000M, 12F, new List<string>() { "Tecnología" }, new List<string>() { "Notebook.jpg", "Notebook2.jpg", "Notebook3.jpg", "Notebook4.jpg", "Notebook5.jpg" });

                await AddProductAsync("Aguardiente", 12345M, 12F, new List<string>() { "Bebidas" }, new List<string>() { "Aguardiente.jpg" });
                await AddProductAsync("ajo", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "ajo.jpg" });
                await AddProductAsync("apio", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "apio.jfif" });
                await AddProductAsync("arandela", 12345M, 12F, new List<string>() { "Tecnología" }, new List<string>() { "arandela.jpg" });
                await AddProductAsync("arveja", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "arveja.jfif" });
                await AddProductAsync("baffless", 12345M, 12F, new List<string>() { "Tecnología" }, new List<string>() { "baffless.jfif" });
                await AddProductAsync("balon de futbol fifa 2014", 12345M, 12F, new List<string>() { "Juguetería" }, new List<string>() { "balon de futbol fifa 2014.jpg" });
                await AddProductAsync("bananasbrasileras", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "bananasbrasileras.jfif" });
                await AddProductAsync("bicicleta de iron man", 12345M, 12F, new List<string>() { "Juguetería" }, new List<string>() { "bicicleta de iron man.jpg" });
                await AddProductAsync("bicicleta de ni､o", 12345M, 12F, new List<string>() { "Juguetería" }, new List<string>() { "bicicleta de ni､o.jpg" });
                await AddProductAsync("bicicleta de ruta", 12345M, 12F, new List<string>() { "Juguetería" }, new List<string>() { "bicicleta de ruta.jpg" });
                await AddProductAsync("bicicleta estatica", 12345M, 12F, new List<string>() { "Juguetería" }, new List<string>() { "bicicleta estatica.jfif" });
                await AddProductAsync("bluray", 12345M, 12F, new List<string>() { "Tecnología" }, new List<string>() { "bluray.jfif" });
                await AddProductAsync("brocoli", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "brocoli.jpg" });
                await AddProductAsync("calabacin", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "calabacin.jpg" });
                await AddProductAsync("calabaza", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "calabaza.jfif" });
                await AddProductAsync("camaradigitalpanasonic", 12345M, 12F, new List<string>() { "Tecnología" }, new List<string>() { "camaradigitalpanasonic.jpg" });
                await AddProductAsync("caminadora", 12345M, 12F, new List<string>() { "Juguetería" }, new List<string>() { "caminadora.jfif" });
                await AddProductAsync("casco zoom bk", 12345M, 12F, new List<string>() { "Juguetería" }, new List<string>() { "casco zoom bk.jpg" });
                await AddProductAsync("cebolla", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "cebolla.jpg" });
                await AddProductAsync("cerveza", 12345M, 12F, new List<string>() { "Bebidas" }, new List<string>() { "cerveza.jpeg" });
                await AddProductAsync("ciruela", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "ciruela.jfif" });
                await AddProductAsync("Coca cola", 12345M, 12F, new List<string>() { "Bebidas" }, new List<string>() { "Coca cola.jfif" });
                await AddProductAsync("col", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "col.jfif" });
                await AddProductAsync("colchonetas", 12345M, 12F, new List<string>() { "Juguetería" }, new List<string>() { "colchonetas.jpg" });
                await AddProductAsync("esparragos", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "esparragos.jfif" });
                await AddProductAsync("espinaca", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "espinaca.jpg" });
                await AddProductAsync("fanta", 12345M, 12F, new List<string>() { "Bebidas" }, new List<string>() { "fanta.jpg" });
                await AddProductAsync("flotador en forma de delfin", 12345M, 12F, new List<string>() { "Juguetería" }, new List<string>() { "flotador en forma de delfin.jpg" });
                await AddProductAsync("frijoles", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "frijoles.jpg" });
                await AddProductAsync("guayaba", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "guayaba.jfif" });
                await AddProductAsync("guisantes", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "guisantes.jpeg" });
                await AddProductAsync("habas", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "habas.jpg" });
                await AddProductAsync("impresoracanon", 12345M, 12F, new List<string>() { "Tecnología" }, new List<string>() { "impresoracanon.jpg" });
                await AddProductAsync("iphone5", 12345M, 12F, new List<string>() { "Apple" }, new List<string>() { "iphone5.jpg" });
                await AddProductAsync("juego de mesa", 12345M, 12F, new List<string>() { "Juguetería" }, new List<string>() { "juego de mesa.jpg" });
                await AddProductAsync("laptop acer", 12345M, 12F, new List<string>() { "Tecnología" }, new List<string>() { "laptop acer.jfif" });
                await AddProductAsync("laptop dell", 12345M, 12F, new List<string>() { "Tecnología" }, new List<string>() { "laptop dell.jpg" });
                await AddProductAsync("lechuga", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "lechuga.jpg" });
                await AddProductAsync("macbookair", 12345M, 12F, new List<string>() { "Apple" }, new List<string>() { "macbookair.jpg" });
                await AddProductAsync("mancuernas", 12345M, 12F, new List<string>() { "Juguetería" }, new List<string>() { "mancuernas.jpg" });
                await AddProductAsync("mandarinas", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "mandarinas.jfif" });
                await AddProductAsync("Mango", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "Mango.png" });
                await AddProductAsync("manzana", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "manzana.jpg" });
                await AddProductAsync("maquinadeafeitar", 12345M, 12F, new List<string>() { "Tecnología" }, new List<string>() { "maquinadeafeitar.jpg" });
                await AddProductAsync("maracuya", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "maracuya.jpg" });
                await AddProductAsync("melocoton", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "melocoton.jfif" });
                await AddProductAsync("melon", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "melon.jfif" });
                await AddProductAsync("mesa de ping pong", 12345M, 12F, new List<string>() { "Juguetería" }, new List<string>() { "mesa de ping pong.jpg" });
                await AddProductAsync("microondas", 12345M, 12F, new List<string>() { "Tecnología" }, new List<string>() { "microondas.jpg" });
                await AddProductAsync("monopatin", 12345M, 12F, new List<string>() { "Juguetería" }, new List<string>() { "monopatin.jpg" });
                await AddProductAsync("nabos", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "nabos.jfif" });
                await AddProductAsync("Papaya", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "Papaya.jpg" });
                await AddProductAsync("patines zoom chasis aluminio", 12345M, 12F, new List<string>() { "Juguetería" }, new List<string>() { "patines zoom chasis aluminio.jfif" });
                await AddProductAsync("patineta", 12345M, 12F, new List<string>() { "Juguetería" }, new List<string>() { "patineta.jpg" });
                await AddProductAsync("pelota de playa", 12345M, 12F, new List<string>() { "Juguetería" }, new List<string>() { "pelota de playa.jpg" });
                await AddProductAsync("pepinos", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "pepinos.jfif" });
                await AddProductAsync("pepsi", 12345M, 12F, new List<string>() { "Bebidas" }, new List<string>() { "pepsi.jpg" });
                await AddProductAsync("peras", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "peras.jfif" });
                await AddProductAsync("pimientos", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "pimientos.jfif" });
                await AddProductAsync("platano", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "platano.jpg" });
                await AddProductAsync("POMELO", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "POMELO.jfif" });
                await AddProductAsync("portatiltouchhewlettpackard", 12345M, 12F, new List<string>() { "Tecnología" }, new List<string>() { "portatiltouchhewlettpackard.jfif" });
                await AddProductAsync("raquetas", 12345M, 12F, new List<string>() { "Juguetería" }, new List<string>() { "raquetas.jfif" });
                await AddProductAsync("REMOLACHA", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "REMOLACHA.jfif" });
                await AddProductAsync("REPOLLO", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "REPOLLO.jfif" });
                await AddProductAsync("samsunggalaxys5", 12345M, 12F, new List<string>() { "Tecnología" }, new List<string>() { "samsunggalaxys5.jpg" });
                await AddProductAsync("SANDIA", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "SANDIA.jfif" });
                await AddProductAsync("silbato profesional", 12345M, 12F, new List<string>() { "Juguetería" }, new List<string>() { "silbato profesional.jfif" });
                await AddProductAsync("Sprite", 12345M, 12F, new List<string>() { "Bebidas" }, new List<string>() { "Sprite.jpg" });
                await AddProductAsync("tabletintel7", 12345M, 12F, new List<string>() { "Tecnología" }, new List<string>() { "tabletintel7.jpg" });
                await AddProductAsync("TAMARINDO", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "TAMARINDO.jpg" });
                await AddProductAsync("teatroencasa", 12345M, 12F, new List<string>() { "Tecnología" }, new List<string>() { "teatroencasa.jpg" });
                await AddProductAsync("televisionsonybravia", 12345M, 12F, new List<string>() { "Tecnología" }, new List<string>() { "televisionsonybravia.jfif" });
                await AddProductAsync("tequila", 12345M, 12F, new List<string>() { "Bebidas" }, new List<string>() { "tequila.jpg" });
                await AddProductAsync("TOMATE", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "TOMATE.jpg" });
                await AddProductAsync("trompo", 12345M, 12F, new List<string>() { "Juguetería" }, new List<string>() { "trompo.jpg" });
                await AddProductAsync("tvledsamsung", 12345M, 12F, new List<string>() { "Tecnología" }, new List<string>() { "tvledsamsung.jpg" });
                await AddProductAsync("UVA", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "UVA.jfif" });
                await AddProductAsync("vino espumoso brut royal", 12345M, 12F, new List<string>() { "Bebidas" }, new List<string>() { "vino espumoso brut royal.jpg" });
                await AddProductAsync("vino espumoso demi sec", 12345M, 12F, new List<string>() { "Bebidas" }, new List<string>() { "vino espumoso demi sec.jpg" });
                await AddProductAsync("vino tinto malbec", 12345M, 12F, new List<string>() { "Bebidas" }, new List<string>() { "vino tinto malbec.jpg" });
                await AddProductAsync("Vino Tinto", 12345M, 12F, new List<string>() { "Bebidas" }, new List<string>() { "Vino Tinto.jpg" });
                await AddProductAsync("vodka", 12345M, 12F, new List<string>() { "Bebidas" }, new List<string>() { "vodka.jpg" });
                await AddProductAsync("whisky", 12345M, 12F, new List<string>() { "Bebidas" }, new List<string>() { "whisky.jpg" });
                await AddProductAsync("xbox360", 12345M, 12F, new List<string>() { "Tecnología" }, new List<string>() { "xbox360.jpeg" });
                await AddProductAsync("ZANAHORIA", 12345M, 12F, new List<string>() { "Verdulería" }, new List<string>() { "ZANAHORIA.jfif" });





                await _context.SaveChangesAsync();
            }
        }

        private async Task AddProductAsync(string name, decimal price, float stock, List<string> categories, List<string> images)
        {
            Product product = new()
            {
                Description = name,
                Name = name,
                Price = price,
                Stock = stock,
                ProductCategories = new List<ProductCategory>(),
                ProductImages = new List<ProductImage>()
            };

            foreach (string? category in categories)
            {
                product.ProductCategories.Add(new ProductCategory { Category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == category) });
            }


            foreach (string? image in images)
            {
                string imageId = $"~/images/products/{image}";
                product.ProductImages.Add(new ProductImage { ImageId = imageId });
            }

            _context.Products.Add(product);
        }



    }
}