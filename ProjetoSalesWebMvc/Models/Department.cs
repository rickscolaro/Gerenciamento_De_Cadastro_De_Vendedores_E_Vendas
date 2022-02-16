

using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace ProjetoSalesWebMvc.Models {

    public class Department {

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Seller> Sellers { get; set; } = new List<Seller>(); // Um departamento tem muitos vendedores

        // Não inclui atributos que são coleções
        public Department( string name) {

            Name = name;
        }

       
        public Department() { }


        // Adicionar vendedor
        public void AddSeller(Seller seller) {

            Sellers.Add(seller);
        }

        // Total de vendas do departamento dentro do intervalo de data
        public double TotalSales(DateTime initial, DateTime final) {

            // A soma de vendedores para cada vendedor na lista
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
        }
    }
}