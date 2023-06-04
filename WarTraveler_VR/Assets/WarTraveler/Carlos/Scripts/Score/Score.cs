using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score instance;
    
    [Header("--- CURRENT SCORE ---")]
    [Space(10)]
    [SerializeField] private TextMeshProUGUI _scoreTMP;
    [SerializeField] private int _currentScore;

    [Header("--- SCORE GETTED ---")]
    [Space(10)]
    [SerializeField] private Transform _positionToSpawn;
    [SerializeField] private TextMeshProUGUI _scoreGettedPrefab;
    
    private void Awake()
    {
        instance = this;

        _scoreTMP = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        _currentScore = 0;
    }

    [ContextMenu("Add Score")]
    private void AddScore_DEBUG()
    {
        AddScore(100);
    }
    
    public void AddScore(int scoreToAdd)
    {
        _currentScore += scoreToAdd;
        _scoreTMP.text = $"{_currentScore}";
        
        ShowScoreInGame(scoreToAdd);
    }

    private void ShowScoreInGame(int scoreGetted)
    {
        TextMeshProUGUI score = Instantiate(_scoreGettedPrefab, _positionToSpawn);
        score.text = $"+{scoreGetted}";
        
        Destroy(score.gameObject, 2.5f);
    }
}
