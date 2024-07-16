using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using WebApplication1.Filters;
using WebApplication1.Helpers;
using WebApplication1.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});


builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));//d��ar�dan dosya y�klemek i�in 



builder.Services.AddSingleton<IHelper , Helper>();
builder.Services.AddAutoMapper(Assembly .GetExecutingAssembly());
builder.Services.AddScoped<NotFoundFilter>();

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


//blog/abc => blog controller > article action method �al��s�n.
//blog/ddd => blog controller > article action method �al��s�n.
//app.MapControllerRoute(
//    name: "pages",
//    pattern: "blog/{*article}", defaults: new {controller="Blog",action="Article"});


//bunlar convertional bazl� routingdir..net 6 bunu destekler ama kullanmak sa�l�kl� de�ildir kodlar birbirini ezebilir.

//app.MapControllerRoute(
//    name: "article",
//    pattern: "{controller=Blog}/{action=Article}/{name}/{id}");



//app.MapControllerRoute(
//    name: "pages",
//    pattern: "{controller}/{action}/{page}/{pagesize}");//controller ve actiona bir de�er vermezsek page ve pagesize ile b�t�n controller ve actionlarda �al���r


//app.MapControllerRoute(
//    name: "getbyid",
//    pattern: "{controller}/{action}/{productid}");//e�er id yerine productid girilirse

//app.MapControllers();
app.MapControllerRoute(
    name: "default",
   pattern: "{controller=home}/{action=�ndex}/{id?}");

app.Run();
