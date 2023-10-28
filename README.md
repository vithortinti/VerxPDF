# VerxPDF
VerxPDF is a free, open-source application that offers basic offline PDF editing features. With it, you can convert PDFs into JPG images, delete unwanted pages and combine several PDF files into a single document. However, it's important to note that its functionalities are limited to these three specific actions in the context of PDF editing. Although we can't say that it is the definitive solution for all PDF editing needs, it is a useful option for simple tasks. In addition, the development of a graphical interface is being considered for the future, making the application more accessible to ordinary users. It's worth noting that VerxPDF is a command-line application and works offline, without the need for an internet connection.

---

## Download
This branch is restricted to the application's source code. If you want to download the application directly, you can find more information about this process [here](https://github.com/vithortinti/VerxPDF.App).

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
dotnet run <EXECUTOR-NAME> [PARAMETERS]
```
> Note: Executors and parameters in the [How to Use](#how-to-use) section.

---

## How to use
In this section, the application's features and how they are used will be detailed.

Before demonstrating, here are some standard parameters that should be used in all executors:

-p (Required): Parameter used to identify which PDF document will be used. <br>
-d (Required): Parameter used to indicate where the modifications made to the application will be placed. <br>

> Note: The parameters have no defined order of use, it doesn't matter if you use them at the beginning or at the end of the command line, what matters is that the required parameters are specified. Except for the resource name, it should always be the first to be specified.

### Image
```bash
verxpdf image [-p <PDF-FILE>] [-d <DESTINATION-DIRECTORY>] [-s <SIZE> OPTIONAL] [-q <QUALITY> OPTIONAL]
```
-s: Image size parameter. You can see more about the sizes in the [image-config](#image-configuration) option. <br>
- [width]x[height]: Custom image size when converting PDF to image. <br>
-q: Image quality parameter. Multiplies the image size by the integer entered.

### Image Configuration
```bash
verxpdf image-config [--create-size <SIZE-NAME> <SIZE> | --update-size <SIZE-NAME> <NEW-SIZE> | --delete-size <SIZE-NAME> | --show-size <SIZE-NAME> | --show-sizes]
```
--create-size: Stores a custom size for the image it will be converted into with the name you choose. <br>
--delete-size: Deletes a configured size. <br>
--update-size: Updates a configured size. <br>
--show-sizes: Shows all configured sizes. <br>
--show-size: Shows a configured size. <br>

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
