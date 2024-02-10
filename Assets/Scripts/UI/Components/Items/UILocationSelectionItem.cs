using UI;
using UnityEngine;
using UnityEngine.UI;
using LocationStatus = GameState.LocationStatus;
using LocationItemStateViewEnum = UI.LocationItemStateView.LocationItemStateViewEnum;
using LocationData = LocationsData.LocationData;


public class UILocationSelectionItem : PoolObject
{
  [SerializeField] private UIProgressBar         ui_progress_bar          = null;
  [SerializeField] private Image                 ui_location_image        = null;
  [SerializeField] private Image                 ui_lock_image            = null;
  [SerializeField] private UITextMesh            ui_location_title        = null;
  [SerializeField] private UITextMesh            ui_location_text         = null;
  [SerializeField] private UIButtonBase          ui_button                = null;
  [SerializeField] private LocationItemStateView location_item_state_view = null;


  private LocationStatus            location_status  = null;
  private LocationItemStateViewEnum state            = default;


  public void init( LocationData location_data, int amount_installed_furniture, int cur_location_idx, bool location_opened, int idx )
  {
    location_status = new LocationStatus( location_data.location_idx, amount_installed_furniture );

    ui_button.onClick += _ => onBtnClick();
    
    transform.SetSiblingIndex( idx );

    initViewComponents( location_data, cur_location_idx, location_opened );
  }
  
  protected override void deinit()
  {
    base.deinit();

    ui_button.removeAllSubscriptions();

    location_status = null;
    state = default;
    ui_location_image.sprite = null;
    ui_progress_bar.deinit();
  }

  private void initViewComponents( LocationData location_data, int cur_location_idx, bool location_opened )
  {
    state = getCurState( location_opened, cur_location_idx, location_data );
    
    location_item_state_view.initLocationItemState( state );
    
    if ( state == LocationItemStateViewEnum.CURRENT_REAL ) 
      ui_progress_bar.init( location_status.installed_furniture, location_data.getItemDatasAmount() );

    setSprites( location_data.image_name );
    setText( location_data.name );
  }

  private LocationItemStateViewEnum getCurState( bool location_opened, int cur_location_idx, LocationData location_data )
  {
    if ( cur_location_idx == location_data.location_idx )
    {
      return location_data.location_type == LocationsData.LocationType.REAL 
        ? LocationItemStateViewEnum.CURRENT_REAL 
        : LocationItemStateViewEnum.CURRENT_FAKE;
    }
    
    if ( location_opened && location_data.location_type == LocationsData.LocationType.FAKE )
      return LocationItemStateViewEnum.CURRENT_FAKE;

    return location_opened ? LocationItemStateViewEnum.OPENED : LocationItemStateViewEnum.CLOSED;
  }

  private void setSprites( string background_name )
  {
    ui_lock_image.enabled = true;
    ui_location_image.sprite = spriteManager.atlas_location.getSprite( background_name );
  }

  private void setText( string naming )
  {
    if ( state == LocationItemStateViewEnum.CURRENT_FAKE || state == LocationItemStateViewEnum.OPENED ) 
      ui_location_text.setText( state == LocationItemStateViewEnum.CURRENT_FAKE ? GameText.coming : GameText.completed );
    
    ui_location_title.setText( naming.getTextFromStaticString() );
  }

  private void onBtnClick()
  {
    if ( state == LocationItemStateViewEnum.CURRENT_REAL || state == LocationItemStateViewEnum.OPENED ) 
      viewManager.addLocationView( location_status );
  }
}