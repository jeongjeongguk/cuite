﻿using CUITControls = Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;

namespace CUITe.Controls.HtmlControls
{
    public class HtmlXml : HtmlCustom
    {
        private const string _tagName = "xml";

        public HtmlXml(CUITControls.HtmlCustom sourceControl = null, string searchProperties = null)
            : base(_tagName, sourceControl, searchProperties)
        {
        }
    }
}