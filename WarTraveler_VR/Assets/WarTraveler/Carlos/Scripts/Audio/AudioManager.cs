using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;
    public static AudioManager instance;

    /// <summary>
    /// Método para no destruir entre escenas este objeto;
    /// </summary>
    private void DontDestroy()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }
    
    private void Awake()
    {
        DontDestroy();
        
        foreach (Sound s in sounds)
        {
           s.source = gameObject.AddComponent<AudioSource>();
           s.source.clip = s.clip;

           s.source.volume = s.volume;
           s.source.pitch = s.pitch;
           s.source.loop = s.loop;
           s.source.playOnAwake = s.playOnAwake;
        }
    }

    private void Start()
    {
        Play("MainTheme");
    }

    /// <summary>
    /// Método para iniciar un sonido por el nombre que tenga en la lista de sonidos;
    /// </summary>
    /// <param name="name"></param>
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    /// <summary>
    /// Método para iniciar un sonido por el nombre que tenga en la lista de sonidos;
    /// </summary>
    /// <param name="name"></param>
    public void PlayOneShot(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.PlayOneShot(s.source.clip);
    }
    
    /// <summary>
    /// Método para parar un sonido por el nombre que tenga en la lista de sonidos;
    /// </summary>
    /// <param name="name"></param>
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }
}