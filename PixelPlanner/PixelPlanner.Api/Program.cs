using PixelPlanner.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers().Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddAppServices();

var app = builder.Build();

app.UseSwagger()
    .UseSwaggerUI(x => x.DisplayOperationId())
    .UseHttpsRedirection()
    .UseAuthorization();

app.MapControllers();
app.Run();
