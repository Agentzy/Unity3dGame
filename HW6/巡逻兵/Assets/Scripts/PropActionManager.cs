using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropActionManager : SSActionManager{

    public void prop_run(GameObject prop){
        //巡逻兵巡逻动作
        PropMoveAction prop_move = PropMoveAction.getAction(prop.transform.position);
        this.addAction(prop, prop_move, this);
    }

    //游戏结束时巡逻兵动作停止
    public void DestroyAllAction(){
        DestroyAll();
    }
}
