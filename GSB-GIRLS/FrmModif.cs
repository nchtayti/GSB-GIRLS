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
    public partial class FrmModif : Accueil
    {
        private GSBgirls maConnexion;
        private Visiteur levisiteur;
        bool fermeture = false;
        public FrmModif()
        {
            InitializeComponent();
            maConnexion = new GSBgirls();
            bsVisiteurs.DataSource = maConnexion.Visiteur.ToList();
        }



        private void btnModif_Click(object sender, EventArgs e)
        {

                var filteredData2 = Modele.MaConnexion.Visiteur.ToList()
               .Where(x => x.idVisiteur == dgvVisiteurs.SelectedRows[0].Cells[6].Value.ToString());

                BindingSource bsmodif = new BindingSource();
                bsmodif.DataSource = filteredData2; // application du filtre
                bsmodif.MoveFirst();

                Visiteur monVisiteur = (Visiteur)bsmodif.Current;

                FrmModifVisiteur fmodifvisiteur = new FrmModifVisiteur(monVisiteur);
                fmodifvisiteur.Show();
                this.Hide();
        }

        private void FModif_Load(object sender, EventArgs e)
        {
            bsVisiteurs.DataSource = maConnexion.Visiteur.ToList();

            if (fermeture) return;
            var LQuery = maConnexion.Visiteur.ToList()
                .Select(x => new
                {
                    x.nom,
                    x.prenom,
                    x.rue,
                    x.cp,
                    x.ville,
                    x.identifiant,
                    x.idVisiteur
                }).OrderBy(x => x.nom);

            bsVisiteurs.DataSource = LQuery;
            //bsVisiteurs.DataSource = maConnexion.Visiteur.OrderBy(x=>x.nom).ToList();

            dgvVisiteurs.DataSource = bsVisiteurs;

            dgvVisiteurs.Columns[0].HeaderText = "Nom";
            dgvVisiteurs.Columns[1].HeaderText = "Prénom";
            dgvVisiteurs.Columns[2].HeaderText = "Adresse";
            dgvVisiteurs.Columns[3].HeaderText = "Code Postal";
            dgvVisiteurs.Columns[4].HeaderText = "Ville";
            dgvVisiteurs.Columns[5].HeaderText = "Identifiant";

            dgvVisiteurs.Columns[6].Visible = false;
            dgvVisiteurs.Rows[0].Selected = true;

            // On cache le menu gestion utilisateur si l'utilisateur a le DROIT a 0
            if (levisiteur.droit == 0)
            {

                btnSupp.Visible = false;
                btnModif.Visible = false;
                btnajouter.Visible = false;
            }


        }

        private void retour_Click(object sender, EventArgs e)
        {
            string message = "Voulez-vous vraiment quitter ?";
            string caption = "Fermeture de l'application";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            // Affichage de la boîte de dialogue 
            result = MessageBox.Show(message, caption, buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                this.Hide();

                /* this.Close();
                 maConnexion.Dispose();  // Pour libérer la connexion à la base de données */
                /*Application.Exit();     // Pour quitter l'application    */
            }
        }
        private void FModif_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Close();
            fermeture = true;
        }
        private void bajouter_Click(object sender, EventArgs e)
        {
            FrmAjoutVisiteur ajout = new FrmAjoutVisiteur();
            ajout.Show();
            this.Hide();
        }

      /*  private void btnSupp_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Voulez-vous vraiment supprimer ce visiteur ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var filteredData = Modele.MaConnexion.Visiteur.ToList()
             .Where(x => x.idVisiteur == dgvVisiteurs.SelectedRows[0].Cells[6].Value.ToString());

                    BindingSource bs = new BindingSource();
                    bs.DataSource = filteredData; // application du filtre
                    bs.MoveFirst();

                    Visiteur monVisiteur = (Visiteur)bs.Current;

                    Modele.MaConnexion.Visiteur.Remove(monVisiteur);
                    Modele.MaConnexion.SaveChanges();

                    FModif modif = new FModif();
                    modif.Show();
                    this.Hide();
                }
                else
                {
                    //
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Suppression erreur : " + ex.ToString(), "Action");
            }
        }*/
    }
}