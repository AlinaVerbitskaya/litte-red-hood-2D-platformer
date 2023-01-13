using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class EventManager
{
    public static Action OnPlayerDeath;
    public static Action OnCoinCollected;
    public static Action OnPlayerInDeathTrigger;
    public static Action<Checkpoint> OnCheckpointActivated;
}

