﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
           .AddFacebook(Foptions =>
           {
               Foptions.AppId = "783478808509094";
               Foptions.AppSecret = "603a11fb352f6693d9185538a49943fa";
           })
           .AddVK(Voptions =>{
               Voptions.ClientId = "6406580";
               Voptions.ClientSecret = "SbqA9hn3xfcoAplU5SMv";
               Voptions.Fields.Add("first_name");
               Voptions.Fields.Add("last_name");
               Voptions.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "first_name");
               Voptions.ClaimActions.MapJsonKey(ClaimTypes.Surname, "last_name");
           })
           .AddTwitter(Toptions =>
           {
               Toptions.ConsumerKey = "K4rGUY6SwKPcvCcm73TNFDMgr";
               Toptions.ConsumerSecret = "7v87qSbvOu4KIAVoVzwgYgN3q3oEutE0QVjz2xnrz9CJGXSIzK";
           })
           .AddCookie();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseWebSockets();
            app.UseMiddleware<ChatWebSocketMiddleware>();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
