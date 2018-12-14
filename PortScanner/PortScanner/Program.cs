using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace PortScanner
{
    class Program
    {
        static void Main(string[] args)
        {
            var openPorts = new List<int>();

            Parallel.For(1, 656, index =>
            {
                var port = Scan((index * 100) - 99, index * 100);
                if (port.HasValue)
                {
                    openPorts.Add(port.Value);
                }
            });

            foreach(var port in openPorts)
            {
                Console.WriteLine($"TCP Port {port} is open");
            }

            Console.ReadLine();
        }

        public static int? Scan(int portStart, int portRange)
        {
            var ip = "10.181.75.15";

            var openPorts = new List<int>();

            while (portStart <= portRange)
            {
                try
                {
                    var client = new TcpClient(ip, portStart);

                    openPorts.Add(portStart);

                    Console.WriteLine($"TCP Port {portStart} is open - on ip {ip}");

                    return portStart;
                }
                catch (Exception)
                {
                    //Console.WriteLine($"Port {portStart} is closed - on ip {ip}");
                }

                portStart++;
            }

            return null;
        }
    }    
}
