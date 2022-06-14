using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Angular_ASPNETCore_CustomersService.Models {
  
  public class Barang
  {
    [Key]
    public int idBarang { get; set; }
    public string judul { get; set; }
    public string jenisBarang { get; set; }
    public string deskripsi { get; set; }
    public int idPemilik { get; set; }
    public string bukaLelang { get; set; }
    public string tutupLelang { get; set; }
    public string hargaAwal { get; set; }
    public string kelipatan { get; set; }
  }

}