using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuChange : MonoBehaviour
{
    [SerializeField] private Button exitBtn;
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button gameBtn;
    void Start()
    {
        gameBtn.onClick.AddListener(GameScene);
        settingsBtn.onClick.AddListener(SettingsScene);
        exitBtn.onClick.AddListener(Exit);
    }

    private void GameScene()
    {
        SceneManager.LoadScene(1);
    }

    private void SettingsScene()
    {
        throw new System.NotImplementedException();
    }

    private void Exit()
    {
        throw new System.NotImplementedException();
    }
}
