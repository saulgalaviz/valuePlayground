//using Serilog; //Packages installed for serilog.aspnetcore and serilog.sinks.file for this solution

using TestAPI.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Part of serilog. Here, we can set minimum level of logging to several options, warning, error, debugging, etc. Can add paramter to set length of logging to minute, hour, day etc (not needed).
/*Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
    .WriteTo.File("log/valueLogs.txt", rollingInterval: RollingInterval.Day).CreateLogger();*/

//Tell builder to not use default logging and instead to use Serilog
//builder.Host.UseSerilog();

//installed both Microsoft.ASPNetCore.JsonPatch and Microsoft.ASPNetCore.MVC.newtonsoftjson
//builder.Services.AddControllers() previously, but added .AddNewtonsoftJson to add patching, a separate package I installed.
builder.Services.AddControllers(option =>
{
    option.ReturnHttpNotAcceptable = true; //Added this entire piece which means response type must be json or else error is sent out
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters(); //The AddXMLDataC.... piece makes it so XML can also be acceptable as a response type
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add custom service for logging
//addsingleton is highest - created when application starts and used whenever application requests an implementation. addscope is next hightest is used whenever a request is called, a new object will be created,
//AddTransient is lowest - every time request is accesssed - a new request will be added
//Simple to change implementation by just changing class name, for below example changed from Logging class to LoggingV2
builder.Services.AddSingleton<ILogging, LoggingV2>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
