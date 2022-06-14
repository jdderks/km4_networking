using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int gridX = 25;
    [SerializeField] private int gridZ = 25;



    private Player player = null;
    private GameObject playerGO = null;

    [SerializeField] private Vector2Int currentPlayerPosition;

    [SerializeField] private Transform tileParent;

    Camera camera;

    private Tile selectedTile;

    private GameObject selectedTileObject = null;
    private GameObject hoveredTileObject = null;

    private List<Tile> tiles = new List<Tile>();

    public List<Tile> Tiles { get => tiles; set => tiles = value; }
    public Camera Camera { get => camera; set => camera = value; }
    public Tile SelectedTile { get => selectedTile; set => selectedTile = value; }
    public Vector2Int CurrentPlayerPosition { get => currentPlayerPosition; set => currentPlayerPosition = value; }

    public void Generate()
    {
        Camera = Camera.main;
        for (int x = 0; x < gridX; x++)
        {
            for (int z = 0; z < gridZ; z++)
            {
                var rotation = Quaternion.Euler(90, 0, 0);

                //Create a new tile from a quad
                GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Quad);
                tile.transform.position = new Vector3(x, 0, z);
                tile.transform.rotation = rotation;
                tile.transform.parent = tileParent;


                //Give tile nice color
                //From https://docs.unity3d.com/ScriptReference/Material.SetColor.html
                var renderer = tile.GetComponent<Renderer>();
                renderer.material = new Material(Shader.Find("Specular"));
                int randomInt = Random.Range(200, 255);
                Color groundColor = new Color32(0, (byte)randomInt, 0, 255);
                renderer.material.SetColor("_Color", groundColor);

                //Give the tile its data
                Tile t = tile.AddComponent<Tile>();
                t.x = x;
                t.z = z;
                t.greenery = (byte)randomInt;

                Tiles.Add(t);
            }
        }

        //CreatePlayer(1, GetTileFromPosition(5, 5));
        //camera = playerGO.GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if (camera != null)
        {
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.gameObject != hoveredTileObject && hoveredTileObject != null || hitInfo.collider == null)
                {
                    var r = hoveredTileObject.GetComponent<Renderer>();
                    r.material.color = new Color32(0, (byte)hoveredTileObject.GetComponent<Tile>().greenery, 0, 255);
                    hoveredTileObject = null;
                }

                if (hitInfo.collider.gameObject.GetComponent<Tile>() != null)
                {
                    var hoverTile = hitInfo.collider.gameObject.GetComponent<Tile>();

                    //Check if tile is neighboring the player occupied tile
                    if (hoverTile.x >= CurrentPlayerPosition.x - 1 && hoverTile.x <= CurrentPlayerPosition.x + 1 &&
                        hoverTile.z >= CurrentPlayerPosition.y - 1 && hoverTile.z <= CurrentPlayerPosition.y + 1)
                    {
                        if (hoverTile != GetTileFromPosition(CurrentPlayerPosition.x, CurrentPlayerPosition.y))
                        {
                            hoveredTileObject = hitInfo.collider.gameObject;
                            var renderer = hoveredTileObject.GetComponent<Renderer>();
                            var starColor = renderer.material.color;
                            renderer.material.color = Color.yellow;

                            if (Input.GetMouseButtonDown(0))
                            {
                                selectedTileObject = hitInfo.collider.gameObject;
                                SelectedTile = selectedTileObject.GetComponent<Tile>();
                            }
                        }
                    }
                    else
                    {
                        hoveredTileObject = hitInfo.collider.gameObject;
                        var renderer = hoveredTileObject.GetComponent<Renderer>();
                        var starColor = renderer.material.color;
                        renderer.material.color = Color.gray;
                    }

                }
            }
        }
        else
        {
            if (hoveredTileObject != null)
            {
                var r = hoveredTileObject.GetComponent<Renderer>();
                r.material.color = new Color32(0, (byte)hoveredTileObject.GetComponent<Tile>().greenery, 0, 255);
                hoveredTileObject = null;
            }
        }

        //if (selectedTileObject != null)
        //{

        //float step = speed * Time.deltaTime;
        //playerGO.transform.position = Vector3.MoveTowards(playerGO.transform.position, selectedTileObject.transform.position, step);
        //}
    }



    public Tile GetTileFromPosition(int x, int z)
    {
        for (int i = 0; i < Tiles.Count; i++)
        {
            if (Tiles[i].x == x && Tiles[i].z == z)
            {
                return Tiles[i];
            }
        }
        return null;
    }
    public Tile GetTileFromPosition(Vector2Int position)
    {
        for (int i = 0; i < Tiles.Count; i++)
        {
            if (Tiles[i].x == position.x && Tiles[i].z == position.y)
            {
                return Tiles[i];
            }
        }
        return null;
    }
}
