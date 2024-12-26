using DataAccess;
using DataAccess.Implementation;
using DataAccess.Interface;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.Implementation;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(x =>
            x.UseSqlServer(connectionString));
        }

        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<ICategoryService, CategoryService>();
        }

        public static void InjectRepositories(IServiceCollection services)
        {
            // services.AddTransient<IRepository<Note>, NoteRepository>();
            services.AddTransient<IRepository<Category>, CategoryRepository>();
        }

        
    }
}
