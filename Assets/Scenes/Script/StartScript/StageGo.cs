using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageGo : MonoBehaviour
{
    //버튼 연결
    [Header("Stage_Button")]
    [SerializeField] private Button Stage1Button;

    //씬 지정
    [SerializeField] private string StageScene = "Stage1";


    //기능
    private void Awake()
    {
        Stage1Button.onClick.AddListener(Stage1);
    }

    private void Stage1()
    {
        SceneManager.LoadScene(StageScene);
    }
}