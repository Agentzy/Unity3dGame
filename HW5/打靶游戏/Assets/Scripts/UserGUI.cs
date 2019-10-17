using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myInterface;

public class UserGUI : MonoBehaviour {
	public bool isButtonDown = false;//是否有箭发射
	public Camera camera;
	public UserAction action;
	public ISceneController scene;//场景接口

	void Start () {
		GameObject obj = GameObject.Find("Main Camera");
		camera = obj.GetComponent<Camera>();
		action = SSDirector.getInstance().currentScenceController as UserAction;
		scene = SSDirector.getInstance().currentScenceController as ISceneController;
	}
	
	// Update is called once per frame
	void OnGUI () {
        GUIStyle scoreStyle = new GUIStyle();
		GUIStyle buttonStyle = new GUIStyle();
		buttonStyle = new GUIStyle("Button");
        buttonStyle.fontSize = 26;
		scoreStyle.fontSize = 30;
		scoreStyle.normal.textColor = Color.black;
		float wind = scene.getWind();
		GUI.Label(new Rect(Screen.width - 200, Screen.height / 2-180, 100, 50), "Score: " + action.getScore().ToString(),scoreStyle);
        if(wind < 0)
			GUI.Label(new Rect(Screen.width - 220, Screen.height / 2-150, 100, 50), "Wind left: " + string.Format("{0:N2}",wind.ToString()),scoreStyle);
		else
			GUI.Label(new Rect(Screen.width - 220, Screen.height / 2-150, 100, 50), "Wind right: " + wind.ToString(),scoreStyle);
		if (GUI.Button(new Rect(Screen.width - 200, Screen.height / 2-100, 140, 70), "Restart",buttonStyle)){
                action.reStart();
        }
		if (Input.GetMouseButtonDown(0) && !isButtonDown) {
            Ray mouseRay = camera.ScreenPointToRay (Input.mousePosition);
            action.hit(mouseRay.direction);
            isButtonDown = true;
        }
        else if(Input.GetMouseButtonDown(0) && isButtonDown) {  
            isButtonDown = false;
        }
	}
}
