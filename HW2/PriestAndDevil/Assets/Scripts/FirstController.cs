using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mygame;


//创建场记
public class FirstController:MonoBehaviour, ISceneController, UserAction{
    public CoastModel startCoast;
    public CoastModel endCoast;
    public BoatModel boat;
    public RoleModel[] roles;
    InterGUI inter_gui;

    Vector3 water_pos = new Vector3(0,0.2F,0);

    void Awake(){
        SSDirector director = SSDirector.getInstance();
        //Debug.Log("222");
        director.currentScenceController = this;
        inter_gui = gameObject.AddComponent<InterGUI>() as InterGUI;
        LoadResources();
    }
    public void LoadResources(){
        //创建对象
        startCoast = new CoastModel("from");
        endCoast = new CoastModel("to");
        boat = new BoatModel();
        roles = new RoleModel[6];
        GameObject water = Instantiate(Resources.Load("Perfabs/water",typeof(GameObject)),water_pos,Quaternion.identity,null) as GameObject;
        water.name = "water";
        for(int i = 0;i < 3;i ++){
            RoleModel priest = new RoleModel("priest");
            priest.setName("priest" + i);
            //priest.setClick();
            //Debug.Log(startCoast.getEmptyIndex());
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
        //Debug.Log("222");
        if(boat.isEmpty() || inter_gui.state != 0)
            return;
        boat.moveBoat();
        //Debug.Log("222");
        inter_gui.state = CheckGameState();
        //Debug.Log(CheckGameState());
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
            role.setPos(coast.getEmptyPos());
            role.getOnCoast(coast);
            coast.getOnCoast(role);
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

    public void reStart(){
        startCoast.Reset();
        endCoast.Reset();
        //Debug.Log(boat.getBoatPos());
        boat.Reset();
        //boat.moveBoat();
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