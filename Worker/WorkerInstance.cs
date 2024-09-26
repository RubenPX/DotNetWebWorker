using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.DocumentLayoutAnalysis.PageSegmenter;
using UglyToad.PdfPig.Fonts.Standard14Fonts;
using UglyToad.PdfPig.Writer;

namespace Worker;

internal class WorkerInstance {
    private Action<string> postMessage;

    public WorkerInstance(Action<string> postMessage) {
        this.postMessage = postMessage;
    }

    public async void onMessage(string data) {
        if (data.ToLower() == "init") await testInit();
    }

    private async Task testInit() {
        postMessage($"[PDF Engine] Initializing Core...");

        PdfDocumentBuilder pdfDocumentBuilder = new PdfDocumentBuilder();
        PdfPageBuilder page = pdfDocumentBuilder.AddPage(PageSize.A4);
        PdfDocumentBuilder.AddedFont font = pdfDocumentBuilder.AddStandard14Font(Standard14Font.Helvetica);
        page.AddText("Hello World!", 12, new PdfPoint(25, 700), font);

        postMessage($"[PDF Engine] Initializing Document Layout Analysis...");

        PdfDocument pdf = PdfDocument.Open(pdfDocumentBuilder.Build());
        var blocks = RecursiveXYCut.Instance.GetBlocks(pdf.GetPage(1).GetWords());

        postMessage($"[PDF Engine] Initialized");
    }

}
