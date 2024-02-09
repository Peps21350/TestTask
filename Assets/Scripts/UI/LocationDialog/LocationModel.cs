using System.Collections.Generic;
using UnityEngine;
using ItemData = ItemDatas.ItemData;
using LocationData = LocationDatas.LocationData;


public class LocationModel : BaseModel
{
  protected CachedObject<LocationDatas> cached_location_datas = new CachedObject<LocationDatas>( "GameData/LocationData" );
  
  protected LocationDatas location_data = default;
  
  private LocationData cur_location_data = default;
  private ItemDatas    items_data        = default;
  
  private int cur_location_number   = 0;
  private int cur_furniture_number  = 0;

  public bool canInstallNextFurniture => items_data != null && items_data.item_datas.Length >= cur_furniture_number + 1;
  
  public LocationModel()
  {
    PlayerPrefs.DeleteAll();// todo delete
    cur_location_number  = LocalSave.curLocationNumber;
    cur_furniture_number = getCurFurnitureNumber();
    cur_location_data    = getLocationData();
    items_data           = cur_location_data.item_datas;
  }
  
  public ItemData? getCurFurniture()
  {
    List<ItemData> data = getSortedItemsData();
    if ( data == null || data.Count < cur_furniture_number + 1 )
      return null;
      
    return data[cur_furniture_number];
  }

  public void saveLocationNumber( int number )
  {
    LocalSave.curLocationNumber = number;
  }

  public void tryIncrementFurnitureNumber()
  {
    if ( items_data == null )
      return;
    
    if ( canInstallNextFurniture )
      cur_furniture_number++;
    else
    {
      location_data.setLocationCompleted( cur_location_number );
      incrementLocationNumber();
      saveLocationNumber();

      updateData();
      cur_furniture_number = 0;
    }
  }

  private void updateData()
  {
    getLocationData();
    getSortedItemsData();
  }
  
  private List<ItemData> getSortedItemsData()
  {
    if ( items_data == null )
      items_data = cur_location_data.item_datas;
    
    return items_data.getSortedItemDatas();
  }

  private void incrementLocationNumber()
  {
    cur_location_number++;
  }

  private void saveLocationNumber()
  {
    LocalSave.curLocationNumber = cur_location_number;
  }

  public LocationData getLocationData()
  {
    if ( location_data == null )
      location_data = cached_location_datas.loadResources();
    
    cur_location_data = location_data.getLocationData( cur_location_number ) ?? new LocationData();
    return cur_location_data;
  }

  private int getCurFurnitureNumber()
  {
    return LocalSave.getCurLocationFurnitureNumber( cur_location_number );
  }
  
  public void saveCurFurnitureNumber( int value )
  {
    LocalSave.setCurLocationFurnitureNumber( cur_location_number, value );
  }
}