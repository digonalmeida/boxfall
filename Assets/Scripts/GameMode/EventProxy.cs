using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventProxy : MonoBehaviour
{
    private static EventProxy _instance;

    public static EventProxy Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = CreateInstance();
            }

            return _instance;
        }
    }

    private static EventProxy CreateInstance()
    {
        GameObject eventPryxoObject = new GameObject("_event_proxy", typeof(EventProxy));
        DontDestroyOnLoad(eventPryxoObject);

        return eventPryxoObject.GetComponent<EventProxy>();
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }
}
