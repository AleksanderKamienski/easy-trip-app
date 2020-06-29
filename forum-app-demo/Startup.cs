using forum_app_demo.Services;
using Forum.Data;
using Forum.Data.Models;
using Forum.Service;
using Forum.Web.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;

namespace Forum.Web
{
    public class Startup
    {
        // This is a new Startup constructor.
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IForum, ForumService>();
            services.AddScoped<IPost, PostService>();
            services.AddScoped<IGroup, GroupService>();
            services.AddScoped<IUsersGroups, UsersGroupsService>();
            services.AddScoped<IPostReply, PostReplyService>();
            services.AddScoped<IApplicationUser, ApplicationUserService>();
            services.AddSingleton<IUpload, UploadService>();
            services.AddScoped<IPostFormatter, PostFormatter>();
            services.AddSingleton(Configuration);
            services.AddTransient<DataSeeder>();
            services.AddMvc();

            //services.AddRazorPages();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("EditPolicy", policy =>
                    policy.Requirements.Add(new GroupRequirement()));
            });

            services.AddScoped<IAuthorizationHandler, GroupAuthorizationHandler>();
            //services.AddScoped<IAuthorizationHandler, ForumAuthorizationHandler>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DataSeeder dataSeeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            dataSeeder.SeedAll();


            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
