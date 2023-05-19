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

            Console.WriteLine("Serwer zosta³ zatrzymany.");
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
            Console.WriteLine($"Serwer nas³uchuje na porcie {port}...");

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

                // Pobierz datê i godzinê w strefie czasowej klienta
                DateTime clientTime = DateTime.Now;
                TimeZoneInfo clientTimeZone = TimeZoneInfo.Local;
                DateTime clientLocalTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(clientTime, clientTimeZone.Id);

                // Przygotuj odpowiedŸ dla klienta
                response = $"Autor serwera: {authorName}\n";
                response = $"Adres IP klienta: {clientAddress}\n";
                response += $"Data i godzina w strefie czasowej klienta: {clientLocalTime}";

                // Zamieñ odpowiedŸ na tablicê bajtów
                byte[] data = Encoding.UTF8.GetBytes(response);

                // Wyœlij odpowiedŸ do klienta
                NetworkStream stream = client.GetStream();
                await stream.WriteAsync(data, 0, data.Length);

                // Zamknij po³¹czenie
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wyst¹pi³ b³¹d obs³ugi klienta: {ex.Message}");
            }

            return response;
        }
    }
}


