﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSB_GIRLS
{
    public partial class FInfoVisiteur : Accueil
    {
        private Visiteur visiteurConnect;
        public FInfoVisiteur()
        {
            InitializeComponent();
            visiteurConnect = new Visiteur();
            bsVisiteur.DataSource = Modele.VisiteurConnect;
            var FilteredData = Modele.MaConnexion.Laboratoire.ToList()
                           .Where((x => x.idLabo == (Modele.VisiteurConnect.Laboratoire.idLabo)));

        }



    }
}
