using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mygame;

public class PDState {
	//左侧河岸的牧师和魔鬼数量
	public int start_priest;
    public int start_devil;
    public int boat_pos;//0表示开始岸，1表示结束岸
    public PDState() { }
    public PDState(int start_priest, int start_devil, int boat_priest, int boat_devil, int boat_pos){
        if(boat_pos == 1){
			this.start_priest = start_priest;
			this.start_devil = start_devil;
		}
		//需要统计船上的数量
		else{
			this.start_priest = start_priest + boat_priest;
			this.start_devil = start_devil + boat_devil;
		}
        this.boat_pos = boat_pos;
    }
    public BoatAction getNextBoatAction(){
		BoatAction next = BoatAction.empty;
		//船在左边的情况
		if(boat_pos == 0){
			if(start_priest == 3 && start_devil == 3){
				if(Random.Range(0f,1f) <= 0.5f){
					next = BoatAction.PD;
				}
				else{
					next = BoatAction.DD;
				}
			}
			if(start_priest == 3 && start_devil == 2){
				next = BoatAction.DD;
			}
			if(start_priest == 3 && start_devil == 1){
				next = BoatAction.PP;
			}
			if(start_priest == 2 && start_devil == 2){
				next = BoatAction.PP;
			}
			if(start_priest == 0 && start_devil == 3){
				next = BoatAction.DD;
			}
			if(start_priest == 2 && start_devil == 1){
				next = BoatAction.P;
			}
			if(start_priest == 0 && start_devil == 2){
				next = BoatAction.DD;
			}
			if(start_priest == 1 && start_devil == 1){
				next = BoatAction.PD;
			}
		}
		//船在右边的情况
		else{
			if(start_priest == 2 && start_devil == 2){
				next = BoatAction.P;
			}
			if(start_priest == 3 && start_devil == 2){
				next = BoatAction.D;
			}
			if(start_priest == 3 && start_devil == 1){
				next = BoatAction.D;
			}
			if(start_priest == 3 && start_devil == 0){
				next = BoatAction.D;
			}
			if(start_priest == 1 && start_devil == 1){
				next = BoatAction.PD;
			}
			if(start_priest == 0 && start_devil == 2){
				next = BoatAction.D;
			}
			if(start_priest == 0 && start_devil == 1){
				if(Random.Range(0f,1f) <= 0.5f){
					next = BoatAction.D;
				}
				else{
					next = BoatAction.P;
				}
			}
		}
		return next;
	}
}
