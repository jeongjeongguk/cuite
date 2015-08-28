﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using CUITe.Browsers;
using Microsoft.VisualStudio.TestTools.UITesting;
using CUITControls = Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;

namespace CUITe.Controls
{
    /// <summary>
    /// Base wrapper class for all CUITe controls
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ControlBase<T> : ControlBase
        where T : UITestControl
    {
        private readonly T sourceControl;
        private readonly PropertyExpressionCollection searchProperties;

        protected ControlBase(T sourceControl, string searchProperties = null)
            : base(sourceControl)
        {
            if (sourceControl == null)
                throw new ArgumentNullException("sourceControl");

            this.sourceControl = sourceControl;
            this.searchProperties = new PropertyExpressionCollection();

            SetSearchProperties(searchProperties);
            SourceControl.SearchProperties.AddRange(this.searchProperties);
        }

        public T SourceControl
        {
            get { return sourceControl; }
        }

        /// <summary>
        /// Wraps the adding of search properties for the UITestControl where
        /// the property expression is 'EqualTo'.
        /// </summary>
        /// <param name="sPropertyName"></param>
        /// <param name="sValue"></param>
        public void SetSearchProperty(string sPropertyName, string sValue)
        {
            SourceControl.SearchProperties.Add(sPropertyName, sValue, PropertyExpressionOperator.EqualTo);
        }

        /// <summary>
        /// Wraps the adding of search properties for the UITestControl where
        /// the property expression is 'Contains'.
        /// </summary>
        /// <param name="sPropertyName"></param>
        /// <param name="sValue"></param>
        public void SetSearchPropertyRegx(string sPropertyName, string sValue)
        {
            SourceControl.SearchProperties.Add(sPropertyName, sValue, PropertyExpressionOperator.Contains);
        }
        
        /// <summary>
        /// Gets the CUITe UI control object from the descendants of this control using the search parameters are passed. 
        /// You don't have to create the object repository entry for this.
        /// </summary>
        /// <typeparam name="T">Pass the CUITe control you are looking for.</typeparam>
        /// <param name="searchProperties">In 'Key1=Value1;Key2=Value2' format. For example 'Id=firstname' 
        /// or use '~' for Contains such as 'Id~first'</param>
        /// <returns>CUITe control object</returns>
        public TControl Get<TControl>(string searchProperties = null) where TControl : ControlBase
        {
            var control = ControlBaseFactory.Create<TControl>(null, searchProperties);
            control.SourceControl.Container = SourceControl;
            return control;
        }

        public void SetSearchProperties(string searchProperties)
        {
            // fill the UITestControl's search properties based on the search string provided

            // iterate through the class inheritance hierarchy to get a list of property names for the specific control
            // Note: Some properties may not be valid to use for search (ex. filter property names). MS does not provide and exact list
            List<FieldInfo> controlProperties = new List<FieldInfo>();

            Type nestedType = typeof(T);
            Type nestedPropertyNamesType = nestedType.GetNestedType("PropertyNames");

            while (nestedType != typeof(object))
            {
                if (nestedPropertyNamesType != null)
                {
                    controlProperties.AddRange(nestedPropertyNamesType.GetFields());
                }

                nestedType = nestedType.BaseType;
                nestedPropertyNamesType = nestedType.GetNestedType("PropertyNames");
            }

            if (searchProperties == null)
            {
                return;
            }

            // Split on groups of key/value pairs
            string[] saKeyValuePairs = searchProperties.Split(
                new[] { ';' },
                StringSplitOptions.RemoveEmptyEntries);

            foreach (string sKeyValue in saKeyValuePairs)
            {
                PropertyExpressionOperator compareOperator = PropertyExpressionOperator.EqualTo;

                // If split on '=' does not work, then try '~'
                // Split at the first instance of '='. Other instances are considered part of the value.
                string[] saKeyVal = sKeyValue.Split(
                    new[] { '=' },
                    2);
                if (saKeyVal.Length != 2)
                {
                    // Otherwise try to split on '~'. If it works then compare type is Contains
                    // Split at the first instance of '~'. Other instances are considered part of the value.
                    saKeyVal = sKeyValue.Split(
                        new[] { '~' },
                        2);
                    if (saKeyVal.Length == 2)
                    {
                        compareOperator = PropertyExpressionOperator.Contains;
                    }
                    else
                    {
                        throw new InvalidSearchParameterFormatException(searchProperties);
                    }
                }

                // Find the first property in the list of known values
                string valueName = saKeyVal[0];

                if ((typeof(T).IsSubclassOf(typeof(CUITControls.HtmlControl))) && (valueName.Equals("Value", StringComparison.OrdinalIgnoreCase)))
                {
                    //support for backward compatibility where search properties like "Value=Log In" are used
                    valueName += "Attribute";
                }

                FieldInfo foundField = controlProperties.Find(
                    searchProperty => searchProperty.Name.Equals(valueName, StringComparison.OrdinalIgnoreCase));

                if (foundField == null)
                {
                    throw new InvalidSearchKeyException(valueName, searchProperties, controlProperties.Select(x => x.Name).ToList());
                }

                // Add the search property, value and type
                this.searchProperties.Add(foundField.GetValue(null).ToString(), saKeyVal[1], compareOperator);
            }
        }

        /// <summary>
        /// Clicks on the center of the UITestControl based on its point on the screen.
        /// This may "work-around" Coded UI tests (on third-party controls) that throw the following exception:
        /// Microsoft.VisualStudio.TestTools.UITest.Extension.FailedToPerformActionOnBlockedControlException: Another control is blocking the control. Please make the blocked control visible and retry the action.
        /// </summary>
        public void PointAndClick()
        {
            WaitForControlReady();
            int x = SourceControl.BoundingRectangle.X + SourceControl.BoundingRectangle.Width / 2;
            int y = SourceControl.BoundingRectangle.Y + SourceControl.BoundingRectangle.Height / 2;
            Mouse.Click(new Point(x, y));
        }

        protected PropertyExpressionCollection SearchProperties
        {
            get { return searchProperties; }
        }

        /// <summary>
        /// Run/evaluate JavaScript code in the DOM context.
        /// </summary>
        /// <param name="code">The JavaScript code.</param>
        protected void RunScript(string code)
        {
            BrowserWindow browserWindow = (BrowserWindow)SourceControl.TopParent;
            InternetExplorer.RunScript(browserWindow, code);
        }
    }
}