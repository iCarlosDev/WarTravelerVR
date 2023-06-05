using TMPro;
using UnityEngine;

public class TrincherasScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _lastGameScoreTMP;
    [SerializeField] private TextMeshProUGUI _maxScoreTMP;

    public void UpdateScore()
    {
        _lastGameScoreTMP.text = $"{GameManager.instance.TrincherasScore}";
        _maxScoreTMP.text = $"{GameManager.instance.TrincherasMaxScore}";
    }
}
