namespace RotaViagem.API.ViewModels
{
    public class UpdateRotaViewModel
    {
        public int Id { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public decimal Valor { get; set; }
    }
}
