using System.Collections;
using UnityEngine;


public class LocationView : BaseView<LocationModel, LocationPresenter, LocationView>
{
  [Header(nameof(LocationView))] 
  [SerializeField] private UIButtonBase    ui_btn_install_furniture  = null;
  [SerializeField] private UIButtonBase    ui_btn_location_selection = null;
  [SerializeField] private UIPrefabSpawner ui_prefab_spawner         = null;
  [SerializeField] private SpriteRenderer  ui_sprite_renderer        = null;


  public UIPrefabSpawner prefabSpawner  => ui_prefab_spawner;
  public SpriteRenderer  spriteRenderer => ui_sprite_renderer;
  
  
  protected override void initMyComponents()
  {
    base.initMyComponents();
        
    //ui_btn_install_furniture.onClick += _ => onLocationSelectionClick();
    
    //ui_btn_location_selection.onClick += _ => onInstallFurnitureClick();
  }

  public override IEnumerator init()
  {
    yield return base.init();
    
    presenter.setLocationSprite();
    setActiveBtnInstallFurniture( presenter.canInstallNextFurniture );
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
    presenter.onLocationSelectionClick();
  }
}