using Infrastructure.Data;
using Infrastructure.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<FreeBookDbContext>
(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BookConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<FreeBookDbContext>();
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
app.UseEndpoints(Endpoint =>
    {
        Endpoint.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Accounts}/{action=Login}/{id?}"
              );



        Endpoint.MapControllerRoute(

    name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    });
app.Run();
