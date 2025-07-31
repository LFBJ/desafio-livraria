namespace BookLibraryAPI.Models
{
    /// <summary>
    /// Representa um gênero literário, que pode ter vários livros.
    /// </summary>
    public class Genero
    {
        /// <summary>
        /// Identificador único do gênero.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do gênero (ex: Ficção, Romance, Terror).
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Lista de livros associados a este gênero.
        /// </summary>
        public List<Livro> Livros { get; set; }
    }
}
