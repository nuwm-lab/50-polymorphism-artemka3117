

using System;
using System.Collections.Generic;

public class HalfPlane
{
    private double _a1, _a2, _b;
    public double A1 => _a1;
    public double A2 => _a2;
    public double B => _b;

    public HalfPlane(double a1, double a2, double b)
    {
        _a1 = a1;
        _a2 = a2;
        _b = b;
    }

    /// <summary>
    /// Виводить коефіцієнти півплощини
    /// </summary>
    public virtual void PrintCoefficients()
    {
        Console.WriteLine($"Коефіцієнти півплощини: a1={_a1}, a2={_a2}, b={_b}");
    }

    /// <summary>
    /// Перевіряє, чи належить точка півплощині
    /// </summary>
    public virtual bool ContainsPoint(double x1, double x2)
    {
        return _a1 * x1 + _a2 * x2 <= _b;
    }

    public override string ToString() => $"HalfPlane: a1={_a1}, a2={_a2}, b={_b}";
}

// Похідний клас "півпростір"
public class HalfSpace : HalfPlane
{
    private double _a3;
    public double A3 => _a3;

    public HalfSpace(double a1, double a2, double a3, double b) : base(a1, a2, b)
    {
        _a3 = a3;
    }

    /// <summary>
    /// Виводить коефіцієнти півпростору
    /// </summary>
    public override void PrintCoefficients()
    {
        Console.WriteLine($"Коефіцієнти півпростору: a1={A1}, a2={A2}, a3={_a3}, b={B}");
    }

    /// <summary>
    /// Перевіряє, чи належить точка півпростору
    /// </summary>
    public bool ContainsPoint(double x1, double x2, double x3)
    {
        return A1 * x1 + A2 * x2 + _a3 * x3 <= B;
    }

    public override string ToString() => $"HalfSpace: a1={A1}, a2={A2}, a3={_a3}, b={B}";
}

class Program
{
    static double[] ReadDoubles(string prompt, int count)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            var input = Console.ReadLine();
            if (input == null)
            {
                Console.WriteLine("Ввід перервано.");
                return null;
            }
            var arr = input.Split();
            if (arr.Length != count)
            {
                Console.WriteLine($"Помилка: потрібно {count} чисел.");
                continue;
            }
            var result = new double[count];
            bool ok = true;
            for (int i = 0; i < count; i++)
                ok &= double.TryParse(arr[i], out result[i]);
            if (ok) return result;
            Console.WriteLine("Помилка: некоректний ввід.");
        }
    }

    static void Main()
    {
        var objects = new List<HalfPlane>();
        while (true)
        {
            Console.WriteLine("Виберіть тип об'єкта: 1 - Півплощина, 2 - Півпростір, 0 - Вихід");
            var choice = Console.ReadLine();
            if (choice == "0") break;
            if (choice == "1")
            {
                var hpCoeffs = ReadDoubles("Введіть коефіцієнти для півплощини (a1 a2 b):", 3);
                if (hpCoeffs == null) continue;
                HalfPlane obj = new HalfPlane(hpCoeffs[0], hpCoeffs[1], hpCoeffs[2]);
                objects.Add(obj);
            }
            else if (choice == "2")
            {
                var hsCoeffs = ReadDoubles("Введіть коефіцієнти для півпростору (a1 a2 a3 b):", 4);
                if (hsCoeffs == null) continue;
                HalfPlane obj = new HalfSpace(hsCoeffs[0], hsCoeffs[1], hsCoeffs[2], hsCoeffs[3]);
                objects.Add(obj);
            }
            else
            {
                Console.WriteLine("Некоректний вибір.");
            }
        }

        Console.WriteLine("\nВсі створені об'єкти:");
        foreach (var obj in objects)
        {
            obj.PrintCoefficients();
            Console.WriteLine(obj);
        }
    }
}
