using CrockySwamp;
using System.Net;
using System.Net.Sockets;
using System.Text;

List<string> messages = new List<string>();

var server = new TcpListener(IPAddress.Parse("192.168.31.69"), 8888);
server.Start();

var setUpData = await GetStartUpData(server);

int size = Convert.ToInt16(setUpData.Split(new char[] { ' ' })[0]);
string name = setUpData.Split(new char[] { ' ' })[1];

Swamp swamp = new(size);
Naturalist naturalist = new(swamp, name);

while ((await XTalk(server) != "q"));
    NextStep(swamp, naturalist);


server.Stop();


async Task<string> GetStartUpData(TcpListener server)
{
    using var tcpClient = await server.AcceptTcpClientAsync();
    var stream = tcpClient.GetStream();

    StartUp(stream);

    var result = await ReceiveClientData(stream);

    return result;
}

async Task<string> XTalk(TcpListener server)
{
    using var tcpClient = await server.AcceptTcpClientAsync();
    var stream = tcpClient.GetStream();
    var result = await ReceiveClientData(stream);

    return result;
}

async void StartUp(NetworkStream stream)
{
    List<string> greetings = SetGreetings();
    var responce = GetResponce(greetings);
    await stream.WriteAsync(responce);
}

async Task<string> ReceiveClientData(NetworkStream stream)
{
    int bytesRead = 10;
    List<byte> query = new List<byte>();

    while ((bytesRead = stream.ReadByte()) != '#')
        query.Add((byte)bytesRead);

    var data = Encoding.UTF8.GetString(query.ToArray());

    return data;
}

byte[] GetResponce(List<string> data)
{
    StringBuilder responce = new StringBuilder();

    foreach (var message in data)
        responce.Append(message);

    return Encoding.UTF8.GetBytes(responce.ToString());
}

List<string> SetGreetings()
{
    List<string> result = new List<string>();

    result.Add("Hello! There will be a swamp with corcks and frogs! And naturalist!@");
    result.Add("Please, input swamp size and naturalist's name devided by space.@" );
    result.Add("#@");

    return result;
}

void NextStep(Swamp swamp, Naturalist naturalist)
{
    Console.Clear();
    swamp.Move();
    naturalist.Observe();
}

