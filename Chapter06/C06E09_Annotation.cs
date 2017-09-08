﻿using System.IO;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Annot;
using iText.Kernel.Pdf.Navigation;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iTextBuildingBlocks;

namespace Chapter06
{
    public class C06E09_Annotation
    {
private static readonly string DEST = $@"{Paths.ResultsPath}\chapter06\jekyll_hyde_annotation.pdf";

        public static void Main(string[] args)
        {
            FileInfo file = new FileInfo(DEST);
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }

            CreatePdf(DEST);
        }

        private static void CreatePdf(string dest)
        {
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));

            Document document = new Document(pdf);

            PdfAction js = PdfAction.CreateJavaScript("app.alert('Boo!');");

            PdfAnnotation la1 = new PdfLinkAnnotation(new Rectangle(0, 0, 0, 0))
                .SetHighlightMode(PdfAnnotation.HIGHLIGHT_INVERT)
                .SetAction(js)
                .SetBorderStyle(PdfAnnotation.STYLE_UNDERLINE);

            Link link1 = new Link("here", (PdfLinkAnnotation)la1);

            document.Add(new Paragraph()
                .Add("Click ")
                .Add(link1)
                .Add(" if you want to be scared."));

            PdfAnnotation la2 = new PdfLinkAnnotation(new Rectangle(0, 0, 0, 0))
                .SetDestination(PdfExplicitDestination.CreateFit(2))
                .SetHighlightMode(PdfAnnotation.HIGHLIGHT_PUSH)
                .SetBorderStyle(PdfAnnotation.STYLE_INSET);

            Link link2 = new Link("next page", (PdfLinkAnnotation)la2);

            document.Add(new Paragraph()
                .Add("Go to the ")
                .Add(link2)
                .Add(" if you're too scared."));

            document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
            document.Add(new Paragraph().Add("There, there, everything is OK."));

            document.Close();
        }
    }
}