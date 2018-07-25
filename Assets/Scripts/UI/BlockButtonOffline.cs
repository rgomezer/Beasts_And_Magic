using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class BlockButtonOffline : EventTrigger
{

    private PlayerAttackOffline pBlock;
    private EventTrigger pTrigger;
    private UnityAction<BaseEventData> call;
  
    // Use this for initialization
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Initialize()
    {
        pTrigger = GameObject.Find("/Canvas/BlockButton").GetComponent<EventTrigger>();
        Debug.Assert(pTrigger != null);

        pBlock = GetComponent<PlayerAttackOffline>();
        Debug.Assert(pBlock != null);

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        call = new UnityAction<BaseEventData>(PointerDown);
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(call);
        pTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        call = new UnityAction<BaseEventData>(PointerUp);
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(call);
        pTrigger.triggers.Add(entry);
    }

    private void PointerDown(BaseEventData eventData)
    {
        //Debug.Log("pointer enter");
        pBlock.Block();
    }
    private void PointerUp(BaseEventData eventData)
    {
        //Debug.Log("pointer exit");
        pBlock.StopBlock();
    }
}
