using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapVisual : MonoBehaviour
{
    [System.Serializable]
    public struct TilemapSpriteUV
    {
        public TileMap.TileMapObject.TileMapSprite tilemapSprite;
        public Vector2Int uv00Pixels;
        public Vector2Int uv11Pixels;
    }

    private struct UVCoords
    {
        public Vector2 uv00;
        public Vector2 uv11;
    }

    [SerializeField] private TilemapSpriteUV[] tilemapSpriteUVArray;
    private GridMap<TileMap.TileMapObject> grid;
    private Mesh mesh;
    private bool updateMesh;
    private Dictionary<TileMap.TileMapObject.TileMapSprite, UVCoords> uvCoordsDictionary;

    private void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        Texture texture = GetComponent<MeshRenderer>().material.mainTexture;
        float textureWidth = texture.width;
        float textureHeight = texture.height;

        uvCoordsDictionary = new Dictionary<TileMap.TileMapObject.TileMapSprite, UVCoords>();

        foreach (TilemapSpriteUV tilemapSpriteUV in tilemapSpriteUVArray)
        {
            uvCoordsDictionary[tilemapSpriteUV.tilemapSprite] = new UVCoords
            {
                uv00 = new Vector2(tilemapSpriteUV.uv00Pixels.x / textureWidth, tilemapSpriteUV.uv00Pixels.y / textureHeight),
                uv11 = new Vector2(tilemapSpriteUV.uv11Pixels.x / textureWidth, tilemapSpriteUV.uv11Pixels.y / textureHeight),
            };
        }
    }


    public void setGrid(GridMap<TileMap.TileMapObject> t_grid)
    {
        this.grid = t_grid;

        UpdateTileMapVisual();

        grid.onGridObjectChange += Grid_OnGridValueChange;
    }

    private void Grid_OnGridValueChange(object sender, GridMap<TileMap.TileMapObject>.onGridObjectChangeEventArgs e)
    {
        updateMesh = true;
    }


    private void LateUpdate()
    {
        if (updateMesh == true)
        {
            updateMesh = false;
            UpdateTileMapVisual();
        }
    }


    private void UpdateTileMapVisual()
    {
        MeshUtils.CreateEmptyMeshArrays(grid.getWidth() * grid.getHeight(), out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

        for (int x = 0; x < grid.getWidth(); x++)
        {
            for (int y = 0; y < grid.getHeight(); y++)
            {
                int index = x * grid.getHeight() + y;
                Vector3 quadSize = new Vector3(1, 1) * grid.getCellSize();

                TileMap.TileMapObject gridObject = grid.getGridObject(x, y);
                TileMap.TileMapObject.TileMapSprite tilemapSprite = gridObject.getTileMapSprite();
                Vector2 gridUV00, gridUV11;
                if (tilemapSprite == TileMap.TileMapObject.TileMapSprite.None)
                {
                    gridUV00 = Vector2.zero;
                    gridUV11 = Vector2.zero;
                    quadSize = Vector3.zero;
                }
                else
                {
                    UVCoords uvCoords = uvCoordsDictionary[tilemapSprite];
                    gridUV00 = uvCoords.uv00;
                    gridUV11 = uvCoords.uv11;
                }
                MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, grid.worldPos(x, y) + quadSize * .5f, 0f, quadSize, gridUV00, gridUV11);
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

    }
}
