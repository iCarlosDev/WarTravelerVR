using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    public static PlayerPrefsManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("TrincherasScore", GameManager.instance.TrincherasScore);
        PlayerPrefs.SetInt("TrincherasMaxScore", GameManager.instance.TrincherasMaxScore);
        
        PlayerPrefs.SetInt("AntiaereoScore", GameManager.instance.AntiaereoScore);
        PlayerPrefs.SetInt("AntiaereoMaxScore", GameManager.instance.AntiaereoMaxScore);
    }

    public void LoadData()
    {
        GameManager.instance.TrincherasScore = PlayerPrefs.GetInt("TrincherasScore");
        GameManager.instance.TrincherasMaxScore = PlayerPrefs.GetInt("TrincherasMaxScore");
        
        GameManager.instance.AntiaereoScore = PlayerPrefs.GetInt("AntiaereoScore");
        GameManager.instance.AntiaereoMaxScore = PlayerPrefs.GetInt("AntiaereoMaxScore");
    }
}
