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
using System.Data;

namespace AvaloniaApplication1;

public partial class Procedures : Window
{
    public Procedures()
    {
        InitializeComponent();
        string анализ = "CALL bolnitca.Анализ();";
        string стаж = "CALL bolnitca.Стаж();";
        string стаживозраст = "CALL bolnitca.`Стаж и возраст`();";
        Analyze(анализ);
        Stazh(стаж);
        StazhAge(стаживозраст);
        
    }
    private List<Doctors> doctor;
    private List<Analyze> analyze;
    private string connStr = "server=192.168.161.1;database=bolnitca;port=3306;User Id=admin;password=Qwertyu1!ZZZ";
    private MySqlConnection conn;
    

    public void Analyze(string sql)
    {
        analyze = new List<Analyze>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentAnalyze = new Analyze()
            {
                Код = reader.GetInt32("Код"),
                Анализ  = reader.GetString("Анализ"),
            };
            analyze.Add(currentAnalyze);
        }
        conn.Close();
        AnalyzeGrid.ItemsSource = analyze;
    }
    public void Stazh(string sql)
    {
        doctor = new List<Doctors>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentDoctor = new Doctors()
            {
                Код = reader.GetInt32("Код"),
                Фамилия  = reader.GetString("Фамилия"),
                Имя = reader.GetString("Имя"),
                Отчество = reader.GetString("Отчество"),
                Пол = reader.GetString("Пол"),
                Специализация = reader.GetInt32("Специализация"),
                Возраст = reader.GetInt32("Возраст"),
                Стаж = reader.GetInt32("Стаж")
            };
            doctor.Add(currentDoctor);
        }
        conn.Close();
        StazhGrid.ItemsSource = doctor;
    }
    public void StazhAge(string sql)
    {
        doctor = new List<Doctors>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentDoctor = new Doctors()
            {
                Код = reader.GetInt32("Код"),
                Фамилия  = reader.GetString("Фамилия"),
                Имя = reader.GetString("Имя"),
                Отчество = reader.GetString("Отчество"),
                Пол = reader.GetString("Пол"),
                Специализация = reader.GetInt32("Специализация"),
                Возраст = reader.GetInt32("Возраст"),
                Стаж = reader.GetInt32("Стаж")
            };
            doctor.Add(currentDoctor);
        }
        conn.Close();
        StazhAgeGrid.ItemsSource = doctor;
    }
    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        AvaloniaApplication1.Form2 form2 = new AvaloniaApplication1.Form2();
        Close();
        form2.Show();
    }
}