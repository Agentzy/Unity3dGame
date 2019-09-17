using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mygame;

public class ClickGUI : MonoBehaviour
{
    private UserAction action;
    RoleModel role = null;//点击Role
    BoatModel boat = null;//点击Boat
    public void setBoatModel(BoatModel obj){
        boat = obj;
    }
    public void setRoleModel(RoleModel obj){
        role = obj;
    }

    void Start(){
        action = SSDirector.getInstance().currentScenceController as UserAction;
    }

    void OnMouseDown(){
        if(role == null && boat == null)
            return;
        if(boat != null){
            action.moveBoat();
            return;
        }
        if(role != null){
            action.moveRole(role);
            return;
        }
    }
}
