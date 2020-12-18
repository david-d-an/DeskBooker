using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Processor;
using DeskBooker.DataAccess.Contexts;
using DeskBooker.DataAccess.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Data.SqlClient;

namespace DeskBooker.Web
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddRazorPages();
      var connectionString = Configuration.GetConnectionString("CurrentDataSource");

      services.AddDbContext<SQLiteContext>(builder =>
          builder.UseSqlite(connectionString)
      );
      EnsureDatabaseExists<SQLiteContext>(connectionString);

      services.AddScoped<IDeskRepository, DeskRepository>();
      services.AddScoped<IDeskBookingRepository, DeskBookingRepository>();
      services.AddScoped<IDeskBookingRequestProcessor, DeskBookingRequestProcessor>();
      // services.AddTransient<IDeskRepository, DeskRepository>();
      // services.AddTransient<IDeskBookingRepository, DeskBookingRepository>();
      // services.AddSingleton<IDeskRepository, DeskRepository>();
      // services.AddSingleton<IDeskBookingRepository, DeskBookingRepository>();

      // Make MySql Connection Service
      var connStrMySql = Configuration.GetConnectionString("MySqlConnection");
      services.AddDbContext<MySqlContext>(builder =>                   
          builder.UseMySQL(connStrMySql)
      );
      EnsureDatabaseExists<MySqlContext>(connStrMySql);

      services.AddScoped<IEmployeeRepository, EmployeeRepository>();
      // services.AddTransient<IEmployeeRepository, EmployeeRepository>();
    }

    // private static void EnsureDatabaseExists(SqliteConnection connection)
    // {
    //   var builder = new DbContextOptionsBuilder<DeskBookerContext>();
    //   builder.UseSqlite(connection);

    //   using var context = new DeskBookerContext(builder.Options);
    //   context.Database.EnsureCreated();
    // }

    private static void EnsureDatabaseExists<T>(string connectionString) 
    where T : DbContext, new()
    {
        var builder = new DbContextOptionsBuilder<T>();
        if (typeof(T) == typeof(MySqlContext)) {
          builder.UseMySQL(connectionString);
        }
        else if (typeof(T) == typeof(SQLiteContext)) {
          builder.UseSqlite(connectionString);
        }
        else if (typeof(T) == typeof(SqlServerContext)) {
          builder.UseSqlServer(connectionString);
        }

        using (var context = 
          (T)(Activator.CreateInstance(typeof(T), new object[] { builder.Options })))
          ((T)context).Database.EnsureCreated();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapRazorPages();
      });
    }
  }
}
