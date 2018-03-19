using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Eaton.Mentoria.Repository.Context;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Eaton.Mentoria.Domain.Contracts;
using Eaton.Mentoria.Repository.Repositories;

namespace Eaton.Mentoria.WebApi
{
    public class Startup
    {

        public IConfiguration Configuration {get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }      

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           // services.AddMvc();
           services.AddDbContext<IMentoriaContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc().AddJsonOptions(Options => {
                Options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(ICategoriaRepository), typeof(CategoriaRepository));
            services.AddScoped(typeof(ISedeRepository), typeof(SedeRepository));
            services.AddScoped(typeof(IMentoriaRepository), typeof(MentoriaRepository));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
