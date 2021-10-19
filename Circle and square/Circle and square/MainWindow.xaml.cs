using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Circle_and_square
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CanvasShapes CanvasShapes;
        private Shape ActiveShape;

        public MainWindow()
        {
            InitializeComponent();
            CanvasShapes = new CanvasShapes();
            CanvasShapes.AddShape(Sqaure0);
            CanvasShapes.AddShape(Circle0);
            ActiveShape = CanvasShapes.GetShape(0);
            ActiveShapeComboBox.Items.Add(ActiveShape.Name);
            ActiveShape = CanvasShapes.GetShape(1);
            ActiveShapeComboBox.Items.Add(ActiveShape.Name);
            ActiveShape = CanvasShapes.GetShape(0);
            ColorRectangle.Fill = Brushes.Black;
        }
        private void LoadProperties()
        {/*
          * Loads slected Shape object properties into controls
          */
            XPositionSlider.Value = (double)Canvas.GetLeft(ActiveShape);
            YPositionSlider.Value = (double)Canvas.GetTop(ActiveShape);
            SizeSlider.Value = ActiveShape.Width;
            SizeTextBox.Text = ActiveShape.Width.ToString();
            SizeSlider.Value = ActiveShape.Width;
            SizeTextBox.Text = ActiveShape.Width.ToString();

            FillCheckBox.IsChecked = ActiveShape.Fill != null;

            SolidColorBrush colorBrush = (SolidColorBrush)ActiveShape.Stroke;
            BlueSlider.Value = colorBrush.Color.B;
            GreenSlider.Value = colorBrush.Color.G;
            RedSlider.Value = colorBrush.Color.R;
        }

        private void ActiveShapeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ActiveShapeComboBox.SelectedIndex != -1)
            {
                ActiveShape = CanvasShapes.GetShape(ActiveShapeComboBox.SelectedIndex);
                LoadProperties();
            }
        }

        private void AddShapeButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            if (CanvasShapes.HowManyShapes() != 0)
            {
                if (b == AddCircleButton)
                {
                    Ellipse ellipse = new()
                    {
                        Width = 50,
                        Height = 50,
                        Stroke = Brushes.Black,
                        Name = "Circle" + CanvasShapes.HowManyCircles(),
                    };
                    ShapesCanvas.Children.Add(ellipse);
                    Canvas.SetTop(ellipse, 225);
                    Canvas.SetLeft(ellipse, 225);
                    CanvasShapes.AddShape(ellipse);
                }
                else
                {
                    Rectangle rectangle = new()
                    {
                        Width = 50,
                        Height = 50,
                        Stroke = Brushes.Black,
                        Name = "Square" + CanvasShapes.HowManySquares(),
                    };
                    ShapesCanvas.Children.Add(rectangle);
                    Canvas.SetTop(rectangle, 225);
                    Canvas.SetLeft(rectangle, 225);
                    CanvasShapes.AddShape(rectangle);
                }
            }
            else
            {
                if (b == AddCircleButton)
                {
                    Ellipse ellipse = new()
                    {
                        Width = ActiveShape.Width,
                        Height = ActiveShape.Height,
                        Stroke = ActiveShape.Stroke,
                        Name = "Circle" + CanvasShapes.HowManyCircles(),
                    };
                    Canvas.SetTop(ellipse, Canvas.GetTop(ActiveShape));
                    Canvas.SetLeft(ellipse, Canvas.GetTop(ActiveShape));
                    ShapesCanvas.Children.Add(ellipse);
                    CanvasShapes.AddShape(ellipse);
                }
                else
                {
                    Rectangle rectangle = new()
                    {
                        Width = ActiveShape.Width,
                        Height = ActiveShape.Height,
                        Stroke = ActiveShape.Stroke,
                        Name = "Square" + CanvasShapes.HowManySquares(),
                     };
                    Canvas.SetTop(rectangle, Canvas.GetTop(ActiveShape));
                    Canvas.SetLeft(rectangle, Canvas.GetTop(ActiveShape));
                    ShapesCanvas.Children.Add(rectangle);
                    CanvasShapes.AddShape(rectangle);
                }
            }
            ActiveShape = CanvasShapes.GetShape(CanvasShapes.HowManyShapes() - 1);
            ActiveShapeComboBox.Items.Add(ActiveShape.Name);
            ActiveShapeComboBox.SelectedIndex = CanvasShapes.HowManyShapes() - 1;
            ActiveShapeComboBox.SelectedItem = CanvasShapes.HowManyShapes() - 1;
        }

        private void ColorSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = (Slider)sender;
            SolidColorBrush colorBrush = new() { Color = Color.FromRgb((byte)RedSlider.Value, (byte)GreenSlider.Value, (byte)BlueSlider.Value) };
            ColorRectangle.Fill = colorBrush;
            ActiveShape.Stroke = colorBrush;

            if ((bool)FillCheckBox.IsChecked)
            {
                ActiveShape.Fill = colorBrush;
            }

            byte value = (byte)slider.Value;
            if (slider == RedSlider)
            {
                RedTextBox.Text = value.ToString();
            }
            else if (slider == GreenSlider)
            {
                GreenTextBox.Text = value.ToString();
            }
            else
            {
                BlueTextBox.Text = value.ToString();
            }
        }

        private void FillCheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            SolidColorBrush colorBrush = new() { Color=Color.FromRgb((byte)RedSlider.Value, (byte)GreenSlider.Value, (byte)BlueSlider.Value) };

            ActiveShape.Fill = (bool)FillCheckBox.IsChecked ? colorBrush : null;
        }

        private void DeleteAcvitveShapeButton_Click(object sender, RoutedEventArgs e)
        {
            ActiveShapeComboBox.Items.Clear();
            ShapesCanvas.Children.Remove(ActiveShape);
            CanvasShapes.RemoveShape(ActiveShape);

            for (int i = 0; i < CanvasShapes.HowManyShapes(); i++)
            {
                ActiveShape = CanvasShapes.GetShape(i);
                ActiveShapeComboBox.Items.Add(ActiveShape.Name);
            }

            ActiveShapeComboBox.SelectedIndex = 0;
            ActiveShapeComboBox.SelectedItem = 0;

            if (CanvasShapes.HowManyShapes() != 0)
            {
                ActiveShape = CanvasShapes.GetShape(0);
            }
        }

        private void PositionSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider s = (Slider)sender;
            if (s == XPositionSlider)
            {
                Canvas.SetLeft(ActiveShape, XPositionSlider.Value);
                XPositionTextBox.Text = (Math.Floor((XPositionSlider.Value / 0.5)) * 0.5 + ActiveShape.Width / 2).ToString();
                XPositionSlider.Maximum = 500 - ActiveShape.Width; //Limits range of movement in x axis to not allow Shape object to be outside the canvas
            }
            else
            {
                Canvas.SetTop(ActiveShape, YPositionSlider.Value);
                YPositionTextBox.Text = (Math.Floor((YPositionSlider.Value / 0.5)) * 0.5 + ActiveShape.Width / 2).ToString();
                YPositionSlider.Maximum = 500 - ActiveShape.Width; //Limits range of movement in y axis to not allow Shape object to be outside the canvas
            }
        }

        private void SizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ActiveShape != null)
            {
                SizeSlider.Value = (int)SizeSlider.Value;
                ActiveShape.Width = SizeSlider.Value;
                ActiveShape.Height = SizeSlider.Value;
                SizeTextBox.Text = SizeSlider.Value.ToString();
                PositionSlider_ValueChanged(XPositionSlider, e); //Triggers PositionSlider_ValueChanged event to update ranges of movement
                PositionSlider_ValueChanged(YPositionSlider, e);
            }
        }
    }
}
