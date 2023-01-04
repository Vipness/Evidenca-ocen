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

izpis(ocene);

string[] ne = { "ne", "n", "no" };
string input = "ja";

do
{
    ocene = izberi(ocene);
    izpis(ocene);
    Console.WriteLine("\nAli želiš nadaljevati? (za zaustavitev vnesi 'ne', 'n' ali 'no') ");
    input = Console.ReadLine().ToLower();
}
while (!(ne.Contains(input)));

static string[,] izberi(string[,] ocene)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\nIzberi kaj želiš narediti:");
    Console.ResetColor();

    Console.WriteLine("1 - Dodaj oceno \n2 - Uredi oceno \n3 - Izbriši predmet \n4 - Izpiši ocene za določen predmet");

    int st_izbire = int.Parse(Console.ReadLine());

    switch (st_izbire)
    {
        case 1: ocene = dodaj(ocene); break;
        /*
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

    return ocene;
}

static string[,] dodaj(string[,] ocene)
{
    Console.Write("\nPri katerem predmetu si želiš dodati oceno? ");
    string predmet = Console.ReadLine();

    int stolp_predmeta = najdi_predmet(ocene, predmet);
    if (stolp_predmeta == -1)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Predmet ni bil najden! Poskusi znova.");
        Console.ResetColor();
        dodaj(ocene);
    }

    int vrsta = 0;
    while (!(String.IsNullOrWhiteSpace(ocene[vrsta, stolp_predmeta])))
    {
        vrsta++;
        if (vrsta >= ocene.GetLength(0)) // če je out of range
            ocene = povecaj(ocene);
    }

    Console.Write("Vnesi pridobljeno oceno: ");
    string ocena = Console.ReadLine();

    while (int.Parse(ocena) < 0 || int.Parse(ocena) > 5)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("Vnešena ocena je neveljavna! Prosim vnesi ponovno: ");
        Console.ResetColor();
        ocena = Console.ReadLine();
    }

    ocene[vrsta, stolp_predmeta] = ocena;

    for (int i = 1; i < ocene.GetLength(0); i++)
        ocene[i, 0] = Convert.ToString(i);

    return ocene;
}

static string[,] povecaj(string[,] ocene)
{
    string[,] nova = new string[ocene.GetLength(0)+1, ocene.GetLength(1)];

    for(int i = 0; i < ocene.GetLength(0); i++)
        for(int j = 0; j < ocene.GetLength(1); j++)
            nova[i, j] = ocene[i, j];

    return nova;
}

static int najdi_predmet(string[,] ocene, string predmet)
{
    for(int j = 1; j < ocene.GetLength(1); j++)
    {
        if (ocene[0, j] == predmet)
            return j;
    }
    return -1;
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