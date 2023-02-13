var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "corsphotos",
                      builder =>
                      {
                          builder
                            .WithOrigins("*") // specifying the allowed origin
                            .AllowAnyMethod() // defining the allowed HTTP method
                            .AllowAnyHeader(); // allowing any header to be sent
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseCors("corsphotos");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
