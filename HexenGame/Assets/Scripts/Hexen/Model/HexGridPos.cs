using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A struct that holds 3 ints to represent the position on a HexGrid.
/// </summary>
public struct HexGridPos
{
    public int Q { get; set; }
    public int R { get; set; }
    public int S { get; set; }

    public HexGridPos(int q, int r, int s)
    {
        Q = q;
        R = r;
        S = s;
    }

    /// <summary>
    /// A string with q,r,s.
    /// </summary>
    /// <returns>Strings</returns>
    public override string ToString()
    {
        return $"HexGridPos ({Q},{R},{S})";
    }

    /// <summary>
    /// Operator overload: + between 2 HexGridPos.
    /// </summary>
    /// <param name="a">HexGridPos 1</param>
    /// <param name="b">HexGridPos 2</param>
    /// <returns>HexGridPos: Sum of 2 HexGridPos</returns>
    public static HexGridPos operator+ (HexGridPos a, HexGridPos b)
    {
        HexGridPos result = new HexGridPos();

        result.Q = a.Q + b.Q ;
        result.R = a.R + b.R ;
        result.S = a.S + b.S ;
        return result ;
    }

    /// <summary>
    /// Operator overload: * Between HexGridPos and Scalar.
    /// </summary>
    /// <param name="a">HexGridPos</param>
    /// <param name="b">Scalar</param>
    /// <returns>HexGridPos: HexGridPos Multiplied with a Scalar</returns>
    public static HexGridPos operator* (HexGridPos a, int b)
    {
        HexGridPos result = new HexGridPos();

        result.Q = a.Q * b;
        result.R = a.R * b;
        result.S = a.S * b;
        return result ;
    } 
}
