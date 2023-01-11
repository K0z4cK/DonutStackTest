using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutTowerMove : MonoBehaviour
{
    private int _leftCorner;
    private int _rightCorner;
    private int _downSpawnCorner;
    private Camera _mainCamera;

    private bool _isCreated;
    void Start()
    {
        _mainCamera = Camera.main;

        _leftCorner = -2;
        _rightCorner = 2;
        _downSpawnCorner = -4;
        _isCreated = true;
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && _isCreated)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out RaycastHit raycastHit))
            {
                transform.position = new Vector3(Mathf.Round(Mathf.Lerp(_leftCorner,_rightCorner, raycastHit.point.x)), 0, _downSpawnCorner);
                print(raycastHit.point);
            }
        }
        else if (Input.GetMouseButtonUp(0))
            _isCreated = false;
    }
}
