using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class EventTriggerListener : EventTrigger
{
    public delegate void VoidDelegate();
    public delegate void VoidDelegate_(GameObject go, PointerEventData data);

    public VoidDelegate onClick;
    public VoidDelegate onDown;
    public VoidDelegate onEnter;
    public VoidDelegate onExit;
    public VoidDelegate onUp;
    public VoidDelegate onSelect;
    public VoidDelegate onUpdateSelect;
    public VoidDelegate onDrag;
    public VoidDelegate onMove;
    public VoidDelegate onBeginDrag;
  
    public VoidDelegate_ onClick_;
    public VoidDelegate_ onDrag_;
    public VoidDelegate_ onUp_;
    public VoidDelegate_ onEnter_;
    public VoidDelegate_ onExit_;
    public VoidDelegate_ onDown_;

    static public EventTriggerListener Get(GameObject go)
    {
        EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
        if (listener == null) listener = go.AddComponent<EventTriggerListener>();
        return listener;
    }
    static public EventTriggerListener Get(Transform transform)
    {
        EventTriggerListener listener = transform.GetComponent<EventTriggerListener>();
        if (listener == null) listener = transform.gameObject.AddComponent<EventTriggerListener>();
        return listener;
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null) onClick();
        if (onClick_ != null) onClick_(gameObject, eventData);
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null) onDown();
        if (onDown_ != null) onDown_(gameObject, eventData);
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null) onEnter();
        if (onEnter_ != null) onEnter_(gameObject, eventData);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (onExit != null) onExit();
        if (onExit_ != null) onExit_(gameObject, eventData);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null) onUp();
        if (onUp_ != null) onUp_(gameObject, eventData);
    }
    public override void OnSelect(BaseEventData eventData)
    {
        if (onSelect != null) onSelect();
    }
    public override void OnUpdateSelected(BaseEventData eventData)
    {
        if (onUpdateSelect != null) onUpdateSelect();
    }
    public override void OnDrag(PointerEventData eventData)
    {
        if (onDrag != null) onDrag();
        if (onDrag_ != null) onDrag_(gameObject, eventData);
    }
    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (onBeginDrag != null) onBeginDrag();
    }
    /// <summary>
    /// 清除所有事件
    /// </summary>
    public void ClearAllEvent()
    {
        onClick = null;
        onDown = null;
        onEnter = null;
        onExit = null;
        onUp = null;
        onSelect = null;
        onUpdateSelect = null;
        onDrag = null;
        onMove = null;

        onClick_ = null;
        onDrag_ = null;
        onUp_ = null;
        onEnter_ = null;
        onExit_ = null;
        onDown_ = null;
    }

}


