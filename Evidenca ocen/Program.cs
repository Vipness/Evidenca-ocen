using System.Text;
Console.OutputEncoding = Encoding.UTF8; // izpis nastavil na utf-8, da se vidijo tudi šumniki
Console.Title = "Evidenca ocen"; // spremenim ime konzole

Console.Write("Vnesi število predmetov, ki jih imaš v šoli: ");
int st_pred = int.Parse(Console.ReadLine());

// 2d tabela
string[,] ocene = new string[1, st_pred + 1];
ocene[0, 0] = "Ocene";

// imena predmetov
for (int i = 0; i < st_pred; i++)
{
    Console.Write("Vnesi ime {0} predmeta: ", i + 1);
    ocene[0, i + 1] = Console.ReadLine();
}

izpis(ocene);

static void izpis(string[,] t)
{
    for (int i = 0; i < t.GetLength(0); i++)
    {
        Console.WriteLine();
        for (int j = 0; j < t.GetLength(1); j++)
        {
            if(i == 0 && j > 0)
                Console.ForegroundColor = ConsoleColor.DarkYellow;

            else if(j == 0)
                Console.ForegroundColor = ConsoleColor.DarkBlue;

            Console.Write($"|\t{t[i, j]}\t|");
            Console.ResetColor();
        }
    }
    Console.WriteLine();
}