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

    public bool destroy = false;

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

    private bool CheckDestroy()
    {
        
        if (_donutsCount == 0)
            return true;
        if (_donutsCount == 3)
        {
            List<DonutTypes> types = new List<DonutTypes>();
            foreach (var d in _donuts)
                types.Add(d.DonutType);
            if (types.TrueForAll(x => x == types[0]))
                return true;
        }

        return false;
    }

    public int GetDonutsCount()
    {
        return _donutsCount;
    }
    public Donut GetTopDonut()
    {
        return _donuts.Peek();
    }
    public void RemoveTopDonut()
    {
        _donuts.Pop();
        _donutsCount = _donuts.Count;
        if (CheckDestroy())
        {
            Destroy(this.gameObject);
            destroy = true;
        }
    }
    public void SetTopDonut(Donut newDonut)
    {
        newDonut.transform.parent = this.transform;
        newDonut.transform.localPosition = new Vector3(0, _donutsCount * 0.2f, 0);       
        _donuts.Push(newDonut);
        _donutsCount = _donuts.Count;
        if (CheckDestroy()) { 
            Destroy(this.gameObject); 
            destroy = true;
        }
    }
}
