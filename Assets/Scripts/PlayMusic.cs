using UnityEngine;
using System.Collections;

public class PlayMusic : MonoBehaviour
{
    private AudioSource _audioSource;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();

    }
    public void PlaySong ()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

}

