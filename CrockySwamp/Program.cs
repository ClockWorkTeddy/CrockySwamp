using CrockySwamp;
using System.Net;
using System.Net.Sockets;
using System.Text;

bool firstStep = true;
List<string> messages = new List<string>();
bool Ongoing = true;

while (true)
{
    int size = 0;
    string name = "";

    if (firstStep)
    {
        var data = await GetClientData();
        size = Convert.ToInt16(data[0]);
        name = data[1];

        firstStep = false;
    }


    Swamp swamp = new(size);
    Naturalist naturalist = new(swamp, name);

    while (Ongoing)
        NextStep(swamp, naturalist);
}

async Task<string[]> GetClientData()
{
    var server = new TcpListener(IPAddress.Parse("192.168.31.243"), 8888);
    server.Start();

    using var tcpClient = await server.AcceptTcpClientAsync();
    var stream = tcpClient.GetStream();

    SetGreetings();
    var responce = GetResponce();
    await stream.WriteAsync(responce);

    int bytesRead = 10;
    List<byte> query = new List<byte>();

    while ((bytesRead = stream.ReadByte()) != '#')
        query.Add((byte)bytesRead);

    var setUpParams = Encoding.UTF8.GetString(query.ToArray());

    server.Stop();
    return setUpParams.Split(new char[] { ' ' });
}

byte[] GetResponce()
{
    StringBuilder responce = new StringBuilder();

    foreach (var message in messages)
        responce.Append(message);

    return Encoding.UTF8.GetBytes(responce.ToString());
}

void SetGreetings()
{
    messages.Add("Hello! There will be a swamp with corcks and frogs! And naturalist!@");
    messages.Add("Please, input swamp size and naturalist's name devided by space.@" );
    messages.Add("#@");
}

void NextStep(Swamp swamp, Naturalist naturalist)
{
    Console.Clear();
    swamp.Move();
    naturalist.Observe();
    char inputKey = Console.ReadKey().KeyChar;

    if (inputKey == 'q')
        Ongoing= false;
}

