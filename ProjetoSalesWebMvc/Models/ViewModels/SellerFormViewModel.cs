
namespace ProjetoSalesWebMvc.Models.ViewModels {

    // Tela de cadastro de vendedor. Campo para selecionar Qual o departamento
    public class SellerFormViewModel {

        public Seller Seller { get; set; }
        
        public ICollection<Department> Departments { get; set; }

    }
}