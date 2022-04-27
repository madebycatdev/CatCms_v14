using System.Collections.Generic;

namespace EuroCMS.Plugin.Kale.KaleCommon.Models
{
    public class Urun
    {
        private string _UrunID = string.Empty;
        public string UrunID { get { return this._UrunID; } set { this._UrunID = value ?? ""; } }

        private string _UrunAdi = string.Empty;
        public string UrunAdi { get { return this._UrunAdi; } set { this._UrunAdi = value ?? ""; } }

        private string _UrunKodu = string.Empty;
        public string UrunKodu { get { return this._UrunKodu; } set { this._UrunKodu = value ?? ""; } }

        private string _Kalinlik = string.Empty;
        public string Kalinlik { get { return this._Kalinlik; } set { this._Kalinlik = value ?? ""; } }

        private string _Yuzey = string.Empty;
        public string Yuzey { get { return this._Yuzey; } set { this._Yuzey = value ?? ""; } }

        private string _Ebat = string.Empty;
        public string Ebat { get { return this._Ebat; } set { this._Ebat = value ?? ""; } }

        private string _KutuIciMiktar = string.Empty;
        public string KutuIciMiktar { get { return this._KutuIciMiktar; } set { this._KutuIciMiktar = value ?? ""; } }

        private string _KutuIciAdet = string.Empty;
        public string KutuIciAdet { get { return this._KutuIciAdet; } set { this._KutuIciAdet = value ?? ""; } }

        private string _Tasarımci = string.Empty;
        public string Tasarımci { get { return this._Tasarımci; } set { this._Tasarımci = value ?? ""; } }

        private string _UrunAciklama = string.Empty;
        public string UrunAciklama { get { return this._UrunAciklama; } set { this._UrunAciklama = value ?? ""; } }

        private string _UrunEbat = string.Empty;
        public string UrunEbat { get { return this._UrunEbat; } set { this._UrunEbat = value ?? ""; } }

        private string _Video = string.Empty;
        public string Video { get { return this._Video; } set { this._Video = value ?? ""; } }

        private string _Facebook = string.Empty;
        public string Facebook { get { return this._Facebook; } set { this._Facebook = value ?? ""; } }

        private string _Twitter = string.Empty;
        public string Twitter { get { return this._Twitter; } set { this._Twitter = value ?? ""; } }

        private string _SEO = string.Empty;
        public string SEO { get { return this._SEO; } set { this._SEO = value ?? ""; } }

        private string _WhiteBoxLink = string.Empty;
        public string WhiteBoxLink { get { return this._WhiteBoxLink; } set { this._WhiteBoxLink = value ?? ""; } }

        public List<Image> ImageList { get; set; }

        public List<Image> Image { get; set; }

        public List<Varyasyon> VaryasyonList { get; set; }

        public List<Ozellik> OzellikList { get; set; }

        public Renk Renk { get; set; }

        public List<Ebat> EbatList { get; set; }

        public List<Dosya> DosyaList { get; set; }

        private string _MarkaAdi = string.Empty;
        public string MarkaAdi { get { return this._MarkaAdi; } set { this._MarkaAdi = value ?? ""; } }

        public List<Marka> MarkaList { get; set; }

        public Cins Cins { get; set; }
    }
}