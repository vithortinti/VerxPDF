# VerxPDF
VerxPDF is a free, open-source application that offers basic offline PDF editing features. With it, you can convert PDFs into JPG images, delete unwanted pages and combine several PDF files into a single document. However, it's important to note that its functionalities are limited to these three specific actions in the context of PDF editing. Although we can't say that it is the definitive solution for all PDF editing needs, it is a useful option for simple tasks. In addition, the development of a graphical interface is being considered for the future, making the application more accessible to ordinary users. It's worth noting that VerxPDF is a command-line application and works offline, without the need for an internet connection.

---

## Download
You can find the available versions and download them [here](https://github.com/vithortinti/VerxPDF/releases).

---

## Libraries
This application makes use of three libraries.
* [PdfiumViewer](https://github.com/pvginkel/PdfiumViewer)
    * Used to convert PDF pages into JPG images.
    * LICENSE: [APACHE 2.0](https://www.apache.org/licenses/LICENSE-2.0.html)
* [PdfSharp](https://www.pdfsharp.net/)
    * Used to delete PDF pages.
    * Used to merge PDF documents.
    * LICENSE: [MIT](https://mit-license.org/)
* [Flexcon](https://github.com/vithortinti/flexcon)
    * Used to create executors for the application.
    * LICENSE: [MIT](https://mit-license.org/)

---

## Getting Started
This session contains the steps for using and starting the application.

1. Check that .NET Core 7.0 is installed.
2. Clone the repository in a directory of your choice.
```
git clone https://github.com/vithortinti/VerxPDF.git
```
3. Check that the Flexcon library is added to the application. If it isn't, you can get it here, or add it via the [/src/DLLs](https://github.com/vithortinti/VerxPDF/tree/main/src/DLLs) repository path.
4. Now you can run the existing commands in the application, just do the following:
```bash
dotnet <EXECUTOR-NAME> [PARAMETERS]
```
> Note: Executors and parameters in the [How to Use](#how-to-use) section.

---

## How to use
In this section, the application's features and how they are used will be detailed.

Before demonstrating, here are some standard parameters that should be used in all executors:

-p (Required): Parameter used to identify which PDF document will be used. 

-d (Required): Parameter used to indicate where the modifications made to the application will be placed.

> Note: The parameters have no defined order of use, it doesn't matter if you use them at the beginning or at the end of the command line, what matters is that the required parameters are specified. Except for the resource name, it should always be the first to be specified.

### Image
```bash
verxpdf image [-p <PDF-FILE>] [-d <DESTINATION-DIRECTORY>] [-s <SIZE> OPTIONAL] [-q <QUALITY> OPTIONAL]
```
-s: Image size parameter.
- slide: Creates a JPG image with size 960x720;
- full-hd: Creates a JPG image with size 1920x1080;
- a4: Creates a JPG image with size 2480x3508;
- {Width}x{Height}: Creates a JPG image with the specified size in pixels. Example: 1000x1000.

-q: Image quality parameter. Must to be used with size parameter.
- low: Low image quality (faster) - Divide the number of pixels by 4;
- normal: Normal image quality (default option) - Uses the size specified in the size parameter;
- high: High image quality (slower) - Multiplies the number of pixels by 4.

### Merge
```bash
verxpdf merge [-p <PDF-FILES>] [-d <DESTINATION-DIRECTORY>] [-n <NEW-PDF-NAME> OPTIONAL]
```
-n: Name of the final PDF when the joining process has finished.

### Delete
```bash
verxpdf delete [-p <PDF-FILE>] [-d <DESTINATION-DIRECTORY>] [-r <PAGE-NUMBER | PAGE-INTERVAL>]
```
-r (Required): Pages to remove.
- {x}: Remove the x page.
- {x}to{y}: Removes from the page x to the page y specified.
- {x}toEnd: Removes from the page x to the last page of the document.
- {x}toEnd-{y}: Removes from the page x to the last page of the document minus y.

### Help
It shows the resources and how they are used within the application.

```bash
verxpdf --help [<OPTION> OPTIONAL]
```
OPTION: Shows the help for a specific option.

### Version
Shows the version of VerxPDF in use.

```bash
verxpdf --version
```
---

### License
This application takes advantage of the APACHE 2.0 license, you can find out more about it [here](https://www.apache.org/licenses/LICENSE-2.0.html).
