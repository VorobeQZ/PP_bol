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

public partial class Doctor : Window
{
    public Doctor()
    {
        InitializeComponent();
        string fullTable = "SELECT * FROM доктор;";//Запрос на отображение таблицы доктор
        ShowTable(fullTable);//Метод отображения таблиц в дата грид
        FillCmb();
    }
    
    private List<Doctors> doctor;//лист с акссесорами доступа для таблицы доктор
    private string connStr = "server=192.168.161.1;database=bolnitca;port=3306;User Id=admin;password=Qwertyu1!ZZZ";//Данные для подключения к MySql
    private MySqlConnection conn;
    
    public void ShowTable(string sql)//Метод отображения таблиц в дата грид
    {
        doctor = new List<Doctors>();
        conn = new MySqlConnection(connStr);//строка поключения
        conn.Open();//Открытие подключения
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentDoctor = new Doctors()//Заполнение данными для грида
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
        conn.Close();//Закрытие подключения
        DoctorGrid.ItemsSource = doctor;//Заполнение данными грида 
    }
    
    public void FillCmb()
    {
        doctor = new List<Doctors>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand("SELECT * FROM доктор", conn);
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
        var typecmb = this.Find<ComboBox>(name:"CmbNum");
        typecmb.ItemsSource = doctor;
    }

    private void TwoSearch_OnClick(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки для поиска по фамилии и имени
    {
        string twotxt = "SELECT * FROM доктор  WHERE Фамилия LIKE '%" + SearchF.Text + "%' AND Имя LIKE '%" + Searchcd.Text + "%'";//Запрос на поиск по фамилии и имени
        ShowTable(twotxt);//Отображение запроса
    }

    private void Back_OnClick(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки возврата на прошлую форму
    {
        AvaloniaApplication1.Form2 form2 = new AvaloniaApplication1.Form2();
        Close();
        form2.Show();
    }

    private void Reset_OnClick(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки обновление таблицы и текстбоксов
    {
        string reset = "SELECT * FROM доктор;";
        ShowTable(reset);
        SearchF.Text = string.Empty;
        Searchcd.Text = string.Empty;
    }

    private void CmbNum_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)//Метод активирующийся по нажатию кнопки для фильтрации по коду
    {
        var TypeCmB = (ComboBox)sender;
        var currentDoctor = TypeCmB.SelectedItem as Doctors;
        var fltrdoctor = doctor
            .Where(x => x.Код == currentDoctor.Код)
            .ToList();
        DoctorGrid.ItemsSource = fltrdoctor;
    }

    private void DeleteData(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки для удаления выбранной строки
    {
        try
        {
            Doctors currentDoctor = DoctorGrid.SelectedItem as Doctors;
            if (currentDoctor == null)
            {
                return;
            }
            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM доктор WHERE Код = " + currentDoctor.Код;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            doctor.Remove(currentDoctor);
            ShowTable("SELECT * FROM доктор;");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void AddData(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки для добавления новых данных
    {
        Doctors newDoctor = new Doctors();
        AddaUpd.AddUpd addWindow = new AddaUpd.AddUpd(newDoctor, doctor);
        addWindow.Show();
        this.Close();
    }

    private void EditData(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки для редактирования данных
    {
        Doctors currentDoctor = DoctorGrid.SelectedItem as Doctors;
        if (currentDoctor == null)
        {
            return;
        }
        AddaUpd.AddUpd editWindow = new AddaUpd.AddUpd(currentDoctor, doctor);
        editWindow.Show();
        this.Close();
    }
}