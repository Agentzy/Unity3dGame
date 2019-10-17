using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myInterface;

public class FirstController : MonoBehaviour, ISceneController, UserAction{

	public IActionManager actionManager;
	public ArrowFactory AF;
	public GameObject arrow;
	public UserGUI user_gui;
	public GameObject bow;


	public bool start = false;

	public int score = 0;

	// Use this for initialization
	void Start () {
		start = true;
		SSDirector director = SSDirector.getInstance();
		director.currentScenceController = this;
		director.currentScenceController.LoadResources();
		user_gui = gameObject.AddComponent<UserGUI>() as UserGUI;
		actionManager = gameObject.AddComponent<CCArrowActionManager>();
		AF = gameObject.AddComponent<ArrowFactory>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void LoadResources(){
        Instantiate(Resources.Load("prefabs/Target"));//加载箭靶
		arrow = Instantiate(Resources.Load("prefabs/Arrow")) as GameObject;
		bow = Instantiate(Resources.Load("prefabs/Bow")) as GameObject;//加载弓
		arrow.transform.position = bow.transform.position;
		arrow.transform.parent = bow.transform;
    }


	public void hit(Vector3 pos){
		arrow = AF.getArrow(bow.transform.position);
		arrow.transform.parent = bow.transform;
		actionManager.Arrowfly(arrow, bow.transform.position);
	}

	public int getScore(){
		return score;
	}
    public void reStart(){
		score = 0;
		start = true;
	}

	public void endGame(){
		start = false;
	}

	public float getWind(){
		return actionManager.getWind();
	}
}
