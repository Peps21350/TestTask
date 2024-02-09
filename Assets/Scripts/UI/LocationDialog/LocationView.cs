using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using LocationStatus = GameState.LocationStatus;


public class LocationView : BaseView<LocationModel, LocationPresenter, LocationView>
{
  [Header(nameof(LocationView))] 
  [SerializeField] private UIButtonBase    ui_btn_install_furniture  = null;
  [SerializeField] private UIButtonBase    ui_btn_location_selection = null;
  [SerializeField] private UIPrefabSpawner ui_prefab_spawner         = null;
  [SerializeField] private Image           ui_image                  = null;


  public UIPrefabSpawner prefabSpawner => ui_prefab_spawner;
  
  
  protected override void initMyComponents()
  {
    base.initMyComponents();
        
    ui_btn_install_furniture.onClick += _ => onInstallFurnitureClick();
  
    ui_btn_location_selection.onClick += _ => onLocationSelectionClick();
  }
  
  protected override void deinit()
  {
    ui_prefab_spawner.despawnItems();
  }

  public IEnumerator init( LocationStatus location_status )
  {
    yield return base.init();

    if ( location_status != null )
      presenter.updateData( location_status );
    
    presenter.setLocationSprite();
    setActiveBtnInstallFurniture( presenter.canInstallNextFurniture );
    
    presenter.spawnData( location_status?.installed_furniture ?? -1 );
  }

  public void setLocationSprite( string sprite_name )
  {
    ui_image.sprite = spriteManager.atlas_location.getSprite( sprite_name );
  }
  
  public void setActiveBtnInstallFurniture( bool state = true )
  {
    ui_btn_install_furniture.setActiveInteractable( state );
  }

  private void onInstallFurnitureClick()
  {
    presenter.onInstallFurnitureClick();
  }

  private void onLocationSelectionClick()
  {
    viewManager.addLocationSelectionView();
  }
}