namespace BookLibraryAPI.DTOs
{

    /// <summary>
    /// DTO para transferência de dados do Livro.
    /// </summary>
    public class LivroDTO
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
        /// Identificador do gênero ao qual o livro pertence.
        /// </summary>
        public int GeneroId { get; set; }

        /// <summary>
        /// Identificador do autor do livro.
        /// </summary>
        public int AutorId { get; set; }
    }
}
