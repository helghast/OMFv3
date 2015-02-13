using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Comp_AI_Penguin : Comp_Base_Enemy
{
    
    //estats
    //nadar
    //disparar tirachines: s'aturdeix i la seva velocitat disminueix
    //bomba: s'enfonsa

    void Awake()
    {
        InitAwake();
    }

    void Start() {
        speed = -4;
    }
    void Update() {
        advance();
    }

}