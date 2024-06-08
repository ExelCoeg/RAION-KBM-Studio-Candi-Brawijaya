using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonScript : MonoBehaviour
{
    public Sprite[] moon;
    public SpriteRenderer spriteRenderer;
    public Transform moonTransform;
    public bool canRotate;
    void Update()
    {
        if (!DayManager.instance.isNight)
        {
            
        }else if(DayManager.instance.isNight){

        }
    }
    private IEnumerator ChangeWeight(float startValue, float endValue, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.rotation = Quaternion.Euler(0,0,Mathf.Lerp(startValue, endValue, elapsed / duration));
            transform.rotation = Quaternion.Euler(0,0,Mathf.Lerp(endValue, startValue, elapsed / duration));
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0,0,0);
    }
}
