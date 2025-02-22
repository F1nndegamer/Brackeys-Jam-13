using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TilemapName { BG, Main, Main2 } // Move the enum outside

[System.Serializable]
public class TileSelection
{
    public Vector2 StartPos;
    public Vector2 EndPos;
    public TilemapName SelectedTilemap; // Add this field
}

public class RemoveTile : MonoBehaviour, KeyFunction
{
    public Tilemap BG;
    public Tilemap main;
    public Tilemap off;
    public static RemoveTile instance;
    
    public List<TileSelection> tiles = new List<TileSelection>();

    private void Start()
    {
        instance = this;
    }

    public void CalledFromTressure()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            Remove(tiles[i].SelectedTilemap, tiles[i].StartPos, tiles[i].EndPos);
        }
    }

    public void Remove(TilemapName tilemapName, Vector2 pos1, Vector2 pos2)
    {
        Tilemap targetTilemap = null;
        
        switch (tilemapName)
        {
            case TilemapName.BG:
                targetTilemap = BG;
                break;
            case TilemapName.Main:
                targetTilemap = main;
                break;
            case TilemapName.Main2:
                targetTilemap = off;
                break;
            default:
                Debug.LogError("Invalid tilemap name");
                return;
        }
        
        Vector3Int tilePos1 = targetTilemap.WorldToCell(pos1);
        Vector3Int tilePos2 = targetTilemap.WorldToCell(pos2);
        
        for (int x = Mathf.Min(tilePos1.x, tilePos2.x); x <= Mathf.Max(tilePos1.x, tilePos2.x); x++)
        {
            for (int y = Mathf.Min(tilePos1.y, tilePos2.y); y <= Mathf.Max(tilePos1.y, tilePos2.y); y++)
            {
                targetTilemap.SetTile(new Vector3Int(x, y, 0), null);
            }
        }
    }
}
