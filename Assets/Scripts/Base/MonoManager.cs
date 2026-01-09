using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoManager : SingleMonoBase<MonoManager>
{
    //定义事件
    public Action updateAction;
    public Action fixedUpdateAction;
    public Action lateUpdateAction;

    //订阅事件
    public void AddUpdateAction(Action action)
    {
        updateAction += action;
    }

    public void RemoveUpdateAction(Action action) { updateAction -= action; }

    public void AddFixedUpdateAction(Action action) { fixedUpdateAction += action; }

    public void RemoveFixedUpdateAction(Action action) { fixedUpdateAction -= action; }

    public void AddLateUpdateAction(Action action) { lateUpdateAction += action; }

    public void RemoveLateUpdateAction(Action action) { lateUpdateAction -= action; }

    //触发事件

    void Update()
    {
        updateAction?.Invoke();
    }

    void FixedUpdate()
    {
        fixedUpdateAction?.Invoke();
    }

    private void LateUpdate()
    {
        lateUpdateAction?.Invoke();
    }



}
