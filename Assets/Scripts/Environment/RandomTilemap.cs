using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomTilemapGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase[] randomTiles;

    public int width = 1000;
    public int height = 1000;

    public float randomTileFrequency = 0.02f;

    void Start()
    {
        GenerateTilemap();
    }

    void GenerateTilemap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (Random.value < randomTileFrequency)
                {
                    TileBase randomTile = randomTiles[Random.Range(0, randomTiles.Length)];
                    tilemap.SetTile(new Vector3Int(x, y, 0), randomTile);
                }
            }
        }
    }
}
