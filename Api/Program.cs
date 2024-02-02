using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);
var MalApiClientId = "0";


if (builder.Environment.IsProduction())
{
    builder.Configuration.AddEnvironmentVariables();
}

MalApiClientId = builder.Configuration["MalClientId"];


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddHttpClient(
    "MALClient",
    client =>
    {
        client.BaseAddress = new Uri("https://api.myanimelist.net");
        client.DefaultRequestHeaders.Add("X-MAL-CLIENT-ID", MalApiClientId);
    }
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("AllowAll");

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Decahex API");

app.Run();

