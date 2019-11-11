using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SunManager : MonoBehaviour
{
    [SerializeField]
    private Color DayColor;
    [SerializeField]
    private float DayTime;
    [SerializeField]
    private Color SunsetColor;
    [SerializeField]
    private float SunsetTime;
    [SerializeField]
    private Color NightColor;
    [SerializeField]
    private float NightTime;

    [SerializeField]
    private float Delay;

    private enum SunState
    {
        SunsetToDay,
        Day,
        SunsetToNight,
        Night
    }
    [SerializeField]
    private SunState StartupSunState;
    private Light SunLight;
    private Tweener MovementTweener;

    // Start is called before the first frame update
    void Start()
    {
        SunLight = GetComponent<Light>();
        StartSun();
    }

    private void StartSun()
    {
        switch (StartupSunState)
        {
            case SunState.SunsetToDay:
                SunLight.DOIntensity(0.5f, 0);
                MovementTweener = SunLight.DOColor(SunsetColor, 0);
                SunSet(true);
                break;
            case SunState.Day:
                SunLight.DOIntensity(1f, 0);
                MovementTweener = SunLight.DOColor(DayColor, 0);
                Day();
                break;
            case SunState.SunsetToNight:
                SunLight.DOIntensity(0.5f, 0);
                MovementTweener = SunLight.DOColor(SunsetColor, 0);
                SunSet(false);
                break;
            case SunState.Night:
                SunLight.DOIntensity(0.25f, 0);
                MovementTweener = SunLight.DOColor(NightColor, 0);
                Night();
                break;
        }
    }

    public void PauseSunMovement()
    {
        MovementTweener.Pause();
    }
    public void ResumeSunMovement()
    {
        MovementTweener.Play();
    }

    private void SunSet(bool DayAfter)
    {
        SunLight.DOIntensity(0.5f, SunsetTime).SetEase(Ease.Linear);
        MovementTweener = SunLight.DOColor(SunsetColor, SunsetTime).SetEase(Ease.Linear).OnComplete(delegate { if (DayAfter) { Day(); } else { Night(); } });
    }

    private void Day()
    {
        SunLight.DOIntensity(1.0f, DayTime).SetEase(Ease.Linear);
        MovementTweener = SunLight.DOColor(DayColor, DayTime).SetEase(Ease.Linear).OnComplete(delegate { SunSet(false); });
    }

    private void Night()
    {
        SunLight.DOIntensity(0.25f, NightTime).SetEase(Ease.Linear);
        MovementTweener = SunLight.DOColor(NightColor, NightTime).SetEase(Ease.Linear).OnComplete(delegate { SunSet(true); });
    }
}
