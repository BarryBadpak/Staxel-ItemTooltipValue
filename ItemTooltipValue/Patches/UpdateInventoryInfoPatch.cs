using Harmony;
using Staxel;
using Staxel.Client;
using Staxel.Items;
using Staxel.Items.ItemComponents;
using Staxel.Rendering;

namespace ItemTooltipValue.Patches
{
    [HarmonyPatch(typeof(InventoryController), "UpdateInventoryInfo")]
    class UpdateInventoryInfoPatch
    {
        [HarmonyPostfix]
        static void AfterUpdateInventoryInfo(Item item)
        {
            // WebOverlayRenderer is created after InventoryController (through OverlayController) so check just in case
            WebOverlayRenderer overlay = ClientContext.WebOverlayRenderer;
            if (overlay != null)
            {
                string sellPrice = item.GetComponents().Contains<PricingComponent>() ? item.GetComponents().Get<PricingComponent>().SellPrice.ToString() : "";
                overlay.Call("modShowInventoryCurrencyTooltip", sellPrice, null, null, null, null, null);
            }
        }
    }
}
