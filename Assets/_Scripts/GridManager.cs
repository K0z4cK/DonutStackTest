using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : SingletonComponent<GridManager>
{
    [SerializeField] private int _width, _height;

    [SerializeField] private Tile _tilePrefab;
    
    [SerializeField] private Vector2 tileOffset;
    private Dictionary<Vector2, Tile> _tiles;
    

    private void Start()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        GenerateGrid();
    }
    private Vector2 GetKeyByTile(Tile tile)
    {
        Vector2 key = default;
        foreach(var t in _tiles)
        {
            if (EqualityComparer<Tile>.Default.Equals(tile, t.Value))
            {
                key = t.Key;
                break;
            }
        }
        return key;
    }
    public Tile GetTile(Vector2 key)
    {
        //Vector2 offsetPos = new Vector2(pos.x + tileOffset.x, pos.y + tileOffset.y);
        if (_tiles.TryGetValue(key, out var tile))
            return tile;
        return null;
    }
    private List<Tile> GetNeigborTiles(Tile tile)
    {
        Vector2 key = GetKeyByTile(tile);
        List<Tile> neighborTiles = new List<Tile>();
        if(key.y < _height-1)
            neighborTiles.Add(GetTile(new Vector2(key.x, key.y + 1)));
        if (key.x < _width-1)
            neighborTiles.Add(GetTile(new Vector2(key.x + 1, key.y)));
        if (key.x > 0)
            neighborTiles.Add(GetTile(new Vector2(key.x - 1, key.y)));
        print($"Neighbors of: {tile.name}");
        if(neighborTiles.Count != 0)
            neighborTiles.ForEach(x => print(x.name));

        var tilesToRemove = neighborTiles.FindAll(x => x.donutTower == null);
        print("Tiles to remove: ");
        tilesToRemove.ForEach(x => print(x.name));
        tilesToRemove.ForEach(x => neighborTiles.Remove(x));

        return neighborTiles;
    }
    public void CheckTopTileEmpty(Tile tile)
    {
        Vector2 tileKey = GetKeyByTile(tile);
        if (tileKey.y < _height-1)
        {
            var topTile = GetTile(new Vector2(tileKey.x, tileKey.y + 1));
            if (topTile.donutTower.destroy == true)
                tile.donutTower.gameObject.GetComponent<DonutTowerMove>().StartMove((int)(tileKey.y));
        }
    }
    public void CheckNeighborTiles(Tile tile)
    {
        List<Tile> neighborTiles = GetNeigborTiles(tile);
        if (neighborTiles.Count == 0)
            return;

        foreach (var neighborTile in neighborTiles)
        {
            var donutTower = neighborTile.donutTower;

            int startCount = tile.donutTower.GetDonutsCount();
            for (int i = 0; i < startCount; i++)
            {
                if (donutTower.GetDonutsCount() < 3)
                    if (donutTower.GetTopDonut().DonutType == tile.donutTower.GetTopDonut().DonutType)
                    {
                        donutTower.SetTopDonut(tile.donutTower.GetTopDonut());
                        tile.donutTower.RemoveTopDonut();
                        CheckTopTileEmpty(tile);
                    }
            }
            
            if(tile.donutTower != null)
            if (tile.donutTower.GetDonutsCount() != 0)
            {
                startCount = donutTower.GetDonutsCount();
                for (int i = 0; i < startCount; i++)
                {
                    if (tile.donutTower.GetDonutsCount() < 3)
                        if (donutTower.GetTopDonut().DonutType == tile.donutTower.GetTopDonut().DonutType)
                        {
                            tile.donutTower.SetTopDonut(donutTower.GetTopDonut());
                            donutTower.RemoveTopDonut();
                            CheckTopTileEmpty(tile);
                        }
                }
            }          
        }
    } 

    private void GenerateGrid()
    {
        for (int x = 0; x < _width; x++) 
            for(int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x-_width/2, 0, y-_height/2), Quaternion.identity, this.transform);
                
                spawnedTile.name = $"Tile{x}{y}";
                _tiles.Add(new Vector2(x, y), spawnedTile);
            }
    }
}
