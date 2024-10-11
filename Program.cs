using ShellCodingTask.Services;

namespace ShellCodingTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
        
            builder.Services.AddSingleton<BlobService>();//new
            builder.Services.AddSingleton<DataProcessingService>();//new
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
            app.UseStaticFiles(); // for testing html
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
