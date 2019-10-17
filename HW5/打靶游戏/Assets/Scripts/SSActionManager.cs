using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myInterface;

public class SSActionManger:MonoBehaviour{
	private Dictionary<int,SSAction> actions = new Dictionary<int, SSAction>();//将执行的动作
	private List<SSAction> waitingToAdd = new List<SSAction>();
	private List<int> waitingToDelete = new List<int>();

	protected void Update(){
		foreach (SSAction ac in waitingToAdd){
			actions[ac.GetInstanceID()] = ac;
		}
		waitingToAdd.Clear();
		foreach (KeyValuePair<int,SSAction> kv in actions){
			SSAction ac = kv.Value;
			if(ac.destory){
				waitingToDelete.Add(ac.GetInstanceID());
			}
			else if (ac.enable){
				ac.Update();
			}
		}
		foreach(int key in waitingToDelete){
				SSAction ac = actions[key];
				actions.Remove(key);
				DestroyObject(ac);
		}
		waitingToDelete.Clear();
	}
	//添加动作并运行
	public void addAction(GameObject gameObject,SSAction action, ISSActionCallback callback){
		action.gameObject = gameObject;
		action.transform = gameObject.transform;
		action.callback = callback;
		waitingToAdd.Add(action);
		action.Start();
	}
}
