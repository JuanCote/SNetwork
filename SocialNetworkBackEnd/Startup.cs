using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SocialNetworkBackEnd.Properties;
using SocialNetworkBackEnd.Repository;
using SocialNetworkBackEnd.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SocialNetworkBackEnd.Interafaces.Post;
using SocialNetworkBackEnd.Interafaces.User;
using SocialNetworkBackEnd.Interafaces.Sub;

namespace SocialNetworkBackEnd
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SocialNetworkBackEnd", Version = "v1" });
            });
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IPostRepository, PostRepository>();

            services.AddTransient<ISubRepository, SubRepository>();
            services.AddTransient<ISubService, SubService>();

            services.Configure<DbConnectionKeys>(Configuration.GetSection("DbConnectionKeys"));
            services.AddControllers().AddNewtonsoftJson();
            services.AddAuthorization();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = AuthOptions.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SocialNetworkBackEnd v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
