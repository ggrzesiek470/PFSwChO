using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PFSwChO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogsController : ControllerBase
    {
        static int port = 12345;
        static string authorName = "Grzegorz Grzegorczyk";
        [HttpGet(Name = "logs")]

        public void Get()
        {
            // Zarejestruj informacje w logach
            LogStartupInformation(DateTime.Now, authorName, port);

            // Uruchom serwer
            StartServer(port);

            Console.WriteLine("Serwer zosta� zatrzymany.");
        }

        static void LogStartupInformation(DateTime startTime, string authorName, int port)
        {
            Console.WriteLine($"Data uruchomienia: {startTime}");
            Console.WriteLine($"Autor serwera: {authorName}");
            Console.WriteLine($"Port TCP: {port}");
        }

        static void StartServer(int port)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            Console.WriteLine($"Serwer nas�uchuje na porcie {port}...");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                _ = HandleClientAsync(client);
            }
        }

        static async Task<string> HandleClientAsync(TcpClient client)
        {
            string response = null;
            try
            {
                // Pobierz adres IP klienta
                IPAddress clientAddress = ((IPEndPoint)client.Client.RemoteEndPoint).Address;

                // Pobierz dat� i godzin� w strefie czasowej klienta
                DateTime clientTime = DateTime.Now;
                TimeZoneInfo clientTimeZone = TimeZoneInfo.Local;
                DateTime clientLocalTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(clientTime, clientTimeZone.Id);

                // Przygotuj odpowied� dla klienta
                response = $"Autor serwera: {authorName}\n";
                response = $"Adres IP klienta: {clientAddress}\n";
                response += $"Data i godzina w strefie czasowej klienta: {clientLocalTime}";

                // Zamie� odpowied� na tablic� bajt�w
                byte[] data = Encoding.UTF8.GetBytes(response);

                // Wy�lij odpowied� do klienta
                NetworkStream stream = client.GetStream();
                await stream.WriteAsync(data, 0, data.Length);

                // Zamknij po��czenie
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wyst�pi� b��d obs�ugi klienta: {ex.Message}");
            }

            return response;
        }
    }
}


