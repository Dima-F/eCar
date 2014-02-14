using System;
using System.IO;
using AppLimit.CloudComputing.SharpBox;
using AppLimit.CloudComputing.SharpBox.DropBox;
using Ionic.Zip;
using eCar.Applicaton.Infrastructure;
using eCar.Applicaton.Models.Service.Interfaces;

namespace eCar.Applicaton.Models.Service.Internal
{
    /// <summary>
    /// ¬ конструкторе инстанциирует закрытое поле IConfigService(IoC), ICloudeStorageCredentials, ICloudStorageConfiguration,
    /// реализует метод дл€ архивировани€ каталога (.zip)
    /// </summary>
    public class CloudService : ICloudService
    {
        private const string ArchiveFolderPath = "/eCar";
        //эта конфигураци€ самого програмного интерфейса SharpBox, в данном случае будет выбрана стандартна€ конфигураци€ дл€ DropBox
        private readonly ICloudStorageConfiguration _cloudStorageConfiguration;
        //это специфичние дл€ пользовател€ токены (дл€ DropBox это API Key, API Secret, им€ пользовател€, пароль)
        private readonly ICloudeStorageCredentials _cloudeStorageCredentials;
        private readonly IConfigService _configService;

        public CloudService(IConfigService configService)
        {
            _configService = configService;
            var config = _configService.Current;

            // currently using DropBox, but this could be made configurable

            _cloudStorageConfiguration = DropBoxConfiguration.GetStandardConfiguration();

            _cloudeStorageCredentials = new DropBoxCredentials
            {
                ConsumerKey = config.Cloud.ConsumerKey,
                ConsumerSecret = config.Cloud.ConsumerSecret,
                UserName = config.Cloud.UserName,
                Password = config.Cloud.Password
            };
        }
        /// <summary>
        /// јрхивирует папку з указаным путем и сохран€ет ее в облачном хранилище.
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public string ArchiveFolder(string folderPath)
        {
            var backupName = Path.GetFileName(folderPath).ToUrlSlug();
            var timestamp = DateTime.Now.ToString("yyyyMMdd-HHmmss");
            var archiveFilename = string.Format("{0}-{1}.zip", backupName, timestamp);

            using (var zip = new ZipFile())
            using (var zipMemoryStream = new MemoryStream())
            {
                zip.AddDirectory(folderPath);
                zip.Save(zipMemoryStream);
                Save(archiveFilename, zipMemoryStream);
            }

            return archiveFilename;
        }
        /// <summary>
        /// —охран€ет в облачном хранилище указаный файл.
        /// </summary>
        private void Save(string filename, MemoryStream memoryStream)
        {
            //ќсновной класс в SharpBox API, в нем есть все небходимые методы дл€ работы с cloud- провайдером.
            var storage = new CloudStorage();
            //открываем соединени€ с DropBox
            storage.Open(_cloudStorageConfiguration, _cloudeStorageCredentials);
            //вз€ть директрию /NBlog в DropBox
            var backupFolder = storage.GetFolder(ArchiveFolderPath);
            if (backupFolder == null) { throw new Exception("Cloud folder not found: " + ArchiveFolderPath); }
            //создаем указаный файл в хранилище
            var cloudFile = storage.CreateFile(backupFolder, filename);
            //собственно записываем данные в файл.
            using (var cloudStream = cloudFile.GetContentStream(FileAccess.Write))
            {
                cloudStream.Write(memoryStream.GetBuffer(), 0, (int)memoryStream.Position);
            }
            //закрываем соединение.
            if (storage.IsOpened) { storage.Close(); }
        }

    }
}
