//using Microsoft.Extensions.Configuration;
//using Microsoft.Owin;
//using Owin;
//namespace WebAPI
//{
//    public class Startup
//    {
//        public Startup(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }

//        public IConfiguration Configuration { get; }

//        // Este método se llama en tiempo de ejecución. Utilice este método para agregar servicios al contenedor.
//        public void ConfigureServices(IServiceCollection services)
//        {
//            services.AddCors(options =>
//            {
//                options.AddPolicy("AllowSpecificOrigin",
//                    builder =>
//                    {
//                        builder.WithOrigins("https://localhost:7176")
//                               .AllowAnyHeader()
//                               .AllowAnyMethod();
//                    });
//            });

//            // Otros servicios que puedas estar agregando
//        }

//        // Este método se llama en tiempo de ejecución. Utilice este método para configurar el pipeline de solicitud HTTP.
//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//        {
//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//            }
//            else
//            {
//                // Configuración de manejo de errores en producción
//            }

//            app.UseHttpsRedirection();
//            app.UseStaticFiles(); // Este middleware es opcional, dependiendo de tus necesidades

//            app.UseRouting();

//            app.UseCors("AllowSpecificOrigin"); // Habilitar CORS usando la política definida

//            app.UseAuthorization();

//            app.UseEndpoints(endpoints =>
//            {
//                endpoints.MapControllers();
//                endpoints.MapControllerRoute(
//                    name: "default",
//                    pattern: "{controller}/{action=Index}/{id?}");
//                endpoints.MapFallbackToController("Index", "Home"); // Manejo de rutas no coincidentes
//            });
//        }

//    }


//}
