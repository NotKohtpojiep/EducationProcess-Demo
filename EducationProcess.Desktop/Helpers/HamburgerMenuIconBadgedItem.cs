using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;

namespace EducationProcess.Desktop.Helpers
{
    public class HamburgerMenuIconBadgedItem : HamburgerMenuItem
    {
        /// <summary>
        /// Identifies the <see cref="Icon"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(object), typeof(HamburgerMenuIconItem), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets a value that specifies an user specific object which can be used as icon.
        /// </summary>
        public object Icon
        {
            get { return GetValue(IconProperty); }

            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty BadgedProperty = DependencyProperty.Register(nameof(Badge), typeof(object), typeof(HamburgerMenuIconItem), new PropertyMetadata(null));

        public object Badge
        {
            get { return GetValue(BadgedProperty); }

            set { SetValue(BadgedProperty, value); }
        }

        protected override Freezable CreateInstanceCore()
        {
            return new HamburgerMenuIconItem();
        }
    }
}
