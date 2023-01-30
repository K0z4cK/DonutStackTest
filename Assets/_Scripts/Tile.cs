using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private DonutTower _donutTower;

    public DonutTower donutTower
    {
        get {
            if (_donutTower != null)
                return _donutTower;
            return null;
        }
        set
        {
            _donutTower = value;
        }
    }
}
