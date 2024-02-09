using System;
using System.Reflection;
using UnityEngine;


public static class StringExtensions
{
  public static string getTextFromStaticString( this string naming )
  {
    Type      type  = typeof( GameText );
    FieldInfo field = type.GetField( naming );
    
    if ( field != null )
      return (string)field.GetValue( null );

    Debug.LogError( "Static string with name " + naming + " not found" );
    return string.Empty;
  }
}
