using AutoMapper;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VueCliMiddleware;
using VueJSDotnet51.Data;
using VueJSDotnet51.Data.FileManager;
using VueJSDotnet51.Data.FirebaseManager;
using Microsoft.OpenApi.Models;

namespace VueJSDotnet51
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
          
            services.AddDbContext<ApiContext>(options => options.UseSqlite(Configuration.GetSection("ConnectionStrings:DefaultConnection").Value));

            //Identity
            //services.AddIdentity<AppUser, IdentityRole>()
            //.AddEntityFrameworkStores<ApiContext>();
            //.AddDefaultTokenProviders();

            var op = new AppOptions();

            op.Credential = GoogleCredential.FromFile("service-account.json");
            FirebaseApp.Create(op);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:5000")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            }
             );



            //// Authentication
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;



            // // JWT
            //}).AddJwtBearer(options => {
            //    options.SaveToken = true;
            //    options.RequireHttpsMetadata = false;

            //    options.TokenValidationParameters = new TokenValidationParameters()
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
            //        ValidateAudience = false,
            //        ValidateIssuer = false,

            //    };

            //});

            services.AddControllers().AddNewtonsoftJson(options =>
   options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
   );
            services.AddScoped<IShiftRepository, ShiftRepository>();
            services.AddScoped<IFileManager, FileManager>();
            services.AddScoped<IFirebaseManager, FirebaseManager>();

         


            services
            .AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
               
            })
            .AddJwtBearer(options =>
            {
                options.Authority = "https://securetoken.google.com/fethi-510c0";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "https://securetoken.google.com/fethi-510c0",
                    ValidateAudience = true,
                    ValidAudience = "fethi-510c0",
                    ValidateLifetime = true,
                   
                };
            });
            services.AddAuthorization(options=> {

             options.AddPolicy("Admin", policy => policy.RequireClaim("Role","admin"));
              options.AddPolicy("Driver", policy => policy.RequireClaim("Role", "driver"));
              options.AddPolicy("Rider", policy => policy.RequireClaim("Role", "rider"));
           });

         //   services.AddScoped<IShiftRepository, ShiftRepository>();

            services.AddAutoMapper();

            //  services.AddCors(options =>
            //  {
            //      options.AddPolicy("AllowSpecificOrigin",
            //          builder => builder.WithOrigins("http://localhost:5000")
            //              .AllowAnyMethod()
            //              .AllowAnyHeader());
            //  }
            //);


        



            // Swagger 

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Taxi Booking API",
                    Description = "Taxi Booking Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Teber Med Tahar",
                        Email = "tebermedtahar@gmail.com",
                        Url = new Uri("https://twitter.com/taha_teber"),
                    },
                });


                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());



                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                };

                c.AddSecurityRequirement(securityRequirement);


            });

        

            
          
           


            //services.AddSpaStaticFiles(configuration =>
            //{
            //    configuration.RootPath = "ClientApp";
            //});

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseSpa(spa =>
            //{
            //    if (env.IsDevelopment())
            //        spa.Options.SourcePath = "ClientApp/";
            //    else
            //        spa.Options.SourcePath = "dist";

            //    if (env.IsDevelopment())
            //    {
            //        spa.UseVueCli(npmScript: "serve");
            //    }

            //});
        }
    }
}
