class Program
{
    static void Main()
    {
        Car myCar = new Car("Lada", "Vesta Sport", "В666ОР", "Black");
        Console.WriteLine($"Вы сели в машину: {myCar.Brand} {myCar.Model}, номер {myCar.Number}, цвет {myCar.Color}.");
        Thread.Sleep(2000);

        bool exit = false;

        try
        {
            while (!exit)
            {
                Console.WriteLine("Выберите действие с машиной:");
                Console.WriteLine("1 - Проверить уровень топлива");
                Console.WriteLine("2 - Завести машину");
                Console.WriteLine("3 - Поменять передачу");
                Console.WriteLine("4 - Ускориться");
                Console.WriteLine("5 - Притормозить");
                Console.WriteLine("6 - Заглушить машину");
                Console.WriteLine("7 - Заправить машину");
                Console.WriteLine("8 - Выход");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine($"Уровень топлива: {myCar.FuelLevel}%");
                        break;
                    case "2":
                        Console.WriteLine(myCar.StartEngine());
                        break;
                    case "3":
                        Console.WriteLine("Введите номер передачи:");
                        int gear = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(myCar.ChangeGear(gear));
                        break;
                    case "4":
                        Console.WriteLine(myCar.Accelerate());
                        break;
                    case "5":
                        Console.WriteLine(myCar.Brake());
                        break;
                    case "6":
                        Console.WriteLine(myCar.StopEngine());
                        break;
                    case "7":
                        Console.WriteLine(myCar.Refuel());
                        break;
                    case "8":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неизвестная команда");
                        break;
                }
            }
        }
        catch (CarExplodedException e)
        {
            Console.WriteLine(e.Message);
            Environment.Exit(0);
        }
    }
}

public class CarExplodedException : Exception
{
    public CarExplodedException(string message) : base(message)
    {
    }
}

public class Car
{
    public string Brand { get; private set; }
    public string Model { get; private set; }
    public string Number { get; private set; }
    public string Color { get; private set; }
    public bool EngineStarted { get; private set; }
    public int Speed { get; private set; }
    public int Gear { get; private set; }
    public int FuelLevel { get; private set; }

    public Car(string brand, string model, string number, string color)
    {
        Brand = brand;
        Model = model;
        Number = number;
        Color = color;
        EngineStarted = false;
        Speed = 0;
        Gear = 0;
        FuelLevel = 100;
    }

    public string StartEngine()
    {
        if ((Gear == 0 || Gear == 1) && FuelLevel > 0)
        {
            EngineStarted = true;
            return "Двигатель заведен";
        }
        else
        {
            EngineStarted = false;
            return "Двигатель не может быть заведен: неверная передача или нет топлива";
        }
    }

    public string StopEngine()
    {
        EngineStarted = false;
        return "Двигатель заглушен";
    }

    public string Accelerate()
    {
        if (EngineStarted && Gear > 0 && FuelLevel > 0)
        {
            if (Speed + 10 <= MaxSpeedForGear(Gear))
            {
                Speed += 10;
                FuelLevel -= 1;

                if (Speed >= 120)
                {
                    throw new CarExplodedException("Машина взорвалась!");
                }

                return $"Текущая скорость: {Speed} км/ч. Звук двигателя: Врум-врум!";
            }
            else
            {
                EngineStarted = false;
                return "Двигатель заглох: превышена максимальная скорость для текущей передачи";
            }
        }
        else
        {
            return "Ускорение невозможно: двигатель не заведен, нейтральная передача или нет топлива";
        }
    }

    public string Brake()
    {
        Speed -= 10;
        if (Speed < 0)
            Speed = 0;
        return $"Текущая скорость: {Speed} км/ч";
    }

    public string ChangeGear(int newGear)
    {
        if (SpeedMatchesGear(newGear))
        {
            Gear = newGear;
            return $"Переключение на {newGear} передачу";
        }
        else
        {
            EngineStarted = false;
            Speed = 0;
            return "Неверная передача для текущей скорости: двигатель заглох. Автомобиль остановлен";
        }
    }

    public string Refuel()
    {
        FuelLevel = 100;
        return "Машина заправлена";
    }

    private bool SpeedMatchesGear(int newGear)
    {
        switch (newGear)
        {
            case 1: return Speed >= 0 && Speed <= 20;
            case 2: return Speed >= 20 && Speed <= 40;
            case 3: return Speed >= 40 && Speed <= 60;
            case 4: return Speed >= 60 && Speed <= 80;
            case 5: return Speed >= 80 && Speed <= 120;
            case -1: return Speed == 0;
            case 0: return true;
            default: return false;
        }
    }

    private int MaxSpeedForGear(int gear)
    {
        switch (gear)
        {
            case 1: return 20;
            case 2: return 40;
            case 3: return 60;
            case 4: return 80;
            case 5: return 120;
            default: return 0;
        }
    }
}
