using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : BaseSingleton<EventManager>
{
    public static event Action<GameState> OnStateChanged;
    public static void _onStateChanged(GameState state)
    {
        OnStateChanged?.Invoke(state);
    }

    public static event Action OnScreenFadeOut;
    public static void _onScreenFadeOut()
    {
        OnScreenFadeOut?.Invoke();
    }

    public static event Action OnScreenFadeIn;
    public static void _onScreenFadeIn()
    {
        OnScreenFadeIn?.Invoke();
    }

    public static event Action OnRankChanged;
    public static void _onRankChanged()
    {
        OnRankChanged?.Invoke();
    }

}
