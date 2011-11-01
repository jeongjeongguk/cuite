﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;

namespace CUITe.Controls.HtmlControls
{
    /// <summary>
    /// CUITe wrapper for HtmlComboBox.
    /// </summary>
    public class CUITe_HtmlComboBox : CUITe_HtmlControl<HtmlComboBox>
    {
        public CUITe_HtmlComboBox() : base() { }
        public CUITe_HtmlComboBox(string sSearchParameters) : base(sSearchParameters) { }

        /// <summary>
        /// Selects the item in the combobox.
        /// </summary>
        /// <param name="sItem">Item as string</param>
        public void SelectItem(string sItem)
        {
            this._control.WaitForControlReady();
            this._control.SelectedItem = sItem;
        }

        /// <summary>
        /// Selects the item in the combobox by index.
        /// </summary>
        /// <param name="index">index of item</param>
        public void SelectItem(int index)
        {
            this._control.WaitForControlReady();
            this._control.SelectedIndex = index;
        }

        /// <summary>
        /// Gets the selected item in a combobox.
        /// </summary>
        public string SelectedItem
        {
            get 
            {
                this._control.WaitForControlReady();
                return this._control.SelectedItem; 
            }
        }

        /// <summary>
        /// Gets the selected index in a combobox.
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                this._control.WaitForControlReady();
                return this._control.SelectedIndex;
            }
        }

        /// <summary>
        /// Gets the count of the items in the combobox.
        /// </summary>
        public int ItemCount
        {
            get
            {
                this._control.WaitForControlReady();
                return this._control.ItemCount;
            }
        }

        /// <summary>
        /// Gets the items in a string array.
        /// </summary>
        public string[] Items
        {
            get
            {
                string[] saTemp = new string[this.ItemCount];
                UITestControlCollection col = this._control.Items;
                int i = -1;
                foreach (UITestControl con in col)
                {
                    i++;
                    var newcon = con as HtmlControl;
                    if (newcon.InnerText != null && newcon.InnerText.Trim() != "")
                    {
                        saTemp[i] = newcon.InnerText;
                    }
                }
                return saTemp;
            }
        }
    }
}
