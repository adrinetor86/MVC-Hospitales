var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
    name:"default",
    pattern:"{controller=Home}/{action=Index}/{id?}"
    );

app.Run();

//var builder = WebApplication.CreateBuilder(args);

////EL CONSTRUCTOR  INDICARÁ COMO GENERAR LA APP MEDIANTE
////METODOS LE INDICAMOS EL TIPADO DE LA APP
//builder.Services.AddControllersWithViews();

//var app = builder.Build();

////wwwroot
//app.UseStaticFiles();
////MVC
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");//pasamos un id
//app.Run();