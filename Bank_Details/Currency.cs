using System;
using System.Numerics;

public class Currency
{
    int total;
    public Currency()
    {
        int r, p;

        Console.Write("Enter the Rupees: ");
        r = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter the Paisa: ");
        p = Convert.ToInt32(Console.ReadLine());

        total = r * 100 + p;
    }
    public Currency(int r, int p)
    {
        total = r * 100 + p;
    }

    public override string ToString()
    {
        int r = total / 100;
        int p = total % 100;

        return "₹" + r + "." + p.ToString("D2");
    }
    public static Currency operator +(Currency c1, Currency c2)
    {
        return new Currency(0, c1.total + c2.total);
    }

    public static Currency operator -(Currency c1, Currency c2)
    {
        return new Currency(0, c1.total - c2.total);
    }

    public static bool operator <(Currency c1, Currency c2)
    {
        return c1.total < c2.total;
    }

    public static bool operator >(Currency c1, Currency c2)
    {
        return c1.total > c2.total;
    }

    public static bool operator >=(Currency c1, Currency c2)
    {
        return c1.total >= c2.total;
    }

    public static bool operator <=(Currency c1, Currency c2)
    {
        return c1.total <= c2.total;
    }

    public static bool operator ==(Currency c1, Currency c2)
    {
        return c1.total == c2.total;
    }

    public static bool operator !=(Currency c1, Currency c2)
    {
        return c1.total != c2.total;
    }
    ~Currency()
    {

    }
}