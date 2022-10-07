using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Excel
{
    using Excel = Microsoft.Office.Interop.Excel;
    using System.Reflection;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadData();
            CreateExcel();
        }


        List<Flat> flats;

        RealEstateEntities context = new RealEstateEntities();

        void LoadData()
        {
            flats = context.Flats.ToList();
        }

        Excel.Application xlApp;
        Excel.Workbook xlWB;
        Excel.Worksheet xlSheet;

        void CreateExcel()
        {

            try
            {
                // Excel elindítása és az applikáció objektum betöltése
                xlApp = new Excel.Application();

                // Új munkafüzet
                xlWB = xlApp.Workbooks.Add(Missing.Value);

                // Új munkalap
                xlSheet = xlWB.ActiveSheet;

                // Tábla létrehozása
                CreateTable(); // Ennek megírása a következő feladatrészben következik

                // Control átadása a felhasználónak
                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex) // Hibakezelés a beépített hibaüzenettel
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

                // Hiba esetén az Excel applikáció bezárása automatikusan
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }

        }

        void CreateTable()
        {
            string[] headers = new string[]
            {
                "Kód",
                "Eladó",
                "Oldal",
                "Kerület",
                "Lift",
                "Szobák száma",
                "Alapterület (m2)",
                "Ár (mFt)",
                "Négyzetméter ár (Ft/m2)"
            };

            for (int i = 0; i < headers.Length; i++)
            {
                xlSheet.Cells[1, i + 1] = headers[i];
            }

            object[,] values = new object[flats.Count, headers.Length];


            int c = 0;
            foreach (var f in flats)
            {
                values[c, 0] = f.Code;
                values[c, 1] = f.Vendor;
                values[c, 2] = f.Side;
                values[c, 3] = f.District;
                if (f.Elevator)
                {
                    values[c, 4] = "Van";
                }
                else
                {
                    values[c, 4] = "Nincs";
                }
                values[c, 5] = f.NumberOfRooms;
                values[c, 6] = f.FloorArea;
                values[c, 7] = f.Price;
                values[c, 8] = $"=1000000*{GetCell(c+2, 8)}/{GetCell(c + 2, 7)}";

                c++;
            }

            xlSheet.get_Range(
            GetCell(2, 1),
            GetCell(1 + values.GetLength(0), values.GetLength(1))).Value2 = values;

            Excel.Range headerRange = xlSheet.get_Range(GetCell(1, 1), GetCell(1, headers.Length));
            headerRange.Font.Bold = true;
            headerRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            headerRange.EntireColumn.AutoFit();
            headerRange.RowHeight = 40;
            headerRange.Interior.Color = Color.LightBlue;
            headerRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);



            Excel.Range FullRange = xlSheet.get_Range(GetCell(1, 1), GetCell(flats.Count + 1, 9));
            FullRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

            Excel.Range FirstColumn = xlSheet.get_Range(GetCell(2, 1), GetCell(flats.Count + 1, 1));
            FirstColumn.Font.Bold = true;
            FirstColumn.Interior.Color = Color.LightYellow;

            Excel.Range NegyzetmeterRange = xlSheet.get_Range(GetCell(2, 9), GetCell(flats.Count + 1, 9));
            NegyzetmeterRange.Interior.Color = Color.LightGreen;
            NegyzetmeterRange.NumberFormat = "#,##0.00";
        }

        private string GetCell(int x, int y)
        {
            string ExcelCoordinate = "";
            int dividend = y;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                dividend = (int)((dividend - modulo) / 26);
            }
            ExcelCoordinate += x.ToString();

            return ExcelCoordinate;
        }
    }
}
