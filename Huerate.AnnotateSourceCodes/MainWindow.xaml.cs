/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace Huerate.AnnotateSourceCodes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            lblPath.Content = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            txtAnnotationText.Text =
                @"Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com";
        }

        private async void btnAnnotate_Click(object sender, RoutedEventArgs e)
        {
            var rootFolder = lblPath.Content.ToString();
            var annotationText = txtAnnotationText.Text;
            var filesCount = await Task.Factory.StartNew(() => Annotate(rootFolder, annotationText), TaskCreationOptions.LongRunning);

            MessageBox.Show(filesCount + " files annotated");
        }

        private int Annotate(string rootFolder, string annotationText)
        {
            var extensions = new HashSet<string>(new[] {".cshtml", ".js", ".scss", ".cs"});

            var files = GetFiles(rootFolder).Where(f => extensions.Contains(Path.GetExtension(f))).ToList();

            foreach (var file in files)
            {
                var extension = Path.GetExtension(file);

                string startComment = "/*";
                string endComment = "*/";

                if (extension == ".cshtml")
                {
                    startComment = "@*";
                    endComment = "*@";
                }

                var text = File.ReadAllText(file, Encoding.UTF8);

                string annotation = string.Format("{0}{1}{2}{1}{3}{1}{1}", startComment, Environment.NewLine, annotationText, endComment);

                if (text.Contains(annotation))
                {
                    File.WriteAllText(file, text, Encoding.UTF8);
                    continue;
                }

                File.WriteAllText(file, annotation + text, Encoding.UTF8);
            }

            return files.Count;
        }

        private IEnumerable<string> GetFiles(string folder)
        {
            foreach (var file in Directory.EnumerateFiles(folder))
            {
                yield return file;
            }

            foreach (var directory in Directory.EnumerateDirectories(folder))
            {
                var dirName = new DirectoryInfo(directory).Name;
                if (dirName == "Properties" || dirName == "bin" || dirName == "obj")
                {
                    continue;
                }

                foreach (var file in GetFiles(directory))
                {
                    yield return file;
                }
            }
        }

        private void btnSelectFolder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                lblPath.Content = dialog.SelectedPath;
            }
        }
    }
}