using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu( fileName = "LocationData", menuName = "ScriptableObjects/Data/LocationData", order = 1 )]
public class LocationDatas : ScriptableObject
{
  #region Serialize Fields
  [SerializeField] public LocationData[] location_data_array = null;
  #endregion
  
  #region Public Methods
  public LocationData? getLocationData( int location_number )
  {
    LocationData? location_data = location_data_array[location_number];//.FirstOrDefault( it => it.location_number == location_number );
    
    // ReSharper disable once ConditionIsAlwaysTrueOrFalse
    if ( location_data == null )
      Debug.LogError( "Empty Data" );
    
    return location_data;
  }

  public List<LocationData> getAllLocationData()
    => location_data_array.ToList();
  #endregion

  [Serializable]
  public struct LocationData
  { 
    public short        location_idx;
    public LocationType location_type;
    public ItemDatas    item_datas;
    public string       image_name;
    public string       name;
  }

  [Serializable]
  public enum LocationType
  {
    REAL,
    FAKE
  }
}