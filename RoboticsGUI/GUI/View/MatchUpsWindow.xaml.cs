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
using System.Windows.Shapes;

namespace Robotics.GUI.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MatchUpsWindow : Window
    {
        public MatchUpsWindow()
        {
            InitializeComponent();

            Grid newGrid=(Grid)this.GridWithSchoolNames;
            Label label = new Label();
            label.Content = "School test";
            label.SetValue(Grid.ColumnProperty,4);
            newGrid.Children.Add(label);

        }

        private void checkBox1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
