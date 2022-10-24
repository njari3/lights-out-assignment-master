using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace lights_out_assignment_master
{
    public static class Component
    {
        public static StackPanel GetNewStackPanel(int i) => new StackPanel
        {
            Name = "myPanel" + i,
            Orientation = Orientation.Horizontal
        };

        public static Rectangle GetNewRectangle(int row, int column)
        {
            return new Rectangle
            {
                Fill = new SolidColorBrush(Colors.Black),
                Width = 100,
                Height = 100,
                Margin = new Thickness(1),
                Name = $"ID_{row}_{column}"

            };
        }
        public static void SetImage(this Rectangle rec, bool status)
        {
            ImageBrush imgBrush = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(getSource(status), UriKind.Relative))
            };
            rec.Fill = imgBrush;
        }
        private static string getSource(bool status) => $"Img/IMAGE_{(status ? "on" : "off")}.png";
    }
}
