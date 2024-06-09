using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Models.Entities;
using Models.Enums;
using Type = Models.Enums.Type;

namespace Services.Database;

public static class SeedDbInitializer
{
    public static void Seed(TenisKlubDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Users.Any())
        {
            return;
        }

        var countries = new Country[]
        {
            new Country { Name = "Bosnia and Herzegovina", ShortName = "BIH" },
            new Country { Name = "Croatia", ShortName = "CRO" },
            new Country { Name = "Serbia", ShortName = "SER" },
            new Country { Name = "Spain", ShortName = "ESP" },
            new Country { Name = "Switzerland", ShortName = "SUI" },
        };

        foreach (Country c in countries)
        {
            context.Country.Add(c);
        }

        context.SaveChanges();

        string adminSalt = GenerateSalt();
        string novakSalt = GenerateSalt();
        string ninoSalt = GenerateSalt();
        string rafaSalt = GenerateSalt();
        string carlosSalt = GenerateSalt();

        var users = new User[]
        {
            new User
            {
                Name = "Admin",
                Surname = "Admin",
                Email = "admin@gmail.com",
                Username = "admin",
                PasswordHash = GenerateHash(adminSalt, "admin"),
                PasswordSalt = adminSalt,
                PhoneNumber = "+38766888999",
                DateOfBirth = new DateTime(1990, 1, 1),
                CountryId = countries.Single(c => c.Name == "Bosnia and Herzegovina").Id,
                Role = Role.Admin
            },
            new User
            {
                Name = "Novak",
                Surname = "Đoković",
                Email = "novak@gmail.com",
                Username = "novakdjokovic",
                PasswordHash = GenerateHash(novakSalt, "novak"),
                PasswordSalt = novakSalt,
                PhoneNumber = "+38167555222",
                DateOfBirth = new DateTime(1981, 2, 2),
                Height = 185,
                Weight = 70,
                CountryId = countries.Single(c => c.Name == "Serbia").Id,
                Role = Role.Player
            },
            new User
            {
                Name = "Nino",
                Surname = "Gosto",
                Email = "nino@gmail.com",
                Username = "ninogosto",
                PasswordHash = GenerateHash(ninoSalt, "nino"),
                PasswordSalt = ninoSalt,
                PhoneNumber = "+387603345789",
                DateOfBirth = new DateTime(2001, 7, 11),
                Height = 180,
                Weight = 72,
                CountryId = countries.Single(c => c.Name == "Bosnia and Herzegovina").Id,
                Role = Role.Player
            },
            new User
            {
                Name = "Carlos",
                Surname = "Alcaraz",
                Email = "carlos@gmail.com",
                Username = "carlosalcaraz",
                PasswordHash = GenerateHash(carlosSalt, "carlos"),
                PasswordSalt = carlosSalt,
                PhoneNumber = "+387603345789",
                DateOfBirth = new DateTime(2001, 7, 11),
                Height = 180,
                Weight = 72,
                CountryId = countries.Single(c => c.Name == "Spain").Id,
                Role = Role.Player
            },
            new User
            {
                Name = "Rafael",
                Surname = "Nadal",
                Email = "rafa@gmail.com",
                Username = "rafaelnadal",
                PasswordHash = GenerateHash(rafaSalt, "rafael"),
                PasswordSalt = rafaSalt,
                PhoneNumber = "+387603345789",
                DateOfBirth = new DateTime(2001, 7, 11),
                Height = 180,
                Weight = 72,
                CountryId = countries.Single(c => c.Name == "Spain").Id,
                Role = Role.Player
            },
            new User
            {
                Name = "Goran",
                Surname = "Ivanisevic",
                Email = "goran@gmail.com",
                Username = "gorani",
                PasswordHash = GenerateHash(adminSalt, "goran"),
                PasswordSalt = adminSalt,
                PhoneNumber = "+387603345789",
                DateOfBirth = new DateTime(2001, 7, 11),
                Height = 180,
                Weight = 72,
                CountryId = countries.Single(c => c.Name == "Croatia").Id,
                Role = Role.Coach
            },
            new User
            {
                Name = "Toni",
                Surname = "Nadal",
                Email = "toni@gmail.com",
                Username = "toninadal",
                PasswordHash = GenerateHash(adminSalt, "toni"),
                PasswordSalt = adminSalt,
                PhoneNumber = "+387603345789",
                DateOfBirth = new DateTime(2001, 7, 11),
                Height = 180,
                Weight = 72,
                CountryId = countries.Single(c => c.Name == "Spain").Id,
                Role = Role.Coach
            }
        };

        foreach (User u in users)
        {
            context.Users.Add(u);
        }

        context.SaveChanges();

        var courts = new Court[]
        {
            new Court
            {
                Name = "Philippe Chatrier",
                CourtNumber = 1,
                CourtStatus = CourtStatus.Active,
                Category = Category.Clay
            },
            new Court
            {
                Name = "Suzanne Lenglen",
                CourtNumber = 2,
                CourtStatus = CourtStatus.Active,
                Category = Category.Clay
            },
            new Court
            {
                Name = "Simonne Mathieu",
                CourtNumber = 3,
                CourtStatus = CourtStatus.Active,
                Category = Category.Clay
            },
            new Court
            {
                Name = "Court 5",
                CourtNumber = 5,
                CourtStatus = CourtStatus.Inactive,
                Category = Category.Clay
            },
            new Court
            {
                Name = "Court 15 Hard",
                CourtNumber = 15,
                CourtStatus = CourtStatus.Active,
                Category = Category.Hard
            },
            new Court
            {
                Name = "Court 25 clay indoor",
                CourtNumber = 25,
                CourtStatus = CourtStatus.Active,
                Category = Category.ClayIndoor
            }
        };

        foreach (Court court in courts)
        {
            context.Court.Add(court);
        }

        context.SaveChanges();

        var items = new Item[]
        {
            new Item
            {
                Name = "Babolat Racket",
                Info = "Professional tennis racket",
                Price = 199,
                Code = "TR123",
                Image = ReadBase64StringFromFile("ImageBase64Files/babolat-racket.txt"),
                Type = Type.Racket,
                Brand = Brand.Babolat,
                Availability = Availability.Available
            },
            new Item
            {
                Name = "Tennis Balls",
                Info = "Pack of 3 tennis balls",
                Price = 15,
                Code = "TB456",
                Image = ReadBase64StringFromFile("ImageBase64Files/tennis-balls.txt"),
                Type = Type.Balls,
                Brand = Brand.Dunlop,
                Availability = Availability.Available
            },
            new Item
            {
                Name = "Tennis Bag",
                Info = "Durable tennis bag",
                Price = 75,
                Code = "TB789",
                Image = ReadBase64StringFromFile("ImageBase64Files/wilson-bag.txt"),
                Type = Type.Bag,
                Brand = Brand.Wilson,
                Availability = Availability.Available
            },
            new Item
            {
                Name = "Tennis Shoes",
                Info = "Comfortable tennis shoes",
                Price = 120,
                Code = "TS101",
                Image = ReadBase64StringFromFile("ImageBase64Files/asics-shoes.txt"),
                Type = Type.Shoes,
                Brand = Brand.Asics,
                Availability = Availability.NotAvailable
            },
            new Item
            {
                Name = "Tennis Shorts",
                Info = "Breathable tennis shorts",
                Price = 45,
                Code = "TS111",
                Image = ReadBase64StringFromFile("ImageBase64Files/nike-shorts.txt"),
                Type = Type.Shorts,
                Brand = Brand.Nike,
                Availability = Availability.Available
            },
            new Item
            {
                Name = "Tennis Shirt",
                Info = "High-performance tennis shirt",
                Price = 35,
                Code = "TS222",
                Image = ReadBase64StringFromFile("ImageBase64Files/yonex-tshirt.txt"),
                Type = Type.Shirt,
                Brand = Brand.Yonex,
                Availability = Availability.Available
            }
        };

        foreach (Item item in items)
        {
            context.Item.Add(item);
        }

        context.SaveChanges();

        var notifications = new Notification[]
        {
            new Notification
            {
                Name = "Welcome",
                Description = "Welcome to our tennis club!",
                CreatedAt = DateTime.Now,
                IsActive = false,
                UserId = users.Single(u => u.Username == "admin").Id
            },
            new Notification
            {
                Name = "Match Schedule",
                Description = "Check out the new match schedule.",
                CreatedAt = DateTime.Now,
                IsActive = true,
                UserId = users.Single(u => u.Username == "admin").Id
            },
            new Notification
            {
                Name = "New Coach Announcement",
                Description = "We are pleased to welcome our new coach, Novak Đoković!",
                CreatedAt = DateTime.Now,
                IsActive = true,
                UserId = users.Single(u => u.Username == "admin").Id
            },
            new Notification
            {
                Name = "Club Maintenance",
                Description = "The tennis courts will undergo maintenance next week.",
                CreatedAt = DateTime.Now,
                IsActive = true,
                UserId = users.Single(u => u.Username == "admin").Id
            },
            new Notification
            {
                Name = "Tournament Announcement",
                Description = "Join us for the annual club tournament next month.",
                CreatedAt = DateTime.Now,
                IsActive = true,
                UserId = users.Single(u => u.Username == "admin").Id
            }
        };

        foreach (Notification notification in notifications)
        {
            context.Notification.Add(notification);
        }

        context.SaveChanges();

        var reviews = new Review[]
        {
            new Review
            {
                NumberOfStars = 5,
                Description = "Great experience!",
                CreatedAt = DateTime.Now,
                UserId = users.Single(u => u.Username == "carlosalcaraz").Id
            },
            new Review
            {
                NumberOfStars = 4,
                Description = "Had a good time.",
                CreatedAt = DateTime.Now,
                UserId = users.Single(u => u.Username == "ninogosto").Id
            },
            new Review
            {
                NumberOfStars = 3,
                Description = "It was okay.",
                CreatedAt = DateTime.Now,
                UserId = users.Single(u => u.Username == "rafaelnadal").Id
            }
        };

        foreach (Review review in reviews)
        {
            context.Review.Add(review);
        }

        context.SaveChanges();

        var results = new Result[]
        {
            new Result
            {
                Opponent = "Roger Federer",
                PlayedAt = DateTime.Now.AddDays(-10),
                MatchResult = "6-3 6-4",
                MatchStatus = MatchStatus.Won,
                CourtId = courts.Single(c => c.CourtNumber == 1).Id,
                UserId = users.Single(u => u.Username == "carlosalcaraz").Id
            },
            new Result
            {
                Opponent = "Rafael Nadal",
                PlayedAt = DateTime.Now.AddDays(-20),
                MatchResult = "6-7 5-7",
                MatchStatus = MatchStatus.Lost,
                CourtId = courts.Single(c => c.CourtNumber == 2).Id,
                UserId = users.Single(u => u.Username == "ninogosto").Id
            },
            new Result
            {
                Opponent = "Andy Murray",
                PlayedAt = DateTime.Now.AddDays(-15),
                MatchResult = "6-4 6-4",
                MatchStatus = MatchStatus.Won,
                CourtId = courts.Single(c => c.CourtNumber == 1).Id,
                UserId = users.Single(u => u.Username == "novakdjokovic").Id
            }
        };

        foreach (Result result in results)
        {
            context.Result.Add(result);
        }

        context.SaveChanges();

        var orders = new Order[]
        {
            new Order
            {
                OrderNumber = "ORD001",
                TotalPrice = 250,
                CreatedAt = DateTime.Now,
                Status = OrderStatus.Active,
                PayingStatus = PayingStatus.NotPayed,
                UserId = users.Single(u => u.Username == "ninogosto").Id,
                OrderDetails = new List<OrderDetails>
                {
                    new OrderDetails { ItemId = items.Single(i => i.Code == "TR123").Id, Quantity = 1 },
                    new OrderDetails { ItemId = items.Single(i => i.Code == "TB456").Id, Quantity = 2 }
                }
            },
            new Order
            {
                OrderNumber = "ORD002",
                TotalPrice = 100,
                CreatedAt = DateTime.Now.AddDays(-1),
                Status = OrderStatus.Completed,
                PayingStatus = PayingStatus.Payed,
                UserId = users.Single(u => u.Username == "ninogosto").Id,
                OrderDetails = new List<OrderDetails>
                {
                    new OrderDetails { ItemId = items.Single(i => i.Code == "TS111").Id, Quantity = 1 },
                    new OrderDetails { ItemId = items.Single(i => i.Code == "TS222").Id, Quantity = 1 }
                }
            },
            new Order
            {
                OrderNumber = "ORD003",
                TotalPrice = 350,
                CreatedAt = DateTime.Now,
                Status = OrderStatus.Completed,
                PayingStatus = PayingStatus.Payed,
                UserId = users.Single(u => u.Username == "carlosalcaraz").Id,
                OrderDetails = new List<OrderDetails>
                {
                    new OrderDetails { ItemId = items.Single(i => i.Code == "TR123").Id, Quantity = 1 },
                    new OrderDetails { ItemId = items.Single(i => i.Code == "TS101").Id, Quantity = 1 }
                }
            },
            new Order
            {
                OrderNumber = "ORD004",
                TotalPrice = 200,
                CreatedAt = DateTime.Now.AddDays(-3),
                Status = OrderStatus.Active,
                PayingStatus = PayingStatus.NotPayed,
                UserId = users.Single(u => u.Username == "rafaelnadal").Id,
                OrderDetails = new List<OrderDetails>
                {
                    new OrderDetails { ItemId = items.Single(i => i.Code == "TB789").Id, Quantity = 1 },
                    new OrderDetails { ItemId = items.Single(i => i.Code == "TS111").Id, Quantity = 1 }
                }
            }
        };

        foreach (Order order in orders)
        {
            context.Orders.Add(order);
        }

        context.SaveChanges();

        var images = new ImageModel[]
        {
            new ImageModel()
                { FileName = "Shorts", Image = ReadBase64StringFromFile("ImageBase64Files/nike-shorts.txt") },
            new ImageModel()
                { FileName = "Racket", Image = ReadBase64StringFromFile("ImageBase64Files/babolat-racket.txt") },
            new ImageModel()
                { FileName = "Racket", Image = ReadBase64StringFromFile("ImageBase64Files/wilson-bag.txt") },
            new ImageModel()
                { FileName = "Racket", Image = ReadBase64StringFromFile("ImageBase64Files/babolat-racket.txt") },
            new ImageModel()
                { FileName = "Racket", Image = ReadBase64StringFromFile("ImageBase64Files/babolat-racket.txt") },
            new ImageModel()
                { FileName = "Racket", Image = ReadBase64StringFromFile("ImageBase64Files/babolat-racket.txt") },
        };

        foreach (ImageModel im in images)
        {
            context.ImageModel.Add(im);
        }

        context.SaveChanges();

        var reservations = new Reservation[]
        {
            new Reservation()
            {
                CreatedAt = DateTime.Now.AddDays(-6),
                ReservationStatus = ReservationStatus.Completed,
                UserId = users.Single(u => u.Username == "rafaelnadal").Id,
                Service = new Service()
                {
                    Price = 100,
                    StartDateTime = DateTime.Now.AddDays(-5).AddHours(10).AddMinutes(0),
                    EndDateTime = DateTime.Now.AddDays(-5).AddHours(11).AddMinutes(0),
                    CourtId = courts.Single(u => u.Name == "Philippe Chatrier").Id,
                }
            },
            new Reservation()
            {
                CreatedAt = DateTime.Now.AddDays(-4),
                ReservationStatus = ReservationStatus.Completed,
                UserId = users.Single(u => u.Username == "ninogosto").Id,
                Service = new Service()
                {
                    Price = 100,
                    StartDateTime = DateTime.Now.AddDays(-3).AddHours(14).AddMinutes(0),
                    EndDateTime = DateTime.Now.AddDays(-3).AddHours(15).AddMinutes(0),
                    CourtId = courts.Single(u => u.Name == "Philippe Chatrier").Id,
                }
            }
        };
        
        foreach (Reservation r in reservations)
        {
            context.Reservation.Add(r);
        }

        context.SaveChanges();
    }

    private static string GenerateSalt()
    {
        var provider = new RNGCryptoServiceProvider();
        var byteArray = new byte[16];
        provider.GetBytes(byteArray);

        return Convert.ToBase64String(byteArray);
    }

    private static string GenerateHash(string salt, string password)
    {
        byte[] src = Convert.FromBase64String(salt);
        byte[] bytes = Encoding.Unicode.GetBytes(password);
        byte[] dst = new byte[src.Length + bytes.Length];

        Buffer.BlockCopy(src, 0, dst, 0, src.Length);
        Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

        var algorithm = HashAlgorithm.Create("SHA1");
        var inArray = algorithm?.ComputeHash(dst);

        return Convert.ToBase64String(inArray);
    }

    private static string ReadBase64StringFromFile(string filePath)
    {
        return File.ReadAllText(filePath);
    }
}