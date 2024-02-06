using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using UnityEngine;


public class ClickableBaseGlobal : MonoBehaviourSingleton<ClickableBaseGlobal>
{
  #region Events
  public event Action<GlobalClickableAvailabilityState>  onGlobalClickableStateChanged                 = delegate {};
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

  private readonly HashSet<ClickableBase>           clickable_excludes           = new HashSet<ClickableBase>();
  private readonly HashSet<ClickableBase>           clickable_excludes_uncleared = new HashSet<ClickableBase>();
  private readonly HashSet<ClickableBase>           clickable_excludes_anyway    = new HashSet<ClickableBase>();
  private          GlobalClickableAvailabilityState clickable_availability_state = GlobalClickableAvailabilityState.AVAILABLE_ALL;
  #endregion

  #region Public Fields
  public const int           UNDEFINED_TOUCH_ID = -1;
  public       int           active_touch_id                 { get; set; } = UNDEFINED_TOUCH_ID;
  public       ClickableBase active_in_interaction_clickable { get; set; } = null;


  public GlobalClickableAvailabilityState clickableAvailabilityState => clickable_availability_state;
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

  public bool notifyOnAnyClickableClickOnNotAllowed( ClickableBase clickable, PointerEventData pointer_event_data )
  {
    on_not_allowed_click_subscriptions.call( clickable, pointer_event_data );
    return on_not_allowed_click_subscriptions.any;
  }

  /// <summary>
  /// Sets the global clickable state. Will clear clickable excludes on state change
  /// </summary>
  public void setGlobalClickableState( GlobalClickableAvailabilityState state )
  {
    Debug.LogWarning( $"changed global clickable state to {nameof( state )} {state}" );
    if ( clickable_availability_state == state )
      return;
    
    clearExcludes();
    clickable_availability_state = state;
    onGlobalClickableStateChanged( clickable_availability_state );
  }

  public void addToExclude( params ClickableBase[] clickables )
  {
    if ( clickables.Length == 0 )
      throw new Exception( $"Argument {nameof( clickables )} {clickables} contains no elements!" );

    foreach ( ClickableBase clickable_base in clickables )
      clickable_excludes.Add( clickable_base );
  }
  
  public void addToExcludeWithoutClearing( params ClickableBase[] clickables )
  {
    if ( clickables.Length == 0 )
      throw new Exception( $"Argument {nameof( clickables )} {clickables} contains no elements!" );

    foreach ( ClickableBase clickable_base in clickables )
      clickable_excludes_uncleared.Add( clickable_base );
  }

  public void addToExcludeAnyway( params ClickableBase[] clickables )
  {
    if ( clickables.Length == 0 )
      throw new Exception( $"Argument {nameof( clickables )} {clickables} contains no elements!" );

    foreach ( ClickableBase clickable_base in clickables )
      clickable_excludes_anyway.Add( clickable_base );
  }

  public bool removeFromExclude( ClickableBase clickable )
    => clickable_excludes.Remove( clickable );

  public void removeFromExclude( ClickableBase[] clickables )
  {
    if ( clickables.Length == 0 )
      throw new Exception( $"Argument {nameof( clickables )} {clickables} contains no elements!" );

    foreach ( ClickableBase clickable_base in clickables )
      clickable_excludes.Remove( clickable_base );
  }
  
  public void removeFromExcludeWithoutClearing( ClickableBase clickable )
    => clickable_excludes_uncleared.Remove( clickable );

  public bool isInExclude( ClickableBase clickable )
    => clickable_excludes.Contains( clickable ) || clickable_excludes_uncleared.Contains( clickable );

  public void clearExcludes()
    => clickable_excludes.Clear();

  public bool isInteractableGlobal( ClickableBase clickable )
  {
    if ( clickable_excludes_anyway.Contains( clickable ) )
      return true;

    switch ( clickable_availability_state )
    {
    case GlobalClickableAvailabilityState.DISABLE_ALL:                 return false;
    case GlobalClickableAvailabilityState.AVAILABLE_ALL:               return true;
    case GlobalClickableAvailabilityState.DISABLE_ALL_WITH_EXCLUDES:   return isInExclude( clickable );
    case GlobalClickableAvailabilityState.AVAILABLE_ALL_WITH_EXCLUDES: return !isInExclude( clickable );

    default: throw new ArgumentOutOfRangeException( nameof( clickable_availability_state ) );
    }
  }

  public void resetMultitouchCaches()
  {
    active_in_interaction_clickable = null;
    active_touch_id = UNDEFINED_TOUCH_ID;
  }
  #endregion
}


#region GlobalClickableAvailabilityState
public enum GlobalClickableAvailabilityState : byte
{
  DISABLE_ALL                 = 0,
  DISABLE_ALL_WITH_EXCLUDES   = 1,
  AVAILABLE_ALL               = 2,
  AVAILABLE_ALL_WITH_EXCLUDES = 3
}
#endregion
