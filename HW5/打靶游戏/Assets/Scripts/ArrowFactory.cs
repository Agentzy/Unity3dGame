using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFactory : MonoBehaviour {
    private List<GameObject> used;//正在使用的箭
    private List<GameObject> free;//未激活的箭
	public static ArrowFactory AF;

	//public FirstController controller;
	public GameObject arrowPrefab = null;


	void Awake(){
		if(AF == null){
			used = new List<GameObject>();
			free = new List<GameObject>();
			AF = Singleton<ArrowFactory>.Instance;
		}
	}

	void Start () {
        //controller = SSDirector.getInstance().currentScenceController as FirstController;
		
		//controller.AF = this;
        arrowPrefab = Instantiate(Resources.Load("prefabs/Arrow")) as GameObject;
        arrowPrefab.SetActive(false); 
        arrowPrefab.transform.localEulerAngles = new Vector3(0, 90, 0);
        //free.Add(controller.arrow);
    }
    
    public GameObject getArrow(Vector3 pos) {
		freeArrow();
        GameObject arrow = null;
        if (free.Count == 0) {
            arrow = Instantiate(Resources.Load<GameObject>("Prefabs/Arrow"));
        }
        else {
            arrow = free[0];
			free.Remove(free[0]);
        }

		arrow.SetActive(true);
        used.Add(arrow);
		arrow.transform.position = pos;
		arrow.transform.localEulerAngles = arrowPrefab.transform.localEulerAngles;
		if(arrow.GetComponent<Arrow>() == null){
			arrow.AddComponent<Arrow>();
		}
        return arrow;
    }
    
	//回收箭
    public void freeArrow(){
        foreach (GameObject x in used){
            if ((!x.gameObject.activeSelf) || (!x.GetComponent<Arrow>().hit && x.transform.position.z > 20)){
                free.Add(x);
                used.Remove(x);
                return;
            }
			
        }
    }
}