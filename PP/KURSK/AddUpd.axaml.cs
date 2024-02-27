using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using AvaloniaApplication1;
using System;

namespace AddaUpd;

public partial class AddUpd : Window
{
    private List<Doctors> doctors;
    private Doctors CurrentDoctor;
    
    public AddUpd(Doctors currentDoctor, List<Doctors> doctor)
    {
        InitializeComponent();
        CurrentDoctor = currentDoctor;
        this.DataContext = CurrentDoctor;
        doctors = doctor;
    }
    
    private MySqlConnection conn;
    private string connStr = "server=localhost;database=bolnitca;port=3306;User Id=admin;password=Qwertyu1!ZZZ";
    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var doctorz = doctors.FirstOrDefault(x => x.Код == CurrentDoctor.Код);
        if (doctorz == null)
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO доктор VALUES (" + Convert.ToInt32(Код.Text)+ ", '" + Фамилия.Text + "', '" + Имя.Text + "', '" + Отчество.Text + "', '" + Пол.Text + "', " + Convert.ToInt32(Специализация.Text ) + ", " + Convert.ToInt32(Возраст.Text )+", " + Convert.ToInt32(Стаж.Text )+");";
                MySqlCommand cmd = new MySqlCommand(add, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error" + exception);
            }
        }
        else
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string upd = "UPDATE Доктор SET Фамилия = '" + Фамилия.Text + "', Имя = '" + Имя.Text + "', Отчество = '" + Отчество.Text + "', Пол = '" + Пол.Text + "', Специализация = "+ Convert.ToInt32(Специализация.Text) + ", Возраст = "+ Convert.ToInt32(Возраст.Text)+ ", Стаж = "+ Convert.ToInt32(Стаж.Text) + " WHERE Код = " + Convert.ToInt32(Код.Text) + ";";
                MySqlCommand cmd = new MySqlCommand(upd, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.Write("Error" + exception);
            }
        }
    }

    private void GoBack(object? sender, RoutedEventArgs e)
    {
        var form = new AvaloniaApplication1.Doctor();
        Close();
        form.Show();  
    }
}