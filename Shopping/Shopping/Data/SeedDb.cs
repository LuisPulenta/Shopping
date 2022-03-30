using Shopping.Helpers;
using Shopping.Data;
using Shopping.Data.Entities;
using Shopping.Enums;

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
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Luis", "Núñez", "luis@yopmail.com", "351 681 4963", "Espora 2052", UserType.Admin);
            await CheckUserAsync("2020", "Pablo", "Lacuadri", "pablo@yopmail.com", "351 555 412234", "Villa Santa Ana", UserType.Admin);
            await CheckUserAsync("3030", "Diego", "Maradona", "maradona@yopmail.com", "311 322 4620", "Villa Fiorito", UserType.User);
            await CheckUserAsync("4040", "Lionel", "Messi", "messi@yopmail.com", "311 322 4620", "París", UserType.User);
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task CheckUserAsync(string document, string firstName, string lastName, string email, string phoneNumber, string address, UserType userType)
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
    }
}