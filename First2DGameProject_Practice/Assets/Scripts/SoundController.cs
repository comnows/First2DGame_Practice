using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController soundInstance;
    [SerializeField] AudioSource audioSystem;

    public AudioClip[] soundVFXList;    // 0 - Jump, 1 - Slash, 2 - PlayerHurt, 3 - EnemyHurt, 4 - EnemySpawn

    private void Awake() 
    {
        soundInstance = this;
    }

    public void PlaySoundEffect(int audioIndexToPlay)
    {
        audioSystem.PlayOneShot(soundVFXList[audioIndexToPlay]);
    }
}
