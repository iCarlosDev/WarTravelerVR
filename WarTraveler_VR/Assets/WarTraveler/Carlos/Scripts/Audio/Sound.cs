using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    [SerializeField] private GroupType _groupTypeEnum;
    public enum GroupType
    {
        Music,
        Effect
    }

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    public bool loop;
    
    [HideInInspector]
    public AudioSource source;
}