using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Signal : ScriptableObject
{
    public List<SignalListener> listenersList = new List<SignalListener>();
    public void RaiseSignal()
    {
        //going through list backwards to avoid raising errors
        for(int i = listenersList.Count -1; i >=0; i--)
        {
            listenersList[i].OnSignalRaised();
        }
    }
    public void RegisterListener (SignalListener listener)
    {
        listenersList.Add(listener);
    }

    public void DeregisterListener(SignalListener listener)
    {
        listenersList.Remove(listener);
    }
}
