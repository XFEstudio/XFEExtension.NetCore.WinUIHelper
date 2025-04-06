using XFEExtension.NetCore.WinUIHelper.Profiles.CacheProfiles;
using XFEExtension.NetCore.WinUIHelper.Profiles.CrossVersionProfiles;
using PDDShopManagementSystem.Core.Model;
using XFEExtension.NetCore.StringExtension;

namespace XFEExtension.NetCore.WinUIHelper.Utilities.Helper;

public static class GoodsHelper
{
    public static List<Goods> FilterGoodsInOtherList(IEnumerable<Goods> goodsList, IEnumerable<Goods> oldGoodsList, IEnumerable<Dictionary<string, string>> orderList, IEnumerable<Dictionary<string, string>> afterSalesList)
    {
        var newOrderList = new List<Dictionary<string, string>>();
        newOrderList.AddRange(orderList);
        var newAfterSalesList = new List<Dictionary<string, string>>();
        newAfterSalesList.AddRange(afterSalesList);
        var newGoodsList = new List<Goods>();
        newGoodsList.AddRange(goodsList);
        newGoodsList.AddRange(oldGoodsList);
        var results = new List<Goods>();
        foreach (var goods in newGoodsList)
        {
            var goodsOrderList = newOrderList.Where(order => order["商品id"] == goods.ID).ToList();
            var goodsAfterSalesList = newAfterSalesList.Where(afterSales => afterSales["商品ID"] == goods.ID).ToList();
            if (goodsOrderList.Count > 0 || goodsAfterSalesList.Count > 0)
            {
                if (!oldGoodsList.Any(oldGoods => oldGoods.ID == goods.ID))
                    results.Add(goods);
            }
            foreach (var order in goodsOrderList)
                newOrderList.Remove(order);
            foreach (var afterSales in afterSalesList)
                newAfterSalesList.Remove(afterSales);
        }
        foreach (var orderEntry in newOrderList)
        {
            if (orderEntry.TryGetValue("商品id", out var orderId))
            {
                if (results.FirstOrDefault(goods => goods.ID == orderId) is Goods goodsModel)
                {
                    if (!goodsModel.SkuList.Any(sku => sku.SkuId == orderEntry["样式ID"]))
                        goodsModel.SkuList.Add(new()
                        {
                            SkuId = orderEntry["样式ID"],
                            NewSpec = orderEntry["商品规格"]
                        });
                }
                else
                {
                    results.Add(new()
                    {
                        ID = orderId,
                        GoodsDesc = orderEntry["商品"],
                        SkuList = [new()
                        {
                            SkuId = orderEntry["样式ID"],
                            NewSpec = orderEntry["商品规格"]
                        }]
                    });
                }
            }
        }
        foreach (var afterSalesEntry in newAfterSalesList)
        {
            if (afterSalesEntry.TryGetValue("商品ID", out var afterSalesGoodsId))
            {
                if (results.FirstOrDefault(goods => goods.ID == afterSalesGoodsId) is Goods goodsModel)
                {
                    if (!goodsModel.SkuList.Any(sku => sku.SkuId == afterSalesEntry["sku信息"]))
                        goodsModel.SkuList.Add(new()
                        {
                            NewSpec = afterSalesEntry["sku信息"]
                        });
                }
                else
                {
                    results.Add(new()
                    {
                        ID = afterSalesGoodsId,
                        SkuList = [new()
                        {
                            NewSpec = afterSalesEntry["sku信息"]
                        }]
                    });
                }
            }
        }
        return results;
    }

    public static List<Goods> GetAllNewGoodsInOtherList(IEnumerable<Goods> goodsList, IEnumerable<Goods> oldGoodsList, IEnumerable<Dictionary<string, string>> orderList)
    {
        var newOrderList = new List<Dictionary<string, string>>();
        newOrderList.AddRange(orderList);
        var newGoodsList = new List<Goods>();
        newGoodsList.AddRange(goodsList);
        newGoodsList.AddRange(oldGoodsList);
        var results = new List<Goods>();
        foreach (var goods in newGoodsList)
        {
            foreach (var goodsSku in goods.SkuList)
            {
                var skuOrderList = newOrderList.Where(order => order["样式ID"] == goodsSku.SkuId).ToList();
                if (skuOrderList.Count > 0)
                {
                    foreach (var order in skuOrderList)
                        newOrderList.Remove(order);
                }
            }
            if (!oldGoodsList.Any(oldGoods => oldGoods.ID == goods.ID))
                results.Add(goods);
        }
        foreach (var orderEntry in newOrderList)
        {
            if (orderEntry.TryGetValue("商品id", out var orderId))
            {
                if (results.FirstOrDefault(goods => goods.ID == orderId) is Goods goodsModel)
                {
                    if (!goodsModel.SkuList.Any(sku => sku.SkuId == orderEntry["样式ID"]))
                        goodsModel.SkuList.Add(new()
                        {
                            SkuId = orderEntry["样式ID"],
                            NewSpec = orderEntry["商品规格"]
                        });
                }
                else
                {
                    results.Add(new()
                    {
                        ID = orderId,
                        GoodsDesc = orderEntry["商品"],
                        SkuList = [new()
                        {
                            SkuId = orderEntry["样式ID"],
                            NewSpec = orderEntry["商品规格"]
                        }]
                    });
                }
            }
        }
        ClimbManager.IsGoodsInitialized = true;
        return results;
    }

    public static void ApplyGoodsCode(Goods goods, string code)
    {
        if (!code.IsNullOrWhiteSpace() && int.TryParse(code, out var targetCode) && UserCostProfile.SummonedGoodsCodeDictionary.TryGetValue(targetCode, out var targetSkuList))
            foreach (var targetSku in targetSkuList)
                if (goods.SkuList.FirstOrDefault(sku => sku.NewSpec == targetSku.NewSpec) is GoodsSku goodsSku)
                    goodsSku.Cost = targetSku.Cost;
    }

    public static void ChangeAllSameCode(Goods targetGoods)
    {
        if (!targetGoods.Code.IsNullOrWhiteSpace() && int.TryParse(targetGoods.Code, out var targetCode))
            foreach (var goods in UserCostProfile.GoodsCostList.Where(goods => goods.Code == targetGoods.Code))
                foreach (var sku in goods.SkuList)
                    if (targetGoods.SkuList.FirstOrDefault(targetSku => targetSku.NewSpec == sku.NewSpec) is GoodsSku goodsSku)
                        sku.Cost = goodsSku.Cost;
    }

    public static bool IsAllCostSet() => UserCostProfile.GoodsCostList.All(goods => goods.SkuList.All(sku => sku.IsCostSet));
    public static bool IsAllNecessaryCostSet() => UserCostProfile.GoodsCostList.Select(goods => CacheDataProfile.OrderListClimbResult!.Any(dictionary => dictionary["商品id"] == goods.ID) ? goods : default).All(goods => goods is null || goods.SkuList.All(sku => sku.IsCostSet));
}
