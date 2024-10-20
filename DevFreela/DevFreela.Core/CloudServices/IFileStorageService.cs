namespace DevFreela.Core.CloudServices;

public interface IFileStorageService
{
    void UploadFile(byte[] bytes, string fileName);
}