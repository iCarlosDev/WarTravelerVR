using UnityEngine;

public class Trincheras_LevelManager : MonoBehaviour
{
    [SerializeField] private MiniAntiaereoScore _miniAntiaereoScore;

    private void Awake()
    {
        _miniAntiaereoScore = FindObjectOfType<MiniAntiaereoScore>();
    }

    private void Start()
    {
        PlayerPrefsManager.instance.LoadData();
        
        _miniAntiaereoScore.UpdateScore();
    }
}
