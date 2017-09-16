using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class State
{
    public abstract void Update();
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

public class WalkState : State
{

    public int Direction;
    public float MaxWalkDuration;
    private AI _cat;

    private float _duration;
    private System.Random rand;

    private Waiter waiter;

    public WalkState(AI cat, int dir = 0)
    {
        MaxWalkDuration = 2;
        Direction = dir;
        rand = new System.Random();
        _cat = cat;
        _duration = rand.Next(50, (int)MaxWalkDuration * 100) / 100f;

        waiter = new Waiter(_duration);
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
    public int Speed = 10;

    public float LeftBoundary;
    public float RightBoundary;
	void Start () {
        catState = new IdleState(this);
        LeftBoundary = -1.2f;
        RightBoundary = 1.2f;
	}
	
	// Update is called once per frame
	void Update () {
        catState.Update();
	}

    public void Switch(State state)
    {
        catState = state;
    }

    public Boolean InBoundary()
    {
        return transform.position.x < LeftBoundary || transform.position.x > RightBoundary;
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
