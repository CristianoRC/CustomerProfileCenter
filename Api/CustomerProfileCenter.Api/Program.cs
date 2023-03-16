using CustomerProfileCenter.Api;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new XssSanitizeJsonConvert()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog((builderContext, _, configuration) =>
{
    if (builderContext.HostingEnvironment.IsDevelopment())
        configuration.WriteTo.Console();
    else
        configuration.WriteTo.File("./logs.txt");
    //Aqui poderia ser qualquer outro tipo de sistema de log externo
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