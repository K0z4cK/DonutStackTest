using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutTower : MonoBehaviour
{
    [SerializeField]
    private GameObject _donutPref;

    private Stack<Donut> _donuts;
    private int _donutsCount;
    private Vector3 _position;

    private void Awake()
    {
        _donuts = new Stack<Donut>();
        _donutsCount = Random.Range(1, 4);
        print(_donutsCount);
        _position = transform.position;
        for (int i = 0; i < _donutsCount; i++)
        {
            GameObject newDonut = Instantiate(_donutPref, this.transform);
            newDonut.transform.localPosition = new Vector3(0, i * 0.2f, 0);
            _donuts.Push(newDonut.GetComponent<Donut>());
        }
    }
}
