using UnityEngine;


public static class StringExtensions
{
  public const string LOCAL_LINK_MARKER = "#";

  public static bool isHyperlink( this string value ) => !isLocalLink( value );
  public static bool isLocalLink( this string value ) => value[0] == LOCAL_LINK_MARKER[0];

  public static string addLink( this string value, string link ) => $"<link={link}>{value}</link>";

  public static string changeColor( this string text, Color color )
    => $"<color=#{ColorUtility.ToHtmlStringRGBA( color )}>{text}</color>";

  public static string changeColor( this string text, string hex )
    => $"<color={hex}>{text}</color>";

  public static string makeBoldColoredUppercase( this string text, Color color )
    => $"<b><color=#{ColorUtility.ToHtmlStringRGB( color )}><uppercase>{text}</uppercase></color></b>";

  public static string makeBoldColoredUppercase( this string text, string hex )
    => $"<b><color={hex}><uppercase>{text}</uppercase></color></b>";

  public static string makeBoldColored( this string text, Color color )
    => $"<b><color=#{ColorUtility.ToHtmlStringRGB( color )}>{text}</color></b>";

  public static string makeBoldColored( this string text, string hex )
    => $"<b><color={hex}>{text}</color></b>";

  public static string makeBoldUppercase( this string text )
    => $"<b><uppercase>{text}</uppercase></color></b>";

  public static string makeUppercase( this string text )
    => $"<uppercase>{text}</uppercase>";

  public static string makeBold( this string text )
    => $"<b>{text}</b>";

  public static string changeLineBreak( this string text )
  => $"<br>{text}";

  public static string changeSizePercent( this string text, int percent_text_size )
    => $"<size={percent_text_size}%>{text}</size>";

  public static string changeSizeAbsolute( this string text, int text_size )
    => $"<size={text_size}>{text}";

  public static string replaceLastOccurence( this string source, string find, string replace )
  {
    int place = source.LastIndexOf( find );

    if ( place < 0 )
      return source;

    return source.Remove( place, find.Length ).Insert( place, replace );
  }

  public static string truncate( this string value, int length )
    => (value != null && value.Length > length) ? value.Substring( 0, length ) : value;
  
  public static string trimCsFileNameLikeClass( this string file_name )
  {
    if ( string.IsNullOrEmpty( file_name ) )
      return file_name;

    int startIndex = file_name.LastIndexOf( '\\' ) + 1;
    if ( startIndex == 0 )
      startIndex = file_name.LastIndexOf( '/' ) + 1;

    int length = file_name.Length - startIndex - 3;
    return file_name.Substring( startIndex, length );
  }
}
