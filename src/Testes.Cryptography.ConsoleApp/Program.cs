using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

namespace Testes.Cryptography.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var vpn = NetworkInterface.GetAllNetworkInterfaces().First(x => x.Name == "SonicWall NetExtender");
            var ip = vpn.GetIPProperties().UnicastAddresses.First(x => x.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Address.ToString();

            double cpf = 26475431869;
            cpf = 83036215059;
            Console.WriteLine(cpf);

            Console.ReadKey();

            var pwd = "TST@Pwd#2022!";
            using (SHA1 sha1Hash = SHA1.Create())
            {
                var sourceBytes = Encoding.UTF8.GetBytes(pwd);
                var hashBytes = sha1Hash.ComputeHash(sourceBytes);
                var hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

                Console.WriteLine("The SHA1 hash of " + pwd + " is: " + hash.ToLower());
            }
        }
    }
}
