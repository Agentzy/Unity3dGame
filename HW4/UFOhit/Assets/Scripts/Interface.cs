using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace myinterface{
    public interface ISceneController{
        void LoadResources();//加载场景
    }
    public enum SSActionEventType : int { Started, Competeted}

    //动作处理接口
    public interface ISSActionCallback{
        void SSActionEvent(SSAction source);
    }

    public interface UserAction{
        void hit(Vector3 vec);//用户点击飞碟
        int getScore();//获得分数
        void reStart();
        int getRound();
        void endGame();
    }
}