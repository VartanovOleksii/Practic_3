System.Console.OutputEncoding = System.Text.Encoding.Unicode;

int maxCount = 0;
while (true)
{
    Console.Write("Введіть максимальну кількість відеокарт (N > 0): ");

    if (int.TryParse(Console.ReadLine(), out maxCount) && maxCount > 0)
        break;

    Console.WriteLine("Помилка: введіть додатне число!");
}

List<GPU> gpus = new List<GPU>(maxCount);

start_of_loop:
while (true)
{
    Console.WriteLine("\n==== МЕНЮ ====");
    Console.WriteLine("1 - Додати об'єкт");
    Console.WriteLine("2 - Переглянути додані об'єкти");
    Console.WriteLine("3 - Знайти об'єкт");
    Console.WriteLine("4 - Демонстрація поведінки");
    Console.WriteLine("5 - Видалити об'єкт");
    Console.WriteLine("0 - Вихід");
    Console.Write("Ваш вибір -> ");

    string choice = Console.ReadLine();
    Console.WriteLine();

    switch (choice)
    {
        //Додати об'єкт
        case "1":
            if (gpus.Count >= maxCount)
            {
                Console.WriteLine("Помилка: досягнута максимальна кількість об'єктів!");
                break;
            }

            try
            {
                GPU card = AddGPU();
                gpus.Add(card);
                Console.WriteLine("Відеокарта успішно додана!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            break;

        //Переглянути додані об'єкти
        case "2":
            if (gpus.Count == 0)
            {
                Console.WriteLine("Список порожній.");
            }
            else
            {
                Console.WriteLine("Список відеокарт:");
                foreach (var card in gpus)
                {
                    card.PrintInfo();
                    Console.WriteLine("\n");
                }
            }
            break;

        //Знайти об'єкт
        case "3":
            while (true)
            {
                Console.WriteLine("Виберіть параметр для пошуку:");
                Console.WriteLine("1 - Назва");
                Console.WriteLine("2 - Архітектура");
                Console.WriteLine("0 - Назад");
                Console.Write("Ваш вибір -> ");

                List<GPU> findresults = new List<GPU>();

                string findchoice = Console.ReadLine();
                Console.WriteLine();

                switch (findchoice)
                {
                    case "1":
                        Console.Write("Введіть назву моделі: ");
                        string searchName = Console.ReadLine();
                        findresults = gpus.FindAll(vc => vc.ModelName.Contains(searchName, StringComparison.OrdinalIgnoreCase));
                        break;

                    case "2":
                        Console.WriteLine("Доступні архітектури:");
                        foreach (var arch in Enum.GetValues(typeof(GPUArchitecture)))
                            Console.WriteLine($"- {arch}");

                        Console.Write("Введіть архітектуру: ");
                        string searchArch = Console.ReadLine();

                        if (Enum.TryParse(searchArch, true, out GPUArchitecture archValue))
                        {
                            findresults = gpus.FindAll(vc => vc.Architecture == archValue);
                        }
                        else
                        {
                            Console.WriteLine("Помилка: некоректна архітектура!");
                        }
                        break;

                    case "0":
                        goto start_of_loop;

                    default:
                        Console.WriteLine("Неправильний вибір, спробуйте знову.");
                        break;
                }

                Console.WriteLine("\n");

                if (findresults.Count > 0)
                {
                    foreach (GPU res in findresults)
                    {
                        res.PrintInfo();
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Об'єкт не знайдено");
                }
            }

        //Демонстрація поведінки
        case "4":
            if (gpus.Count == 0)
            {
                Console.WriteLine("Додайте об'єкти для демонстрації поведінки");
            }
            else
            {
                while (true)
                {
                    Console.WriteLine("\n==== МЕНЮ ====");
                    Console.WriteLine("1 - Переглянути характеристики");
                    Console.WriteLine("2 - Кількість років з релізу");
                    Console.WriteLine("3 - Додати до кошику");
                    Console.WriteLine("4 - Видалити з кошику");
                    Console.WriteLine("0 - Назад");
                    Console.Write("Ваш вибір -> ");

                    string subchoice = Console.ReadLine();
                    Console.WriteLine();

                    switch (subchoice)
                    {
                        case "1":
                            gpus[0].PrintInfo();
                            break;

                        case "2":
                            gpus[0].YearsSinceRelease();
                            break;

                        case "3":
                            gpus[0].AddToBasket();
                            break;

                        case "4":
                            gpus[0].DeleteFromBasket();
                            break;

                        case "0":
                            goto start_of_loop;
                            break;

                        default:
                            Console.WriteLine("Неправильний вибір, спробуйте знову.");
                            break;
                    }
                }
            }
            break;

        //Видалити об'єкт
        case "5":
            if (gpus.Count == 0)
            {
                Console.WriteLine("Список порожній.");
                break;
            }
            while (true)
            {
                Console.WriteLine("Виберіть параметр видалення:");
                Console.WriteLine("1 - Номер");
                Console.WriteLine("2 - Назва");
                Console.WriteLine("0 - Назад");
                Console.Write("Ваш вибір -> ");

                string deletechoice = Console.ReadLine();
                Console.WriteLine();

                switch (deletechoice)
                {
                    case "1":
                        int index;

                        while (true)
                        {
                            Console.Write("Введіть номер об'єкту для видалення: ");

                            if (int.TryParse(Console.ReadLine(), out index) && index >= 0 && index < gpus.Count())
                                break;

                            Console.WriteLine("Помилка: введіть корректний індекс!");
                        }

                        gpus.RemoveAt(index);
                        Console.WriteLine("Об'єкт видалено.");
                        break;

                    case "2":
                        Console.Write("Введіть назву моделі для видалення:");
                        string deleteName = Console.ReadLine();

                        var removed = gpus.RemoveAll(vc => vc.ModelName.Equals(deleteName, StringComparison.OrdinalIgnoreCase));
                        Console.WriteLine(removed > 0 ? "Об'єкт видалено." : "Об'єкт не знайдено.");
                        break;

                    case "0":
                        goto start_of_loop;

                    default:
                        Console.WriteLine("Неправильний вибір, спробуйте знову.");
                        break;
                }
            }

        //Вихід
        case "0":
            Console.WriteLine("Вихід із програми...");
            return;

        default:
            Console.WriteLine("Неправильний вибір, спробуйте знову.");
            break;
    }
}

static GPU AddGPU()
{
    GPU vc = null;


    Console.Write("Введіть назву моделі: ");
    string model = Console.ReadLine();


    Console.Write("Введіть частоту GPU (1000–4000): ");
    int frequency = int.Parse(Console.ReadLine());


    GPUArchitecture architecture;
    Console.WriteLine("Виберіть архітектуру: ");
    foreach (var arch in Enum.GetValues(typeof(GPUArchitecture)))
        Console.WriteLine($"- {arch}");
    Console.Write("Ваш вибір: ");
    if (!Enum.TryParse<GPUArchitecture>(Console.ReadLine(), true, out architecture))
    {
        throw new ArgumentException("Архітектура не коректна!");
    }


    Console.Write("Введіть обсяг пам'яті (1–32 ГБ): ");
    int memory = int.Parse(Console.ReadLine());


    DateTime release;
    Console.Write("Введіть дату випуску: ");
    if (!DateTime.TryParse(Console.ReadLine(), out release))
    {
        throw new ArgumentException("Дата не коректна!");
    }


    Console.Write("Введіть розрядність шини (128–2048 біт): ");
    short bus = short.Parse(Console.ReadLine());


    Console.Write("Введіть ціну на релізі (>0$): ");
    decimal price = decimal.Parse(Console.ReadLine());

    //проверка на правильность происходит только после ввода всех переменных
    try
    {
        vc = new GPU(model, frequency, architecture, memory, release, bus, price);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }

    return vc;
}