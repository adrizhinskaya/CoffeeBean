using CoffeeBean.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CoffeeBean.DataAccess;
using CoffeeBean.Entity;

namespace CoffeeBean
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppIdentityDbContext>(
                options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders(); //��������� ����������� ������� �� ���������,
                                             //������������ ��� �������� ������� ��� ������ �������,
                                             //�������� ��������� ����������� ����� � ������ ��������,
                                             //� ����� ��� �������� ������� ������������� ��������������

            services.Configure<IdentityOptions>(options => //�������������� ���������� �������� �������
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
            });

            services.AddControllersWithViews();                                                       
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();// �� �������������� ����. ������ ������������ � cookie
            app.UseAuthorization(); // �� �������������� ����

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
