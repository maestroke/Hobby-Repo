﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DaanLib.Menu {
    /// <summary>
    /// A factory to create a menu based on specific requirements
    /// </summary>
    public class MenuBuilderFactory {
        /// <summary>
        /// Builds a menu based on the parameters given
        /// </summary>
        /// <typeparam name="T">The type of data the menu should hold</typeparam>
        /// <param name="tabSize">The size of a tab</param>
        /// <param name="parentControl">The size of the parent control</param>
        /// <param name="appearance">The appearance of the menu<para>Leave null for default values</para></param>
        /// <returns>A Menu that conforms to the given parameters</returns>
        public IMenu<T> Build<T>(Size tabSize, Control parentControl, MenuAppearance appearance = null) {
            var menu = new Menu<T> {
                appearance = appearance ?? MenuAppearance.GetDefaultAppearance(),
                allowRightClick = false,
                tabSize = tabSize,
                parentControl = parentControl,
                currentTabIndex = -1,
                
            };

            return menu;
        }
    }
}