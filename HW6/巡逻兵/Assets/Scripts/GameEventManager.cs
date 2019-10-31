using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour{
    //玩家逃脱加分
	public delegate void AddScore();
	public static event AddScore addScore;
    // 游戏失败事件
    public delegate void GameOverEvent();
    public static event GameOverEvent GameOver;

    //玩家逃脱
    public void PlayerEscape(){
        if (addScore != null){
            addScore();
        }
    }
    public void gameOver(){
		if(GameOver != null){
			GameOver();
		}
	}
    
}
