using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameLogic : MonoBehaviour
{
    public GameObject[] hollowCubes = new GameObject[26];
    public GameObject fullCube;
    public GameObject flaggedCube;
    public ArrayList prefabs = new ArrayList();
    public int[,,] gameBoard;
    public bool startOfGame = false;
    public LayerMask mask;

    void Start()
    {
        startOfGame = false;
    }

    void Update()
    {
        
    }

    public void rightClick(GameObject clicked){
        if(startOfGame){
            startGame(clicked);
            startOfGame = false;
        }else{
            UpdateGrid(true, clicked);
        }
    }

    public void leftClick(GameObject clicked){
        if(startOfGame){
            startGame(clicked);
            startOfGame = false;
        }else{
            UpdateGrid(false, clicked);
        }
    }

    public void setupGame(int dim){
        foreach(GameObject t in prefabs){
            Destroy(t);
        }
        prefabs.Clear();
        gameBoard = new int[dim,dim,dim];
        for(int i = 0; i < dim; i++){
            for(int j = 0; j < dim; j++){
                for(int k = 0; k < dim; k++){
                    gameBoard[i,j,k] = -100;
                    Vector3 pos = new Vector3(i,j,k);
                    GameObject temp = Instantiate(fullCube, pos, fullCube.transform.rotation);
                    prefabs.Add(temp);
                }
            }
        }  
        startOfGame = true;
    }

    public void startGame(GameObject clicked){
        int mineCount = 1;
        int[] mine = {1,1,1};
        while(mineCount < 0.1 * gameBoard.Length){
            for(int i = 0; i < 3; i++){
                mine[i] = Random.Range(0, gameBoard.GetLength(0));
            }
            gameBoard[mine[0], mine[1], mine[2]] = -99;

            for(int x = -1; x <= 1; x++){
            if((int)clicked.transform.position.x + x>=0 && (int)clicked.transform.position.x + x<gameBoard.GetLength(0)){
                for(int y = -1; y <= 1; y++){
                if((int)clicked.transform.position.y + y>=0 && (int)clicked.transform.position.y + y<gameBoard.GetLength(0)){
                    for(int z = -1; z <= 1; z++){
                    if((int)clicked.transform.position.z + z>=0 && (int)clicked.transform.position.z + z<gameBoard.GetLength(0)){
                        gameBoard[(int)clicked.transform.position.x + x, (int)clicked.transform.position.y + y, (int)clicked.transform.position.z + z] = 0;
                    } 
                }}
            }}
            }
            mineCount = 0;
            for(int i = 0; i < gameBoard.GetLength(0); i++){
                for(int j = 0; j < gameBoard.GetLength(1); j++){
                    for(int k = 0; k < gameBoard.GetLength(2); k++){
                        if(gameBoard[i,j,k] == -99){
                            mineCount++;
                        }
                    }
                }
            }
            }
        mineCount = 0;
        for(int i = 0; i < gameBoard.GetLength(0); i++){
            for(int j = 0; j < gameBoard.GetLength(1); j++){
                for(int k = 0; k < gameBoard.GetLength(2); k++){
                    for(int x = -1; x <= 1; x++){
                        if(i+x>=0 && i+x<gameBoard.GetLength(0)){
                        for(int y = -1; y <= 1; y++){
                            if(j+y>=0 && j+y<gameBoard.GetLength(0)){
                            for(int z = -1; z <= 1; z++){
                                if(k+z>=0 && k+z<gameBoard.GetLength(0)){
                                    if(gameBoard[i+x,j+y,k+z]==-99){
                                        mineCount++;
                                    }
                                }
                            }}
                        }}
                    }
                    if(gameBoard[i,j,k] != -99){
                    gameBoard[i,j,k] = -1 * mineCount;
                    }
                    mineCount=0;
                }
            }
        }  
        for(int i = 0; i < gameBoard.GetLength(0); i++){
                for(int j = 0; j < gameBoard.GetLength(1); j++){
                    for(int k = 0; k < gameBoard.GetLength(2); k++){
                        if(gameBoard[i,j,k] == 0){
                            gameBoard[i,j,k] = -100;
                        }
                    }
                }
            }
        UpdateGrid(false, clicked);        
    }
    

    public void UpdateGrid(bool flagged, GameObject clicked){
       Vector3 copy = clicked.transform.position;
        if(flagged){
            if(gameBoard[(int)copy.x, (int)copy.y, (int)copy.z] == -99 ){
                gameBoard[(int)copy.x, (int)copy.y, (int)copy.z] = 99;
            }else if(gameBoard[(int)copy.x, (int)copy.y, (int)copy.z] == -100){
                gameBoard[(int)copy.x, (int)copy.y, (int)copy.z] = 100;
            } else if(gameBoard[(int)copy.x, (int)copy.y, (int)copy.z] == 100){
                gameBoard[(int)copy.x, (int)copy.y, (int)copy.z] = -100;
            }else if(gameBoard[(int)copy.x, (int)copy.y, (int)copy.z] == 99){
                gameBoard[(int)copy.x, (int)copy.y, (int)copy.z] = -99;
            } else if(gameBoard[(int)copy.x, (int)copy.y, (int)copy.z] <0){
                gameBoard[(int)copy.x, (int)copy.y, (int)copy.z] =  (-1 * gameBoard[(int)copy.x, (int)copy.y, (int)copy.z]) + 30;
            } else {
                gameBoard[(int)copy.x, (int)copy.y, (int)copy.z] =  -1 * (gameBoard[(int)copy.x, (int)copy.y, (int)copy.z] - 30);
            }
            
        }else{
           
            
            if(gameBoard[(int)copy.x, (int)copy.y, (int)copy.z] == -99){//clicked a mine
            Endgame(true);
            } else if(gameBoard[(int)copy.x, (int)copy.y, (int)copy.z] == -100){
                gameBoard[(int)copy.x, (int)copy.y, (int)copy.z] = 0;
                recursiveFinder(copy);
            } else {
                gameBoard[(int)copy.x, (int)copy.y, (int)copy.z] = -1 * gameBoard[(int)copy.x, (int)copy.y, (int)copy.z];
            }
            Destroy(clicked); 
        }
        int tempCount = 0;
        for(int i = 0; i < gameBoard.GetLength(0); i++){
                for(int j = 0; j < gameBoard.GetLength(1); j++){
                    for(int k = 0; k < gameBoard.GetLength(2); k++){
                        if(gameBoard[i,j,k] == -100 || gameBoard[i,j,k] == 100 ){
                            tempCount++;
                        }
                    }
                }
            }
        if(tempCount == 0){
            Endgame(false);
        }
           showBoard();
    }
    public void showBoard(){
        foreach(GameObject t in prefabs){
            Destroy(t);
        }
        prefabs.Clear();
        for(int i = 0; i < gameBoard.GetLength(0); i++){
            for(int j = 0; j < gameBoard.GetLength(1); j++){
                for(int k = 0; k < gameBoard.GetLength(2); k++){
                    if(gameBoard[i,j,k] <0){
                        GameObject temp = Instantiate(fullCube, new Vector3(i,j,k), fullCube.transform.rotation);
                        prefabs.Add(temp);
                    }else if(gameBoard[i,j,k] == 99 || gameBoard[i,j,k] == 100 ||gameBoard[i,j,k] > 30 ){
                        GameObject temp = Instantiate(flaggedCube, new Vector3(i,j,k), flaggedCube.transform.rotation);
                        prefabs.Add(temp);
                    }else if(gameBoard[i,j,k] >0){
                        GameObject temp = Instantiate(hollowCubes[gameBoard[i,j,k] - 1], new Vector3(i,j,k), hollowCubes[gameBoard[i,j,k] - 1].transform.rotation);
                        prefabs.Add(temp);
                    } 
                }
            }
        }
    }
    public void Endgame(bool died){
        if(died){
            Debug.Log("died");
            foreach(GameObject t in prefabs){
                Destroy(t);
            }
            prefabs.Clear();
        } else {
            Debug.Log("won");
        }
    }

    private void recursiveFinder(Vector3 pos){
        for(int x = -1; x <= 1; x++){
                        if((int)pos.x+x>=0 && (int)pos.x+x<gameBoard.GetLength(0)){
                        for(int y = -1; y <= 1; y++){
                            if((int)pos.y+y>=0 && (int)pos.y+y<gameBoard.GetLength(0)){
                            for(int z = -1; z <= 1; z++){
                                if((int)pos.z+z>=0 && (int)pos.z+z<gameBoard.GetLength(0)){
                                    if(gameBoard[(int)pos.x+x,(int)pos.y+y,(int)pos.z+z] <0 && gameBoard[(int)pos.x+x,(int)pos.y+y,(int)pos.z+z] > -30){
                                        gameBoard[(int)pos.x+x,(int)pos.y+y,(int)pos.z+z] = -1* gameBoard[(int)pos.x+x,(int)pos.y+y,(int)pos.z+z];
                                    }
                                    if(gameBoard[(int)pos.x+x,(int)pos.y+y,(int)pos.z+z]==-100 && !(x==0&&y==0&&z==0)){
                                        gameBoard[(int)pos.x+x,(int)pos.y+y,(int)pos.z+z]=0;
                                        recursiveFinder(new Vector3((int)pos.x+x,(int)pos.y+y,(int)pos.z+z));
                                    }
                                }
                            }}
                        }}
                    }
    }



}