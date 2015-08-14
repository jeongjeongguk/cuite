﻿using Microsoft.VisualStudio.TestTools.UITesting;
using CUITControls = Microsoft.VisualStudio.TestTools.UITesting.WpfControls;

namespace CUITe.Controls.WpfControls
{
    /// <summary>
    /// Wrapper class for WpfToolBar
    /// </summary>
    public class WpfToolBar : WpfControl<CUITControls.WpfToolBar>
    {
        public WpfToolBar()
        {
        }

        public WpfToolBar(string searchParameters)
            : base(searchParameters)
        {
        }

        public string Header
        {
            get { return UnWrap().Header; }
        }

        public UITestControlCollection Items
        {
            get { return UnWrap().Items; }
        }
    }
}