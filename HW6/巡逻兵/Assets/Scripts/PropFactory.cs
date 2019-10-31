using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropFactory : MonoBehaviour{
    private List<GameObject> used = new List<GameObject>();//巡逻兵列表

    int[] X_pos = {-6,4,12};
    int[] Z_pos = {-4,6,-12};

    public List<GameObject> GetPatrols(){

        for(int i = 0;i < 3; i ++){
			for(int j = 0;j < 3;j ++){
				GameObject prop = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/zombie"));
				prop.AddComponent<Prop>();
				prop.GetComponent<Prop>().block = i*3+j+1;
				prop.GetComponent<Prop>().is_follow = false;
				prop.transform.position = new Vector3(X_pos[i],0,Z_pos[j]);
				prop.SetActive(true);
				prop.GetComponent<Animator>().SetBool("run", true);
				used.Add(prop);
			}
		}
		return used;
    }

    public void PropInit(List<GameObject> props){
		for(int i = 0;i < 3;i ++){
			for(int j = 0;j < 3;j ++){
				props[i*3+j].transform.position = new Vector3(X_pos[i], 0, Z_pos[j]);
			}
		}
	}
}
