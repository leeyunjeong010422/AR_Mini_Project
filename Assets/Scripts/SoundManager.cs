using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource startBGM;
    [SerializeField] private AudioSource gunClip;
    [SerializeField] private AudioSource reloadClip;
    [SerializeField] private AudioSource birdDieClip;
    [SerializeField] private AudioSource birdMoveClip;
    [SerializeField] private AudioSource gameOverClip;
    [SerializeField] private AudioSource mouseClickClip;

    private float previousTime = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetStartBGM()
    {
        PlayBGM(startBGM);
    }

    public void PlayBGM(AudioSource bgm)
    {
        bgm.time = previousTime;
        bgm.loop = true;
        bgm.Play();
    }

    public void StopBGM()
    {
        previousTime = startBGM.time; //startBGM의 현재 시간 저장
        startBGM.Stop();
    }

    public void PlayGunSound()
    {
        gunClip.PlayOneShot(gunClip.clip);
    }

    public void PlayReloadSound()
    {
        reloadClip.PlayOneShot(reloadClip.clip);
    }

    public void PlayBirdDieSound()
    {
        birdDieClip.PlayOneShot(birdDieClip.clip);
    }

    public void PlayBirdMoveSound()
    {
        birdMoveClip.PlayOneShot(birdMoveClip.clip);
    }

    public void PlayGameOverSound()
    {
        gameOverClip.PlayOneShot(gameOverClip.clip);
    }

    public void PlayMouseClickSound()
    {
        mouseClickClip.PlayOneShot(mouseClickClip.clip);
    }

}
