namespace Domain.Domain.Model.OnlineSerieBoxDTOs
{
    public class OnlineSerieBoxDTO
    {
        public int OnlineSerieBoxId { get; set; }


        public float Price { get; set; }

        public string Name { get; set; }

        public bool IsSecretOpen { get; set; }

        public int Turn { get; set; }
        public int BoxId { get; set; }
    }
}
