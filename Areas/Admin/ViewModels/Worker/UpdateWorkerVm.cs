namespace Bilet_15.Areas.Admin.ViewModels.Worker
{
    public class UpdateWorkerVm
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Desicnation { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
