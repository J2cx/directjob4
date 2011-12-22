using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace directjob4
{
    public enum Annonceur
    {
        NotDefine = 0x0000,
    }
    /*
    public static readonly struct Secteurs
    {
        private Dictionary<string, string> items=new Dictionary<string, string>();

        public Dictionary<string, string> Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
            }
        }

        public Secteurs()
        {
            DataTable dtSecteurs=new DataTable();
            dtSecteurs = SQL.GetTable(@"select * from secteurs");
            foreach (DataRow drSecteur in dtSecteurs.Rows)
            {
                Items.Add(drSecteur["secteur"].ToString(), drSecteur["secteur"].ToString());
            }
        }
    }
    */
    class Parametres
    {
        private string urlAccueil="";
        private string address="";
        private string entreprise="";
        private Annonceur tAnnoceur= new Annonceur();
        //private Secteur tSecteur=new Secteur();
        private int maxPage=5;
        private int pageDelay=0;
        private bool accesDirect=true;
        private string inFirstPage="";
        private string exFirstPage="";
        private string inPageSuivant="";
        private string exPageSuivant="";
        private string inAnnonce="";
        private string exAnnonce="";
        private bool fullPage=false;
        private bool goBack = true;
        private string inGoBack="";
        private string exGoBack="";
        private bool onePage=true;
        private bool annExist=false;
        private bool isTaleo=false;
        private string nextAnn="";
        private bool popUp=false;
        private int tri=0;
        private string inTri="";
        private string exTri="";

        private int nbParams = 26;

        #region Properties
		
        public string UrlAcceil
        {
            set 
            {
                urlAccueil = value;
            }
            get
            {
                return urlAccueil;
            }
        }

        public string Address
        {
            set
            {
                address = value;
            }
            get
            {
                return address;
            }
        }

        public string Entreprise
        {
            set
            {
                entreprise = value;
            }
            get
            {
                return entreprise;
            }
        }

        public Annonceur TAnnoceur
        {
            set
            {
                tAnnoceur = value;
            }
            get
            {
                return tAnnoceur;
            }
        }
        /*
        public Secteur TSecteur
        {
            set
            {
                tSecteur = value;
            }
            get
            {
                return tSecteur;
            }
        }
        */
        public int MaxPage
        {
            set
            {
                maxPage = value;
            }
            get
            {
                return maxPage;
            }
        }

        public int PageDelay
        {
            set
            {
                pageDelay = value;
            }
            get
            {
                return pageDelay;
            }
        }

        public bool AccesDirect
        {
            set
            {
                accesDirect = value;
            }
            get
            {
                return accesDirect;
            }
        }

        public string InFirstPage
        {
            set
            {
                inFirstPage = value;
            }
            get
            {
                return inFirstPage;
            }
        }

        public string ExFirstPage
        {
            set
            {
                exFirstPage = value;
            }
            get
            {
                return exFirstPage;
            }
        }
        
        public string InPageSuivant
        {
            set
            {
                inPageSuivant = value;
            }
            get
            {
                return inPageSuivant;
            }
        }

        public string ExPageSuivant
        {
            set
            {
                exPageSuivant = value;
            }
            get
            {
                return exPageSuivant;
            }
        }

        public string InAnnonce
        {
            set
            {
                inAnnonce = value;
            }
            get
            {
                return inAnnonce;
            }
        }

        public string ExAnnonce
        {
            set
            {
                exAnnonce = value;
            }
            get
            {
                return exAnnonce;
            }
        }

        public bool FullPage
        {
            set
            {
                fullPage = value;
            }
            get
            {
                return fullPage;
            }
        }

        public bool GoBack
        {
            set
            {
                goBack = value;
            }
            get
            {
                return goBack;
            }
        }

        public string InGoBack
        {
            set
            {
                inGoBack = value;
            }
            get
            {
                return inGoBack;
            }
        }

        public string ExGoBack
        {
            set
            {
                exGoBack = value;
            }
            get
            {
                return exGoBack;
            }
        }

        public bool OnePage
        {
            set
            {
                onePage = value;
            }
            get
            {
                return onePage;
            }
        }

        public bool AnnExist
        {
            set
            {
                annExist = value;
            }
            get
            {
                return annExist;
            }
        }

        public bool IsTaleo
        {
            set
            {
                isTaleo = value;
            }
            get
            {
                return isTaleo;
            }
        }

        public string NextAnn
        {
            set
            {
                nextAnn = value;
            }
            get
            {
                return nextAnn;
            }
        }

        public bool PopUp
        {
            set
            {
                popUp = value;
            }
            get
            {
                return popUp;
            }
        }

        public int Tri
        {
            set
            {
                tri = value;
            }
            get
            {
                return tri;
            }
        }

        public string InTri
        {
            set
            {
                inTri = value;
            }
            get
            {
                return inTri;
            }
        }

        public string ExTri
        {
            set
            {
                exTri = value;
            }
            get
            {
                return exTri;
            }
        }

        #endregion


        public Parametres()
        { 
            
        }

        public bool InsertParamsIntoSQL()
        {
            return false;
        }

        public bool GetParamsFromSQL()
        {
            return false;
        }
    }
}
