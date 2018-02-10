namespace MemorySaver.Domain.ServiceContracts.DTOs.Response
{
    public class GetFileResponseDTO
    {
        public byte[] FileBase64 { get; set; }

        public string Description { get; set; }

        public string FileType { get; set; }

        public string FacebookId { get; set; }

        public string VimeoId { get; set; }
    }
}
