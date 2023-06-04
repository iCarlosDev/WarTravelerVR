using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("--- TRINCHERAS ---")]
    [Space(10)]
    [SerializeField] private int _trincherasScore;
    [SerializeField] private int _trincherasMaxScore;
    
    [Header("--- ANTIAEREO ---")]
    [Space(10)]
    [SerializeField] private int _antiaereoScore;
    [SerializeField] private int _antiaereoMaxScore;
    
    //GETTERS && SETTERS//
    public int TrincherasScore => _trincherasScore;
    public int TrincherasMaxScore => _trincherasMaxScore;

    public int AntiaereoScore => _antiaereoScore;
    public int AntiaereoMaxScore => _antiaereoMaxScore;

    /////////////////////////////////////////
    
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

    #region - TRINCHERAS -

    public void UpdateTrincherasScore(int newScore)
    {
        _trincherasScore = newScore;
        
        if (_trincherasScore > _trincherasMaxScore) UpdateTrincherasMaxScore();
    }
    
    private void UpdateTrincherasMaxScore()
    {
        _trincherasMaxScore = _trincherasScore;
    }

    #endregion

    #region - ANTIAEREO -

    public void UpdateAntiaereoScore(int newScore)
    {
        _antiaereoScore = newScore;
        
        if (_antiaereoScore > _antiaereoMaxScore) UpdateAntiaereoMaxScore();
    }

    private void UpdateAntiaereoMaxScore()
    {
        _antiaereoMaxScore = _antiaereoScore;
    }

    #endregion
}
