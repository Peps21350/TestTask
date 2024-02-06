using System;
using System.Collections.Generic;
using System.Linq;


public static class ListExtensions
{
  public static void swap<T>( this IList<T> list, int index_a, int index_b )
  {
    T tmp = list[index_a];
    list[index_a] = list[index_b];
    list[index_b] = tmp;
  }

  public static void cutFirstAndPasteLast<T>( this IList<T> list )
  {
    T obj = list[0];
    list.Remove( obj );
    list.Add( obj );
  }

  public static void cutLastAndPasteFirst<T>( this IList<T> list )
  {
    T obj = list[list.Count - 1];
    list.Remove( obj );
    list.Insert( 0, obj );
  }

  public static T firstOrDefault<T>( this IList<T> list )
  {
    int list_count = list.Count;
    if ( list_count > 0 )
      return list[0];

    return default;
  }

  public static T lastOrDefault<T>( this IList<T> list )
  {
    int list_count = list.Count;
    if ( list_count > 0 )
      return list[list_count - 1];

    return default;
  }

  public static T firstOrDefaultAndRemove<T>( this IList<T> list )
  {
    if ( list.Count == 0 )
      return default;

    T res = list[0];
    list.RemoveAt( 0 );
    return res;
  }

  public static T lastOrDefaultAndRemove<T>( this IList<T> list )
  {
    int index = list.Count - 1;
    if ( index < 0 )
      return default;

    T res = list[index];
    list.RemoveAt( index );
    return res;
  }

  public static int lastIndex<T>( this IList<T> list ) => list.Count - 1;

  public static Action removeMeAction<T>( this IList<T> list, T obj )
    => () => list.Remove( obj );


  public static HashSet<T> toSet<T>( this IEnumerable<string> input ) where T : Enum
    => new HashSet<T>( input.Where( s => !string.IsNullOrWhiteSpace( s ) && Enum.IsDefined( typeof(T), s ) )
      .Select( s => (T)Enum.Parse( typeof(T), s ) ) );
}
