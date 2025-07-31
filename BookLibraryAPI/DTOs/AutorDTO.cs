namespace BookLibraryAPI.DTOs
{
    /// <summary>
    /// DTO para transferência de dados do Autor.
    /// </summary>
    public class AutorDTO
    {
        /// <summary>
        /// Identificador único do autor.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do autor.
        /// </summary>
        public string Nome { get; set; }
    }

}
