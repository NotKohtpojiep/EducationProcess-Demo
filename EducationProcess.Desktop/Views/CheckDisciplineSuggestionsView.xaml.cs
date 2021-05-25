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
using EducationProcess.Desktop.ViewModels;

namespace EducationProcess.Desktop.Views
{
    /// <summary>
    /// Логика взаимодействия для TeachersView.xaml
    /// </summary>
    public partial class CheckDisciplineSuggestionsView : UserControl
    {
        public CheckDisciplineSuggestionsView()
        {
            InitializeComponent();
            DataContext = new CheckDisciplineSuggestionsViewModel();
        }
    }
}
