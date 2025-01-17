# SearchInImages

This application processes image files in a selected directory, extracts text using OCR (Optical Character Recognition), and searches for a specific keyword. The results are saved to a CSV file for further analysis.

## Features
- Select a folder containing image files (.png, .jpg, .jpeg).
- Extract text from images using the Tesseract OCR engine.
- Search for a specific keyword in the extracted text.
- Save the results (including matching text and filenames) to a CSV file.
- Progress updates via a status label and progress bar.

## Requirements
- [.NET Framework](https://dotnet.microsoft.com/)
- [Tesseract OCR](https://github.com/tesseract-ocr/tesseract)
- [CsvHelper](https://joshclose.github.io/CsvHelper/)

## Getting Started

### Prerequisites
1. Download and install Tesseract OCR.
   - Ensure the `tessdata` folder is available in the application directory.
   - Include the necessary language data (e.g., `eng.traineddata` for English).
2. Add the required NuGet packages:
   - `Tesseract`
   - `CsvHelper`

### How to Run
1. Clone or download the project to your local machine.
2. Open the project in Visual Studio.
3. Build the project to restore NuGet packages.
4. Run the application.

### Usage
1. Click on the **Browse Folder** button to select a folder containing image files.
2. Enter the keyword to search in the **Search Text** box.
3. Click on the **Start Processing** button to begin processing images.
4. Monitor progress in the status label and progress bar.
5. After processing, results will be saved in a file named `output.csv` in the application's directory.

## Code Overview

### Main Components

#### `btnBrowseFolder_Click`
- Opens a folder browser dialog to select the directory containing images.
- Updates the folder path in the UI.

#### `btnStartProcessing_Click`
- Validates the selected folder.
- Initiates the background processing of images.

#### `ProcessImages`
- Loads images from the selected folder.
- Processes each image using Tesseract OCR to extract text.
- Searches for the specified keyword in the extracted text.
- Updates the progress bar and status label.
- Saves the results to a CSV file.

#### `SaveResultsToCSV`
- Writes the list of results to a CSV file using CsvHelper.

### Error Handling
- Errors during image processing are displayed in a message box and logged for debugging.

## Example Output
The `output.csv` file contains the following columns:
- `Filename`: Name of the processed image file.
- `MatchingText`: The keyword that matched the extracted text.
- `ExtractedText`: The full text extracted from the image.

## Limitations
- The application currently supports `.png`, `.jpg`, and `.jpeg` files.
- Tesseract OCR may require fine-tuning for better accuracy with non-standard fonts or noisy images.

## Contributing
Contributions, bug reports, and feature requests are welcome! Feel free to fork the repository and create a pull request.

## License
This project is licensed under the MIT License. See the `LICENSE` file for details.

## Acknowledgments
- [Tesseract OCR](https://github.com/tesseract-ocr/tesseract)
- [CsvHelper](https://joshclose.github.io/CsvHelper/)
