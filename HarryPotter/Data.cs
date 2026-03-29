using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

namespace HarryPotter
{
    public class Data
    {
        public List<Spell> ReadSpells()
        {
            using (var reader = new StreamReader("HP_spells.csv"))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true }))
            {
                var records = csv.GetRecords<dynamic>().ToList();
                var spells = new List<Spell>();
                foreach (var r in records)
                {
                    string name = r.spell;
                    string user = r.use;
                    string indexStr = r.index;
                    if (!int.TryParse(indexStr, out int index)) continue;
                    spells.Add(new Spell { Index = index, Name = name, User = user });
                }
                return spells;
            }
        }

        public List<Character> ReadCharacters(List<Spell> spells = null)
        {
            if (spells == null)
                spells = ReadSpells();

            var characters = new List<Character>();
            using (var reader = new StreamReader("HP_characters.csv"))
            {
                string line = reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    var values = SplitCsvLine(line);

                    if (values.Length < 9)
                        continue;

                    string bdate = Convert.ToString(values[7].Replace(",", "") + values[6].Replace(",", ""));

                    string fullName = values[0].Trim();
                    string nickName = values[1].Trim();
                    string hogwartsHouse = values[2].Trim();
                    string interpretedBy = values[3].Trim();
                    string childrenField = values[4].Trim();
                    string image = values[5].Trim();
                    string birthdateField = bdate;
                    string indexField = values[8].Trim();
                    string knownSpellsField = values[9].Trim();

                    if (!int.TryParse(indexField, out int index))
                        continue;

                    DateTime birthdate;

                    if (!DateTime.TryParse(birthdateField, out birthdate))
                    {
                        birthdate = DateTime.MinValue;
                    }

                    var character = new Character
                    {
                        Index = index,
                        FullName = fullName,
                        NickName = nickName,
                        HogwardsHouse = hogwartsHouse,
                        InterpretedBy = interpretedBy,
                        Image = image,
                        Birthdate = birthdate,
                        Children = new List<Child>(),
                        KnownSpells = new List<Spell>()
                    };

                    if (!string.IsNullOrWhiteSpace(childrenField))
                    {
                        var childNames = childrenField.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (var cn in childNames)
                        {
                            var trimmed = cn.Trim();
                            if (trimmed.Length > 0)
                                character.Children.Add(new Child { FullName = trimmed });
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(knownSpellsField))
                    {
                        var tokens = knownSpellsField.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (var raw in tokens)
                        {
                            var token = raw.Trim();
                            if (token.Length == 0)
                                continue;

                            Spell found = null;

                            if (int.TryParse(token, out int spellIndex))
                            {
                                found = spells.FirstOrDefault(s => s.Index == spellIndex);
                            }

                            if (found == null)
                            {
                                found = spells.FirstOrDefault(s => string.Equals(NormalizeSpellName(s.Name), NormalizeSpellName(token), StringComparison.InvariantCultureIgnoreCase));
                            }

                            if (found != null)
                            {
                                character.KnownSpells.Add(found);
                            }
                            else
                            {
                                Console.WriteLine($"Ismeretlen spell '{token}' a '{character.FullName}' karakterhez!");
                            }
                        }
                    }

                    characters.Add(character);
                }
            }

            return characters;
        }

        private static string NormalizeSpellName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return string.Empty;

            var s = name.Trim();

            if (s.StartsWith("\"") && s.EndsWith("\"") && s.Length >= 2)
                s = s.Substring(1, s.Length - 2);
            return s.Trim();
        }

        private static string[] SplitCsvLine(string line)
        {
            if (line == null)
                return new string[0];

            var fields = new List<string>();
            var sb = new StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        sb.Append('"');
                        i++;
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == ',' && !inQuotes)
                {
                    fields.Add(sb.ToString());
                    sb.Clear();
                }
                else
                {
                    sb.Append(c);
                }
            }

            fields.Add(sb.ToString());

            for (int i = 0; i < fields.Count; i++)
            {
                var f = fields[i].Trim();

                if (f.Length >= 2 && f.StartsWith("\"") && f.EndsWith("\""))
                    f = f.Substring(1, f.Length - 2);

                fields[i] = f.Replace("\"\"", "\"").Trim();
            }

            return fields.ToArray();
        }
    }
}
