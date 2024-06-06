using AppProject.Web.Data;
using AppProject.Web.Services;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AppProject.Web
{
    public static class CustomConfiguration
    {
        #region Builder
        public static WebApplicationBuilder AddCustomBuilderConfiguration(this WebApplicationBuilder builder)
        {
            //Data context
            builder.Services.AddDbContext<DataContext>(conf =>
            {
                conf.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"));
            });

            // Services
            AddServices(builder);

            // Toast
            builder.Services.AddNotyf(config =>
            {
                config.DurationInSeconds = 10;
                config.IsDismissable = true;
                config.Position = NotyfPosition.BottomRight;
            });

            return builder;


        }

        private static void AddServices(this WebApplicationBuilder builder)
        {
            // Services
            builder.Services.AddScoped<ITaskListsService, TaskListsService>();
            builder.Services.AddScoped<ITipCalculatorService, TipCalculatorService>();
            builder.Services.AddScoped<IPasswordService, PasswordService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<INoteService, NoteService>();


            //Helpers
        }
        #endregion Builder

        #region App

        public static WebApplication AddCustomConfiguration(this WebApplication app)
        {
            app.UseNotyf();

            return app;
        }
        #endregion App

    }
}
