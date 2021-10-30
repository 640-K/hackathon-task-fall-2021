using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EventInvoker : MonoBehaviour
{
    public List<UnityEvent> eventToInvoke;

    void Invoke(int index) => eventToInvoke[index].Invoke();
}
