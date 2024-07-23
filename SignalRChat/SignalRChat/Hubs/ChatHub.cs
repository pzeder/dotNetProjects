using Microsoft.AspNetCore.SignalR;
using SignalRChat.Services;


namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        private TimedHostedService _timedHostedService;
        public ChatHub(TimedHostedService timedHostedService) {
            _timedHostedService = timedHostedService;
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task WaveHand(string user)
        {
            await Clients.All.SendAsync("ReceiveWave", user);
        }

        public async Task UpdateWaitTime(string user, int amount)
        {
            int updatedWaitTime = _timedHostedService.updateWaitTime(amount);
            string message = $"<b style=\"color: orange\"> updated server wait time to {updatedWaitTime} seconds </b>";
            if (updatedWaitTime == -1)
            {
                message = $"<b style=\"color: red\"> Server waiting time can not be lower then 1 second </b>";
            }
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
