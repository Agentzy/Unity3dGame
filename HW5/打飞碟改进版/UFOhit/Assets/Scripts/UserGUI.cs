using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myinterface;

public class UserGUI : MonoBehaviour {

	private UserAction action;
	public int blood = 5;
	bool is_play = false;
	void Start () {
		action = SSDirector.getInstance().currentScenceController as UserAction;
		blood = 5;
	}
	
	// Update is called once per frame
	void OnGUI () {
		GUIStyle style = new GUIStyle();//获胜或失败
        GUIStyle bloodStyle = new GUIStyle();
        GUIStyle scoreStyle = new GUIStyle();
		GUIStyle buttonStyle = new GUIStyle();
        style.fontSize = 40;
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = Color.red;
		buttonStyle = new GUIStyle("Button");
        buttonStyle.fontSize = 26;
		scoreStyle.fontSize = 30;
		scoreStyle.normal.textColor = Color.red;

		bloodStyle.fontSize = 30;
		bloodStyle.normal.textColor = Color.red;
		if(!is_play){
			if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2-40, 200, 70), "CCAction",buttonStyle)){
                action.chooseAction(1);
				action.reStart();
				is_play = true;
                return;
            }
			if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2+40, 200, 70), "PhysicalAction",buttonStyle)){
				action.chooseAction(0);
				action.reStart();
                is_play = true;
                return;
			}
		}
		//给用户击中事件
		if(is_play) {
			if (Input.GetButtonDown("Fire1")){
                Vector3 pos = Input.mousePosition;
                action.hit(pos);
            }
		}
		GUI.Label(new Rect(Screen.width - 300, 5, 50, 50), "Blood:", bloodStyle);
            //显示当前血量
		for (int i = 0; i < blood; i++){
			GUI.Label(new Rect(Screen.width - 180 + 20 * i, 5, 10, 10), "O", bloodStyle);
		}
		GUI.Label(new Rect(Screen.width - 300, Screen.height / 2-180, 100, 50), "Score: " + action.getScore().ToString(),scoreStyle);
        GUI.Label(new Rect(Screen.width - 150, Screen.height / 2-180, 100, 50), "Round: " + action.getRound().ToString(),scoreStyle);
		if(blood == 0){ 
			GUI.Label(new Rect(Screen.width/2-50,Screen.height/2-70,100,50),"GameOver!",style);
			action.endGame();
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart",buttonStyle)){
                blood = 5;
                //action.reStart();
				is_play = false;
            }
		}
		//超过100分则获胜
		if(action.getScore() >= 100){
			GUI.Label(new Rect(Screen.width/2-50,Screen.height/2-70,100,50),"You win!",style);
			action.endGame();
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart",buttonStyle)){
                blood = 5;
                //action.reStart();
				is_play = false;
            }
		}
	}
	public void ReduceBlood(){
        if(blood > 0)
            blood--;
    }
}
