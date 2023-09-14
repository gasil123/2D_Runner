using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelMenuScript : MonoBehaviour
{
    int levelunlocked;
    [SerializeField] private Button[] levelbuttons;
    [SerializeField] private GameObject[] LockIcons;
    [SerializeField] private GameObject mainMenuButtonGroup;
    [SerializeField] private GameObject LevelMenuGroup;
  
    bool isLevelMenu;
    // Start is called before the first frame update
  

    private void Start()
    {
        levelunlock();
    }
    public void resetProgress()
    {
        Debug.Log("progress resetted");
        PlayerPrefs.DeleteKey("levelunlocked");
        levelunlock();
    }

    public void levelunlock()
    {
        levelunlocked = PlayerPrefs.GetInt("levelunlocked", 1);

        for (int i = 1; i < levelbuttons.Length; i++)
        {
            levelbuttons[i].interactable = false;
            LockIcons[i].SetActive(true);
        }
        for (int i = 0; i < levelunlocked; i++)
        {
            levelbuttons[i].interactable = true;
            LockIcons[i].SetActive(false);
        }
    }

    public void ToggleLevelMenu()
    {
        isLevelMenu = !isLevelMenu;

        if (isLevelMenu)
        {
            mainMenuButtonGroup.SetActive(false);
            LevelMenuGroup.SetActive(true);
        }
        if (!isLevelMenu)
        {
            mainMenuButtonGroup.SetActive(true);
            LevelMenuGroup.SetActive(false);
        }
    }
}
