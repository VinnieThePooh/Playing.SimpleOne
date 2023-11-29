using Playing.GeometryTools.Exceptions;
using Playing.GeometryTools.Models;

namespace Playing.GeometryTools;

public static class TriangleTools
{
    /// <summary>
    /// Возвращает тип треугольника по трём сторонам
    /// </summary>
    /// <param name="a">Длина первой стороны</param>
    /// <param name="b">Длина второй стороны</param>
    /// <param name="c">Длина третей стороны</param>
    /// <param name="comparePrecision">Число знаков после запятой в операция сравнения FPU-чисел</param>
    /// <returns></returns>
    public static TriangleType GetTriangleType(double a, double b, double c, int comparePrecision = 2)
    {
        if (a <= 0 || b <= 0 || c <= 0)
            throw new ArgumentException($"Incorrect side values: ({a}, {b}, {c})");
            
        if (a + b <= c || b + c <= a || a + c <= b)
            throw new InvalidTriangleSidesException($"Incorrect triangle sides: ({a}, {b}, {c})");
        return GetTypeV1(a, b, c, comparePrecision);
    }
    
    private static TriangleType GetTypeV1(double a, double b, double c, int comparePrecision = 2)
    {
        var pi = Math.PI;
        var halfPi = Math.Round(pi / 2, comparePrecision);
        
        //Notes:
        //1. Не переводим в градусы, сравнения в радианах
        //2. Конфигурируемая точность в операциях сравнения - надо ли?
        //3. Формулы: https://ab.al-shell.ru/articles/kak-nayti-ugly-proizvolnogo-treugolnika
        var cosA = (b * b + c * c - a * a) / (2 * b * c);
        var A = Math.Acos(cosA);
        if (Math.Round(A, comparePrecision) == halfPi)
            return TriangleType.RightAngled;
        
        var cosB = (a * a + c * c - b * b) / (2 * a * c);
        var B = Math.Acos(cosB);
        if (Math.Round(B, comparePrecision) == halfPi)
            return TriangleType.RightAngled;
        
        var C = pi - B - A;
        if (Math.Round(C, comparePrecision) == halfPi)
            return TriangleType.RightAngled;
        
        if (A < halfPi && B < halfPi && C < halfPi)
            return TriangleType.AcuteAngled;
        
        return TriangleType.ObtuseAngled;
    }
}