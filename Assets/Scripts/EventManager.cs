using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public delegate void LevelCompleteDelegate();
    public static LevelCompleteDelegate levelComplete;

    //public void LevelComleted()
    //{
    //    levelComplete.Invoke();
    //}
}
