using System;
using System.Text.RegularExpressions;

namespace NETworkManager.Core.Network
{
    public static class ConvertMACAddress
    {
        /// <summary>
        /// Convert a MAC-Address to a byte array
        /// </summary>
        /// <param name="MACAddress"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(string MACAddress)
        {
            // Regex to replace "-" and ":" in MAC-Address
            Regex regex = new Regex("-|:");
            string mac = regex.Replace(MACAddress, "");

            // Build the byte-array
            byte[] bytes = new byte[mac.Length / 2];

            // Convert the MAC-Address into byte and fill it...
            for (int i = 0; i < 12; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(mac.Substring(i, 2), 16);
            }

            return bytes;
        }
    }
}
