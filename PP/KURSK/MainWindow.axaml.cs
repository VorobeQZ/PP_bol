using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mime;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace AvaloniaApplication1;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    private MySqlConnection conn;
    private string connStr = "server=localhost;database=bolnitca;port=3306;User Id=admin;password=Qwertyu1!ZZZ";

    public void Authorization(object? sender, RoutedEventArgs e)
    {
        try
        {
            conn = new MySqlConnection(connStr);
            conn.Open();
            string check ="SELECT * FROM  пользователи WHERE  Логин = '" + Login.Text + "' AND Пароль ='" + Password.Text + "' LIMIT 1"; 
            MySqlCommand cmd = new MySqlCommand(check, conn);
            cmd.ExecuteNonQuery();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1"||dt.Rows[0][0].ToString() == "2" )
            {
                var form2 = new AvaloniaApplication1.Form2();
                this.Hide();
                form2.Show(); 
            }
            else
            {
                Console.Write("Неверный логин или пароль");
            }
        }
        catch (Exception ex)
        {
            LogErr.IsVisible = true;
        }
        conn.Close();
    }
    
    public void Exit_PR(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }
}