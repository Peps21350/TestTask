using System.Collections.Generic;


public static class ListExtensions
{
  public static T firstOrDefault<T>( this IList<T> list )
  {
    int list_count = list.Count;
    if ( list_count > 0 )
      return list[0];

    return default;
  }

}
