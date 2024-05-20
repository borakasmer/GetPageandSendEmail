using GetPageandSendEmail.Service;
using GetPageandSendEmail;
using Hangfire;
using HangfireBasicAuthenticationFilter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Hangfire
builder.Services.AddHangfire(x => x.UseSqlServerStorage("Data Source=192.168.50.173;initial catalog=Hangfire;User Id=****;Password=******;TrustServerCertificate=True;"));
builder.Services.AddHangfireServer();

builder.Services.AddTransient<ICoreService, CoreService>();

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

IConfiguration _configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();
//app.UseHangfireDashboard();
//Install-Package Hangfire.Dashboard.Basic.Authentication
//app.UseHangfireDashboard("/job", new DashboardOptions());
app.UseHangfireDashboard("/job", new DashboardOptions
{
    Authorization = new[]
    {
        new HangfireCustomBasicAuthenticationFilter
        {
         User = _configuration.GetSection("HangfireSettings:UserName").Value,
         Pass = _configuration.GetSection("HangfireSettings:Password").Value
    }
    }
});

app.UseHangfireServer(new BackgroundJobServerOptions());
GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 3 });

var serviceProvider = builder.Services.BuildServiceProvider();
var _coreService = serviceProvider.GetService<ICoreService>();

RecurringJobs.GetWeeklyWebReport(_coreService, _configuration.GetSection("HangfireSettings:ParsingUrl").Value);

app.Run();
