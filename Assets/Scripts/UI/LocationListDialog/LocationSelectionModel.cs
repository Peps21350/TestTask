using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using LocationData = LocationDatas.LocationData;


public class LocationSelectionModel : LocationModel
{
  private List<LocationData> locations_data = default;
  
  private int cur_location_number = 0;
  
  public LocationSelectionModel()
  {
    PlayerPrefs.DeleteAll();// todo delete
    locations_data = getAllLocationData();
  }
  
  public List<LocationData> getAllLocationData()
  {
    if ( location_data == null )
      location_data = cached_location_datas.loadResources();
    
    
    return location_data.getAllLocationData();
  }
}