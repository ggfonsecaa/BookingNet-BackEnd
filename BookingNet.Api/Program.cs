using BookingNet.Api;
using BookingNet.Application;
using BookingNet.Infraestructure;
using BookingNet.Infraestructure.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
    .AddNewtonsoftJson(options => 
        {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
            options.UseMemberCasing();
        });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddResponseCaching();


builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.SectionName));

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfraestructure(builder.Configuration);
builder.Services.AddApi(builder.Configuration);

var app = builder.Build();

app.UseCors(options => 
{
    options.WithOrigins("http://localhost:4200");
    options.AllowAnyHeader();
    options.AllowAnyMethod();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//var supportedCultures = new[] { "en", "es" };
//var localizationOptions =
//    new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
//    .AddSupportedCultures(supportedCultures)
//    .AddSupportedUICultures(supportedCultures);
//localizationOptions.ApplyCurrentCultureToResponseHeaders = true;

//app.UseRequestLocalization(localizationOptions);

app.UseExceptionHandler("/error");
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseResponseCaching();
app.MapControllers();
app.Run();