using lights_out_assignment_master.Model;
using System;
using System.ComponentModel;
using System.Runtime.Remoting.Contexts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace lights_out_assignment_master
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool[,] onButton;
        public int CountSteps = 0;
        public string Text2 { get; set; }
        private readonly Level Level;
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            Level = SetUp.GetConfig()[0];
            onButton = SetUp.ReadButtonSettings(Level);

            InitializeComponent();

            RenderPanel(onButton);
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CountSteps = 0;
            RestartPanelDesign();
            Text2 = $"{CountSteps}";

            onButton = SetUp.ReadButtonSettings(Level);
            foreach (StackPanel element in myStackPanel.Children)
            {
                foreach (Rectangle item in element.Children)
                {
                    int row = GetRowNumberFromName(item.Name);
                    int Columns = GetColumnsNumberFromName(item.Name);

                    item.SetImage(onButton[row, Columns]);

                }
            }
            RaisePropertyChanged("Text2");

        }

        private void RenderPanel(bool[,] onButton)
        {
            for (int i = 0; i < Level.Rows; i++)
            {
                StackPanel sp = Component.GetNewStackPanel(i);

                myStackPanel.Children.Add(sp);

                for (int j = 0; j < Level.Columns; j++)
                {
                    Rectangle rec = Component.GetNewRectangle(i, j);
                    rec.SetImage(onButton[i, j]);

                    sp.Children.Add(rec);
                }
                sp.MouseLeftButtonDown += OnEllipseMouseLeftButtonDown;
            }
        }

        void OnEllipseMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Rectangle)
            {
                Rectangle ClickedRectangle = (Rectangle)e.OriginalSource;

                int Row = GetRowNumberFromName(ClickedRectangle.Name);
                int Column = GetColumnsNumberFromName(ClickedRectangle.Name);

                onButton[Row, Column] = !onButton[Row, Column];

                ClickedRectangle.SetImage(onButton[Row, Column]);

                foreach (StackPanel element in myStackPanel.Children)
                {
                    foreach (Rectangle item in element.Children)
                    {
                        UpdateToggle(item, Row, Column);
                    }
                }

                CountSteps += 1;
                Text2 = $"{CountSteps}";

                if (IsStatusDone())
                    WinPanelDesign();

                RaisePropertyChanged("Text2");
            }

        }

        private void RestartPanelDesign()
        {
            winPanel.Visibility = Visibility.Hidden;
            winText.Visibility = Visibility.Hidden;
            var bc = new BrushConverter();
            myStackPanel.Background = (Brush)bc.ConvertFrom("#FFFF4848");
        }
        private void WinPanelDesign()
        {
            winPanel.Visibility = Visibility.Visible;
            winText.Visibility = Visibility.Visible;
            var bc = new BrushConverter();
            myStackPanel.Background = (Brush)bc.ConvertFrom("#FF5ED16B");
        }
        public bool IsStatusDone()
        {
            for (int i = 0; i < Level.Rows; i++)
            {
                for (int j = 0; j < Level.Columns; j++)
                {
                    if (onButton[i, j] == true)
                    {
                        RestartPanelDesign();
                        return false;
                    }                       
                }
            }
            return true;
        }
        private void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        
        private int GetRowNumberFromName(string name) => Int32.Parse(name.Split(new string[] { "ID_" }, StringSplitOptions.None)[1].Split('_')[0].Trim());
        private int GetColumnsNumberFromName(string name) => Int32.Parse(name.Substring(name.LastIndexOf('_') + 1));

        private void UpdateToggle(Rectangle item,int Row, int Column)
        {
            if (item.Name == "ID_" + Row + "_" + (Column + 1))
            {
                onButton[Row, 1 + Column] = !onButton[Row, 1 + Column];

                item.SetImage(onButton[Row, 1 + Column]);
            }

            if (item.Name == "ID_" + Row + "_" + (Column - 1))
            {
                onButton[Row, Column - 1] = !onButton[Row, Column - 1];

                item.SetImage(onButton[Row, Column - 1]);
            }

            if (item.Name == "ID_" + (Row + 1) + "_" + (Column))
            {
                onButton[Row + 1, Column] = !onButton[Row + 1, Column];

                item.SetImage(onButton[Row + 1, Column]);
            }

            if (item.Name == "ID_" + (Row - 1) + "_" + (Column))
            {
                onButton[Row - 1, Column] = !onButton[Row - 1, Column];

                item.SetImage(onButton[Row - 1, Column]);
            }
        }

    }


}
