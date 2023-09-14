using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager_Script : MonoBehaviour
{
    public AudioMixer audioMixer;

    private void Awake()
    {
        UnMutemaster();
    }
    public void setBGMLevel(float bgm_vol)
    {
        audioMixer.SetFloat("BGMParameter", bgm_vol/2f);
    } 
    public void setSFXSLevel(float sfx_vol)
    {
        audioMixer.SetFloat("SFXParameter", sfx_vol/2f);
    }
    public void Mutemaster()
    {
        audioMixer.SetFloat("MasterParameter", -80f);
    } 
    public void UnMutemaster()
    {
        audioMixer.SetFloat("MasterParameter", 0f);
    }

}
