﻿using CUITControls = Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;

namespace CUITe.Controls.HtmlControls
{
    public class HtmlSpan : HtmlControl<CUITControls.HtmlSpan>
    {
        public HtmlSpan()
        {
        }

        public HtmlSpan(string searchParameters)
            : base(searchParameters)
        {
        }
    }
}