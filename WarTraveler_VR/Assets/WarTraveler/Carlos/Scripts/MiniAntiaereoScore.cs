using TMPro;
using UnityEngine;

public class MiniAntiaereoScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _lastGameScoreTMP;
    [SerializeField] private TextMeshProUGUI _maxScoreTMP;

    public void UpdateScore()
    {
        _lastGameScoreTMP.text = $"{GameManager.instance.AntiaereoScore}";
        _maxScoreTMP.text = $"{GameManager.instance.AntiaereoMaxScore}";
    }
}
