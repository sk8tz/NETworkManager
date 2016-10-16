using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NETworkManager.Core.Network
{
    public static class MagicPacket
    {
        /// <summary>
        /// Create a Magic Packet from a given MAC-Address
        /// </summary>
        /// <param name="MAC">MAC-Address like AB01CD23EF45 without "-" or ":"</param>
        /// <returns>Magic Packet</returns>
        public static byte[] Create(byte[] mac)
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

        public static void Send(byte[] packet, IPAddress broadcast, int port)
        {

        }
    }
}
