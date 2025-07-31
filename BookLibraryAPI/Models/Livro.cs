namespace BookLibraryAPI.Models
{

    /// <summary>
    /// Representa um livro, que pertence a um autor e a um gênero.
    /// </summary>
    public class Livro
    {
        /// <summary>
        /// Identificador único do livro.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Título do livro.
        /// </summary>
        public string Titulo { get; set; }

        /// <summary>
        /// Chave estrangeira para o gênero do livro.
        /// </summary>
        public int GeneroId { get; set; }

        /// <summary>
        /// Referência para o gênero do livro.
        /// </summary>
        public Genero Genero { get; set; }

        /// <summary>
        /// Chave estrangeira para o autor do livro.
        /// </summary>
        public int AutorId { get; set; }

        /// <summary>
        /// Referência para o autor do livro.
        /// </summary>
        public Autor Autor { get; set; }
    }
}
