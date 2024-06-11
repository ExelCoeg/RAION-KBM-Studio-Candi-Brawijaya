using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MoonScript : MonoBehaviour
{
    public Sprite[] moonSprite;
    public GameObject moon;
    public Transform moonTransform;
    [SerializeField] float moonStart;
    [SerializeField] float moonEnd;
    public bool canRotate = true;
    public int moonCount = -1;
    private void Start() {
        moonCount = -1; 
    }
    void Update()
    {
        moonTransform.rotation = Quaternion.Euler(0,0,0);
        if (DayManager.instance.isNight && canRotate)
        {
            StartCoroutine(RotateMoon(moonStart,moonEnd, DayManager.instance.nightTime));
            canRotate = false;
        }else if(!DayManager.instance.isNight && !canRotate){
            canRotate = true;
        }
        ChangeMoon();
    }
    private void ChangeMoon(){
        if (moonCount != DayManager.instance.dayCount && moonCount < moonSprite.Length-1)
        {
            Debug.Log("Changeeeeeee" + moonCount);
            moonCount = DayManager.instance.dayCount;
            moon.GetComponent<Light2D>().lightCookieSprite = moonSprite[moonCount];
        }
    }
    private IEnumerator RotateMoon(float startValue, float endValue, float duration)
    {
        Debug.Log("rotateee");
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.rotation = Quaternion.Euler(0,0,Mathf.Lerp(startValue, endValue, elapsed / duration));
            //moonTransform.rotation = Quaternion.Euler(0,0,Mathf.Lerp(startValue, endValue, elapsed / duration));
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0,0,0);
    }
}
