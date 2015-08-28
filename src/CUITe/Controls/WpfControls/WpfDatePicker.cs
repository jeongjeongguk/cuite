﻿using System;
using CUITControls = Microsoft.VisualStudio.TestTools.UITesting.WpfControls;

namespace CUITe.Controls.WpfControls
{
    /// <summary>
    /// Wrapper class for WpfDatePicker
    /// </summary>
    public class WpfDatePicker : WpfControl<CUITControls.WpfDatePicker>
    {
        public WpfDatePicker(CUITControls.WpfDatePicker sourceControl = null, string searchProperties = null)
            : base(sourceControl ?? new CUITControls.WpfDatePicker(), searchProperties)
        {
        }

        public WpfCalendar Calendar
        {
            get
            {
                WpfCalendar calendar = new WpfCalendar((CUITControls.WpfCalendar)SourceControl.Calendar);
                return calendar;
            }
        }

        public DateTime Date
        {
            get { return SourceControl.Date; }
            set { SourceControl.Date = value; }
        }

        public string DateAsString
        {
            get { return SourceControl.DateAsString; }
            set { SourceControl.DateAsString = value; }
        }

        public bool ShowCalendar
        {
            get { return SourceControl.ShowCalendar; }
            set { SourceControl.ShowCalendar = value; }
        }
    }
}