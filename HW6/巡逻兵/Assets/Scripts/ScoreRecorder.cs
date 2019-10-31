using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour{
    int score = 0;

    // Use this for initialization
    void Start(){
    }
    public int getScore(){
        return score;
    }

    public void setScore(int s){
        score = s;
    }
    public void AddScore(){
        score++;
    }
}

