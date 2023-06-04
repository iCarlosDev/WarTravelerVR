using System;
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
        _miniAntiaereoScore.UpdateScore();
    }
}
