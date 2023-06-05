using UnityEngine;

public class Trincheras_LevelManager : MonoBehaviour
{
    [Header("--- OTHER SCRIPTS ---")]
    [Space(10)]
    [SerializeField] private MiniAntiaereoScore _miniAntiaereoScore;
    [SerializeField] private TrincherasScore _trincherasScore;
    [SerializeField] private Transform _player;

    [Header("--- PLAYER SPAWNS ---")]
    [Space(10)]
    [SerializeField] private Transform[] _playerSpawnsArray;

    private void Awake()
    {
        _miniAntiaereoScore = FindObjectOfType<MiniAntiaereoScore>();
        _trincherasScore = FindObjectOfType<TrincherasScore>();
        _player = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Start()
    {
        AudioManager.instance.Play("LobbyTheme"); //Se inicia la música del Lobby;
        
        //Se actualizan los puntos de los juegos;
        _miniAntiaereoScore.UpdateScore();
        _trincherasScore.UpdateScore();
        
        ChoosePlayerSpawn();
    }

    /// <summary>
    /// Método que escoge donde spawneará el player;
    /// </summary>
    private void ChoosePlayerSpawn()
    {
        _player.position = _playerSpawnsArray[GameManager.instance.PlayerSpawnIndex].position;
        _player.rotation = _playerSpawnsArray[GameManager.instance.PlayerSpawnIndex].localRotation;
    }
}
