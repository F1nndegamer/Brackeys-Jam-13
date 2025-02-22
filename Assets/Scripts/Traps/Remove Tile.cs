using UnityEngine;
using UnityEngine.Tilemaps;

public class RemoveTile : MonoBehaviour
{
    public Tilemap BG;
    public Tilemap main;
    public Tilemap off;
    public static RemoveTile instance;

    private void Start()
    {
        instance = this;
    }

    public void Remove(float tilemapIndex, Vector2 pos1, Vector2 pos2)
    {
        Tilemap targetTilemap = null;
        
        if (tilemapIndex == 0)
            targetTilemap = BG;
        else if (tilemapIndex == 1)
            targetTilemap = main;
        else if (tilemapIndex == 2)
            targetTilemap = off;
        
        if (targetTilemap == null)
        {
            Debug.LogError("Invalid tilemap index");
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
