using UnityEngine;

public class TimeController : MonoBehaviour
{
    private PlayerInstance lastTurn;
    private PlayerInstance currentTurn;
    private WeatherHandler weatherHandler;

    Coroutine timeChangeC;
    void Start()
    {
        currentTurn = TurnController.CurrentPlayersTurn;
        lastTurn = currentTurn;
        weatherHandler = GetComponent<WeatherHandler>();
    }

    void Update()
    {
        if (lastTurn != currentTurn && currentTurn == PlayerInstance.Red)
        {
            if (timeChangeC != null) StopCoroutine(timeChangeC);
            DayTimes dayTime = weatherHandler.CurrentTime;
            if (dayTime == DayTimes.MidNight) dayTime = DayTimes.Morning;
            else dayTime = (DayTimes)((int)dayTime + 1);
            timeChangeC = StartCoroutine(weatherHandler.ContiniousChangeTime(dayTime));
        }
    }
}
