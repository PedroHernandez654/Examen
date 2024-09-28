using ExamenPrueba2.Model;
using ExamenPrueba2.Model.ViewModel;
using Microsoft.AspNetCore.SignalR.Client;

namespace ExamenPrueba2.DataHub
{
    public class SignalRClient
    {
        private HubConnection _hubConnection;
        //Eventos para recibir, actualizar y eliminar la fila
        public event Action<RandomTable> DataReceived;
        public event Action<int, int> RowUpdated;
        public event Action<int> ItemDeleted;


        public SignalRClient()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7183/randomDataHub")
                .Build();

            _hubConnection.On<RandomTable>("ReceiveRandomData", (randomData) =>
            {
                DataReceived?.Invoke(randomData);
            });

            _hubConnection.On<int, int>("UpdateRowData", (rowId, newValue) =>
            {
                RowUpdated?.Invoke(rowId, newValue);
            });

            _hubConnection.On<int>("ReceiveDeleteNotification", (id) =>
            {
                ItemDeleted?.Invoke(id);
            });
        }

        public async Task StartAsync()
        {
            try
            {
                await _hubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                
            }
        }
        public async Task StopAsync()
        {
            try
            {
                await _hubConnection.StopAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task SendRandomData(RandomTable randomData)
        {
            await _hubConnection.InvokeAsync("SendRandomData", randomData);
        }

        public async Task SendDeleteNotification(int id)
        {
            await _hubConnection.InvokeAsync("SendDeleteNotification", id);
        }

    }
}
