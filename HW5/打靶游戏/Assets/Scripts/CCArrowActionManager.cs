using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myInterface;

public class CCArrowActionManager : SSActionManger,ISSActionCallback,IActionManager {


    public float wind = 0;

    protected new void Update(){
        base.Update();
    }
    
	public void SSActionEvent(SSAction source) {
        source.gameObject.SetActive(false);
        source.gameObject.GetComponent<Arrow>().hit = false;
    }

    public void Arrowfly(GameObject arrow,Vector3 pos){
        arrow.transform.parent = null;
        CCArrowAction action = CCArrowAction.GetSSAction();
        arrow.transform.position = pos;
        this.addAction(arrow,action,this);
        arrow.GetComponent<Arrow>().action = action;
        wind = Random.Range(-5f,5f);
    }

    public float getWind(){
        return wind;
    }
}