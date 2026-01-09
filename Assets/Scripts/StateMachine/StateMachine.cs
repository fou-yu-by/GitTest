using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StateMachine
{
    //当前状态
    private StateBase currentState;

    //状态机字典
    private Dictionary<Type, StateBase> stateDictionary = new Dictionary<Type, StateBase>();

    //宿主接口变量
    private IStateMachineOwner owner;

    public StateMachine(IStateMachineOwner owner)
    {
        Init(owner);
    }

    private void Init(IStateMachineOwner owner)
    {
        this.owner = owner;
    }

    /// <summary>
    /// 进入指定状态，若没有则创建状态
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="isReload">是否可重载</param>
   public void EnterState<T>(bool isReload = false) where T : StateBase,new()
    {
        if(currentState != null && currentState.GetType() == typeof(T) && isReload == false)
        {
            return;
        }
        if(currentState != null){
            ExitCurrentState();
        }
        currentState = LoadState<T>();
        EnterCurrentState();
    }

    /// <summary>
    /// 创建状态加入字典
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public StateBase LoadState<T>() where T : StateBase, new()
    {
        Type type = typeof(T);
        if(!stateDictionary.TryGetValue(type, out StateBase state))
        {
            state = new T();
            state.Init(owner);
            stateDictionary.Add(type, state);
            
        }
        return state;
    }

    /// <summary>
    /// 退出当前状态机
    /// </summary>
    public void ExitCurrentState()
    {
        currentState.OnExit();
        MonoManager.Instance.RemoveUpdateAction(currentState.Update);
        MonoManager.Instance.RemoveFixedUpdateAction(currentState.FixedUpdate);
        MonoManager.Instance.RemoveLateUpdateAction(currentState.LateUpdate);
    }


    /// <summary>
    /// 进入当前状态机
    /// </summary>
    public void EnterCurrentState()
    {
        currentState.OnEnter();

        MonoManager.Instance.AddUpdateAction(currentState.Update);
        MonoManager.Instance.AddLateUpdateAction(currentState.LateUpdate);
        MonoManager.Instance.AddFixedUpdateAction(currentState.FixedUpdate);
    }


    /// <summary>
    /// 清空状态机
    /// </summary>
    public void Clear()
    {
        ExitCurrentState();
        currentState = null;
        stateDictionary.Clear();
    }


}
