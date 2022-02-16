
using Microsoft.EntityFrameworkCore;
using ProjetoSalesWebMvc.Data;
using ProjetoSalesWebMvc.Models;

namespace ProjetoSalesWebMvc.Services {

    // Carregar os departamentos para poder selecionar na tela cadastro
    public class DepartmentService {

        private readonly SalesWebMvcContext _context;

        public DepartmentService(SalesWebMvcContext context) {
            
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync() {

            // Lista ordenada por nomes
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }

    }
}