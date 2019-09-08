using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToe : MonoBehaviour
{
    private int[,] map = new int[3,3]; //构建棋盘
    private int turn = 1;//白方1，黑方0
    
    public Texture2D white;
    public Texture2D black;
    private enum gameMode{player,computer};//Vs玩家\Vs电脑
    private gameMode mode = gameMode.player;

    // Start is called before the first frame update
    void Start()
    {
        initGame();
    }

    void initGame(){
        for(int i = 0;i < 3;i ++){
            for(int j = 0;j < 3;j ++){
                map[i,j] = 0;
            }
        }
        turn = 1;//白方先走
        //isWin = false;
    }

    void OnGUI(){
        //游戏标题
        GUIStyle labelstyle = new GUIStyle();
        labelstyle.normal.background = null;
        labelstyle.normal.textColor = Color.red;
        labelstyle.fontSize = 60;
        GUI.Label(new Rect(345,20,100,50),"TicTacToe",labelstyle);

        
        if(GUI.Button(new Rect(200,150,80,70),"重置游戏"))
            initGame();
        if(GUI.Button(new Rect(200,230,80,70),"挑战玩家")){
            initGame();
            mode = gameMode.player;
        }

        if(GUI.Button(new Rect(200,310,80,70),"挑战电脑")){
            initGame();
            mode = gameMode.computer;
        }
        //玩家模式
        if(mode == gameMode.player){
            for (int i=0; i<3; i++) {    
                for (int j=0; j<3; j++) {    
                      
                    //空白格子被点击
                    if (GUI.Button (new Rect (370 + i * 70, 160 + j * 70, 70, 70), "")) {    
                        //游戏未结束
                        if (isWin() == 0) {    
                            if (turn == 1)    
                                map [i, j] = 1;  
                            else    
                                map [i, j] = 2;    
                            turn = 1-turn;    
                        }    
                    } 
                    if (map [i, j] == 1)    
                        GUI.Button (new Rect (370 + i * 70, 160 + j * 70, 70, 70), white);  
                    if (map [i, j] == 2)  
                        GUI.Button (new Rect (370 + i * 70, 160 + j * 70, 70, 70), black);   
                }  
            }
        }

        if(mode == gameMode.computer){
            for (int i=0; i<3; i++) {    
                for (int j=0; j<3; j++) {    
                    if (map [i, j] == 1)    
                        GUI.Button (new Rect (370 + i * 70, 160 + j * 70, 70, 70), white);  
                    if (map [i, j] == 2)  
                        GUI.Button (new Rect (370 + i * 70, 160 + j * 70, 70, 70), black);  
                    //空白格子被点击
                    if (GUI.Button (new Rect (370 + i * 70, 160 + j * 70, 70, 70), "") && turn == 1) {    
                        //游戏未结束
                        if (isWin() == 0) {    
                            map[i,j] = 1;
                            turn = 1-turn;   
                        }    
                    }
                    else if(turn == 0){
                        Invoke("GoAI", (float)(0.3));//延迟0.3秒后调用AI函数
                        //GoAI();//AI走棋
                        turn = 1-turn;
                    }    
                }  
            }
        }

        //显示结果
        GUIStyle resStyle = new GUIStyle();
        resStyle.normal.background = null;
        resStyle.normal.textColor = Color.red;
        resStyle.fontSize = 40;
        if(isWin() == 1){
            GUI.Label(new Rect(385,110,100,50),"白方获胜",resStyle);
        }
        else if(isWin() == 2){
            GUI.Label(new Rect(385,110,100,50),"黑方获胜",resStyle);
        }
        else if(isWin() == 3){
            GUI.Label(new Rect(390,110,100,50),"平局",resStyle);
        }
        if(mode == gameMode.player)
            GUI.Label(new Rect(370,400,100,50),"正在挑战玩家",resStyle);
        else
            GUI.Label(new Rect(370,400,100,50),"正在挑战电脑",resStyle);

    }

    void GoAI(){
        //AI有取胜机会
        for(int i = 0; i < 3;i ++){
            //检查横
            for(int j = 0;j < 3;j ++){
                if(map[i,j%3] == 2 && map[i,(j+1)%3] == 2 && map[i,(j+2)%3] == 0){
                    map[i,(j+2)%3] = 2;
                    //GUI.Button (new Rect (370 + i * 70, 160 + ((j+2)%3) * 70, 70, 70), black);
                    return;  
                }
            }
            //检查竖
            for(int j = 0;j < 3;j ++){
                if(map[j%3,i] == 2 && map[(j+1)%3,i] == 2 && map[(j+2)%3,i] == 0){
                    map[(j+2)%3,i] = 2;
                    //GUI.Button (new Rect (370 + ((j+2)%3) * 70, 160 + i * 70, 70, 70), black);
                    return;  
                }
            }
            //检查斜
            for(int j = 0;j < 3;j ++){
                if(map[j%3,j%3] == 2 && map[(j+1)%3,(j+1)%3] == 2 && map[(j + 2)%3,(j+2)%3] == 0){
                    map[(j+2)%3,(j+2)%3] = 2;
                    //GUI.Button (new Rect (370 + ((j+2)%3) * 70, 160 + ((j+2)%3) * 70, 70, 70), black);
                    return; 
                }
                if(map[(j+2)%3,2-(j+2)%3] == 2 && map[(j+1)%3,2-(j+1)%3] == 2 && map[j,2-j] == 0){
                    map[j,2-j] = 2;
                    //GUI.Button (new Rect (370 + j * 70, 160 + (2-j) * 70, 70, 70), black);
                    return; 
                }
            }   
        }
        //玩家有获胜机会
        for(int i = 0; i < 3;i ++){
            //检查横
            for(int j = 0;j < 3;j ++){
                if(map[i,j%3] == 1 && map[i,(j+1)%3] == 1 && map[i,(j+2)%3] == 0){
                    map[i,(j+2)%3] = 2;
                    //GUI.Button (new Rect (370 + i * 70, 160 + ((j+2)%3) * 70, 70, 70), black);
                    return;  
                }
            }
            //检查竖
            for(int j = 0;j < 3;j ++){
                if(map[j%3,i] == 1 && map[(j+1)%3,i] == 1 && map[(j+2)%3,i] == 0){
                    map[(j+2)%3,i] = 2;
                    //GUI.Button (new Rect (370 + ((j+2)%3) * 70, 160 + i * 70, 70, 70), black);
                    return;  
                }
            }
            //检查斜
            for(int j = 0;j < 3;j ++){
                if(map[j%3,j%3] == 1 && map[(j+1)%3,(j+1)%3] == 1 && map[(j + 2)%3,(j+2)%3] == 0){
                    map[(j+2)%3,(j+2)%3] = 2;
                    //GUI.Button (new Rect (370 + ((j+2)%3) * 70, 160 + ((j+2)%3) * 70, 70, 70), black);
                    return; 
                }
                if(map[(j+2)%3,2-(j+2)%3] == 1 && map[(j+1)%3,2-(j+1)%3] == 1 && map[j,2-j] == 0){
                    map[j,2-j] = 2;
                    
                    return; 
                }
            }   
        }
        //都没有则随机下一个空白格
        int a = (int)Random.Range (0, 3);
        int b = (int)Random.Range (0, 3);
        int cnt = 0;
        while (map [a, b] != 0) {
            a = a + 1;
            a %= 3;
            cnt++;
            if (cnt == 3)
                break;
        }
        cnt = 0;
        while (map [a, b] != 0) {
            b = b + 1;
            b %= 3;
            if (cnt == 3)
                break;
        }
        //都不行找第一个空格
        if(map[a,b] != 0){
            for(int i = 0;i < 3;i ++){
                for(int j = 0;j < 3;j ++){
                    if(map[i,j] == 0){
                        map[i,j] = 2;
                        return;
                    }
                }
            }
        }
        map[a,b] = 2;
        return;
    }

    int isWin(){
        //横
        for(int i = 0;i < 3;i ++){
            if(map[i,0] != 0 && map[i,1] == map[i,0] && map[i,2] == map[i,0]){
                return map[i,0];
            }
        }
        //竖
        for(int i = 0;i < 3;i ++){
            if(map[0,i] != 0 && map[1,i] == map[0,i] && map[2,i] == map[0,i]){
                return map[0,i];
            }
        }
        //斜
        if((map[0,0] != 0 && map[1,1] == map[0,0] && map[2,2] == map[0,0]) || (map[2,0] != 0 && map[1,1] == map[2,0] && map[0,2] == map[2,0])){
            return map[1,1];
        }
        int cnt = 0;
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                if (map [i, j] != 0)
                    cnt ++;
            }
        }
        if(cnt == 9)
            return 3;//下满平局
        return 0;//未结束
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
