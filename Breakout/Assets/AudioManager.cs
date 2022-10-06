using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] internal GameManager gameManager;
    [SerializeField] private AudioSource audioSource;
    [SerializeField]private List<Sound> Sounds = new List<Sound>();

    private void Awake()
    {
        LoadSound("paddle_hit");
        LoadSound("wall_hit");
        LoadSound("brick-hit-2");
    }

    public void PlaySound (string name)
    {
        Sound toPlay = GetSound(name);
        audioSource.PlayOneShot(toPlay.clip);
    }

    private Sound GetSound (string name)
    {
        for (int i = 0; i < Sounds.Count; i++)
            if (Sounds[i].name == name) return Sounds[i];
        Debug.LogError($"Sound {name} doesnt exist");
        return new Sound();
    }
     
    public void LoadSound (string name)
    {
        Sound sound;
        sound.name = name;
        sound.clip = Resources.Load<AudioClip>($"Sounds/{name}");
        Assert.Equals(sound.clip != null, $"Sound {name} failed to load");
        Sounds.Add(sound);
    }

    [Serializable] public struct Sound
    {
        public string name;
        public AudioClip clip;

        public bool IsNull ()
        {
            return clip == null;
        }
    }
}