
using Microsoft.EntityFrameworkCore;
using TreeTask.Models;
using TreeTask.Service;

namespace TreeTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add DbContext 
            builder.Services.AddDbContext<AssetsContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DB")));
            builder.Services.AddScoped<AssetsTreeService>();

            // Add services to the container.
            builder.Services.AddControllers();
            //.AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            //    options.JsonSerializerOptions.WriteIndented = true;
            //});


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
