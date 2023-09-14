using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject loadscreen;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI tmpro;
    float loadingprogress;
    float progressValue;
    
    private void Start()
    {
        if (tmpro!=null)
        {
            int level = PlayerPrefs.GetInt("levelunlocked",1);
            if (level <= 2 && tmpro)
            {
                tmpro.text = "Start";
            }
            else
                tmpro.text = "Continue";
        } 
    }
    public void QuitaPP()
    {
        Application.Quit();

    }
    public void timescalePause()
    {
        Time.timeScale = 0;
    }
    public void timescalePlay()
    {
        Time.timeScale = 1;
    }
    public void continuegame() 
    {
        int level =  PlayerPrefs.GetInt("levelunlocked",1);
        if (level < 1)
        {
            Loadscene(1);
        }
        else
        {
            Loadscene(level);
        }
        

    }
    public void LoadLevel(int buildindex)
    {
        StartCoroutine(Loadscene(buildindex));
    }
    public void LoadMainMenu()
    {
        StartCoroutine(Loadscene(0));
    }
    public void RestartLevel()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(Loadscene(index));
    }
    
    public void loadNextLevel()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(Loadscene(index+1));
    }
    IEnumerator Loadscene(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        loadscreen.SetActive(true);
        while (!operation.isDone)
        {
            loadingprogress = operation.progress;
            progressValue = Mathf.Clamp01(loadingprogress / 0.9f);
            slider.value = progressValue;
            yield return null;
        }
        if (operation.isDone)
        {
            loadscreen.SetActive(false);
        }
    }
}
