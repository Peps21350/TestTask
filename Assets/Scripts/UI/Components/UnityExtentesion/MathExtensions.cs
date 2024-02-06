using UnityEngine;
using System;
using System.Globalization;
using System.Linq;


public static class MathExtensions
{
  public const int BYTES_IN_MB = 1048576;

  private static readonly float HALF_ROTATION = 180.0f;
  private static readonly float FULL_ROTATION = 360.0f;

  private static readonly string[] SHORT_NUMBER_SUFFIXES = { "", "k", "M", "B", "T", "Q" };
  private static readonly int[]    NUM_POWER_EXCEPTION   = { 6, 9 };

  private const int COMA_POWER     = 3;
  private const int ACCURACY_POWER = 2;

  /// <summary>Postfix in PERCENT mode is always "%"</summary>
  public static string toStringShort( this double number, NumberType type = NumberType.DEFAULT, string postfix = "" )
  {
    string number_sign = number < 0 ? "-" : "";
    number = number.abs();

    int    number_power   = (int)Math.Log10( number.withMin( 1.0 ) );
    int    num_of_triades = ( number_power - 1 ) / 3;
    string suffix         = SHORT_NUMBER_SUFFIXES[num_of_triades.toRange( 0, SHORT_NUMBER_SUFFIXES.Length - 1 )];
    double short_num      = number / Math.Pow( 10, num_of_triades * 3 );

    int num_count_after_coma;
    switch ( type )
    {
    case NumberType.DEFAULT:
      num_count_after_coma = number_power > COMA_POWER && !NUM_POWER_EXCEPTION.Contains( number_power ) ? 1 : 0;
      double accuracy      = number_power < ACCURACY_POWER ? 0 : Math.Pow( 10, -number_power );

      short_num += accuracy;
      break;

    case NumberType.PERCENT:
    case NumberType.CAN_BE_FLOAT:
      int first_num_after_coma  = (int)( number * 10  % 10 );
      int second_num_after_coma = (int)( number * 100 % 10 );
      if ( second_num_after_coma != 0 )
        num_count_after_coma = 2;
      else
      if ( first_num_after_coma != 0 )
        num_count_after_coma = 1;
      else
        num_count_after_coma = 0;
      break;

    case NumberType.PERCENT_ROUND:
    {
      CultureInfo culture_info = new CultureInfo("en-US");
      return number.ToString("0.##", culture_info) + "%";
    }

    default: throw new ArgumentOutOfRangeException( nameof( type ), type, null );
    }

    string result = ( (int)short_num ).ToString();
    if ( num_count_after_coma > 0 )
    {
      int num_after_point = (int)( ( short_num - (int)short_num ) * Math.Pow( 10, num_count_after_coma ) );
      result += $".{num_after_point}";
    }

    if ( type == NumberType.PERCENT )
      postfix = "%";

    return $"{number_sign}{result}{suffix}{postfix}";
  }

  public static string toStringShort( this float number, NumberType type = NumberType.DEFAULT, string postfix = "" )
  {
    return ( (double)number ).toStringShort( type, postfix );
  }

  public static string toStringShort( this long number, NumberType type = NumberType.DEFAULT, string postfix = "" )
  {
    return ( (double)number ).toStringShort( type, postfix );
  }

  public static string toStringShort( this int number, NumberType type = NumberType.DEFAULT, string postfix = "" )
  {
    return ( (double)number ).toStringShort( type, postfix );
  }

  public static string toStringShort( this decimal number, NumberType type = NumberType.DEFAULT, string postfix = "" )
  {
    return ( (double)number ).toStringShort( type, postfix );
  }

  public static Vector2 abs( this Vector2 data )
  {
    return new Vector2( data.x.abs(), data.y.abs() );
  }

  public static Vector3 abs( this Vector3 data )
  {
    return new Vector3( data.x.abs(), data.y.abs(), data.z.abs() );
  }

  public static Vector2 clamp01( Vector2 value )
  {
    return new Vector2( Mathf.Clamp01( value.x ), Mathf.Clamp01( value.y ) );
  }

  public static float clampAngle( this float angle, float min, float max )
  {
    if ( angle.greater( HALF_ROTATION ) )
      angle -= FULL_ROTATION;

    angle = Mathf.Clamp( angle, min, max );
    if ( angle.less( 0.0f ) )
      angle += FULL_ROTATION;

    return angle;
  }

  public static double lerp ( double a, double b, float t )
  {
    return a + ( b - a ) * clamp01 ( t );
  }

  public static double clamp01( double value )
  {
    if ( value < 0.0 )
      return 0.0f;
    return value > 1.0 ? 1d : value;
  }

  public static Vector3 normalizeRotation( this Vector3 rotation )
  {
    return new Vector3( normAngle( rotation.x ), normAngle( rotation.y ), normAngle( rotation.z ) );

    float normAngle( float angle )
    {
      if ( angle.greater( HALF_ROTATION ) )
        angle -= FULL_ROTATION;

      return angle;
    }
  }
}

public enum NumberType : byte
{
  DEFAULT       = 0,
  PERCENT       = 1,
  CAN_BE_FLOAT  = 2,
  PERCENT_ROUND = 3,
}
