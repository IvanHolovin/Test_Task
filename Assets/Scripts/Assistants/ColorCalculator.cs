using System.Collections.Generic;
using DefaultNamespace.Vegitables;
using UnityEngine;

namespace DefaultNamespace.Assistants
{
    public class ColorCalculator : MonoBehaviour
    {
        public Color CalculateColor(List<Vegetable> vegetables)
        {
            Color resultColor = new Color();
            foreach (var vegetable in vegetables)
            {
                resultColor += vegetable.Color();
            }
            resultColor /= vegetables.Count;
            return resultColor;
        }

        public float CalculateResult(LevelData levelData, List<Vegetable> vegetables)
        {
            float result = 0f;
            Color resultColor = CalculateColor(vegetables);
            Color levelColor = levelData.ColorResult();

            result = Mathf.Abs(resultColor.r - levelColor.r) + 
                     Mathf.Abs(resultColor.g - levelColor.g) + 
                     Mathf.Abs(resultColor.b - levelColor.b);
            
            result = 100 - result * 33.33f;
            return result;
        }
    }
}