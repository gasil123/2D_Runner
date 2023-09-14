using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject Settings;

    bool switchMenuSettings = true;

    public void SwitchMenuOrSettings()
    {
        switchMenuSettings = !switchMenuSettings;

        if (switchMenuSettings)
        {
            Menu.SetActive(true);
            Settings.SetActive(false);
        }
        if (!switchMenuSettings)
        {
            Menu.SetActive(false);
            Settings.SetActive(true);
        }
    }

}
