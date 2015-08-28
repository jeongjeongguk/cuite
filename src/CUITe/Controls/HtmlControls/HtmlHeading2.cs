﻿using CUITControls = Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;

namespace CUITe.Controls.HtmlControls
{
    public class HtmlHeading2 : HtmlCustom
    {
        private const string _tagName = "h2";

        public HtmlHeading2(CUITControls.HtmlCustom sourceControl = null, string searchProperties = null)
            : base(_tagName, sourceControl, searchProperties)
        {
        }
    }
}