using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneController{
        void LoadResources();//加载场景
}

public interface UserAction{
    void PlayerMove(float pos_X, float pos_Z);//玩家移动
    int getScore();//获得分数
    void reStart();
    bool isEndGame();
}

public interface ISSActionCallback{
    void SSActionEvent(SSAction source,int intParam = 0,GameObject objectParam = null);
}
