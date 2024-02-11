using System;


public class ClickableBaseGlobal : MonoBehaviourSingleton<ClickableBaseGlobal>
{
  #region Events
  public event Action<ClickableBase, bool> onAnyClickableInteractableChanged = delegate { };
  #endregion

  #region Public Fields
  public const int           UNDEFINED_TOUCH_ID = -1;
  public       int           activeTouchID                 { get; set; } = UNDEFINED_TOUCH_ID;
  public       ClickableBase activeInInteractionClickable { get; set; } = null;
  #endregion


  #region Public Methods
  public void notifyOnAnyClickableInteractableChanged( ClickableBase clickable, bool is_interactable )
    => onAnyClickableInteractableChanged( clickable, is_interactable );
  #endregion
}