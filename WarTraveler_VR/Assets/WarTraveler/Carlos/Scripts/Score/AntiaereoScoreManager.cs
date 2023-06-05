using TMPro;
using UnityEngine;

public class AntiaereoScoreManager : MonoBehaviour
{
    public static AntiaereoScoreManager instance;
    
    [Header("--- CURRENT SCORE ---")]
    [Space(10)]
    [SerializeField] private TextMeshProUGUI _scoreTMP;
    [SerializeField] private int _currentScore;

    [Header("--- SCORE GETTED ---")]
    [Space(10)]
    [SerializeField] private Transform _positionToSpawn;
    [SerializeField] private TextMeshProUGUI _scoreGettedPrefab;

    //GETTERS && SETTERS//
    public int CurrentScore => _currentScore;

    ///////////////////////////////////////////
    
    private void Awake()
    {
        instance = this;

        _scoreTMP = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        _currentScore = 0;
    }

    #region - DEBUG -
    
    [ContextMenu("Add Score")]
    private void AddScore_DEBUG()
    {
        AddScore(100);
    }
    
    #endregion
    
    /// <summary>
    /// Método para añadir puntuación a la puntuación total;
    /// </summary>
    /// <param name="scoreToAdd"></param>
    public void AddScore(int scoreToAdd)
    {
        //Se suma la puntuación conseguida a la total y se actualiza la UI;
        _currentScore += scoreToAdd;
        _scoreTMP.text = $"{_currentScore}";
        
        ShowScoreInGame(scoreToAdd);
    }

    /// <summary>
    /// Método para enseñar en UI la puntuación conseguida;
    /// </summary>
    /// <param name="scoreGetted"></param>
    private void ShowScoreInGame(int scoreGetted)
    {
        //Se instancia la puntuación conseguida; y se muestra en UI;
        TextMeshProUGUI score = Instantiate(_scoreGettedPrefab, _positionToSpawn);
        score.text = $"+{scoreGetted}";
        
        //La destruimos después de 2.5s;
        Destroy(score.gameObject, 2.5f);
    }
}
