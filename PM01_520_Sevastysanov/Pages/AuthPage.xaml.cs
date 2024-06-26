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

namespace PM01_520_Sevastysanov.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void TextBl_Changed(object sender, RoutedEventArgs e)
        {
            if (TxtBox_Log.Text.Length > 0)
            {
                TxtBl_Log.Visibility = Visibility.Collapsed;
            }
            else
            {
                TxtBl_Log.Visibility = Visibility.Visible;
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PaswBox.SecurePassword.Length > 0)
            {
                TxtBl_Pasw.Visibility = Visibility.Collapsed;
            }
            else
            {
                TxtBl_Pasw.Visibility = Visibility.Visible;
            }
        }

        private void Btn_entrance(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtBox_Log.Text) || string.IsNullOrEmpty(PaswBox.Password))
            {
                MessageBox.Show("Введите логин и пароль", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var db = new Entities6())
            {
                var user = db.Parcipiants
                    .AsNoTracking()
                    .FirstOrDefault(u => u.Phone == TxtBox_Log.Text);

                if (user == null)
                {
                    MessageBox.Show("Такого пользователя не существует!", "Не существующий пользователь", MessageBoxButton.OK, MessageBoxImage.Error);
                    TxtBox_Log.Clear();
                    PaswBox.Clear();
                }
                else if (PaswBox.Password.Length >= 6)
                {
                    if (user.Password != PaswBox.Password)
                    {
                        MessageBox.Show($"Пароль указан не верно {user.Password}", "Ошибка пароля", MessageBoxButton.OK, MessageBoxImage.Warning);
                        PaswBox.Clear();
                    }
                    else
                    {
                        
                        // Получаем доступ к главному окну и изменяем его заголовок
                        if (Application.Current.MainWindow is MainWindow mainWindow)
                        {
                            mainWindow.Title = "Участник";
                        }
                        NavigationService?.Navigate(new Uchastn());
                        
                           
                        
                        TxtBox_Log.Clear();
                        PaswBox.Clear();
                    }
                }
                else if (PaswBox.Password.Length < 6)
                {
                    MessageBox.Show("Вы указали маленький пароль", "Маленький пароль", MessageBoxButton.OK, MessageBoxImage.Warning);
                    PaswBox.Clear();
                }





            }
        }
    }
}
