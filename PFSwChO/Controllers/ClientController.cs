using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using System.Text;

namespace PFSwChO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        // GET: HomeController
        [HttpGet(Name = "client")]
        public ActionResult Index()
        {
            string serverIp = "127.0.0.1"; // Adres IP serwera
            int serverPort = 12345; // Port serwera

            try
            {
                TcpClient client = new TcpClient();
                client.Connect(serverIp, serverPort);

                Console.WriteLine("Połączono z serwerem.");
                var message = "wiadomość";
                byte[] data = Encoding.UTF8.GetBytes(message);

                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);

                byte[] responseBuffer = new byte[1024];
                int bytesRead = stream.Read(responseBuffer, 0, responseBuffer.Length);
                string response = Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);

                Console.WriteLine("Odpowiedź serwera:");
                Console.WriteLine(response);

                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd podczas komunikacji z serwerem: {ex.Message}");
            }
            return Ok();
        }
    }
}
