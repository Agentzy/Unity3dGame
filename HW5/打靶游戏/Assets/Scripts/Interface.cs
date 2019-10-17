using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace myInterface {

	public interface ISceneController{
        void LoadResources();//加载场景
		float getWind();//风力获取
    }

	public enum SSActionEventType : int { Started, Competeted}
	 //动作处理接口
    public interface ISSActionCallback{
        void SSActionEvent(SSAction source);
    }

	//箭飞行动作
	public interface IActionManager{
        void Arrowfly(GameObject arrow, Vector3 pos);
		float getWind();
    }

	public interface UserAction{
        void hit(Vector3 vec);
        int getScore();//获得分数
        void reStart();
        void endGame();
    }
}

