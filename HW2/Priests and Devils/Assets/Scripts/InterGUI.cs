using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mygame;
public class InterGUI : MonoBehaviour
{
    private UserAction action;
    public int state = 0;//0表示重新开始，1失败，2获胜
    // Start is called before the first frame update
    void Start()
    {
        action = SSDirector.getInstance().currentScenceController as UserAction;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI(){
        if(state == 1){
            GUI.Label(new Rect(Screen.width/2-40,Screen.height/2-40,100,50),"GameOver!");
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart")){
                state = 0;
                action.reStart();
            }
        }
        else if(state == 2){
            GUI.Label(new Rect(Screen.width/2-40,Screen.height/2-40,100,50),"You Win!");
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart")){
                state = 0;
                action.reStart();
            }
        }
    }
}
