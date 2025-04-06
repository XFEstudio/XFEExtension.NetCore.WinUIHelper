using PDDShopManagementSystem.Core.Model;

namespace XFEExtension.NetCore.WinUIHelper.Utilities.Helper;

public static class Extension
{
    public static IEnumerable<Goods> GetNewGoods(this IEnumerable<Goods> newGoodsList, IEnumerable<Goods> oldGoodsList, IEnumerable<Dictionary<string, string>> orderList) => GoodsHelper.GetAllNewGoodsInOtherList(newGoodsList, oldGoodsList, orderList);

    public static FixedCost GetForCurrentFixedCost(this FixedCost fixedCost, DateTime startTime, DateTime endTime)
    {
        var deltaTime = endTime - startTime;
        var newFixedCost = new FixedCost
        {
            StaffCost = fixedCost.StaffCost * deltaTime.TotalDays / 30,
            PlacementCost = fixedCost.PlacementCost * deltaTime.TotalDays / 365,
            WaterAndElectricityCost = fixedCost.WaterAndElectricityCost * deltaTime.TotalDays / 365,
            CustomCost = fixedCost.CustomCost * deltaTime.TotalDays / 365
        };
        return newFixedCost;
    }

    public static bool IsEqualTo<T>(this IList<T> list, IList<T> targetList, Func<T, T, bool> predict)
    {
        if (list.Count != targetList.Count) return false;
        var isEqual = true;
        for (var i = 0; i < list.Count; i++)
        {
            if (!predict(list[i], targetList[i]))
                isEqual = false;
        }
        return isEqual;
    }

    public static double GetNormalDouble(this double number) => double.IsNormal(number) ? number : 0;
}
