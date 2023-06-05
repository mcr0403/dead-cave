using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public AudioSource mainMusic, levelMusic, bossMusic;

    public AudioSource[] sfx;
    

    public void PlayMainMenuMusic()
    {
        levelMusic.Stop();
        bossMusic.Stop();

        mainMusic.Play();
    }

    public void PlayLevelMusic()
    {
        if (!levelMusic.isPlaying)
        {
            levelMusic.Play();
            mainMusic.Stop();
            bossMusic.Stop();

        }

    }

    public void PlayBossMusic()
    {

        Debug.Log("Boss");
        levelMusic.Stop();
        mainMusic.Stop();

        bossMusic.Play();
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();
    }

    

}
