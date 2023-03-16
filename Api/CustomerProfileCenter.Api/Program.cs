using CustomerProfileCenter.Api;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new XssSanitizeJsonConvert()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog((_, _, configuration) =>
{
    //Aqui poderia ser qualquer outro tipo de sistema de log externo
    //configuration.WriteTo.File("./logs.txt");
    configuration.WriteTo.Console();
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();