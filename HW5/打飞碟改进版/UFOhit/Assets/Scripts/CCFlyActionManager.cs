﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myinterface;

public class CCFlyActionManager : SSActionManger,ISSActionCallback,IActionManager {

    public SSActionEventType Complete = SSActionEventType.Competeted;
    
	public void SSActionEvent(SSAction source) {
        Complete = SSActionEventType.Competeted;
        source.gameObject.SetActive(false);
    }

	public void UFOfly(GameObject disk){
		Complete = SSActionEventType.Started;
        CCFlyAction action = CCFlyAction.getAction(disk.GetComponent<Disk>().Speed);
        addAction(disk, action, this);//激活对象，添加动作
	}
}