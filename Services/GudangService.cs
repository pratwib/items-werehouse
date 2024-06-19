using ItemsWarehouse.Models;

namespace ItemsWarehouse.Services;

using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

public class GudangService
{
    private readonly string _connectionString;

    public GudangService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Gudang> GetAllGudang()
    {
        List<Gudang> gudangs = new List<Gudang>();
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Gudang";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Gudang gudang = new Gudang
                    {
                        KodeGudang = Convert.ToInt32(reader["KodeGudang"]),
                        NamaGudang = reader["NamaGudang"].ToString()
                    };
                    gudangs.Add(gudang);
                }
            }
        }

        return gudangs;
    }

    public void AddGudang(string namaGudang)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string query = "INSERT INTO Gudang (NamaGudang) VALUES (@NamaGudang)";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@NamaGudang", namaGudang);
            cmd.ExecuteNonQuery();
        }
    }

    public void UpdateGudang(int kodeGudang, string namaGudang)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string query = "UPDATE Gudang SET NamaGudang = @NamaGudang WHERE KodeGudang = @KodeGudang";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@NamaGudang", namaGudang);
            cmd.Parameters.AddWithValue("@KodeGudang", kodeGudang);
            cmd.ExecuteNonQuery();
        }
    }

    public void DeleteGudang(int kodeGudang)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string query = "DELETE FROM Gudang WHERE KodeGudang = @KodeGudang";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@KodeGudang", kodeGudang);
            cmd.ExecuteNonQuery();
        }
    }
}