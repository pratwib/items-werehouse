namespace ItemsWarehouse.Models;

using System;

public class Barang
{
    public int KodeBarang { get; set; }
    public string NamaBarang { get; set; }
    public decimal HargaBarang { get; set; }
    public int JumlahBarang { get; set; }
    public DateTime ExpiredBarang { get; set; }
    public int KodeGudang { get; set; }
}