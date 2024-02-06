using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <notice>
/// Order of events execution:
/// 1. OnPointerEnter
/// 2. OnPointerDown
/// 3. OnBeginDrag
/// 4. OnDrag
/// 5. OnPointerUp
/// 6. OnPointerClick
/// 7. OnEndDrag
///
/// In any time when mouse exit rect of clickable: OnPointerExit
/// </notice>
public partial class ClickableBase : MonoBehaviourBase, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
  #region Public Events
  public event Action<PointerEventData> onClick
  {
    add
    {
      if ( on_click_subscriptions == null )
        on_click_subscriptions = new EventAction<PointerEventData>();

      on_click_subscriptions.add( value );
    }
    remove
    {
      if ( on_click_subscriptions == null )
        return;

      on_click_subscriptions.remove( value );
    }
  }
  public event Action<PointerEventData> onNotInteractableClick
  {
    add
    {
      if ( on_not_interactable_click_subscriptions == null )
        on_not_interactable_click_subscriptions = new EventAction<PointerEventData>();

      on_not_interactable_click_subscriptions.add( value );
    }
    remove
    {
      if ( on_not_interactable_click_subscriptions == null )
        return;

      on_not_interactable_click_subscriptions.remove( value );
    }
  }
  public event Action<PointerEventData> onLongClick
  {
    add
    {
      if ( on_long_click_subscriptions == null )
        on_long_click_subscriptions = new EventAction<PointerEventData>();

      on_long_click_subscriptions.add( value );
    }
    remove
    {
      if ( on_long_click_subscriptions == null )
        return;

      on_long_click_subscriptions.remove( value );
    }
  }
  public event Action<PointerEventData> onPointerDown                    = null;
  public event Action<PointerEventData> onPointerEnter                   = null;
  public event Action<PointerEventData> onPointerUp                      = null;
  public event Action<PointerEventData> onPointerExit                    = null;
  public event Action<bool>             onInteractableChanged            = null;
  public event Action<bool>             onInteractableInHierarchyChanged = null;
  #endregion

  #region Enum Long Click Behaviour
  public enum LongPressBehaviour : byte
  {
    SINGLE_ACTION = 0,
    REPEAT_CLICKS = 1,
    NO_ACTION     = 2
  }
  #endregion

  #region Serialize Fields
  [HideInInspector, SerializeField] private bool               is_multitouch_supported                   = false;
  [HideInInspector, SerializeField] private bool               is_handle_click_only_in_place_where_taped = false;
  [HideInInspector, SerializeField] private LongPressBehaviour long_press_behaviour                      = LongPressBehaviour.SINGLE_ACTION;
  [HideInInspector, SerializeField] private float              long_click_delay                          = 0.6f;

  [Header( "Repeat Clicks settings" )]
  [HideInInspector, SerializeField, Range( 2, 20 )] private byte approximate_max_clicks_per_sec  = 5;
  [HideInInspector, SerializeField, Range( 1, 5 )]  private byte time_when_max_clicks            = 3;
  [HideInInspector, SerializeField]                 private AnimationCurve from_min_to_max_curve = null;
  #endregion

  #region Private Fields
  private static readonly AnimationCurve default_from_min_to_max_curve = AnimationCurve.Linear( 0.0f, 0.0f, 1.0f, 1.0f );

  private EventAction<PointerEventData> on_click_subscriptions                  = null;
  private EventAction<PointerEventData> on_long_click_subscriptions             = null;
  private EventAction<PointerEventData> on_not_interactable_click_subscriptions = null;

  private readonly List<CanvasGroup> canvas_group_caches           = new List<CanvasGroup>();
  private          bool              is_long_click_action_executed = false;
  private          bool              is_interactable               = true;
  private          bool              is_interactable_in_hierarchy  = true;

  private bool snapshot_interactable               = false;
  private bool snapshot_interactable_in_hierarchy  = false;
  private bool snapshot_interactable_global        = false;
  private bool snapshot_interactable_by_multitouch = false;
  #endregion

  #region Public Fields
  public bool hasOnClickSubscriptions => on_click_subscriptions?.any ?? false;
  #endregion


  #region initMyComponents Methods
  protected override void initMyComponents()
  {
    base.initMyComponents();

    notifyInteractableChanged();
  }
  #endregion

  #region Unity Methods
  void IPointerDownHandler.OnPointerDown( PointerEventData pointer_data )
  {
    if ( !canUseClickableGlobal )
      return;

    onPhysicallyDown();

    initInteraction( pointer_data );
    clickableGlobal.notifyOnAnyClickablePointerDownIgnoreIsInteractable( this, pointer_data );
    if ( !canContinueInteraction )
      return;

    clickableGlobal.notifyOnAnyClickablePointerDown( this, pointer_data );
    onPointerDown?.Invoke( pointer_data );
  }

  void IPointerUpHandler.OnPointerUp( PointerEventData pointer_data )
  {
    if ( !canUseClickableGlobal )
      return;

    onPhysicallyUp();

    stopLongClickCoroutine();
    if ( !canContinueInteraction )
      return;

    clickableGlobal.notifyOnAnyClickablePointerUp( this, pointer_data );
    onPointerUp?.Invoke( pointer_data );
  }

  void IPointerClickHandler.OnPointerClick( PointerEventData pointer_data )
  {
    if ( !canUseClickableGlobal || !clickableGlobal.active_in_interaction_clickable )
      return;

    if ( !snapshot_interactable_global )
      simulateGlobalNotAllowedClick( pointer_data );
    else
    if ( !snapshot_interactable || !snapshot_interactable_in_hierarchy )
      simulateNotInteractableClick( pointer_data );
    else
    if ( snapshot_interactable_by_multitouch && !is_long_click_action_executed && ( !is_handle_click_only_in_place_where_taped || !pointer_data.dragging ) )
      simulateClick( pointer_data );

    endInteraction();
  }

  void IPointerEnterHandler.OnPointerEnter( PointerEventData pointer_data )
  {
    onPointerEnter?.Invoke( pointer_data );
  }

  void IPointerExitHandler.OnPointerExit( PointerEventData pointer_data )
  {
    onPhysicallyExit();

    stopLongClickCoroutine();
    onPointerExit?.Invoke( pointer_data );

    endInteraction();
  }

  private void OnCanvasGroupChanged()
  {
    initComponents();

    //code copypasted from UnityEngine.UI.Selectable
    bool group_allow_interaction = true;
    Transform t = transform;
    while ( t != null )
    {
      t.GetComponents( canvas_group_caches );
      bool should_break = false;
      for ( int i = 0; i < canvas_group_caches.Count; i++ )
      {
        if ( !canvas_group_caches[i].interactable )
        {
          group_allow_interaction = false;
          should_break = true;
        }
        
        if ( canvas_group_caches[i].ignoreParentGroups )
          should_break = true;
      }

      if ( should_break )
        break;

      t = t.parent;
    }

    isInteractableInHierarchy = group_allow_interaction;
  }

  protected void OnTransformParentChanged()
  {
    //code copypasted from UnityEngine.UI.Selectable

    // If our parenting changes figure out if we are under a new CanvasGroup.
    OnCanvasGroupChanged();
  }
  #endregion

  #region Public Methods and Properties
  public void setOnClick( Action<PointerEventData> action )
  {
    if ( on_click_subscriptions == null )
      on_click_subscriptions = new EventAction<PointerEventData>();
    else
      on_click_subscriptions.clear();

    on_click_subscriptions.add( action );
  }

  public void simulateClick( PointerEventData pointer_data )
  {
    if ( !on_click_subscriptions?.any ?? true )
      return;

    clickableGlobal.notifyOnAnyClickableClick( this, pointer_data );

    on_click_subscriptions.call( pointer_data );
  }

  public void simulateNotInteractableClick( PointerEventData pointer_data )
  {
    if ( !on_not_interactable_click_subscriptions?.any ?? true )
      return;

    clickableGlobal.notifyOnAnyClickableNotInteractableClick( this, pointer_data );
    on_not_interactable_click_subscriptions.call( pointer_data );
  }

  public bool isInteractable
  {
    get => is_interactable;
    set
    {
#if UNITY_EDITOR
      if ( EditorApplication.isPlaying )
#endif
        initComponents();

      is_interactable = value;
      notifyInteractableChanged();
    }
  }
  
  
  public bool isInteractableInHierarchy
  {
    get => is_interactable_in_hierarchy;
    set
    {
      if ( value == is_interactable_in_hierarchy )
        return;

      is_interactable_in_hierarchy = value;
      onInteractableInHierarchyChanged?.Invoke( is_interactable_in_hierarchy );
    }
  }

  public bool isMultitouchSupported
  {
    get => is_multitouch_supported;
    set => is_multitouch_supported = value;
  }

  public bool isHandleClickOnlyInPlaceWhereTaped
  {
    get => is_handle_click_only_in_place_where_taped;
    set => is_handle_click_only_in_place_where_taped = value;
  }

  public LongPressBehaviour longPressBehaviour
  {
    get => long_press_behaviour;
    set => long_press_behaviour = value;
  }

  public float longClickDelay
  {
    get => long_click_delay;
    set => long_click_delay = value;
  }

  public byte approximateMaxClicksPerSec
  {
    get => approximate_max_clicks_per_sec;
    set => approximate_max_clicks_per_sec = value;
  }

  public byte timeWhenMaxClicks
  {
    get => time_when_max_clicks;
    set => time_when_max_clicks = value;
  }

  public AnimationCurve fromMinToMaxCurve
  {
    get => from_min_to_max_curve;
    set => from_min_to_max_curve = value;
  }
  #endregion

  #region Protected Methods
  protected virtual void onPhysicallyDown() {}
  protected virtual void onPhysicallyUp() {}
  protected virtual void onPhysicallyExit() {}

  protected bool canContinueInteraction
    => snapshot_interactable_global && snapshot_interactable && snapshot_interactable_by_multitouch && snapshot_interactable_in_hierarchy;

  protected bool canInteractByMultitouchRightNow
    => isMultitouchSupported || !isNowAnyClickableInteract;
  
  protected bool canUseClickableGlobal
    => MonoBehaviourSingleton<ClickableBaseGlobal>.isRegistered;

  protected void initInteraction( PointerEventData pointer_data )
  {
    if ( !canUseClickableGlobal )
      return;

    initComponents();

    is_long_click_action_executed       = false;
    snapshot_interactable_global        = clickableGlobal.isInteractableGlobal( this );
    snapshot_interactable               = isInteractable;
    snapshot_interactable_in_hierarchy  = isInteractableInHierarchy;
    snapshot_interactable_by_multitouch = tryInteractByMultitouch();

    Debug.Log( $"{nameof( is_long_click_action_executed )} {is_long_click_action_executed}, {nameof( snapshot_interactable_global )} {snapshot_interactable_global}, {nameof( snapshot_interactable )} {snapshot_interactable}, {nameof( snapshot_interactable_in_hierarchy )} {snapshot_interactable_in_hierarchy}, {nameof( snapshot_interactable_by_multitouch )} {snapshot_interactable_by_multitouch}" );

    bool tryInteractByMultitouch()
    {
      fixInteractionsByMultitouchPossibleErrors();

      if ( !isNowAnyClickableInteract )
      {
        clickableGlobal.active_in_interaction_clickable = this;
        clickableGlobal.active_touch_id = pointer_data.pointerId;
        return true;
      }

      return isMultitouchSupported;
    }

    void fixInteractionsByMultitouchPossibleErrors()
    {
      int touch_count = Input.touchCount;
      if ( touch_count == 0 || touch_count == 1 )
        clickableGlobal.resetMultitouchCaches();
    }
  }

  protected void endInteraction()
  {
    if ( MonoBehaviourSingleton<ClickableBaseGlobal>.isRegistered && clickableGlobal.active_in_interaction_clickable == this )
      clickableGlobal.resetMultitouchCaches();
  }
  #endregion

  #region Private Methods
  private bool isNowAnyClickableInteract => canUseClickableGlobal && clickableGlobal.active_touch_id != ClickableBaseGlobal.UNDEFINED_TOUCH_ID;

  private void simulateLongClick( PointerEventData pointer_data )
  {
    if ( !on_long_click_subscriptions?.any ?? true )
      return;
    
    clickableGlobal.notifyOnAnyClickableLongClick( this, pointer_data );
    on_long_click_subscriptions.call( pointer_data );
  }

  private void simulateGlobalNotAllowedClick( PointerEventData pointer_data )
  {
    if ( !clickableGlobal.notifyOnAnyClickableClickOnNotAllowed( this, pointer_data ) )
      return;
  }

  private void stopLongClickCoroutine()
  {
  }

  private IEnumerator repeatClicksCoroutine( PointerEventData pointer_data )
  {
    AnimationCurve animation_curve = from_min_to_max_curve != null ? from_min_to_max_curve : default_from_min_to_max_curve;
    float holding_time             = 0.1f;
    float time_from_previous_click = 0.0f;
    while ( canContinueInteraction && isInteractable && isInteractableInHierarchy && gameObject.activeSelf && gameObject.activeInHierarchy )
    {
      float time_in_range     = holding_time.toRange( time_when_max_clicks / 5.0f, time_when_max_clicks );
      float clicks_per_sec    = animation_curve.Evaluate( time_in_range / time_when_max_clicks ) * approximate_max_clicks_per_sec;
      float seconds_per_click = ( 1.0f / clicks_per_sec ).toRange( 0.0f, 1.0f );

      while ( time_from_previous_click >= seconds_per_click )
      {
        time_from_previous_click -= seconds_per_click;
        simulateClick( pointer_data );
      }
      yield return null;

      holding_time             += Time.unscaledDeltaTime;
      time_from_previous_click += Time.unscaledDeltaTime;
    }
  }

  private void notifyInteractableChanged()
  {
    if ( Application.isPlaying )
      clickableGlobal.notifyOnAnyClickableInteractableChanged( this, is_interactable );

    onInteractableChanged?.Invoke( is_interactable );
  }
  #endregion
  
  protected override void OnDestroy()
  {
    onPointerDown                    = null;
    onPointerEnter                   = null;
    onPointerUp                      = null;
    onPointerExit                    = null;
    onInteractableChanged            = null;
    onInteractableInHierarchyChanged = null;

    on_click_subscriptions                  = null;
    on_long_click_subscriptions             = null;
    on_not_interactable_click_subscriptions = null;
  }
}

