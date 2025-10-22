using System.Text.RegularExpressions;

public class GPU
{
    private string _modelName;
    private int _gpuClock;
    private GPUArchitecture _architecture;
    private int _memorySize;
    private DateTime _releaseDate;
    private short _memoryBusWidth;
    private decimal _launchPrice;

    public bool InBasket { get; private set; } = false;

    public string ModelName
    {
        get => _modelName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Назва моделі не може бути порожньою.");

            if (value.Length < 5 || value.Length > 40)
                throw new ArgumentException("Довжина назви моделі має бути від 5 до 40 символів.");

            if (!Regex.IsMatch(value, @"^[a-zA-Z0-9\s]+$"))
                throw new ArgumentException("Назва моделі може містити лише латинські літери та цифри.");

            _modelName = value;
        }
    }

    public int GpuClock
    {
        get => _gpuClock;
        set
        {
            if (value < 1000 || value > 4000)
                throw new ArgumentException("Частота GPU має бути в діапазоні 1000-4000 МГц.");
            _gpuClock = value;
        }
    }

    public GPUArchitecture Architecture
    {
        get => _architecture;
        set
        {
            if (!Enum.IsDefined(typeof(GPUArchitecture), value))
            {
                throw new ArgumentException("Архітектура не коректна!");
            }
            _architecture = value;
        }
    }

    public int MemorySize
    {
        get => _memorySize;
        set
        {
            if (value < 1 || value > 32)
                throw new ArgumentException("Об'єм пам'яті має бути в діапазоні 1–32 ГБ.");
            _memorySize = value;
        }
    }

    public DateTime ReleaseDate
    {
        get => _releaseDate;
        set
        {
            if (value > DateTime.Now)
                throw new ArgumentException("Дата випуску не може бути у майбутньому.");
            _releaseDate = value;
        }
    }

    public short MemoryBusWidth
    {
        get => _memoryBusWidth;
        set
        {
            if (value < 128 || value > 2048)
                throw new ArgumentException("Розрядність шини має бути в діапазоні 128-2048 біт.");
            _memoryBusWidth = value;
        }
    }

    public decimal LaunchPrice
    {
        get => _launchPrice;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Ціна на релізі має бути більше 0.");
            _launchPrice = value;
        }
    }

    public void PrintInfo()
    {
        Console.WriteLine($"Модель: {ModelName}");
        Console.WriteLine($"GPU Clock: {GpuClock} МГц");
        Console.WriteLine($"Архітектура: {Architecture}");
        Console.WriteLine($"Пам'ять: {MemorySize} ГБ");
        Console.WriteLine($"Розрядність шини: {MemoryBusWidth} біт");
        Console.WriteLine($"Дата випуску: {ReleaseDate.ToShortDateString()}");
        Console.WriteLine($"Ціна на релізі: {LaunchPrice} $");
        if (InBasket) { Console.WriteLine("Відеокарта знаходиться в кошику"); }
        else { Console.WriteLine("Відеокарта не знаходиться в кошику"); }
    }


    public int YearsSinceRelease()
    {
        return DateTime.Now.Year - ReleaseDate.Year;
    }

    public void AddToBasket()
    {
        if (!InBasket)
        {
            InBasket = true;
            Console.WriteLine("Відеокарта додана в кошик");
        }
        else
        {
            Console.WriteLine("Відеокарта вже знаходиться в кошику");
        }
    }

    public void DeleteFromBasket()
    {
        if (InBasket)
        {
            InBasket = false;
            Console.WriteLine("Відеокарта видалена з кошика");
        }
        else
        {
            Console.WriteLine("Відеокарти не було в кошику");
        }
    }
}