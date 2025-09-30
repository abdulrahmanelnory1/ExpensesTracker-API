
using ExpensesTracker.Models;
using ExpensesTracker.Models;
using ExpensesTracker.Repository;
using ExpensesTracker.Repository;
using ExpensesTracker.UnitOfWork;
using ExpensesTracker.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ExpensesTracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //builder.Services.AddScoped<IExpenseService, ExpenseService>();

            builder.Services.AddScoped<UserRepo>();
            //  builder.Services.AddScoped<ExpensesRepo>();


            builder.Services.AddDbContext<ExpensesTrackerContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ExpTracCon")));

            builder.Services.AddScoped<CategoryRepo>();
            builder.Services.AddScoped<ExpenseRepo>();
            builder.Services.AddScoped<RecurringExpenseRepo>();
            builder.Services.AddScoped<UserRepo>();
            builder.Services.AddScoped<GenericRepo<Item>>();
            builder.Services.AddScoped<GenericRepo<Budget>>();
            builder.Services.AddScoped<AppUnitOfWork>();
            builder.Services.AddAuthentication(op => op.DefaultAuthenticateScheme = "mySchema")
                .AddJwtBearer("mySchema", option =>

                {
                    SecurityKey secKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("this is my key, Abdulrahman Elnory"));
                    var signCre = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256);

                    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = secKey
                    };

                }

                );

            builder.Services.AddHostedService<RecurringExpenseService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
