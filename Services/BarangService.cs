using ItemsWarehouse.Models;

namespace ItemsWarehouse.Services;

using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

public class BarangService
{
    private readonly string _connectionString;

    public BarangService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Barang> GetAllBarang()
    {
        List<Barang> barangs = new List<Barang>();
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Barang";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Barang barang = new Barang
                    {
                        KodeBarang = Convert.ToInt32(reader["KodeBarang"]),
                        NamaBarang = reader["NamaBarang"].ToString(),
                        HargaBarang = Convert.ToDecimal(reader["HargaBarang"]),
                        JumlahBarang = Convert.ToInt32(reader["JumlahBarang"]),
                        ExpiredBarang = Convert.ToDateTime(reader["ExpiredBarang"]),
                        KodeGudang = Convert.ToInt32(reader["KodeGudang"])
                    };
                    barangs.Add(barang);
                }
            }
        }

        return barangs;
    }

    public void AddBarang(string namaBarang, decimal hargaBarang, int jumlahBarang, DateTime expiredBarang,
        int kodeGudang)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string query = "INSERT INTO Barang (NamaBarang, HargaBarang, JumlahBarang, ExpiredBarang, KodeGudang) " +
                           "VALUES (@NamaBarang, @HargaBarang, @JumlahBarang, @ExpiredBarang, @KodeGudang)";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@NamaBarang", namaBarang);
            cmd.Parameters.AddWithValue("@HargaBarang", hargaBarang);
            cmd.Parameters.AddWithValue("@JumlahBarang", jumlahBarang);
            cmd.Parameters.AddWithValue("@ExpiredBarang", expiredBarang);
            cmd.Parameters.AddWithValue("@KodeGudang", kodeGudang);
            cmd.ExecuteNonQuery();
        }
    }

    public void UpdateBarang(int kodeBarang, string namaBarang, decimal hargaBarang, int jumlahBarang,
        DateTime expiredBarang, int kodeGudang)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string query = "UPDATE Barang SET NamaBarang = @NamaBarang, HargaBarang = @HargaBarang, " +
                           "JumlahBarang = @JumlahBarang, ExpiredBarang = @ExpiredBarang, KodeGudang = @KodeGudang " +
                           "WHERE KodeBarang = @KodeBarang";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@NamaBarang", namaBarang);
            cmd.Parameters.AddWithValue("@HargaBarang", hargaBarang);
            cmd.Parameters.AddWithValue("@JumlahBarang", jumlahBarang);
            cmd.Parameters.AddWithValue("@ExpiredBarang", expiredBarang);
            cmd.Parameters.AddWithValue("@KodeGudang", kodeGudang);
            cmd.Parameters.AddWithValue("@KodeBarang", kodeBarang);
            cmd.ExecuteNonQuery();
        }
    }

    public void DeleteBarang(int kodeBarang)
    {
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string query = "DELETE FROM Barang WHERE KodeBarang = @KodeBarang";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@KodeBarang", kodeBarang);
            cmd.ExecuteNonQuery();
        }
    }

    public DataTable FilterBarangWithPaging(int page, int pageSize, string gudangName, DateTime? expiredDate)
    {
        DataTable dt = new DataTable();
        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("FilterBarangWithPaging", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@page", page);
            cmd.Parameters.AddWithValue("@pageSize", pageSize);
            cmd.Parameters.AddWithValue("@gudangName", gudangName);
            cmd.Parameters.AddWithValue("@expiredDate", expiredDate);

            using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
            {
                adapter.Fill(dt);
            }
        }

        return dt;
    }
}