using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public int score;
    void Start (){
        score = 0;
    }
    public void Record(Disk disk){
        score +=  disk.score;
    }
    public void Reset(){
        score = 0;
    }
}
