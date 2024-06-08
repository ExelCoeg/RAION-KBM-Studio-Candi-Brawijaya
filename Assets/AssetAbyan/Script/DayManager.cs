using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class DayManager : MonoBehaviour
{
    public static DayManager instance;
    public GameObject globalVolume;
    [SerializeField] private int dayCount;
    [Header("TIME")]
    [SerializeField] public float clock;
    [SerializeField] private int timeToDay;
    [SerializeField] public int dayTime;
    [SerializeField] private int timeToNight;
    [SerializeField] private int nightTime;
    public bool isNight;
    private bool isToNight;
    private bool isToDay;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        SpawnManager.instance.WavesSet(dayCount);
    }

    void Update()
    {
        clock += Time.deltaTime;
        if (clock > dayTime + nightTime)
        {
            clock = 0;
            dayCount++;
            SpawnManager.instance.WavesSet(dayCount);
        }
        
        // Transisi ke malam
        if (!isToNight && clock > (dayTime - timeToNight/2) && !isNight)
        {
            isToNight = true;
            isToDay = false; // Reset isToDay flag
            StopAllCoroutines();
            StartCoroutine(ChangeWeight(0, 1, timeToNight));
        }

        // Transisi ke siang
        if (!isToDay && clock > (dayTime + nightTime - timeToDay/5) && isNight)
        {
            isToDay = true;
            isToNight = false; // Reset isToNight flag
            StopAllCoroutines();
            StartCoroutine(ChangeWeight(1, 0, timeToDay));
        }

        setNight();
    }

    private void setNight()
    {
        isNight = clock > dayTime;
    }

    private IEnumerator ChangeWeight(float startValue, float endValue, float duration)
    {
        float elapsed = 0f;
        Volume volume = globalVolume.GetComponent<Volume>();

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            volume.weight = Mathf.Lerp(startValue, endValue, elapsed / duration);
            yield return null;
        }

        volume.weight = endValue;
    }
}
