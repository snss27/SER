using Microsoft.AspNetCore.Http;

namespace SER.Domain.Services;
public interface IFilesService
{
	String SaveStudentPhoto(IFormFile photo, String groupAlias, String studentAlias);
	String[] SaveStudentPhotos(IFormFile[] photos, String groupAlias, String studentAlias);
}
