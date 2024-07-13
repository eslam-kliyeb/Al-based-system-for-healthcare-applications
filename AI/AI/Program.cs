using AI.Extensions;
using AI.Middlewares;
namespace AI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //================================================
            var builder = WebApplication.CreateBuilder(args);
            #region Add services to the container
            builder.Services.AddDataBasesServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddBadRequestServices();
            builder.Services.AddCorsPolicyServices(builder.Configuration);
            builder.Services.AddSwaggerService();
            #endregion
            var app = builder.Build();
            await DbInitializer.InitializeDbAsync(app);
            #region Configure the HTTP request pipeline.
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //===================My MiddleWare=========================
                app.UseMiddleware<ExceptionMiddleWare>();
                //=============================================
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            #endregion
            app.Run();
        }
    }
}
