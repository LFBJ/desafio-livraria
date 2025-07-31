namespace BookLibraryAPI.Models
{

    /// <summary>
    /// Representa um autor, que pode possuir vários livros.
    /// </summary>
    public class Autor
    {
        /// <summary>
        /// Identificador único do autor.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do autor.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Lista de livros escritos por este autor.
        /// </summary>
        public List<Livro> Livros { get; set; }
    }
}
