using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TP_WinIoT_4
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            SyntheseVocalAsync("Welcome to media player");
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            MediaElement ob = new MediaElement();
            var voc = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
            Windows.Media.SpeechSynthesis.SpeechSynthesisStream stream = await voc.SynthesizeTextToStreamAsync("Welcome");
            ob.SetSource(stream, stream.ContentType);
        }

        public async void SyntheseVocalAsync(String text)
        {
            MediaElement ob = new MediaElement();
            var voc = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
            Windows.Media.SpeechSynthesis.SpeechSynthesisStream stream = await voc.SynthesizeTextToStreamAsync(text);
            ob.SetSource(stream, stream.ContentType);
        }

        private async void Play_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            StorageFolder sf = Windows.ApplicationModel.Package.Current.InstalledLocation;
            sf = await sf.GetFoldersAsync(@"media");
            StorageFile sF = await sf.GetFileAsync("music.mp3");
        }
    }
}
