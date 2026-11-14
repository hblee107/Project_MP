using UnityEngine;
using TMPro;
public class Grpic : MonoBehaviour
{
    public TMP_Dropdown RatioDropdown;

    public void ChangeRatio()
    {
        switch (RatioDropdown.value)
        {
            case 0:
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                break;

            case 1:
                Screen.SetResolution(1920, 1200, Screen.fullScreen);
                break;

            case 2:
                Screen.SetResolution(1440, 1080, Screen.fullScreen);
                break;

            case 3:
                Screen.SetResolution(2560, 1080, Screen.fullScreen);
                break;
        }
    }
}
