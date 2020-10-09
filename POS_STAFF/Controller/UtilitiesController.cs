using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_STAFF.Controller
{
    public class UtilitiesController
    {
        private DataGridView InitializeDataGridView(DataGridView gridView, string[][] datagridViewParams)
        {
            foreach (var dgvP in datagridViewParams)
            {
                gridView.Columns.Add(dgvP[0], dgvP[1]);
            }

            return gridView;
        }

        public static ListView InitializeListView(ListView listView, string[][] listViewParams)
        {

            foreach (var dgvP in listViewParams)
            {
                listView.Columns.Add(dgvP[0], int.Parse(dgvP[1]));
            }

            listView.View = System.Windows.Forms.View.Details;
            listView.GridLines = true;
            listView.FullRowSelect = true;
            listView.HideSelection = false;
            listView.MultiSelect = false;
            listView.Font = new System.Drawing.Font("Century Gothic", 14);

            return listView;
        }
    }
}
