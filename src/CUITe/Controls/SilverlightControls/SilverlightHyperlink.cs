﻿#if SILVERLIGHT_SUPPORT
using CUITControls = Microsoft.VisualStudio.TestTools.UITesting.SilverlightControls;

namespace CUITe.Controls.SilverlightControls
{
    /// <summary>
    /// CUITe wrapper for SilverlightHyperlink.
    /// </summary>
    public class SilverlightHyperlink : SilverlightControl<CUITControls.SilverlightHyperlink>
    {
        public SilverlightHyperlink(CUITControls.SilverlightHyperlink sourceControl = null, string searchProperties = null)
            : base(sourceControl ?? new CUITControls.SilverlightHyperlink(), searchProperties)
        {
        }
    }
}
#endif