using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Level_number : MonoBehaviour
{
    private TextMeshProUGUI Leveltmpro;

    private void Awake()
    {
        Leveltmpro = GetComponent<TextMeshProUGUI>();
        Leveltmpro.text = "Level "+ SceneManager.GetActiveScene().buildIndex.ToString();
    }

}
