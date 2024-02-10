using System.Collections;
using UnityEngine;


public class LocationSelectionView : BaseView<LocationSelectionModel, LocationSelectionPresenter, LocationSelectionView>
{
  [Header(nameof(LocationView))] 
  [SerializeField] private UIScrollSpawner ui_scroll_spawner = null;


  public UIScrollSpawner scrollSpawner => ui_scroll_spawner;
  

  public override IEnumerator init()
  {
    yield return base.init();
    
    setTitle( GameText.areas );
    presenter.spawnData();
  }
  
  protected override void deinit()
  {
    ui_scroll_spawner.despawnItems();
  }
}