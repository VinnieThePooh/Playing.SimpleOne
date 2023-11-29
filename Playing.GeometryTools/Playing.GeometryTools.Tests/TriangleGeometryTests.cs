using System.Runtime.InteropServices;
using FluentAssertions;
using Playing.GeometryTools.Exceptions;
using Playing.GeometryTools.Models;

namespace Playing.GeometryTools.Tests;

public class TriangleGeometryTests
{
    [Fact]
    public void GetTriangleType_ThrowsExceptions()
    {
        double a = 0, b = 20, c = 30;
        Assert.Throws<ArgumentException>(() => TriangleTools.GetTriangleType(a, b, c));
        a = -3;
        Assert.Throws<ArgumentException>(() => TriangleTools.GetTriangleType(a, b, c));
        a = 5;
        Assert.Throws<InvalidTriangleSidesException>(() => TriangleTools.GetTriangleType(a, b, c));
    }

    [Fact]
    public void GetTriangleType_NaiveLogicChecks()
    {
        //прямоугольный
        double a = 3, b = 4, c = 5;
        var type = TriangleTools.GetTriangleType(a, b, c);
        type.Should().Be(TriangleType.RightAngled);
        
        //Notes: 
        //1. Генератор треугольников: https://matematika-club.ru/generator-treugolnikov
        //остроугольный
        a = 5;
        b = 1.5;
        c = 5.146;
        type = TriangleTools.GetTriangleType(a, b, c);
        type.Should().Be(TriangleType.AcuteAngled);
        
        //тупоугольный
        a = 15.966;
        b = 5.698;
        c = 12;
        type = TriangleTools.GetTriangleType(a, b, c);
        type.Should().Be(TriangleType.ObtuseAngled);
    }
    
    [Theory]
    [InlineData(3)]
    [InlineData(5)]
    public void GetTriangleType_PreciseChecks(int precision)
    {
        //прямоугольный
        double a = 3, b = 4, c = 5;
        var type = TriangleTools.GetTriangleType(a, b, c, precision);
        type.Should().Be(TriangleType.RightAngled);
        
        //Notes: 
        //1. Генератор треугольников: https://matematika-club.ru/generator-treugolnikov
        //остроугольный
        a = 5;
        b = 1.5;
        c = 5.146;
        type = TriangleTools.GetTriangleType(a, b, c);
        type.Should().Be(TriangleType.AcuteAngled);
        
        //тупоугольный
        a = 16.966281;
        b = 5.698684;
        c = 12;
        type = TriangleTools.GetTriangleType(a, b, c);
        type.Should().Be(TriangleType.ObtuseAngled);
    }
}