using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TP4
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        bool pause = false;
        StorageFolder sf = Windows.ApplicationModel.Package.Current.InstalledLocation;

        public MainPage()
        {
            this.InitializeComponent();
            this.LoadListmp3();
            
        }

        private async void Button_Click(object sender, RoutedEventArgs e)  //intercepter et passer la méthode « Click » de votre bouton en « async »
        {

            SyntheseVocal("welcome to music player");
        }

        private async void SyntheseVocal(String sText)  //intercepter et passer la méthode « Click » de votre bouton en « async »
        {
            MediaElement me = new MediaElement(); //créer un objet de type « mediaElement » avec l’opérateur « new »
            var voc = new Windows.Media.SpeechSynthesis.SpeechSynthesizer(); //créer le synthétiseur vocal avec une variable de type « var » instanciée par new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
            Windows.Media.SpeechSynthesis.SpeechSynthesisStream flux = await voc.SynthesizeTextToStreamAsync(sText); //créer un flux vocal « stream » de type « Windows.Media.SpeechSynthesis.SpeechSynthesisStream
            me.SetSource(flux, flux.ContentType); //passer votre flux à votre objet « mediaElement » par la méthode « SetSource »
            me.Play(); // demander à votre objet « mediaElement » de jouer le flux par sa méthode « Play »
                       // Vérifiez que la synthèse fonctionne correctement.

        }

        private async void Play_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            try
            {
                if (pause == false)
                {
                    sf = Windows.ApplicationModel.Package.Current.InstalledLocation;
                    //récupérer le chemin du répertoire d'installation de l'app
                    sf = await sf.GetFolderAsync(@"media");
                    //fait pointer sf sur le répetoire média

                    //Lire partie
                    //StorageFile sFile = await sf.GetFileAsync("music.mp3");
                    //Créer sFile qui pointe sur Extrait.mp3

                    ///
                    //2e partie
                    if (listmp3.SelectedIndex == -1)
                    {
                        MessageDialog msgbox = new MessageDialog("Veuillez sélectionner un fichier!", "Info");
                        await msgbox.ShowAsync();
                        return;
                    }
                    StorageFile sFile = await sf.GetFileAsync(listmp3.SelectedItem.ToString());


                    mediaEl.SetSource(await sFile.OpenAsync(FileAccessMode.Read), sFile.ContentType);
                    //Paramètre :  fichier ouvert en lecture et contenttype du fichier
                    mediaEl.Play();
                }
                else mediaEl.Play();

            }
            catch(Exception ex)
            {
                Windows.UI.Popups.MessageDialog msgbox = new Windows.UI.Popups.MessageDialog("Erreur :" + ex.Message.ToString());
                await msgbox.ShowAsync();
            }

        }

        private void Stop_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            mediaEl.Stop();
        }

        private void Pause_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            mediaEl.Pause();
            pause = true;
        }

        private async void Info_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            //SyntheseVocal("fichier music.mp3");
            Windows.Storage.FileProperties.BasicProperties basicPropetties = await sf.GetBasicPropertiesAsync();
            int iTaille = (int)basicPropetties.Size;
            string filesize = "";
            if (iTaille > 1024)
            {
                iTaille = iTaille / 1024;
                if (iTaille > 1024)
                {
                    iTaille = iTaille / 1024;
                    filesize = iTaille.ToString() + "méga octets";
                }
                else
                {
                    filesize = iTaille.ToString() + "kilo octets";
                }
            }
            else
            {
                filesize = iTaille.ToString() + "octets";
            }

            SyntheseVocal("fichier" + sf.Name + "" + "Taille" + filesize);
        }

        private void volumeslider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            mediaEl.Volume = (double)volumeslider.Value/10 ;
        }

        private async void LoadListmp3()
        {
            StorageFolder Folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            Folder = await Folder.GetFolderAsync(@"media");
            var files = await Folder.GetFilesAsync();
            listmp3.Items.Clear();

            foreach (var filename in files)
            {
                listmp3.Items.Add(filename.Name);
            }
        }

        private async void Mp3_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            SWIoT.ServiceClient sw = new SWIoT.ServiceClient();
            String sRep = await sw.GetMp3FilesAsync();
            list_Mp3.Items.Clear();
            if (sRep.Length > 3)
            {
                while(sRep.Length > 2)
                {
                    String sItem = sRep.Substring(0, sRep.IndexOf("##"));
                    list_Mp3.Items.Add(sItem);
                    sRep = sRep.Substring(sRep.IndexOf("##") + 2);
                }
            }
            if (!StandardPopup.IsOpen)
            {
                StandardPopup.IsOpen = true;
            }
        }
        private async void AddMp3Clicked(object sender, RoutedEventArgs e)
        {
            if (!StandardPopup.IsOpen)
            {
                StandardPopup.IsOpen = false;
            }
            try
            {
                if (list_Mp3.SelectedIndex != -1)
                {
                    StorageFolder Folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                    String sNameFile = list_Mp3.Items[list_Mp3.SelectedIndex].ToString();
                    Uri source = new Uri("http://iot4.lab3il.fr/_mp3/" + sNameFile);
                    Folder = await Folder.GetFolderAsync(@"media");
                    StorageFile destinationFile = await Folder.CreateFileAsync(sNameFile, CreationCollisionOption.GenerateUniqueName);
                    BackgroundDownloader downloader = new BackgroundDownloader();
                    DownloadOperation download = downloader.CreateDownload(source, destinationFile);
                    download.CostPolicy = BackgroundTransferCostPolicy.Always;
                    var downloadTask = download.StartAsync();
                    await downloadTask;
                    LoadListmp3();
                }
            }
            catch (Exception ex)
            {
                MessageDialog msgbox = new MessageDialog("Erreur :" + ex.Message.ToString(), "My App");
                await msgbox.ShowAsync();
            }
        }
        private async void ClosePopupClicked(object sender, RoutedEventArgs e)
        {
            if (StandardPopup.IsOpen)
            {
                StandardPopup.IsOpen = false;
            }
        }
    }
}
