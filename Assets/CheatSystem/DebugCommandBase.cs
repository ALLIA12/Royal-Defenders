using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DebugCommandBase
{
    private string _commandId;
    private string _commandDescreption;
    private string _commandFormat;
    public string commandId { get { return _commandId; } }
    public string commandDescreption { get { return _commandDescreption; } }
    public string commandFormat { get { return _commandFormat; } }
    public DebugCommandBase(string commandId, string commandDescreption, string commandFormat)
    {
        _commandId = commandId;
        _commandDescreption = commandDescreption;
        _commandFormat = commandFormat;
    }
}
public class DebugCommand : DebugCommandBase
{
    private Action command;
    public DebugCommand(string id, string descreption, string format, Action command) : base(id, descreption, format)
    {
        this.command = command;
    }
    public void Invoke()
    {
        command.Invoke();
    }
}
