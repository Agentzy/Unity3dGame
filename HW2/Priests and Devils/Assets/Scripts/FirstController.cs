using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mygame;

namespace mygame{
    //导演类
    public class SSDirector:System.Object{
        private static SSDirector _instance;//单例
        public ISceneController currentScenceController{get;set;}
        //获取单例
        public static SSDirector getInstance(){
            if(_instance == null){
                _instance = new SSDirector();
            }
            return _instance;
        }
    }

    public interface ISceneController{
        void LoadResources();//加载场景
    }

    //用户操作
    public interface UserAction{
        void moveBoat();//移动船
        void moveRole(RoleModel role);//移动对象
        void reStart();//重启游戏
        int CheckGameState();//检查游戏是否结束
    }

}

//创建场记
public class FirstController:MonoBehaviour, ISceneController{
    public CoastModel startCoast;
    public CoastModel endCoast;
    public BoatModel boat;
    public RoleModel[] roles;
    InterGUI inter_gui;

    Vector3 water_pos = new Vector3(0,0.5f,0);

    void Start(){
        SSDirector director = SSDirector.getInstance();
        director.currentScenceController = this;
        inter_gui = gameObject.AddComponent<InterGUI>() as InterGUI;
        LoadResources();
    }
    public void LoadResources(){
        //创建对象
        GameObject water = Instantiate(Resources.Load("Perfabs/water",typeof(GameObject)),water_pos,Quaternion.identity,null) as GameObject;
        water.name = "water";
        for(int i = 0;i < 3;i ++){
            RoleModel priest = new RoleModel("priest");
            priest.setName("priest" + i);
            priest.setPos(startCoast.getEmptyPos());
            priest.getOnCoast(startCoast);
            startCoast.getOnCoast(priest);
            roles[i] = priest;
        }

        for(int i = 0;i < 3;i ++){
            RoleModel devil = new RoleModel("devil");
            devil.setName("devil" + i);
            devil.setPos(startCoast.getEmptyPos());
            devil.getOnCoast(startCoast);
            startCoast.getOnCoast(devil);
            roles[i + 3] = devil;
        }
    } 

    public void moveBoat(){
        if(boat.isEmpty() || inter_gui.state != 0)
            return;
        boat.moveBoat();
        inter_gui.state = CheckGameState();
    }

    public void moveRole(RoleModel role){
        if(inter_gui.state != 0) 
            return;
            //role在船上
        if(role.isOnBoat()){
            CoastModel coast;
            //在结束位置
            if(boat.getBoatPos() == 1){
                coast = endCoast;
            }
            else
                coast = startCoast;
            boat.rolegetOffBoat(role.getName());
            role.setPos(boat.getEmptyPos());
            role.getOnCoast(coast);
            boat.rolegetOnBoat(role);
        }
        else{
            CoastModel coast = role.getCoast();
            //船无空位或没有停靠陆地
            if(boat.getEmptyIndex() == -1 || coast.getCoastSign() != boat.getBoatPos())
                return;
            coast.getOffCoast(role.getName());
            role.setPos(boat.getEmptyPos());
            role.getOnboat(boat);
            boat.rolegetOnBoat(role);
        }
        inter_gui.state = CheckGameState();
    }

    public void Restart(){
        startCoast.Reset();
        endCoast.Reset();
        boat.Reset();
        for(int i = 0;i < roles.Length;i ++){
            roles[i].Reset();
        }
    }

    public int CheckGameState(){
        int start_priest = (startCoast.getRoleNumber())[0];
        int start_devil = (startCoast.getRoleNumber())[1];
        int end_priest = (endCoast.getRoleNumber())[0];
        int end_devil = (endCoast.getRoleNumber())[1];

        if(end_priest == 3 && end_devil == 3)
            return 2;
        int [] boat_roles = boat.getRoleNumber();
        //船在开始岸
        if(boat.getBoatPos() == 0){
            start_priest += boat_roles[0];
            start_devil += boat_roles[1];
        }
        else{
            end_priest += boat_roles[0];
            end_devil += boat_roles[1];
        }

        if((start_priest > 0 && start_priest < start_devil) || (end_priest>0&&end_priest<end_devil))
            return 1;
        return 0;
    }
}