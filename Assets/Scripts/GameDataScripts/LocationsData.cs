using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu( fileName = "LocationData", menuName = "ScriptableObjects/Data/LocationData", order = 1 )]
public class LocationsData : ScriptableObject
{
  #region Serialize Fields
  [SerializeField] public LocationData[] location_data_array = null;
  #endregion
  
  #region Public Methods
  public LocationData? getLocationData( int location_number )
  {
    if ( location_data_array.Length < location_number + 1 )
      return null;
    
    LocationData? location_data = location_data_array[location_number];
    
    // ReSharper disable once ConditionIsAlwaysTrueOrFalse
    if ( location_data == null )
      Debug.LogError( "Empty Data" );
    
    return location_data;
  }

  public List<LocationData> getSortedLocationsData()
    => location_data_array.ToList().OrderBy( t => t.location_idx ).ToList();
  #endregion

  [Serializable]
  public struct LocationData
  { 
    public short        location_idx;
    public LocationType location_type;
    public ItemDatas    item_datas;
    public string       image_name;
    public string       name;
    
    public int getItemDatasAmount()
    {
      return item_datas.item_data_array.Length;
    }
  }

  [Serializable]
  public enum LocationType
  {
    REAL,
    FAKE
  }
}