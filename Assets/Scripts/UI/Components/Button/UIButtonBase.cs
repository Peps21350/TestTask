using UnityEngine;
using UnityEngine.UI;


public class UIButtonBase : ButtonBase
{
  #region Serialize Fields
  [Header( nameof( UIButtonBase ) )]
  [SerializeField] protected Image      image     = null;
  [SerializeField] protected UITextMesh txt_title = null;

  [SerializeField] private Color color_normal   = new( 1.0f, 1.0f, 1.0f, 1.0f );
  [SerializeField] private Color color_pressed  = new( 0.6f, 0.6f, 0.6f, 1.0f );
  [SerializeField] private Color color_disabled = new( 0.8f, 0.8f, 0.8f, 0.5f );
  #endregion

  
  #region Public Methods
  public void setTitle( string title_text )
  {
    initComponents();

    if ( !txt_title )
      return;

    txt_title.enabled = !string.IsNullOrWhiteSpace( title_text );
    txt_title.setText( title_text, true );
  }

  public void setActiveInteractable( bool state )
  {
    isInteractable = state;
  }
  #endregion

  #region Protected Methods
  protected override void setButtonState( ButtonState button_state = ButtonState.NORMAL )
  {
    crossFadeColor( button_state );
  }
  #endregion

  #region Private Methods
  private void crossFadeColor( ButtonState button_state )
  {
    if ( image == null )
      return;
    
    Color color = isInteractable && isInteractableInHierarchy
      ? button_state == ButtonState.PRESSED ? color_pressed : color_normal
      : color_disabled;
    
    image.color = color;
  }
  #endregion
}
