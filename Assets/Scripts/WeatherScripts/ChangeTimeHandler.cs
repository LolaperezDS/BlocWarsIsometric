using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTimeHandler : MonoBehaviour
{
    private PlayerInstance lastTurn;
    private TimesOfDay timeController;
    private DayTimes CurrentTime = DayTimes.MidNight;
    [SerializeField] private float timeChangeAnimationTime = 0.5f;

    private float currentAnimTime = 0;

    void Start()
    {
        lastTurn = TurnController.CurrentPlayersTurn;
        timeController = GetComponent<TimesOfDay>();
        timeController.SetTime(CurrentTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (lastTurn != TurnController.CurrentPlayersTurn)
        {
            lastTurn = TurnController.CurrentPlayersTurn;
            if (CurrentTime == DayTimes.MidNight) CurrentTime = DayTimes.Morning;
            else CurrentTime++;
        }
    }

    /*
    private void TickOfChangeTimeAnimation()
    {
        if (currentAnimTime >= timeChangeAnimationTime) timeController.SetTime(CurrentTime);
        if (timeController.RangeFromDayTimes(CurrentTime) == timeController.CurrentTimeFloat)
        {
            currentAnimTime = 0;
        }
        else
        {
            currentAnimTime += Time.deltaTime;
            if (timeController.CurrentTimeFloat + 1 / timeChangeAnimationTime > 1) 
            {
                timeController.SetTime(timeController.CurrentTimeFloat + 1 / timeChangeAnimationTime - 1);
            }
            else
            {
                timeController.SetTime(timeController.CurrentTimeFloat + 1 / timeChangeAnimationTime);
            }
        }
    }
    */
}
