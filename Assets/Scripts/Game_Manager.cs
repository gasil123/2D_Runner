using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] coins;
    public static Game_Manager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        timescalePlay();
    }
    private void Start()
    {
        foreach(GameObject coin in coins)
        {
            //if(coin.GetComponent<>)
        }
    }
    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex > 1)
        {
            Destroy(GameObject.FindWithTag("GameManager"));
        }
    }
    private void timescalePause()
    {
        Time.timeScale = 0;
    }
    private void timescalePlay()
    {
        Time.timeScale = 1;
    }
}
