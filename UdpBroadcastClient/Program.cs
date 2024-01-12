using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpBroadcastClient {


    class UdpBroadcastClient {
        private const int Port = 8000;
        private static UdpClient udpClient = new UdpClient();

        static void Main(string[] args) {
            udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, Port));
            udpClient.BeginReceive(ReceiveCallback, null);

            Console.WriteLine("Listening for broadcasts...");
            Console.ReadLine();

            udpClient.Close();
        }

        private static void ReceiveCallback(IAsyncResult ar) {
            try {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, Port);
                byte[] bytes = udpClient.EndReceive(ar, ref endPoint);
                string message = Encoding.ASCII.GetString(bytes);

                Console.WriteLine($"Received broadcast: {message}");

                // Send response
                string responseMessage = "Response from client";
                byte[] responseBytes = Encoding.ASCII.GetBytes(responseMessage);
                udpClient.Send(responseBytes, responseBytes.Length, endPoint);

                // Continue listening
                udpClient.BeginReceive(ReceiveCallback, null);
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

}
