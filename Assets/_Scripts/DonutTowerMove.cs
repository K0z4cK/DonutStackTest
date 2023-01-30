using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutTowerMove : MonoBehaviour
{
    private DonutTower _donutTower;

    private int _horizontalCorner;
    private int _downSpawnCorner;
    private Camera _mainCamera;

    private Vector3 _endPosition;

    private bool _isCreated;
    private bool _isMovingToEndPosition;
    private Tile _tile;
    void Start()
    {
        _donutTower = this.GetComponent<DonutTower>();
        _mainCamera = Camera.main;
        _endPosition = new Vector3();

        _horizontalCorner = 2;
        _downSpawnCorner = -4;
        _isCreated = true;
        _isMovingToEndPosition = false;
    }

    private Tile SetEndPosition(int startPos)
    {     
        Tile currentTile;
        Tile previousTile = null;
        for (int i = startPos; i < 7; i++)
        {
            currentTile = GridManager.Instance.GetTile(new Vector2(transform.position.x + _horizontalCorner, i));

            if (currentTile.donutTower != null )
            {
                if (currentTile.donutTower.destroy)
                {
                    _endPosition = new Vector3(transform.position.x, transform.position.y, currentTile.transform.position.z);
                    currentTile.donutTower = _donutTower;
                    if(previousTile.donutTower != null)
                    previousTile.donutTower = null;
                    return currentTile;
                }
                else if(startPos != i)
                {
                    if (i == 0)
                        _endPosition = new Vector3(0, 0, _downSpawnCorner);
                    else
                        _endPosition = new Vector3(transform.position.x, transform.position.y, previousTile.transform.position.z);
                    previousTile.donutTower = _donutTower;
                    //GridManager.Instance.CheckNeighborTiles(previousTile);
                    return previousTile;
                }
            }

            else if (i == 6 && currentTile.donutTower == null)
            {
                _endPosition = new Vector3(transform.position.x, transform.position.y, currentTile.transform.position.z);
                currentTile.donutTower = _donutTower;
               
                return currentTile;
            }
            previousTile = currentTile;            
        }
        return null;
    }
    public void StartMove(int startPos)
    {
        _isCreated = false;
        _tile = SetEndPosition(startPos);

        _isMovingToEndPosition = true;
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && _isCreated)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                transform.position = new Vector3(Mathf.Round(Mathf.Lerp(-_horizontalCorner, _horizontalCorner, raycastHit.point.x)), 0, _downSpawnCorner);
            }
        }
        else if (Input.GetMouseButtonUp(0) && _isCreated) {
            StartMove(0);

            /*if (tile.donutTower != null)
                tile = GridManager.Instance.GetTile(new Vector2(_endPosition.x, _endPosition.z-1));
            tile.donutTower = _donutTower;
            _endPosition = tile.transform.position;
            Debug.Log(tile.name);*/
        }
        if (_isMovingToEndPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPosition, 5f * Time.deltaTime);
            if (transform.position == _endPosition)
            {
                _isMovingToEndPosition = false;
                var top = _donutTower.GetTopDonut();
                print("top: " + top.DonutType.ToString());
                if (_tile != null)
                {
                    if (_tile.donutTower != null)
                    {
                        GridManager.Instance.CheckNeighborTiles(_tile);
                       // GridManager.Instance.CheckTopTileEmpty(_tile);
                    }
                }
            }
        }          
    }

    private void OnDestroy()
    {
        _tile.donutTower = null;
    }
}
