using CrockySwamp;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CrockySwamp
{
    public static class Program
    {
        static TcpListener Server;
        static TcpClient Client;
        async public static Task Main(string[] args)
        {
            List<string> messages = new List<string>();

            Server = new TcpListener(IPAddress.Parse(""), 8888);
            Server.Start();
            Client = await Server.AcceptTcpClientAsync();

            List<string> greetings = SetGreetings();

            var setUpData = await XTalk(greetings);

            int size = Convert.ToInt16(setUpData.Split(new char[] { ' ' })[0]);
            string name = setUpData.Split(new char[] { ' ' })[1];

            Swamp swamp = new(size);
            Naturalist naturalist = new(swamp, name);

            while (await XTalk(new List<string> { "press" }) != "q")
                NextStep(swamp, naturalist);


            Server.Stop();
        }

        async static Task<string> GetStartUpData()
        {
            var stream = Client.GetStream();

            StartUp(stream);

            var result = await ReceiveClientData(stream);

            return result;
        }

        async static Task<string> XTalk(List<string> messages)
        {
            var stream = Client.GetStream();

            if (messages.Count > 0)
            {
                var responce = GetResponce(messages);
                await stream.WriteAsync(responce);
            }

            var result = await ReceiveClientData(stream);

            return result;
        }

        async static void StartUp(NetworkStream stream)
        {
            List<string> greetings = SetGreetings();

            var responce = GetResponce(greetings);
            await stream.WriteAsync(responce);
        }

        async static Task<string> ReceiveClientData(NetworkStream stream)
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
        }
    }
}

