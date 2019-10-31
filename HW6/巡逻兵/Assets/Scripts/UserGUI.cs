using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {

    private UserAction action;
    void Start (){
        action = SSDirector.getInstance().currentScenceController as UserAction;
    }

    void Update(){
        //获取方向键的偏移量
        float translationX = Input.GetAxis("Horizontal");
        float translationZ = Input.GetAxis("Vertical");
        //移动玩家
        action.PlayerMove(translationX, translationZ);
    }
    void OnGUI(){
        GUIStyle style = new GUIStyle();//获胜或失败
        GUIStyle scoreStyle = new GUIStyle();
		GUIStyle buttonStyle = new GUIStyle();
        style.fontSize = 40;
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = Color.red;
		buttonStyle = new GUIStyle("Button");
        buttonStyle.fontSize = 40;
		scoreStyle.fontSize = 30;
		scoreStyle.normal.textColor = Color.red;

		GUI.Label(new Rect(Screen.width - 300, Screen.height / 2-180, 100, 50), "Score: " + action.getScore().ToString(),scoreStyle);
		if(action.isEndGame()){ 
			GUI.Label(new Rect(Screen.width/2-50,Screen.height/2-70,100,50),"GameOver!",style);
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart",buttonStyle)){
                action.reStart();
            }
		}
    }
}
