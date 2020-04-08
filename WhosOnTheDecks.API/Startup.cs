using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using WhosOnTheDecks.API.Data;

namespace WhosOnTheDecks.API
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
            //Allows use of Sqlite database by providing the connection string
            services.AddDbContext<DataContext>(x => x.UseSqlite
            (Configuration.GetConnectionString("DefaultConnection")));
            
            //Allows us to use Controllers
             services.AddControllers(); 

             // Allows the use of Cors as a service which relates to our Angular front end
             services.AddCors();

            
            //Allows Auth interface and repository to be used
            //It will use the same reference within the same http requests 
            //but will generate another if the a new http request is made 
            services.AddScoped<IAuthRepository, AuthRepository>();

            //Adding authentication as a service
            //It will validate the key associated with tokens when a user is logging in
            //First it validates the authentication scheme applied
            //Then the options are applied to the Jwtbearer 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        //Validates the key found in appsettings.json
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                            .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                            //Issuer is localhost so no validation needed atm
                            ValidateIssuer = false,
                            //Audience is localhost so no validation is needed atm
                            ValidateAudience = false
                    };
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection(); 
            // disabling this in production as we dont want https cetificates being used currently

            app.UseRouting(); // allows us to use routing 

            app.UseAuthentication(); //Allow the use of the authentication scheme applied to the Jwt tokens

            app.UseAuthorization(); // allows use of request authorisation

            // This is the cors policy to allow any origin method or header
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            // Allows app to use controller end points
            // Allows fallback to controller fallback as an endpoint
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapFallbackToController("Index", "Fallback");
            });
        }
    }
}
