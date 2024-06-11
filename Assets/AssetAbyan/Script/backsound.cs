using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backsound : MonoBehaviour
{
    [SerializeField]bool isDay;
    [SerializeField]private AudioSource audioSource;
    public AudioClip audioClipDay;
    public AudioClip audioClipNight;
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (DayManager.instance.isNight && isDay)
        {
            Debug.Log("haiiiiiiiiiiiiiiiiiiiiiiiiiiiii Night Musikkkkkkk");
            PlayNight();
        }else if(!DayManager.instance.isNight && !isDay){
            Debug.Log("haiiiiiiiiiiiiiiiiiiiiiiiiiiiii Musik");
            PlayDay();
        }
    }
    void PlayDay(){
        audioSource.Stop();
        audioSource.PlayOneShot(audioClipDay,1);
        isDay = true;
    }
    void PlayNight(){
        audioSource.Stop();
        audioSource.PlayOneShot(audioClipNight,1);
        isDay = false;
    }
}
