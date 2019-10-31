using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropCollide : MonoBehaviour {
    //玩家和巡逻兵装载的BoxCollide碰撞
    void OnTriggerEnter(Collider collider){
        if (collider.gameObject.tag == "Player"){
            this.gameObject.transform.parent.GetComponent<Prop>().is_follow = true;
            this.gameObject.transform.parent.GetComponent<Prop>().player = collider.gameObject;
        }
    }

    //玩家逃脱后属性改变,切换回巡逻动作
    void OnTriggerExit(Collider collider){
        if (collider.gameObject.tag == "Player"){
            this.gameObject.transform.parent.GetComponent<Prop>().is_follow = false;
            this.gameObject.transform.parent.GetComponent<Prop>().player = null;
            //玩家逃脱
            Singleton<GameEventManager>.Instance.PlayerEscape();
        }
    }
}
