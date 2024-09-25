using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.DocumentLayoutAnalysis.PageSegmenter;
using UglyToad.PdfPig.Fonts.Standard14Fonts;
using UglyToad.PdfPig.Writer;
using System.Runtime.CompilerServices;
using Microsoft.JSInterop;

namespace Worker;

internal partial class MGEXWorker {
    private static MGEXWorker? _instance = null;
    public static MGEXWorker instance {
        get { 
            if (_instance == null) _instance = new MGEXWorker();
            return _instance;
        }
    }
    [JSExport] internal static void Test([JSMarshalAs<Function<JSType.String>>] Action<string> consoleLog) => instance.Init(consoleLog);

    private void Init(Action<string> consoleLog) {
        consoleLog($"[PDF Engine] Initializing Core...");

        PdfDocumentBuilder pdfDocumentBuilder = new PdfDocumentBuilder();
        PdfPageBuilder page = pdfDocumentBuilder.AddPage(PageSize.A4);
        PdfDocumentBuilder.AddedFont font = pdfDocumentBuilder.AddStandard14Font(Standard14Font.Helvetica);
        page.AddText("Hello World!", 12, new PdfPoint(25, 700), font);

        consoleLog($"[PDF Engine] Initializing Document Layout Analysis...");

        PdfDocument pdf = PdfDocument.Open(pdfDocumentBuilder.Build());
        var blocks = RecursiveXYCut.Instance.GetBlocks(pdf.GetPage(1).GetWords());

        consoleLog($"[PDF Engine] Initialized");
    }
}
