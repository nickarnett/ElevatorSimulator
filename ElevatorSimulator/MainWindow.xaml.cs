﻿using System.Windows;
using System.Windows.Controls.Ribbon;
using ElevatorApp.Util;

namespace ElevatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            
            this.DataContext = new SimulatorViewModel() { SelectedElevatorNumber = 1 };
        }

        private void Logger_OnItemLogged(object sender, Logger.Event @event)
        {
            // this.txtBxLog.Text += $"{@event}\n";
        }

        public SimulatorViewModel ViewModel
        {
            get
            {
                if (this.DataContext is SimulatorViewModel vm)
                    return vm;

                return null;
            }
        }

        private void Init_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel?.Controller.Init();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel?.Logger.ClearItems();
        }

        private void clkSelectElevator(object sender, RoutedEventArgs e)
        {
        }

        private void ListView_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement el && el.Tag is int num)
            {
                if (this.ViewModel.SelectedElevatorNumber != num)
                    this.ViewModel.SelectedElevatorNumber = num;
            }

        }
    }
}
