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
    /// � ������������ ������������� �������� ���� IConfigService(IoC), ICloudeStorageCredentials, ICloudStorageConfiguration,
    /// ��������� ����� ��� ������������� �������� (.zip)
    /// </summary>
    public class CloudService : ICloudService
    {
        private const string ArchiveFolderPath = "/eCar";
        //��� ������������ ������ ����������� ���������� SharpBox, � ������ ������ ����� ������� ����������� ������������ ��� DropBox
        private readonly ICloudStorageConfiguration _cloudStorageConfiguration;
        //��� ����������� ��� ������������ ������ (��� DropBox ��� API Key, API Secret, ��� ������������, ������)
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
        /// ���������� ����� � �������� ����� � ��������� �� � �������� ���������.
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
        /// ��������� � �������� ��������� �������� ����.
        /// </summary>
        private void Save(string filename, MemoryStream memoryStream)
        {
            //�������� ����� � SharpBox API, � ��� ���� ��� ���������� ������ ��� ������ � cloud- �����������.
            var storage = new CloudStorage();
            //��������� ���������� � DropBox
            storage.Open(_cloudStorageConfiguration, _cloudeStorageCredentials);
            //����� ��������� /NBlog � DropBox
            var backupFolder = storage.GetFolder(ArchiveFolderPath);
            if (backupFolder == null) { throw new Exception("Cloud folder not found: " + ArchiveFolderPath); }
            //������� �������� ���� � ���������
            var cloudFile = storage.CreateFile(backupFolder, filename);
            //���������� ���������� ������ � ����.
            using (var cloudStream = cloudFile.GetContentStream(FileAccess.Write))
            {
                cloudStream.Write(memoryStream.GetBuffer(), 0, (int)memoryStream.Position);
            }
            //��������� ����������.
            if (storage.IsOpened) { storage.Close(); }
        }

    }
}
