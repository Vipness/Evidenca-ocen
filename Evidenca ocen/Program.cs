using System.Text;
Console.OutputEncoding = Encoding.UTF8; // izpis nastavil na utf-8, da se vidijo tudi šumniki
Console.Title = "Evidenca ocen"; // spremenim ime konzole

Console.Write("Vnesi število predmetov, ki jih imaš v šoli: ");
int st_pred = int.Parse(Console.ReadLine());

// 2d tabela
string[,] ocene = new string[1, st_pred + 1];
ocene[0, 0] = "Ocene";

// imena predmetov
for (int i = 0; i < st_pred; i++){
    Console.Write("Vnesi ime {0} predmeta: ", i + 1);
    ocene[0, i + 1] = Console.ReadLine();
}

string[] ne = { "ne", "n", "no" };
string input = "ja";

do
{
    izberi(ocene);

    Console.Write("Ali želiš nadaljevati? ");
    input = Console.ReadLine().ToLower();
}
while (!(ne.Contains(input)));

static void izberi(string[,] ocene)
{
    Console.Clear();

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Izberi kaj želiš narediti:");
    Console.ResetColor();

    Console.WriteLine("1 - Dodaj oceno \n2 - Uredi oceno \n3 - Izbriši predmet \n4 - Izpiši ocene za določen predmet");

    int st_izbire = int.Parse(Console.ReadLine());

    switch (st_izbire)
    {
        /*
        case 1: ocene = dodaj(ocene); break; // probi kaj bi se zgodilo če se ne zapiše v ocene
        case 2: ocene = uredi(ocene); break;
        case 3: ocene = izbriši(ocene); break;
        case 4: izpiši_za_predmet(ocene); break;
        */
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Vnesena je bila napačna številka! Poskusi ponovno."); // izpiše z rdečo
            Console.ResetColor();
            izberi(ocene);
            break;
    }
    izpis(ocene);
}

static void izpis(string[,] t)
{
    for (int i = 0; i < t.GetLength(0); i++)
    {
        Console.WriteLine();
        for (int j = 0; j < t.GetLength(1); j++)
        {
            if(i == 0 && j > 0)
                Console.ForegroundColor = ConsoleColor.Yellow;

            else if(j == 0)
                Console.ForegroundColor = ConsoleColor.Blue;

            Console.Write($"|\t{t[i, j]}\t|");
            Console.ResetColor();
        }
    }
    Console.WriteLine();
}