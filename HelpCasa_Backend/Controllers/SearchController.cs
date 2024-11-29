using Microsoft.AspNetCore.Mvc;
using HelpCasa.Data;
using HelpCasa.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpCasa.Controllers
{
    /// <summary>
    /// Controller responsável pela funcionalidade de pesquisa.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Construtor da classe SearchController, responsável pela injeção de dependência do contexto de banco de dados.
        /// </summary>
        /// <param name="context">O contexto de banco de dados injetado.</param>
        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }


    }
}