using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Comp_AI_Turtle : Comp_Base_Enemy
{
    
    //estats
    //nadar
    //disparar tirachines: s'aturdeix i la seva velocitat disminueix
    //bomba: s'enfonsa
    public Animator anim;
    float timer;
    void Awake()
    {
        InitAwake();
        anim = GetComponent<Animator>();
    }

    void Start() {
        speed = -4;
    }
    void Update() {
        if (state == 0) {   //nadar
            advance();
        } else if (state == 1) { //collisio
            isDeading();
        } else if (state == 2) { //muerto
            disable();
        }
    }


    public void isDeading() {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("hit")) {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f) {
                state = 2;
            }
        }
        advance(speed/2f);

    }

    void OnTriggerEnter(Collider other) {
        if (state == 0 && other.gameObject.name == "Player") {
            Debug.Log("xoca contra el player");
            state = 1;
            anim.SetBool("receiveHit", true);
            timer = anim.GetNextAnimatorStateInfo(0).length;
        }
    }
    
}

