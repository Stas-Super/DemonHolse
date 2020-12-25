using Boo.Lang.Runtime.DynamicDispatching;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.Audio;
using Assets.Scripts;

public class PlayerControl : MPC
{
    #region Variables
    public int _heroLives;

    /// <summary>
    /// Длина прыжка
    /// </summary>
    public int jumpLength;


    /// <summary>
    /// Высота прыжка
    /// </summary>
    public int jumpHeight;

    /// <summary>
    /// Время перед супер ударом
    /// </summary>
    public int timeForSuperAtack;

    /// <summary>
    /// Время на прыжок 
    /// </summary>
    public float TimeForJump;
    public float TimeToNextJump = 1.0f;

    /// <summary>
    /// Время сколько должна длиться отака
    /// </summary>
    [SerializeField] float timeForAtack;

    /// <summary>
    /// Время перед следующей атакой
    /// </summary>
    [SerializeField] float timeToNextAtack=1.0f;

    /// <summary>
    /// Джойстик для упровления персоонажем
    /// </summary>
    public Djoystick djoy;

    /// <summary>
    /// Супер Атака
    /// </summary>
    public bool superAtack = false;

    /// <summary>
    /// Звук бега
    /// </summary>
    public AudioClip _playerWalk;

    public AudioClip _playerFite;
    /// <summary>
    /// Скорость с которой должен двигаться персоонаж
    /// </summary>
    public float speed = 0;
   
    public float move = 0;

    public AudioClip playerRun;
    public AudioClip s_playerWalk;
    public AudioClip playerFit;
    /// <summary>
    /// Обработка события клик по кнопке "Атака"
    /// </summary>
    public bool isAtack = false;

    /// <summary>
    /// Аниматор
    /// </summary>
    private Animator ani;

    /// <summary>
    /// физика персоонажа
    /// </summary>
    private Rigidbody2D rg;

    /// <summary>
    /// Отоброжение Персоонажа
    /// </summary>
    private SpriteRenderer sr;

    /// <summary>
    /// Супер Атака
    /// </summary>
    private bool isSuperAtack = false;
    public float maxSpeed = 10f;
    private bool isFacingRight = true;
    public AudioSource audioSourse;
    public int speedSide;
    /// <summary>
    /// Хронит в себе время пред тем как персоонаж начнет бежать
    /// </summary>
    public float _timeForRun = 0;
    #endregion
    public void Start()
    {
        _lives = _heroLives;
        sr = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
        rg = GetComponent<Rigidbody2D>();
    }
    bool isJump = false;
    bool isRun = false;
    [SerializeField] bool isDead = false;

    /// <summary>
    /// Это обработчик клика по виртуальной кнопке удар
    /// </summary>
    public void HideButton()
    {
        audioSourse.clip = playerFit;
        isAtack = true;
        ani.SetInteger("States", 2);
        audioSourse.Play();
    }
    /// <summary>
    /// Метод который возвращает жизни
    /// </summary>
    /// <returns>Жизни</returns>
    protected override void Daying()
    {
        isDead = true;
        ani.SetInteger("Lives", 0);
        Debug.Log("Dead");
    }
    /// <summary>
    /// Это обработчик события клик по кнопке "прыжок"
    /// </summary>

    public void JumpButton()
    {
        isJump = true;
        transform.Translate(jumpLength, jumpHeight, 0);
        ani.SetInteger("States", 3);
    }

    /// <summary>
    /// Метод каторый выполняет анимацию удара
    /// </summary>
    public void SuperHide()
    {   
        isSuperAtack = true;
        ani.SetInteger("States", 1);
    }
    /// <summary>
    /// Отзеркаливание персоонажа
    /// </summary>
    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    public void Update()
    {
        if (djoy.isUsed)
        {
            _timeForRun += Time.deltaTime;
            if(_timeForRun > 2)
            {
                ani.SetInteger("States", 4);
                Debug.Log(_timeForRun);
                Debug.Log(speedSide);
            }
            isRun = true;
            audioSourse.clip = _playerWalk;
            speedSide = 1;
            ani.SetInteger("States", 4);
            ani.SetFloat("Speed", Mathf.Abs(djoy.muveVector / 100));
            audioSourse.loop = true;
            if (audioSourse.isPlaying == false)
                audioSourse.Play();
            if (djoy.muveVector > 0)
            {
                speedSide = 1;
                if (sr.flipX == true)
                    sr.flipX = false;
            }
            else if (djoy.muveVector < 0)
            { 
                speedSide = -1;
                if (sr.flipX == false)
                    sr.flipX = true;
            }
            transform.Translate(speedSide/20.0f, 0, 0);
            if (Time.deltaTime == 3f)
            {
                transform.Translate(speedSide + 3 / 20.0f, 0, 0);
                ani.SetFloat("Speed", 1.5f);
            }
            Debug.Log(speedSide + "Скорость бега");
            Debug.Log(speed + "Сорость");
        }
        if (!djoy.isUsed && !isJump && !isAtack)
        {
            _timeForRun = 0;
            ani.SetFloat("Speed", 0);
            ani.SetInteger("States", 0);
            audioSourse.loop = false;
            transform.Translate(0, 0, 0);
        }
        if (isJump)
        {
            TimeForJump += Time.deltaTime;
            if (TimeForJump > TimeToNextJump)
            {
                TimeForJump = 0;
                isJump = false;
                ani.SetInteger("States", 0);
            }

        }
        else if (isAtack)
        {
            timeForAtack += Time.deltaTime;
            Debug.Log(timeForAtack);
            if (timeForAtack >= timeToNextAtack)
            {
                timeForAtack = 0;
                isAtack = false;
                ani.SetInteger("States", 0);
            }
        }
        else if (isSuperAtack)
        {
            timeForAtack = Time.deltaTime;
            if (timeForAtack >= timeToNextAtack)
            {
                timeForAtack = 0;
                isSuperAtack = false;
                ani.SetInteger("States", 0);
                Debug.Log("Is Work");
            }
        }
        else if (_heroLives == 0)
        {
            Daying();
        }
        if (!isJump) return;
        else if (!isAtack) return;
        else if (!isSuperAtack) return;

    }
} 
