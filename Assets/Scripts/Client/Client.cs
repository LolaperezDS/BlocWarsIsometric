using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System;
using System.Threading.Tasks;

public class Client : MonoBehaviour
{
    public bool isConnected { get; protected set; } = false;
    [SerializeField] private int port;
    [SerializeField] private string host;

    private NetworkStream stream;
    private TcpClient client;
    private Task<string> task;

    public const int BUFFER_STANDART_SIZE = 1024;
    public const int LARGE_BUFFER_SIZE = 2 * 65536;

    public PlayerInitialization Connect(PlayerData pd)
    {
        // INITIALIZATION
        client = new TcpClient(host, port);
        stream = client.GetStream();

        Task t = SendMessageAsync(JsonUtility.ToJson(pd));
        t.Wait();

        // ѕри соединении, сервер отправл€ет BoardStatement и Order => (PlayerInstance)
        task = RecieveMessageAsync();
        task.Wait();
        return JsonUtility.FromJson<PlayerInitialization>(task.Result);
    }


    private void Update()
    {
        if (isConnected)
        {
            isConnected = client.Connected;
            task = RecieveMessageAsync();
            if (task.Status == TaskStatus.RanToCompletion)
            {
                // ѕолучен пакет

                // ActionHandler.Handle(task.Result);

                task = RecieveMessageAsync();
            }
        }
    }


    // Called from ActionHandler
    public void SendPack(string pack)
    {
        SendMessageAsync(pack);
    }

    private async Task SendMessageAsync(string message, int buffSize = BUFFER_STANDART_SIZE)
    {
        await Task.Run(() => SendHandler(message, buffSize));
    }

    private void SendHandler(string message, int buffSize)
    {
        byte[] msg = new byte[buffSize];
        msg = Encoding.Default.GetBytes(message);  // конвертируем строку в массив байт

        stream.Write(msg, 0, msg.Length);     // отправл€ем сообщение
    }


    private async Task<String> RecieveMessageAsync(int buffSize = BUFFER_STANDART_SIZE)
    {
        return await Task<String>.Run(() => RecieveHandler(buffSize));
    }


    private string RecieveHandler(int buffSize)
    {
        byte[] msg = new byte[buffSize];     // готовим место дл€ прин€ти€ сообщени€
        int count = stream.Read(msg, 0, msg.Length);   // читаем сообщение от клиента
        return Encoding.Default.GetString(msg, 0, count); // выводим на экран полученное сообщение в виде строк
    }
}
