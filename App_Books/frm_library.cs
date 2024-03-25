using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace App_Books
{
    public partial class frm_library : Form
    {
        public frm_library()
        {
            InitializeComponent();
        }

        static async Task<Cls_books> PostProductoAsync(string jsonData)
        {
            string apiUrl = router.Books;

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();

                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {

                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        Cls_books miModelo = JsonConvert.DeserializeObject<Cls_books>(jsonResponse);
                        MessageBox.Show("Datos enviados correctamente");
                        return miModelo;
                    }
                    else
                    {
                        MessageBox.Show($"Error en la solicitud: {response.StatusCode} - {response.ReasonPhrase}");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                    return null;
                }
            } 
        }





        private async void btn_guardar_Click(object sender, EventArgs e)
        {
            var data = new
            {
                titulo = txt_nombre.Text,
                autor = txt_autor.Text,
                genero = txt_genero.Text,
                anio = int.Parse(txt_anio.Text),
            };
            string jsonData = JsonConvert.SerializeObject(data);
            Cls_books books = await PostProductoAsync(jsonData);
        }
    }
}
