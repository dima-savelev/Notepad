using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Notepad
{
    /// <summary>
    /// Логика взаимодействия для DelLine.xaml
    /// </summary>
    public partial class DelLine : Window
    {
        public DelLine()
        {
            InitializeComponent();
        }
        private void DelLine_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(numberText.Text, out RemoveLine.Number) && RemoveLine.Number > 0)
            {
                Close();
            }
            else
            {
                MessageBox.Show("Введены неверные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                numberText.Clear();
            }
        }

        private void numberText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
