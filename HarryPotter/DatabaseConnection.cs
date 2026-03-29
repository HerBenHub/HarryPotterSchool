using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using MySql.Data.MySqlClient;
using CsvHelper;

namespace HarryPotter
{
    public class DatabaseConnection
    {
        private readonly string _connectionString;

        public DatabaseConnection(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            _connectionString = connectionString;
        }

        public bool WriteAllToDb(List<Spell> spells, List<Character> characters)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();

                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {

                        var deleteSpellsCmd = new MySqlCommand("DELETE FROM spells", conn, transaction);
                        int deletedSpells = deleteSpellsCmd.ExecuteNonQuery();

                        var deleteCharactersCmd = new MySqlCommand("DELETE FROM characters", conn, transaction);
                        int deletedChars = deleteCharactersCmd.ExecuteNonQuery();

                        int spellIndex = 0;
                        foreach (var spell in spells)
                        {
                            spellIndex++;
                              var insertSpellCmd = new MySqlCommand("INSERT INTO `spells` (`spell_id`, `name`, `use`) VALUES (@Index, @Name, @Use)", conn, transaction);
                                insertSpellCmd.Parameters.AddWithValue("@Index", spell.Index);
                                insertSpellCmd.Parameters.AddWithValue("@Name", spell.Name);
                                insertSpellCmd.Parameters.AddWithValue("@Use", spell.User);
                                try
                                {
                                    insertSpellCmd.ExecuteNonQuery();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"SQL hiba: {insertSpellCmd.CommandText}");
                                    Console.WriteLine(ex);
                                    throw;
                                }
                            
                        }

                        foreach (var character in characters)
                        {
                            var insertCharacterCmd = new MySqlCommand(
                                "INSERT INTO `characters` (`character_id`, `full_name`, `nickname`, `hogwarts_house`, `interpreted_by`, `image`, `birthdate`) " +
                                "VALUES (@Index, @FullName, @NickName, @HogwartsHouse, @InterpretedBy, @Image, @Birthdate)", conn, transaction);

                            insertCharacterCmd.Parameters.AddWithValue("@Index", character.Index);
                            insertCharacterCmd.Parameters.AddWithValue("@FullName", character.FullName);
                            insertCharacterCmd.Parameters.AddWithValue("@NickName", character.NickName);
                            insertCharacterCmd.Parameters.AddWithValue("@HogwartsHouse", character.HogwardsHouse);
                            insertCharacterCmd.Parameters.AddWithValue("@InterpretedBy", character.InterpretedBy);
                            insertCharacterCmd.Parameters.AddWithValue("@Image", character.Image);
                            insertCharacterCmd.Parameters.AddWithValue("@Birthdate", character.Birthdate);

                            try
                            {
                                insertCharacterCmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"SQL hiba: {insertCharacterCmd.CommandText}");
                                foreach (MySqlParameter p in insertCharacterCmd.Parameters) Console.WriteLine($"{p.ParameterName}={p.Value}");
                                Console.WriteLine(ex);
                                throw;
                            }
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        try { transaction.Rollback(); } catch { }
                        return false;
                    }
                }
            }
        }

    }
}
