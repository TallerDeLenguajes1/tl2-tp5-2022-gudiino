using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using MvcCadeteria;
using MvcCadeteria.Repositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<MiRepositorioCadete, RepositorioCadete>();
builder.Services.AddTransient<MiRepositorioCliente, RepositorioCliente>();
builder.Services.AddTransient<MiRepositorioPedido, RepositorioPedido>();
builder.Services.AddTransient<MiRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddTransient<MiRepositorioCadeteria, RepositorioCadeteria>();

// agregar mapeador de las digerentes clases para pasar a la vista
builder.Services.AddAutoMapper(typeof(Program));
//builder.Services.AddHostFiltering(new Filter.VerificarSession());// corregir inicio de secion

// configuracion de mi perfil de mapeo//https://www.youtube.com/watch?v=8GbkIP2uC6o
var mapperConfig = new MapperConfiguration(m => 
{
    m.AddProfile(new MiPerfilDeMapeo());//se puede agregar otro archivo de mapeo en una linea separada
});
IMapper mapper = mapperConfig.CreateMapper();
//builder.Services.AddSingleton(mapper);

//*****************************************************************  SESIONES
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(60);//timespan es un intervalo de tiempo
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
//******************************** autenticacion
builder.Services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
            {
                config.AccessDeniedPath = "/Usuarios/Error";
                config.LoginPath = "/Usuarios/Index";
            });


builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ADMIN", policy => policy.RequireRole("Administrador"));
                options.AddPolicy("CDT", policy => policy.RequireRole("Cadete"));
            });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

//************ SESIONES... va despues de useAutorization
app.UseSession();
//***********
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuarios}/{action=Index}/{id?}");

app.Run();
