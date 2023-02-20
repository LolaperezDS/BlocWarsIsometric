using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
    public const int LARGE_BUFFER_SIZE = 65536;

    public string Connect(string pd)
    {
        // INITIALIZATION
        client = new TcpClient(host, port);
        client.ReceiveBufferSize = LARGE_BUFFER_SIZE;
        client.SendBufferSize = LARGE_BUFFER_SIZE;
        stream = client.GetStream();
        stream.SetLength(LARGE_BUFFER_SIZE);
        Debug.Log("������ �� ����������� ���������");
        SendMessageAsync(pd);
        // ��� ����������, ������ ���������� BoardStatement � Order => (PlayerInstance)
        Debug.Log("���������� � ����� �������");
        isConnected = true;
        return RecieveHandler(LARGE_BUFFER_SIZE);
    }


    private void Update()
    {
        if (isConnected)
        {
            if (task == null) task = RecieveMessageAsync();
            isConnected = client.Connected;
            if (task.Status == TaskStatus.RanToCompletion)
            {
                ActionHandler.ApplyAction(ActionWrapper.Unwrap(task.Result));
                task = RecieveMessageAsync();
            }
        }
    }


    // Called from ActionHandler
    public void SendPack(string pack)
    {
        SendMessageAsync(pack);
    }

    private async void SendMessageAsync(string message, int buffSize = BUFFER_STANDART_SIZE)
    {
        Task taskn = Task.Run(() => SendHandler(message, buffSize));
        await taskn;
    }

    private void SendHandler(string message, int buffSize)
    {
        byte[] msg = new byte[buffSize];
        msg = Encoding.Default.GetBytes(message);  // ������������ ������ � ������ ����

        stream.Write(msg, 0, msg.Length);     // ���������� ���������
    }


    private async Task<String> RecieveMessageAsync(int buffSize = BUFFER_STANDART_SIZE)
    {
        return await Task<String>.Run(() => RecieveHandler(buffSize));
    }


    private string RecieveHandler(int buffSize)
    {
        byte[] msg = new byte[buffSize];     // ������� ����� ��� �������� ���������
        int count = stream.Read(msg, 0, msg.Length);   // ������ ��������� �� �������
        return Encoding.Default.GetString(msg, 0, count); // ������� �� ����� ���������� ��������� � ���� �����
    }
}
