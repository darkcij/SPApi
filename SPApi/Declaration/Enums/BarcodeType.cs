using System;

namespace SPApi.Declaration.Enums
{

    // 요약:
    //     Enumerates barcode formats known to this package.
    [Flags]
    public enum BarcodeType
    {
        // 요약:
        //     Aztec 2D barcode format.
        AZTEC = 1,
        //
        // 요약:
        //     CODABAR 1D format.
        CODABAR = 2,
        //
        // 요약:
        //     Code 39 1D format.
        CODE_39 = 4,
        //
        // 요약:
        //     Code 93 1D format.
        CODE_93 = 8,
        //
        // 요약:
        //     Code 128 1D format.
        CODE_128 = 16,
        //
        // 요약:
        //     Data Matrix 2D barcode format.
        DATA_MATRIX = 32,
        //
        // 요약:
        //     EAN-8 1D format.
        EAN_8 = 64,
        //
        // 요약:
        //     EAN-13 1D format.
        EAN_13 = 128,
        //
        // 요약:
        //     ITF (Interleaved Two of Five) 1D format.
        ITF = 256,
        //
        // 요약:
        //     MaxiCode 2D barcode format.
        MAXICODE = 512,
        //
        // 요약:
        //     PDF417 format.
        PDF_417 = 1024,
        //
        // 요약:
        //     QR Code 2D barcode format.
        QR_CODE = 2048,
        //
        // 요약:
        //     RSS 14
        RSS_14 = 4096,
        //
        // 요약:
        //     RSS EXPANDED
        RSS_EXPANDED = 8192,
        //
        // 요약:
        //     UPC-A 1D format.
        UPC_A = 16384,
        //
        // 요약:
        //     UPC-E 1D format.
        UPC_E = 32768,
        //
        // 요약:
        //     UPC_A | UPC_E | EAN_13 | EAN_8 | CODABAR | CODE_39 | CODE_93 | CODE_128 |
        //     ITF | RSS_14 | RSS_EXPANDED without MSI (to many false-positives) and IMB
        //     (not enough tested, and it looks more like a 2D)
        All_1D = 61918,
        //
        // 요약:
        //     UPC/EAN extension format. Not a stand-alone format.
        UPC_EAN_EXTENSION = 65536,
        //
        // 요약:
        //     MSI
        MSI = 131072,
        //
        // 요약:
        //     Plessey
        PLESSEY = 262144,
        //
        // 요약:
        //     Intelligent Mail barcode
        IMB = 524288,
    }

}
