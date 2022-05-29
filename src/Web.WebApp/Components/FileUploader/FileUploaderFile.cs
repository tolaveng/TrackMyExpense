using Microsoft.AspNetCore.Components.Forms;

namespace Web.WebApp.Components.FileUploader
{
    public record FileUploaderFile( string Name, string FileName, IBrowserFile BowserFile = null);
}
