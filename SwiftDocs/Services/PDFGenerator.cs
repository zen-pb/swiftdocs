using System;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Fonts;

namespace SwiftDocs.Services
{
    public class PDFGenerator
    {
        public void GeneratePdf(string filePath, string title, string content)
        {
            try
            {
                GlobalFontSettings.FontResolver = CustomFontResolver.Instance;

                var document = new PdfDocument();
                document.Info.Title = title;

                var page = document.AddPage();
                var gfx = XGraphics.FromPdfPage(page);
                
                XFont titleFont = new XFont("Helvetica", 18);
                XFont contentFont = new XFont("Helvetica", 12);
                
                gfx.DrawString(title, titleFont, XBrushes.Black,
                    new XRect(0, 0, page.Width, 40), XStringFormats.TopCenter);
                
                string[] lines = content.Split(new[] { '\n' }, StringSplitOptions.None);
                
                double yPos = 60;
                
                foreach (var line in lines)
                {
                    gfx.DrawString(line, contentFont, XBrushes.Black,
                        new XRect(40, yPos, page.Width - 80, page.Height - 100), XStringFormats.TopLeft);
                    
                    yPos += contentFont.GetHeight() + 2;
                }

                document.Save(filePath);
                Console.WriteLine($"PDF generated successfully at: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating PDF: {ex.Message}");
            }
        }
    }
}