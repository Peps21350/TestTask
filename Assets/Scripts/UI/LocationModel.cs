using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ItemData = ItemDatas.ItemData;
using LocationData = LocationDatas.LocationData;


public class LocationModel : BaseModel
{
  private LocationDatas locationData { get; }

  private ItemDatas    items_data        = default;
  private LocationData cur_location_data = default;
  
  private int cur_location_number  = 0;
  private int cur_furniture_number = 0;
  
  public LocationModel()
  {
    cur_location_number = LocalSave.curLocationNumber;
    cur_furniture_number = getCurFurnitureNumber();
    locationData = Resources.Load<LocationDatas>( "GameData/LocationData" );
    cur_location_data = getLocationData();
  }
  
  public ItemData getCurFurniture()
  {
    return getSortedItemsData()[cur_furniture_number];
  }

  public List<ItemData> getSortedItemsData()
  {
    items_data = cur_location_data.item_datas;
    return items_data.getSortedItemDatas();
  }

  public void saveLocationNumber( int number )
  {
    LocalSave.curLocationNumber = number;
  }

  public void incrementFurnitureNumber()
  {
    if ( items_data.item_datas.Length <= cur_furniture_number )
    {
      locationData.setLocationCompleted( cur_location_number );
      incrementLocationNumber();
      saveLocationNumber();

      updateData();
      cur_furniture_number = 0;
    }
    else
      cur_furniture_number++;
  }

  private void updateData()
  {
    getLocationData();
    getSortedItemsData();
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
    cur_location_data = locationData.getLocationData( cur_location_number ) ?? new LocationData();
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