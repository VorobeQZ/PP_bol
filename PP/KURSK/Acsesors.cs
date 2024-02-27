using System;
namespace AvaloniaApplication1;

public partial class Doctors
{
    public int Код { get; set; }
    public string Фамилия { get; set; }
    public string Имя { get; set; }
    public string Отчество { get; set; }
    public string Пол { get; set; }
    public int Специализация { get; set; }
    public int Возраст { get; set; }
    public int Стаж { get; set; }
}
public partial class Patients
{
    public int Код { get; set; }
    public string Фамилия { get; set; }
    public string Имя { get; set; }
    public string Отчество { get; set; }
    public string Пол { get; set; }
    public int Возраст { get; set; }
    public string Полис { get; set; }
}
public class Analyze
{
    public int Код { get; set; }
    public string Анализ { get; set; }
}
public class Diagnosis
{
    public int Код { get; set; }
    public string Диагноз { get; set; }
}
public class Specalization
{
    public int Код { get; set; }
    public string Специализация { get; set; }
}
public class Analyzes
{
    public int Код { get; set; }
    public int Анализ { get; set; }
    public string Дата { get; set; }
    public int Пациент { get; set; }
    public string Результат { get; set; }
}
public class Cabinet
{
    public int Код { get; set; }
    public string Наименование { get; set; }
    public int Доктор { get; set; }
}
public class Healing
{
    public int Код { get; set; }
    public int Дагноз { get; set; }
    public int Осмотр { get; set; }
    public string Лечение { get; set; }
}
public class Osmotr
{
    public int Код { get; set; }
    public string Дата { get; set; }
    public int Пациент { get; set; }
    public int Кабинет { get; set; }
}