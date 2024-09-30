using dal.Data;
using dal.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using mvc2.Extensions;
using mvc2.Helpers;
using System.Configuration;

namespace mvc2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var Builder = WebApplication.CreateBuilder(args);
            #region configure services

            
            Builder.Services.AddControllersWithViews();
            Builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(Builder.Configuration.GetConnectionString("DefaultConnection")));
            Builder.Services.AddApplicationServices();
            Builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
            Builder.Services.AddIdentity<ApplicationUser, IdentityRole>(/*config =>
            {
                //config.Password.RequiredUniqueChars = 1;
                //config.Password.RequireUppercase = true;    
                //config.Password.RequireLowercase = true;
                //config.Password.RequireDigit=true;
                //config.User.RequireUniqueEmail=true;
                //config.Lockout.MaxFailedAccessAttempts = 6;
            }*/).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            IServiceCollection serviceCollection = Builder.Services.Configure<MailSettings>(Builder.Configuration.GetSection("MailSettings"));
            Builder.Services.AddScoped<EmailSettings>();
            #endregion

            var app = Builder.Build();

            #region configure
            if (Builder.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run();
            #endregion
            CreateHostBuilder(args).Build().Run();
        }




        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.UseStartup<Startup>();
                });
    }
}
