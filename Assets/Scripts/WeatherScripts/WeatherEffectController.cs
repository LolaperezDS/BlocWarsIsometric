using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherEffectController : MonoBehaviour
{
    private PlayerInstance lastTurn;
    private PlayerInstance currentTurn;

    private bool isInEffect = false;

    private float chanceOfExit = 0.15f;
    private float chanceOfRain = 0.08f;
    private float chanceOfAsh = 0.05f;
    private float chanceOfGodRays = 0.05f;

    [SerializeField] private WeatherEffectAbs RekindledAhsesObj;
    [SerializeField] private WeatherEffectAbs GodRaysObj;
    [SerializeField] private WeatherEffectAbs RainObj;

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
            lastTurn = currentTurn;
            if (isInEffect)
            {
                if (IsEventStart(chanceOfExit))
                {
                    isInEffect = false;
                    RekindledAhsesObj.DeactivateEffect();
                    GodRaysObj.DeactivateEffect();
                    RainObj.DeactivateEffect();
                    return;
                }
            }
            else
            {
                isInEffect = true;
                if (IsEventStart(chanceOfRain))
                {
                    RainObj.ActivateEffect();
                    return;
                }
                else if (IsEventStart(chanceOfAsh))
                {
                    RekindledAhsesObj.ActivateEffect();
                    return;
                }
                else if (IsEventStart(chanceOfGodRays))
                {
                    GodRaysObj.ActivateEffect();
                    return;
                }
                else isInEffect = false;
            }
        }
    }

    private bool IsEventStart(float chance)
    {
        float flag = Random.Range(0f, 1f);
        return chance >= flag;
    }
}
