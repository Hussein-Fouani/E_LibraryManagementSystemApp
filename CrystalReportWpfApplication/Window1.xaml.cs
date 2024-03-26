using System.Windows;

namespace CrystalReportWpfApplication
{
    /// <summary>
    /// Interaction logic for Window1.xaml

    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            reportViewer.Owner = this;
            LoadData();
        }
        private void LoadData()
        {
            CrystalReport1 reportDocument = new CrystalReport1();
            reportDocument.Load(@"CrystalReport1.rep");
            reportViewer.ViewerCore.ReportSource = reportDocument;
        }
    }
}
