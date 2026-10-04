using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingButtons : MonoBehaviour
{
    // 버튼 연결
    [Header("Buttons")]
    [SerializeField] private Button GrapicButton;
    [SerializeField] private Button SoundButton;
    [SerializeField] private Button KeyBindingButton;
    [SerializeField] private Button ReturnButton;

    // 씬 지정
    [Header("Scenes")]
    [SerializeField] private string GrapicSettingScene = "GrapicSet";
    [SerializeField] private string SoundSettingScene = "SoundSet";
    [SerializeField] private string KeyBindingScene = "KeyBinding";
    [SerializeField] private string ReturnScene = "Main_Menu";


    // 버튼 기능 연결
    private void Awake()
    {
        GrapicButton.onClick.AddListener(GrapicSettingGo);
        SoundButton.onClick.AddListener(SoundSettingGo);
        KeyBindingButton.onClick.AddListener(KeyBindingGo);
        ReturnButton.onClick.AddListener(ReturnGo);
    }


    // 그래픽 설정
    private void GrapicSettingGo()
    {
        SceneManager.LoadScene(GrapicSettingScene);
    }


    // 사운드 설정
    private void SoundSettingGo()
    {
        SceneManager.LoadScene(SoundSettingScene);
    }


    // 키 설정
    private void KeyBindingGo()
    {
        SceneManager.LoadScene(KeyBindingScene);
    }


    // 메인 메뉴로 돌아가기
    private void ReturnGo()
    {
        SceneManager.LoadScene(ReturnScene);
    }
}