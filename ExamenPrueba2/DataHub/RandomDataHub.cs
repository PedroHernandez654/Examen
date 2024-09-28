using ExamenPrueba2.Model;
using Microsoft.AspNetCore.SignalR;

namespace ExamenPrueba2.DataHub
{
    public class RandomDataHub : Hub
    {
        public async Task SendRandomData(RandomTable randomData)
        {
            await Clients.All.SendAsync("ReceiveRandomData", randomData);
        }
        public async Task SendDeleteNotification(int id)
        {
            await Clients.All.SendAsync("ReceiveDeleteNotification", id);
        }

    }
}
