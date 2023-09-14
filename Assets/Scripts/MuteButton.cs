using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    [SerializeField] private Toggle muteButton;
    private bool isMuted = false;

    private void Start()
    {
        isMuted = PlayerPrefs.GetInt("IsMuted", 0) == 1;

        UpdateAudioState();
    }

    private void UpdateAudioState()
    {
        AudioListener.volume = isMuted ? 0f : 1f;

        // Change the button's text to reflect the current audio state
        Text buttonText = muteButton.GetComponentInChildren<Text>();
        buttonText.text = isMuted ? "Unmute" : "Mute";
    }

    public void ToggleAudio()
    {
        isMuted = !isMuted;
        UpdateAudioState();

        PlayerPrefs.SetInt("IsMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }
}
