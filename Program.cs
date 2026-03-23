using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarryPotter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Data data = new Data();
            List<Character> characters = data.ReadCharacters();
            List<Spell> spells = data.ReadSpells();

            //Tesztelés: kiíratás a konzolra
            foreach (var character in characters)
            {
                Console.WriteLine($"Karakterek: {character.FullName}, Becenév: {character.NickName}, Ház: {character.HogwardsHouse}, Leírva: {character.InterpretedBy}");
                Console.WriteLine("Ismert igék:");
                foreach (var spell in character.KnownSpells)
                {
                    Console.WriteLine($"- {spell.Name} (Felhasználó: {spell.User})");
                }
                Console.WriteLine();
            }

            DatabaseConnection dbConnection = new DatabaseConnection("server=localhost;database=HP;password=;user=root;port=3306");

            
            if (dbConnection.WriteAllToDb(spells, characters))
            {
                Console.WriteLine("Adatok sikeresen mentve az adatbázisba.");
            }
            else
            {
                Console.WriteLine("Hiba történt az adatok mentése során.");
            }



            Console.ReadLine();
        }
    }
}
