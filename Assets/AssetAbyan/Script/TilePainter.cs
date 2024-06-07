using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePainter : MonoBehaviour
{
    public Tile grassDead;
    public Vector3Int position;
    public Tilemap groundTileMap;
    // Start is called before the first frame update
    [ContextMenu("Paint")]
    void Paint(){
        groundTileMap.SetTile(position, grassDead);
    }
}
