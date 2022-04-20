﻿using Shopping.Helpers;
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