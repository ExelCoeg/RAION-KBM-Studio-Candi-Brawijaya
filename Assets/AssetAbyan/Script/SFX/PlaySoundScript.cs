using System.Collections;
using System.Collections.Generic;
using SHG.SoundManager;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundScript : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] SoundType soundType;
    [SerializeField,Range(0f, 1f)] float volume = 1;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    

    [Header("setUpDistance")]
    [SerializeField] int distance = 10;
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start() {
        audioSource.spatialBlend = 1;
        audioSource.maxDistance = distance;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
    }
    [ContextMenu("Play")]
    public void PlaySound(int index = 0)
    {
        // Debug.LogWarning(SoundManager.PlaySound(soundType, volume, index).name);
        audioSource.PlayOneShot(SoundManager.getClip(soundType, index),volume);
    }
}
