using ExamenPrueba2.DataHub;
using ExamenPrueba2.Interfaces;
using ExamenPrueba2.Model;
using ExamenPrueba2.Model.ViewModel;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Timers;


namespace ExamenPrueba2.Services
{
    public class RegisterRandomDataService : IRegisterRandomDataService
    {
        private readonly IHubContext<RandomDataHub> _hubContext;
        private int counter;
        private static ConcurrentDictionary<int, System.Timers.Timer> rowTimers = new ConcurrentDictionary<int, System.Timers.Timer>();

        private const string CounterFilePath = "counter.txt";

        //Leer contenido
        private void LoadCounter()
        {
            if (File.Exists(CounterFilePath))
            {
                var counterValue = File.ReadAllText(CounterFilePath);
                if (int.TryParse(counterValue, out int loadedCounter))
                {
                    counter = loadedCounter;
                }
            }
        }
        //Guardar mi id counter
        private void SaveCounter()
        {
            File.WriteAllText(CounterFilePath, counter.ToString());
        }

        public RegisterRandomDataService(IHubContext<RandomDataHub> hubContext)
        {
            _hubContext = hubContext;
            LoadCounter();
        }

        public async Task<(string,bool)> SendRandomDataToClients(RandomViewModel randomData)
        {
            counter++;
            SaveCounter();
            RandomTable randomTable = new RandomTable();
            randomTable.Id = counter;
            randomTable.Nombre = randomData.Nombre;
            randomTable.Multiplo = randomData.Multiplo;
            randomTable.Frecuencia = randomData.Frecuencia;
            randomTable.NumeroRandom = await MakeMultiplo(randomData.Frecuencia);

            if (counter <= 10)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveRandomData", randomTable);

                //Iniciar mi cronometro
                StartRowTimer(randomTable);

                return ("Registro Exitoso",true);
            }
            else
            {
                return ("No se aceptan más registros",false);
            }

        }

        private void StartRowTimer(RandomTable randomTable)
        {
            System.Timers.Timer timer = new System.Timers.Timer(randomTable.Frecuencia * 1000);
            timer.Elapsed += async (sender, e) => await UpdateRandomNumber(randomTable);
            timer.AutoReset = true;
            timer.Enabled = true;

            rowTimers[randomTable.Id] = timer;
        }
        private async Task UpdateRandomNumber(RandomTable randomTable)
        {
            randomTable.NumeroRandom = await MakeMultiplo(randomTable.Frecuencia);

            await _hubContext.Clients.All.SendAsync("UpdateRowData", randomTable.Id, randomTable.NumeroRandom);
        }

        private async Task<int> MakeMultiplo(int multiplo)
        {
            return await Task.Run(() =>
            {
                Random random = new Random();
                int randomValue = random.Next(1, 100);

                while (randomValue % multiplo != 0)
                {
                    randomValue = random.Next(1, 100);
                }

                return randomValue;
            });
        }
    }
}
