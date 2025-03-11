
using Microsoft.EntityFrameworkCore;
using Steeltoe.Management.Endpoint;
using Steeltoe.Management.Tracing;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using Steeltoe.Connector.PostgreSql;
using Microsoft.Extensions.Configuration;
using Steeltoe.Extensions.Configuration;
//using Microsoft.Extensions.Configuration;
//using Microsoft.AspNetCore;
//using Steeltoe.Management.Endpoint.CloudFoundry;
//using Steeltoe.Common.Hosting;

       
    var builder = WebApplication.CreateBuilder(args);

    var confBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddCloudFoundry();

    var config = confBuilder.Build();
    var appName = config["vcap:application:application_name"];
    var instanceId = config["vcap:application:instance_id"];
    Console.WriteLine("InstanceID =>"+ instanceId);
    Console.WriteLine("App Name =>" + appName);

    var dbHost = config["vcap:services:postgres:0:credentials:hosts:0"];
    var dbUser = config["vcap:services:postgres:0:credentials:user"];
    var dbName = config["vcap:services:postgres:0:credentials:db"];
    var dbPort = config["vcap:services:postgres:0:credentials:port"];
    var dbPassword = config["vcap:services:postgres:0:credentials:password"];
    var dbConnectionUrl = "Host=" + dbHost+"; Port = "+dbPort+"; Database = "+dbName+"; Username = "+dbUser+"; Password = "+dbPassword;

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
  
    builder.Services.AddDistributedTracingAspNetCore();
    //builder.Services.AddPostgresConnection(config);

    builder.Services.AddDbContext<Demo_ASP_API.Data.AppDbContext>(options =>
                    options.UseNpgsql((dbHost != null) ? dbConnectionUrl : builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Host.ConfigureLogging(logging =>
    {
        logging.AddConsole();
    });

    var app = builder.Build();
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(builder => builder.AllowAnyOrigin());
    //app.UseCors("SteeltoeManagement");
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseDefaultFiles();
    //activate the CF security middleware
    //app.UseCloudFoundrySecurity();
    //app.UseRouting();
    //app.UseAuthorization();

    app.MapControllers();
    app.Run();
    
  
       
