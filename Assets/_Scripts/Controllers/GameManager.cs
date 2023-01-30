using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum DonutTypes { Yellow, Blue, Brown, Pink }
public enum DonutTowerStates { Created, Move, StayingAtPosition }

public class GameManager : SingletonComponent<GameManager>
{
    public static Action OnTowerCreated;

}
