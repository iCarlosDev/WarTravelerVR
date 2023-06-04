using UnityEngine;

public class Trincheras_LevelManager : MonoBehaviour
{
    [SerializeField] private MiniAntiaereoScore _miniAntiaereoScore;
    [SerializeField] private Transform _player;

    [SerializeField] private Transform[] _playerSpawnsArray;

    private void Awake()
    {
        _miniAntiaereoScore = FindObjectOfType<MiniAntiaereoScore>();
        _player = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Start()
    {
        AudioManager.instance.Play("LobbyTheme");
        
        _miniAntiaereoScore.UpdateScore();
        
        ChoosePlayerSpawn();
    }

    private void ChoosePlayerSpawn()
    {
        _player.position = _playerSpawnsArray[GameManager.instance.PlayerSpawnIndex].position;
        _player.rotation = _playerSpawnsArray[GameManager.instance.PlayerSpawnIndex].localRotation;
    }
}
