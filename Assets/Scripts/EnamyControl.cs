using Assets.Scripts;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class EnamyControl : MPC
{
    public enum EnamyStats
    {
        Idel,
        Fite,
        Daying,
    }
    public PlayerControl player;
    public int _damage = 1;
    static Animator anim;
    public EnamyStats currentStates;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentStates = EnamyStats.Idel;
        Ray2D ray = new Ray2D();
        RaycastHit2D raycast2D = new RaycastHit2D();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentStates = EnamyStats.Fite;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        currentStates = EnamyStats.Idel;
    }
    private void Idel()
    {
        anim.SetInteger("States", 0);
    }
    private void Fite()
    {
        anim.SetInteger("States", 1);
        player._heroLives = player._heroLives - _damage;
        if (player._heroLives > 0)
            return;
        else if (player._heroLives == 0)
            currentStates = EnamyStats.Idel;
    }
    // Update is called once per frame
    void Update()
    {
        switch (currentStates)
        {
            case EnamyStats.Idel:
                Idel();
                break;
            case EnamyStats.Fite:
                Fite();
                break;
            case EnamyStats.Daying:
                Debug.Log(3);
                break;
        }
    }
}
