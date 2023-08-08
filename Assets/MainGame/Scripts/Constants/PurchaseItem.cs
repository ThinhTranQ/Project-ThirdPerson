using System.Collections.Generic;

namespace MainGame.Constants
{
    public static class PurchaseItem
    {
        public static string ItemRemoveAds = "remove_ads";
        public static string ItemPremium = "sub_premium";
        public static readonly List<string> NonConsumables = new() { ItemRemoveAds };
        public static readonly List<string> ConSumables = new() { };
        public static readonly List<string> Subscriptions = new() { ItemPremium };

        public static string RemoveAdsItem => ItemRemoveAds;
    }
}