using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class Test : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [ContextMenu("Play")]
    public void play(){
        if (audioSource == null)
        {
            Debug.LogWarning("Audio Null");
            return;
        }
        audioSource.Play();
    }
}
