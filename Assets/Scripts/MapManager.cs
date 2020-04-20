using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Texture2D MapTexture;
    private const float UNITS_PER_PIXEL = 1f;

    [Header("Tielmap Stuff")]
    [SerializeField] private Tilemap PlatformsTilemap;
    [SerializeField] private TileBase PlatformTile;
    [SerializeField] private Tilemap TriggerTilemap;
    [SerializeField] private TileBase WaterDropletTile;
    [SerializeField] private TileBase HealthpackTile;
    [SerializeField] private TileBase EnemyTile;
    [SerializeField] private TileBase DoorTile;

    [Header("Objects to Spawn")]
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Wall;
    [SerializeField] private GameObject HealthPack;
    [SerializeField] private GameObject WaterDroplet;
    [SerializeField] private GameObject Door;
    [SerializeField] private GameObject Enemy;

    private const string PLAYER_COLOR = "0000FF";//dark blue
    private const string WALL_COLOR = "000000";
    private const string HEALTH_PACK_COLOR = "00FF00";//green
    private const string WATER_DROPLET_COLOR = "ADD8E6";//light blue
    private const string DOOR_COLOR = "FFFF00";//yellow
    private const string ENEMY_COLOR = "FF0000";//red

    private void Start()
    {
        Vector3 spawnPosition;
        Vector3Int TilePosition;

        for (int x = 0; x < MapTexture.width; x++)
        {
            for (int y = 0; y < MapTexture.height; y++)
            {
                string cellColor = ColorUtility.ToHtmlStringRGB(MapTexture.GetPixel(x, y));

                switch (cellColor)
                {
                    case PLAYER_COLOR:
                        TilePosition = new Vector3Int(x, y, 1);
                        //spawnPosition = new Vector3(x, y) * UNITS_PER_PIXEL;
                        SpawnPlayer(TilePosition);
                        break;
                    case WALL_COLOR:
                        //spawnPosition = new Vector3Int(x, y, 1);
                        TilePosition = new Vector3Int(x, y, 1);
                        //PlatformsTilemap.SetTile(TilePosition, PlatformTile);
                        SpawnWall(TilePosition);
                        break;
                    case HEALTH_PACK_COLOR:
                        //spawnPosition = new Vector3(x, y) * UNITS_PER_PIXEL;
                        TilePosition = new Vector3Int(x, y, 1);
                        //TriggerTilemap.SetTile(TilePosition, HealthpackTile);
                        SpawnHealthPack(TilePosition);
                        break;
                    case WATER_DROPLET_COLOR:
                        //spawnPosition = new Vector3(x, y) * UNITS_PER_PIXEL;
                        TilePosition = new Vector3Int(x, y, 1);
                        //TriggerTilemap.SetTile(TilePosition, WaterDropletTile);
                        SpawnWaterDroplet(TilePosition);
                        break;
                    case DOOR_COLOR:
                        //spawnPosition = new Vector3(x, y) * UNITS_PER_PIXEL;
                        TilePosition = new Vector3Int(x, y, 1);
                        //TriggerTilemap.SetTile(TilePosition, DoorTile);
                        SpawnDoor(TilePosition);
                        break;
                    case ENEMY_COLOR:
                        //spawnPosition = new Vector3(x, y) * UNITS_PER_PIXEL;
                        TilePosition = new Vector3Int(x, y, 1);
                        //TriggerTilemap.SetTile(TilePosition, EnemyTile);
                        SpawnEnemy(TilePosition);
                        break;
                }
            }
        }
    }

    private void SpawnPlayer(Vector3 position)
    {
        GameObject obj = GameObject.Instantiate(Player, transform);
        obj.transform.position = position;
    }

    private void SpawnWall(Vector3 position)
    {
        GameObject obj = GameObject.Instantiate(Wall, transform);
        obj.transform.position = position;
    }

    private void SpawnHealthPack(Vector3 position)
    {
        GameObject obj = GameObject.Instantiate(HealthPack, transform);
        obj.transform.position = position;
    }

    private void SpawnWaterDroplet(Vector3 position)
    {
        GameObject obj = GameObject.Instantiate(WaterDroplet, transform);
        obj.transform.position = position;
    }

    private void SpawnDoor(Vector3 position)
    {
        GameObject obj = GameObject.Instantiate(Door, transform);
        obj.transform.position = position;
    }

    private void SpawnEnemy(Vector3 position)
    {
        GameObject obj = GameObject.Instantiate(Enemy, transform);
        obj.transform.position = position;
    }
}
