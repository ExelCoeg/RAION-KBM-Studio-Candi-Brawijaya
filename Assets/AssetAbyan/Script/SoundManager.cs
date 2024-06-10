    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SoundType{
    Atack,
    FootStep,
    Hit,
}
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundlist;
    public static SoundManager instance;
    private AudioSource audioSource;

    private void Awake() {
        if (instance == null){
            instance = this;
        }
    }
    public static void PlaySoung(SoundType soundType, float volume){
        instance.audioSource.PlayOneShot(instance.soundlist[(int)soundType], volume);
    }
}
