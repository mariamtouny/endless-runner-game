using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] tilePrefabs; 
    public float zSpawn = 0;
    public float tileLength = 2.55f;
    private Transform playerTransform;
    private List<GameObject> activeTiles;
    public int numberOfTiles = 5;
    GameObject road;
    public float generationDistanceThreshold =1.275f; 
    private float lastGenerationZ;
    void Start()
    {
        road = GameObject.FindGameObjectWithTag("road");
        activeTiles = new List<GameObject>();
        //GenerateTile();
        for (int i = 0; i < numberOfTiles; i++)
        {
            if (i == 0)
            {
                GenerateFirstTiles();
                GenerateFirstTiles();
                GenerateFirstTiles();
            }
            else GenerateTile();
        }
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(playerTransform.position.z);
        //Debug.Log(transform.position.z+ zSpawn);
            if (playerTransform.position.z < transform.position.z + zSpawn + (tileLength*numberOfTiles)) // Distance check
            {
                GenerateTile();
                DeleteTile();
                lastGenerationZ = transform.position.z; 
            }

       
        //if (road) Destroy(road);
    }

    public void GenerateFirstTiles()
    {

        
        //Debug.Log(transform.position);
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + zSpawn);
        Vector3 spawnPositionLeft = new Vector3(transform.position.x + 2.988099983f, transform.position.y, transform.position.z + zSpawn);
        Vector3 spawnPositionRight = new Vector3(transform.position.x - 2.988099983f, transform.position.y, transform.position.z + zSpawn);

        GameObject g1 = Instantiate(tilePrefabs[0], spawnPosition, Quaternion.Euler(0, 90, 0));
        GameObject g2 = Instantiate(tilePrefabs[0], spawnPositionLeft, Quaternion.Euler(0, 90, 0));
        GameObject g3 = Instantiate(tilePrefabs[0], spawnPositionRight, Quaternion.Euler(0, 90, 0));
        activeTiles.Add(g1);
        activeTiles.Add(g2);
        activeTiles.Add(g3);

        zSpawn -= tileLength;

    }



    public void GenerateTile()
    {

        int TileIndex = Random.Range(0, tilePrefabs.Length);
        int TileIndexLeft = Random.Range(0, tilePrefabs.Length);
        int TileIndexRight = Random.Range(0, tilePrefabs.Length);
        
        //Debug.Log(transform.position);
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + zSpawn);
        Vector3 spawnPositionLeft = new Vector3(transform.position.x + 2.988099983f, transform.position.y, transform.position.z + zSpawn);
        Vector3 spawnPositionRight = new Vector3(transform.position.x - 2.988099983f, transform.position.y, transform.position.z + zSpawn);


        GameObject g1 = Instantiate(tilePrefabs[TileIndex], spawnPosition, Quaternion.Euler(0, 90, 0));
        GameObject g2 = Instantiate(tilePrefabs[TileIndexLeft], spawnPositionLeft, Quaternion.Euler(0, 90, 0));
        GameObject g3 = Instantiate(tilePrefabs[TileIndexRight], spawnPositionRight, Quaternion.Euler(0, 90, 0));
        activeTiles.Add(g1);
        activeTiles.Add(g2);
        activeTiles.Add(g3);


        zSpawn -= tileLength;

    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);

    }

}
