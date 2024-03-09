using Hangfire;
using HangfireBasicAuthenticationFilter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHangfire((sp, config) => 
{
	var connectionString = sp.GetRequiredService<IConfiguration>().GetConnectionString("DbConnection");
	config.UseSqlServerStorage(connectionString);
});
builder.Services.AddHangfireServer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHangfireDashboard("/test/job-dashboard",
	new DashboardOptions
	{
		DashboardTitle = "Hangfire Job Demo Application",
		DarkModeEnabled = true,
		DisplayStorageConnectionString = false,
		Authorization = new[]
		{
			new HangfireCustomBasicAuthenticationFilter
			{
				User = "admin",
				Pass = "admin123"
			}
		}
	});

app.Run();
