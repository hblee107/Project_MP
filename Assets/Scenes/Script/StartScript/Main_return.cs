using UnityEngine;
using UnityEngine.SceneManagement;


public class Main_Return : MonoBehaviour
{
    public void ReturnButton()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
