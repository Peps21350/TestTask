using System.Collections;
using UnityEngine;


public class LocationView : BaseView<LocationModel, LocationPresenter, LocationView>
{
  [Header(nameof(LocationView))] 
  [SerializeField] private UIButtonBase    ui_btn_install_furniture  = null;
  [SerializeField] private UIButtonBase    ui_btn_location_selection = null;
  [SerializeField] private UIPrefabSpawner ui_prefab_spawner         = null;


  public UIPrefabSpawner prefabSpawner => ui_prefab_spawner;
  
  protected override void initMyComponents()
  {
    base.initMyComponents();
        
    ui_btn_install_furniture.onClick += _ => onLocationSelectionClick();
    
    ui_btn_location_selection.onClick += _ => onInstallFurnitureClick();
  }

  public override IEnumerator init()
  {
    yield return base.init();
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