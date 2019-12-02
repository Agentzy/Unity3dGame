using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mygame;
public class InterGUI : MonoBehaviour
{
    private UserAction action;
    public int state = 0;//0表示重新开始，1失败，2获胜
    public int time;//剩余时间
    private PDState curr_state;
    private string tips = "";
    private int cnt = 0;
    void Awake(){
        action = SSDirector.getInstance().currentScenceController as UserAction;
        time = 100;
        cnt = 0;
    }

    void FixedUpdate(){
        Time.fixedDeltaTime = 1;
        time --;
        cnt += 1;
    }

    void OnGUI(){
        GUIStyle style = new GUIStyle();
        GUIStyle timeStyle = new GUIStyle();
        GUIStyle buttonStyle = new GUIStyle();
        timeStyle.fontSize = 40;
        timeStyle.normal.textColor = Color.black;
        style.fontSize = 60;
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = Color.red;
        buttonStyle = new GUIStyle("Button");
        buttonStyle.fontSize = 40;
        if (GUI.Button(new Rect(Screen.width / 2 - 100, 50, 100, 50), "Tips", buttonStyle)){
            int[] arr = action.getRoleNum();
            curr_state = new PDState(arr[0], arr[1], arr[2], arr[3], arr[4]);
            BoatAction bation = curr_state.getNextBoatAction();
            if(bation == BoatAction.P){
                tips = "Move one Priest to boat";
            }
            else if(bation == BoatAction.D){
                tips = "Move one Devil to boat";
            }
            else if(bation == BoatAction.DD){
                tips = "Move two Devil to boat";
            }
            else if(bation == BoatAction.PD){
                tips = "Move one Devil and one Priest to boat";
            }
            else if(bation == BoatAction.PP){
                tips = "Move two Priest to boat";
            }
            else{
                tips = "state error";
            }
            //Debug.Log(tips);
        }
        if (state == 0){
            //5秒后清空提示
            if(cnt == 5){
                tips = "";
                cnt = 0;
            }
            GUI.Label(new Rect(Screen.width / 2 - 250, 150, 100, 50), tips, timeStyle);
            GUI.Label(new Rect(Screen.width - 500, 80, 100, 50), "TimeLeft: " + time, timeStyle);
            if (time == 0) 
                state = 1;//时间耗尽
        }
        else if(state == 1){
            GUI.Label(new Rect(Screen.width/2-50,Screen.height/2-200,100,50),"GameOver!",style);
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2-100, 140, 70), "Restart",buttonStyle)){
                state = 0;
                time = 100;
                cnt = 0;
                action.reStart();
            }
        }
        else if(state == 2){
            GUI.Label(new Rect(Screen.width/2-50,Screen.height/2-200,100,50),"You Win!",style);
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2-100, 140, 70), "Restart",buttonStyle)){
                state = 0;
                time = 100;
                cnt = 0;
                action.reStart();
            }
        }
    }
}
