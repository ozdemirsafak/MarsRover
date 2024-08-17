#region Case
// Mars'taki Gezgin
//Robotik gezginin pozisyonu ve konumu, x-y koordinatları ve pusulada yer alan yön harfleri ile belirtilmektedir (N, W, S, E). Mars yüzeyindeki bu yüzey, x ve y koordinatlarına göre bölünmüştür. Robotik gezgin için 0,0, N pozisyonu, diktörtgen yüzeyin sol alt köşesinde ve yönünün kuzeye dönük olduğunu göstermektedir.
//NASA robotik gezgini kontrol etmek için bir takım harf katarları göndermektedir. Gönderilebilen harfler ise ‘L’, ‘R’ ve ‘M’ dir. ‘L’ ve ‘R’ komutu, robotik gezgini ve kamerasını hiç hareket ettirmeden olduğu yerde 90 derece sola ya da sağa döndürmektedir. ‘M’ ise robotik gezgini bulunduğu yönde bir adım ilerletmektedir.
//Robotik gezgin, (x, y) koordinatından (x, y+1) koordinatına konumlandığında kuzey (N) yönünde 1 adım gittiği kabul edilmektedir.
//Input:
//Ekrandaki ilk input, Mars’taki yüzeyin sağ üst köşesinin koordinatı olacaktır. Sol alt köşe koordinatı (0,0) olarak kabul edilecektir.
//Ekrandan alınan ikinci input, robotik gezginin ilk bulunduğu konum olacaktır. Bir boşluk ile ayrılacaktır.
//Ekrandan alınan son input ise NASA’dan gönderilen harf katarı olacaktır. Bu katarın içinde sadece L, R ve M harfleri olabilir.
//Yüzeyde iki adet robotik gezgin bulunmaktadır. Bir robot hareketini bitirmeden diğer robot hareketine başlayamaz.
//NASA’dan gönderilen harf katarları sonucu robotik gezginin son konumu, diktörtgen yüzeyin bulunduğu x veya y koordinatından en az birini aşarsa, aşan koordinat dikdörtgenin maksimum konumunda kalacaktır. (ÖRN: diktörtgenin koordinatları 0,0 ve 5,5 ise ve yeni konum 7,4 çıktı ise robotik gezginin son konumu 5,4 olacaktır.)

//Output:
//Her iki robotun da son konumu ve yönü gösterilmelidir.

//Test Input:
//5 5

//1 2 N
//LMLMLMLMM

//3 3 E
//MMRMMRMRRM

//Beklenen Output:
//1 3 N
//5 1 E

#endregion

/// <summary>
/// Bu sınıf,Mars Rover'ın bir ızgara üzerinde verilen komutlara göre hareket etmesini ve yön değiştirmesini sağlar.
/// </summary>
class MarsRover
{
    //X ve Y koordinatları için proplarımızı tanımlıyoruz.
    public int X { get; set; }
    public int Y { get; set; }
    public char Direction { get; set; }
    private readonly int _maxX;
    private readonly int _maxY;

    /// <summary>
    /// Mars Rover nesnesini belirtilen başlangıç konumu (X, Y), yön (Direction) ve
    /// maksimum sınarları alır.
    /// </summary>
    /// <param name="x">Rover'ın başlangıç X koordinatı.</param>
    /// <param name="y">Rover'ın başlangıç Y koordinatı.</param>
    /// <param name="direction">Rover'ın başlangıç yönü ('N', 'E', 'S', 'W').</param>
    /// <param name="maxX">Rover'ın hareket edebileceği maksimum X sınırı.</param>
    /// <param name="maxY">Rover'ın hareket edebileceği maksimum Y sınırı.</param>
    public MarsRover(int x, int y, char direction, int maxX, int maxY)
    {
        X = x;
        Y = y;
        Direction = direction;
        _maxX = maxX;
        _maxY = maxY;
    }

    public void ExecuteCommands(string commands)
    {
        // Harf katarındaki harfleri alıyoruz tek tek.
        foreach (var command in commands)
        {
            // L veya R ise dönüş için Rotate methoduna yolluyoruz command ile birlikte.
            if (command == 'L' || command == 'R')
                Rotate(command);
            // M ise yani hareket et ise Move methodunu çalıştırıyoruz.
            else if (command == 'M')
                Move();
        }
    }

    private void Rotate(char rotation)
    {
        string directions = "NESW";

        //Mevcut yönün indeksini buluyoruz.
        int index = directions.IndexOf(Direction);

        // Eğer Direction = N ise, yani indeks 0 ise ve sola dönmek istiyorsak, batıya W yönelmemiz gerekiyor. 0 + 3 = 3, bu da "NESW" stringinde W harfinin indeksidir.
        // Eğer Direction = E(indeks 1) ise, sola dönmek bizi kuzeye N yönlendirir. 1 + 3 = 4 ve 4 % 4 = 0, bu da N olur.
        if (rotation == 'L')
            index = (index + 3) % 4;

        // Eğer Direction = N ise, sağa dönmek bizi doğuya (E) yönlendirir. 0 + 1 = 1, bu da E olur.
        // Eğer Direction = W (indeks 3) ise, sağa dönmek bizi kuzeye N yönlendirir. 3 + 1 = 4 ve 4 % 4 = 0, bu da N olur.
        else if (rotation == 'R')
            index = (index + 1) % 4;

        // Yeni indekse karşılık gelen yön, Direction özelliğine atanır.
        Direction = directions[index];
    }

    private void Move()
    {
        switch (Direction)
        {
            //Kuzeye dönük ise Y koordinatını 1 arttır.Eğer Y+1 _maxY 'yi geçiyorsa Y değeri _maxY olur
            case 'N':
                Y = Math.Min(Y + 1, _maxY);
                break;
            //Güneye dönük ise Y koordinatı 1 azaltılır..Eğer Y-1 0'ın altına iniyorsa Y değeri 0 olur
            case 'S':
                Y = Math.Max(Y - 1, 0);
                break;

            // Doğuya dönük ise X koordinatını 1 arttır.Eğer X+1 _maxX 'yi geçiyorsa X değeri _maxX olur
            case 'E':
                X = Math.Min(X + 1, _maxX);
                break;

            //Batıya dönük ise X koordinatı 1 azaltılır..Eğer X-1 0'ın altına iniyorsa X değeri 0 olur
            case 'W':
                X = Math.Max(X - 1, 0);
                break;
        }
    }

    public void PrintPosition()
    {
        // X, Y ve Direction yani konumu yazdırıyoruz.
        Console.WriteLine($"{X} {Y} {Direction}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Lütfen ızgara sayısını giriniz.Örneğin 5 5 şeklinde");

        // İlk önce kullanıcıdan sınır değerini alıyoruz yani ızgara kaça kaç olmalı? Split ile boşluk karakterlerine göre ayırır ve diziye dönüştürür.
        var boundary = Console.ReadLine().Split();

        // Ekranda girdiğimiz 0.indeks X koordinatı ikincisi ise Y kordinatını belirliyor
        int maxX = int.Parse(boundary[0]);
        int maxY = int.Parse(boundary[1]);

        for (int i = 0; i < 2; i++)
        {
            Console.WriteLine("Lütfen yönü belirtin 1 2 N şeklinde");
            // Başlangıç pozisyonunu alıyoruz. Örneğin 1 2 N
            var initialPosition = Console.ReadLine().Split();
            //İlk girdiğimiz X koordinatı başlangıç değeri
            int startX = int.Parse(initialPosition[0]);
            // Y başlangıç değeri
            int startY = int.Parse(initialPosition[1]);
            // Yön değeri
            char direction = char.Parse(initialPosition[2]);

            // MarsRover nesnesi oluşturuyoruz,oluştururken de değerleri constructor'a iletiyoruz.
            var rover = new MarsRover(startX, startY, direction, maxX, maxY);

            Console.WriteLine("Lütfen harf katarını giriniz LMLMLM şeklinde");
            //Harf katarını alıyoruz.
            string commands = Console.ReadLine();

            //commands'i ExecuteCommands metoduna yolluyoruz.
            rover.ExecuteCommands(commands);

            //Rover'ın son konumunu ekrana yazdırmak için methoda yolluyoruz.
            rover.PrintPosition();
        }
    }
}


