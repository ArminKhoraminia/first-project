using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TopLearn.Core.Convertors;
using TopLearn.Core.Services;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;

var builder = WebApplication.CreateBuilder(args);

#region Add services

builder.Services.AddRazorPages();
builder.Services.AddMvc(services => services.EnableEndpointRouting = false);//?????????????????????????????

#region Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
});
#endregion

#region DataBase Context
builder.Services.AddDbContext<TopLearnContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TopLearnConnection"));
});
#endregion

#region Ioc
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserPanelService, UserPanelService>();
builder.Services.AddTransient<IWalletService, WalletService>();
builder.Services.AddTransient<IAdminService, AdminService>();
builder.Services.AddTransient<IPermissionService, PermissionService>();
builder.Services.AddTransient<ICourseService, CourseService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IForumService, ForumService>();
builder.Services.AddTransient<IViewRenderService, RenderViewToString>();
#endregion

#endregion 


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

#region Error404

app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Response.Redirect("/Home/Error404");
    }
});

#endregion

#region Using App

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMvc();//?????????????????????????????
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

#endregion

#region UseEndpoints

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

    endpoints.MapControllerRoute(name: "areas", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(name: "Default", pattern: "{controller=Home}/{action=Index}/{id?}");

});

#endregion

app.Run();
