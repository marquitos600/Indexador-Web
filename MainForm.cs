using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using PdfiumViewer;
using System.Xml.Linq;
using AxWMPLib;
using System.Diagnostics;

namespace Indexador
{
    public partial class MainForm : Form
    {
        private List<string> pdfFiles = new List<string>();
        private HashSet<int> blockedFiles = new HashSet<int>(); // Índices de archivos bloqueados
        private int currentIndex = -1;
        private PdfViewer pdfViewer;
        private PdfDocument currentDocument;
        private PdfDocument nullDocument;
        private List<Dictionary<string, string>> metadataList = new List<Dictionary<string, string>>(); // Array para guardar metadatos
        private TextBox[] txtMetaName = new TextBox[5];
        private TextBox[] txtMetaValue = new TextBox[5];
        private Button btnPlayAudio;

        public MainForm()
        {
            InitializeComponent();
            InitializePdfViewer();
            InitializeMetadataControls();
            InitializePlayButton();

            // Inicialización del mediaPlayer después de InitializeComponent
            InitializeMediaPlayer();
        }

        private void InitializePdfViewer()
        {
            pdfViewer = new PdfViewer();
            pdfViewer.Dock = DockStyle.None;
            pdfViewer.Location = new System.Drawing.Point(250, 12); // Ajusta la ubicación según sea necesario
            pdfViewer.Size = new System.Drawing.Size(400, 600); // Ajusta el tamaño según sea necesario
            pdfViewer.BorderStyle = BorderStyle.FixedSingle; // Agregar borde
            this.Controls.Add(pdfViewer);
        }

        private void InitializeMediaPlayer()
        {
            mediaPlayer = new AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(mediaPlayer)).BeginInit();
            mediaPlayer.Dock = DockStyle.None;
            mediaPlayer.Location = new System.Drawing.Point(250, 12); // Ajusta la ubicación según sea necesario
            mediaPlayer.Size = new System.Drawing.Size(400, 600); // Ajusta el tamaño según sea necesario
            mediaPlayer.Visible = false; // Inicialmente oculto
            this.Controls.Add(mediaPlayer); // Agrega el control a los controles del formulario
            ((System.ComponentModel.ISupportInitialize)(mediaPlayer)).EndInit();
            mediaPlayer.settings.autoStart = false; // Desactivar reproducción automática
        }

        private Panel metadataPanel;

        private void InitializeMetadataControls()
        {
            metadataPanel = new Panel();
            metadataPanel.Location = new System.Drawing.Point(690, 40); // Ajusta la ubicación según sea necesario
            metadataPanel.Size = new System.Drawing.Size(240, 230); // Ajusta el tamaño según sea necesario
            metadataPanel.BorderStyle = BorderStyle.FixedSingle; // Agregar borde
            this.Controls.Add(metadataPanel);

            this.lblName = new Label();
            this.lblValue = new Label();
            this.btnCargar = new Button();

            this.lblName.Text = "Nombre";
            this.lblName.Location = new System.Drawing.Point(10, 10);
            this.lblName.Size = new System.Drawing.Size(100, 23);
            metadataPanel.Controls.Add(this.lblName);

            this.lblValue.Text = "Valor";
            this.lblValue.Location = new System.Drawing.Point(130, 10);
            this.lblValue.Size = new System.Drawing.Size(100, 23);
            metadataPanel.Controls.Add(this.lblValue);

            for (int i = 0; i < 5; i++)
            {
                this.txtMetaName[i] = new TextBox();
                this.txtMetaValue[i] = new TextBox();

                this.txtMetaName[i].Location = new System.Drawing.Point(10, 40 + i * 30);
                this.txtMetaName[i].Name = $"txtMetaName{i}";
                this.txtMetaName[i].Size = new System.Drawing.Size(100, 20);
                metadataPanel.Controls.Add(this.txtMetaName[i]);

                this.txtMetaValue[i].Location = new System.Drawing.Point(130, 40 + i * 30);
                this.txtMetaValue[i].Name = $"txtMetaValue{i}";
                this.txtMetaValue[i].Size = new System.Drawing.Size(100, 20);
                metadataPanel.Controls.Add(this.txtMetaValue[i]);
            }

            this.btnCargar.Location = new System.Drawing.Point(80, 200);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(75, 23);
            this.btnCargar.Text = "Cargar";
            this.btnCargar.UseVisualStyleBackColor = true;
            this.btnCargar.Click += new EventHandler(this.btnCargar_Click);
            metadataPanel.Controls.Add(this.btnCargar);
        }

        private void InitializePlayButton()
        {
            btnPlayAudio = new Button();
            btnPlayAudio.Text = "Reproducir Audio";
            btnPlayAudio.Location = new Point(250, 620);
            btnPlayAudio.Size = new Size(150, 30);
            btnPlayAudio.Visible = false; // Inicialmente oculto
            btnPlayAudio.Click += new EventHandler(btnPlayAudio_Click);
            this.Controls.Add(btnPlayAudio);
        }

        private void btnAgregarArchivos_Click(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
            if (pdfFiles.Count >= 100)
            {
                MessageBox.Show("No se pueden agregar más de 100 archivos.");
                return;
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "PDF files (*.pdf)|*.pdf|Audio files (*.mp3)|*.mp3";
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string fileName in openFileDialog.FileNames)
                    {
                        if (pdfFiles.Count >= 100)
                        {
                            MessageBox.Show("Se ha alcanzado el límite de 100 archivos.");
                            break;
                        }

                        if (pdfFiles.Contains(fileName))
                        {
                            MessageBox.Show($"El archivo '{fileName}' ya está en la lista.");
                            continue;
                        }

                        pdfFiles.Add(fileName);
                        listBoxArchivos.Items.Add(Path.GetFileName(fileName));
                        metadataList.Add(new Dictionary<string, string>());
                    }
                }
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                listBoxArchivos.SelectedIndex = currentIndex;
                MostrarArchivoActual();
            }
            else
            {
                MessageBox.Show("No existen más documentos anteriores.");
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (currentIndex < pdfFiles.Count - 1)
            {
                currentIndex++;
                listBoxArchivos.SelectedIndex = currentIndex;
                MostrarArchivoActual();
            }
            else
            {
                MessageBox.Show("No existen más documentos siguientes.");
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                string timestamp = DateTime.Now.ToString("dd_MM_yyyy_HH_mm");
                saveFileDialog.FileName = $"index_{timestamp}.xml";
                saveFileDialog.Filter = "XML files (*.xml)|*.xml";
                saveFileDialog.Title = "Guardar archivo XML";
                saveFileDialog.DefaultExt = "xml";
                saveFileDialog.AddExtension = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string xmlPath = saveFileDialog.FileName;

                    var documentsElement = new XElement("Documents");

                    for (int i = 0; i < pdfFiles.Count; i++)
                    {
                        var docInfo = new XElement("Document",
                            new XElement("FileName", Path.GetFileName(pdfFiles[i])),
                            new XElement("FilePath", pdfFiles[i])
                        );

                        foreach (var metadata in metadataList[i])
                        {
                            docInfo.Add(new XElement(metadata.Key, metadata.Value));
                        }

                        documentsElement.Add(docInfo);
                    }

                    var xmlDoc = new XDocument(documentsElement);
                    try
                    {
                        xmlDoc.Save(xmlPath);
                        MessageBox.Show($"Metadatos exportados a {xmlPath}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al guardar el archivo XML: {ex.Message}");
                    }
                }
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            if (currentIndex < 0 || currentIndex >= metadataList.Count)
            {
                MessageBox.Show("No hay un documento seleccionado.");
                return;
            }

            var metadata = new Dictionary<string, string>();

            for (int i = 0; i < 5; i++)
            {
                var nameControl = txtMetaName[i];
                var valueControl = txtMetaValue[i];
                if (nameControl != null && valueControl != null)
                {
                    string name = nameControl.Text;
                    string value = valueControl.Text;
                    if (!string.IsNullOrEmpty(name) && !ContainsInvalidCharacters(name))
                    {
                        metadata[name] = value;
                    }
                    else if (!string.IsNullOrEmpty(name))
                    {
                        MessageBox.Show($"El nombre del campo '{name}' contiene caracteres inválidos.");
                        return;
                    }
                }
            }

            metadataList[currentIndex] = metadata;
            MessageBox.Show("Metadatos cargados correctamente.");
            if (currentIndex < pdfFiles.Count - 1)
            {
                currentIndex++;
                listBoxArchivos.SelectedIndex = currentIndex;
                MostrarArchivoActual();
            }
            else
            {
                MessageBox.Show("No existen más documentos siguientes.");
            }
            listBoxArchivos.Invalidate(); // Actualiza el color de la lista de archivos
        }

        private void btnPlayAudio_Click(object sender, EventArgs e)
        {
            if (mediaPlayer.Visible && Path.GetExtension(pdfFiles[currentIndex]).ToLower() == ".mp3")
            {
                try
                {
                    mediaPlayer.Ctlcontrols.play();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al reproducir el audio: {ex.Message}");
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Está seguro de que desea limpiar la lista de documentos? Todos los datos guardados se perderán.", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                pdfFiles.Clear();
                metadataList.Clear();
                blockedFiles.Clear();
                listBoxArchivos.Items.Clear();
                currentIndex = -1;
                currentDocument?.Dispose();
                //pdfViewer = new PdfViewer();
                //pdfViewer.Document = null;
                pdfViewer.Visible = false;
                pictureBox2.Visible = true;
                mediaPlayer.Visible = false;
                btnPlayAudio.Visible = false;
                LimpiarCamposMetadata();
                MessageBox.Show("La lista de documentos ha sido limpiada.");
            }
        }

        private void listBoxArchivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxArchivos.SelectedIndex >= 0 && !blockedFiles.Contains(listBoxArchivos.SelectedIndex))
            {
                currentIndex = listBoxArchivos.SelectedIndex;
                MostrarArchivoActual();
                LimpiarCamposMetadata(); // Limpiar campos de metadata
                CargarCamposMetadata(); // Cargar metadatos almacenados
                listBoxArchivos.Invalidate(); // Fuerza el redibujado para aplicar el color
            }
            else if (blockedFiles.Contains(listBoxArchivos.SelectedIndex))
            {
                MessageBox.Show("Este archivo está bloqueado y no puede ser seleccionado.");
            }
        }

        private void MostrarArchivoActual()
        {
            if (currentIndex >= 0 && currentIndex < pdfFiles.Count)
            {
                string currentFile = pdfFiles[currentIndex];

                // Dispone el documento actual si existe antes de cargar uno nuevo
                currentDocument?.Dispose();
                mediaPlayer.Visible = false; // Ocultar el reproductor de medios
                btnPlayAudio.Visible = false; // Ocultar el botón de reproducción

                try
                {
                    if (Path.GetExtension(currentFile).ToLower() == ".pdf")
                    {
                        // Carga y asigna el nuevo documento PDF
                        currentDocument = PdfDocument.Load(currentFile);
                        pdfViewer.Document = currentDocument;
                        pdfViewer.Visible = true;
                    }
                    else if (Path.GetExtension(currentFile).ToLower() == ".mp3")
                    {
                        // Carga y asigna el nuevo archivo MP3 sin reproducirlo automáticamente
                        mediaPlayer.URL = currentFile;
                        mediaPlayer.Visible = true;
                        btnPlayAudio.Visible = true; // Mostrar el botón de reproducción
                        pdfViewer.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    var result = MessageBox.Show($"Archivo corrupto, no es posible abrirlo. ¿Desea eliminar el documento de la lista?\n{ex.Message}", "Error", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        pdfFiles.RemoveAt(currentIndex);
                        listBoxArchivos.Items.RemoveAt(currentIndex);
                        metadataList.RemoveAt(currentIndex);
                    }
                    else
                    {
                        blockedFiles.Add(currentIndex);
                    }
                }
            }
        }

        private void LimpiarCamposMetadata()
        {
            for (int i = 0; i < 5; i++)
            {
                var nameControl = txtMetaName[i];
                var valueControl = txtMetaValue[i];
                if (nameControl != null) nameControl.Text = string.Empty;
                if (valueControl != null) valueControl.Text = string.Empty;
            }
        }

        private void CargarCamposMetadata()
        {
            if (currentIndex >= 0 && currentIndex < metadataList.Count)
            {
                var metadata = metadataList[currentIndex];
                int i = 0;
                foreach (var item in metadata)
                {
                    if (i < 5)
                    {
                        txtMetaName[i].Text = item.Key;
                        txtMetaValue[i].Text = item.Value;
                        i++;
                    }
                }
            }
        }

        private void listBoxArchivos_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();
            var metadata = metadataList[e.Index];
            bool isCompleted = metadata.Any(); // Comprueba si hay metadatos cargados

            if (isCompleted)
            {
                e.Graphics.FillRectangle(System.Drawing.Brushes.LightGreen, e.Bounds);
                e.Graphics.DrawString(listBoxArchivos.Items[e.Index].ToString(), e.Font, System.Drawing.Brushes.Green, e.Bounds);
            }
            else
            {
                if (e.Index == listBoxArchivos.SelectedIndex)
                {
                    e.Graphics.FillRectangle(System.Drawing.Brushes.LightBlue, e.Bounds);
                    e.Graphics.DrawString(listBoxArchivos.Items[e.Index].ToString(), e.Font, System.Drawing.Brushes.Blue, e.Bounds);
                }
                else
                {
                    e.Graphics.DrawString(listBoxArchivos.Items[e.Index].ToString(), e.Font, System.Drawing.Brushes.Black, e.Bounds);
                }
            }
            e.DrawFocusRectangle();
        }

        private bool ContainsInvalidCharacters(string input)
        {
            char[] invalidChars = Path.GetInvalidFileNameChars();
            return input.Any(ch => invalidChars.Contains(ch));
        }

        private void mediaPlayer_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("La funcion de agregar campos esta solo disponible en la version Enterprise. https://mpsolution.netlify.app/");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://mpsolution.netlify.app/") { UseShellExecute = true });
        }
    }
}


