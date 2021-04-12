﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;

namespace AD_FlightGear.Controls
{
    using System.Collections.Generic;
    using System.Windows;
    using OxyPlot;
    /// <summary>
    /// Interaction logic for dataGraph.xaml
    /// </summary>
    public partial class dataGraph : UserControl
    {
        private int length;
        public string Title_Left { get; set; }
        public Button selectedItem { get; set; }
        

        
        public IList<Button> buttons { get; set; }

        private graphs_vm graphs_VM;

        public void set_graphs_VM(graphs_vm graphs_)
        {
            this.graphs_VM = graphs_;
            DataContext = graphs_VM;
        }
        public dataGraph()

        {
            InitializeComponent();
            buttons = new List<Button>();
          //  buttons = graphs_VM.VM_DBflight._ListFeature;
           // data_list.ItemsSource = graphs_VM.VM_DBflight._ListFeature; 
        }
        /*
        public void addButtons()
        {
            length = graphs_VM.VM_DBflight.MapDb.Count;
            for (int i = 0; i < graphs_VM.VM_DBflight.MapDb.Count; i++)
            {
                buttons.Add(new Button { ButtonContent = graphs_VM.VM_DBflight.MapDb[i].Name, ButtonID = (i).ToString() });

            }
            data_list.ItemsSource = buttons;
        }
        */
        /*
        public class Button
        {
            public string ButtonContent { get; set; }
            public string ButtonID { get; set; }
        }
        */
        public void data_list_MouseDoubleClick(Object sender, MouseButtonEventArgs e)
        {
            if (data_list.SelectedItem != null)
            {
                object selectedItem_object = data_list.SelectedItem;
                selectedItem = (Button)selectedItem_object;
                graphs_VM.VM_ChooseIndex = int.Parse(selectedItem.ButtonID);
                graphs_VM.DataPoints_6(int.Parse(selectedItem.ButtonID));


                graphs_VM.VM_C.updateChoose(graphs_VM.VM_PointsRun, graphs_VM.VM_PointsReg, graphs_VM.VM_TimeInt);
            }
        }


        private void ic_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void data_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        //חדש

        public void initializeDll()
        {
            try
            {
                Assembly dll = Assembly.LoadFile(graphs_VM.VM_PathDll);
                Type[] type = dll.GetExportedTypes();
                
                foreach (Type t in type)
                {
                    if (t.Name == "Graph_I")
                    {
                        graphs_VM.VM_C = Activator.CreateInstance(t);
                    }
                }
                DLLgraph.Children.Add(graphs_VM.VM_C.create());
            }
            catch (Exception e)
            {
                Console.WriteLine("Error load dll", e);
            } 
        }
        private void Button_dll(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "dll files (*.dll)|*.dll|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                graphs_VM.VM_PathDll = openFileDialog.FileNames[0];
                initializeDll();
            }
        }

        private void openCsvReg(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "csv files (*.csv)|*.csv";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                graphs_VM.VM_PathCsvReg = openFileDialog.FileNames[0];
            }
            graphs_VM.initDBreg();
        }

        private void openCsvRun(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "csv files (*.csv)|*.csv";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                graphs_VM.VM_PathCsv = openFileDialog.FileNames[0];
            }
            graphs_VM.VM_PathDll = @"C:\Users\Amit\source\repos\circle\circle\bin\Debug\circle.dll";
            initializeDll();

            graphs_VM.initDBrun();
            length = graphs_VM.VM_DBflight.MapDb.Count;
            for (int i = 0; i < graphs_VM.VM_DBflight.MapDb.Count; i++)
            {
                buttons.Add(new Button { ButtonContent = graphs_VM.VM_DBflight.MapDb[i].Name, ButtonID = (i).ToString() });

            }
            data_list.ItemsSource = buttons;
        }
    }
}
