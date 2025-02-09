using System;
using System.Collections.Generic;
using EmployeeLibrary;

class Program
{
    static void Main()
    {
        string[] Menu = { "New", "Display", "Search", "Sort (By Name)","Sort (By Salary)", "Exit" };
        int highlight = 0;
        bool looping = true;

        List<Employee> employees = new List<Employee>(); 

        do
        {
            Console.Clear();
            for (int i = 0; i < Menu.Length; i++)
            {
                Console.SetCursorPosition(60, 30 / (Menu.Length + 1) * (i + 1));
                if (i == highlight)
                {
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine(Menu[i]);
            }

            Console.ResetColor();

            ConsoleKeyInfo ckey = Console.ReadKey();
            switch (ckey.Key)
            {
                case ConsoleKey.UpArrow:
                    highlight = (highlight == 0) ? Menu.Length - 1 : highlight - 1;
                    break;

                case ConsoleKey.DownArrow:
                    highlight = (highlight == Menu.Length - 1) ? 0 : highlight + 1;
                    break;

                case ConsoleKey.Enter:
                    switch (highlight)
                    {
                        case 0:
                            AddEmployee(employees);
                            break;
                        case 1:
                            DisplayEmployees(employees);
                            break;
                        case 2:
                            SearchEmployee(employees);
                            break;
                        case 3:
                            SortEmployees(employees, new SortByName());
                            break;
                        
                        case 4:
                            SortEmployees(employees, new SortBySalary());
                            break;
                        case 5:
                            looping = false;
                            break;
                    }
                    break;
                case ConsoleKey.Escape:
                    looping = false;
                    break;
            }

        } while (looping);
    }

    static void AddEmployee(List<Employee> employees)
    {
        Console.Clear();
        Console.WriteLine("Enter Employee Details:");

        Console.Write("Enter Employee Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Employee Salary: ");
        double salary = double.Parse(Console.ReadLine());

        Console.Write("Enter Employee Age: ");
        int age = int.Parse(Console.ReadLine());

        Console.Write("Enter Gender (M for Male, F for Female): ");
        char genderInput = Console.ReadKey().KeyChar;
        Console.WriteLine();

        Gender gender = (genderInput == 'M' || genderInput == 'm') ? Gender.Male : Gender.Female;

        Employee emp = new Employee(name, salary, age, gender);
        employees.Add(emp);

        Console.WriteLine("\nEmployee added successfully!");
        Console.WriteLine("Press any key to return to the menu...");
        Console.ReadKey();
    }

    static void DisplayEmployees(List<Employee> employees)
    {
        Console.Clear();
        if (employees.Count == 0)
        {
            Console.WriteLine("No employees to display.");
        }
        else
        {
            Console.WriteLine("Employee List:");
            Console.WriteLine("----------------------------");
            foreach (Employee emp in employees)
            {
                emp.DisplayData();
            }
        }
        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey();
    }

    static void SearchEmployee(List<Employee> employees)
    {
        Console.Clear();
        if (employees.Count == 0)
        {
            Console.WriteLine("No employees to search.");
            Console.ReadKey();
            return;
        }

        Console.Write("Enter Employee ID or Name to search: ");
        string input = Console.ReadLine();

        bool found = false;
        foreach (Employee emp in employees)
        {
            if (emp.ID.ToString() == input || emp.Name.Equals(input, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("\nEmployee Found:");
                emp.DisplayData();
                found = true;
                break;
            }
        }

        if (!found)
        {
            Console.WriteLine("\nEmployee not found.");
        }

        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey();
    }

    static void SortEmployees(List<Employee> employees, IComparer<Employee> comparer)
    {
        if (employees.Count == 0)
        {
            Console.WriteLine("No employees to sort.");
            Console.ReadKey();
            return;
        }

        
        for (int i = 0; i < employees.Count - 1; i++)
        {
            for (int j = 0; j < employees.Count - i - 1; j++)
            {
                if (comparer.Compare(employees[j], employees[j + 1]) > 0)
                {
                   

                    Employee temp = employees[j];
                    employees[j] = employees[j + 1];
                    employees[j + 1] = temp;
                }
            }
        }

        Console.WriteLine("Employees sorted.");
        DisplayEmployees(employees);
    }
}
