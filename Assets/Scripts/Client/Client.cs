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
    public const int LARGE_BUFFER_SIZE = 65536 * 5;

    public string Connect(string pd)
    {
        // INITIALIZATION
        client = new TcpClient(host, port);
        client.ReceiveBufferSize = LARGE_BUFFER_SIZE;
        stream = client.GetStream();
        Debug.Log("������ �� ����������� ���������");
        SendMessageAsync(pd);
        // ��� ����������, ������ ���������� BoardStatement � Order => (PlayerInstance)
        Debug.Log("���������� � ����� �������");
        isConnected = true;
        return RecieveHandler(LARGE_BUFFER_SIZE);
    }


    private void Update()
    {
        client.ReceiveBufferSize = BUFFER_STANDART_SIZE * 8;
        if (isConnected)
        {
            if (task == null) task = RecieveMessageAsync();
            isConnected = client.Connected;

            if (!isConnected) Debug.Log("�������� �� �������");

            if (task.Status == TaskStatus.RanToCompletion)
            {
                ActionHandler.ApplyAction(ActionWrapper.Unwrap(task.Result));
                Debug.Log("������� ������: " + task.Result);
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
        // byte[] msg = new byte[buffSize];
        // msg = Encoding.Default.GetBytes(message); // ������������ ������ � ������ ����
        //
        // stream.Write(msg, 0, msg.Length); // ���������� ���������
        
        byte[] msg = new byte[message.Length + 2];
        msg[0] = 0x00;
        Encoding.Default.GetBytes(message).CopyTo(msg, 1);  // ������������ ������ � ������ ����
        msg[message.Length + 2 - 1] = 0x01;
        stream.Write(msg, 0, msg.Length);     // ���������� ���������
    }


    private async Task<String> RecieveMessageAsync()
    {
        return await Task<String>.Run(() => RecieveHandler());
    }

    // private string RecieveHandler(int buffSize)
    // {
    //     int numberOfBytes = GetNumberOfBytes();
    //
    //     StringBuilder stringBuilder = new StringBuilder();
    //     byte[] bytes = new byte[buffSize];
    //     int length;
    //
    //     // Read incomming stream into byte arrary. 					
    //     while (stringBuilder.ToString().Length != numberOfBytes)
    //     {
    //         length = stream.Read(bytes, 0, bytes.Length);
    //         if (length == 0) break;
    //         int count = stringBuilder.Length;
    //         var incommingData = new byte[length];
    //         Array.Copy(bytes, 0, incommingData, 0, length);
    //         // Convert byte array to string message. 						
    //         stringBuilder.Append(Encoding.ASCII.GetString(incommingData));
    //         if (count == stringBuilder.Length) break;
    //     }
    //
    //     Debug.Log("Number of chars received: " + stringBuilder.Length + "\nMessage: " + stringBuilder);
    //
    //     return stringBuilder.ToString();
    // }
    
    private string RecieveHandler()
    {
        GetZeroByte();
            
        StringBuilder stringBuilder = new StringBuilder();
        byte[] msg = new byte[1];
            
        while (true)
        {
            int count = stream.Read(msg, 0, 1);
            if (count == 0)
            {
                // throw new Exception("Cannot read from tcp connection");
                continue;
            }

            if (msg[0] == 0x01)
            {
                break;
            }

            stringBuilder.Append(Encoding.Default.GetString(msg, 0, 1));
        }

        Debug.Log("�������� ���������:" + stringBuilder.ToString());
        return stringBuilder.ToString();
    }

    // private int GetNumberOfBytes()
    // {
    //     byte[] bytes = new byte[1024];
    //     stream.Read(bytes, 0, bytes.Length);
    //
    //     string numberOfBytes = Encoding.ASCII.GetString(bytes);
    //
    //     Debug.Log("Number of bytes we should receive: " + numberOfBytes);
    //
    //     return Convert.ToInt32(numberOfBytes);
    // }
    
    private void GetZeroByte()
    {
        byte[] bytes = new byte[1]; 
        while (true)
        {
            int count = stream.Read(bytes, 0, 1);
            if (count == 0)
            {
                //throw new Exception("Cannot read from tcp connection");
                continue;
            }

            if (bytes[0] == 0x00)
            {
                return;
            }
        }
    }
}