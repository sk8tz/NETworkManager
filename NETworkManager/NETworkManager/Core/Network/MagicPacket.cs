using System.Net;
using System.Net.Sockets;

namespace NETworkManager.Core.Network
{
    public static class MagicPacket
    {
        /// <summary>
        /// Create a Magic Packet from MAC-Address
        /// </summary>
        /// <param name="MAC">MAC-Address as byte[]</param>
        /// <returns>Magic Packet as byte array</returns>
        public static byte[] CreateFromBytes(byte[] mac)
        {
            byte[] packet = new byte[17 * 6];

            for (int i = 0; i < 6; i++)
            {
                packet[i] = 0xFF;
            }

            for (int i = 1; i <= 16; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    packet[i * 6 + j] = mac[j];
                }
            }

            return packet;
        }

        /// <summary>
        /// Send a Magic Packet
        /// </summary>
        /// <param name="packet">Magic Packet as byte array</param>
        /// <param name="broadcast">Broadcast-Address</param>
        /// <param name="port">Port-Number</param>
        public static void Send(byte[] packet, IPAddress broadcast, int port)
        {
            UdpClient udpClient = new UdpClient();

            udpClient.Connect(broadcast, port);

            udpClient.Send(packet, packet.Length);
        }
    }
}
