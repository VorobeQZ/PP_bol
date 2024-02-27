using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
namespace AvaloniaApplication1;

public partial class Form2 : Window
{
    public Form2()
    {
        InitializeComponent();
    }
    public void Doctor(object? sender, RoutedEventArgs e)
    {
        var doctor = new AvaloniaApplication1.Doctor();
        Close();
        doctor.Show(); 
    }
        
    public void Patient(object? sender, RoutedEventArgs e)
    {
        var patient = new AvaloniaApplication1.Patient();
        Close();
        patient.Show(); 
    }
    public void Procedure(object? sender, RoutedEventArgs e)
    {
        var procedure = new AvaloniaApplication1.Procedures();
        Close();
        procedure.Show(); 
    }

    private void Exit_OnClick(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }
}