using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    //버튼 연결
    [Header("Buttons")]
    [SerializeField] private Button StageButton;
    [SerializeField] private Button CreditButton;
    [SerializeField] private Button GalleryButton;
    [SerializeField] private Button SettingButton;
    [SerializeField] private Button QuitButton;

    //씬 지정
    [SerializeField] private string StageScene = "Stage";
    [SerializeField] private string CreditScene = "Credit";
    [SerializeField] private string GalleryScene = "Gallery";
    [SerializeField] private string SettingScene = "Setting";

    //기능
    private void Awake()
    {
        StageButton.onClick.AddListener(Stage);
        CreditButton.onClick.AddListener(Credit);
        GalleryButton.onClick.AddListener(OpenGallery);
        SettingButton.onClick.AddListener(OpenSetting);
        QuitButton.onClick.AddListener(QuitGame);
    }

    private void Stage()
    {
        SceneManager.LoadScene(StageScene);
    }

    private void Credit()
    {
        SceneManager.LoadScene(CreditScene);
    }

    private void OpenGallery()
    {
        SceneManager.LoadScene(GalleryScene);
    }

    private void OpenSetting()
    {
        SceneManager.LoadScene(SettingScene);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}