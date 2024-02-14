using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Severstal.Core.Contracts;
using Severstal.Data;
using Severstal.Core;
using Severstal.ExportDocument;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Severstal
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();
            services.AddDbContext<AppContext>();
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IService, Service>();
            services.AddTransient<IExport, ExportWord>();

            // Построение провайдера служб
            var serviceProvider = services.BuildServiceProvider();

            // Получение экземпляра IService из DI
            var service = serviceProvider.GetRequiredService<IService>();

            // Запуск главной формы, передавая IService в конструктор
            Application.Run(new Main(serviceProvider, service));
        }
    }
}

