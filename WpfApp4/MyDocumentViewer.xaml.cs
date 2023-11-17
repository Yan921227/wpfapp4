using System.Windows;
using System.Windows.Documents;

namespace WpfApp4
{
    /// <summary>
    /// MyDocumentViewer.xaml 的互動邏輯
    /// </summary>
    public partial class MyDocumentViewer : Window
    {
        public MyDocumentViewer()
        {
            InitializeComponent();
        }

        public void New_Executed(object sender,
          System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            MyDocumentViewer myDocumentViewer = new MyDocumentViewer();
            myDocumentViewer.Show();
        
        }

        private void Open_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Open");
        }

        private void Save_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Save");
        }

        private void rtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object property = rtbEditor.Selection.GetPropertyValue (TextElement.FontWeightProperty);
            boldButton.IsChecked = (property is FontWeight && (FontWeight)
            property == FontWeights.Bold);

            property = rtbEditor.Selection.GetPropertyValue (TextElement.FontStyleProperty);
            italicButton.IsChecked = (property is FontStyle && (FontStyle)
            property == FontStyles.Italic);

            property = rtbEditor.Selection.GetPropertyValue(TextElement.FontStyleProperty);
            italicButton.IsChecked = (property is FontStyle && (FontStyle)
            property == FontStyles.Italic);

        }
    }
    }

