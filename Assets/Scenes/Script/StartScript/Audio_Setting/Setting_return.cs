using UnityEngine;
using UnityEngine.SceneManagement;


public class Audio_Return_Button : MonoBehaviour
{
    public void ReturnButton()
    {
        SceneManager.LoadScene("Setting");
    }
}
