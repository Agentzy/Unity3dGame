using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour, ISSActionCallback{
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
			if(ac.destroy){
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
 
    public void addAction(GameObject gameObject,SSAction action, ISSActionCallback callback){
		action.gameObject = gameObject;
		action.transform = gameObject.transform;
		action.callback = callback;
		waitingToAdd.Add(action);
		action.Start();
	}

    public void SSActionEvent(SSAction source, int intParam = 0, GameObject objectParam = null){
        //跟随动作
        if(intParam == 0){
            PropFollowAction follow = PropFollowAction.getAction(objectParam.gameObject.GetComponent<Prop>().player);
            this.addAction(objectParam, follow, this);
        }
        //玩家逃脱重新开始巡逻
        else{
            PropMoveAction move = PropMoveAction.getAction(objectParam.gameObject.GetComponent<Prop>().start_position);
            this.addAction(objectParam, move, this);
        }
    }

    // 清空动作
    public void DestroyAll(){
        foreach (KeyValuePair<int, SSAction> kv in actions){
            SSAction ac = kv.Value;
            ac.destroy = true;
        }
    }
}
