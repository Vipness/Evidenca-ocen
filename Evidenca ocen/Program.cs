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

string input = "ja";

do
{
    ocene = izberi(ocene);
    izpis(ocene);
    Console.Write("\nAli želiš nadaljevati? ('n' za zaustavitev) ");
    input = Console.ReadLine().ToLower();
}
while (input != "n");

static string[,] izberi(string[,] ocene)
{
    Console.WriteLine("\nIzberi kaj želiš narediti:");
    Console.WriteLine("0 - Dodaj predmet \n1 - Dodaj oceno \n2 - Uredi oceno \n3 - Izbriši predmet \n4 - Izpiši ocene za določen predmet");

    int st_izbire = int.Parse(Console.ReadLine());

    switch (st_izbire)
    {
        case 0: ocene = dodaj_predmet(ocene); break;
        case 1: ocene = dodaj_oceno(ocene); break;
        case 2: ocene = uredi(ocene); break;
        case 3: ocene = izbrisi(ocene); break;
        case 4: izpisi_predmet(ocene); break;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nVnešena je bila napačna številka! Poskusi znova."); // izpiše z rdečo
            Console.ResetColor();
            ocene = izberi(ocene);
            break;
    }

    return ocene;
}

static string[,] dodaj_predmet(string[,] ocene)
{
    if(ocene.GetLength(1) == 1)
    {
        Console.Write("Vnesi ime novega predmeta: ");
        string predmet = Console.ReadLine();

        string[,] nova = new string[ocene.GetLength(0), ocene.GetLength(1) + 1];

        for(int i = 0; i < ocene.GetLength(0); i++)
            for (int j = 0; j < ocene.GetLength(1); j++)
                nova[i, j] = ocene[i, j];

        nova[0, 1] = predmet;

        return nova;
    }

    else
    {
        Console.Write("\nPrivzeti stolpec za dodajanje predmeta je zadnji. Ali bi to rad spremenil? ('y' za spremembo) ");
        string odgovor = Console.ReadLine();
        int stolp_predmeta = ocene.GetLength(1); // default stolpec je zadnji in ker se vse pomakne za 1 naprej ne rabim dat -1

        if (odgovor.ToLower() == "y") // če rečemo da, ga lahko spremenimo
        {
            izpis(ocene);
            Console.Write("\nNa kateri stolpec si želiš uvrstiti nov predmet? ");
            stolp_predmeta = int.Parse(Console.ReadLine()) - 1; // -1 ker se indexi zacnejo z 0
            while(stolp_predmeta < 0 || stolp_predmeta > ocene.GetLength(1))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nVnešena številka stolpca je neveljavna! Poskusi ponovno: ");
                Console.ResetColor();
                stolp_predmeta = int.Parse(Console.ReadLine()) - 1;
            }
        }

        Console.Write("Vnesi ime novega predmeta: ");
        string predmet = Console.ReadLine();
        string[,] nova = new string[ocene.GetLength(0), ocene.GetLength(1) + 1];

        for (int i = 0; i < ocene.GetLength(0); i++)
            for (int j = 0; j < ocene.GetLength(1); j++)
            {
                if (j >= stolp_predmeta)
                    nova[i, j + 1] = ocene[i, j];

                else
                    nova[i, j] = ocene[i, j];
            }

        nova[0, stolp_predmeta] = predmet;
        
        return nova;
    }
}

static string[,] dodaj_oceno(string[,] ocene)
{
    if(ocene.GetLength(1) == 1)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nV tabeli ni predmetov, da bi jim lahko dodal oceno!");
        Console.ResetColor();
        return izberi(ocene);
    }

    izpis(ocene);
    int stolp_predmeta = 1;

    if(ocene.GetLength(1) > 2)
    {
        Console.Write("\nPri katerem predmetu si želiš dodati oceno? ");
        string predmet = Console.ReadLine();

        stolp_predmeta = najdi_predmet(ocene, predmet);
        if (stolp_predmeta == -1)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Predmet ni bil najden! Poskusi znova.");
            Console.ResetColor();
            return dodaj_oceno(ocene);
        }
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
        Console.Write("Vnešena ocena je neveljavna! Vnesi ponovno: ");
        Console.ResetColor();
        ocena = Console.ReadLine();
    }

    ocene[vrsta, stolp_predmeta] = ocena;

    for (int i = 1; i < ocene.GetLength(0); i++)
        ocene[i, 0] = Convert.ToString(i);

    return ocene;
}

static string[,] uredi(string[,] ocene)
{
    if (ocene.GetLength(0) == 1) // če ima samo 1 vrstico nima ocen zato jih ne mores urejat
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nV tabeli ni zapisanih nobenih ocen, zato jih ne moreš urejati!");
        Console.ResetColor(); 
        return izberi(ocene); // na novo dobiš izbiro funkcij
    }

    Console.Write("\nPri katerem predmetu si želiš urediti oceno? ");
    string predmet = Console.ReadLine();

    int stolp_predmeta = najdi_predmet(ocene, predmet);
    if (stolp_predmeta == -1)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Predmet ni bil najden! Poskusi znova.");
        Console.ResetColor();
        uredi(ocene);
    }

    int vrsta = 1; // default vrsta je 1, če jih je več ga vpraša katera in spremeni vrednost
    if(ocene.GetLength(0) > 2)
    {
        Console.Write("Katero oceno po vrsti si želiš si urediti? ");
        vrsta = int.Parse(Console.ReadLine());
    }

    Console.Write("Vnesi novo oceno: ");
    ocene[vrsta, stolp_predmeta] = Console.ReadLine();

    return ocene;
}

static string[,] izbrisi(string[,] ocene)
{
    if (ocene.GetLength(1) == 1)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nV tabeli ni predmetov, ki bi jih lahko izbrisal!");
        Console.ResetColor();
        return izberi(ocene);
    }

    Console.Write("\nKateri predmet si želiš izbrisati? ");
    string predmet = Console.ReadLine();

    int stolp_predmeta = najdi_predmet(ocene, predmet);
    if (stolp_predmeta == -1)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Predmet ni bil najden! Poskusi znova.");
        Console.ResetColor();
        return izbrisi(ocene);
    }

    string[,] nova = new string[ocene.GetLength(0), ocene.GetLength(1) - 1];

    for(int i = 0; i < ocene.GetLength(0); i++)
        for(int j = 0; j < ocene.GetLength(1); j++)
        {
            if (j < stolp_predmeta)
                nova[i, j] = ocene[i, j];

            else if (j > stolp_predmeta)
                nova[i, j - 1] = ocene[i, j];
        }

    return nova;
}

static string[,] izpisi_predmet(string[,] ocene)
{
    if (ocene.GetLength(1) == 1)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nV tabeli ni predmetov, ki bi jih lahko izpisal!");
        Console.ResetColor();
        return izberi(ocene);
    }

    Console.Write("\nZa kater predmet bi rad izpisal ocene? ");
    string predmet = Console.ReadLine();

    int stolp_predmeta = najdi_predmet(ocene, predmet);
    if (stolp_predmeta == -1)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Predmet ni bil najden! Poskusi znova.");
        Console.ResetColor();
        return izberi(ocene);
    }

    string[,] novo = new string[ocene.GetLength(0), 2];

    for (int i = 0; i < ocene.GetLength(0); i++)
    {
        novo[i, 0] = ocene[i, 0];
        novo[i, 1] = ocene[i, stolp_predmeta];
    }

    izpis(novo);
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
    for (int j = 1; j < ocene.GetLength(1); j++)
        if (ocene[0, j].ToLower() == predmet.ToLower())
            return j;

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