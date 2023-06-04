using TMPro;
using UnityEngine;
public class Timer : MonoBehaviour
{
    [SerializeField] private float _timeRemaining = 300f;
    [SerializeField] private bool _timerIsRunning;
    [SerializeField] private TextMeshProUGUI _timerTMP;

    private void Awake()
    {
        _timerTMP = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        // Starts the timer automatically
        _timerIsRunning = true;
    }
    
    void Update()
    {
        if (!_timerIsRunning) return;
        
        if (_timeRemaining > 0)
        {
            _timeRemaining -= Time.deltaTime;
            DisplayTime(_timeRemaining);
        }
        else
        {
            Debug.Log("Time has run out!");
            _timeRemaining = 0;
            _timerIsRunning = false;
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        _timerTMP.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}