using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;
using CsvHelper;
using System.Globalization;

namespace SearchInImages
{
    public partial class Main : Form
    {
        private string imageDirectory;
        private List<Result> results = new List<Result>(); 
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                imageDirectory = folderDialog.SelectedPath;
                txtFolderPath.Text = imageDirectory;
            }
        }

        private void btnStartProcessing_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(imageDirectory))
            {
                MessageBox.Show("Please select an image directory.");
                return;
            }

            // Start processing in the background
            Task.Run(() => ProcessImages());
            
        }
        private async void ProcessImages()
        {
            // Update UI on the UI thread
            UpdateStatusLabel("Processing images...");
            progressBar.Value = 0;

            // Get list of image files
            string[] imageFiles = Directory.GetFiles(imageDirectory, "*.png")
                .Union(Directory.GetFiles(imageDirectory, "*.jpg"))
                .Union(Directory.GetFiles(imageDirectory, "*.jpeg"))
                .ToArray();

            int totalFiles = imageFiles.Length;
            progressBar.Maximum = totalFiles;
            int processedCount = 0;

            // Initialize Tesseract OCR engine
            using (TesseractEngine engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
            {
                foreach (string imageFile in imageFiles)
                {
                    try
                    {
                        // Open image file
                        using (var img = Pix.LoadFromFile(imageFile))
                        {
                            // Process image with Tesseract OCR
                            engine.SetVariable("tessedit_pageseg_mode", "7"); // Single block of text
                            var page = engine.Process(img);
                            string extractedText = page.GetText().Trim();

                            // Search for matching texts
                             
                                if (extractedText.Contains(URL_TextBox.Text))
                                {
                                    // Add result to the list
                                    results.Add(new Result
                                    {
                                        Filename = Path.GetFileName(imageFile),
                                        MatchingText = URL_TextBox.Text,
                                        ExtractedText = extractedText
                                    });
                                }
                             
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error processing {Path.GetFileName(imageFile)}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    // Update progress bar
                    processedCount++;
                    progressBar.Value = processedCount;

                    // Update status label
                    UpdateStatusLabel($"Processed {processedCount} of {totalFiles} images.");
                }
            }

            // Save results to CSV
            SaveResultsToCSV();

            // Update status label
            UpdateStatusLabel("Processing complete. Results saved to output.csv.");
        }

        private void SaveResultsToCSV()
        {
            using (var writer = new StreamWriter("output.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(results);
            }
        }

        private void UpdateStatusLabel(string text)
        {
            if (lblStatus.InvokeRequired)
            {
                lblStatus.Invoke(new Action(() => lblStatus.Text = text));
            }
            else
            {
                lblStatus.Text = text;
            }
        }
    }
}
