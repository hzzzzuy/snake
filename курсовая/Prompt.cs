using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace курсовая
{
    public static class Prompt
    {
        // Определение статического метода ShowDialog, принимающего текст и заголовок окна
        public static string ShowDialog(string text, string caption)
        {
            Window prompt = new Window()
            {
                Width = 300,
                Height = 150,
                Title = caption
            };
            Label textLabel = new Label() { Content = text, Margin = new Thickness(20, 20, 20, 5) };
            TextBox textBox = new TextBox() { Width = 240, Margin = new Thickness(20, 5, 20, 5) };
            Button confirmation = new Button() { Content = "Ok", Width = 100, Margin = new Thickness(200, 5, 20, 0) };

            // Обработка события клика по кнопке: закрытие окна
            confirmation.Click += (sender, e) => { prompt.Close(); };

            prompt.Content = new StackPanel
            {
                Children =
                {
                    textLabel,
                    textBox,
                    confirmation
                }
            };
            prompt.ShowDialog();

            return textBox.Text;
        }
    }
}
