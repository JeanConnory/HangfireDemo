using Hangfire.Server;

namespace HangfireDemo.Jobs;

public class TestJob
{
	private readonly ILogger _logger;

	public TestJob(ILogger<TestJob> logger) => _logger = logger;

	public void WriteLog(string logMessage)
	{
		_logger.LogInformation($"{DateTime.UtcNow:dd-MM-yyyy hh:mm:ss tt} {logMessage}");
	}
}
