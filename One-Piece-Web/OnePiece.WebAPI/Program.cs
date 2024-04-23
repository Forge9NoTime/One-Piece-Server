namespace OnePiece.WebAPI
{
    using Microsoft.EntityFrameworkCore;
    using One_Piece.Data;
    using One_Piece.Service.Interfaces;
    using OnePiece.Web.Infrastructure.Extentions;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<OnePieceDbContext>(opt =>
            opt.UseSqlServer(connectionString));

            builder.Services.AddApplicationServices(typeof(IMissionService));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(setup =>
            {
                setup.AddPolicy("OnePiece", policyBuilder =>
                {
                    policyBuilder.WithOrigins("https://localhost:7224")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

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

            app.UseCors("OnePiece");

            app.Run();
        }
    }
}