class Car
{
    public string Model { get; private set; }
    private bool IsEngineRunning = false;

    public Car(string model)
    {
        Model = model;
    }

    public string StartCar()
    {
        if (IsEngineRunning)
            return $"Машина {Model} уже заведена";
        IsEngineRunning = true;
        return $"Машина {Model} завелась";
    }

    public string StopCar()
    {
        if (!IsEngineRunning)
            return $"Машина {Model} уже остановлена";
        IsEngineRunning = false;
        return $"Машина {Model} остановилась";
    }

    public string AccelerateCar()
    {
        if (!IsEngineRunning)
            return $"Нельзя газовать, пока машина {Model} не заведена";
        return $"Машина {Model} газанула";
    }

    public string BrakeCar()
    {
        if (!IsEngineRunning)
            return $"Нельзя тормозить, пока машина {Model} не заведена";
        return $"Машина {Model} притормозила";
    }

    public string ShiftGearCar()
    {
        if (!IsEngineRunning)
            return $"Нельзя переключать передачи, пока машина {Model} не заведена";
        return $"Машина {Model} переключила передачу";
    }

    public string Honk()
    {
        return $"Машина {Model} подала гудок";
    }
}

class Program
{
    static void Main()
    {
        List<Car> cars = new List<Car>
        {
            new Car("Toyota Camry"),
            new Car("Ford Mustang"),
            new Car("Volkswagen Golf"),
            new Car("Tesla Model S"),
            new Car("Land Rover Defender")
        };

        Car selectedCar = ChooseCar(cars);

        while (true)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Запустить машину");
            Console.WriteLine("2. Заглушить машину");
            Console.WriteLine("3. Газануть");
            Console.WriteLine("4. Притормозить");
            Console.WriteLine("5. Переключить передачу");
            Console.WriteLine("6. Подать гудок");
            Console.WriteLine("7. Выбрать другую машину");
            Console.WriteLine("8. Выход");

            if (int.TryParse(Console.ReadLine(), out int action))
            {
                switch (action)
                {
                    case 1:
                        Console.WriteLine(selectedCar.StartCar());
                        break;
                    case 2:
                        Console.WriteLine(selectedCar.StopCar());
                        break;
                    case 3:
                        Console.WriteLine(selectedCar.AccelerateCar());
                        break;
                    case 4:
                        Console.WriteLine(selectedCar.BrakeCar());
                        break;
                    case 5:
                        Console.WriteLine(selectedCar.ShiftGearCar());
                        break;
                    case 6:
                        Console.WriteLine(selectedCar.Honk());
                        break;
                    case 7:
                        selectedCar = ChooseCar(cars);
                        break;
                    case 8:
                        return; // Выход из программы
                    default:
                        Console.WriteLine("Неизвестное действие");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите число от 1 до 8.");
            }
        }
    }

    static Car ChooseCar(List<Car> cars)
    {
        while (true)
        {
            Console.WriteLine("Выберите автомобиль:");
            for (int i = 0; i < cars.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {cars[i].Model}");
            }

            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= cars.Count)
            {
                return cars[choice - 1];
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите число от 1 до " + cars.Count);
            }
        }
    }
}
