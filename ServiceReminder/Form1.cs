using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace ServiceReminder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            appPath = Application.StartupPath;
        }

        string appPath = "";
        string loadedFile = "";

        List<Element> AllElements = new List<Element>();
        List<string> FloorPathes = new List<string>();

        public void ParseAllElementsFromFile(string path)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path);

            XmlElement xStruct = xDoc.DocumentElement; //xStruct - структура универа
            
            foreach(XmlNode xBuilding in xStruct)
            {
                foreach(XmlNode xCorps in xBuilding)
                {
                    foreach(XmlNode xFloor in xCorps)
                    {
                        FloorPathes.Add(xFloor.Attributes["pathToDrawing"]?.Value ?? "");

                        foreach(XmlNode xElement in xFloor)
                        {
                            Element element = new Element();
                            element.Name = xElement.Attributes["name"]?.Value ?? "";
                            int.TryParse(xElement.Attributes["numberOfFirehose"]?.Value, out element.NumberOfFirehose);
                            int x = 0, y = 0;
                            int.TryParse(xElement.Attributes["X"]?.Value, out x);
                            int.TryParse(xElement.Attributes["Y"]?.Value, out y);
                            element.Position.X = x;
                            element.Position.Y = y;
                            element.Type = (Types) (xElement.Attributes["type"]?.Value[0] ?? 'e');
                            
                            element.Floor = xFloor.Attributes["name"]?.Value;
                            element.Corps = xCorps.Attributes["name"]?.Value;
                            element.Building = xBuilding.Attributes["name"]?.Value;
                            
                            AllElements.Add(element);
                        }
                    }
                }
            }
        }

        private void Button_addNode_Click(object sender, EventArgs e)
        {

        }

        private void Button_RemoveNode_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                loadedFile = Properties.Settings.Default.lastDataFile;

                if (loadedFile != "")
                {
                    ParseAllElementsFromFile(loadedFile);
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ParseAllElementsFromFile("Test.xml");
                return;

                using (var dialog = new OpenFileDialog())
                {
                    dialog.FileName = Properties.Settings.Default.lastDataFile;
                    //dialog.Filter = ".xml";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        ParseAllElementsFromFile(dialog.FileName);
                    }
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }
    }
}
