using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int radius;
    public GameObject grassTile;
    public GameObject waterTile;

    public GameObject sandTile;

    public GameObject rocksTile;
    public GameObject mountainTile;

    public float seed = 1234567;
    public float scale = 10f;
    public float xOffset = 0.101f;
    public float yOffset = 0.101f;
    private void Awake()
    {
    }
    void GenerateCoordinates()
    {
        //Empty list of coordinates
        List<Vector3> tileCoordinates = new List<Vector3>();


        //Code to fill the list with coordinates
        //Adds the middle tile
        tileCoordinates.Add(new Vector3(0, 0, 0));
        //Generates the central row
        for (int i = 0; i < radius; i++)
        {
            tileCoordinates.Add(new Vector3(0, 0, i + 1));
            tileCoordinates.Add(new Vector3(0, 0, -i - 1));
        }

        //Generates remaining rows

        int rowsRemaining = radius * 2; //Tracks amount of rows left to generate
        float horizontalDisplacement = 0; //How far the generated tile should be moved horizontally
        float verticalDisplacement = 0; //How far the generated tile should be moved vertically
        int currentRowLength = radius * 2; //Length of the current row being generated (amount of tiles)

        //This loops runs once for each row remaining
        for (int rowID = 0; rowID < rowsRemaining; rowID++)
        {

            //If past half the rows (thus switching to lower rows), reset counters
            if (rowID == radius)
            {
                horizontalDisplacement = 0;
                verticalDisplacement = 0;
                currentRowLength = radius * 2;
            }


            horizontalDisplacement = horizontalDisplacement + 0.5f;
            currentRowLength = currentRowLength - 1;

            if (rowID < radius)
            {
                verticalDisplacement = verticalDisplacement + 0.866f;
            }

            else
            {
                verticalDisplacement = verticalDisplacement - 0.866f;
            }


            for (int tileID = 0; tileID <= currentRowLength; tileID++)
            {
                tileCoordinates.Add(new Vector3(verticalDisplacement, 0, radius - tileID - horizontalDisplacement));
            }

        }


        for (int i = 0; i < tileCoordinates.Count; i++)
        {

            RenderHexFab(tileCoordinates[i]);


        }
    }

    void RenderHexFab(Vector3 coordinate)
    {
        float x = coordinate.x;
        float y = coordinate.z;
        float sample = Mathf.PerlinNoise(x + seed + xOffset, y + seed + yOffset) * scale;
        if (sample < 2)
        {
            GameObject newTile = (GameObject)Instantiate(waterTile, coordinate, Quaternion.Euler(0, 90, 0));
            newTile.gameObject.name = "tile_" + sample.ToString();

        }
        else if (sample < 3)
        {
            GameObject newTile = (GameObject)Instantiate(sandTile, coordinate, Quaternion.Euler(0, 90, 0));
            newTile.gameObject.name = "tile_" + sample.ToString();
        }
        else if (sample > 8)
        {
            GameObject newTile = (GameObject)Instantiate(mountainTile, coordinate, Quaternion.Euler(0, 90, 0));
            newTile.gameObject.name = "tile_" + sample.ToString();
        }
        else if (sample > 7)
        {
            GameObject newTile = (GameObject)Instantiate(rocksTile, coordinate, Quaternion.Euler(0, 90, 0));
            newTile.gameObject.name = "tile_" + sample.ToString();
        }
        else
        {
            GameObject newTile = (GameObject)Instantiate(grassTile, coordinate, Quaternion.Euler(0, 90, 0));
            newTile.gameObject.name = "tile_" + sample.ToString();
        }
    }
    void Start()
    {
        GenerateCoordinates();
    }

    // Update is called once per frame
    void Update()
    {

    }


}
