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

public partial class Patient : Window
{
    public Patient()
    {
        InitializeComponent();
        string fullTable = "SELECT * FROM пациент;";
        ShowTable(fullTable);
        FillCmb();
    }
    
    private List<Patients> patient;
    private string connStr = "server=localhost;database=bolnitca;port=3306;User Id=admin;password=Qwertyu1!ZZZ";
    private MySqlConnection conn;
    
    public void ShowTable(string sql)
    {
        patient = new List<Patients>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentPatient = new Patients()
            {
                Код = reader.GetInt32("Код"),
                Фамилия  = reader.GetString("Фамилия"),
                Имя = reader.GetString("Имя"),
                Отчество = reader.GetString("Отчество"),
                Пол = reader.GetString("Пол"),
                Возраст = reader.GetInt32("Возраст"),
                Полис = reader.GetString("Полис")
            };
            patient.Add(currentPatient);
        }
        conn.Close();
        PatientGrid.ItemsSource = patient;
    }
    
    public void FillCmb()
    {
        patient = new List<Patients>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand("SELECT * FROM пациент", conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentPatient = new Patients()
            {
                Код = reader.GetInt32("Код"),
                Фамилия  = reader.GetString("Фамилия"),
                Имя = reader.GetString("Имя"),
                Отчество = reader.GetString("Отчество"),
                Пол = reader.GetString("Пол"),
                Возраст = reader.GetInt32("Возраст"),
                Полис = reader.GetString("Полис")
            };
            patient.Add(currentPatient);
        }
        conn.Close();
        var typecmb = this.Find<ComboBox>(name:"CmbNum");
        typecmb.ItemsSource = patient;
    }

    private void TwoSearch_OnClick(object? sender, RoutedEventArgs e)
    {
        string twotxt = "SELECT * FROM пациент  WHERE Фамилия LIKE '%" + SearchF.Text + "%' AND Имя LIKE '%" + Searchcd.Text + "%'";
        ShowTable(twotxt);
    }

    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        AvaloniaApplication1.Form2 form2 = new AvaloniaApplication1.Form2();
        Close();
        form2.Show();
    }

    private void Reset_OnClick(object? sender, RoutedEventArgs e)
    {
        string reset = "SELECT * FROM пациент;";
        ShowTable(reset);
        SearchF.Text = string.Empty;
        Searchcd.Text = string.Empty;
    }

    private void CmbNum_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var TypeCmB = (ComboBox)sender;
        var currentPatient = TypeCmB.SelectedItem as Patients;
        var fltrpatient = patient
            .Where(x => x.Код == currentPatient.Код)
            .ToList();
        PatientGrid.ItemsSource = fltrpatient;
    }

    
}