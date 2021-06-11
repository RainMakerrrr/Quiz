using System.Collections.Generic;
using System.Linq;
using Quiz.GameState;
using UnityEngine.UI;

public static class Utilities
{
    public static void SetAlpha(this Image image, float alpha)
    {
        var color = image.color;
        color.a = alpha;
        image.color = color;
    }

    public static IEnumerable<Level> QuickSortAscending(IEnumerable<Level> list)
    {
        if (list.Count() <= 1) return list;
        var pivot = list.First();

        var less = list.Skip(1).Where(i => i.LevelOrder <= pivot.LevelOrder);
        var greater = list.Skip(1).Where(i => i.LevelOrder > pivot.LevelOrder);

        return QuickSortAscending(less).Union(new List<Level> {pivot}).Union(QuickSortAscending(greater));
    }
}