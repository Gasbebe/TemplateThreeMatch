using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width;
    public int height;

    public int borderSize;

    public GameObject tilePrefab;
    public GameObject[] gamePiecePrefabs;

    Tile[,] m_allTiles;
    GameObject[,] m_allGamePieces;

    void SetupTiles()
    {
        for(int i = 0; i < width; i++)
        {
            for(int j= 0; j <height; j++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3(i, j, 0), Quaternion.identity) as GameObject;
                tile.name = "Tile (" + i + "," + j + ")";
                m_allTiles[i, j] = tile.GetComponent<Tile>();
                tile.transform.parent = transform;
                m_allTiles[i, j].Init(i, j, this);
            }
        }
    }

    void SetupCamera()
    {
        Camera.main.transform.position = new Vector3((float)(width-1) / 2f, (float)(height-1) / 2f, -10f);
        float aspectRatio = (float) Screen.width / (float) Screen.height;
        float verticalSize = (float)height / 2f + (float)borderSize;
        float horziontalSize = ((float)width / 2f + (float)borderSize) / aspectRatio;

        Camera.main.orthographicSize = (verticalSize > horziontalSize) ? verticalSize : horziontalSize;
    }

    
    GameObject GetRandomGamePicece()
    {
        int randomIdx = Random.Range(0, gamePiecePrefabs.Length);

        if(gamePiecePrefabs[randomIdx] == null)
        {
            Debug.LogWarning("Board  null");
        }

        return gamePiecePrefabs[randomIdx];
    }

    void PlaceGamePiece(GamePiece gamePiece, int x, int y)
    {
        if(gamePiece == null)
        {
            Debug.LogWarning("Board Invlid GamePiece");
            return;
        }
        gamePiece.transform.position = new Vector3(x, y, 0);
        gamePiece.transform.rotation = Quaternion.identity;
        gamePiece.SetCoord(x, y);
    }

    void FillRandom()
    {
        for(int i = 0; i< width; i++)
        {
            for(int j = 0; j< height; j++)
            {
                GameObject randomPiece = Instantiate(GetRandomGamePicece(), Vector3.zero, Quaternion.identity) as GameObject;

                if(randomPiece != null)
                {
                    PlaceGamePiece(randomPiece.GetComponent<GamePiece>(), i, j);
                }
            }
        }
    }
 
    void Start()
    {
        m_allTiles = new Tile[width, height];
        m_allGamePieces = new GameObject[width, height];
        SetupTiles();
        SetupCamera();
        FillRandom();
    }

    void Update()
    {

    }
}
