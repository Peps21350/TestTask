using UnityEngine.EventSystems;
using System;


public class ClickableBaseGlobal : MonoBehaviourSingleton<ClickableBaseGlobal>
{
  #region Events
  public event Action<ClickableBase, PointerEventData>   onAnyClickableClick                           = delegate {};
  public event Action<ClickableBase, PointerEventData>   onAnyClickableNotInteractableClick            = delegate {};
  public event Action<ClickableBase, PointerEventData>   onAnyClickableLongClick                       = delegate {};
  public event Action<ClickableBase, PointerEventData>   onAnyClickablePointerDown                     = delegate {};
  public event Action<ClickableBase, PointerEventData>   onAnyClickablePointerUp                       = delegate {};
  public event Action<ClickableBase, bool>               onAnyClickableInteractableChanged             = delegate {};
  public event Action<ClickableBase, PointerEventData>   onAnyClickablePointerDownIgnoreIsInteractable = delegate {};
  public event Action<ClickableBase, PointerEventData>   onAnyClickableClickOnNotAllowed
  {
    add => on_not_allowed_click_subscriptions.add( value );
    remove => on_not_allowed_click_subscriptions.remove( value );
  }
  #endregion

  #region Private Fields
  private readonly EventAction<ClickableBase, PointerEventData> on_not_allowed_click_subscriptions = new EventAction<ClickableBase, PointerEventData>();
  #endregion

  #region Public Fields
  public const int           UNDEFINED_TOUCH_ID = -1;
  public       int           activeTouchID                 { get; set; } = UNDEFINED_TOUCH_ID;
  public       ClickableBase activeInInteractionClickable { get; set; } = null;
  #endregion


  #region Public Methods
  public void notifyOnAnyClickableClick( ClickableBase clickable, PointerEventData pointer_event_data )
    => onAnyClickableClick( clickable, pointer_event_data );

  public void notifyOnAnyClickableNotInteractableClick( ClickableBase clickable, PointerEventData pointer_event_data )
    => onAnyClickableNotInteractableClick( clickable, pointer_event_data );

  public void notifyOnAnyClickableLongClick( ClickableBase clickable, PointerEventData pointer_event_data )
    => onAnyClickableLongClick( clickable, pointer_event_data );

  public void notifyOnAnyClickablePointerDown( ClickableBase clickable, PointerEventData pointer_event_data )
    => onAnyClickablePointerDown( clickable, pointer_event_data );

  public void notifyOnAnyClickablePointerUp( ClickableBase clickable, PointerEventData pointer_event_data )
    => onAnyClickablePointerUp( clickable, pointer_event_data );

  public void notifyOnAnyClickableInteractableChanged( ClickableBase clickable, bool is_interactable )
    => onAnyClickableInteractableChanged( clickable, is_interactable );

  public void notifyOnAnyClickablePointerDownIgnoreIsInteractable( ClickableBase clickable, PointerEventData pointer_event_data )
    => onAnyClickablePointerDownIgnoreIsInteractable( clickable, pointer_event_data );

  public void resetMultitouchCaches()
  {
    activeInInteractionClickable = null;
    activeTouchID = UNDEFINED_TOUCH_ID;
  }
  #endregion
}