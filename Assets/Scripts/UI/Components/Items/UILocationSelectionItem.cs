using UnityEngine;
using UnityEngine.UI;
using LocationStatus = GameState.LocationStatus;


public class UILocationSelectionItem : PoolObject
{
  [SerializeField] private UIProgressBar ui_progress_bar   = null;
  [SerializeField] private Image         ui_location_image = null;
  [SerializeField] private UITextMesh    ui_location_text  = null;
  [SerializeField] private UIButtonBase  ui_button         = null;

  
  private LocationStatus location_status = null;

  public void init( int cur_value, int max_value, string background_name, string naming, int index, int location_idx )
  {
    location_status = new LocationStatus( location_idx, cur_value );
    
    ui_button.onClick += _ => onBtnClick();
    
    transform.SetSiblingIndex( index );
    
    ui_progress_bar.init( cur_value, max_value );
    
    ui_location_image.sprite = spriteManager.atlas_location.getSprite( background_name );
    
    ui_location_text.setText(  naming.getTextFromStaticString() );
  }
  
  protected override void deinit()
  {
    base.deinit();

    ui_button.removeAllSubscriptions();

    ui_location_image.sprite = null;
    ui_progress_bar.deinit();
  }

  private void onBtnClick()
  {
    viewManager.addLocationView( location_status );
  }
}