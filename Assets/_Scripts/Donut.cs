using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donut : MonoBehaviour
{
    [SerializeField]
    private DonutTypes _donutType;
    public DonutTypes DonutType { get { return _donutType; } }
    private Renderer _donutRenderer;

    private void Awake()
    {
        _donutType = (DonutTypes)Random.Range(0, 4);
        _donutRenderer = GetComponent<Renderer>();
        _donutRenderer.material = Resources.Load("Materials/Donuts/" + _donutType.ToString(), typeof(Material)) as Material;

    }
}
