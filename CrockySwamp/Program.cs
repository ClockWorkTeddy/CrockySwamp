using CrockySwamp;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CrockySwamp
{
    public static class Program
    {
        static TcpListener? Server;
        static TcpClient? Client = new TcpClient();
        async public static Task Main(string[] args)
        {
            await AwaitClient();
            
            var setUpData = (await XTalk(SetGreetings())).Split(new char[] { ' ' });

            Swamp swamp = new(Convert.ToInt16(setUpData[0]));
            Naturalist naturalist = new(swamp, setUpData[1]);

            do
            {
                NextStep(swamp, naturalist);
            }
            while (await XTalk(new List<string> { "press" }) != "q");

            Server?.Stop();
        }

        async static Task AwaitClient()
        {
            Server = new TcpListener(IPAddress.Parse("192.168.0.53"), 8888);
            Server.Start();
            Client = await Server.AcceptTcpClientAsync();
        }

        async static Task<string> XTalk(List<string> messages)
        {
            if (Client == null)
                return "";

            var stream = Client.GetStream();

            if (messages.Count > 0)
            {
                var responce = GetResponce(messages);
                await stream.WriteAsync(responce);
            }

            var result = ReceiveClientData(stream);

            return result;
        }

        static string ReceiveClientData(NetworkStream stream)
        {
            int bytesRead = 10;
            List<byte> query = new List<byte>();

            while ((bytesRead = stream.ReadByte()) != '#')
                query.Add((byte)bytesRead);

            var data = Encoding.UTF8.GetString(query.ToArray());

            return data;
        }

        static byte[] GetResponce(List<string> data)
        {
            StringBuilder responce = new StringBuilder();

            foreach (var message in data)
                responce.Append(message);

            return Encoding.UTF8.GetBytes(responce.ToString());
        }

        static List<string> SetGreetings()
        {
            List<string> result = new List<string>();

            result.Add("Hello! There will be a swamp with corcks and frogs! And naturalist!@");
            result.Add("Please, input swamp size and naturalist's name devided by space.@");
            result.Add("#@");

            return result;
        }

        static void NextStep(Swamp swamp, Naturalist naturalist)
        {
            Console.Clear();
            swamp.Move();
            naturalist.Observe();
            Drawer.Print();
        }
    }
}

