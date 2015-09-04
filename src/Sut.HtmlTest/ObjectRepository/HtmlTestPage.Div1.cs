﻿using CUITe.Controls.HtmlControls;

namespace Sut.HtmlTest.ObjectRepository
{
    public class Div1 : HtmlDiv
    {
        // TODO: The factory should be able to create instances with default constructor
        // ReSharper disable once UnusedParameter.Local
        // the constructor requires a parameter in order for it to be dynamically created by CUITe
        public Div1(string searchProperties)
            : base("id=div1")
        {
        }

        public Div2 div2
        {
            get { return Find<Div2>(); }
        }
    }
}