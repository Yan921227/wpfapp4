using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using Microsoft.Win32;
using Xceed.Wpf.Toolkit.Primitives;

namespace WpfApp4
{
    /// <summary>
    /// MyDocumentViewer.xaml 的互動邏輯
    /// </summary>
    public partial class MyDocumentViewer : Window
    {

        Color fontColor = Colors.Black;
       

        public MyDocumentViewer()
        {
            InitializeComponent();
            fontColorPicker.SelectedColor = fontColor;  /*初始化 一開始出現黑色*/

            foreach (var fontFamily in Fonts.SystemFontFamilies)
            {
                fontFamilyComboBox.Items.Add(fontFamily); /*Source字體點下去只有單純的字串*/
            }
            fontFamilyComboBox.SelectedIndex = 8;

            fontSizeComboBox.ItemsSource = new List<Double> { 8, 9, 10, 11, 12, 20, 24, 32, 40, 50, 70 };
            fontSizeComboBox.SelectedIndex = 4;
        }

        public void New_Executed(object sender,
          System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            MyDocumentViewer myDocumentViewer = new MyDocumentViewer();
            myDocumentViewer.Show();

        }

        private void Open_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                TextRange textRange = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                using (FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.Open))
                {
                    textRange.Load(fileStream, DataFormats.Rtf);

                }
            }
        }

        private void Save_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*"
            };
            if ( saveFileDialog.ShowDialog() == true)
            {
                TextRange textRange = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    textRange.Load(fileStream, DataFormats.Rtf);

                }
            }
        }

        private void rtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {    //依據所選取文字的字體粗細，同步更新boldButton的狀態
            object property = rtbEditor.Selection.GetPropertyValue(TextElement.FontWeightProperty);
            boldButton.IsChecked = (property is FontWeight && (FontWeight)
            property == FontWeights.Bold);

            //依據所選取文字的字體是否加斜體，同步更新italicButton的狀態
            property = rtbEditor.Selection.GetPropertyValue(TextElement.FontStyleProperty);
            italicButton.IsChecked = (property is FontStyle && (FontStyle)
            property == FontStyles.Italic);

            //依據所選取文字的字體是否加底線，同步更新underlineButton的狀態
            property = rtbEditor.Selection.GetPropertyValue(TextElement.FontStyleProperty);
            italicButton.IsChecked = (property is FontStyle && (FontStyle)
            property == FontStyles.Italic);

            //依據所選取文字的字體大小，同步更fontSizeButton的狀態
            property = rtbEditor.Selection.GetPropertyValue(TextElement.FontSizeProperty);
            fontSizeComboBox.SelectedItem = property;

            //依據所選取文字的字形，同步更新fontFamilyCombox的狀態
            property = rtbEditor.Selection.GetPropertyValue(TextElement.FontFamilyProperty);
            fontFamilyComboBox.SelectedItem = property;

            //依據所選取文字的字形，同步更新fontColorPicker的狀態
            SolidColorBrush? forgroundProperty = rtbEditor.Selection .GetPropertyValue(TextElement.ForegroundProperty) as SolidColorBrush;
        }

        private void fontFamilyComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (fontFamilyComboBox.SelectedItem != null)
            {
                rtbEditor.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, fontFamilyComboBox.SelectedItem);/*回傳字型*/
            }

        }

        private void fontSizeComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (fontSizeComboBox.SelectedItem != null)
            {
                rtbEditor.Selection.ApplyPropertyValue(TextElement.FontSizeProperty,fontSizeComboBox.SelectedItem);
            }
        }

        private void fontColorPicker_SelectionChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            fontColor = (Color)fontColorPicker.SelectedColor;
            SolidColorBrush fontBrush = new SolidColorBrush(fontColor);
            rtbEditor.Selection.ApplyPropertyValue(TextElement.ForegroundProperty , fontBrush);
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        { 
          rtbEditor.Document.Blocks.Clear();
        }
    }
    }

