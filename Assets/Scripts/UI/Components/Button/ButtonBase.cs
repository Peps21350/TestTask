
public abstract class ButtonBase : ClickableBase
{
  #region initMyComponents Methods
  protected override void initMyComponents()
  {
    base.initMyComponents();

    onInteractableChanged            += (_) => setButtonState();
    onInteractableInHierarchyChanged += (_) => setButtonState();
  }
  #endregion

  #region Protected Methods
  protected sealed override void onPhysicallyDown()
  {
    if ( canInteractByMultitouchRightNow )
      setButtonState( ButtonState.PRESSED );
  }

  protected sealed override void onPhysicallyUp()
  {
    setButtonState();
  }

  protected sealed override void onPhysicallyExit()
  {
    setButtonState();
  }

  protected abstract void setButtonState( ButtonState button_state = ButtonState.NORMAL );
  #endregion

  #region Protected Enum
  protected enum ButtonState : byte
  {
    NORMAL   = 0,
    PRESSED  = 1,
    DISABLED = 2
  }
  #endregion
}
