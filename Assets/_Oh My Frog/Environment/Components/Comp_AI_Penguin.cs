using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Comp_AI_Penguin : Comp_Base_Enemy
{
    
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