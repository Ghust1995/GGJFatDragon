﻿using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;

public class CommandEventArgs : EventArgs
{
    public Command Command { get; set; }
}

public class Player : MonoBehaviour, IControllable {

    private bool _commandSuccess;
    public float timeLimitRange;
    public float timePace;
    private float _lastTime;
    private Command _command = new Command();
    private Command _lastCommand = new Command();
    private System.Random _random = new System.Random();
    private Array _valuesCommandTypes = Enum.GetValues(typeof(CommandType));

    public event EventHandler<CommandEventArgs> CommandEventHandler;
    
    // Use this for initialization
	void Start () {
        _lastTime = Time.time;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (_lastCommand != _command)
	    {
	        if (CommandEventHandler != null)
	        {
	            CommandEventHandler.Invoke(this, new CommandEventArgs()
	            {
	                Command = _command,
	            });
	        }
	    }
	    _lastCommand = _command;
	}

    public Command getRandomCommand()
    {
        CommandType randomRight = (CommandType)_valuesCommandTypes.GetValue(_random.Next(_valuesCommandTypes.Length));
        CommandType randomLeft = (CommandType)_valuesCommandTypes.GetValue(_random.Next(_valuesCommandTypes.Length));
        return new Command(randomLeft, randomRight);
    }

    public void ResolveCommandResult(bool result)
    {
        float actualTime = Time.time;
        bool didPlayerGetRight = false;
        if(result)
        {
            float timeDelta = actualTime - _lastTime;
            if(timePace - timeLimitRange/2 <= timeDelta && timeDelta <= timePace + timeLimitRange / 2)
            {
                didPlayerGetRight = true;
            }
            _lastTime = actualTime;
        }

        if(didPlayerGetRight)
        {
            Debug.Log("Success!");
        }
        else
        {
            Debug.Log("Fail!");
        }
    }

    public void MoveLeftSide(CommandType command, GameState state)
    {
        _command.Left = command;
    }

    public void MoveRightSide(CommandType command, GameState state)
    {
        _command.Right = command;
    }

    internal static Player Create(Vector3 initialPos)
    {
        var playerPrefab = Resources.Load<Player>("Prefabs/Player");

        var player = Instantiate<Player>(playerPrefab);
        player.transform.position = initialPos;


        return player;
    }
}