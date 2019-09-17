using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mygame{
    public class BoatModel{
        public GameObject boat;
        Move move;
        Vector3 [] fromPos;//开始位置
        Vector3 [] toPos;//结束位置
        RoleModel[] roles = new RoleModel[2];//船上最多两个角色
        int boat_pos=0;//开始位置0，结束位置1
        public BoatModel(){
            boat_pos = 0;
            fromPos = new Vector3[]{new Vector3(5F,1.8F,0), new Vector3(6F,1.8F,0)};
            toPos = new Vector3[]{new Vector3(-6F,1.8F,0),new Vector3(-5F,1.8F,0)};
            move = boat.AddComponent(typeof(Move)) as Move;
            boat.AddComponent(typeof(ClickGUI));
        }

        public void moveBoat(){
            if(boat_pos == 0){
                move.MoveTo(new Vector3(-5,1,0));
            }
            else if(boat_pos == 1){
                move.MoveTo(new Vector3(5,1,0));
            }
            boat_pos = 1-boat_pos;
        }

        public bool isEmpty(){
            for(int i = 0;i < roles.Length;i ++){
                if(roles[i] != null)
                    return false;
            }
            return true;
        }

        public int getEmptyIndex(){
            for(int i = 0;i < roles.Length;i ++){
                if(roles[i] == null)
                    return i;
            }
            return -1;
        }

        public Vector3 getEmptyPos(){
            if(boat_pos == 0){
                return fromPos[getEmptyIndex()];
            }
            else{
                return toPos[getEmptyIndex()];
            }
        }

        public void rolegetOnBoat(RoleModel role){
            roles[getEmptyIndex()] = role;
        }

        public RoleModel rolegetOffBoat(string name){
            for (int i = 0; i < roles.Length; i++) {
				if (roles[i] != null && roles[i].getName() == name) {
					RoleModel temp = roles[i];
					roles[i] = null;
					return temp;
				}
			}
			return null;
        }

        public int getBoatPos(){
            return boat_pos;
        }
        public GameObject getBoat(){
            return boat;
        }

        public void Reset(){
            if(boat_pos == 1)
                moveBoat();
            boat_pos = 0;
            move.reset();
        }

        public int[]getRoleNumber(){
            int[] count = {0,0};
            for(int i = 0;i < roles.Length; i ++){
                if(roles[i].getType() == 0){
                    count[0] ++;
                }
                else
                    count[1] ++;
            }
            return count;
        }
    }


    public class RoleModel{
        GameObject role;
        int roleType;//0为牧师1为魔鬼
        Move move;
        ClickGUI clickgui;
        CoastModel coast;
        bool onBoat = false;

        public RoleModel(string type){
            if(type == "priest"){
                role = Object.Instantiate(Resources.Load("Perfabs/Priest", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
                roleType = 0;
            }
            else if(type == "devil"){
                role = Object.Instantiate(Resources.Load("Perfabs/Devil", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
                roleType = 1;
            }
            else{
                Debug.Log("Error Type");
            }
            move = role.AddComponent(typeof(Move)) as Move;
            clickgui = role.AddComponent(typeof(ClickGUI)) as ClickGUI;
            clickgui.setRoleModel(this);
        }

        public string getName(){
            return role.name;
        }

        public void setName(string _name){
            role.name = _name;
        }

        public void setPos(Vector3 v){
            role.transform.position = v;
        }

        public int getType(){
            return roleType;
        }

        public bool isOnBoat(){
            return onBoat;
        }

        public CoastModel getCoast(){
            return coast;
        }

        public void getOnboat(BoatModel boat){
            coast = null;
            role.transform.parent = boat.getBoat().transform;
            onBoat = true;
        }

        public void getOnCoast(CoastModel coastmodel){
            coast = coastmodel;
            role.transform.parent = null;
            onBoat = false;
        }

        public void Reset(){
            move.reset();
            coast = (SSDirector.getInstance().currentScenceController as FirstController).startCoast;
            getOnCoast(coast);
            setPos(coast.getEmptyPos());
            coast.getOnCoast(this);
        }
    }

    public class CoastModel{
        GameObject coast;
        Vector3[] positions;
        int coastSign;//开始陆地0，结束陆地1
        RoleModel[] roles = new RoleModel[6];//3个魔鬼3个牧师
        public CoastModel(string state){
            positions = new Vector3[] {new Vector3(7F,2.3F,0), new Vector3(8F,2.3F,0), new Vector3(9F,2.3F,0), 
            new Vector3(10F,2.3F,0), new Vector3(11F,2.3F,0), new Vector3(12F,2.3F,0)};

            if(state == "from"){
                coast = Object.Instantiate (Resources.Load ("Perfabs/Stone", typeof(GameObject)), new Vector3(8,1,0), Quaternion.identity, null) as GameObject;
                coast.name = "from";
                coastSign = 0;
            }
            else if(state == "to"){
                coast = Object.Instantiate (Resources.Load ("Perfabs/Stone", typeof(GameObject)), new Vector3(-8,1,0), Quaternion.identity, null) as GameObject;
                coast.name = "to";
                coastSign = 1;
            }
            else{
                Debug.Log("Coast state error");
            }

        }

        public int getEmptyIndex(){
            for(int i = 0;i < roles.Length;i ++){
                if(roles[i] == null)
                    return i;
            }
            return -1;
        }

        public Vector3 getEmptyPos(){
            Vector3 pos = positions[getEmptyIndex()];
            if(coastSign == 1)
                pos.x = -pos.x;
            return pos;
        }

        public int getCoastSign(){
            return coastSign;
        }

        public void getOnCoast(RoleModel role){
            roles[getEmptyIndex()] = role;
        }

       public RoleModel getOffCoast(string roleName){
           for(int i = 0;i < roles.Length;i ++){
               if(roles[i] != null && roles[i].getName() == roleName){
                   RoleModel tmp = roles[i];
                   roles[i] = null;
                   return tmp;
               }
           }
           return null;
       } 

        public int [] getRoleNumber(){
            int[] count = {0,0};
            for(int i = 0;i < roles.Length; i ++){
                if(roles[i] != null){
                    if(roles[i].getType() == 0){
                        count[0] ++;
                    }
                    else
                        count[1] ++;
                }
            }
            return count;

        }

        public void Reset(){
            roles = new RoleModel[6];
        }

    }

    //移动状态
    public class Move{
        float speed = 30;
        int move_state = 0;//0表示不移动，1表示移动到中间，2表示移动到终点
        Vector3 middle;
        Vector3 end;

        void update(){
            if(move_state == 1){
                transform.position = Vector3.MoveTowards (transform.position, middle, speed * Time.deltaTime);
                //移动到中间后往目的地移动
                if(transform.position == middle)
                    move_state = 2;
            }
            else if(move_state == 2){
                transform.position = Vector3.MoveTowards(transform.position,end,speed*Time.deltaTime);
                //到目的地后停止
                if(transform.position == end) 
                    move_state = 0;
            }
        }

        //Boat和Role移动
        public void MoveTo(Vector3 dest){
            end = dest;
            middle = dest;//与角色相关有两个方向坐标与dest一致，只需根据条件该另一个坐标
            //船水平方向移动
            if(dest.y == transform.position.y){
                move_state = 2;
            }
            //Role从陆地移动到船
            else if(dest.y < transform.position.y){
                middle.y = transform.position.y;
            }
            //Role从船到陆地
            else{
                middle.x = transform.position.x;
            }
            move_state = 1;
        }

        public void reset(){
            move_state = 0;
        }
    }
}
