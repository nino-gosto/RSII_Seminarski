using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Models.Requests;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Services.Database;
using Services.Services;
using Services.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ItemService, ItemService>();
builder.Services.AddTransient<CourtService, CourtService>();
builder.Services.AddTransient<CountryService, CountryService>();
builder.Services.AddTransient<UserService, UserService>();
builder.Services.AddTransient<NotificationService, NotificationService>();
builder.Services.AddTransient<OrderService, OrderService>();
builder.Services.AddTransient<ServiceService, ServiceService>();
builder.Services.AddTransient<ReservationService, ReservationService>();
builder.Services.AddTransient<ReviewService, ReviewService>();
builder.Services.AddTransient<ResultService, ResultService>();
builder.Services.AddTransient<ImageService, ImageService>();
builder.Services.AddTransient<NotificationRabbitService, NotificationRabbitService>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<INotificationRabbitService, NotificationRabbitService>();

builder.Services.AddAutoMapper(typeof(ItemService));
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TenisKlubDbContext>(options =>
{   
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});
builder.Services.AddCors(options => options.AddPolicy(
    name: "CorsPolicy",
    builder1 => builder1.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<TenisKlubDbContext>();
        SeedDbInitializer.Seed(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the database.");
    }
}


string hostname = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "127.0.0.1";
string username = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME") ?? "guest";
string password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? "guest";
string virtualHost = Environment.GetEnvironmentVariable("RABBITMQ_VIRTUALHOST") ?? "/";

var factory = new ConnectionFactory
{
    HostName = hostname,
    UserName = username,
    Password = password,
    VirtualHost = virtualHost,
};
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "notification",
    durable: false,
    exclusive: false,
    autoDelete: true,
    arguments: null);

Console.WriteLine(" [*] Waiting for messages.");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += async (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine(message.ToString());
    var notification = JsonSerializer.Deserialize<NotificationRabbitUpsertDto>(message);
    using (var scope = app.Services.CreateScope())
    {
        var notificationsService = scope.ServiceProvider.GetRequiredService<INotificationRabbitService>();

        if (notification != null)
        {
            try
            {

                await notificationsService.Insert(notification);
            }
            catch (Exception e)
            {
                throw new Exception("Error ", e);
            }
        }
    }
    Console.WriteLine(Environment.GetEnvironmentVariable("Some"));
};
channel.BasicConsume(queue: "notification",
    autoAck: true,
    consumer: consumer);

app.Run();