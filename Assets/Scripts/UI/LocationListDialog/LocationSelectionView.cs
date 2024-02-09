using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class LocationSelectionView : BaseView<LocationSelectionModel, LocationSelectionPresenter, LocationSelectionView>
{
  [Header(nameof(LocationView))] 
  [SerializeField] private UIScrollSpawner ui_scroll_spawner         = null;
  [SerializeField] private Image           ui_image                  = null;


  public UIScrollSpawner scrollSpawner => ui_scroll_spawner;
  

  public override IEnumerator init()
  {
    yield return base.init();
    
    presenter.spawnData();
  }
  
  protected override void deinit()
  {
    base.deinit();
    
    ui_scroll_spawner.despawnItems();
  }
}