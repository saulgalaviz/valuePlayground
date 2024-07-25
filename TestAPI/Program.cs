var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//installed both Microsoft.ASPNetCore.JsonPatch and Microsoft.ASPNetCore.MVC.newtonsoftjson
//builder.Services.AddControllers() previously, but added .AddNewtonsoftJson to add patching, a separate package I installed.
builder.Services.AddControllers(option =>
{
    option.ReturnHttpNotAcceptable = true; //Added this entire piece which means response type must be json or else error is sent out
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters(); //The AddXMLDataC.... piece makes it so XML can also be acceptable as a response type
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
