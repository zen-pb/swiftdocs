using PdfSharp.Fonts;
using System.IO;

namespace SwiftDocs.Services
{
    public class CustomFontResolver : IFontResolver
    {
        public static readonly CustomFontResolver Instance = new CustomFontResolver();

        public string DefaultFontName => "Helvetica";

        public byte[] GetFont(string faceName)
        {
            string fontPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Helvetica.ttf");
            return File.ReadAllBytes(fontPath);
        }

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            return new FontResolverInfo("Helvetica");
        }
    }
}