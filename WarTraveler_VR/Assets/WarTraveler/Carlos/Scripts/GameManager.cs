using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
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

    private void Start()
    {
        Application.targetFrameRate = 1000;
        //QualitySettings.vSyncCount = 0;
    }

    public void CallAntiaereoScene()
    {
        if (IsInvoking(nameof(LoadAntiaereoScene))) return;
        
        TransitionsManager.instance.Fade_IN();
        Invoke(nameof(LoadAntiaereoScene), 1.5f);
    }
    
    private void LoadAntiaereoScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
