using UnityEngine;

public class TimeController : MonoBehaviour
{
    private PlayerInstance lastTurn;
    private WeatherHandler weatherHandler;
    void Start()
    {
        lastTurn = TurnController.CurrentPlayersTurn;
        weatherHandler = GetComponent<WeatherHandler>();
    }

    void Update()
    {
        // if (lastTurn != TurnController.CurrentPlayersTurn && TurnController.CurrentPlayersTurn == PlayerInstance.Red)
        if (lastTurn != TurnController.CurrentPlayersTurn)
        {
            lastTurn = TurnController.CurrentPlayersTurn;

            DayTimes dayTime = weatherHandler.CurrentTime;
            if (dayTime == DayTimes.MidNight) dayTime = DayTimes.Morning;
            else dayTime = (DayTimes)((int)dayTime + 1);
            StartCoroutine(weatherHandler.ContiniousChangeTime(dayTime));
        }
    }
}
