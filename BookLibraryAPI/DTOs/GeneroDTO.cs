namespace BookLibraryAPI.DTOs
{
    /// <summary>
    /// DTO para transferência de dados do Gênero.
    /// </summary>
    public class GeneroDTO
    {
        /// <summary>
        /// Identificador único do gênero.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do gênero.
        /// </summary>
        public string Nome { get; set; }
    }
}
