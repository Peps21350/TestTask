using System;
using System.Numerics;

public static class MyMath
{
  public static bool equal(in this float a, in float b, in float eps = 1E-45f) => (double) a == (double) b || (double) Math.Abs(a - b) < (double) eps;

  public static bool isZero(in this float a, in float eps = 1E-45f) => a.equal(0.0f, in eps);

  public static bool greater(in this float a, in float b, in float eps = 1E-45f) => (double) a - (double) b > (double) eps;

  public static bool less(in this float a, in float b, in float eps = 1E-45f) => b.greater(in a, in eps);

  public static bool greaterOrEqual(in this float a, in float b, in float eps = 1E-45f) => a.equal(in b, in eps) || a.greater(in b, in eps);

  public static bool lessOrEqual(in this float a, in float b, in float eps = 1E-45f) => b.greaterOrEqual(in a, in eps);

  public static float abs(in this float data) => Math.Abs(data);

  public static bool equal(in this double a, in double b, in double eps = 5E-324) => a == b || Math.Abs(a - b) < eps;

  public static bool isZero(in this double a, in double eps = 5E-324) => a.equal(0.0, in eps);

  public static bool greater(in this double a, in double b, in double eps = 5E-324) => a - b > eps;

  public static bool less(in this double a, in double b, in double eps = 5E-324) => b.greater(in a, in eps);

  public static bool greaterOrEqual(in this double a, in double b, in double eps = 5E-324) => a.equal(in b) || a.greater(in b, in eps);

  public static bool lessOrEqual(in this double a, in double b, in double eps = 5E-324) => b.greaterOrEqual(in a, in eps);

  public static double abs(in this double data) => Math.Abs(data);

  public static long toRange(
    in this long value,
    in long inclusive_minimum,
    in long inclusive_maximum)
  {
    if (value < inclusive_minimum)
      return inclusive_minimum;
    return value > inclusive_maximum ? inclusive_maximum : value;
  }

  public static int toRange(in this int value, in int inclusive_minimum, in int inclusive_maximum)
  {
    if (value < inclusive_minimum)
      return inclusive_minimum;
    return value > inclusive_maximum ? inclusive_maximum : value;
  }

  public static float toRange(
    in this float value,
    in float inclusive_minimum,
    in float inclusive_maximum)
  {
    if (value < (double) inclusive_minimum)
      return inclusive_minimum;
    return value > (double) inclusive_maximum ? inclusive_maximum : value;
  }

  public static double toRange(
    in this double value,
    in double inclusive_minimum,
    in double inclusive_maximum)
  {
    if (value < inclusive_minimum)
      return inclusive_minimum;
    return value > inclusive_maximum ? inclusive_maximum : value;
  }

  public static bool isInRange(
    in this double value,
    in double inclusive_min,
    in double exclusive_max)
  {
    return value.greaterOrEqual(in inclusive_min) && value.less(in exclusive_max);
  }

  public static bool isInRange(in this float value, in float inclusive_min, in float exclusive_max) => value.greaterOrEqual(in inclusive_min) && value.less(in exclusive_max);

  public static bool isInRange(in this long value, in long inclusive_min, in long exclusive_max) => value >= inclusive_min && value < exclusive_max;

  public static bool isInRange(in this int value, in int inclusive_min, in int exclusive_max) => value >= inclusive_min && value < exclusive_max;

  public static long withMin(in this long value, in long inclusive_minimum) => value < inclusive_minimum ? inclusive_minimum : value;

  public static long withMax(in this long value, in long inclusive_maximum) => value > inclusive_maximum ? inclusive_maximum : value;

  public static int withMin(in this int value, in int inclusive_minimum) => value < inclusive_minimum ? inclusive_minimum : value;

  public static int withMax(in this int value, in int inclusive_maximum) => value > inclusive_maximum ? inclusive_maximum : value;

  public static float withMin(in this float value, in float inclusive_minimum) => (double) value < (double) inclusive_minimum ? inclusive_minimum : value;

  public static float withMax(in this float value, in float inclusive_maximum) => (double) value > (double) inclusive_maximum ? inclusive_maximum : value;

  public static double withMin(in this double value, in double inclusive_minimum) => value < inclusive_minimum ? inclusive_minimum : value;

  public static double withMax(in this double value, in double inclusive_maximum) => value > inclusive_maximum ? inclusive_maximum : value;

  public static float round(in this float a) => (float) Math.Round((double) a, 2);

  public static float roundToFloat(in this double a) => (float) Math.Round(a, 2);

  public static int roundToInt(in this float a) => (int) Math.Round((double) a);

  public static double round(in this double a) => Math.Round(a, 2);

  public static int roundToInt(in this double a) => (int) Math.Round(a);

  public static int zeroLastDigits(in int value, int divider = 100)
  {
    if (value < divider)
      divider /= 10;
    return divider == 0 ? value : value / divider * divider;
  }

  public static bool equal(in this Vector2 a, in Vector2 b, in float eps = 1E-45f) => a.X.equal(in b.X, in eps) && a.Y.equal(in b.Y, in eps);

  public static bool isZeroVector(in this Vector2 a, in float eps = 1E-45f) => a.X.equal(0.0f, in eps) && a.Y.equal(0.0f, in eps);

  public static Vector2 trimLength(in this Vector2 a, in float max_len)
  {
    float num = a.Length();
    return (double) num > (double) max_len ? a * (max_len / num) : a;
  }
}
