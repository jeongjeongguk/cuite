﻿using CUITControls = Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;

namespace CUITe.Controls.HtmlControls
{
    public class HtmlHeading5 : HtmlCustom
    {
        private const string _tagName = "h5";

        public HtmlHeading5(CUITControls.HtmlCustom sourceControl = null, string searchProperties = null)
            : base(_tagName, sourceControl, searchProperties)
        {
        }
    }
}