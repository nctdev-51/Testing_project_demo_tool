using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicExercises.Exercises;

public static class Triangle
{
    public static string Classify(int a, int b, int c)
    {
        if (a <= 0 || b <= 0 || c <= 0) return "Invalid";
        if (a + b <= c || a + c <= b || b + c <= a) return "NotTriangle";
        if (a == b && b == c) return "Equilateral";
        if (a == b || b == c || a == c) return "Isosceles";
        return "Scalene";
    }
}