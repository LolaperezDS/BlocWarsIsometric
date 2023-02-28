using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConnectDataParser : MonoBehaviour
{
    [SerializeField] private Button connectBtn;
    [SerializeField] private InputField nick;
    [SerializeField] private InputField ip;
    [SerializeField] private InputField port;


    void Start()
    {
        connectBtn.onClick.AddListener(ConnectToServer);
    }

    private void ConnectToServer()
    {
        GlobalSettings.NickName = nick.text;
        GlobalSettings.HostIP = ip.text;
        GlobalSettings.HostPort = System.Convert.ToInt32(port.text);

        SceneManager.LoadScene(2);
    }
}
