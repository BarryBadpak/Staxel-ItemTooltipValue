using Staxel;
using Staxel.Rendering;
using Sunbeam;

namespace ItemTooltipValue
{
    public class Modhook : BaseMod
    {
        public override string ModIdentifier => "InvValueTooltip";

        private bool AssetsInjected = false;
        private string DomAsset { get; set; }
        private string ScriptAsset { get; set; }
        private string StyleAsset { get; set; }

        /// <summary>
        /// Load assets
        /// </summary>
        public override void GameContextInitializeInit()
        {
            this.DomAsset = this.AssetLoader.ReadFileContent("Assets/index.min.html");
            this.ScriptAsset = this.AssetLoader.ReadFileContent("Assets/main.min.js");
            this.StyleAsset = this.AssetLoader.ReadFileContent("Assets/style.min.css");
        }

        /// <summary>
        /// Initialize mod by injecting assets
        /// They need to be injected through UniverseUpdate, when using ClientContextInitializeBefore the
        /// webpage is not ready yet, we could patch a hook to this to have a proper method hook to inject
        /// assets, but this also works for now
        /// </summary>
        public override void UniverseUpdateAfter()
        {
            this.InjectAssets();
        }

        /// <summary>
        /// Injects the UI assets
        /// </summary>
        private void InjectAssets()
        {
            if(this.AssetsInjected)
            {
                return;
            }

            WebOverlayRenderer overlay = ClientContext.WebOverlayRenderer;
            if(overlay == null)
            {
                return;
            }

            overlay.CallPreparedFunction("(() => { const el = document.createElement('style'); el.type = 'text/css'; el.appendChild(document.createTextNode('" + this.StyleAsset + "')); document.head.appendChild(el); })();");
            overlay.CallPreparedFunction("$('#Inventory_Info:not(:has(#inventory_sellvalue_box))').append(\"" + this.DomAsset + "\");");
            overlay.CallPreparedFunction(this.ScriptAsset);

            this.AssetsInjected = true;
        }
    }
}
