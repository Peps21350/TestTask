using System;


public static class MyMath
{
  public static bool greater( in this float a, in float b, in float eps = 1E-45f )
  {
    return a - (double)b > eps;
  }

  public static bool less( in this float a, in float b, in float eps = 1E-45f )
  {
    return b.greater( in a, in eps );
  }
  
  public static float abs( in this float data )
  {
    return Math.Abs( data );
  }

  public static double abs( in this double data )
  {
    return Math.Abs( data );
  }
  
  public static int toRange( in this int value, in int inclusive_minimum, in int inclusive_maximum )
  {
    if ( value < inclusive_minimum )
      return inclusive_minimum;

    return value > inclusive_maximum ? inclusive_maximum : value;
  }
  
  public static bool isInRange( in this int value, in int inclusive_min, in int exclusive_max )
  {
    return value >= inclusive_min && value < exclusive_max;
  }

  public static double withMin( in this double value, in double inclusive_minimum )
  {
    return value < inclusive_minimum ? inclusive_minimum : value;
  }
}