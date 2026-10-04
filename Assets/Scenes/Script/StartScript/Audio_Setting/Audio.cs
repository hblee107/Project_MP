
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Volume Sliders")]
    [SerializeField] private Slider MasterSound;
    [SerializeField] private Slider SFXSound;
    [SerializeField] private Slider BGMSound;

    [Header("Mixer Parameter Names")]
    [SerializeField] private string MasterParameter = "Master Volume";
    [SerializeField] private string SFXParameter = "SFX Volume";
    [SerializeField] private string BGMParameter = "BGM Volume";

    private void Start()
    {
        // 저장된 볼륨 불러오기
        float master = PlayerPrefs.GetFloat("MasterVolume", 1f);
        float sfx = PlayerPrefs.GetFloat("SFXVolume", 1f);
        float bgm = PlayerPrefs.GetFloat("BGMVolume", 1f);

        // 슬라이더에 값 적용
        MasterSound.value = master;
        SFXSound.value = sfx;
        BGMSound.value = bgm;

        // 슬라이더 이벤트 자동 연결
        MasterSound.onValueChanged.AddListener(SetMasterVolume);
        SFXSound.onValueChanged.AddListener(SetSFXVolume);
        BGMSound.onValueChanged.AddListener(SetBGMVolume);

        // 처음 실행할 때도 볼륨 적용
        SetMasterVolume(master);
        SetSFXVolume(sfx);
        SetBGMVolume(bgm);
    }

    public void SetMasterVolume(float volume)
    {
        SetVolume(MasterParameter, volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        SetVolume(SFXParameter, volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetBGMVolume(float volume)
    {
        SetVolume(BGMParameter, volume);
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    // 0~1 값을 데시벨로 변환
    private void SetVolume(string parameter, float volume)
    {
        if (volume <= 0.0001f)
        {
            audioMixer.SetFloat(parameter, -80f);
        }
        else
        {
            audioMixer.SetFloat(parameter, Mathf.Log10(volume) * 20f);
        }
    }
}