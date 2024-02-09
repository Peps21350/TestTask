using System.Collections;
using UnityEngine;
using UnityEngine.UI;


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
    base.deinit();
    
    ui_prefab_spawner.despawnItems();
  }

  public override IEnumerator init()
  {
    yield return base.init();
    
    presenter.setLocationSprite();
    setActiveBtnInstallFurniture( presenter.canInstallNextFurniture );
  }

  public void setLocationSprite( string sprite_name )
  {
    ui_image.sprite = spriteManager.atlas_location.getSprite( sprite_name );
  }
  
  public void setActiveBtnInstallFurniture( bool state )
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