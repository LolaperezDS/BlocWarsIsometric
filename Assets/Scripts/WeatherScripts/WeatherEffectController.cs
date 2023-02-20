using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherEffectController : MonoBehaviour
{
    private PlayerInstance lastTurn;
    private PlayerInstance currentTurn;

    private bool isinEffect;

    private float multiplayerOfExit = 3;
    private float chanceOfRain = 0.08f;
    private float chanceOfAsh = 0.05f;
    private float chanceOfGodRays = 0.05f;

    void Start()
    {
        currentTurn = TurnController.CurrentPlayersTurn;
        lastTurn = currentTurn;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastTurn != currentTurn)
        {
            if (isinEffect)
            {
                if (IsEventStart(chanceOfRain * multiplayerOfExit))
                {
                    return;
                }
                if (IsEventStart(chanceOfAsh * multiplayerOfExit))
                {
                    return;
                }
                if (IsEventStart(chanceOfGodRays * multiplayerOfExit))
                {
                    return;
                }
            }
            else
            {
                if (IsEventStart(chanceOfRain))
                {
                    return;
                }
                if (IsEventStart(chanceOfAsh))
                {
                    return;
                }
                if (IsEventStart(chanceOfGodRays))
                {
                    return;
                }
            }
        }
    }

    private bool IsEventStart(float chance)
    {
        float flag = Random.Range(0f, 1f);
        return chance >= flag;
    }
}
