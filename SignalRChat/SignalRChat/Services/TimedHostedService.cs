using Microsoft.AspNetCore.SignalR;
using SignalRChat.Hubs;

namespace SignalRChat.Services
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<TimedHostedService> _logger;
        private readonly IHubContext<ChatHub> _hubContext;
        private Timer _timer;
        private int waitTime = 2;

        public TimedHostedService(ILogger<TimedHostedService> logger, IHubContext<ChatHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(waitTime));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("Timed Hosted Service is working.");
            _hubContext.Clients.All.SendAsync("ReceiveMessage", "Server", $"<b style=\"color: green\"> This is a message sent every {waitTime} seconds </b>");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        // returns -1 if user tries to set the amount below the lowest value of 1
        public int updateWaitTime(int amount)
        {
            waitTime += amount;
            if (waitTime < 1)
            {
                waitTime = 1;
                _timer.Change(TimeSpan.FromSeconds(waitTime), TimeSpan.FromSeconds(waitTime));
                return -1;
            }
            _timer.Change(TimeSpan.FromSeconds(waitTime), TimeSpan.FromSeconds(waitTime));
            return waitTime;
        }
    }
}
