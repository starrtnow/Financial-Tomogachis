using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class State
{
    public abstract void Update();
    public abstract void Enter();
}

public class Waiter
{

    private float _duration;
    private float _timeLeft;
    public Waiter(float duration)
    {
        _duration = duration;
        _timeLeft = duration;
    }

    public void Update()
    {
        _timeLeft -= Time.deltaTime;
    }

    public Boolean Done()
    {
        return _timeLeft <= 0;
    }
}
public class IdleState : State
{
    private Waiter _waiter;
    public float maxWaitTime = 2;
    private System.Random rand;

    private AI _cat;
    public IdleState(AI cat)
    {
        _cat = cat;

        rand = new System.Random();
        var duration = rand.Next(50, (int)maxWaitTime * 100) / 100f;
        _waiter = new Waiter(duration);
    }

    public override void Enter()
    {
        
    }

    public override void Update()
    {
        _waiter.Update();
        if (_waiter.Done())
        {
            var walk = new WalkState(_cat, rand.Next(0, 2));
            _cat.Switch(walk);
        }
    }
}

public class SleepState : State
{

    private AI _cat;
    private Waiter _waiter;
    private System.Random rand;

    public SleepState(AI cat)
    {
        rand = new System.Random();
        _cat = cat;

        var duration = rand.Next(2, 13);
        _waiter = new Waiter(duration);

    }

    public override void Update()
    {
        _waiter.Update();
        if (_waiter.Done())
        {
            _cat.Switch(new IdleState(_cat));
        }
    }

    public override void Enter()
    {
        _cat.catSprite.sprite = _cat.sleepSprite;
    }
}

public class WalkState : State
{

    public int Direction;
    public float MaxWalkDuration;
    private AI _cat;

    private float _duration;
    private System.Random rand;

    private Waiter waiter;

    public PointLogic Points;

    public WalkState(AI cat, int dir = 0)
    {
        MaxWalkDuration = 2;
        Direction = dir;
        rand = new System.Random();
        _cat = cat;
        _duration = rand.Next(50, (int)MaxWalkDuration * 100) / 100f;

        waiter = new Waiter(_duration);
    }

    public override void Enter()
    {
        if (Direction == 0)
        {
            _cat.catSprite.sprite = _cat.rightSprite;
        }
        else
        {
            _cat.catSprite.sprite = _cat.leftSprite;
        }
    }

    public override void Update()
    {
        waiter.Update();
        if (waiter.Done()){
            _cat.Switch(new IdleState(_cat));
        }

        if (Direction == 0)
        {
            _cat.MoveRight();
        }
        else
        {
            _cat.MoveLeft();
        }
    }

}
public class AI : MonoBehaviour {

    // Use this for initialization
    private State catState;
    public float Speed = 1;

    public float LeftBoundary;
    public float RightBoundary;

    public PointLogic point;

    public Sprite leftSprite;
    public Sprite rightSprite;
    public Sprite sleepSprite;

    public CatImages sad0;
    public CatImages sad1;
    public CatImages sad2;
    public CatImages sad3;

    public SpriteRenderer catSprite;

	void Start () {
        catState = new SleepState(this);
        Switch(new SleepState(this));
        LeftBoundary = -2.15f;
        RightBoundary = 2.15f;
	}
	
	// Update is called once per frame
	void Update () {
        catState.Update();
        HealthUpdate();
	}

    public void Switch(State state)
    {
        catState = state;
        catState.Enter();
    }

    public Boolean InBoundary()
    {
        return transform.position.x < LeftBoundary || transform.position.x > RightBoundary;
    }

    private float GetSpeed()
    {
        return Math.Max(point.Health / 100f, 0.0f);
    }

    public void HealthUpdate()
    {
        Speed = GetSpeed();

        if (point.Health > 80)
        {
            leftSprite = sad0.left;
            rightSprite = sad0.right;
        }
        else if (point.Health > 60)
        {
            leftSprite = sad1.left;
            rightSprite = sad1.right;
        }
        else if (point.Health > 40)
        {
            leftSprite = sad2.left;
            rightSprite = sad2.right;
        }
        else
        {
            leftSprite = sad3.left;
            rightSprite = sad3.right;
        }
    }

    public void MoveLeft()
    {
        print(LeftBoundary);
        if (transform.position.x > LeftBoundary)
        {
            transform.position += -transform.right * Speed * Time.deltaTime;
        }
    }

    public void MoveRight()
    {
        if(transform.position.x < RightBoundary)
        {
            transform.position += transform.right * Speed * Time.deltaTime;
        }
    }
    
}
