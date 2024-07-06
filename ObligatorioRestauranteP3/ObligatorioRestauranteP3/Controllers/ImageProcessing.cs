using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model.Strings;
using Microsoft.EntityFrameworkCore;
using ObligatorioP3.Models;

namespace ObligatorioP3.Controllers
{
    public static class ImageProcessing
    {
        
        public static async Task SavePicture(String Directorio, int Id, IFormFile FotoUsuario)
        {
            var fileName = Path.GetFileNameWithoutExtension(FotoUsuario.FileName);
            var fileExt = Path.GetExtension(FotoUsuario.FileName);
            var newUserID = Id;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + Directorio, newUserID + fileExt);
            using (var stream = System.IO.File.Create(filePath))
            {
                await FotoUsuario.CopyToAsync(stream);
            }
        }
        public static List<string> GetfullFileName(string Directorio, string? Id)
        {
            var filePath = "wwwroot/" + Directorio;
            var files = Directory.GetFiles(filePath);
            List<string> FileName = new List<string>();
            if (Id != null)
            {
                foreach (var file in files)
                {
                    string extension = Path.GetExtension(file);
                    string name = Path.GetFileNameWithoutExtension(file);
                    FileName.Add(name + extension);
                }
            }
            else
            {
                foreach (var file in files)
                {
                    if (Path.GetFileNameWithoutExtension(file) == Id)
                    {
                        string extension = Path.GetExtension(file);
                        string name = Path.GetFileNameWithoutExtension(file);
                        FileName.Add(name + extension);
                    }
                }
            }
            return FileName;
        }
    }
}
