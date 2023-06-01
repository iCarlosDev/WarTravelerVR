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

    public void CallAntiaereoScene()
    {
        if (IsInvoking(nameof(LoadAntiaereoScene))) return;
        
        Invoke(nameof(LoadAntiaereoScene), 1.5f);
    }
    
    private void LoadAntiaereoScene()
    {
        SceneManager.LoadScene(1);
    }
}
