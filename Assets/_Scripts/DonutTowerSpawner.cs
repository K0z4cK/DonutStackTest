using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutTowerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _donutTowerPrefab;
    private Vector3 _baseSpawnPosition;

    private void Start()
    {
        _baseSpawnPosition = new Vector3(0, 0, -4);
    }
    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;
        else
        {
            Instantiate(_donutTowerPrefab, _baseSpawnPosition, _donutTowerPrefab.transform.rotation);
        }
    }
}
