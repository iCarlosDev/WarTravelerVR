using System.Collections.Generic;
using UnityEngine;

public class TrincherasManager : MonoBehaviour
{
    public static TrincherasManager instance;

    [SerializeField] private PlayerScriptStorage _playerScriptStorage;

    [Header("--- SCORE ---")] 
    [SerializeField] private TrincherasScore _trincherasScore;
    [SerializeField] private int _score;

    [Header("--- TIMER ---")] 
    [SerializeField] private float _time;
    
    [Header("--- DIANAS ---")] 
    [SerializeField] private List<Animator> _dianasList;
    [SerializeField] private int _dianasListIndex;

    [Header("--- COLLIDERS INCREASE ---")] 
    [SerializeField] private List<BoxCollider> _collidersIncreaseList;
    
    [Header("--- BOX COLLIDERS ---")]
    [SerializeField] private BoxCollider[] _boxCollidersArray;
    [SerializeField] private bool _isFirstCollider;
    
    [Header("--- DOORS ---")]
    [SerializeField] private Animator[] _doorArray;

    [Header("--- TRINCHERAS PARAMS ---")]
    [SerializeField] private bool _gameStarted;
    
    [SerializeField] private float _timeToCheck;
    
    //GETTERS && SETTERS//
    public int Score
    {
        get => _score;
        set => _score = value;
    }

    //////////////////////////////////////
    
    private void Awake()
    {
        instance = this;

        _playerScriptStorage = FindObjectOfType<PlayerScriptStorage>();
        _trincherasScore = GetComponentInChildren<TrincherasScore>();
        _dianasList.AddRange(transform.GetChild(3).GetComponentsInChildren<Animator>());
        _collidersIncreaseList.AddRange(transform.GetChild(4).GetComponentsInChildren<BoxCollider>());
    }

    private void Start()
    {
        _isFirstCollider = true;
        
        _boxCollidersArray[1].enabled = false;
        _boxCollidersArray[0].enabled = true;

        foreach (Animator animator in _dianasList)
        {
            animator.GetComponent<TargetAnimationControl>().IsInTrincheras = true;
            animator.SetTrigger("TargetDown"); 
        }
        
        _timeToCheck = 0f;
        CloseDoors(0);
    }

    private void Update()
    {
        if (_gameStarted) _time += Time.deltaTime;

        _timeToCheck += Time.deltaTime;
        if (_timeToCheck < 0.3f) return;

        for (int i = 0; i < _doorArray.Length; i++)
        {
            _doorArray[i].ResetTrigger("Open");
            _doorArray[i].ResetTrigger("Close");
        }
        
        if (!_gameStarted)
        {
            if (CheckIfPlayerHasWeapons())
            {
                OpenDoors(1);
            }
            else
            {
                CloseDoors(1);
            }
            
            CloseDoors(2);
        }
        else
        {
            CloseDoors(1);
            OpenDoors(2);
        }
        
        _timeToCheck = 0f;
    }

    private bool CheckIfPlayerHasWeapons()
    {
        return _playerScriptStorage.PlayerInventory.WeaponsList.Count != 0 || _playerScriptStorage.TakePouchAmmo.GrabbedWeaponsList.Count != 0;
    }

    private void OpenDoors(int doorIndex)
    {
        switch (doorIndex)
        {
            case 0:
                _doorArray[0].SetTrigger("Open");
                _doorArray[1].SetTrigger("Open");
                break;
            case 1:
                _doorArray[0].SetTrigger("Open");
                break;
            case 2:
                _doorArray[1].SetTrigger("Open");
                break;
        }
    }

    private void CloseDoors(int doorIndex)
    {
        switch (doorIndex)
        {
            case 0:
                _doorArray[0].SetTrigger("Close");
                _doorArray[1].SetTrigger("Close");
                break;
            case 1:
                _doorArray[0].SetTrigger("Close");
                break;
            case 2:
                _doorArray[1].SetTrigger("Close");
                break;
        }
    }

    public void SetDianaUp()
    {
        _dianasList[_dianasListIndex].SetTrigger("TargetUp");
        _dianasListIndex++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (_isFirstCollider)
        {
            _gameStarted = true;
            
            _score = 0;
            
            _boxCollidersArray[0].enabled = false;
            _boxCollidersArray[1].enabled = true;

            _time = 0f;
            
            _isFirstCollider = false;
        }
        else
        {
            _gameStarted = false;
            _boxCollidersArray[1].enabled = false;
            _boxCollidersArray[0].enabled = true;

            foreach (BoxCollider dianaIncrease in _collidersIncreaseList)
            {
                dianaIncrease.gameObject.SetActive(true);
            }
            
            GameManager.instance.UpdateTrincherasScore(Score);
            _trincherasScore.UpdateScore();
        
            _dianasListIndex = 0;

            _isFirstCollider = true;
        }
    }
}
