using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myinterface;

public class CCFlyActionManager : SSActionManger,ISSActionCallback {

    public SSActionEventType Complete = SSActionEventType.Competeted;
    
	public void SSActionEvent(SSAction source) {
        //count--;
        Complete = SSActionEventType.Competeted;
        source.gameObject.SetActive(false);
    }

	public void UFOfly(Disk disk){
		Complete = SSActionEventType.Started;
        CCFlyAction action = CCFlyAction.getAction(disk.Speed);
        addAction(disk.gameObject, action, this);//激活对象，添加动作
	}
}