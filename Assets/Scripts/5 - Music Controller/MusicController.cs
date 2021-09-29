using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField]
    private PuzzleGameSaver puzzleGameSaver;

    private AudioSource bgMusicClip;

    private float musicVolumn;

    private void Awake()
    {
        GetAudioSource();
    }

    private void Start()
    {
        musicVolumn = puzzleGameSaver.musicVolume;
        PlayOrTurnOffMusic(musicVolumn);
    }

    void GetAudioSource()
    {
        bgMusicClip = GetComponent<AudioSource>();
    }

    public void SetMusicVolumn(float volumn)
    {
        PlayOrTurnOffMusic(volumn);
    }

    void PlayOrTurnOffMusic(float volumn)
    {
        this.musicVolumn = volumn;
        bgMusicClip.volume = musicVolumn;

        if(bgMusicClip.volume > 0)
        {
            if (!bgMusicClip.isPlaying)
            {
                bgMusicClip.Play();
            }

            puzzleGameSaver.musicVolume = musicVolumn;
            puzzleGameSaver.SaveGameData();
        }
        else if(bgMusicClip.volume == 0)
        {
            if (bgMusicClip.isPlaying)
            {
                bgMusicClip.Stop();
            }

            puzzleGameSaver.musicVolume = musicVolumn;
            puzzleGameSaver.SaveGameData();
        }
    }

    public float GetMusicVolumn()
    {
        return this.musicVolumn;
    }

}
