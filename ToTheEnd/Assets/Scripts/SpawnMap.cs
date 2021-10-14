using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnMap : MonoBehaviour
{
    // prefabs
    public GameObject Tile;
    public GameObject Wall;
    public GameObject PC;
    public GameObject Enemy;

    private List<GameObject> AllBG = new List<GameObject>();
    private GameObject Player;
    //private List<GameObject> Enemies = new List<GameObject>();
    //public Vector2 PlayerLoc = new Vector2();

    // Screen/UI
    private int MapHeight;
    private int MapWidth;
    public RectTransform ScrollRect;

    // player controls
    public PlayerMovement PlayerController;
    public EnemyController EnemyController;

    // block settings:
    public enum TileSides
    {
        Up = 0,
        Down,
        Left,
        Right
    };
    // keep track of blocks:
    private List<GameObject> allWalls = new List<GameObject>();
     
    // Start is called before the first frame update
    void Start()
    {
        TileBackground(12,10);
        SpawnPC(5,5);
        SpawnEnemy(5,7);
        //SpawnEnemy(2,2);
        SaveScreenSize();
        Refocus();
        PlayerController.ReadSides();
        SpawnSingleBlock(5, 6, TileSides.Up);
        SpawnSingleBlock(5, 7, TileSides.Down);
        SpawnSingleBlock(5, 8, TileSides.Left);
        SpawnSingleBlock(5, 9, TileSides.Right);
    } 

    void TileBackground(int height, int width){
        for (int i = 0; i<height; i++)
        {
            for (int j = 0; j<width; j++)
            {
                GameObject singleTile = GameObject.Instantiate(Tile,Tile.transform.parent);
                singleTile.SetActive(true);// = true;
                //Debug.Log(singleTile.name);
                RectTransform singleTileRect = singleTile.GetComponent<RectTransform>();
                singleTileRect.anchoredPosition = new Vector2(25+ 50*i, 25 + 50*j);
                AllBG.Add(singleTile);
            }
        }
    }

    void SpawnPC(int row, int col){
        Player = PC;
        //Player = GameObject.Instantiate(PC,PC.transform.parent);
        RectTransform singleTileRect = Player.GetComponent<RectTransform>();
        singleTileRect.anchoredPosition = new Vector2(25+ 50*row, 25 + 50*col);
        Player.SetActive(true);
        PlayerController.Player = singleTileRect;
        PlayerController.MainController = this;
        //PlayerLoc.x = singleTileRect.anchoredPosition[0];
        //PlayerLoc.y = singleTileRect.anchoredPosition[1];

    }

    void SpawnEnemy(int row, int col){
        GameObject singleTile = GameObject.Instantiate(Enemy,Enemy.transform.parent);
        singleTile.SetActive(true);
        RectTransform singleTileRect = singleTile.GetComponent<RectTransform>();
        singleTileRect.anchoredPosition = new Vector2(25+ 50* row, 25 + 50*col);
        //Enemies.Add(singleTile);
        EnemyController.AddNewEnemy(singleTile.GetComponent<EnemyMovement>());

    }

    void SpawnSingleBlock(int row, int col, TileSides side)
    {
        GameObject singleWall;
        RectTransform singleWallRect;
        switch (side)
        {
            case TileSides.Up:
                singleWall = Instantiate(Wall, Wall.transform.parent);
                singleWall.SetActive(true);
                singleWallRect = singleWall.GetComponent<RectTransform>();
                singleWallRect.anchoredPosition = new Vector2(25 + 50 * row, 50 + 50 * col);
                singleWallRect.eulerAngles = new Vector3(0,0,90);
                allWalls.Add(singleWall);
                break; 
            case TileSides.Down:
                singleWall = Instantiate(Wall, Wall.transform.parent);
                singleWall.SetActive(true);
                singleWallRect = singleWall.GetComponent<RectTransform>();
                singleWallRect.anchoredPosition = new Vector2(25 + 50 * row, 50 * col);
                singleWallRect.eulerAngles = new Vector3(0, 0, 90); 
                allWalls.Add(singleWall);
                break;

            case TileSides.Left:
                singleWall = Instantiate(Wall, Wall.transform.parent);
                singleWall.SetActive(true);
                singleWallRect = singleWall.GetComponent<RectTransform>();
                singleWallRect.anchoredPosition = new Vector2(50 * row, 25 + 50 * col);
                singleWallRect.eulerAngles = new Vector3(0, 0, 0); 
                allWalls.Add(singleWall);
                break;

            case TileSides.Right:
                singleWall = Instantiate(Wall, Wall.transform.parent);
                singleWall.SetActive(true);
                singleWallRect = singleWall.GetComponent<RectTransform>();
                singleWallRect.anchoredPosition = new Vector2(50 + 50 * row, 25 + 50 * col);
                singleWallRect.eulerAngles = new Vector3(0, 0, 0); 
                allWalls.Add(singleWall);
                break;

        }
    }

    void SaveScreenSize()
    {
        MapHeight = Screen.height - 10 - 150;
        MapWidth = Screen.width - 20;
    }

    public void Refocus(){
        // based on screen size and position of the PC:
        int newTop = 0;
        int newBottom = -400;
        int newLeft = 0;
        int newRight = -150;

        int totalHeight = 400 + MapHeight;
        int totalWidth = 150 + MapWidth;

        // height
        if (MapHeight-25 > PlayerController.Player.anchoredPosition.y)
        {
            newBottom = 0;
            newTop = -400;
        } 
        else if (400 + 25 < PlayerController.Player.anchoredPosition.y)
        {

            //no change
        }
        else
        {
            // center it 
            newBottom = -1 * (int)(PlayerController.Player.anchoredPosition.y*400/totalHeight);
            newTop = -400-newBottom;
        }

        // width
        if (MapWidth - 25 > PlayerController.Player.anchoredPosition.x)
        {
            newRight = 0;
            newLeft = -150;
        }
        else if (150 + 25 < PlayerController.Player.anchoredPosition.x)
        {
            // no change

        }
        else
        {
            // center it 
            newLeft = -1 *(int)(PlayerController.Player.anchoredPosition.x * 150 / totalWidth);
            newRight = -150 - newLeft;
        }

        //Debug.Log(PlayerController.Player.anchoredPosition);
        //Debug.Log(ScrollRect.offsetMin);
        //Debug.Log(newLeft);
        //Debug.Log(newTop);
        //Debug.Log(newRight);
        //Debug.Log(newBottom);
        //ScrollRect.offsetMax = new Vector2(newLeft, newTop);
        //ScrollRect.offsetMin = new Vector2(newRight, newBottom);
        ScrollRect.offsetMin = new Vector2(newRight, newBottom);
        ScrollRect.offsetMax = new Vector2(-newLeft, -newTop);
        //Debug.Log(ScrollRect.offsetMax);
        //PlayerController.ReadSides();

    }

    
    // turn based -- let enemy go:
    public void PlayerHasMoved()
    {
        EnemyController.StartEnemyTurn();
    }

    // turn based -- let player go:
    public void EnemyHasMoved()
    {
        PlayerController.ReadSides(); // reenable button of sides;
    }

    // when any enemy touches the player:


    // when the player touches the goal:
    

}
