using System;
using System.Collections.Generic;
using System.IO;
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

            StackPanel sPanelScrollView = this.scrollViewerStackPanel;
            var allLines = File.ReadAllLines("C:\\Sheet1.csv").Select(a => a.Split('\n'));

            var csvLines = allLines.ToArray();
            string[] strArray = new string[csvLines.Length];
            int arCount=0;

            foreach (var linesIn in allLines)
            {
                
                foreach (var line in linesIn)
                {
                    //    Console.WriteLine(" " + line.ToString());
                    strArray[arCount++] = line.ToString();
                }
            }


            for (int j = 0; j < strArray.Length; j++)
            {
                String str = strArray[j];
                String[] strList = str.Split(',');

                Grid ngrid=createNewGrid(strList[0], strList[1], strList[2], strList[3]);
                sPanelScrollView.Children.Add(ngrid);

            }






           
        }



        public Grid createNewGrid(String team1,String team2,String team3,String team4) {
            Grid sPanelGrid = new Grid();
            Thickness thickness = new Thickness();
            thickness.Right = thickness.Left =  thickness.Bottom = 10; thickness.Top = 0;
            sPanelGrid.Margin = thickness;
            ColumnDefinition cdef1 = createNewColumnDefinitionOfUnitLength();
            ColumnDefinition cdef2 = createNewColumnDefinitionOfUnitLength();
            ColumnDefinition cdef3 = createNewColumnDefinitionOfUnitLength();
            ColumnDefinition cdef4 = createNewColumnDefinitionOfUnitLength();
            ColumnDefinition cdef5 = createNewColumnDefinitionOfUnitLength();
            ColumnDefinition cdef6 = createNewColumnDefinitionOfUnitLength();

            sPanelGrid.ColumnDefinitions.Add(cdef1);
            sPanelGrid.ColumnDefinitions.Add(cdef2);
            sPanelGrid.ColumnDefinitions.Add(cdef3);
            sPanelGrid.ColumnDefinitions.Add(cdef4);
            sPanelGrid.ColumnDefinitions.Add(cdef5);
            sPanelGrid.ColumnDefinitions.Add(cdef6);

            RadioButton rbutton = new RadioButton();
            rbutton.GroupName = "MyGroup";
            rbutton.Content = "";
            sPanelGrid.Children.Add(rbutton);
            Grid.SetColumn(rbutton,0);


            Label t1 = createLabel(team1);
            sPanelGrid.Children.Add(t1);
            Grid.SetColumn(t1, 1);

            Label t2 = createLabel(team2);
            sPanelGrid.Children.Add(t2);
            Grid.SetColumn(t2, 2);

            Label t3 = createLabel(team3);
            sPanelGrid.Children.Add(t3);
            Grid.SetColumn(t3, 3);



            Label t4 = createLabel(team4);
            sPanelGrid.Children.Add(t4);
            Grid.SetColumn(t4, 4);

            return sPanelGrid;
        }



        public ColumnDefinition createNewColumnDefinitionOfUnitLength() {

            ColumnDefinition cdef= new ColumnDefinition();
            cdef.Width = new GridLength(1, GridUnitType.Star);
            return cdef;
        }


        public Label createLabel(String content) {
            Label lbl = new Label();
            lbl.Content = content;
            return lbl;

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
