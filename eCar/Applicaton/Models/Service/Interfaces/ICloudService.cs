namespace eCar.Applicaton.Models.Service.Interfaces
{
    /// <summary>
    /// Интерфейс предлагает единственный метод для архивации папки
    /// </summary>
    public interface ICloudService
    {
        string ArchiveFolder(string folderPath);
    }
}