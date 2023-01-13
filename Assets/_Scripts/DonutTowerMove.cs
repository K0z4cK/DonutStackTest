using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutTowerMove : MonoBehaviour
{
    private int _leftCorner;
    private int _rightCorner;
    private int _downSpawnCorner;
    private Camera _mainCamera;

    private Vector3 _endPosition;

    private bool _isCreated;
    private bool _isMovingToEndPosition;
    void Start()
    {
        _mainCamera = Camera.main;
        _endPosition = new Vector3();

        _leftCorner = -2;
        _rightCorner = 2;
        _downSpawnCorner = -4;
        _isCreated = true;
        _isMovingToEndPosition = false;
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && _isCreated)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                transform.position = new Vector3(Mathf.Round(Mathf.Lerp(_leftCorner, _rightCorner, raycastHit.point.x)), 0, _downSpawnCorner);
            }
        }
        else if (Input.GetMouseButtonUp(0) && _isCreated) {
            _isCreated = false;
            _endPosition = new Vector3(transform.position.x, transform.position.y, 3);
            _isMovingToEndPosition = true;
        }
        if (_isMovingToEndPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPosition, 5f * Time.deltaTime);
            if (transform.position == _endPosition)
                _isMovingToEndPosition = false;
        }
            
    }
}
