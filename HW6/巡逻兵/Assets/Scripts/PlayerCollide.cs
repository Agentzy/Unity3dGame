using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollide : MonoBehaviour{

    void OnCollisionEnter(Collision other){
        //玩家和巡逻兵碰撞触发死亡动画和游戏结束订阅
        if (other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<Animator>().SetTrigger("death");
            this.GetComponent<Animator>().SetTrigger("attack");//玩家死亡和巡逻兵攻击动画触发
            Singleton<GameEventManager>.Instance.gameOver();
        }
    }
}
