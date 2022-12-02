using AutoMapper;
using MvcCadeteria;
using MvcCadeteria.Repositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<MiRepositorioCadete, RepositorioCadete>();
builder.Services.AddAutoMapper(typeof(Program));

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
    options.IdleTimeout = TimeSpan.FromSeconds(600);//timespan es un intervalo de tiempo
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
//********************************




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

app.UseAuthorization();

//************ SESIONES... va despues de useAutorization
app.UseSession();
//***********
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
