using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : BaseSingleton<EventManager>
{
    /*
     * 
      
    public static event Action<CollectibleType> OnCollected;
    public static void _onCollected(CollectibleType collectibleType)
    {
        OnCollected?.Invoke(collectibleType);
    }

    public static event Action<float> OnStackValueChanged;
    public static void _onStackValueChanged(float value)
    {
        OnStackValueChanged?.Invoke(value);
    }

    public static event Action OnUseCurrency;
    public static void _onUseCurrency()
    {
        OnUseCurrency?.Invoke();
    }
    
    */

    public static event Action<GameState> OnStateChanged;
    public static void _onStateChanged(GameState state)
    {
        OnStateChanged?.Invoke(state);
    }

    public static event Action OnHitObstacle;
    public static void _onHitObstacle()
    {
        OnHitObstacle?.Invoke();
    }

    public static event Action OnLevelRestart;
    public static void _onLevelRestart()
    {
        OnLevelRestart?.Invoke();
    }


}
