﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace EducationProcess.Desktop.Views
{
    /// <summary>
    /// Логика взаимодействия для UmoManagerMainView.xaml
    /// </summary>
    public partial class UmoManagerMainView : UserControl
    {
        public UmoManagerMainView()
        {
            InitializeComponent();
        }
        private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            this.HamburgerMenuControl.Content = e.InvokedItem;
        }
    }
}
