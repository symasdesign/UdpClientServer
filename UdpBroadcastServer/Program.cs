using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpBroadcastServer {


    class UdpBroadcastServer {
        private const int Port = 8000;
        private static UdpClient udpClient = new UdpClient();

        static void Main(string[] args) {
            // Sende den Broadcast
            Console.WriteLine("Press Enter to send Broadcast...");
            Console.ReadLine();

            SendBroadcast("is there anybody out there?");

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();

            udpClient.Close();
        }

        private static void SendBroadcast(string message) {
            try {
                udpClient.EnableBroadcast = true;
                var endPoint = new IPEndPoint(IPAddress.Broadcast, Port);
                byte[] bytes = Encoding.ASCII.GetBytes(message);
                udpClient.Send(bytes, bytes.Length, endPoint);

                Console.WriteLine("Broadcast sent.");
            } catch (Exception ex) {
                Console.WriteLine($"Error sending broadcast: {ex.Message}");
            }
        }

    }
}
